using B_U2_S2_G5_PROJECT.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace B_U2_S2_G5_PROJECT.Services
{
    public class ClienteService : ServiceBase
    {
        public ClienteService(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {

        }

        public async Task<List<Cliente>> GetClientiAsync()
        {
            return await _applicationDbContext.Clienti.AsNoTracking().ToListAsync();
        }
        public async Task<Cliente> GetClienteById(Guid id)
        {
            return await _applicationDbContext.Clienti.FindAsync(id);
        }
        public async Task<bool> DeleteCliente(Cliente cliente)
        {
            _applicationDbContext.Clienti.Remove(cliente);
            return await SaveAsync();
        }

        public async Task<bool> UpdateCliente(Cliente cliente)
        {
            Cliente existing = await GetClienteById(cliente.Id);
            if (existing is null)
            {
                return false;
            }

            existing.Nome = cliente.Nome;
            existing.Cognome = cliente.Cognome;
            existing.Email = cliente.Email;
            existing.Telefono = cliente.Telefono;
            return await SaveAsync();
        }



        public async Task<bool> CreateClienteAsync(Cliente cliente)
        {
            await _applicationDbContext.Clienti.AddAsync(cliente);

            return await SaveAsync();
        }
    }
}
