using client.core.Common;

namespace client.core
{
    public class Person: BaseModel
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string Identification { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public Client Client { get; set; }
    }
}