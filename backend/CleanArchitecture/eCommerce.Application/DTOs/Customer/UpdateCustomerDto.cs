using System.ComponentModel.DataAnnotations;

namespace eCommerce.Application.DTOs.Customer;

public class UpdateCustomerDto
{

    public int CustomerId { get; set; }

  
    [Required(ErrorMessage = "Customer Name is Mandatory")]
    [MaxLength(50, ErrorMessage = "Name should be within 50 characters length")]
    public string ContactName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Addres is Mandatory")]
    [MaxLength(200, ErrorMessage = "Address should be within the range of 200 characters")]
    public string Address { get; set; } = string.Empty;

    [Required(ErrorMessage = "City is Mandatory")]
    [MaxLength(25, ErrorMessage = "City should be within the range of 25 characters")]
    public string City { get; set; } = string.Empty;


 
    [Required(ErrorMessage = "Phone number is Mandatory")]
    [MaxLength(20, ErrorMessage = "Phone number cannot exceed 20 characters")]
    public string Phone { get; set; } = string.Empty;


    [EmailAddress(ErrorMessage = "Email must be in correct format for example jonhn@myshoeworld.com")]
    [MaxLength(100, ErrorMessage = "Email should be within the range of 100 characters")]
    public string? Email { get; set; }

  
    [Required(ErrorMessage = "Zipcode is Mandatory")]

    public int Zipcode { get; set; }
}
