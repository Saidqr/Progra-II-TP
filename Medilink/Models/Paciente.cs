namespace Medilink.Models
{
    public class Paciente : Rol
    {
        public string Expediente { get; set; }
        public List<ConsultaMedica> consultas { get; set; }
        //Ehhhhh
    }
}