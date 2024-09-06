// Hifenizar.cs - compile with: /doc:Hifenizar.xml
using System;
using System.Text;
using System.Collections;

namespace Ilitera.Common
{	/// <summary>
	/// Classe para hifenizar Textos em portugu�s
	/// </summary>
	/// <remarks>Esta classe cont�m apenas duas propriedades p�blicas:
	/// (1) Texto (string) - onde deve ser colocado o Texto a ser Hifenizado;
	/// (2) Separacoes (int[]) - vetor para recuperar as posi��es das hifeniza��es.
	/// Preste aten��o � utiliza��o de StringBuilders e ArrayLists.
	/// Por Andr� Tom�s Velloso (andre@velloso.com) - Revista F�rum Access</remarks>
	// Note nas condi��es da classe que no C# os operadores '&&' e '||' s�o 
	// expertos e s� avaliam o segundo operando quando o primeiro �, 
	// respectivamente, true ou false.
	public sealed class Hifenizar
	{	/// <summary>Membro para guardar Vetor de hifeniza��es.</summary>
		private char[] _TextoaSeparar;
		/// <summary>Membro para guardar Texto a ser hifenizado.</summary>
		private int[]  _PosicoesDeSeparacoes;
		/// <summary>Texto (string) a ser hifenizado</summary>
		public string Texto
		{
				set 
		   {
			   _TextoaSeparar  = (value).ToCharArray();
			   HifenizadorPadrao();    }
			get 
			{
				System.Text.StringBuilder A=new System.Text.StringBuilder();
				A.Append(_TextoaSeparar);	
				return A.ToString();	  }	
		}
		/// <summary>Vetor de posi��es onde � possivel hifenizar o texto.
		/// A �ltima posi��o do vetor � sempre zero para indicar que 
		/// n�o h� mais hifeniza��o poss�vel.
		/// </summary>
		public int[] Separacoes	{get {return _PosicoesDeSeparacoes;}}
		/// <summary>Contrutor que coloca o valor de NewTexto na propriedade Texto</summary>
		/// <param name="NewTexto">Texto (string) a ser hifenizado</param>
		public Hifenizar(string NewTexto) {this.Texto=NewTexto;}	
		/// <summary>Construtor padr�o, chama o outro cosntrutor com uma string vazia</summary>
		public Hifenizar() : this(String.Empty) {}
		/// <summary>M�todo boleano, procura um caractere em um vetor de caracteres</summary>
		/// <param name="C">Caractere a ser procurado no conjunto.</param>
		/// <param name="SetOfChar">Conjunto (char[]) de caracteres.</param>
		/// <returns>Boleano indicando se o caractere pertence ao conjunto</returns>
		private static bool IsInSet(char C, char[] SetOfChar)
		{
				foreach (char member in SetOfChar) { if (C==member) return true;}
			return false; 	}
		/// <summary>Rotina que cont�m toda a logica de Hifeniza��o
		/// de palavras em portugu�s</summary>
		/// <param name="Palavra">
		/// Um array (char[]) contendo a palavra, cada caractere em uma posi��o
		/// do array come�ando pela posi��o 0 e contendo como �ltimo caractere
		/// um espa�o que � a termina��o esperada pelo algoritimo.</param>
		/// <returns>Retorna um vetor de posi��es de hifeniza��o para a palavra</returns>
		private static int[] HifenizarPalavra(char[] Palavra)
		{  // Conjuntos de caracteres para representar os grupos cosonantais 
			// n�o pass�veis de separa��o (Set1 e Set3) ou (Set2 e Set4)
			char[] Set1 = new char[]{'b','B','c','C','d','D','f','F',
										'g','G','p','P','t','T','v','V'}; 
			char[] Set3 = new char[]{'l','L','r','R'};
			char[] Set2 = new char[]{'c','C','l','L','n','N'};
			char[] Set4 = new char[]{'h','H'};
			// Conjunto de caracteres contendo as vogais mais a letra Y.
			char[] SetVogal = new char[]{'a','A','�','�','�','�','�','�','�','�','�','�',
											'e','E','�','�','�','�','i','I','�','�',
											'o','O','�','�','�','�','�','�','�','�',
											'u','U','�','�','�','�','y','Y'};
			int i=0;
			ArrayList sep = new ArrayList();
			// este while procura a segunda vogal da palavra.
			while (!(IsInSet(Palavra[i],SetVogal)||Char.IsWhiteSpace(Palavra[i]))) i++;
			// avan�a para o pr�ximo caractere se o caractere atual � uma vogal
			if (IsInSet(Palavra[i],SetVogal)) i++;
			// executa o algoritimo enquanto n�o encontrar um espa�o em branco
			while (!(Char.IsWhiteSpace(Palavra[i])))	
			{  // analisa se o caractere atual � uma vogal n�o precedida por outra
				if (IsInSet(Palavra[i],SetVogal) && (!(IsInSet(Palavra[i-1],SetVogal))))
				{  // verifica se os dois caracteres atuais constituem um grupo consonantal 
					// n�o separ�vel. Se forem hifeniza duas posi��es atr�s.
					// Se n�o forem hifeniza uma posi��o atr�s.
					if ((IsInSet(Palavra[i-1],Set4) && IsInSet(Palavra[i-2],Set2)) || 
						(IsInSet(Palavra[i-1],Set3) && IsInSet(Palavra[i-2],Set1)))
						sep.Add(i-2);	else sep.Add(i-1);	}
				i++;	}
			// �ltima posi��o do vetor de separa��es deve conter um zero.
			sep.Add(0);
			// converte o Arraylist em um vetor de inteiros e retorna o vetor.
			return (int[])sep.ToArray(typeof(int));      }
		/// <summary>Este m�todo � chamado pelo set da propriedade Texto.
		/// varre o texto procurando palavras e chama o m�todo HifenizarPalavras
		/// para palavra construindo um vetor de separa��es para todo o texto
		/// com o cada vetor de retorno de cada palavra.</summary>
		private void HifenizadorPadrao()
		{
				int L=_TextoaSeparar.Length;
			// Array list para construir o vetor de separa��es do Texto
			ArrayList sep = new ArrayList();
			int Tam=0;
			for (int i=0;i<L;i++)
			{  // Se encontrou uma palavra faz a separa��o: se o caractere
				// atual j� n�o pertence a ela ou � o �ltimo do vetor
				if (Tam>0 && ((!(Char.IsLetter(_TextoaSeparar[i]))) || (i==L-1)))
				{  // se � o �ltimo caracter do texto incrementa (Tam) e (i)
					if ((i==L-1) && Char.IsLetter(_TextoaSeparar[i])) {Tam++;i++;}
					// Declara e monta vetor com a palavra a ser hifenizada
					char[] tmpPalavra=new char[Tam+1];
					for (int j=0;j<Tam;j++) tmpPalavra[j]=_TextoaSeparar[i-Tam+j];
					// coloca espa�o na �ltima posi��o
					tmpPalavra[Tam]=' ';
					// passa a palavra para ser hifenizada e pegar as separa��es
					int[] tmpSep=HifenizarPalavra(tmpPalavra);
					// coloca as posi��es de separa��o da palavra no Arraylist 
					// corrigindo os �ndices em rela��o ao vetor principal.
					for (int j=0;j<tmpSep.Length-1;j++) sep.Add(i-Tam+tmpSep[j]);
					Tam=0;	}
				// incrementa Tam enquanto o caractere for letra, ou seja, 
				// enquanto a palavra achada n�o terminar
				if ((i<L) && (Char.IsLetter(_TextoaSeparar[i]))) Tam++;		}
			// adiciona zero para indicar o fim do vetor de separa��es
			sep.Add(0);
			// converte o Arraylist em um vetor de inteiros 
			// atualizao membro da classe _PosicoesDeSeparacoes.
			_PosicoesDeSeparacoes=(int[])sep.ToArray(typeof(int));		}
	}
}
