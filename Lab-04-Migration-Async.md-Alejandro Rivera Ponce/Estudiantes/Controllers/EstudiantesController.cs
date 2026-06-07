using Estudiantes.Data;
using Estudiantes.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Estudiantes.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EstudiantesController : ControllerBase
{
    private readonly AppDbContext _context;

    public EstudiantesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> ObtenerTodos()
    {
        var lista = await _context.Estudiantes.ToListAsync();
        return Ok(lista);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObtenerPorId(int id)
    {
        var estudiante = await _context.Estudiantes.FindAsync(id);
        if (estudiante is null)
            return NotFound(new { mensaje = $"Estudiante con id {id} no encontrado." });
        return Ok(estudiante);
    }

    [HttpPost]
    public async Task<IActionResult> Crear([FromBody] Estudiante nuevo)
    {
        _context.Estudiantes.Add(nuevo);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(ObtenerPorId), new { id = nuevo.Id }, nuevo);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Actualizar(int id, [FromBody] Estudiante datos)
    {
        var existente = await _context.Estudiantes.FindAsync(id);
        if (existente is null)
            return NotFound(new { mensaje = $"Estudiante con id {id} no encontrado." });

        existente.Nombre   = datos.Nombre;
        existente.Carrera  = datos.Carrera;
        existente.Promedio = datos.Promedio;
        await _context.SaveChangesAsync();
        return Ok(existente);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        var existente = await _context.Estudiantes.FindAsync(id);
        if (existente is null)
            return NotFound(new { mensaje = $"Estudiante con id {id} no encontrado." });

        _context.Estudiantes.Remove(existente);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}