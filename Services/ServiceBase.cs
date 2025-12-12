using B_U2_S2_G5_PROJECT.Models.Entity;

namespace B_U2_S2_G5_PROJECT.Services
{
    public abstract class ServiceBase
    {
        protected readonly ApplicationDbContext _applicationDbContext;
        protected ServiceBase(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        protected async Task<bool> SaveAsync()
        {
            bool result = false;

            try
            {
                result = await _applicationDbContext.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return result;
        }

    }
}
