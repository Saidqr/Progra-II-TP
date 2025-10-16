namespace Medilink.Models
{
    public class Medico : Rol
    {
        public string Matricula { get; set; }
        public string Especialidad { get; set; }

        public List<ConsultaMedica> Consultas { get; set; }
        public List<Hospital> Hospitales { get; set; }
        //Fijarse que un medico puede tener una lista de pacientes
    }
}