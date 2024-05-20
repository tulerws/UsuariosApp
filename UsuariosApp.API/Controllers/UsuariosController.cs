using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UsuariosApp.API.DTOs;
using UsuariosApp.API.Security;
using UsuariosApp.Domain.Entities;
using UsuariosApp.Domain.Interfaces.Services;

namespace UsuariosApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        //atributo
        private readonly IUsuarioDomainService _usuarioDomainService;

        //método construtor para inicializar o atributo
        public UsuariosController(IUsuarioDomainService usuarioDomainService)
        {
            _usuarioDomainService = usuarioDomainService;
        }

        [HttpPost]
        [Route("autenticar")]
        [ProducesResponseType(typeof(AutenticarUsuarioResponseDTO), 200)]
        public IActionResult Autenticar(AutenticarUsuarioRequestDTO dto)
        {
            try
            {
                //autenticando o usuário na camada de domínio
                var usuario = _usuarioDomainService.Autenticar(dto.Email, dto.Senha);

                //retornar os dados do usuário
                var response = new AutenticarUsuarioResponseDTO
                {
                    Id = usuario.Id,
                    Nome = usuario.Nome,
                    Email = usuario.Email,
                    DataHoraAcesso = DateTime.Now,
                    AccessToken = JwtTokenSecurity.GenerateToken(usuario.Id),
                    DataHoraExpiracao = DateTime.Now.AddHours(JwtTokenSecurity.ExpirationInHours)
                };

                //HTTP 200 (OK)
                return StatusCode(200, new { message = "Usuário autenticado com sucesso", response });
            }
            catch(ApplicationException e)
            {
                //HTTP 401 (UNAUTHORIZED)
                return StatusCode(401, new { e.Message });
            }
            catch(Exception e)
            {
                //HTTP 500 (INTERNAL SERVER ERROR)
                return StatusCode(500, new { e.Message });
            }
        }

        [HttpPost]
        [Route("criar")]
        [ProducesResponseType(typeof(CriarUsuarioResponseDTO), 201)]
        public IActionResult Criar(CriarUsuarioRequestDTO dto) 
        {
            try
            {
                var usuario = new Usuario
                {
                    Nome = dto.Nome,
                    Email = dto.Email,
                    Senha = dto.Senha
                };

                _usuarioDomainService.CriarConta(usuario);

                var response = new CriarUsuarioResponseDTO
                {
                    Id = usuario.Id,
                    Nome = usuario.Nome,
                    Email = usuario.Email,
                    DataHoraCadastro = usuario.DataHoraCadastro.Value
                };

                //HTTP 201 (CREATED)
                return StatusCode(201, new { message = "Conta de usuário criado com sucesso.", response });
            }
            catch(ApplicationException e)
            {
                //HTTP 422 (UNPROCESSABLE ENTITY)
                return StatusCode(422, new { e.Message });
            }
            catch(Exception e)
            {
                //HTTP 500 (INTERNAL SERVER ERROR)
                return StatusCode(500, new { e.Message });
            }
        }
    }
}
