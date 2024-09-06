using System;
using System.Data;
using System.Collections;
using System.Text;
using Ilitera.Data;
using Ilitera.Common;

namespace Ilitera.Opsa.Data
{	
	public enum TipoConta: int
	{
		Default,
		Caixa,
		Bancos,
		Imobilizado,
		ContasReceber,
		ContasPagar
	}

	public enum NivelConta: int
	{
		N1=1,
		N2=2,
		N3=3,
		N4=4,
		N5=5
	}

	public enum GrupoConta: int
	{
		Ativo=1,
		Passivo=2,
		Despesa=3,
		Receita=4
	}

	[Database("opsa","Conta","IdConta")]
	public class Conta: Ilitera.Data.Table 
	{
		private int _IdConta;
		private string _Numero = string.Empty;
		private string _Descricao = string.Empty;
		private int _IndNivelConta;
		private int _IndTipoConta;
		private int _IndGrupoConta;
		private bool _IsPositiva;


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Conta()
		{

		}
		public override int Id
		{
			get{return _IdConta;}
			set{_IdConta = value;}
		}
		public string Numero
		{
			get{return _Numero;}
			set{_Numero = value;}
		}
		public string Descricao
		{
			get{return _Descricao;}
			set{_Descricao = value;}
		}
		public int IndNivelConta
		{
			get{return _IndNivelConta;}
			set{_IndNivelConta = value;}
		}
		public int IndTipoConta
		{
			get{return _IndTipoConta;}
			set{_IndTipoConta = value;}
		}
		public int IndGrupoConta
		{
			get{return _IndGrupoConta;}
			set{_IndGrupoConta = value;}
		}
		public bool IsPositiva
		{
			get{return _IsPositiva;}
			set{_IsPositiva = value;}
		}
		[Persist(false)]
		public int Nivel1
		{
			get
			{
				return Int32.Parse(this.Numero.Substring(0, 1));
			}
			set
			{
				string val = value.ToString("0") 
							+ this.Numero.Substring(1, 6);

				this.Numero = val;
			}
		}
		[Persist(false)]
		public int Nivel2
		{
			get
			{
				return Int32.Parse(this.Numero.Substring(1, 1));
			}
			set
			{
				string val = this.Numero.Substring(0, 1)
					+ value.ToString() 
					+ this.Numero.Substring(2, 5);

				this.Numero = val;
			}
		}
		[Persist(false)]
		public int Nivel3
		{
			get
			{
				return Int32.Parse(this.Numero.Substring(2, 1));
			}
			set
			{
				string val = this.Numero.Substring(0, 2)
					+ value.ToString() 
					+ this.Numero.Substring(3, 4);

				this.Numero = val;
			}
		}
		[Persist(false)]
		public int Nivel4
		{
			get
			{
				return Int32.Parse(this.Numero.Substring(3, 2));
			}
			set
			{
				string val = this.Numero.Substring(0, 3)
					+ value.ToString("00") 
					+ this.Numero.Substring(5, 2);

				this.Numero = val;
			}
		}
		[Persist(false)]
		public int Nivel5
		{
			get
			{
				return Int32.Parse(this.Numero.Substring(5, 2));
			}
			set
			{
				string val = this.Numero.Substring(0, 5)
					+ value.ToString("00");

				this.Numero = val;
			}
		}

		public string GetNumeroAbreviado()
		{
			string ret;

			if(this.IndNivelConta==(int)NivelConta.N1)
				ret = this.Nivel1.ToString("0");
			else if(this.IndNivelConta==(int)NivelConta.N2)
				ret = this.Nivel2.ToString("0");
			else if(this.IndNivelConta==(int)NivelConta.N3)
				ret = this.Nivel3.ToString("0");
			else if(this.IndNivelConta==(int)NivelConta.N4)
				ret = this.Nivel4.ToString("00");
			else
				ret = this.Nivel5.ToString("00");

			return ret;
		}

		public string GetNumero()
		{
			string ret;

			if(this.IndNivelConta==(int)NivelConta.N1)
				ret = this.Nivel1.ToString("0");
			else if(this.IndNivelConta==(int)NivelConta.N2)
				ret = this.Nivel1.ToString("0") + "." 
					+ this.Nivel2.ToString("0");
			else if(this.IndNivelConta==(int)NivelConta.N3)
				ret = this.Nivel1.ToString("0") + "." 
					+ this.Nivel2.ToString("0") + "." 
					+ this.Nivel3.ToString("0");
			else if(this.IndNivelConta==(int)NivelConta.N4)
				ret = this.Nivel1.ToString("0") + "." 
					+ this.Nivel2.ToString("0") + "." 
					+ this.Nivel3.ToString("0") + "." 
					+ this.Nivel4.ToString("00");
			else
				ret = this.Nivel1.ToString("0") + "." 
				    + this.Nivel2.ToString("0") + "." 
				    + this.Nivel3.ToString("0") + "." 
				    + this.Nivel4.ToString("00")+ "." 
				    + this.Nivel5.ToString("00");

			return ret;
		}

		public override string ToString()
		{
			return this.GetNumero()+ " - " + this.Descricao;
		}

		public int Save(bool bVal)
		{
			return base.Save ();
		}

		public override void Delete()
		{
			ArrayList list = GetSubContas(this, (NivelConta)Enum.Parse(typeof(NivelConta), this.IndNivelConta.ToString()));

			if(list.Count>0)
				throw new Exception("Esta conta possui " +list.Count.ToString() 
					+" sub-contas, não pode ser excluída!");
			
			base.Delete ();
		}

		public override void Validate()
		{
			VeriricarNivel();
			VerificarGrupoConta();
			VerificarIsPositiva();

			base.Validate ();
		}

		public override int Save()
		{
			int ret = 0;
			
			if((Conta)mirrorOld!=null && this.Id!=0)
			{
				if(((Conta)mirrorOld).Nivel1!=this.Nivel1)
					ret = AtualizarNivel1();
				else if(((Conta)mirrorOld).Nivel2!=this.Nivel2)
					ret = AtualizarNivel2();
				else if(((Conta)mirrorOld).Nivel3!=this.Nivel3)
					ret = AtualizarNivel3();
				else if(((Conta)mirrorOld).Nivel4!=this.Nivel4)
					ret = AtualizarNivel4();
				else
					ret = base.Save ();
			}
			else
				ret = base.Save ();

			return ret;
		}

		private static int GetNextNumber(NivelConta nivelConta)
		{
			int ret = 0;
			return ret;
		}

		private static GrupoConta GetGrupoConta(int N1)
		{
			GrupoConta ret;

			switch (N1)
			{
				case 1:
					ret = GrupoConta.Ativo;
					break;
				case 2:
					ret = GrupoConta.Despesa;
					break;
				case 3:
					ret =GrupoConta.Receita;
					break;
				case 4:
					ret = GrupoConta.Passivo;
					break;
				default:
					ret = 0;
					break;
			}

			return ret;
		}

		public Conta AddConta()
		{
			Conta conta = new Conta();
			conta.Numero = "0000000";
			
			if(this.IndNivelConta==(int)NivelConta.N1)
			{
				conta.IndNivelConta = (int)NivelConta.N2;
				conta.Nivel1 = this.Nivel1;
				conta.Nivel2 = GetNextNumber(NivelConta.N2);
			}
			else if(this.IndNivelConta==(int)NivelConta.N2)
			{
				conta.IndNivelConta = (int)NivelConta.N3;
				conta.Nivel1 = this.Nivel1;
				conta.Nivel2 = this.Nivel2;
				conta.Nivel3 = GetNextNumber(NivelConta.N3);
			}
			else if(this.IndNivelConta==(int)NivelConta.N3)
			{
				conta.IndNivelConta = (int)NivelConta.N4;
				conta.Nivel1 = this.Nivel1;
				conta.Nivel2 = this.Nivel2;
				conta.Nivel3 = this.Nivel3;
				conta.Nivel4 = GetNextNumber(NivelConta.N4);
			}
			else if(this.IndNivelConta==(int)NivelConta.N4)
			{
				conta.IndNivelConta = (int)NivelConta.N5;
				conta.Nivel1 = this.Nivel1;
				conta.Nivel2 = this.Nivel2;
				conta.Nivel3 = this.Nivel3;
				conta.Nivel4 = this.Nivel4;
				conta.Nivel5 = GetNextNumber(NivelConta.N5);
			}
			else if(this.IndNivelConta==(int)NivelConta.N5)
				throw new Exception("Clique em novo no nível acima!");

			conta.IndTipoConta	= this.IndTipoConta;
			conta.IndGrupoConta = (int)GetGrupoConta(this.Nivel1);
			conta.IsPositiva	= ((conta.Nivel1==1)||(conta.Nivel1==2));

			return conta;
		}

		private void VerificarIsPositiva()
		{
			switch (this.Nivel1)
			{
				case 1:
				case 2:
					if(!this.IsPositiva)
						throw new Exception("Está conta não é positiva!");
					break;
				case 3:
				case 4:
					if(this.IsPositiva)
						throw new Exception("Está conta não é negativa!");
					break;
			}
		}

		private void VerificarGrupoConta()
		{
			switch (this.Nivel1)
			{
				case 1:
					if(this.IndGrupoConta!=(int)GrupoConta.Ativo)
						throw new Exception("Grupo Inválido!");
					break;
				case 2:
					if(this.IndGrupoConta!=(int)GrupoConta.Despesa)
						throw new Exception("Grupo Inválido!");
					break;
				case 3:
					if(this.IndGrupoConta!=(int)GrupoConta.Receita)
						throw new Exception("Grupo Inválido!");
					break;
				case 4:
					if(this.IndGrupoConta!=(int)GrupoConta.Passivo)
						throw new Exception("Grupo Inválido!");
					break;
			}
		}

		private void VeriricarNivel()
		{
			int nivel = 0;

			if(this.Numero.Substring(1, 6) == "000000")
				nivel = (int)NivelConta.N1;
			else if(this.Numero.Substring(1, 6) != "000000"
				 && this.Numero.Substring(2, 5) == "00000")
				nivel = (int)NivelConta.N2;
			else if(this.Numero.Substring(1, 6) != "000000"
				 && this.Numero.Substring(2, 5) != "00000"
				 && this.Numero.Substring(3, 4) == "0000")
				nivel = (int)NivelConta.N3;
			else if(this.Numero.Substring(1, 6) != "000000"
				 && this.Numero.Substring(2, 5) != "00000"
				 && this.Numero.Substring(3, 4) != "0000"
				 && this.Numero.Substring(5, 2) == "00")
				nivel = (int)NivelConta.N4;
			else 
				nivel = (int)NivelConta.N5;

			if(nivel != this.IndNivelConta)
				throw new Exception("Nível informado incorreto!");
		}

		private static ArrayList GetSubContas(Conta conta, NivelConta nivelConta)
		{
			StringBuilder str = new StringBuilder();

			if(nivelConta==NivelConta.N1)
				str.Append("(LEFT(Numero, 1) = '"+conta.Numero.Substring(0, 1)+"')");
			else if(nivelConta==NivelConta.N2)
				str.Append("(LEFT(Numero, 2) = '"+ conta.Numero.Substring(0, 2)+"')");
			else if(nivelConta==NivelConta.N3)
				str.Append("(LEFT(Numero, 3) = '"+ conta.Numero.Substring(0, 3)+"')");
			else if(nivelConta==NivelConta.N4)
				str.Append("(LEFT(Numero, 5) = '"+ conta.Numero.Substring(0, 5)+"')");
			else 
				str.Append("IdConta =-1");

			str.Append(" AND IdConta <> "+conta.Id);

			return new Conta().Find(str.ToString());
		}

		private int AtualizarNivel1()
		{
			int ret = 0;

			ArrayList list = GetSubContas(((Conta)this.mirrorOld), NivelConta.N1);

			IDbTransaction trans = this.GetTransaction();
					
			try
			{
				ret = base.Save ();

				foreach(Conta conta in list)
				{
					conta.Nivel1 = this.Nivel1;
					conta.Transaction = trans;
					conta.Save(true);
				}

				trans.Commit();

				return ret;
			}
			catch
			{
				trans.Rollback();

				throw new Exception("Nível 01 não pode ser alterado!");
			}
		}

		private int AtualizarNivel2()
		{
			int ret = 0;

			ArrayList list = GetSubContas(((Conta)this.mirrorOld), NivelConta.N2);

			IDbTransaction trans = this.GetTransaction();
					
			try
			{
				ret = base.Save ();

				foreach(Conta conta in list)
				{
					conta.Nivel2 = this.Nivel2;
					conta.Transaction = trans;
					conta.Save(true);
				}

				trans.Commit();

				return ret;
			}
			catch
			{
				trans.Rollback();

				throw new Exception("Nível 02 não pode ser alterado!");
			}
		}

		private int AtualizarNivel3()
		{
			int ret = 0;

			ArrayList list = GetSubContas(((Conta)this.mirrorOld), NivelConta.N3);

			IDbTransaction trans = this.GetTransaction();
					
			try
			{
				ret = base.Save ();

				foreach(Conta conta in list)
				{
					conta.Nivel3 = this.Nivel3;
					conta.Transaction = trans;
					conta.Save(true);
				}

				trans.Commit();

				return ret;
			}
			catch
			{
				trans.Rollback();

				throw new Exception("Nível 03 não pode ser alterado!");
			}
		}

		
		private int AtualizarNivel4()
		{
			int ret = 0;

			ArrayList list = GetSubContas(((Conta)this.mirrorOld), NivelConta.N4);

			IDbTransaction trans = this.GetTransaction();
					
			try
			{
				ret = base.Save();

				foreach(Conta conta in list)
				{
					conta.Nivel4 = this.Nivel4;
					conta.Transaction = trans;
					conta.Save(true);
				}

				trans.Commit();

				return ret;
			}
			catch
			{
				trans.Rollback();

				throw new Exception("Nível 04 não pode ser alterado!");
			}
		}
	}
}
