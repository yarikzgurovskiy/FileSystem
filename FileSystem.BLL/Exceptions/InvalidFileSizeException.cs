using System;
using System.Collections.Generic;
using System.Text;

namespace FileSystem.BLL.Exceptions {
    public class InvalidFileSizeException : Exception {
        public InvalidFileSizeException() { }

        public InvalidFileSizeException(string message) : base(message) { }

        public InvalidFileSizeException(string message, Exception inner)
            : base(message, inner) { }

    }
}
