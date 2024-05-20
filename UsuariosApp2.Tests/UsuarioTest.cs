using Bogus;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.API.DTOs;
using UsuariosApp2.API.DTOs.RequestDTO;
using Xunit;

namespace UsuariosApp.Tests
{
    public class UsuariosTest
    {
        [Fact]
        public CriarUsuarioRequestDTO Criar_Usuario_Com_Sucesso_Test()
        {
            #region Gerar os dados do usuário

            //instanciando a biblioteca do Bogus (FAKE DATA)
            var faker = new Faker("pt_BR");

            //Criando os dados da requisição para cadastrar o usuário
            var request = new CriarUsuarioRequestDTO
            {
                Nome = faker.Person.FullName,
                Email = faker.Internet.Email(),
                Senha = "@Teste123",
                SenhaConfirmacao = "@Teste123"
            };

            //serializando os dados em JSON
            var json = new StringContent(JsonConvert.SerializeObject(request),
                Encoding.UTF8, "application/json");

            #endregion

            #region Enviando a requisição para a API

            var client = new WebApplicationFactory<Program>().CreateClient();
            var result = client.PostAsync("/api/usuario/criar", json).Result;

            #endregion

            #region Verificar a resposta

            result.StatusCode.Should().Be(HttpStatusCode.Created);
            result.Content.ReadAsStringAsync().Result
                .Should().Contain("Usuário cadastrado com sucesso.");

            #endregion

            return request;
        }

        [Fact]
        public void Email_Ja_Cadastrado_Test()
        {
            #region Criando um usuário na API

            //realizando o cadastro de um usuário na API
            var request = Criar_Usuario_Com_Sucesso_Test();

            //serializando os dados em JSON
            var json = new StringContent(JsonConvert.SerializeObject(request),
                Encoding.UTF8, "application/json");

            #endregion

            #region Enviando a requisição para a API

            var client = new WebApplicationFactory<Program>().CreateClient();
            var result = client.PostAsync("/api/usuario/criar", json).Result;

            #endregion

            #region Verificar a resposta

            result.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            result.Content.ReadAsStringAsync().Result
                .Should().Contain("O email informado já está cadastrado. Tente outro.");

            #endregion
        }

        [Fact]
        public void Autenticar_Usuario_Com_Sucesso_Test()
        {
            #region Criando um usuário na API

            //realizando o cadastro de um usuário na API
            var request = Criar_Usuario_Com_Sucesso_Test();

            //serializando os dados em JSON
            var json = new StringContent(JsonConvert.SerializeObject(request),
                Encoding.UTF8, "application/json");

            #endregion

            #region Enviando a requisição para a API

            var client = new WebApplicationFactory<Program>().CreateClient();
            var result = client.PostAsync("/api/usuario/autenticar", json).Result;

            #endregion

            #region Verificar a resposta

            result.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Content.ReadAsStringAsync().Result
                .Should().Contain("Usuário autenticado com sucesso");

            #endregion
        }

        [Fact]
        public void Acesso_Negado_Test()
        {
            #region Autenticando um usuário na API

            var request = new AutenticarUsuarioRequestDTO
            {
                Email = "teste@teste.com",
                Senha = "@Teste4321"
            };

            //serializando os dados em JSON
            var json = new StringContent(JsonConvert.SerializeObject(request),
                Encoding.UTF8, "application/json");

            #endregion

            #region Enviando a requisição para a API

            var client = new WebApplicationFactory<Program>().CreateClient();
            var result = client.PostAsync("/api/usuario/autenticar", json).Result;

            #endregion

            #region Verificar a resposta

            result.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
            result.Content.ReadAsStringAsync().Result
                .Should().Contain("Acesso negado. Usuário não encontrado.");

            #endregion
        }
    }
}



