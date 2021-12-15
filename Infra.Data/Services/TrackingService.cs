using Dominio.Entidade.Configuracoes;
using Dominio.Entidade.Pedido;
using Dominio.Entidade.StatusPedido;
using Dominio.Entidade.Tracking;
using Dominio.Interface.Servico.Nf_e;
using Dominio.Interface.Servico.Pedido;
using Dominio.Interface.Servico.Status;
using Dominio.Interface.Servico.Tracking;
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
    public class TrackingService : ITrackingService
    {
        private readonly INotaFiscalService _notaFiscalService;
        private readonly IStatuService _statuService;
        private readonly ILoginService _loginService;
        private readonly StatusConfig _statusConfig;
        public TrackingService(INotaFiscalService notaFiscalService, IStatuService statuService, ILoginService loginService,
            IOptions<StatusConfig> statusConfig)
        {
            _notaFiscalService = notaFiscalService;
            _statuService = statuService;
            _loginService = loginService;
            _statusConfig = statusConfig.Value;
        }

        public string AdicionaStatus(StatusPedidoCS statusPedidoCS)
        {
            {
                if (statusPedidoCS.Status.ToLower() == _statusConfig.StatusNota)
                {
                    _notaFiscalService.BuscaXml(statusPedidoCS.CodReferencia, statusPedidoCS.IdPedido);
                }

                else if(statusPedidoCS.Status.ToLower() == _statusConfig.StatusTracking)
                {
                    //Pegar o dados de tracking e mandar para a Hub
                    BuscaTracking(statusPedidoCS);
                }

                else
                {
                    _statuService.AdicionaStatusDiferentes(statusPedidoCS);
                }

                return null;
            }
        }

        public async Task<string> BuscaTracking(StatusPedidoCS statusPedidoCS)
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

                    statusPedidoCS.CodReferencia = statusPedidoCS.CodReferencia.Replace("HB-", "");

                    //var postUrl = _configOms.BaseUrl + _configOms.OrderUrl;
                    //COLOCAR URL DO OMS
                    var postUrl = $"https://api.cacaudigital.xyz:8443/cacaushow/oms/v1/orders/{statusPedidoCS.IdPedido}?format=1";
                    var requestContent = new StringContent(string.Empty, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.GetAsync(postUrl);

                    response.EnsureSuccessStatusCode();
                    string conteudo =
                        await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                    var settings = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                    };

                    var pedido = JsonSerializer.Deserialize<PedidoCS>(conteudo, settings);

                    //if(conteudo != null)
                    //{
                    //    var tracking = new TrackingCS()
                    //    {
                    //        Code = pedido.tracking.number,
                    //        Url = pedido.tracking.url,
                    //        ShippingDate = pedido.tracking.dateSend,
                    //        ShippingProvider = pedido.tracking.client,
                    //        ShippingService = pedido.tracking.client,
                    //    }
                    //}

                    var tracking = new TrackingCS()
                    {
                        Code = "BR1223123", //NUMERO DO RASTREIO
                        Url = "teste.com.br", //LINK PARA RASTREAR O PEDIDO
                        ShippingDate = "2021-12-14 00:00:00", //DATA DO ENVIO 
                        ShippingProvider = "Correios", //TRANSPORTADORA
                        ShippingService = "Sedex" //MODALIDADE DO ENVIO
                    };

                    

                    EnviaTracking(tracking, statusPedidoCS.CodReferencia);

                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return null;
        }

        public async Task<string> EnviaTracking(TrackingCS trackingCS, string codReferencia)
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

                    codReferencia = codReferencia.Replace("HB-", "");

                    //var postUrl = _configOms.BaseUrl + _configOms.OrderUrl;
                    var postUrl = $"https://rest.hub2b.com.br/Orders/{codReferencia}/Tracking";
                    var requestContent = new StringContent(JsonSerializer.Serialize(trackingCS), Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(postUrl, requestContent);

                    response.EnsureSuccessStatusCode();
                    string conteudo =
                        await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                    var settings = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                    };

                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return null;
        }
    }
}
