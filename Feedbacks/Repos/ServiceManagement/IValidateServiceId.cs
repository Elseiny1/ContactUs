using Feedbacks.Entities.Forms;

namespace Feedbacks.Repos.ServiceManagement
{
    public interface IValidateServiceId
    {
        public Task<Service> GetServiceByIdAsync(string id);
    }
}
