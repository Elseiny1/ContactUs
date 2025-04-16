using Feedbacks.Entities.Forms;
using Feedbacks.ViewModels.Services;

namespace Feedbacks.Repos.ServiceManagement
{
    public interface IServiceRepo
    {
        Task<Service> AddServiceAsync(ServiceViewModel service);
        Task<ServiceViewModel> UpdateServiceAsync(ServiceViewModel service);
        Task<string> DeleteServiceAsync(string id);
        Task<IEnumerable<Service>> GetAllServicesAsync();
        Task<Service> GetServiceByIdAsync(string id);
        Task<Service> GetServiceByNameAsync(string name);

    }
}
