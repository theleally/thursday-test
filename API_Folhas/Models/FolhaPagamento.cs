using System;
using System.ComponentModel.DataAnnotations;
using API_Folhas.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Folhas.Models
{
    public class FolhaPagamento
    {
        public FolhaPagamento() => CriadoEm = DateTime.Now;
        public int FolhaPagamentoId { get; set; }
        public int FuncionarioId { get; set; }
        [ForeignKey("FuncionarioId")]
        public Funcionario Funcionario { get; set; }
        public int ValorHora { get; set; }
        public int QuantidadeHoras { get; set; }
        public int SalarioBruto { get; set; }
        public double ImpostoRenda { get; set; }
        public double ImpostoInss { get; set; }
        public int ImpostoFgts { get; set; }
        public double SalarioLiquido { get; set; }
        public string Mes { get; set; }
        public string Ano { get; set; }
        public DateTime CriadoEm { get; set; }
    }
}