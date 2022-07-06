using System;

namespace client.application.Execptions
{
    public class NotFoundExepction: ApplicationException
    {
        public NotFoundExepction(string name, object key) : base($"Entity \"{name}\" ({key})  no fue encontrado")
        {
        }
    }
}