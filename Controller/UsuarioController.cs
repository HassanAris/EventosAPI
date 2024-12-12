using Microsoft.AspNetCore.Mvc;
using EventosAPI.Models;
using EventosAPI.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using BCrypt.Net;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using EventosAPI.DTOs;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Cors;

namespace EventosAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;
        private readonly JwtService _jwtService;

        public UsuariosController(UsuarioService usuarioService, JwtService jwtService)
        {
            _usuarioService = usuarioService;
            _jwtService = jwtService;
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
        public async Task<IActionResult> Login([FromBody] LoginUsuarioDTO usuario)
        {
            var usuarios = await _usuarioService.Login(usuario.Email, usuario.Senha);

            if (usuarios == null)
            {
                return Unauthorized("Email ou Senha Inválidos.");
            }


            // Aqui podemos adicionar a geração de um token JWT
            return Ok(new { message = "Login realizado com sucesso!" });

        }

        [HttpGet("ConsultaUser")]
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

        [HttpGet("ObterUsuarios")]
        public async Task<IActionResult> ObterUsuarios()
        {
            var usuarios = await _usuarioService.GetAllUsuario();

            if (usuarios == null) return NotFound();

            return Ok(usuarios);

        }

        [HttpGet("ListarUsuariosPorEvento/{id}")]
        public async Task<IActionResult> ListarUsuariosPorEvento(int id)
        {
            var usuarios = await _usuarioService.ListarUsuariosPorEvento(id);

            if (usuarios == null) return NotFound();
            return Ok(usuarios);

        }

        [HttpGet("ObterUsuarioLogado")]
        public async Task<IActionResult> ObterUsuarioLogado()
        {
            var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var usuarios = await _usuarioService.GetUsuarioById(userId);

            if (usuarios == null) return NotFound();

            return Ok(usuarios);

        }

        [HttpGet("ObterUsuarioOrg")]
        public async Task<IActionResult> ListarUsuariosPorOrg()
        {
            var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var usuarios = await _usuarioService.ListarUsuariosPorOrg(userId);

            if (usuarios == null) return NotFound();

            return Ok(usuarios);

        }

        [HttpPost("AtualizarInstituicaoId")]
        public async Task<IActionResult> AtualizarInstituicaoId(string nome, string email)
        {
            var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var usuarios = await _usuarioService.AtualizarInstituicaoId(userId, nome, email);

            if (usuarios == null) return NotFound();

            return Ok(usuarios);
        }


        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {

            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            if (!string.IsNullOrEmpty(token))
            {
                await _jwtService.RevokeTokenAsync(token);
            }

            // Não há necessidade de limpar cookies ou sessões para JWT.
            return Ok("Logout realizado com sucesso.");
        }



    }
}


