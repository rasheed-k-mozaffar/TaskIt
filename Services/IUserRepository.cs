using System;
namespace TaskIt.Web.Services
{
    public interface IUserRepository
    {
        Task Register(RegisterUserModel model);
        Task SignIn(LoginUserModel model);

        Task LogoutPOST();
        


    }
}

