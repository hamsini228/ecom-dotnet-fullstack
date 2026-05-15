namespace eCommerce.Application.DTOs.Customer;

public class CustomerDto
{
    public int CustomerId { get; set; }
    public string ContactName { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string? Email { get; set; }

    public int Zipcode { get; set; }
    public int UserId { get; set; }

}
