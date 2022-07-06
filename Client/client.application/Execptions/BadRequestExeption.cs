using System;

namespace client.application.Execptions
{
    public class BadRequestExeption: ApplicationException
    {
        public BadRequestExeption(string message) : base(message)
        {
        }
    }
}