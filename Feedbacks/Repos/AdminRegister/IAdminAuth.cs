namespace Feedbacks.Repos.AdminRegister
{
    public interface IAdminAuth
    {
        public Task<string> AdminRegisterAsync(RegisterAccount register);
        public Task<string> AdminLoginAsync(LoginViewModel login);
    }
}
