using System.ComponentModel.DataAnnotations;
namespace ENTITIES
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "User id required")]
        public long userId;

        [Required(ErrorMessage = "Password required")]
        public string pass;
    }
    public class LoginResponse : CrmResponse
    {
        public string token;
        public UsrWebEntity user;

    }
}