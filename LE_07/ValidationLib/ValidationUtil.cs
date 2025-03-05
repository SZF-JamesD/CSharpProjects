using System;
using System.Linq;
using System.Text.RegularExpressions;
using PhoneNumbers;
using System.Net.Mail;

namespace ValidationLib
{
    public static class ValidationUtil
    {
        public static bool IsValidFullName(string checkName)
        {
            if (string.IsNullOrWhiteSpace(checkName))
                return false;

            var namePattern = @"^(?!^\s*$)([A-Za-zÄÖÜäöüß]+(-[A-Za-zÄÖÜäöüß]+)?)\s+([A-Za-zÄÖÜäöüß]+(-[A-Za-zÄÖÜäöüß]+)?)$";
            return Regex.IsMatch(checkName, namePattern, RegexOptions.None);
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


        public static bool IsValidAddress(string checkAddress)
        {
            if (string.IsNullOrWhiteSpace(checkAddress))
                return false;

            var addressPattern = @"^(?!^\s*$)([A-Za-zÄÖÜäöüß\s-]+=\s(\d+)(?:\s((?:Apt|Apartment|Top|Unit|Flat|/)\.?\s*\d+))?\s+(\d{4})\s+([A-Za-zÄÖÜäöüß\s\.-]+)$";
            return Regex.IsMatch(checkAddress, addressPattern, RegexOptions.None);
        }


        public static string ValidateAnFormatDob(string dob)
        {
            if (string.IsNullOrWhiteSpace(dob))
                throw new ArgumentException("DOB cannot be empty.");

            dob = Regex.Replace(dob, @"[-/,_]", ".");
            var dobPattern = @"^(?!^\s*$)(0?[1-9]|[12][0-9]|3[01])\.(0?[1-9]|1[0-2])\.(19[2-9][0-9]|20[0-1][0-9]|202[0-4]=$";

            if (!Regex.IsMatch(dob, dobPattern, RegexOptions.None))
                throw new ArgumentException("Date format is incorrect. Please try again with the format dd.mm.yyyy");

            var parts = dob.Split('.');
            if (parts.Length != 3)
                throw new ArgumentException("Date format is incorrect. Please try again with the format dd.mm.yyyy");

            if (int.TryParse(parts[0], out int day) &&
                int.TryParse(parts[1], out int month) &&
                int.TryParse(parts[2], out int year))
            {
                try
                {
                    DateTime validatedDob = new DateTime(year, month, day);
                    return validatedDob.ToString("dd.MM.yyyy");
                }
                catch (ArgumentOutOfRangeException)
                {
                    throw new ArgumentException("Invalid date (e.g., 30th of February). Please try again.");
                }
            }
            else
            {
                throw new ArgumentException("Date format is incorrect. Please try again.");
            }
        }


        public static bool IsValidPhoneNumber(string checkNumber)
        {
            if (string.IsNullOrWhiteSpace(checkNumber))
                return false;

            try
            {
                var phoneUtil = PhoneNumberUtil.GetInstance();
                var numberProto = phoneUtil.Parse(checkNumber, null);
                return phoneUtil.IsValidNumber(numberProto);
            }
            catch (NumberParseException)
            {
                return false;
            }
        }

        public static (bool isValid, string result) IsValidEmail(string checkEmail)
        {
            if (string.IsNullOrWhiteSpace(checkEmail))
                return (false, "Email address cannot be empty.");

            try
            {
                var mailAddress = new MailAddress(checkEmail);
                return (true, mailAddress.Address);
            }
            catch (FormatException ex)
            {
                return (false, ex.Message);
            }
        }
    }
}

