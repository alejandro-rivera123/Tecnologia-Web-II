using Microsoft.EntityFrameworkCore;
using Estudiantes.Data;
using Estudiantes.Interfaces;
using Estudiantes.Models;

namespace Estudiantes.Services;

public class EstudianteServicio : IEstudianteServicio
{
    private readonly AppDbContext _context;

    public EstudianteServicio(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Estudiante>> ObtenerTodosAsync() =>
        await _context.Estudiantes.ToListAsync();

    public async Task<Estudiante?> ObtenerPorIdAsync(int id) =>
        await _context.Estudiantes.FindAsync(id);

    public async Task<Estudiante> CrearAsync(Estudiante estudiante)
    {
        _context.Estudiantes.Add(estudiante);
        await _context.SaveChangesAsync();
        return estudiante;
    }

    public async Task<Estudiante?> ActualizarAsync(int id, Estudiante datos)
    {
        var estudiante = await _context.Estudiantes.FindAsync(id);
        if (estudiante is null) return null;

        estudiante.Nombre   = datos.Nombre;
        estudiante.Promedio = datos.Promedio;
        estudiante.Carrera  = datos.Carrera;
        await _context.SaveChangesAsync();
        return estudiante;
    }

    public async Task<bool> EliminarAsync(int id)
    {
        var estudiante = await _context.Estudiantes.FindAsync(id);
        if (estudiante is null) return false;

        _context.Estudiantes.Remove(estudiante);
        await _context.SaveChangesAsync();
        return true;
    }
}