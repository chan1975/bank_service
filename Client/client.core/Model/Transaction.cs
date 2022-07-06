using System;
using client.core.Common;

namespace client.core
{
    public class Transaction: BaseModel
    {
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public int Amount { get; set; }
        public int Balance { get; set; }
        public int AccountId { get; set; }
    }
}