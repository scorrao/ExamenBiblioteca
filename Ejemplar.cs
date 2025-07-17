public class Ejemplar
{
    public int Codigo { get; set; }
    public string Titulo { get; set; }
    public string Autor { get; set; }
    public string Genero { get; set; }

    public override string ToString()
    {
        return $"{Codigo} - {Titulo}";
    }
}