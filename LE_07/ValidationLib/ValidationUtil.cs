using System;
using System.Linq;
using System.Text.RegularExpressions;
using PhoneNumbers;
using System.Net.Mail;

namespace ValidationLib
{
    public static class ValidationUtil
    {
        public static ValidationResult<string> IsValidFullName(string checkName)
        {
            if (string.IsNullOrWhiteSpace(checkName))
                return ValidationResult<string>.Failure("Full name cannot be empty.");

            var namePattern = @"^(?!^\s*$)([A-Za-zÄÖÜäöüß]+(-[A-Za-zÄÖÜäöüß]+)?)\s+([A-Za-zÄÖÜäöüß]+(-[A-Za-zÄÖÜäöüß]+)?)$";
            if (!Regex.IsMatch(checkName, namePattern))
                return ValidationResult<string>.Failure("Invalid full name format.");

            return ValidationResult<string>.Success(CapitalizeName(checkName));
        }


        public static string CapitalizeName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return name;

            string CapitalizeHyphen(string part)
            {
                return string.Join("-", part.Split('-').Select(word =>
                word.Length > 0 ? char.ToUpper(word[0]).ToString() + word.Substring(1).ToLower() : word));
            }

            var parts = name.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            return string.Join(" ", parts.Select(part => CapitalizeHyphen(part)));
        }


        public static ValidationResult<AddressParts> IsValidAddress(string checkAddress)
        {
            if (string.IsNullOrWhiteSpace(checkAddress))
                return ValidationResult<AddressParts>.Failure("Address cannot be empty.");

            var addressPattern = @"^(?<Street>[A-Za-zÄÖÜäöüß\s-]+)\s(?<Number>\d+)(?:\s(?<Apartment>(?:Apt|Apartment|Top|Unit|Flat|/)\.?\s*\d+))?\s+(?<PostalCode>\d{4})\s+(?<City>[A-Za-zÄÖÜäöüß\s\.-]+)$";
            var match = Regex.Match(checkAddress, addressPattern);
            if (!match.Success)
                return ValidationResult<AddressParts>.Failure("Invalid address format, please follow the Street, nr(optionally with apt number), Postal Code(4 Numbers), City.");

            var parts = new AddressParts
            {
                Street = match.Groups["Street"].Value.Trim(),
                Number = match.Groups["Number"].Value.Trim(),
                Apartment = match.Groups["Apartment"].Success ? match.Groups["Apartment"].Value.Trim() : null,
                PostalCode = match.Groups["PostalCode"].Value.Trim(),
                City = match.Groups["City"].Value.Trim()
            };


            return ValidationResult<AddressParts>.Success(parts);
        }


        public static ValidationResult<string> ValidateAnFormatDob(string dob)
        {
            if (string.IsNullOrWhiteSpace(dob))
                return ValidationResult<string>.Failure("DOB cannot be empty.");

            dob = Regex.Replace(dob, @"[-/,_]", ".");
            var dobPattern = @"^(?!^\s*$)(0?[1-9]|[12][0-9]|3[01])\.(0?[1-9]|1[0-2])\.(19[2-9][0-9]|20[0-1][0-9]|202[0-4]$";

            if (!Regex.IsMatch(dob, dobPattern, RegexOptions.None))
                return ValidationResult<string>.Failure("Date format is incorrect. Please try again with the format dd.mm.yyyy");

            var parts = dob.Split('.');
            if (parts.Length != 3)
                return ValidationResult<string>.Failure("Date format is incorrect. Please try again with the format dd.mm.yyyy");

            if (int.TryParse(parts[0], out int day) &&
                int.TryParse(parts[1], out int month) &&
                int.TryParse(parts[2], out int year))
            {
                try
                {
                    DateTime validatedDob = new DateTime(year, month, day);
                    return ValidationResult<string>.Success(validatedDob.ToString("dd.MM.yyyy"));
                }
                catch (ArgumentOutOfRangeException)
                {
                    return ValidationResult<string>.Failure("Invalid date (e.g., 30th of February). Please try again.");
                }
            }
            else
            {
                return ValidationResult<string>.Failure("Date format is incorrect. Please try again.");
            }
        }


        public static ValidationResult<string> IsValidPhoneNumber(string checkNumber)
        {
            if (string.IsNullOrWhiteSpace(checkNumber))
                return ValidationResult<string>.Failure("Phone number cannot be empty.");

            try
            {
                var phoneUtil = PhoneNumberUtil.GetInstance();
                var numberProto = phoneUtil.Parse(checkNumber, null);
                if (phoneUtil.IsValidNumber(numberProto))
                {
                    string formattedNumber = phoneUtil.Format(numberProto, PhoneNumberFormat.E164);
                    return ValidationResult<string>.Success(formattedNumber);
                }
                else
                {
                    return ValidationResult<string>.Failure($"Invalid number: {checkNumber}");
                }
            }
            catch (NumberParseException ex)
            {
                return ValidationResult<string>.Failure("Invalid phone number: " + ex.Message);
            }
        }

        public static ValidationResult<string> IsValidEmail(string checkEmail)
        {
            if (string.IsNullOrWhiteSpace(checkEmail))
                return ValidationResult<string>.Failure("Email address cannot be empty.");

            try
            {
                var mailAddress = new MailAddress(checkEmail);
                return ValidationResult<string>.Success(mailAddress.Address);
            }
            catch (FormatException ex)
            {
                return ValidationResult<string>.Failure("Invalid email address: " + ex.Message);
            }
        }
    }
}

