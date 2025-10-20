using Medilink.Models;
namespace Medilink.Interfaces;
public interface IHospitalService
{
    Task<IEnumerable<Hospital>> GetHospitals();
    Task<Hospital> GetHospital(int id);
    Task<Hospital> AddHospital(Hospital hospital);
    Task<bool> UpdateHospital(Hospital hospital);
    Task<bool> DeleteHospital(int id);
}