public class EstudianteServicio
{
    private List<Estudiante> _estudiantes = new List<Estudiante>
    {
        new Estudiante { Nombre = "Ana",     Carrera = "Sistemas", Notas = new List<decimal> { 85, 90, 78 } },
        new Estudiante { Nombre = "Luis",    Carrera = "Sistemas", Notas = new List<decimal> { 60, 55, 70 } },
        new Estudiante { Nombre = "María",   Carrera = "Civil",    Notas = new List<decimal> { 91, 88, 95 } },
        new Estudiante { Nombre = "Carlos",  Carrera = "Sistemas", Notas = new List<decimal> { 45, 50, 48 } },
        new Estudiante { Nombre = "Sofía",   Carrera = "Civil",    Notas = new List<decimal> { 72, 68, 75 } },
        new Estudiante { Nombre = "Diego",   Carrera = "Civil",    Notas = new List<decimal> { 55, 60, 58 } },
        new Estudiante { Nombre = "Valeria", Carrera = "Sistemas", Notas = new List<decimal> { 88, 92, 85 } },
    };

    public async Task<List<Estudiante>> GetTodosAsync()
    {
        await Task.Delay(300);
        return _estudiantes;
    }

    public async Task<Estudiante> BuscarPorNombreAsync(string nombre)
    {
        await Task.Delay(150);
        var estudiante = _estudiantes.FirstOrDefault(e => e.Nombre == nombre);
        if (estudiante == null)
            throw new Exception($"No se encontró ningún estudiante con el nombre '{nombre}'");
        return estudiante;
    }
}


