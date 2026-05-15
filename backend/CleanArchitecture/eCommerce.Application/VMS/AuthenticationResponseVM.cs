namespace eCommerce.Application.VMS;

public class AuthenticationResponseVM
{
    //going to be payload
    public string Email { get; set; }=string.Empty;
    public string RollName { get; set; }= string.Empty;
    public string Token {  get; set; }=string.Empty;
    public bool IsAuthenticated { get; set; } =false;
    public string Message {  get; set; }=string.Empty; 
    public int UserId { get; set; }
}
