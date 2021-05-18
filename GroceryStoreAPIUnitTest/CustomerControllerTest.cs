using System;
using System.Collections.Generic;
using AutoMapper;
using Core.Contracts;
using Core.DTOs;
using GroceryStoreAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace GroceryStoreAPIUnitTest
{
    public class CustomerControllerTest
    {

        private readonly Mock<IRepositoryWrapper> _mockRepo;
        private readonly Mock<ILoggerManager> _mockLogger;
        private readonly Mock<IMapper> _mockMapper;

        private readonly CustomerController _controller;

        public CustomerControllerTest()
        {
            _mockRepo = new Mock<IRepositoryWrapper>();
            _mockLogger = new Mock<ILoggerManager>();
            _mockMapper = new Mock<IMapper>();

            _controller = new CustomerController(_mockLogger.Object, _mockRepo.Object, _mockMapper.Object );
        }



        [Fact]
        public void CreateCustomerTest()
        {

            var result = _controller.CreateCustomer(new Core.DTOs.CustomerForCreationDto() { Name = "John" });
            Assert.True(result.Id > 0);
            Assert.IsType<ObjectResult>(result.Result);

        }

        [Theory]
        [InlineData(-100, "")]
        [InlineData(1000, "")]
        [InlineData(0, "")]
        public void CatchInvalidUpdateCustomerTest(int id, string name)
        {
            var result = _controller.UpdateCustomer(id, new Core.DTOs.CustomerForUpdateDto() { Name = name });
            Assert.IsType<ObjectResult>(result.Result);
        }

        [Fact]
        public void UpdateCustomerTest()
        {
            
            var result = _controller.UpdateCustomer(1, new Core.DTOs.CustomerForUpdateDto() { Name = "John" });
         
            Assert.IsType<ObjectResult>(result.Result);
        }

      
        [Fact]
        public void GetAllCustomersTest()
        {

            var result = _controller.GetAllCustomers();
            Assert.IsType<ActionResult<IEnumerable<CustomerDto>>>( result.Result);
        }

        [Fact]
        public void GetCustomerTest()
        {

            var result = _controller.GetCustomerById(1);
            Assert.True(result.Id == 1);
        }

    }
}
