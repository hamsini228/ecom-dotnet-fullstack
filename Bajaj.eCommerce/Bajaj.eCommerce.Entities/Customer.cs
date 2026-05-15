using System.ComponentModel.DataAnnotations;

namespace Bajaj.eCommerce.Entities;

public class Customer
{
    [Key]
    public int CustomerId { get; set; }

    [Display(Name ="Customer Name")]
    [Required(ErrorMessage ="Customer Name is Mandatory")]
    [MaxLength(50,ErrorMessage ="Name should be within 50 characters length")]
    public string ContactName { get; set; }=string.Empty;

    [Required(ErrorMessage ="Addres is Mandatory")]
    [MaxLength(200,ErrorMessage ="Address should be within the range of 200 characters")]
    public string Address { get; set; } = string.Empty;

    [Required(ErrorMessage = "City is Mandatory")]
    [MaxLength(25, ErrorMessage = "City should be within the range of 25 characters")]
    public string City { get; set; }= string.Empty;


    [Display(Name ="Contact #")]
    [Required(ErrorMessage = "Phone number is Mandatory")]
    [MaxLength(20, ErrorMessage = "Phone number cannot exceed 20 characters")]
    public string Phone { get; set; }=string.Empty;

    [Display(Name ="Email ID")]
    [EmailAddress(ErrorMessage ="Email must be in correct format for example jonhn@myshoeworld.com")]
    [MaxLength(100, ErrorMessage = "Email should be within the range of 100 characters")]
    public string? Email {  get; set; }

    [Display(Name ="Zip Code")]
    [Required(ErrorMessage = "Zipcode is Mandatory")]
    [MaxLength(10, ErrorMessage = "zipcode should be within the range of 10 characters")]
    public int Zipcode { get; set; }

    public ICollection<Cart>? Carts { get; set; }
}
