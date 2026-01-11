using FluentValidation.Results;
using Shared;

namespace DirectoryService.Application.Extensions
{
    public static class ValidationExtensions
    {
        public static Errors ToErrors(this ValidationResult validationResult)
        {
            return validationResult.Errors.Select(e => Error.Validation(
                    e.ErrorCode,
                    e.ErrorMessage,
                    e.PropertyName)).ToList();
        }
    }
}
