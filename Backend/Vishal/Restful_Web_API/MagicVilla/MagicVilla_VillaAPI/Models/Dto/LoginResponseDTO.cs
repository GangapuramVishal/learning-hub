namespace MagicVilla_VillaAPI.Models.Dto
{
    //take the user details and token is used to validate
    public class LoginResponseDTO
    {
        public LocalUser User {  get; set; }    
        public string Token { get; set; }
    }
}
