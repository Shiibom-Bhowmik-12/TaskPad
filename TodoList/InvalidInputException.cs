using System;

namespace TodoList
{
    public class InvalidInputException : Exception
    {
        //invalid input exception
        public InvalidInputException(string message) : base(message)
        { }
    }
}
