using System.Collections.Generic;
using TimeLogger.Application.Customers;

namespace TimeLogger.UI.Models.Customers
{
    public class CustomersByFreelancerViewModel
    {
        public IList<CustomerModel> Customers { get; set; }

        public CustomersByFreelancerViewModel()
        {
            Customers = new List<CustomerModel>();
        }

        public CustomersByFreelancerViewModel(IList<CustomerModel> customers)
        {
            Customers = customers;
        }
    }
}
