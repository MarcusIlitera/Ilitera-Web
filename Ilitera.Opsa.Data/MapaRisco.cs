using System;
using System.Collections.Generic;
using System.Text;
using Ilitera.Data;
using Ilitera.Common;
using System.IO;

namespace Ilitera.Opsa.Data
{
    [Database("opsa", "MapaRisco", "IdMapaRisco")]
    public class MapaRisco : Ilitera.Opsa.Data.Documento
    {
        private int _IdMapaRisco;
        private LaudoTecnico _IdLaudoTecnico;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public MapaRisco()
        {

        }
        public override int Id
        {
            get { return _IdMapaRisco; }
            set { _IdMapaRisco = value; }
        }
        [Obrigatorio(true, "Laudo Técnico é campo obrigatório")]
        public LaudoTecnico IdLaudoTecnico
        {
            get { return _IdLaudoTecnico; }
            set { _IdLaudoTecnico = value; }
        }

        public string GetDiretorioPadrao()
        {
            string ret;

            if (this.Id !=0 && this.mirrorOld == null)
                this.Find();

           ret = Path.Combine(Fotos.GetRaizPath(), 
                    Path.Combine(Path.Combine(this.IdCliente.GetFotoDiretorioPadrao(), "MapaRisco"), this.DataLevantamento.Year.ToString()));

           return ret;
        }

        public override void Validate()
        {
            this.IdDocumentoBase.Id = (int)Documentos.MapaRisco;
            
            if (this.Id == 0 && this.IdPedido.Id == 0)
                throw new Exception("Pedido é campo obrigatório!");

            base.Validate();
        }

        public static MapaRisco GetMapaRisco(Pedido pedido)
        {
            MapaRisco mapaRisco = new MapaRisco();
            mapaRisco.Find("IdPedido=" + pedido.Id);

            if (mapaRisco.Id == 0)
            {
                LaudoTecnico laudoTecnico = LaudoTecnico.GetUltimoLaudo(pedido.IdCliente.Id, false);

                mapaRisco.Inicialize();
                mapaRisco.IdCliente = pedido.IdCliente;
                mapaRisco.IdLaudoTecnico = laudoTecnico;
                mapaRisco.IdDocumentoBase.Id = (int)Documentos.MapaRisco;
                mapaRisco.IdPedido = pedido;
                mapaRisco.IdPrestador.Id = pedido.IdPrestador.Id;
                mapaRisco.DataLevantamento = laudoTecnico.hDT_LAUDO;
            }

            return mapaRisco;
        }
    }

    [Database("opsa", "PlantaMapaRisco", "IdPlantaMapaRisco")]
    public class PlantaMapaRisco : Ilitera.Data.Table
    {
        private int _IdPlantaMapaRisco;
        private MapaRisco _IdMapaRisco;
        private string _Descricao = string.Empty;
        private string _Arquivo = string.Empty;


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public PlantaMapaRisco()
        {
        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public PlantaMapaRisco(int Id)
        {
            this.Find(Id);
        }
        public override int Id
        {
            get { return _IdPlantaMapaRisco; }
            set { _IdPlantaMapaRisco = value; }
        }
        public MapaRisco IdMapaRisco
        {
            get { return _IdMapaRisco; }
            set { _IdMapaRisco = value; }
        }
        public string Descricao
        {
            get { return _Descricao; }
            set { _Descricao = value; }
        }
        public string Arquivo
        {
            get { return _Arquivo; }
            set { _Arquivo = value; }
        }

        public string GetArquivo()
        {
            return System.IO.Path.Combine(this.IdMapaRisco.GetDiretorioPadrao(), this.Arquivo);

        }
    }
}
