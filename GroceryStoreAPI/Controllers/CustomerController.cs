using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Contracts;
using Microsoft.AspNetCore.Mvc;
using Core.DTOs;
using Core.Entities;


namespace GroceryStoreAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private IMapper _mapper;

        public CustomerController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetAllCustomers()
        {
            try
            {
                var customers = await _repository.Customer.GetAllCustomersAsync();
                _logger.LogInfo($"Returned all customers from database.");
                var customersResult = _mapper.Map<IEnumerable<CustomerDto>>(customers);
                return Ok(customersResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllCustomers action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> GetCustomerById(int id)
        {
            try
            {
                var customer = await _repository.Customer.GetCustomerByIdAsync(id);

                if (customer == null)
                {
                    _logger.LogError($"Customer with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned customer with id: {id}");

                    var customerResult = _mapper.Map<CustomerDto>(customer);
                    return Ok(customerResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetCustomerById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CustomerForCreationDto customer)
        {
            try
            {
                if (customer == null)
                {
                    _logger.LogError("Customer object sent from client is null.");
                    return BadRequest("Customer object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid customer object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var customerEntity = _mapper.Map<Customer>(customer);

                _repository.Customer.CreateCustomer(customerEntity);
               await  _repository.SaveAsync();

                var createdCustomer = _mapper.Map<CustomerDto>(customerEntity);

                return CreatedAtRoute("CustomerById", new { id = createdCustomer.Id }, createdCustomer);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateCustomer action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, [FromBody] CustomerForUpdateDto customer)
        {
            try
            {
                if (customer == null)
                {
                    _logger.LogError("Customer object sent from client is null.");
                    return BadRequest("Customer object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid customer object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var customerEntity = _repository.Customer.GetCustomerByIdAsync(id).Result;
                if (customerEntity == null)
                {
                    _logger.LogError($"Customer with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _mapper.Map(customer, customerEntity);

                _repository.Customer.UpdateCustomer(customerEntity);
                await _repository.SaveAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateCustomer action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
