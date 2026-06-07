using System;
using System.Collections.Generic;
using Dia3;

var productos = new List<Producto>
{
    new Producto { Nombre = "Laptop",     Categoria = "Tecnología", Precio = 4500, Stock = 10 },
    new Producto { Nombre = "Mouse",      Categoria = "Tecnología", Precio = 120,  Stock = 50 },
    new Producto { Nombre = "Silla",      Categoria = "Muebles",    Precio = 800,  Stock = 5  },
    new Producto { Nombre = "Monitor",    Categoria = "Tecnología", Precio = 1200, Stock = 0  },
    new Producto { Nombre = "Escritorio", Categoria = "Muebles",    Precio = 950,  Stock = 3  },
};

EjercicioLinq.EjecutarTodos(productos);


// ── Ejercicio Integrador LINQ + Async ────────────────────────────────────────
await EjercicioIntegrador();

async Task EjercicioIntegrador()
{
    Console.WriteLine("============= EJERCICIO INTEGRADOR: Estudiates ============\n");

    var servicio = new EstudianteServicio();
    var estudiantes = await servicio.GetTodosAsync();

    // 1. Todos los estudiantes ordenados por promedio desc
    Console.WriteLine("1. Estudiantes ordenados por promedio (desc):");
    var ordenados = estudiantes.OrderByDescending(e => e.Promedio);
    foreach (var e in ordenados)
        Console.WriteLine($"   {e.Nombre,-10} | {e.Carrera,-10} | Promedio: {e.Promedio:F2} | {e.NivelRendimiento}");

    // 2. Promedio general y cuántos están por encima
    Console.WriteLine("\n2. Promedio general del grupo:");
    var promedioGeneral = estudiantes.Average(e => e.Promedio);
    var porEncima = estudiantes.Count(e => e.Promedio > promedioGeneral);
    Console.WriteLine($"    Promedio general : {promedioGeneral:F2}");
    Console.WriteLine($"    Por encima       : {porEncima} estudiantes");

    // 3. Estadísticas por carrera
    Console.WriteLine("\n3. Estadísticas por carrera:");
    var porCarrera = estudiantes
        .GroupBy(e => e.Carrera)
        .Select(g => new
        {
            Carrera      = g.Key,
            Cantidad     = g.Count(),
            Promedio     = g.Average(e => e.Promedio),
            MejorEstud   = g.MaxBy(e => e.Promedio)?.Nombre,
            Aprobados    = g.Count(e => e.Aprobado)
        });
    foreach (var c in porCarrera)
    {
        Console.WriteLine($"   Carrera  : {c.Carrera}");
        Console.WriteLine($"   Cantidad : {c.Cantidad}  |  Promedio: {c.Promedio:F2}");
        Console.WriteLine($"   Mejor    : {c.MejorEstud}  |  Aprobados: {c.Aprobados}");
        Console.WriteLine();
    }

    // 4. Buscar por nombre — uno que existe y uno que no
    Console.WriteLine("4. Búsqueda por nombre:");
    try
    {
        var encontrado = await servicio.BuscarPorNombreAsync("Ana");
        Console.WriteLine($"    Encontrado: {encontrado.Nombre} — {encontrado.Carrera} — Promedio: {encontrado.Promedio:F2}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"    Error: {ex.Message}");
    }

    try
    {
        var noExiste = await servicio.BuscarPorNombreAsync("Pedro");
        Console.WriteLine($"    Encontrado: {noExiste.Nombre}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"    Error: {ex.Message}");
    }

    // 5. Desafío — Estudiantes en riesgo (nota mínima < 50)
    Console.WriteLine("\n5. Estudiantes en riesgo (nota mínima < 50):");
    var enRiesgo = estudiantes
        .Where(e => e.Notas.Min() < 50)
        .Select(e => new
        {
            e.Nombre,
            e.Carrera,
            NotaMinima = e.Notas.Min(),
            e.Promedio
        })
        .OrderBy(e => e.NotaMinima);
    foreach (var e in enRiesgo)
        Console.WriteLine($"   {e.Nombre,-10} | {e.Carrera,-10} | Nota mín: {e.NotaMinima} | Promedio: {e.Promedio:F2}");

    Console.WriteLine();
}