using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Feedbacks.Repos.AdminRegister
{
    public class AdminAuth : IAdminAuth
    {
        private readonly UserManager<AdminIdentity> _userManager;
        private readonly SignInManager<AdminIdentity> _signInManager;
        private readonly FeedbackDbContext _context;

        public AdminAuth(UserManager<AdminIdentity> userManager,
            FeedbackDbContext context,
            SignInManager<AdminIdentity> signInManager)
        {
            _userManager = userManager;
            _context = context;
            _signInManager = signInManager;
        }

        public async Task<RegisterAccount> AdminRegisterAsync(RegisterAccount register)
        {
            if(register is null || register.Email is null)
            {
                register.massage = "Register model is null.";
                return register;
            }

            var anyExistingUser = await _context.Users.AnyAsync();
            if(anyExistingUser)
            {
                    register.massage = "Admin already exists.";
                    return register;
            }

            if (register.Password != register.ConfirmPassword)
            {
                register.massage = "Password and Confirm Password do not match.";
                return register;
            }

            var existingUser = await _userManager.FindByEmailAsync(register.Email);
            if (existingUser != null)
            {
                register.massage = "Admin already exists.";
                return register;
            }

            try
            {
                var user = new AdminIdentity();

                await _userManager.SetEmailAsync(user, register.Email);
                var result = await _userManager.CreateAsync(user, register.Password);

                if (!result.Succeeded)
                {
                    await _userManager.DeleteAsync(user);
                    register.massage = result.Errors.Select(e => e.Description).ToString();
                    return register;
                }
                await _userManager.AddToRoleAsync(user, "Admin");

                var activateResult = await ActivateAccountAsync(register.Email);
                if (!activateResult)
                {
                    await _userManager.DeleteAsync(user);
                    register.massage = "Failed to activate account.";
                    return register;
                }

                await _signInManager.SignInAsync(user, isPersistent: true);
                register.massage = "Admin registered successfully.";
                return register;

            }
            catch (Exception ex)
            {
                register.massage = $"An error occurred: {ex.Message}";
                return register;
            }
        }
        private async Task<bool> ActivateAccountAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return false;

            user.EmailConfirmed = true;
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<LoginViewModel> AdminLoginAsync(LoginViewModel login)
        {
            if (login is null)
            {
                login.Masssage = "Login model is null.";
                return login;
            }
            try
            {
                var user = await _userManager.FindByEmailAsync(login.Email);
                if (user == null)
                {
                    login.Masssage = "User not found.";
                    return login;
                }
                var result = await _signInManager.PasswordSignInAsync(user, login.Password, isPersistent: false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: true);
                    login.Masssage = "Admin logged in successfully.";
                    return login;
                }
                else
                {
                    login.Masssage = "Invalid login attempt.";
                    return login;
                }
            }
            catch (Exception ex)
            {
                login.Masssage = $"An error occurred: {ex.Message}";
                return login;
            }
        }
    }
}
