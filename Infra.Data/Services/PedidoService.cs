using Dominio.Entidade;
using Dominio.Entidade.Configuracoes;
using Dominio.Entidade.Pedido;
using Dominio.Interface.Servico.Pedido;
using Microsoft.AspNetCore.WebUtilities;
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
        private readonly ILoginService _loginService;

        public PedidoService(IOptions<HubConfig> configuration, ILoginService loginService)
        {
            this.config = configuration.Value;
            _loginService = loginService;
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
            catch (Exception)
            {
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
            catch (Exception)
            {
                throw;
            }
        }
    }
}
