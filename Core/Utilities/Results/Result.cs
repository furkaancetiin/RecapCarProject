using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class DataResult : IResult
    {
        public DataResult(bool success,string message):this(success)
        {            
            Message = message;
        }

        public DataResult(bool success)
        {
            Success = success;
        }
        public bool Success { get; }

        public string Message { get; }
    }
}
