using Microsoft.EntityFrameworkCore;
using EFCore_Prescriptions.DTOs;
using EFCore_Prescriptions.Models;

namespace EFCore_Prescriptions.Services
{
    public class PrescriptionService : IPrescriptionService
    {

        public readonly DatabaseContext _dbContext;

        public PrescriptionService(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PrescriptionDTO> GetPrescriptionAsync(int id)
        {
            var premed = await _dbContext.Prescriptions.Where(e => e.IdPrescription == id)
                .Include(premed => premed.DoctorInstance)
                .Include(premed => premed.PatientInstance)
                .Include(premed => premed.PrescriptionMedicaments)
                .ThenInclude(med => med.Medicament)
                .FirstOrDefaultAsync();

            if (premed == null)
            {
                throw new ArgumentNullException();
            }

            List<MedicamentDTO> list = new List<MedicamentDTO>();

            foreach (PrescriptionMedicament pm in premed.PrescriptionMedicaments)
            {
                list.Add(new MedicamentDTO
                {
                    IdMedicament = pm.IdMedicament,
                    Name = pm.Medicament.Name,
                    Description = pm.Medicament.Description,
                    Type = pm.Medicament.Type
                });
            }

            var result = new PrescriptionDTO
            {
                IdPrescription = id,
                Date = premed.Date,
                DueDate = premed.DueDate,
                Patient = new PatientDTO
                {
                    Id = premed.PatientInstance.IdPatient,
                    Name = premed.PatientInstance.FirstName,
                    LastName = premed.PatientInstance.LastName,
                    Birthdate = premed.PatientInstance.Birthdate,
                },
                Doctor = new DoctorDTO
                {
                    Id = premed.DoctorInstance.IdDoctor,
                    Name = premed.DoctorInstance.FirstName,
                    LastName = premed.DoctorInstance.LastName,
                    Email = premed.DoctorInstance.Email,
                },
                Medicaments = list
            };


            return result;
        }
    }
}

