using Dominio.Entidade.Autenticacao;
using Dominio.Entidade.Configuracoes;
using Dominio.Entidade.Pedido;
using Dominio.Interface.Servico.Pedido;
using Infra.Data.Services;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repositorio;
using System.Threading.Tasks;

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
                OrderUrl = "cacaushow/oms/v1/orders",
                Autenticacao = new OmsAuthRequest()
                {
                    //clientid = "nathan.system@cacaushow.com.br",
                    //clientsecret = "N@t$$89451340oliv",
                    clientid = "marcos.costa@cacaushow.com.br",
                    clientsecret = "Cshow2022",
                    userName = "6045",
                }
            };

            var redis = Options.Create(configRedis);
            var hub = Options.Create(config);
            var oms = Options.Create(Omsconfig);

            _loginService = new LoginService(hub, new RedisRepositorio(redis), oms);

            _pedidoService = new PedidoService(hub, _loginService, oms);
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

        [TestMethod]
        public async Task OMSEstaRetornandoPedidoAtravesDoCodigoHub()
        {
            var pedido = await _pedidoService.BuscarPedidoPorReferenceIdOMS("HB-811216704");

            Assert.IsNotNull(pedido);
            Assert.IsTrue(pedido != 0);
        }

        [TestMethod]
        public async Task SolicitacaoDeCancelamentoFoiConcluida()
        {
            await _pedidoService.EnviarSolicitacacaoCancelamentoOMS(1967);
        }

        [TestMethod]
        public async Task CancelamentoPedidoRealizadoOMS()
        {
            await _pedidoService.CancelarPedidoOMS(1967);
        }
    }
}