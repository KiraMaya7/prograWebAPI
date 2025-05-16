using API_LabProgra.Models;
using API_LabProgra.Servicios;
using API_LabProgra.DTOS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace API_LabProgra.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuentaController : ControllerBase
    {
        private readonly ICuentaService _cuentaService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<CuentaController> _logger;

        public CuentaController(
            ICuentaService cuentaService,
            IConfiguration configuration,
            ILogger<CuentaController> logger)
        {
            _cuentaService = cuentaService;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DTOCuenta>>> GetCuentas()
        {
            try
            {
                var cuentas = await _cuentaService.Alluser();
                var cuentasDTO = cuentas.Select(c => DTOCuenta.FromEntity(c)).ToList();
                return Ok(cuentasDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todas las cuentas");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DTOCuenta>> GetCuenta(int id)
        {
            try
            {
                var cuenta = await _cuentaService.GetUserById(id);

                if (cuenta == null)
                {
                    return NotFound($"Usuario con ID {id} no encontrado");
                }

                return Ok(DTOCuenta.FromEntity(cuenta));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener cuenta con ID {Id}", id);
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpGet("rol/{rolId}")]
        public async Task<ActionResult<IEnumerable<DTOCuenta>>> GetCuentasByRol(int rolId)
        {
            try
            {
                var cuentas = await _cuentaService.GetUsersByRole(rolId);
                var cuentasDTO = cuentas.Select(c => DTOCuenta.FromEntity(c)).ToList();
                return Ok(cuentasDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener cuentas con rol {RolId}", rolId);
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpGet("username/{username}")]
        public async Task<ActionResult<DTOCuenta>> GetCuentaByUsername(string username)
        {
            try
            {
                var cuenta = await _cuentaService.GetUserByUsername(username);

                if (cuenta == null)
                {
                    return NotFound($"Usuario '{username}' no encontrado");
                }

                return Ok(DTOCuenta.FromEntity(cuenta));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener cuenta con username {Username}", username);
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPost]
        public async Task<ActionResult<DTOCuenta>> CreateCuenta(CreateDTOCuenta DTOCuenta)
        {
            try
            {

                if (string.IsNullOrEmpty(DTOCuenta.Usuario) || DTOCuenta.Usuario.Length < 4)
                {
                    return BadRequest("El nombre de usuario debe tener al menos 4 caracteres");
                }

                if (string.IsNullOrEmpty(DTOCuenta.Contraseña) || DTOCuenta.Contraseña.Length < 6)
                {
                    return BadRequest("La contraseña debe tener al menos 6 caracteres");
                }

                if (string.IsNullOrEmpty(DTOCuenta.Correo) || !new EmailAddressAttribute().IsValid(DTOCuenta.Correo))
                {
                    return BadRequest("El correo electrónico no es válido");
                }

                if (await _cuentaService.UsernameExists(DTOCuenta.Usuario))
                {
                    return BadRequest("El nombre de usuario ya está en uso");
                }

                // Verificar si el correo ya existe
                if (await _cuentaService.EmailExists(DTOCuenta.Correo))
                {
                    return BadRequest("El correo electrónico ya está registrado");
                }

                // Convertir DTO a entidad
                var cuenta = DTOCuenta.ToEntity();

                // Hashear la contraseña
                cuenta.Contraseña = HashPassword(cuenta.Contraseña);

                var resultado = await _cuentaService.AddUser(cuenta);

                if (resultado > 0)
                {
                    return StatusCode(201, new
                    {
                        message = "Cuenta creada exitosamente",
                        id = resultado
                    });
                }
                else
                {
                    return BadRequest("No se pudo crear la cuenta");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear cuenta");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCuenta(int id, UpdateDTOCuenta DTOCuenta)
        {
            if (id != DTOCuenta.IdUsuario)
            {
                return BadRequest("ID de usuario no coincide");
            }

            try
            {
                if (!string.IsNullOrEmpty(DTOCuenta.Correo) && !new EmailAddressAttribute().IsValid(DTOCuenta.Correo))
                {
                    return BadRequest("El correo electrónico no es válido");
                }

                var cuenta = DTOCuenta.ToEntity();

                if (!string.IsNullOrEmpty(cuenta.Contraseña))
                {
                    cuenta.Contraseña = HashPassword(cuenta.Contraseña);
                }

                var resultado = await _cuentaService.UpdateUser(cuenta);

                if (resultado == 0)
                {
                    return NotFound($"Usuario con ID {id} no encontrado");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar cuenta con ID {Id}", id);
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCuenta(int id)
        {
            try
            {
                var resultado = await _cuentaService.DeleteUser(id);

                if (resultado == 0)
                {
                    return NotFound($"Usuario con ID {id} no encontrado");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar cuenta con ID {Id}", id);
                return StatusCode(500, "Error interno del servidor");
            }
        }


        // Método para hashear contraseñas
        private string HashPassword(string password)
        {
            return Convert.ToBase64String(
                System.Security.Cryptography.SHA256.HashData(
                    System.Text.Encoding.UTF8.GetBytes(password)
                )
            );
        }
    }
}