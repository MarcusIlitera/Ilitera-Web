using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml;

using Comunicacao_Ilitera.Models;
using Comunicacao_Ilitera.Providers;
using Comunicacao_Ilitera.Results;


namespace Comunicacao_Ilitera.Controllers
{
    [Authorize]
    [RoutePrefix("api/IliController")]
    public class IliController : ApiController
    {
        
        //[HttpPost]
        //[HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]

        [Route("ReturnXmlDocument")]
        public string ReturnXmlDocument(HttpRequestMessage request)
        {
            var doc = new XmlDocument();
            doc.Load(request.Content.ReadAsStreamAsync().Result);
                        

            return doc.DocumentElement.OuterXml;
        }

        //chamada
        //var client = new HttpClient();
        //var album = new Album() { AlbumName = "PowerAge", Artist = "AC/DC" };
        //var result = client.PostAsync<Album>("http://localhost/AspNetWebApi/albums/rpc/ReturnXmlDocument",
        //                                        album, new XmlMediaTypeFormatter())
        //                    .Result;


        [Route("Teste")]
        [AcceptVerbs("GET", "POST")]
        [AllowAnonymous]
        public string Teste(string xValor)
        {
            return "Teste " + xValor;
        }

        //exemplo chamada
        //   http://localhost:52513/Api/IliController/Teste?xValor=222


        [Route("AcessarPagina")]
        [AcceptVerbs("GET", "POST")]
        [AllowAnonymous]
        public string AcessarPagina( string xValor )
        {
            // var response = new HttpResponseMessage(HttpStatusCode.Redirect);
            //response.Headers.Location = new Uri("https://www.ilitera.net.br/essence");

            var client = new WebClient();
            var content = client.DownloadString("https://www.ilitera.net.br/essence");
            //carrega a página.  Será que está executando código aspx.cs ?  Pensar em teste
            //se não carregar página,  vou precisar colocar todo o código aqui


            return content.ToString() ;
        }


    }
}
