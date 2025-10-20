namespace Medilink.Models
{
    public class ConsultaMedica
    {
        public int Id { get; set; }
        public int IdMedico { get; set; }
        public int IdPaciente { get; set; }
        public string Estado { get; set; }
        public string Observaciones { get; set; }
        public Medico Medico { get; set; }
        public Paciente Paciente {get; set;}
        public DateTime Fecha { get; set; }
        

    }

}