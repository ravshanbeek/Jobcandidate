using System.ComponentModel.DataAnnotations;

namespace Jobcandidate.Application;

public static class CandidateValidations
{
    public static List<(string PropertyName, string ErrorMessage)> ValidateCandidate(CandiateCreateOrModifyDto dto)
    {
        var errors = new List<(string PropertyName, string ErrorMessage)>();

        if (dto is null)
        {
            errors.Add((nameof(dto), $"{nameof(dto)} cannot be null"));
            return errors;
        }

        if (string.IsNullOrWhiteSpace(dto.FirstName))
            errors.Add((nameof(dto.FirstName), $"{nameof(dto.FirstName)} cannot be null"));

        if (string.IsNullOrWhiteSpace(dto.LastName))
            errors.Add((nameof(dto.LastName), $"{nameof(dto.LastName)} cannot be null"));

        if (string.IsNullOrWhiteSpace(dto.Comments))
            errors.Add((nameof(dto.Comments), $"{nameof(dto.Comments)} cannot be null"));

        var email = new EmailAddressAttribute();
        if (string.IsNullOrEmpty(dto.Email) || !email.IsValid(dto.Email))
            errors.Add((nameof(dto.Email), $"{nameof(dto.Email)} is not a valid email address"));

        var phone = new PhoneAttribute();
        if (string.IsNullOrEmpty(dto.PhoneNumber) || !phone.IsValid(dto.PhoneNumber))
            errors.Add((nameof(dto.PhoneNumber), $"{nameof(dto.PhoneNumber)} is not a valid phone number"));

        return errors;
    }

}
