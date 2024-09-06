using System;

namespace Ilitera.Common
{
	public class ExceptionTrocaSenha : System.Exception
	{
		public ExceptionTrocaSenha (string message) : base(message)
		{
		}
	}

	public class ExceptionFirstTimeLogin: System.Exception
	{
		public ExceptionFirstTimeLogin(string message): base(message)
		{
		}
	}

	public class ExceptionLogin: System.Exception
	{
		public ExceptionLogin(string message): base(message)
		{
		}
	}

	public class ExceptionSenhaExpirada: System.Exception
	{
        public ExceptionSenhaExpirada(string message) : base(message)
		{
		}
	}

    public class ExceptionSenhaInvalida : System.Exception
    {
        public ExceptionSenhaInvalida(string message)
            : base(message)
        {
        }
    }

	public class ExceptionEmailExistente: System.Exception
	{
		public ExceptionEmailExistente(string message): base(message)
		{
		}
	}

	public class ExceptionEmpregadoExistente: System.Exception
	{
		public ExceptionEmpregadoExistente(string message): base(message)
		{
		}
	}
}
