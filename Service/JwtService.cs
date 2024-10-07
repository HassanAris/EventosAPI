using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

public class JwtService
{
    private readonly string _secretKey;
    private readonly string _issuer;
    private readonly string _audience;

    public JwtService()
    {
        _secretKey = "YqB4a1WxM6Nf7Pv2KdZrQbJ9XsUvTgHc"; // Você pode mover isso para um arquivo de configuração
        _issuer = "https://localhost:44329/";
    }

    public string GenerateToken(int id)
    {
        // Verifique se o ID é válido
        if (id <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(id), "O ID deve ser maior que zero.");
        }

        // Configuração da chave de segurança
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // Adiciona o ID como claim
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, id.ToString()) // Usa o ID do usuário
        };

        // Gera o token JWT
        var token = new JwtSecurityToken(
            issuer: _issuer,
            audience: _issuer,
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

}
