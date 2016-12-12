using System;

namespace _2_zadatak
{
    public class TodoAccessDeniedException : Exception
    {
        public TodoAccessDeniedException(string message) : base(message)
        {

        }
    }

    class DuplicateTodoItemException : Exception
    {
        public DuplicateTodoItemException(Guid id) : base("duplicate id: {" + id.ToString() + "}")
        {

        }
    }
}
