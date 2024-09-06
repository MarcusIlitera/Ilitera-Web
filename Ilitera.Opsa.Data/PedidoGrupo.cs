// Mestra
using System;
using System.Data;
using System.Text;
using System.Collections;
using Ilitera.Data;
using Ilitera.Common;

namespace Ilitera.Opsa.Data
{
	[Database("opsa","PedidoGrupo","IdPedidoGrupo")]
	public class PedidoGrupo : Ilitera.Data.Table 
	{
		private int _IdPedidoGrupo;
		private Cliente _IdCliente;
		private Prestador _IdPrestador;
		private int _Numero;
		private DateTime _DataSolicitacao = DateTime.Today;
		private DateTime _DataImpressao = DateTime.Today;
		private string _Solicitante = string.Empty;
//		private string _Contato = string.Empty;
//		private Prestador _IdAgendador;
//		private Prestador _IdAutorizador;
		private string _Observacao = string.Empty;
		private Compromisso _IdCompromisso;
		private int _NumeroPDT;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public PedidoGrupo()
		{

		}

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public PedidoGrupo(Pedido pedido)
		{
			if(pedido.IdPedidoGrupo.Id==0)
			{
				this.Inicialize();
				this.IdCliente = pedido.IdCliente;
				this.IdPrestador = pedido.IdPrestador;
				if(pedido.DataSolicitacao == new DateTime())
					this.DataSolicitacao =  DateTime.Today;
				else
					this.DataSolicitacao = pedido.DataSolicitacao;
				this.Numero = PedidoGrupo.GetNumeroPedido();
			}
			else
				this.Find(pedido.IdPedidoGrupo.Id);
			if(pedido.DataConclusao==new DateTime())
				pedido.DataConclusao = new DateTime(1753, 1, 1);
		}

		public override int Id
		{
			get{return _IdPedidoGrupo;}
			set{_IdPedidoGrupo = value;}
		}
		[Obrigatorio(true, "Número é obrigatório!")]
		public int Numero
		{
			get{return _Numero;}
			set{_Numero = value;}
		}
		[Obrigatorio(true, "Cliente é obrigatório!")]
		public Cliente IdCliente
		{
			get{return _IdCliente;}
			set{_IdCliente = value;}
		}
		[Obrigatorio(true, "Prestador é campo obrigatório!")]
		public Prestador IdPrestador
		{
			get{return _IdPrestador;}
			set{_IdPrestador = value;}
		}
		[Obrigatorio(true, "Data de Solicitação é obrigatório!")]
		public DateTime DataSolicitacao
		{
			get{return _DataSolicitacao;}
			set{_DataSolicitacao = value;}
		}

		public DateTime DataImpressao
		{
			get{return _DataImpressao;}
			set{_DataImpressao = value;}
		}
		public string Solicitante
		{
			get{return _Solicitante;}
			set{_Solicitante = value;}
		}
//		public string Contato
//		{
//			get{return _Contato;}
//			set{_Contato = value;}
//		}

//		public Prestador IdAgendador
//		{
//			get{return _IdAgendador;}
//			set{_IdAgendador = value;}
//		}
//
//		public Prestador IdAutorizador
//		{
//			get{return _IdAutorizador;}
//			set{_IdAutorizador = value;}
//		}

		public string Observacao
		{
			get{return _Observacao;}
			set{_Observacao = value;}
		}

		public Compromisso IdCompromisso
		{
			get{return _IdCompromisso;}
			set{_IdCompromisso = value;}
		}

		public int NumeroPDT
		{
			get{return _NumeroPDT;}
			set{_NumeroPDT = value;}
		}

        public string GetObrigacoes()
        {
            string where = "IdObrigacao IN (SELECT IdObrigacao FROM Pedido WHERE"
                            +" IdPedidoGrupo=" + this.Id + ")"
                            + " ORDER BY Nome";

            DataSet ds = new Obrigacao().Get(where);

            System.Text.StringBuilder str = new System.Text.StringBuilder();

            str.Append("Aviso de Agendamento\n");

            foreach (DataRow row in ds.Tables[0].Rows)
                str.Append(Convert.ToString(row["Nome"]) + ", ");

            if (str.ToString() != string.Empty)
                return str.ToString().Remove(str.ToString().Length - 2, 2);
            else
                return string.Empty;
        }

        public string GetNomeObrigacoes(string quebraLinha)
        {
            ArrayList listPedidos = new Pedido().Find("IdPedidoGrupo=" + this.Id);

            listPedidos.Sort();

            StringBuilder sb = new StringBuilder();

            foreach (Pedido pedido in listPedidos)
                sb.Append(pedido.ToString() + quebraLinha);

            return sb.ToString();
        }

        public string GetDataAtividades(string quebraLinha)
        {
            StringBuilder sb = new StringBuilder();

            if (this.IdCompromisso.mirrorOld == null)
                this.IdCompromisso.Find();

            ArrayList list = new ArrayList();

            if (this.IdCompromisso.IdRepetir.Id != 0)
                list = new Compromisso().Find("IdRepetir=" + this.IdCompromisso.IdRepetir.Id + " ORDER BY DataInicio");
            else
                list.Add(this.IdCompromisso);

            for (int i = 0; i < list.Count; i++)
                sb.Append(((Compromisso)list[i]).GetDataComprimisso() + quebraLinha);

            return sb.ToString();
        }

        public override int Save()
        {
            if (this.IdCompromisso.Id != 0)
            {
                if (this.IdPrestador.mirrorOld == null)
                    this.IdPrestador.Find();

                if (this.IdCompromisso.mirrorOld == null)
                    this.IdCompromisso.Find();

                if (this.IdCompromisso.IdPessoa.Id != this.IdPrestador.IdPessoa.Id)
                {
                    if (this.IdCompromisso.IdRepetir.Id == 0)
                    {
                        this.IdCompromisso.IdPessoa = this.IdPrestador.IdPessoa;
                        this.IdCompromisso.Save();
                    }
                    else
                    {
                        ArrayList listCompromisso = new Compromisso().Find("IdRepetir=" + this.IdCompromisso.IdRepetir.Id);

                        foreach (Compromisso compromisso in listCompromisso)
                        {
                            compromisso.IdPessoa = this.IdPrestador.IdPessoa;
                            compromisso.Save();
                        }
                    }
                }
            }
            return base.Save();
        }

		public override string ToString()
		{
			return this._Numero.ToString("000.000");
		}

        public static int GetNumeroPedido()
        {
            PedidoGrupo pedidoGrupo = new PedidoGrupo();
            pedidoGrupo.FindMax("Numero", "Numero IS NOT NULL");
            return ++pedidoGrupo.Numero;
        }

		public void ApagarCompromisso()
		{
			Compromisso compromisso = new Compromisso();
			compromisso.Find(this.IdCompromisso.Id);

			this.ApagarCompromisso(compromisso);
		}

        public void ApagarCompromisso(Compromisso compromisso)
        {
            if (this.IdCompromisso.Id == compromisso.Id)
            {
                compromisso.Validate();

                this.IdCompromisso = new Compromisso();

                if (this.Id != 0)
                    this.Save();

                if (compromisso.IdRepetir.Id != 0)
                {
                    ArrayList listCompromisso = new Compromisso().Find("IdRepetir=" + compromisso.IdRepetir.Id
                        + " AND IdCompromisso<>" + compromisso.Id);

                    if (listCompromisso.Count > 0)
                    {
                        new Repetir().Delete("IdRepetir=" + compromisso.IdRepetir.Id);
                        return;
                    }
                }
                compromisso.Delete();
            }
            else
                compromisso.Delete();
        }

		public string GetPrestadoresAgendadosPedido()
		{
			System.Text.StringBuilder str = new System.Text.StringBuilder();

			ArrayList list;

			this.IdCompromisso.Find();
		
			if(this.IdCompromisso.IdRepetir.Id!=0)
				list = new Pessoa().Find("IdPessoa IN (SELECT IdPessoa FROM Compromisso WHERE IdRepetir="+this.IdCompromisso.IdRepetir.Id+")");
			else
				list = new Pessoa().Find("IdPessoa IN (SELECT IdPessoa FROM Compromisso WHERE IdCompromisso="+this.IdCompromisso.Id+")");

			foreach(Pessoa pessoa in list)
				str.Append(pessoa.NomeAbreviado + "\r\n");

			return str.ToString();
		}
	}
}
