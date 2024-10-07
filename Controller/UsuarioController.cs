using Microsoft.AspNetCore.Mvc;
using EventosAPI.Models;
using EventosAPI.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using BCrypt.Net;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace EventosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;

        public UsuariosController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] TbUsuario usuario)
        {
            var result = await _usuarioService.Register(usuario);

            if (result != "Usuário registrado com sucesso!")
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] TbUsuario usuario)
        {
            var usuarios = await _usuarioService.Login(usuario.Email, usuario.Senha);

            if (usuarios == null)
            {
                return Unauthorized("Email ou Senha Inválidos.");
            }


            // Aqui podemos adicionar a geração de um token JWT
            return Ok(new { message = "Login realizado com sucesso!" });

        }

        [HttpPost("ConsultaUser")]
        public async Task<IActionResult> ConsultaUser([FromBody] TbUsuario usuario)
        {
            var usuarios = await _usuarioService.Login(usuario.Email, usuario.Senha);

            if (usuarios == null)
            {
                return Unauthorized("Email ou Senha Inválidos.");
            }

            // Aqui podemos adicionar a geração de um token JWT
            return Ok(new { message = "Login realizado com sucesso!" });

        }


        [HttpPost("GerarToken")]
        public async Task<IActionResult> GerarToken(string email)
        {
            var email2 = User.FindFirst(ClaimTypes.Name)?.Value;
            var usuario = await _usuarioService.GetUsuarioByEmail(email);

            var serviceToken = new JwtService();

            var token = serviceToken.GenerateToken(usuario.Id);

            if (token == null)
            {
                return Unauthorized("Não Foi Possivel Gerar o Token");
            }

            // Aqui podemos adicionar a geração de um token JWT
            return Ok(new { message = "Login realizado com sucesso!", token = token });

        }




    }
}


