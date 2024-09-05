using FluentValidation.Results;
using May.Errors;

namespace May.FluentValidation;

public static class FluentValidationResultExtensions
{
    public static List<Error> AsErrors(this ValidationResult valResult) => valResult.Errors.Select(valFailure => Error.Invalid(valFailure.ErrorCode, valFailure.ErrorMessage)).ToList();
}