using System;

namespace projectmanagement.exception
{
    internal class ProjectNotFoundException : Exception
    {
        // Default constructor
        public ProjectNotFoundException() : base("Project not found.")
        {
        }

        // Parameterized constructor to allow custom error messages
        public ProjectNotFoundException(string message) : base(message)
        {
        }

        // Parameterized constructor to pass custom message and inner exception
        public ProjectNotFoundException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
