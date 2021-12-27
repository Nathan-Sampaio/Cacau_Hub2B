using Dominio.Entidade.Configuracoes;
using Dominio.Entidade.Pedido;
using Dominio.Entidade.StatusPedido;
using Dominio.Entidade.Tracking;
using Dominio.Interface.Servico.Nf_e;
using Dominio.Interface.Servico.Pedido;
using Dominio.Interface.Servico.Status;
using Dominio.Interface.Servico.Tracking;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
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
        private readonly OmsConfig _configOms;
        private readonly ILogger<TrackingService> _logger;

        public TrackingService(INotaFiscalService notaFiscalService, IStatuService statuService, ILoginService loginService,
            IOptions<StatusConfig> statusConfig, IOptions<OmsConfig> omsConfig, ILogger<TrackingService> logger)
        {
            _notaFiscalService = notaFiscalService;
            _statuService = statuService;
            _loginService = loginService;
            _statusConfig = statusConfig.Value;
            _configOms = omsConfig.Value;
            _logger = logger;
        }

        public async Task AdicionaStatus(StatusPedidoCS statusPedidoCS)
        {
            try
            {
                _logger.LogInformation($"AdicionaStatus: {JsonSerializer.Serialize(statusPedidoCS)}");

                if (statusPedidoCS.Status.ToLower() == _statusConfig.StatusNota)
                {
                    _logger.LogInformation($"BuscaXml");

                   await _notaFiscalService.BuscaXml(statusPedidoCS.CodReferencia, statusPedidoCS.IdPedido);
                }

                else if (statusPedidoCS.Status.ToLower() == _statusConfig.StatusTracking)
                {
                    _logger.LogInformation($"BuscaTracking");

                    //Pegar o dados de tracking e mandar para a Hub
                    await BuscaTracking(statusPedidoCS);
                }

                else
                {
                    _logger.LogInformation($"AdicionaStatusDiferentes");

                    await _statuService.AdicionaStatusDiferentes(statusPedidoCS);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocorreu um erro no TrackingService.AdicionaStatus:  {ex.Message}");
                throw;
            }
        }

        public async Task BuscaTracking(StatusPedidoCS statusPedidoCS)
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
                    var postUrl = $"{_configOms.BaseUrl}{_configOms.OrderUrl}/{statusPedidoCS.IdPedido}";
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

                    if (conteudo != null)
                    {
                        var tracking = new TrackingCS()
                        {
                            Code = pedido.Tracking.Number,
                            Url = pedido.Tracking.Url,
                            ShippingDate = pedido.Tracking.DateSend,
                            ShippingProvider = pedido.Tracking.Client,
                            ShippingService = pedido.Tracking.Client,
                        };

                        await EnviaTracking(tracking, statusPedidoCS.CodReferencia);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocorreu um erro no TrackingService.BuscaTracking:  {ex.Message}");

                throw new Exception(ex.Message);
            }
        }

        public async Task EnviaTracking(TrackingCS trackingCS, string codReferencia)
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
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocorreu um erro no TrackingService.EnviaTracking:  {ex.Message}");

                throw new Exception(ex.Message);
            }
        }
    }
}
