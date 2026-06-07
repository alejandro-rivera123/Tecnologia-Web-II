using Estudiantes.Models;
using Microsoft.AspNetCore.Mvc;

namespace Estudiantes.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MateriasController : ControllerBase
{
    private static readonly List<Materia> _materias = new()
    {
        new Materia { Id = 1, Nombre = "Tecnología Web II",      Sigla = "SIS-0151", Creditos = 6 },
        new Materia { Id = 2, Nombre = "Base de Datos",          Sigla = "SIS-0102", Creditos = 8 },
        new Materia { Id = 3, Nombre = "Ingeniería de Software",  Sigla = "SIS-0134", Creditos = 6 },
    };

    [HttpGet]
    public IActionResult ObtenerTodas()
    {
        return Ok(_materias);
    }

    [HttpGet("{id}")]
    public IActionResult ObtenerPorId(int id)
    {
        var materia = _materias.FirstOrDefault(m => m.Id == id);
        if (materia is null)
            return NotFound(new { mensaje = "Materia no encontrada" });
        return Ok(materia);
    }

    [HttpGet("buscar")]
    public IActionResult Buscar([FromQuery] string? nombre, [FromQuery] int? creditos)
    {
        if (nombre is null && creditos is null)
            return BadRequest(new { mensaje = "Debe indicar al menos un filtro de búsqueda" });

        var resultado = _materias.AsQueryable();

        if (nombre is not null)
            resultado = resultado.Where(m => m.Nombre.Contains(nombre, StringComparison.OrdinalIgnoreCase));

        if (creditos is not null)
            resultado = resultado.Where(m => m.Creditos == creditos);

        return Ok(resultado.ToList());
    }


    [HttpPost]
    public IActionResult Crear([FromBody] Materia materia)
    {
        if (string.IsNullOrWhiteSpace(materia.Nombre))
            return BadRequest(new { mensaje = "El nombre es obligatorio" });

        if (string.IsNullOrWhiteSpace(materia.Sigla))
            return BadRequest(new { mensaje = "La sigla es obligatoria" });

        if (materia.Creditos <= 0)
            return BadRequest(new { mensaje = "Los créditos deben ser mayor a 0" });

        materia.Id = _materias.Any()
            ? _materias.Max(m => m.Id) + 1
            : 1;

        _materias.Add(materia);
        return CreatedAtAction(nameof(ObtenerPorId), new { id = materia.Id }, materia);
    }

    [HttpPut("{id}")]
    public IActionResult Actualizar(int id, [FromBody] Materia datos)
    {
        if (string.IsNullOrWhiteSpace(datos.Nombre))
            return BadRequest(new { mensaje = "El nombre es obligatorio" });

        if (string.IsNullOrWhiteSpace(datos.Sigla))
            return BadRequest(new { mensaje = "La sigla es obligatoria" });

        if (datos.Creditos <= 0)
            return BadRequest(new { mensaje = "Los créditos deben ser mayor a 0" });

        var materia = _materias.FirstOrDefault(m => m.Id == id);
        if (materia is null)
            return NotFound(new { mensaje = "Materia no encontrada" });

        materia.Nombre   = datos.Nombre;
        materia.Sigla    = datos.Sigla;
        materia.Creditos = datos.Creditos;

        return Ok(materia);
    }

  
    [HttpPatch("{id}/creditos")]
    public IActionResult ActualizarCreditos(int id, [FromBody] Materia datos)
    {
        if (datos.Creditos <= 0)
            return BadRequest(new { mensaje = "Los créditos deben ser mayor a 0" });

        var materia = _materias.FirstOrDefault(m => m.Id == id);
        if (materia is null)
            return NotFound(new { mensaje = "Materia no encontrada" });

        materia.Creditos = datos.Creditos;
        return Ok(materia);
    }

    [HttpDelete("{id}")]
    public IActionResult Eliminar(int id)
    {
        var materia = _materias.FirstOrDefault(m => m.Id == id);
        if (materia is null)
            return NotFound(new { mensaje = "Materia no encontrada" });

        _materias.Remove(materia);
        return NoContent();
    }
}