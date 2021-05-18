using System;
using System.Collections.Generic;
using AutoMapper;
using Core.DTOs;
using Core.Entities;

namespace GroceryStoreAPI
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            CreateMap<Customer, CustomerDto>();
            CreateMap<CustomerDto, CustomerDto>();

            CreateMap<Customer, CustomerForCreationDto>();
            CreateMap<CustomerForCreationDto,Customer>();
            CreateMap<Customer, CustomerForUpdateDto>();
            CreateMap<CustomerForUpdateDto, Customer>();
        }
    }
}
