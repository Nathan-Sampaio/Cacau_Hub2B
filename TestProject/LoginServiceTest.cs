using Dominio.Entidade.Autenticacao;
using Dominio.Entidade.Configuracoes;
using Infra.Data.Services;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Repositorio;

namespace TestProject
{
    [TestClass]
    public class LoginServiceTest
    {
        private readonly LoginService _loginService;
        private readonly HubConfig config;
        private readonly RedisConfig configRedis;

        public LoginServiceTest()
        {
            config = new HubConfig() {
                BaseURL = "https://rest.hub2b.com.br/",
                LoginURL = "oauth2/login",
                RedisTokenKey="AccessTokenHub",
                Autenticacao = new OAuthRequest()
                {
                    client_id = "UwSjZbV99eZN9sVXkvaIsow20AIciQ",
                    client_secret = "hKDpu93dvUTJMqO83IHk9nV9vSLtFJ",
                    userName = "testes@2032g",
                    password = "PXwInKodUAXhM8kcurQw",
                    grant_type = "password",
                    scope = "inventory orders catalog",
                }
            };

            configRedis = new RedisConfig()
            {
                ConnectionString = "localhost:6379"
                //"localhost:13919,password=senhadoredis"
            };

            var redis = Options.Create(configRedis);
            var hub = Options.Create(config);

            _loginService = new LoginService(hub, new RedisRepositorio(redis));
        }

        [TestMethod]
        public void Is_TokenHub_NotEmpty()
        {
            var token = _loginService.RecuperarTokenAcessoHub().Result;

            Assert.IsNotNull(token);
        }
    }
}
