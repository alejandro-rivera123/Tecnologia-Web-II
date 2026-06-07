using Estudiantes.Data;
using Estudiantes.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Estudiantes.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MateriasController : ControllerBase
{
    private readonly AppDbContext _context;

    public MateriasController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> ObtenerTodas()
    {
        var lista = await _context.Materias.ToListAsync();
        return Ok(lista);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObtenerPorId(int id)
    {
        var materia = await _context.Materias.FindAsync(id);
        if (materia is null)
            return NotFound(new { mensaje = $"Materia con id {id} no encontrada." });
        return Ok(materia);
    }

    [HttpGet("buscar")]
    public async Task<IActionResult> Buscar([FromQuery] string? nombre, [FromQuery] int? creditos)
    {
        if (nombre is null && creditos is null)
            return BadRequest(new { mensaje = "Debe indicar al menos un filtro de búsqueda" });

        var query = _context.Materias.AsQueryable();

        if (nombre is not null)
            query = query.Where(m => m.Nombre.Contains(nombre));

        if (creditos is not null)
            query = query.Where(m => m.Creditos == creditos);

        return Ok(await query.ToListAsync());
    }

    [HttpPost]
    public async Task<IActionResult> Crear([FromBody] Materia materia)
    {
        if (string.IsNullOrWhiteSpace(materia.Nombre))
            return BadRequest(new { mensaje = "El nombre es obligatorio" });
        if (string.IsNullOrWhiteSpace(materia.Sigla))
            return BadRequest(new { mensaje = "La sigla es obligatoria" });
        if (materia.Creditos <= 0)
            return BadRequest(new { mensaje = "Los créditos deben ser mayor a 0" });

        _context.Materias.Add(materia);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(ObtenerPorId), new { id = materia.Id }, materia);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Actualizar(int id, [FromBody] Materia datos)
    {
        if (string.IsNullOrWhiteSpace(datos.Nombre))
            return BadRequest(new { mensaje = "El nombre es obligatorio" });
        if (string.IsNullOrWhiteSpace(datos.Sigla))
            return BadRequest(new { mensaje = "La sigla es obligatoria" });
        if (datos.Creditos <= 0)
            return BadRequest(new { mensaje = "Los créditos deben ser mayor a 0" });

        var existente = await _context.Materias.FindAsync(id);
        if (existente is null)
            return NotFound(new { mensaje = $"Materia con id {id} no encontrada." });

        existente.Nombre   = datos.Nombre;
        existente.Sigla    = datos.Sigla;
        existente.Creditos = datos.Creditos;
        await _context.SaveChangesAsync();
        return Ok(existente);
    }

    [HttpPatch("{id}/creditos")]
    public async Task<IActionResult> ActualizarCreditos(int id, [FromBody] Materia datos)
    {
        if (datos.Creditos <= 0)
            return BadRequest(new { mensaje = "Los créditos deben ser mayor a 0" });

        var existente = await _context.Materias.FindAsync(id);
        if (existente is null)
            return NotFound(new { mensaje = $"Materia con id {id} no encontrada." });

        existente.Creditos = datos.Creditos;
        await _context.SaveChangesAsync();
        return Ok(existente);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        var existente = await _context.Materias.FindAsync(id);
        if (existente is null)
            return NotFound(new { mensaje = $"Materia con id {id} no encontrada." });

        _context.Materias.Remove(existente);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}