using FluentValidation;
using MediatR;
using System.Data;

namespace Drug_Procurement.Behaviours;

public class ValidationBehaviour<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validator;

    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validator)
    {
        _validator = validator;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if(_validator.Any())
        {
            var context = new ValidationContext<TRequest>(request);
            var validationResults = await Task.WhenAll(_validator.Select(x => x.ValidateAsync(context, cancellationToken)));
            var failures = validationResults.SelectMany(a => a.Errors).Where(f => f != null).ToList();
            if (failures.Any()) 
            { 
                throw new ExceptionHandlers.ValidationException(failures);
            }
        }
        return await next();
    }
}
