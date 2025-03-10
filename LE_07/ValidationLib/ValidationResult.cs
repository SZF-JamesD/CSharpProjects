﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValidationLib
{
    public class ValidationResult<T>
    {
        public bool IsValid { get; }
        public T Value { get; }
        public string ErrorMessage { get; }

        private ValidationResult(bool isValid, T value, string errorMessage)
        {
            IsValid = isValid;
            Value = value;
            ErrorMessage = errorMessage;
        }

        public static ValidationResult<T> Success(T value) => new ValidationResult<T>(true, value, null);

        public static ValidationResult<T> Failure(string errorMessage) => new ValidationResult<T>(false, default(T), errorMessage);
    }
}
