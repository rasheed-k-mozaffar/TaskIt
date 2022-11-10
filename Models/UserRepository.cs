using System;

namespace TaskIt.Web.Models
{
    public class UserRepository : Controller , IUserRepository 
    {
        private readonly SignInManager<IdentityUser> SignInManager;
        private readonly UserManager<IdentityUser> UserManager;

        //using dependency injection to inject sign in and user managers to the repo so they can
        //be accessed by all the local methods to sign in , log out and register new users.
        public UserRepository(SignInManager<IdentityUser> _signInManager , UserManager<IdentityUser> _userManager)
        {
            SignInManager = _signInManager;
            UserManager = _userManager;
        }

        
        //If the user confirms wanting to log out , this method will be called in the controller.
        //Else , dont logout action method will handle the user's request.
        public async Task LogoutPOST()
        {
            await SignInManager.SignOutAsync();
        }

        //If the user credentials provided meet the contraints , that will lead to a successful
        //registeration and then the user will be signed in automatically. else , we return the view
        public async Task Register(RegisterUserModel model)
        {
            //Creating an idenitty user object and assigning the username and email to those of the model object.
            var user = new IdentityUser()
            {
                UserName = model.Email,
                Email = model.Email
            };

            //Creating the identity result object using the create async method by passing the idenitty
            //object and the password that the user has provided in the form to it.
            var result = await UserManager.CreateAsync(user, model.Password);

            //validating the result if it has passed the constraint , if true , sign the user in
            //Else , the controller should return the view with model errors.
            if (result.Succeeded)
            {
                await SignInManager.SignInAsync(user, false);
            }
  
        }



        public async Task SignIn(LoginUserModel model)
        {
            //Creating a sign in result object using password sign in async method
            //passing some parameters , email , password , bool var if the user wants to stay signed in or not

            var identityResult = await SignInManager.PasswordSignInAsync(model.Email , model.Password , model.RememberMe , false);

            //if the result evaluates to be true , then return the user the home page
            //Else , the controller should return the same view while rendering an error message
            //for the user to elabortae on what has gone wrong.
            if (identityResult.Succeeded)
                RedirectToAction("Index", "Home");

            ModelState.AddModelError("", "Either the email or password is incorrect.");
        }
    }
}

