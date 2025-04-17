namespace Feedbacks.Repos.AdminRegister
{
    public interface IAdminAuth
    {
        public Task<RegisterAccount> AdminRegisterAsync(RegisterAccount register);
        public Task<LoginViewModel> AdminLoginAsync(LoginViewModel login);
    }
}
