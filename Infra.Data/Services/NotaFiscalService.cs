using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;
using Dominio.Entidade.Nf_e;
using Dominio.Interface.Servico.Nf_e;
using Dominio.Interface.Servico.Pedido;
using Newtonsoft.Json;

namespace Infra.Data.Services
{
    public class NotaFiscalService : INotaFiscalService
    {
        private readonly ILoginService _loginService;
        private readonly IEnviaNotaFiscalHubService _enviaNotaFiscalHubService;
        public NotaFiscalService(ILoginService loginService,
            IEnviaNotaFiscalHubService enviaNotaFiscalHubService)
        {
            _loginService = loginService;
            _enviaNotaFiscalHubService = enviaNotaFiscalHubService;
        }
        public async Task<string> BuscaXml(string numeroPedido, string idPedido)
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

                    //var postUrl = _configOms.BaseUrl + _configOms.OrderUrl;
                    var postUrl = $"https://api.cacaudigital.xyz:8443/cacaushow/oms/v1/orders/{idPedido}/invoice/xml";
                    var requestContent = new StringContent(string.Empty, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.GetAsync(postUrl);

                    response.EnsureSuccessStatusCode();
                    string conteudo =
                        await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                    var xml = JsonConvert.DeserializeObject<XmlNotaFiscal>(conteudo);

                    var settings = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                    };

                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(xml.Xml);

                    var key = xmlDoc.GetElementsByTagName("infNFe")[0].OuterXml;
                    var number = xmlDoc.GetElementsByTagName("nNF")[0].InnerText;
                    var cfop = xmlDoc.GetElementsByTagName("CFOP")[0].InnerText;
                    var serie = xmlDoc.GetElementsByTagName("serie")[0].InnerText;
                    var totalAmount = xmlDoc.GetElementsByTagName("vOrig")[0].InnerText;
                    var issueDate = xmlDoc.GetElementsByTagName("dhEmi")[0].InnerText;

                    key = key.Substring(29, 44);
                    
                    var nota = new NotaFiscalCS()
                    {
                        Xml = xml.Xml,
                        Key = key,
                        Number = number,
                        Cfop = cfop,
                        Series = serie,
                        TotalAmount = totalAmount,
                        IssueDate = issueDate,
                        XmlReference = xml.Xml,
                        Packages = "1"
                    };

                    var enviaNota = _enviaNotaFiscalHubService.EnviaNotaHub(nota, numeroPedido);

                    return xml.Xml;
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
