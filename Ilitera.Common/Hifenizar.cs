// Hifenizar.cs - compile with: /doc:Hifenizar.xml
using System;
using System.Text;
using System.Collections;

namespace Ilitera.Common
{	/// <summary>
	/// Classe para hifenizar Textos em português
	/// </summary>
	/// <remarks>Esta classe contém apenas duas propriedades públicas:
	/// (1) Texto (string) - onde deve ser colocado o Texto a ser Hifenizado;
	/// (2) Separacoes (int[]) - vetor para recuperar as posições das hifenizações.
	/// Preste atenção à utilização de StringBuilders e ArrayLists.
	/// Por André Tomás Velloso (andre@velloso.com) - Revista Fórum Access</remarks>
	// Note nas condições da classe que no C# os operadores '&&' e '||' são 
	// expertos e só avaliam o segundo operando quando o primeiro é, 
	// respectivamente, true ou false.
	public sealed class Hifenizar
	{	/// <summary>Membro para guardar Vetor de hifenizações.</summary>
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
		/// <summary>Vetor de posições onde é possivel hifenizar o texto.
		/// A última posição do vetor é sempre zero para indicar que 
		/// não há mais hifenização possível.
		/// </summary>
		public int[] Separacoes	{get {return _PosicoesDeSeparacoes;}}
		/// <summary>Contrutor que coloca o valor de NewTexto na propriedade Texto</summary>
		/// <param name="NewTexto">Texto (string) a ser hifenizado</param>
		public Hifenizar(string NewTexto) {this.Texto=NewTexto;}	
		/// <summary>Construtor padrão, chama o outro cosntrutor com uma string vazia</summary>
		public Hifenizar() : this(String.Empty) {}
		/// <summary>Método boleano, procura um caractere em um vetor de caracteres</summary>
		/// <param name="C">Caractere a ser procurado no conjunto.</param>
		/// <param name="SetOfChar">Conjunto (char[]) de caracteres.</param>
		/// <returns>Boleano indicando se o caractere pertence ao conjunto</returns>
		private static bool IsInSet(char C, char[] SetOfChar)
		{
				foreach (char member in SetOfChar) { if (C==member) return true;}
			return false; 	}
		/// <summary>Rotina que contém toda a logica de Hifenização
		/// de palavras em português</summary>
		/// <param name="Palavra">
		/// Um array (char[]) contendo a palavra, cada caractere em uma posição
		/// do array começando pela posição 0 e contendo como último caractere
		/// um espaço que é a terminação esperada pelo algoritimo.</param>
		/// <returns>Retorna um vetor de posições de hifenização para a palavra</returns>
		private static int[] HifenizarPalavra(char[] Palavra)
		{  // Conjuntos de caracteres para representar os grupos cosonantais 
			// não passíveis de separação (Set1 e Set3) ou (Set2 e Set4)
			char[] Set1 = new char[]{'b','B','c','C','d','D','f','F',
										'g','G','p','P','t','T','v','V'}; 
			char[] Set3 = new char[]{'l','L','r','R'};
			char[] Set2 = new char[]{'c','C','l','L','n','N'};
			char[] Set4 = new char[]{'h','H'};
			// Conjunto de caracteres contendo as vogais mais a letra Y.
			char[] SetVogal = new char[]{'a','A','á','Á','à','À','â','Â','ã','Ã','ä','Ä',
											'e','E','é','É','ê','Ê','i','I','í','Í',
											'o','O','ó','Ó','ô','Ô','õ','Õ','ö','Ö',
											'u','U','ú','Ú','ü','Ü','y','Y'};
			int i=0;
			ArrayList sep = new ArrayList();
			// este while procura a segunda vogal da palavra.
			while (!(IsInSet(Palavra[i],SetVogal)||Char.IsWhiteSpace(Palavra[i]))) i++;
			// avança para o próximo caractere se o caractere atual é uma vogal
			if (IsInSet(Palavra[i],SetVogal)) i++;
			// executa o algoritimo enquanto não encontrar um espaço em branco
			while (!(Char.IsWhiteSpace(Palavra[i])))	
			{  // analisa se o caractere atual é uma vogal não precedida por outra
				if (IsInSet(Palavra[i],SetVogal) && (!(IsInSet(Palavra[i-1],SetVogal))))
				{  // verifica se os dois caracteres atuais constituem um grupo consonantal 
					// não separável. Se forem hifeniza duas posições atrás.
					// Se não forem hifeniza uma posição atrás.
					if ((IsInSet(Palavra[i-1],Set4) && IsInSet(Palavra[i-2],Set2)) || 
						(IsInSet(Palavra[i-1],Set3) && IsInSet(Palavra[i-2],Set1)))
						sep.Add(i-2);	else sep.Add(i-1);	}
				i++;	}
			// última posição do vetor de separações deve conter um zero.
			sep.Add(0);
			// converte o Arraylist em um vetor de inteiros e retorna o vetor.
			return (int[])sep.ToArray(typeof(int));      }
		/// <summary>Este método é chamado pelo set da propriedade Texto.
		/// varre o texto procurando palavras e chama o método HifenizarPalavras
		/// para palavra construindo um vetor de separações para todo o texto
		/// com o cada vetor de retorno de cada palavra.</summary>
		private void HifenizadorPadrao()
		{
				int L=_TextoaSeparar.Length;
			// Array list para construir o vetor de separações do Texto
			ArrayList sep = new ArrayList();
			int Tam=0;
			for (int i=0;i<L;i++)
			{  // Se encontrou uma palavra faz a separação: se o caractere
				// atual já não pertence a ela ou é o último do vetor
				if (Tam>0 && ((!(Char.IsLetter(_TextoaSeparar[i]))) || (i==L-1)))
				{  // se é o último caracter do texto incrementa (Tam) e (i)
					if ((i==L-1) && Char.IsLetter(_TextoaSeparar[i])) {Tam++;i++;}
					// Declara e monta vetor com a palavra a ser hifenizada
					char[] tmpPalavra=new char[Tam+1];
					for (int j=0;j<Tam;j++) tmpPalavra[j]=_TextoaSeparar[i-Tam+j];
					// coloca espaço na última posição
					tmpPalavra[Tam]=' ';
					// passa a palavra para ser hifenizada e pegar as separações
					int[] tmpSep=HifenizarPalavra(tmpPalavra);
					// coloca as posições de separação da palavra no Arraylist 
					// corrigindo os índices em relação ao vetor principal.
					for (int j=0;j<tmpSep.Length-1;j++) sep.Add(i-Tam+tmpSep[j]);
					Tam=0;	}
				// incrementa Tam enquanto o caractere for letra, ou seja, 
				// enquanto a palavra achada não terminar
				if ((i<L) && (Char.IsLetter(_TextoaSeparar[i]))) Tam++;		}
			// adiciona zero para indicar o fim do vetor de separações
			sep.Add(0);
			// converte o Arraylist em um vetor de inteiros 
			// atualizao membro da classe _PosicoesDeSeparacoes.
			_PosicoesDeSeparacoes=(int[])sep.ToArray(typeof(int));		}
	}
}
