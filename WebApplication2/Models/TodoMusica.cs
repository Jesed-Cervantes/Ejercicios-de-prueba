namespace WebApplication2.Models
{
    public class TodoMusica
    {
        public long Id { get; set; }
        public string? Title { get; set; }
        public DateTime FechaLanzamiento { get; set; }
        public long? UserId { get; set; }
    }
}
