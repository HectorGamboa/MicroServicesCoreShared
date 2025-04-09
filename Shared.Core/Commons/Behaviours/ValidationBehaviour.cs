using FluentValidation;
using MediatR;
using Shared.Core.Commons.Bases;
using ValidationException = Shared.Core.Commons.Exceptions.ValidationException;

namespace Shared.Core.Commons.Behaviours
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);
                var validationResult = await Task.WhenAll(_validators.Select(x => x.ValidateAsync(context, cancellationToken)));
                var failures = validationResult.
                    Where(x => x.Errors.Any()).
                    SelectMany(x => x.Errors).
                    Select(x => new BaseError()
                    {
                        PropertyName = x.PropertyName,
                        ErrorMessage = x.ErrorMessage,
                    }).ToList();
                if (failures.Any())
                {
                    throw new ValidationException(failures);
                }
            }
            return await next();
        }
    }
}
