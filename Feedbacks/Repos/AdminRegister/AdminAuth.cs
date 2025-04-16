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

        public async Task<string> AdminRegisterAsync(RegisterAccount register)
        {
            if(register is null)
                return "Register model is null.";

            var anyExistingUser = await _context.Users.AnyAsync();
            if(anyExistingUser)
                return "Admin already exists.";

            if (register.Password != register.ConfirmPassword)
                return "Password and Confirm Password do not match.";

            var existingUser = await _userManager.FindByEmailAsync(register.Email);
            if (existingUser != null)
                return "Invalid Attempt";//to protect the admin email

            try
            {
                var user = new AdminIdentity();

                await _userManager.SetEmailAsync(user, register.Email);
                var result = await _userManager.CreateAsync(user, register.Password);

                if (!result.Succeeded)
                {
                    await _userManager.DeleteAsync(user);
                    var errors = result.Errors.Select(e => e.Description).ToList();
                    return string.Join(", ", errors);
                }
                await _userManager.AddToRoleAsync(user, "Admin");

                var activateResult = await ActivateAccountAsync(register.Email);
                if (!activateResult)
                {
                    await _userManager.DeleteAsync(user);
                    return "Failed to activate account.";
                }

                await _signInManager.SignInAsync(user, isPersistent: true);

                return "Admin registered Successfully.";

            }
            catch (Exception ex)
            {
                return $"An error occurred: {ex.Message}";
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

        public async Task<string> AdminLoginAsync(LoginViewModel login)
        {
            if (login is null)
                return "Login model is null.";
            try
            {
                var user = await _userManager.FindByEmailAsync(login.Email);
                if (user == null)
                    return "User not found.";

                var result = await _signInManager.PasswordSignInAsync(user, login.Password, isPersistent: false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: true);
                    return "Admin logged in successfully.";
                }
                else
                {
                    return "Invalid login attempt.";
                }
            }
            catch (Exception ex)
            {
                return $"An error occurred: {ex.Message}";
            }
        }
    }
}
