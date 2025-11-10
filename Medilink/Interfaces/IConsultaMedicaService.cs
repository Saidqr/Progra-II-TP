using Medilink.Models;

namespace Medilink.Interfaces
{
    public interface IConsultaMedicaService
    {
        public Task<IEnumerable<ConsultaMedica>> GetConsultas();
        public Task<ConsultaMedica> GetConsulta(int id);
        public Task<ConsultaMedica> AddConsulta(ConsultaMedica consulta);
        public Task <bool> UpdateConsulta(ConsultaMedica consulta);
        public Task <bool> EliminarConsulta(int id);
        //public void RecetarMedicamentos(); //Falta añadir insumo de parametro

    }
}