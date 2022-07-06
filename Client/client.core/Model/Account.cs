using client.core.Common;

namespace client.core
{
    public class Account: BaseModel
    {
        public int Number { get; set; }
        public string Type { get; set; }
        public int Balance { get; set; }
        public bool IsActive { get; set; }
        public int ClientId { get; set; }
    }
}