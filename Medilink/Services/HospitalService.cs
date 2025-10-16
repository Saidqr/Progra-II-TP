using Medilink.Context;
using Medilink.Interfaces;
using Medilink.Models;
using Microsoft.EntityFrameworkCore;

namespace Medilink.Services;

public class HospitalService : IHospitalService
{
    private readonly MedilinkDbContext _context;

    public HospitalService(MedilinkDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Hospital>> GetHospitals()
    {
        return await _context.Hospitales.ToListAsync();
    }

    public async Task<Hospital> GetHospital(int id)
    {
        return await _context.Hospitales.FindAsync(id);
    }

    public async Task<Hospital> AddHospital(Hospital hospital)
    {
        _context.Hospitales.Add(hospital);
        await _context.SaveChangesAsync();
        return hospital;
    }

    public async Task<bool> UpdateHospital(Hospital hospital)
    {
        _context.Entry(hospital).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _context.Hospitales.AnyAsync(h => h.Id == hospital.Id))
            {
                return false;
            }
            else
            {
                throw;
            }
        }
    }

    public async Task<bool> DeleteHospital(int id)
    {
        var hospital = await GetHospital(id);
        if (hospital == null) return false;

        _context.Hospitales.Remove(hospital);
        await _context.SaveChangesAsync();
        return true;
    }
}