using System.Collections.Generic;

namespace TimeLogger.Domain.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int FreelancerId { get; set; }
        public ICollection<Project> Projects { get; set; }
    }
}
