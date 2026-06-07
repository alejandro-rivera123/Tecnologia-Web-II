using Estudiantes.Interfaces;
using Estudiantes.Models;
using Microsoft.AspNetCore.Mvc;

namespace Estudiantes.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EstudiantesController : ControllerBase
{
    private readonly IEstudianteServicio _servicio;

    public EstudiantesController(IEstudianteServicio servicio)
    {
        _servicio = servicio;
    }

    [HttpGet]
    public async Task<IActionResult> ObtenerTodos() =>
        Ok(await _servicio.ObtenerTodosAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> ObtenerPorId(int id)
    {
        var estudiante = await _servicio.ObtenerPorIdAsync(id);
        if (estudiante is null)
            return NotFound(new { mensaje = $"Estudiante con id {id} no encontrado." });
        return Ok(estudiante);
    }

    [HttpPost]
    public async Task<IActionResult> Crear([FromBody] Estudiante nuevo)
    {
        var creado = await _servicio.CrearAsync(nuevo);
        return CreatedAtAction(nameof(ObtenerPorId), new { id = creado.Id }, creado);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Actualizar(int id, [FromBody] Estudiante datos)
    {
        var actualizado = await _servicio.ActualizarAsync(id, datos);
        if (actualizado is null)
            return NotFound(new { mensaje = $"Estudiante con id {id} no encontrado." });
        return Ok(actualizado);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        var eliminado = await _servicio.EliminarAsync(id);
        if (!eliminado)
            return NotFound(new { mensaje = $"Estudiante con id {id} no encontrado." });
        return NoContent();
    }
}