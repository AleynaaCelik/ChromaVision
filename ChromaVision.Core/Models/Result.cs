using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChromaVision.Core.Models
{
    public class Result
    {
        public bool Succeeded { get; }
        public IReadOnlyList<string> Errors { get; }

        protected Result(bool succeeded, IEnumerable<string>? errors = null)
        {
            Succeeded = succeeded;
            Errors = errors?.ToArray() ?? new string[0];
        }

        public static Result Success()
        {
            return new Result(true);
        }

        public static Result Failure(IEnumerable<string> errors)
        {
            return new Result(false, errors);
        }

        public static Result Failure(string error)
        {
            return new Result(false, new[] { error });
        }
    }

    public class Result<T> : Result
    {
        public T? Data { get; }

        protected Result(T? data, bool succeeded, IEnumerable<string>? errors = null)
            : base(succeeded, errors)
        {
            Data = data;
        }

        public static Result<T> Success(T data)
        {
            return new Result<T>(data, true);
        }

        public new static Result<T> Failure(IEnumerable<string> errors)
        {
            return new Result<T>(default, false, errors);
        }

        public new static Result<T> Failure(string error)
        {
            return new Result<T>(default, false, new[] { error });
        }
    }

}
