using System;

namespace ConsoleAppArquiSoftDao02
{
    public class DAOException : Exception
    {
        public DAOException(string message) : base(message)
        {
        }

        public DAOException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

}