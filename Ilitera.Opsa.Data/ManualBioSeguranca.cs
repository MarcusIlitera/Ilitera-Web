using System;
using System.Collections;
using Ilitera.Data;
using Ilitera.Common;
using System.Data;
using System.Text;

namespace Ilitera.Opsa.Data
{
	/// <summary>
	///
	/// </summary>
	/// 
	public enum IndPosicaoImage:int
	{
		Superior,
		Inferior
	}
	
	[Database("opsa","AssuntoBase","IdAssuntoBase","","Assunto(Pergunta) de BioSegurança")]
	public class AssuntoBase : Table
	{
		private int _IdAssuntoBase;
		private string _Assunto=string.Empty;
		private string _Pergunta=string.Empty;
		private int _Sequencia;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public AssuntoBase()
		{
		}

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public AssuntoBase(int Id)
		{
			this.Find(Id);
		}
        public override int Id
		{	
			get{return _IdAssuntoBase;}
			set{_IdAssuntoBase = value;}
		}
		public string Assunto
		{	
			get{return _Assunto;}
			set{_Assunto = value;}
		}
		public string Pergunta
		{	
			get{return _Pergunta;}
			set{_Pergunta = value;}
		}
		public int Sequencia
		{
			get{return _Sequencia;}
			set{_Sequencia = value;}
		}
	}
	
	[Database("opsa","TopicoBase","IdTopicoBase","","Tópico(Resposta) de BioSegurança")]
	public class TopicoBase : Table
	{
		private int _IdTopicoBase;
		private AssuntoBase _IdAssuntoBase;
		private string _Topico=string.Empty;
		private string _Resposta=string.Empty;
		private string _TextoHTML=string.Empty;
		private int _Sequencia;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public TopicoBase()
		{
		}

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public TopicoBase(int Id)
		{
			this.Find(Id);
		}
		public override int Id
		{
			get{return _IdTopicoBase;}
			set{_IdTopicoBase = value;}
		}
		public AssuntoBase IdAssuntoBase
		{
			get{return _IdAssuntoBase;}
			set{_IdAssuntoBase = value;}
		}
		public string Topico
		{
			get{return _Topico;}
			set{_Topico = value;}
		}
		public string Resposta
		{
			get{return _Resposta;}
			set{_Resposta = value;}
		}
		public string TextoHTML
		{
			get{return _TextoHTML;}
			set{_TextoHTML = value;}
		}
		public int Sequencia
		{
			get{return _Sequencia;}
			set{_Sequencia = value;}
		}
	}

	[Database("opsa","TopicoBaseImage","IdTopicoBaseImage","","Imagem para Tópico(Resposta) de BioSegurança")]
	public class TopicoBaseImage : Table
	{
		private int _IdTopicoBaseImage;
		private TopicoBase _IdTopicoBase;
		private string _PathImage=string.Empty;
		private short _IndPosicao;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public TopicoBaseImage()
		{
		}

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public TopicoBaseImage(int Id)
		{
			this.Find(Id);
		}
		public override int Id
		{
			get{return _IdTopicoBaseImage;}
			set{_IdTopicoBaseImage = value;}
		}
		public TopicoBase IdTopicoBase
		{
			get{return _IdTopicoBase;}
			set{_IdTopicoBase = value;}
		}
		public string PathImage
		{
			get{return _PathImage;}
			set{_PathImage = value;}
		}
		public short IndPosicao
		{
			get{return _IndPosicao;}
			set{_IndPosicao = value;}
		}
	}

	[Database("opsa","TopicoBioSeguranca","IdTopicoBioSeguranca","","Tópico para Manual de BioSegurança")]
	public class TopicoBioSeguranca : Table
	{
		private int _IdTopicoBioSeguranca;
		private Ghe _IdGhe;
		private AssuntoBase _IdAssuntoBase;
		private TopicoBase _IdTopicoBase;
		private string _TextoHTML=string.Empty;
		private int _Sequencia;
		private string _Topico=string.Empty;
		private bool _IsFromCliente;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public TopicoBioSeguranca()
		{
		}

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public TopicoBioSeguranca(int Id)
		{
			this.Find(Id);
		}
		public override int Id
		{
			get{return _IdTopicoBioSeguranca;}
			set{_IdTopicoBioSeguranca = value;}
		}
		public Ghe IdGhe
		{
			get{return _IdGhe;}
			set{_IdGhe= value;}
		}
		public AssuntoBase IdAssuntoBase
		{
			get{return _IdAssuntoBase;}
			set{_IdAssuntoBase = value;}
		}
		public TopicoBase IdTopicoBase
		{
			get{return _IdTopicoBase;}
			set{_IdTopicoBase = value;}
		}
		public string TextoHTML
		{
			get{return _TextoHTML;}
			set{_TextoHTML = value;}
		}
		public int Sequencia
		{
			get{return _Sequencia;}
			set{_Sequencia = value;}
		}
		public string Topico
		{
			get{return _Topico;}
			set{_Topico = value;}
		}
		public bool IsFromCliente
		{
			get{return _IsFromCliente;}
			set{_IsFromCliente = value;}
		}
	}

	[Database("opsa","TopicoBioSegurancaImage","IdTopicoBioSegurancaImage","","Imagem de Tópico para Manual de BioSegurança")]
	public class TopicoBioSegurancaImage : Table
	{
		private int _IdTopicoBioSegurancaImage;
		private TopicoBioSeguranca _IdTopicoBioSeguranca;
		private string _PathImage=string.Empty;
		private short _IndPosicao;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public TopicoBioSegurancaImage()
		{
		}

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public TopicoBioSegurancaImage(int Id)
		{
			this.Find(Id);
		}
		public override int Id
		{
			get {return _IdTopicoBioSegurancaImage;}
			set {_IdTopicoBioSegurancaImage = value;}
		}
		public TopicoBioSeguranca IdTopicoBioSeguranca
		{
			get {return _IdTopicoBioSeguranca;}
			set {_IdTopicoBioSeguranca = value;}
		}
		public string PathImage
		{
			get {return _PathImage;}
			set	{_PathImage = value;}
		}
		public short IndPosicao
		{
			get {return _IndPosicao;}
			set {_IndPosicao = value;}
		}
	}
}
