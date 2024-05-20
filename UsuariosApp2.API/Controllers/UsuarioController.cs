using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UsuariosApp.API.DTOs;
using UsuariosApp2.API.DTOs.RequestDTO;
using UsuariosApp2.API.DTOs.ResponseDTO;
using UsuariosApp2.API.Security;
using UsuariosApp2.Domain.Entities;
using UsuariosApp2.Domain.Interfaces.Services;

namespace UsuariosApp2.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        //atributo
        private readonly IUsuarioDomainService _usuarioDomainService;

        //método construtor
        public UsuarioController(IUsuarioDomainService usuarioDomainService)
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
                var usuario = _usuarioDomainService.Autenticar(dto.Email, dto.Senha);

                if (usuario == null)
                {
                    return NotFound();
                }
                else
                {
                    var response = new AutenticarUsuarioResponseDTO
                    {
                        Id = usuario.Id,
                        Nome = usuario.Nome,
                        Email = usuario.Email,
                        DataHoraAcesso = DateTime.Now,
                        AccessToken = JWTTokenSecurity.GenerateToken(usuario.Id),
                        DataHoraExpiracao = DateTime.Now.AddHours(JWTTokenSecurity.ExpirationInHours)
                    };

                    return Ok(new { message = "Usuário autenticado com sucesso", response });
                }
            }
            catch(ApplicationException e)
            {
                //HTTP 401(UNAUTHORIZED)
                return Unauthorized(new { e.Message });
            }
            catch(Exception e)
            {
                return StatusCode(500, new { e.Message } );
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
                    Senha = dto.Senha,
                    Email = dto.Email
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
                return StatusCode(201, new { message = "Usuário cadastrado com sucesso.", response });
            }
            catch(ApplicationException e)
            {

                //HTTP 422 (UNPROCESSABLE ENTITY)
                return UnprocessableEntity(new { e.Message });
            }
            catch (Exception e)
            {

                //HTTP 500 (INTERNAL SERVER ERROR)
                return StatusCode(500, new { e.Message } );
            }
        }
    }
}
