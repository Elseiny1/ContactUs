using Feedbacks.Entities.Forms;
using Feedbacks.ViewModels.Services;

namespace Feedbacks.Repos.ServiceManagement
{
    public interface IServiceRepo
    {
        Task<Service> Add(ServiceViewModel service);
        Task<ServiceViewModel> Update(ServiceViewModel service);
        Task<string> Delete(string id);
        Task<IEnumerable<Service>> GetAllServicesAsync();
        Task<Service> GetServiceById(string id);
        Task<Service> GetServiceByName(string name);

    }
}
