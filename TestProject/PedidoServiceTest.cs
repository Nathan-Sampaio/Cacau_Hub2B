using Dominio.Entidade.Autenticacao;
using Dominio.Entidade.Configuracoes;
using Dominio.Entidade.Pedido;
using Dominio.Interface.Servico.Pedido;
using Infra.Data.Services;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repositorio;

namespace TestProject
{
    [TestClass]
    public class PedidoServiceTest
    {
        private readonly IPedidoServico _pedidoService;
        private readonly ILoginService _loginService;
        private readonly HubConfig config;
        private readonly RedisConfig configRedis;
        private readonly OmsConfig Omsconfig;
        private FiltroPedido filtroPedido;

        public PedidoServiceTest()
        {
            config = new HubConfig()
            {
                BaseURL = "https://rest.hub2b.com.br/",
                LoginURL = "oauth2/login",
                Pedido_BuscarURL = "Orders",
                RedisTokenKey = "AccessTokenHub",
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

            filtroPedido = new FiltroPedido()
            {
                Client = "416.078.168-30",
            };

            Omsconfig = new OmsConfig()
            {
                RedisTokenKey = "AccessTokenOms",
                BaseUrl = "https://api.cacaudigital.xyz:8443/",
                LoginUrl = "auth/v1/jwt/token/",
                Autenticacao = new OmsAuthRequest()
                {
                    clientid = "UwSjZbV99eZN9sVXkvaIsow20AIciQ",
                    clientsecret = "hKDpu93dvUTJMqO83IHk9nV9vSLtFJ",
                    userName = "testes@2032g",
                }
            };

            var redis = Options.Create(configRedis);
            var hub = Options.Create(config);
            var oms = Options.Create(Omsconfig);

            _loginService = new LoginService(hub, new RedisRepositorio(redis), oms);

            _pedidoService = new PedidoService(hub, _loginService);
        }

        [TestMethod]
        public void Esta_RetornandoPedidos()
        {
            var pedidos = _pedidoService.BuscarPedidosHub(filtroPedido).Result;

            Assert.IsTrue(pedidos.totalObjects > 0);
        }

        [TestMethod]
        public void Esta_ConsultandoPedidoPorId()
        {
            var pedido = _pedidoService.BuscarPedidosHubPorOrderId(807276452).Result;

            Assert.IsNotNull(pedido);
        }
    }
}