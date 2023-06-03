using System;

namespace GherkinWebAPI.CustomExceptions
{
    public class CustomException : Exception
    {
        public CustomException(string message) : base(message)
        {
        }
    }
}