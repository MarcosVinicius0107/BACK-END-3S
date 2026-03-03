using Filmes.WebAPI.DTO;
using Filmes.WebAPI.Interfaces;
using Filmes.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Filmes.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly IUsuarioRepository _usuarioRepository;

    public LoginController(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    [HttpPost]
    public IActionResult Login(LoginDTO loginDto)
    {
        try
        {
            Usuario usuarioBuscado = _usuarioRepository.BuscarPorEmailESenha(loginDto.Email!, loginDto.Senha!);

            if (usuarioBuscado == null)
            {
                return NotFound("Email ou senha inválidos!");
            }

            //Caso encontre o usuário, prosseguir para criação do token

            //1 - Definir as informações que serão fornecidas no token - Payload

            var claims = new[]
            {
                //formato da claim
                new Claim(JwtRegisteredClaimNames.Jti, usuarioBuscado.IdUsuario),

                new Claim(JwtRegisteredClaimNames.Email, usuarioBuscado.Email!),

                //existe a possibilidade de criar uma claim personalizada
                //new Claim("claim_personalizada", "Valor da claim personalizada")
            };

            //2 - Definir a chave de acesso ao token
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("filmes-chave-autenticacao-webapi-dev"));

            //3 - Definir as credenciais do token - Header
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //4 - Gerar o token
            var token = new JwtSecurityToken(
                issuer: "Filmes.WebAPI", //Emissor do token
                audience: "Filmes.WebAPI", //Destinatário do token
                claims: claims, //Informações do usuário
                expires: DateTime.Now.AddMinutes(5), //Tempo de expiração do token
                signingCredentials: creds //Credenciais do token
            );

            //5 - Retornar o token para o cliente
            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            });

        }
        catch (Exception erro)
        {

            return BadRequest(erro.Message);
        }
    }

}
