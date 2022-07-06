using client.core.Common;

namespace client.core
{
    public class Client : BaseModel
    {
        public string Password { get; set; }
        public bool Status { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; }
    }
}