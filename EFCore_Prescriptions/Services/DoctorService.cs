using Microsoft.EntityFrameworkCore;
using EFCore_Prescriptions.Models;
using EFCore_Prescriptions.DTOs;

namespace EFCore_Prescriptions.Services
{
    public class DoctorService : IDoctorService
    {

        public readonly DatabaseContext _dbContext;

        public DoctorService(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<DoctorDTO>> GetDoctorAsync()
        {

            var result = await _dbContext.Doctors.AsQueryable().Select(d => new DoctorDTO
            {
                Id = d.IdDoctor,
                Name = d.FirstName,
                LastName = d.LastName,
                Email = d.Email
            }).ToListAsync();

            return result;
        }

        public async Task<Boolean> AddDoctorAsync(DoctorDTO doctor)
        {
            _dbContext.Doctors.Add(new Doctor()
            {
                FirstName = doctor.Name,
                LastName = doctor.LastName,
                Email = doctor.Email
            });
            await _dbContext.SaveChangesAsync();

            return true;
        }
        
        public async Task<Boolean> UpdateDoctorAsync(DoctorDTO doctor)
        {
            var result = await _dbContext.Doctors.Where(e => e.IdDoctor == doctor.Id).FirstOrDefaultAsync();
            if (result != null)
            {
                result.FirstName = doctor.Name;
                result.LastName = doctor.LastName;
                result.Email = doctor.Email;

                await _dbContext.SaveChangesAsync();
            }
            else
            {
                return false;
            }

            return true;
        }
        
        public async Task<Boolean> RemoveDoctorAsync(int id)
        {

            var result = await _dbContext.Doctors.Where(d => d.IdDoctor == id).Include(p => p.Prescriptions).FirstOrDefaultAsync();

            if (result == null)
            {
                return false;
            }

            _dbContext.Remove(result);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
