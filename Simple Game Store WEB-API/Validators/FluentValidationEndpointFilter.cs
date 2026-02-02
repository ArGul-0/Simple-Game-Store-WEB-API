using FluentValidation;

namespace Simple_Game_Store_WEB_API.Validators
{
    public class FluentValidationEndpointFilter<T> : IEndpointFilter where T : class // Generic Endpoint Filter For FluentValidation
    {
        private readonly IValidator<T> validator;

        public FluentValidationEndpointFilter(IValidator<T> validator)
        {
            this.validator = validator;
        }

        /// <summary>
        /// Invokes the filter to validate the DTO using FluentValidation.
        /// </summary>
        /// <returns></returns>
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var DTO = context.Arguments.OfType<T>().FirstOrDefault(); // Get the DTO from the context arguments
            if (DTO is not null)
            {
                var result = await validator.ValidateAsync(DTO, context.HttpContext.RequestAborted); // Validate the DTO
                if (!result.IsValid)
                {
                    var errors = result.Errors // Collect validation errors
                        .GroupBy(e => e.PropertyName)
                        .ToDictionary(g => g.Key ?? string.Empty, g => g.Select(e => e.ErrorMessage).ToArray());

                    return Results.ValidationProblem(errors); // Return validation problem result
                }
            }

            return await next(context); // Proceed to the next filter or endpoint
        }
    }
}
