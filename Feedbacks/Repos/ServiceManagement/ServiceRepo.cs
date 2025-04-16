using Feedbacks.Entities.Forms;
using Feedbacks.Repos.ServiceManagement;
using Feedbacks.ViewModels.Services;

namespace Feedbacks.Repos
{
    public class ServiceRepo : IServiceRepo
    {
        private readonly FeedbackDbContext _context;
        public ServiceRepo(FeedbackDbContext context)
        {
            _context = context;
        }

        //While the return is null there's somthing wrong
        public async Task<Service> Add(ServiceViewModel service)
        {
            if (service is null)
                return null;

            var newService = new Service();
            newService.ServiceName = service.ServiceName;

            try
            {
                await _context.AddAsync(newService);
                await _context.SaveChangesAsync();

                return newService;
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine(ex.Message);
                return null;
            }

        }

        public async Task<ServiceViewModel> Update(ServiceViewModel service)
        {
            if (service is null)
            {
                return null;
            }

            var existingService = await _context.Services.FindAsync(service.Id);
            if (existingService is null)
            {
                service.Massage = "service not found";
                return service;
            }

            if(existingService.IsDeleted)
            {
                service.Massage = "this service has been deleted";
                return service;
            }

            existingService.ServiceName = service.ServiceName;
            service.Id = existingService.Id;
            try
            {
                _context.Update(existingService);
                await _context.SaveChangesAsync();
                return service;
            }
            catch (Exception ex)
            {
                service.Massage = ex.Message;
                return service;
            }
        }

        public async Task<string> Delete(string id)
        {
            var existingService = await _context.Services.FindAsync(id);
            if (existingService == null || existingService.IsDeleted)
                return "service not found";

            existingService.IsDeleted = true;

            try
            {
                _context.Update(existingService);
                await _context.SaveChangesAsync();
                return $"service {existingService.ServiceName} deleted succesfully";
            }catch (Exception ex)
            {
                return $"something went wrong {ex.Message}";
            }
            
        }

        public async Task<IEnumerable<Service>> GetAllServicesAsync()
        {
            var services = await _context.Services
                .Where(s => !s.IsDeleted)
                .ToListAsync();
            return services;
        }

        public async Task<Service> GetServiceById(string id)
        {
            var service = await _context.Services
                .Where(s => s.Id == id && !s.IsDeleted)
                .FirstOrDefaultAsync();
            if (service is null)
                return null;

            return service;
        }

        public async Task<Service> GetServiceByName(string name)
        {
            var service = await _context.Services
                .Where(s => s.ServiceName == name && !s.IsDeleted)
                .FirstOrDefaultAsync();
            if (service is null)
                return null;
            return service;
        }
    }
}
