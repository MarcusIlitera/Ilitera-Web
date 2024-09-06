using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities
{
    public class ClassificacaoFuncionalDTO
    {
        public int IdClassificacaoFuncional { get; set; }
        public int IdEmpregado { get; set; }
        public int CodGFIP { get; set; } 
        public string Funcao { get; set; } 
        public string Setor { get; set; }    
        public DateTime InicioFuncao { get; set; }   
        public DateTime TerminoFuncao { get; set; }
        public string Jornada { get; set; }
        public string Cbo { get; set; }
        public string DescricaoFuncao { get; set; }
        public string NomeAlocado { get; set; }
        public string Alocado { get; set; }
        public string atual { get; set; }
        public string DescricaoCargo { get; set; }
        public string tCentro_Custo{ get; set; }
    }
}
