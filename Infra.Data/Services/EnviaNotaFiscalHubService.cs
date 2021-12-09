using Dominio.Entidade.Nf_e;
using Dominio.Interface.Servico.Nf_e;
using Dominio.Interface.Servico.Pedido;
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
    public class EnviaNotaFiscalHubService : IEnviaNotaFiscalHubService
    {
        private readonly ILoginService _loginService;
        public EnviaNotaFiscalHubService(ILoginService loginService)
        {
            _loginService = loginService;
        }

        public async Task<string> EnviaNotaHub(NotaFiscalCS notaFiscalCS, string numeroPedido)
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

                    //var postUrl = _configOms.BaseUrl + _configOms.OrderUrl;
                    var postUrl = $"https://rest.hub2b.com.br/Orders/{numeroPedido}/Invoice";
                    var requestContent = new StringContent(JsonSerializer.Serialize(notaFiscalCS), Encoding.UTF8, "application/json");

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
                throw new Exception(ex.Message);
            }

            return default;
        }
    }
}
