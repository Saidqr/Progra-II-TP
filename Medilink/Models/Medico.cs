namespace Medilink.Models
{
    public class Medico : IRol
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Matricula { get; set; }
        public string Especialidad { get; set; }

        public List<ConsultaMedica> Consultas{ get; set; }
        //Fijarse que un medico puede tener una lista de pacientes
    }
}