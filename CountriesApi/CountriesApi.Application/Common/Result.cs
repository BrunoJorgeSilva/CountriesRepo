using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountriesApi.Application.Common
{
    public class Result<T>
    {
        public T? Value { get; private set; }
        public string? ErrorMessage { get; private set; }
        public bool IsSuccess { get; private set; }

        private Result(T value)
        {
            Value = value;
            IsSuccess = true;
        }

        private Result(string errorMessage, T empty)
        {
            ErrorMessage = errorMessage;
            IsSuccess = false;
            Value = empty;
        }

        public static Result<T> Success(T value) => new Result<T>(value);

        public static Result<T> Failure(string errorMessage, T empty) => new Result<T>(errorMessage, empty);
    }
}
