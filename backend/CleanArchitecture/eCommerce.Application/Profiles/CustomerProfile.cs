using AutoMapper;
using eCommerce.Application.DTOs.Customer;
using eCommerce.Domain;

namespace eCommerce.Application.Profiles;

public class CustomerProfile:Profile
{
    public CustomerProfile()
    {
        CreateMap<Customer,CustomerDto>();
        CreateMap<CreateCustomerDto,Customer>().ReverseMap();
        CreateMap<UpdateCustomerDto,Customer>().ReverseMap();
    }
}
