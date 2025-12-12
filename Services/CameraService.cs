using B_U2_S2_G5_PROJECT.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace B_U2_S2_G5_PROJECT.Services
{
    public class CameraService:ServiceBase
    {
        public CameraService(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {

        }

        public async Task<List<Camera>> GetCamereAsync()
        {
            return await _applicationDbContext.Camere.AsNoTracking().ToListAsync();
        }
        public async Task<Camera> GetCameraById(Guid id)
        {
            return await _applicationDbContext.Camere.FindAsync(id);
        }
        public async Task<bool> DeleteCamera(Camera camera)
        {
            _applicationDbContext.Camere.Remove(camera);
            return await SaveAsync();
        }

        public async Task<bool> UpdateCamera(Camera camera)
        {
            Camera existing = await GetCameraById(camera.Id);
            if (existing is null)
            {
                return false;
            }

            existing.Numero = camera.Numero;
            existing.Tipo = camera.Tipo;
            existing.Prezzo = camera.Prezzo;
           
            return await SaveAsync();
        }



        public async Task<bool> CreateCameraAsync(Camera camera)
        {
            await _applicationDbContext.Camere.AddAsync(camera);

            return await SaveAsync();
        }
    }
}
