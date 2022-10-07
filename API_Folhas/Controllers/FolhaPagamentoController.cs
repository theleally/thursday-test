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
