using Api.Data.Collections;
using Api.Models;
using Api.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuradoController : ControllerBase
    {
        private readonly CuradoService _CuradoService;

        public CuradoController(CuradoService CuradoService)
        {
            _CuradoService = CuradoService;
        }

        /// <summary>
        /// Esta Função consulta todos os registros de Curados
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<Curado>> Get()
        {
            return await _CuradoService.GetAllInfectados();
        }

        /// <summary>
        /// Esta Função consulta os registros de curados por CPF informado
        /// </summary>
        /// <remarks>
        /// Campo CPF de ser informado para a consulta
        /// </remarks>
        [HttpGet("{CPF}")]
        public async Task<Curado> Get(string CPF)
        {
            return await _CuradoService.GetByCPF(CPF);
        }

        /// <summary>
        /// Esta Função altera somente o endereço do curado
        /// </summary>
        /// <remarks>
        /// Campo CPF de ser informado para atualizar as informações
        /// </remarks>
        [HttpPatch("{CPF}/endereco/{endereco}")]
        public async Task<ActionResult> Atualizar_end([FromRoute] string CPF, [FromRoute] string endereco)
        {
           bool ret = await _CuradoService.UpdateEnd(CPF, endereco);
            if (ret == false)
                return Ok("Atualizacao não realizada.");

            return Ok("Endereço atualizado com sucesso.");
        }

        /// <summary>
        /// Esta Função para criar um novo registro de curado
        /// </summary>
        /// <remarks>
        /// Os campos CPF, Nome completo, Data nascimento e Endereço completo são Obrigatorios
        /// </remarks>
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CuradoDtoinput dto)
        {
            var curado = new Curado(dto.cpf, dto.Nome_completo, dto.DataNascimento, dto.Sexo, dto.Endereco_completo,dto.Telefones);

            string ret = await _CuradoService.Inserir(curado);

            if (ret != "ok")
                return BadRequest(ret);

            return Ok("O cadastro foi salvo com Sucesso !");
        }

        /// <summary>
        /// Esta Função Atualizará todos os campos do registro Curado
        /// </summary>
        /// <remarks>
        /// Os campos CPF, Nome completo, Data nascimento e Endereço completo são Obrigatorios
        /// </remarks>
        [HttpPut("CPF")]
        public async Task<IActionResult> Update(string CPF, [FromBody] CuradoDto dto)
        {
            var curado = new Curado(CPF, dto.Nome_completo, dto.DataNascimento, dto.Sexo, dto.Endereco_completo, dto.Telefones);
            var dtoret = await  _CuradoService.Atualizar(CPF, curado);

            if (dtoret == false)
            {
                return BadRequest("Informações não atualizadas.");
            }

            return Ok("Informações Atualizadas com Sucesso");
        }

        /// <summary>
        /// Esta Função excluirá o registro do Curado
        /// </summary>
        /// <remarks>
        /// O campo CPF deve ser Informado
        /// </remarks>
        [HttpDelete("{CPF}")]
        public async Task<IActionResult> Delete(string CPF)
        {
            var ret = await _CuradoService.Remove(CPF);

            if (ret == 0)
            {
                return BadRequest("Informações não excluida.");
            }
            return Ok("As informações foram excluidas com sucesso.");
        }
    }
}
