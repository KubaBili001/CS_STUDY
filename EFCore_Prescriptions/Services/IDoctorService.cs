using EFCore_Prescriptions.DTOs;

namespace EFCore_Prescriptions.Services
{
    public interface IDoctorService
    {

        Task<List<DoctorDTO>> GetDoctorAsync();
        Task<Boolean> AddDoctorAsync(DoctorDTO doctor);
        Task<Boolean> UpdateDoctorAsync(DoctorDTO doctor);
        Task<Boolean> RemoveDoctorAsync(int id);

    }
}
