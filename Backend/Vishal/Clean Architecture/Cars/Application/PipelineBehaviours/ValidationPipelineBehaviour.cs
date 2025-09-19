using Application.Exceptions;
using Application.PipelineBehaviours.Contracts;
using FluentValidation;
using MediatR;

//Custom pipeline
namespace Application.PipelineBehaviours
{
    //for every request mediator when we are going to call from controller with send the below pipeline will run 
    public class ValidationPipelineBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>, IValidatable
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationPipelineBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if(_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                List<string> errors = new();
                var validationResults = await Task
                    .WhenAll(
                    _validators
                       .Select(x => x.ValidateAsync(context, cancellationToken)));
                var failures = validationResults.SelectMany(r => r.Errors)
                    .Where(f => f != null)
                    .ToList();
                if(failures.Count != 0)
                {
                    foreach(var failure in failures)
                    {
                        errors.Add(failure.ErrorMessage);         //this error msgs will be error msgs that will create when we create failures validations
                    }
                    throw new CustomValidationException(errors, "one or more validation failure(s) occured.");
                }
            }
            return await next();   //if the request has no validation then, this line will say to pipeline to continue to next operation in line 

        }
    }
}
