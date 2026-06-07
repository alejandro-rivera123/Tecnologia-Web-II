public class Estudiante
{
    public string Nombre { get; set; }
    public string Carrera { get; set; }
    public List<decimal> Notas { get; set; }

    public decimal Promedio => Notas.Average();

    public bool Aprobado => Promedio >= 51;

    public string NivelRendimiento => Promedio switch
    {
        >= 85 => "Estratégico",
        >= 70 => "Autónomo",
        >= 51 => "Resolutivo",
        >= 25 => "Receptivo",
        _     => "Preformal"
    };
}