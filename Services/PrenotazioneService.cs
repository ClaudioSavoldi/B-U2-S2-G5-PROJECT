using B_U2_S2_G5_PROJECT.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace B_U2_S2_G5_PROJECT.Services
{
    public class PrenotazioneService:ServiceBase
    {
        public PrenotazioneService(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {

        }

        public async Task<List<Prenotazione>> GetPrenotazioneAsync()
        {
            return await _applicationDbContext.Prenotazioni.AsNoTracking().ToListAsync();
        }
        public async Task<Prenotazione> GetPrenotazioneById(Guid id)
        {
            return await _applicationDbContext.Prenotazioni.FindAsync(id);
        }
        public async Task<bool> DeletePrenotazione(Prenotazione prenotazione)
        {
            _applicationDbContext.Prenotazioni.Remove(prenotazione);
            return await SaveAsync();
        }

        public async Task<bool> UpdatePrenotazione(Prenotazione prenotazione)
        {
            Prenotazione existing = await GetPrenotazioneById(prenotazione.Id);
            if (existing is null)
            {
                return false;
            }

            existing.ClienteId = prenotazione.ClienteId;
            existing.CameraId = prenotazione.CameraId;
            existing.DataInizio = prenotazione.DataInizio;
            existing.DataFine = prenotazione.DataFine;
            existing.Stato = prenotazione.Stato;

            return await SaveAsync();
        }



        public async Task<bool> CreatePrenotazioneAsync(Prenotazione prenotazione)
        {
            await _applicationDbContext.Prenotazioni.AddAsync(prenotazione);

            return await SaveAsync();
        }
    }
}

