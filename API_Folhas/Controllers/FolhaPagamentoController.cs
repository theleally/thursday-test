using System.Collections.Generic;
using System.Linq;
using API_Folhas.Models;
using Microsoft.AspNetCore.Mvc;

namespace API_Folhas.Controllers
{
    [ApiController]
    [Route("api/folha")]
    public class FolhaPagamentoController : ControllerBase
    {
        private readonly DataContext _context;

        public FolhaPagamentoController(DataContext context) =>
            _context = context;

        private static List<Funcionario> funcionarios = new List<Funcionario>();

        // GET: /api/folha/listar
        [Route("listar")]
        [HttpGet]
        public IActionResult Listar() =>
            Ok(_context.Folhas.ToList());

        // POST: /api/folha/cadastrar
        [Route("cadastrar")]
        [HttpPost]
        public IActionResult Cadastrar([FromBody] FolhaPagamento folhap)
        {
            folhap.SalarioBruto = folhap.QuantidadeHoras * folhap.ValorHora;
            folhap.ImpostoFgts = folhap.SalarioBruto * 0.08;
            if (folhap.SalarioBruto > 1903.98 && folhap.SalarioBruto < 2826.65)
            {
                folhap.ImpostoRenda = 142.8;
            } else if (folhap.SalarioBruto < 3751.05) {
                folhap.ImpostoRenda = 354.8;
            } else if (folhap.SalarioBruto < 4664.68) {
                folhap.ImpostoRenda = 636.13;
            } else {
                folhap.ImpostoRenda = 869.36;
            }
            if (folhap.SalarioBruto < 1693.72)
            {
                folhap.ImpostoInss = 0.08 * folhap.SalarioBruto;
            } else if (folhap.SalarioBruto < 2822.90)
            {
                folhap.ImpostoInss = 0.09 * folhap.SalarioBruto;
            } else if (folhap.SalarioBruto < 5645.80)
            {
                folhap.ImpostoInss = 0.11 * folhap.SalarioBruto;
            } else
            {
                folhap.ImpostoInss = 621.03;
            }
            folhap.SalarioLiquido = folhap.SalarioBruto - folhap.ImpostoRenda - folhap.ImpostoInss;
            _context.Folhas.Add(folhap);
            _context.SaveChanges();
            return Created("", folhap);
        }

        // GET: /api/folha/buscar/123
        [Route("buscar/{cpf}")]
        [HttpGet]
        public IActionResult Buscar([FromRoute] string cpf)
        {
            FolhaPagamento folhap =
                _context.Folhas.FirstOrDefault
            (
                f => f.Funcionario.Cpf.Equals(cpf)
            );
            return folhap != null ? Ok(folhap) : NotFound();
        }

         //GET: /api/folha/filtrar/12/2022
        [Route("filtrar/{mes}/{ano}")]
        [HttpGet]
        public IActionResult Filtrar([FromRoute] string mes, [FromRoute] string ano)
        {
            
            FolhaPagamento folham =
                _context.Folhas.FirstOrDefault
            (
                f => f.Mes.Equals(mes)
            );
            FolhaPagamento folhaa = _context.Folhas.FirstOrDefault
            (
                f => f.Ano.Equals(ano)
            );
            return folham != null || folhaa != null ? Ok(folham) : NotFound();
        }
    }
}
