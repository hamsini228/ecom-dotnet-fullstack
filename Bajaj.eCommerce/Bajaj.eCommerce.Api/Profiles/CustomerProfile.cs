using AutoMapper;
using Bajaj.eCommerce.Api.DTOs.Customers;
using Bajaj.eCommerce.Entities;

namespace Bajaj.eCommerce.Api.Profiles;

public class CustomerProfile:Profile
{
    public CustomerProfile()
    {
        CreateMap<Customer, CustomerDto>();
    }
}
