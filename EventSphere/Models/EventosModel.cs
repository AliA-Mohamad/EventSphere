namespace EventSphere.Models
{
    public class EventosModel
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string DataInicio  { get; set; }
        public string Datafim { get; set; }

    }
}
