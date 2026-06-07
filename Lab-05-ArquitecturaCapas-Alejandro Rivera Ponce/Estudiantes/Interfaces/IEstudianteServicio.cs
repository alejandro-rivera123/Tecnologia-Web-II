using Estudiantes.Models;

namespace Estudiantes.Interfaces;
//chatsito me lo confirmo
public interface IEstudianteServicio
{
    Task<List<Estudiante>> ObtenerTodosAsync();
    Task<Estudiante?> ObtenerPorIdAsync(int id);
    Task<Estudiante> CrearAsync(Estudiante estudiante);
    Task<Estudiante?> ActualizarAsync(int id, Estudiante datos);
    Task<bool> EliminarAsync(int id);
}