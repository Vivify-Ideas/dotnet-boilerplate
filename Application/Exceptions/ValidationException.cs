using FluentValidation.Results;


namespace Application.Exceptions
{
    public class ValidationException : ApplicationException
    {
        public List<string> ValidationErrors { get; set; }

        public ValidationException(IEnumerable<ValidationFailure> failures)
        {
            ValidationErrors = new List<string>();

            foreach (var validationError in failures)
            {
                ValidationErrors.Add(validationError.ErrorMessage);
            }
        }
    }
}
