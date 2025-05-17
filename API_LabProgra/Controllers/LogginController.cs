using Microsoft.AspNetCore.Mvc;
using System.Text;
using API_LabProgra.DTOS;
using API_LabProgra.Servicios;
using System.ComponentModel.DataAnnotations;


namespace API_LabProgra.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ICuentaService _cuentaService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<LoginController> _logger;

        public LoginController(
            ICuentaService cuentaService,
            IConfiguration configuration,
            ILogger<LoginController> logger)
        {
            _cuentaService = cuentaService;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { message = "Datos de inicio de sesión inválidos", errors = ModelState });
                }

                var usuario = await _cuentaService.ValidateUser(model.Username, model.Password);

                if (usuario == null)
                {
                    _logger.LogWarning("Intento de login fallido para usuario: {Username}", model.Username);
                    return Unauthorized(new { message = "Usuario o contraseña incorrectos" });
                }

                var tokenService = new TokenService(_configuration);
                var token = tokenService.GenerateToken(usuario);

                return Ok(new LoginResponse
                {
                    IdUsuario = usuario.IdUsuario,
                    Nombre = $"{usuario.Nombre}",
                    Apellidos = $"{usuario.Apellidos}",
                    Usuario = usuario.Usuario,
                    Correo = usuario.Correo,
                    Rol = usuario.Rol,
                    Token = token
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en el proceso de login para usuario {Username}", model.Username);
                return StatusCode(500, new { message = "Error interno del servidor" });
            }
        }
    }

    // a solicitud de login
    public class LoginRequest
    {
        [Required(ErrorMessage = "El nombre de usuario es obligatorio")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El usuario debe tener entre 3 y 50 caracteres")]
        public string Username { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "La contraseña debe tener entre 6 y 100 caracteres")]
        public string Password { get; set; }
    }

    // respuesta de login
    public class LoginResponse
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Usuario { get; set; }
        public string Correo { get; set; }
        public int Rol { get; set; }
        public string Token { get; set; }
    }
}
