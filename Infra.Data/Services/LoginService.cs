using Dominio.Entidade.Autenticacao;
using Dominio.Entidade.Configuracoes;
using Dominio.Interface.Repositorios;
using Dominio.Interface.Servico.Pedido;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infra.Data.Services
{
    public class LoginService : ILoginService
    {
        private readonly HubConfig config;
        private readonly IRedisRepositorio _redis;

        public LoginService(IOptions<HubConfig> configuration, IRedisRepositorio redis)
        {
            this.config = configuration.Value;
            this._redis = redis;
        }

        public async Task<string> RecuperarTokenAcessoHub()
        {
            try
            {
                var tokenAcesso = await _redis.RecuperarChave(config.RedisTokenKey);

                if (!String.IsNullOrEmpty(tokenAcesso))
                    return tokenAcesso;

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.PostAsync(
                        config.BaseURL + config.LoginURL,
                        new StringContent(JsonSerializer.Serialize(config.Autenticacao), Encoding.UTF8, "application/json"));

                    response.EnsureSuccessStatusCode();
                    string conteudo =
                        response.Content.ReadAsStringAsync().Result;

                    OAuthToken resultado = JsonSerializer.Deserialize<OAuthToken>(conteudo);

                    await _redis.GravarChave(config.RedisTokenKey, resultado.access_token, resultado.expires_in);

                    return resultado.access_token;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<string> RecuperarTokenAcessoOms()
        {
            try
            {
                var tokenAcesso = await _redis.RecuperarChave(config.RedisTokenKey);

                if (!String.IsNullOrEmpty(tokenAcesso))
                    return tokenAcesso;

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.PostAsync(
                        config.BaseURL + config.LoginURL,
                        new StringContent(JsonSerializer.Serialize(config.Autenticacao), Encoding.UTF8, "application/json"));

                    response.EnsureSuccessStatusCode();
                    string conteudo =
                        response.Content.ReadAsStringAsync().Result;

                    OAuthToken resultado = JsonSerializer.Deserialize<OAuthToken>(conteudo);

                    await _redis.GravarChave(config.RedisTokenKey, resultado.access_token, resultado.expires_in);

                    return resultado.access_token;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
