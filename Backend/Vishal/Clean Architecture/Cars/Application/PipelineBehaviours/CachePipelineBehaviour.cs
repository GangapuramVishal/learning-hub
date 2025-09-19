using Application.Models;
using Application.PipelineBehaviours.Contracts;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;

//Custom pipeline
namespace Application.PipelineBehaviours
{
    public class CachePipelineBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>    //generic type, it's a mediator pipeline behaviour
        where TRequest : IRequest<TResponse>, ICacheable                                                //must implement IRequest&ICacheable                   //any TRequest that is of type IRequest from mediator - that's the once this pipeline going to run for.
    {
        private readonly IDistributedCache _cache;

        private readonly CacheSettings _cacheSettings;
        public CachePipelineBehaviour(IDistributedCache cache, IOptions<CacheSettings> cacheSettings)        // for Cahchesetting we are not directly injecting instead using IOptions
        {
            _cache = cache;
            _cacheSettings = cacheSettings.Value;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (request.ByPassCache) return await next();                           //if it is bypass then this going to pass for next request un pipeline  

            TResponse response;
            string cacheKey = $"{_cacheSettings.ApplicationName}: {request.CacheKey}";    //to enchance our cachekey by adding ApplicationName bcoz we are using one server
            var cacheResponse = await _cache.GetAsync(cacheKey, cancellationToken);

            if(cacheResponse != null) 
            {
                 //In cache we store values in Dictionary(Key:value) so we have to deserialie into a type we want
                response = JsonConvert.DeserializeObject<TResponse>(Encoding.Default.GetString(cacheResponse));
            }
            else
            {
                //Get the response and write to cache
                response = await GetResponseandWriteToCacheAsync();

            }

            return response;

            async Task<TResponse> GetResponseandWriteToCacheAsync()
            {
                response = await next();

                if(response != null)
                {
                    var slidingExpiration = request.SlidingExpiration == null ?
                        request.SlidingExpiration :
                        TimeSpan.FromMinutes(_cacheSettings.SlidingExpiration);
                    var cacheOptions = new DistributedCacheEntryOptions
                    {
                        SlidingExpiration = slidingExpiration,
                        AbsoluteExpiration = DateTime.Now.AddDays(1)
                    };

                    var serializedData = Encoding.Default
                        .GetBytes(
                            JsonConvert
                            .SerializeObject(response,
                                Formatting.Indented,
                                new JsonSerializerSettings()
                                {
                                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                                }));
                    await _cache.SetAsync(cacheKey, serializedData, cacheOptions, cancellationToken);
                }
                return response;
            }
        }
    }
}






















//we need to isolate when will this pipeline to be executed.
//bcoz we dont have to run the pipeline for all the mediator request.
//we need to have separation btw these pipeline bcoz if we got many pipeline then all pipelines will run - which effect performance
//so we to give conditions which pipeline should run in which condition
// so we create a contracts folder in this application for above reasons

//in the above we marke the mediator request that we want them to cache.
//we mark them with interface ICacheable - must implement 
//this is one of the ways to seprate to avoid running all our pipeline 