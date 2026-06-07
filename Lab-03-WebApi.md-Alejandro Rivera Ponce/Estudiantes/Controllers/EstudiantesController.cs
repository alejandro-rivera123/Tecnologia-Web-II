using Estudiantes.Models;
using Microsoft.AspNetCore.Mvc; // ← estaba mal escrito

namespace Estudiantes.Controllers;

[ApiController]  // ← era [ApiControllers] con s
[Route("api/[controller]")]
public class EstudiantesController : ControllerBase
{
    private static readonly List<Estudiante> _estudiantes = new List<Estudiante> // ← List con mayúscula
    {
        new Estudiante { Id = 1, Nombre = "Ana López",   Carrera = "Sistemas", Promedio = 85.9  },
        new Estudiante { Id = 2, Nombre = "Luis Mamani", Carrera = "Sistemas", Promedio = 62.12 },
        new Estudiante { Id = 3, Nombre = "María Ríos",  Carrera = "Sistemas", Promedio = 91    },
    };

    [HttpGet]  
    public IActionResult ObtenerTodos()  
    {
        return Ok(_estudiantes); 
    }

    [HttpGet("{id}")]
    public IActionResult ObtenerPorId(int id)
    {
        var estudiante = _estudiantes.FirstOrDefault(e => e.Id == id);
        if (estudiante is null)
            return NotFound(new { mensaje = "Estudiante no encontrado" });
        return Ok(estudiante);
    }

    [HttpPost]
    public IActionResult Crear([FromBody] Estudiante estudiante)
    {
        estudiante.Id = _estudiantes.Any()
            ? _estudiantes.Max(e => e.Id) + 1
            : 1;
        _estudiantes.Add(estudiante);
        return CreatedAtAction(nameof(ObtenerPorId), new { id = estudiante.Id }, estudiante);
    }

    [HttpPut("{id}")]
    public IActionResult Actualizar(int id, [FromBody] Estudiante datos)
    {
        var estudiante = _estudiantes.FirstOrDefault(e => e.Id == id);
        if (estudiante is null)
            return NotFound(new { mensaje = "Estudiante no encontrado" });

        estudiante.Nombre   = datos.Nombre;
        estudiante.Carrera  = datos.Carrera;
        estudiante.Promedio = datos.Promedio;
        return Ok(estudiante);
    }

    [HttpDelete("{id}")]
    public IActionResult Eliminar(int id)
    {
        var estudiante = _estudiantes.FirstOrDefault(e => e.Id == id);
        if (estudiante is null)
            return NotFound(new { mensaje = "Estudiante no encontrado" });

        _estudiantes.Remove(estudiante);
        return NoContent();
    }
}