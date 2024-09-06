using System;
using System.Collections;
using System.Text;
using System.Data;

using Ilitera.Data;
using Ilitera.Common;

namespace Ilitera.Opsa.Data
{
	[Database("opsa","Responsavel","IdResponsavel")]
	public class Responsavel : Ilitera.Data.Table 
	{
		private int _IdResponsavel;
		private Prestador _IdPrestador;
		private int _IndResponsavelPapel;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Responsavel()
		{

		}
		public override int Id
		{
			get{return _IdResponsavel;}
			set{_IdResponsavel = value;}
		}
		public Prestador IdPrestador
		{
			get{return _IdPrestador;}
			set{_IdPrestador = value;}
		}
		public int IndResponsavelPapel
		{
			get{return _IndResponsavelPapel;}
			set{_IndResponsavelPapel = value;}
		}

        public static string GetNome(ResponsavelPapel papel)
        {
            string ret;

            if (ResponsavelPapel.AcessoAuditoria == papel)
                ret = "Auditoria";
            else if (ResponsavelPapel.Boleto == papel)
                ret = "Recebe o Boleto";
            else if (ResponsavelPapel.CIPA == papel)
                ret = "CIPA";
            else if (ResponsavelPapel.Contrato == papel)
                ret = "Contrato";
            else if (ResponsavelPapel.ASOPCIBranco == papel)
                ret = "Pode imprimir Aso e PCI em branco";
            else if (ResponsavelPapel.ContatoPrincipal == papel)
                ret = "Contato Principal";
            else if (ResponsavelPapel.Diretoria == papel)
                ret = "Diretoria";
            else if (ResponsavelPapel.NaoRecebeAvisos == papel)
                ret = "Não Recebe Avisos";
            else
                ret = papel.ToString();

            return ret;
        }
	}
}
