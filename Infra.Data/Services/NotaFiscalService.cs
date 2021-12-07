using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Dominio.Entidade.Nf_e;
using Dominio.Interface.Servico.Nf_e;
using Dominio.Interface.Servico.Pedido;

namespace Infra.Data.Services
{
    public class NotaFiscalService : INotaFiscalService
    {
        private readonly ILoginService _loginService;
        public NotaFiscalService(ILoginService loginService)
        {
            _loginService = loginService;
        }
        public async Task<string> BuscaXml(string numeroPedido)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                    //client.DefaultRequestHeaders.Authorization =
                    //   new AuthenticationHeaderValue("Bearer", await _loginService.RecuperarTokenAcessoOms());

                    numeroPedido = numeroPedido.Replace("HB-", "");

                    var entidade = new NotaFiscalRequisicao()
                    {
                        Id = numeroPedido
                    };

                    //var postUrl = _configOms.BaseUrl + _configOms.OrderUrl;
                    var postUrl = "https://api.cacaudigital.xyz:8443/cacaushow/oms/v1/orders";
                    var requestContent = new StringContent(string.Empty, Encoding.UTF8, "application/json");

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
        }

        public Task<string> CadastraNfe()
        {
            throw new NotImplementedException();
        }
    }
}
