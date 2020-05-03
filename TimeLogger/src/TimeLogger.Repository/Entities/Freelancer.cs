using System.Collections.Generic;

namespace TimeLogger.Domain.Entities
{
    public class Freelancer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Customer> Customers { get; set; }
    }
}
