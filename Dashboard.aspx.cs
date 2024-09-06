using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ilitera.Net
{
    public partial class Dashboard : System.Web.UI.Page
    {

        //se tivesse como salvar esssa página do PowerBI como PDF ou imagem, poderíamos pensar soluções alternativas para a limitação do envio de parâmetros.
        // uma tabela com IdCliente ( do Usuario ) e data/hora da solicitação.  Assim o SQL do dashboard verificaria esse registro e geraria dados
        // apenas relacionados ao cliente do usuario.   Caso um novo usuário clicasse, depois de um tempo mínimo em relação a data/hora do último, geraria
        // um novo registro nessa tabela.  Isso talvez evitasse conflito.
        // problema seria na segmentação ou no uso depois que novo cliente clicou, isso poderia fazer um usuário ver dados de outro cliente.

        //dar uma olhada link abaixo:
        // https://learn.microsoft.com/en-us/power-bi/connect-data/desktop-dynamic-m-query-parameters
        // https://stackoverflow.com/questions/59171921/how-to-pass-parameter-from-asp-net-application-to-power-bi
        // https://learn.microsoft.com/en-us/power-bi/collaborate-share/service-url-filters
        // filtro após URL, poderia usar IdCliente se funcionasse
        // URL do PowerBI?filter=Ano_Exame eq 2020

        // tentando passar parâmetro em relatório com parãmetro
        // https://app.powerbi.com/reportEmbed?reportId=6691f156-ce96-4b90-801d-d4d6cc249344&autoAuth=true&ctid=7b66d68d-edd9-435d-9768-1078d5178543?rp:DB=Opsa_Daiti





        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Redirect(@"https://app.powerbi.com/view?r=eyJrIjoiOWQ4ZTNhYzUtMTJhMi00ZTg2LWI3NTYtMjFiMzA5NWJlNDkxIiwidCI6IjdiNjZkNjhkLWVkZDktNDM1ZC05NzY4LTEwNzhkNTE3ODU0MyJ9");
        }
    }
}