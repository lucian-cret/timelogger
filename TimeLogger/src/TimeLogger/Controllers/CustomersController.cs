using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TimeLogger.Application.Customers;
using TimeLogger.UI.Models.Customers;

namespace TimeLogger.UI.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ICustomersService _customersService;

        public CustomersController(ICustomersService customersService)
        {
            _customersService = customersService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomers(int freelancerId)
        {
            var customers = await _customersService.GetCustomersByFreelancer(freelancerId);
            var viewModel = new CustomersByFreelancerViewModel(customers);
            return View(viewModel);
        }
    }
}
