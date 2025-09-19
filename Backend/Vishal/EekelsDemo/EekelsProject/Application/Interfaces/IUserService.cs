using Domain.SignupLoginEntities;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<SignupResponse> Signup(UserSignupRequest request);
        Task<LoginResponse> Login(UserLoginRequest request);
    }
}
