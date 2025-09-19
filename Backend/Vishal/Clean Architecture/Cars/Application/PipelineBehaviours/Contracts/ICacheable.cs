

namespace Application.PipelineBehaviours.Contracts
{

    public interface ICacheable
    {
        public string CacheKey { get; set; }
        public bool ByPassCache { get; set; }
        public TimeSpan? SlidingExpiration { get; set; }
    }
}






//In this we use interface to segregat btw which pipeline to run and which not.
//it's an interface that is going to say you have to cache for this specific request
//we'll use this interface, to separate btw which request qualify to be cached & which ones do not.