using EFCore_Prescriptions.DTOs;

namespace EFCore_Prescriptions.Services
{
    public interface IPrescriptionService
    {
        Task<PrescriptionDTO> GetPrescriptionAsync(int id);
    }
}
