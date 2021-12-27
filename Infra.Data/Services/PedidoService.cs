using Dominio.Entidade;
using Dominio.Entidade.Configuracoes;
using Dominio.Entidade.Pedido;
using Dominio.Interface.Servico.Pedido;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
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
    public class PedidoService : IPedidoServico
    {
        private readonly HubConfig config;
        private readonly OmsConfig _configOms;
        private readonly ILoginService _loginService;
        private readonly ILogger<PedidoService> _logger;


        public PedidoService(IOptions<HubConfig> configuration, ILoginService loginService,
            IOptions<OmsConfig> configOms, ILogger<PedidoService> logger)
        {
            config = configuration.Value;
            _loginService = loginService;
            _configOms = configOms.Value;
            _logger = logger;
        }

        public async Task<PedidosResponse> BuscarPedidosHub(FiltroPedido filtro)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                    client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", await _loginService.RecuperarTokenAcessoHub());

                    var queryString = new Dictionary<string, string>();

                    if (!string.IsNullOrWhiteSpace(filtro.Client))
                        queryString.Add("client", filtro.Client);

                    if (!string.IsNullOrWhiteSpace(filtro.purchaseFrom))
                        queryString.Add("purchaseFrom", filtro.purchaseFrom);

                    if (!string.IsNullOrWhiteSpace(filtro.purchaseTo))
                        queryString.Add("purchaseTo", filtro.purchaseTo);

                    HttpResponseMessage response = await client.GetAsync(
                        QueryHelpers.AddQueryString($"{config.BaseURL}{config.Pedido_BuscarURL}"
                        , queryString)
                    );

                    response.EnsureSuccessStatusCode();
                    string conteudo =
                         await response.Content.ReadAsStringAsync();

                    var settings = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                    };

                    return JsonSerializer.Deserialize<PedidosResponse>(conteudo, settings);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro no método PedidoService.BuscarPedidosHub: " + ex.InnerException);
                throw;
            }
        }

        public async Task<Pedido> BuscarPedidosHubPorOrderId(int orderId)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                    client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", await _loginService.RecuperarTokenAcessoHub());

                    HttpResponseMessage response = await client.GetAsync(
                        $"{config.BaseURL}{config.Pedido_BuscarURL}/{orderId}"
                    );

                    response.EnsureSuccessStatusCode();
                    string conteudo =
                         await response.Content.ReadAsStringAsync();

                    var settings = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                    };

                    return JsonSerializer.Deserialize<Pedido>(conteudo, settings);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro no método PedidoService.BuscarPedidosHubPorOrderId: " + ex.InnerException);
                throw;
            }
        }

        public async Task EnviarPedidoParaOms(PedidoCS pedido)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                    client.DefaultRequestHeaders.Authorization =
                       new AuthenticationHeaderValue("Bearer", await _loginService.RecuperarTokenAcessoOms());

                    var postUrl = _configOms.BaseUrl + _configOms.OrderUrl;
                    var requestContent = new StringContent(JsonSerializer.Serialize(pedido), Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(postUrl, requestContent);

                    response.EnsureSuccessStatusCode();
                    string conteudo =
                        await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                    var settings = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                    };

                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro no método PedidoService.EnviarPedidoParaOms: " + ex.InnerException);

                throw;
            }
        }

        public async Task<int> BuscarPedidoPorReferenceIdOMS(string referenceId)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                    client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", await _loginService.RecuperarTokenAcessoOms());

                    HttpResponseMessage response = await client.GetAsync(
                        $"{_configOms.BaseUrl}{_configOms.OrderUrl}/search?OrderRef={referenceId}&page=1&pageSize=10"
                    );

                    response.EnsureSuccessStatusCode();
                    string conteudo =
                         await response.Content.ReadAsStringAsync();

                    var settings = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                    };

                    if (string.IsNullOrEmpty(conteudo))
                    {
                        return 0;
                    }

                    var pedidosEncontrados = JsonSerializer.Deserialize<List<PedidoCS>>(conteudo, settings);

                    return pedidosEncontrados.FirstOrDefault().id;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task EnviarSolicitacacaoCancelamentoOMS(int IdPedido)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                    client.DefaultRequestHeaders.Authorization =
                       new AuthenticationHeaderValue("Bearer", await _loginService.RecuperarTokenAcessoOms());

                    var patchUrl = _configOms.BaseUrl + _configOms.OrderUrl + $"/{IdPedido}/statuses/cancellationRequested";

                    var requestContent = new StringContent(
                        JsonSerializer.Serialize(new { 
                            Code = IdPedido,
                            Description = "Requisição de cancelamento pedido Hub"
                        }), Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PatchAsync(patchUrl, requestContent);

                    response.EnsureSuccessStatusCode();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task CancelarPedidoOMS(int IdPedido)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                    client.DefaultRequestHeaders.Authorization =
                       new AuthenticationHeaderValue("Bearer", await _loginService.RecuperarTokenAcessoOms());

                    var patchUrl = _configOms.BaseUrl + _configOms.OrderUrl + $"/{IdPedido}/statuses/canceled";

                    HttpResponseMessage response = await client.PatchAsync(patchUrl, null);

                    response.EnsureSuccessStatusCode();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
