using FluentValidation.Results;

namespace PMS.OrganizationService.Application.Exceptions;

public class ValidationException : Exception
{
    public List<string> ValidationErrors { get; set; }

    public ValidationException(ValidationResult validationResult)
    {
        ValidationErrors = new List<string>();

        foreach (var validationError in validationResult.Errors)
        {
            ValidationErrors.Add(validationError.ErrorMessage);
        }
    }
}