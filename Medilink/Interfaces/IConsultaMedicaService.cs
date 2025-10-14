using Medilink.Models;

namespace Medilink.Interfaces
{
    public interface IConsultaMedicaService
    {
        public List<ConsultaMedica> GetConsultas();
        public ConsultaMedica GetConsulta(int id);
        public void AddConsulta(ConsultaMedica consulta);
        public void UpdateConsulta(ConsultaMedica consulta);
        public void EliminarConsulta(int id);
        public void RecetarMedicamentos(); //Falta añadir insumo de parametro

    }
}