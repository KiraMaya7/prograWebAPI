using API_LabProgra.Models;
using API_LabProgra.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_LabProgra.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistorialMedicoController : ControllerBase
    {
        private readonly SistemaMedicoContext _context;
        private readonly ILogger<HistorialMedicoController> _logger;

        public HistorialMedicoController(SistemaMedicoContext context, ILogger<HistorialMedicoController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HistorialMedicoDTO>>> GetHistorialMedico()
        {
            try
            {
                var historiales = await _context.HistorialMedicos
                    .Include(h => h.IdDoctorNavigation)
                        .ThenInclude(d => d.IdUsuarioNavigation)
                    .Include(h => h.IdUsuarioNavigation)
                    .ToListAsync();

                return historiales.Select(h => HistorialMedicoDTO.FromEntity(h)).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los historiales médicos");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HistorialMedicoDTO>> GetHistorialMedico(int id)
        {
            try
            {
                var historial = await _context.HistorialMedicos
                    .Include(h => h.IdDoctorNavigation)
                        .ThenInclude(d => d.IdUsuarioNavigation)
                    .Include(h => h.IdUsuarioNavigation)
                    .FirstOrDefaultAsync(h => h.HistorialId == id);

                if (historial == null)
                {
                    return NotFound($"Historial médico con ID {id} no encontrado");
                }

                return HistorialMedicoDTO.FromEntity(historial);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener historial médico con ID {Id}", id);
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpGet("Paciente/{pacienteId}")]
        public async Task<ActionResult<IEnumerable<HistorialMedicoDTO>>> GetHistorialMedicoPaciente(int pacienteId)
        {
            try
            {
                var pacienteExists = await _context.Cuenta.AnyAsync(u => u.IdUsuario == pacienteId);
                if (!pacienteExists)
                {
                    return NotFound($"Paciente con ID {pacienteId} no encontrado");
                }

                var historiales = await _context.HistorialMedicos
                    .Include(h => h.IdDoctorNavigation)
                        .ThenInclude(d => d.IdUsuarioNavigation)
                    .Where(h => h.IdUsuario == pacienteId)
                    .ToListAsync();

                if (!historiales.Any())
                {
                    return NotFound($"No se encontraron historiales médicos para el paciente con ID {pacienteId}");
                }

                return historiales.Select(h => HistorialMedicoDTO.FromEntity(h)).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener historiales médicos del paciente con ID {PacienteId}", pacienteId);
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpGet("Doctor/{doctorId}")]
        public async Task<ActionResult<IEnumerable<HistorialMedicoDTO>>> GetHistorialMedicoDoctor(int doctorId)
        {
            try
            {
                var doctorExists = await _context.Doctores.AnyAsync(d => d.IdDoctor == doctorId);
                if (!doctorExists)
                {
                    return NotFound($"Doctor con ID {doctorId} no encontrado");
                }

                var historiales = await _context.HistorialMedicos
                    .Include(h => h.IdUsuarioNavigation)
                    .Where(h => h.IdDoctor == doctorId)
                    .ToListAsync();

                if (!historiales.Any())
                {
                    return NotFound($"No se encontraron historiales médicos para el doctor con ID {doctorId}");
                }

                return historiales.Select(h => HistorialMedicoDTO.FromEntity(h)).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener historiales médicos del doctor con ID {DoctorId}", doctorId);
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPost]
        public async Task<ActionResult<HistorialMedicoDTO>> CreateHistorialMedico(HistorialMedicoCreateDTO historialDTO)
        {
            try
            {
 
                var doctorExists = await _context.Doctores.AnyAsync(d => d.IdDoctor == historialDTO.IdDoctor);
                if (!doctorExists)
                {
                    return BadRequest($"El doctor con ID {historialDTO.IdDoctor} no existe");
                }

                var pacienteExists = await _context.Cuenta.AnyAsync(u => u.IdUsuario == historialDTO.IdUsuario);
                if (!pacienteExists)
                {
                    return BadRequest($"El paciente con ID {historialDTO.IdUsuario} no existe");
                }

                var historial = historialDTO.ToEntity();

                _context.HistorialMedicos.Add(historial);
                await _context.SaveChangesAsync();

                var historialCreado = await _context.HistorialMedicos
                    .Include(h => h.IdDoctorNavigation)
                        .ThenInclude(d => d.IdUsuarioNavigation)
                    .Include(h => h.IdUsuarioNavigation)
                    .FirstOrDefaultAsync(h => h.HistorialId == historial.HistorialId);

                return CreatedAtAction(nameof(GetHistorialMedico), new { id = historial.HistorialId }, HistorialMedicoDTO.FromEntity(historialCreado));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear historial médico");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPatch("{id}/Diagnostico")]
        public async Task<IActionResult> UpdateDiagnostico(int id, [FromBody] DiagnosticoUpdateDTO diagnosticoDTO)
        {
            try
            {
                var historial = await _context.HistorialMedicos.FindAsync(id);
                if (historial == null)
                {
                    return NotFound($"Historial médico con ID {id} no encontrado");
                }

                historial.Diagnostico = diagnosticoDTO.Diagnostico;
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar diagnóstico del historial médico con ID {Id}", id);
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPatch("{id}/Receta")]
        public async Task<IActionResult> UpdateReceta(int id, [FromBody] RecetaUpdateDTO recetaDTO)
        {
            try
            {
                var historial = await _context.HistorialMedicos.FindAsync(id);
                if (historial == null)
                {
                    return NotFound($"Historial médico con ID {id} no encontrado");
                }

                historial.Receta = recetaDTO.Receta;
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar receta del historial médico con ID {Id}", id);
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}