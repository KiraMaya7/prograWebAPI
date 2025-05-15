using API_LabProgra.Models;
using API_LabProgra.Servicios;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace API_LabProgra.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuentaController : ControllerBase
    {
        private readonly ICuentaService _cuentaService;

        public CuentaController(ICuentaService cuentaService)
        {
            _cuentaService = cuentaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cuentum>>> GetCuentas()
        {
            try
            {
                var cuentas = await _cuentaService.Alluser();
                return Ok(cuentas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cuentum>> GetCuenta(int id)
        {
            try
            {
                var cuenta = await _cuentaService.GetUserById(id);

                if (cuenta == null)
                {
                    return NotFound($"Usuario con ID {id} no encontrado");
                }

                return Ok(cuenta);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpGet("rol/{rolId}")]
        public async Task<ActionResult<IEnumerable<Cuentum>>> GetCuentasByRol(int rolId)
        {
            try
            {
                var cuentas = await _cuentaService.GetUsersByRole(rolId);
                return Ok(cuentas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpGet("username/{username}")]
        public async Task<ActionResult<Cuentum>> GetCuentaByUsername(string username)
        {
            try
            {
                var cuenta = await _cuentaService.GetUserByUsername(username);

                if (cuenta == null)
                {
                    return NotFound($"Usuario '{username}' no encontrado");
                }

                return Ok(cuenta);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Cuentum>> CreateCuenta(Cuentum cuenta)
        {
            try
            {
                if (await _cuentaService.UsernameExists(cuenta.Usuario))
                {
                    return BadRequest("El nombre de usuario ya está en uso");
                }

                if (await _cuentaService.EmailExists(cuenta.Correo))
                {
                    return BadRequest("El correo electrónico ya está registrado");
                }

                var resultado = await _cuentaService.AddUser(cuenta);

                if (resultado > 0)
                {
                    return CreatedAtAction(nameof(GetCuenta), new { id = resultado }, cuenta);
                }
                else
                {
                    return BadRequest("No se pudo crear la cuenta");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCuenta(int id, Cuentum cuenta)
        {
            if (id != cuenta.IdUsuario)
            {
                return BadRequest("ID de usuario no coincide");
            }

            try
            {
                var resultado = await _cuentaService.UpdateUser(cuenta);

                if (resultado == 0)
                {
                    return NotFound($"Usuario con ID {id} no encontrado");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
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
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<Cuentum>> Login([FromBody] LoginModel login)
        {
            try
            {
                var usuario = await _cuentaService.ValidateUser(login.Username, login.Password);

                if (usuario == null)
                {
                    return Unauthorized("Credenciales inválidas");
                }

                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }

    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}