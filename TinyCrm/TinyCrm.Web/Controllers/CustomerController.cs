﻿using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TinyCrm.Core.Data;
using TinyCrm.Core.Services;
using TinyCrm.Core.Services.Options;

namespace TinyCrm.Web.Controllers
{
    [Route("customer")]
    public class CustomerController : Controller
    {
        private TinyCrmDbContext dbContext_;
        private ICustomerService customerService_;

        public CustomerController()
        {
            dbContext_ = new TinyCrmDbContext();
            customerService_ = new CustomerService(dbContext_);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateCustomerOptions options)
        {
            var result = customerService_.CreateCustomer(options);

            if (result == null) {
                return BadRequest();
            }

            return Json(result);
        }

        [HttpGet]
        public IActionResult Index()
        {
            var customerList = customerService_
                .SearchCustomers(new SearchCustomerOptions())
                .ToList();

            return Json(customerList);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int? id)
        {
            if (id == null) {
                return BadRequest();
            }

            var customer = customerService_
                .SearchCustomers(
                    new SearchCustomerOptions() {
                        CustomerId = id
                    }).SingleOrDefault();

            if (customer == null) {
                return NotFound();
            }

            return Json(customer);
        }

        [HttpPatch("{id}")]
        public IActionResult UpdateCustomer(int id)
        {
            return Ok();
        }
    }
}