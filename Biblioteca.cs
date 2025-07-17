public class Biblioteca
{
    public string Nombre { get; set; }
    public List<Ejemplar> Ejemplares { get; set; } = new();
    public List<Prestamo> Prestamos { get; set; } = new();
}