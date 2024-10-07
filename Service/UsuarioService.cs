using EventosAPI.Models;
using EventosAPI.Repositories;
using Microsoft.AspNetCore.Identity; // Adicione a referência ao namespace Identity

namespace EventosAPI.Services
{
    public class UsuarioService
    {
        private readonly IUsuarioRepository _repository;
        private readonly PasswordHasher<TbUsuario> _passwordHasher;

        public UsuarioService(IUsuarioRepository repository)
        {
            _repository = repository;
            _passwordHasher = new PasswordHasher<TbUsuario>();
        }

        // Método para registrar um novo usuário
        public async Task<string> Register(TbUsuario usuario)
        {
            // Verifica se o email já está em uso
            if (await _repository.EmailExists(usuario.Email))
            {
                return "Email já está em uso.";
            }

            // Criptografa a senha
            usuario.Senha = _passwordHasher.HashPassword(usuario, usuario.Senha);

            // Adiciona o usuário no banco de dados
            await _repository.AddUsuario(usuario);

            return "Usuário registrado com sucesso!";
        }

        // Método para login
        public async Task<TbUsuario?> Login(string email, string senha)
        {
            var usuario = await _repository.GetUsuarioByEmail(email);


            if (usuario == null || _passwordHasher.VerifyHashedPassword(usuario, usuario.Senha, senha) != PasswordVerificationResult.Success)
            {
                return null;
            }
            return usuario;
        }


        public async Task<TbUsuario?> GetUsuarioByEmail(string email)
        {
            var usuario = await _repository.GetUsuarioByEmail(email);

            if (usuario == null)
            {
                return null;
            }
            return usuario;
        }

    }
}
