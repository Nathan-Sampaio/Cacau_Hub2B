using Dominio.Entidade.StatusPedido;
using Dominio.Interface.Servico.Pedido;
using Dominio.Interface.Servico.Status;
using Newtonsoft.Json;
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
    public class StatusService : IStatuService
    {
        private readonly ILoginService _loginService;
        public StatusService(ILoginService loginService)
        {
            _loginService = loginService;
        }
        public async Task<string> AdicionaStatusDiferentes(StatusPedidoCS status)
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

                    var statusDiferente = new StatusDiferente()
                    {
                        Status = status.Status,
                        UpdatedDate = DateTime.Now,
                        Active = true,
                        Message = status.Status
                    };

                    status.CodReferencia = status.CodReferencia.Replace("HB-", "");

                    //var postUrl = _configOms.BaseUrl + _configOms.OrderUrl;
                    var postUrl = $"https://rest.hub2b.com.br/Orders/{status.CodReferencia}/Status";
                    var requestContent = new StringContent(System.Text.Json.JsonSerializer.Serialize(status), Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PutAsync(postUrl, requestContent);

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
