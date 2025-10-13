namespace Medilink.Models
{
    public class Medico : IRol
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Matricula { get; set; }
        public string Especialidad { get; set; }

    }
}