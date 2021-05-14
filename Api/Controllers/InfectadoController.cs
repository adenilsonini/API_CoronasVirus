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
    public class InfectadoController : ControllerBase
    {
        private readonly InfectadoService _InfectadoService;

        public InfectadoController(InfectadoService infectadoService)
        {
            _InfectadoService = infectadoService;
        }

        /// <summary>
        /// Esta Função consulta todos os registros de invectados
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<Infectado>> Get()
        {
            return await _InfectadoService.GetAllInfectados();
        }

        /// <summary>
        /// Esta Função consulta os registros de invectados por CPF informado
        /// </summary>
        /// <remarks>
        /// Campo CPF de ser informado para a consulta
        /// </remarks>
        [HttpGet("{CPF}")]
        public async Task<Infectado> Get(string CPF)
        {
            return await _InfectadoService.GetByCPF(CPF);
        }

        /// <summary>
        /// Esta Função altera somente o endereço do infectado
        /// </summary>
        /// <remarks>
        /// Campo CPF de ser informado para atualizar as informações
        /// </remarks>
        [HttpPatch("{CPF}/endereco/{endereco}")]
        public async Task<ActionResult> Atualizar_end([FromRoute] string CPF, [FromRoute] string endereco)
        {
           bool ret = await _InfectadoService.UpdateEnd(CPF, endereco);
            if (ret == false)
                return Ok("Atualizacao não realizada.");

            return Ok("Endereço atualizado com sucesso.");
        }

        /// <summary>
        /// Esta Função para criar um novo registro de infectado
        /// </summary>
        /// <remarks>
        /// Os campos CPF, Nome completo, Data nascimento e Endereço completo são Obrigatorios
        /// </remarks>
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] InfectadoDto_input dto)
        {
            var infectado = new Infectado(dto.cpf, dto.Nome_completo, dto.DataNascimento, dto.Sexo, dto.Endereco_completo, dto.Latitude, dto.Longitude, dto.Telefones);
         
            string ret = await _InfectadoService.Inserir(infectado);

            if (ret != "ok")
                return BadRequest(ret);

            return Ok("O cadastro foi salvo com Sucesso !");
        }

        /// <summary>
        /// Esta Função Atualizará todos os campos do registro infectados
        /// </summary>
        /// <remarks>
        /// Os campos CPF, Nome completo, Data nascimento e Endereço completo são Obrigatorios
        /// </remarks>
        [HttpPut("CPF")]
        public IActionResult Update(string CPF, [FromBody] InfectadoDto dto)
        {
            var infectador = new Infectado(CPF, dto.Nome_completo, dto.DataNascimento, dto.Sexo, dto.Endereco_completo, dto.Latitude, dto.Longitude, dto.Telefones);
            
            var dtoret = _InfectadoService.Atualizar(CPF, infectador);

            if (dtoret == null)
            {
                return BadRequest("Informações não atualizadas.");
            }

            return Ok("As informações foram atualizadas.");
        }

        /// <summary>
        /// Esta Função excluirá o registro de infectado
        /// </summary>
        /// <remarks>
        /// O campo CPF deve ser Informado
        /// </remarks>
        [HttpDelete("{CPF}")]
        public async Task<IActionResult> Delete(string CPF)
        {
            var ret = await _InfectadoService.Remove(CPF);

            if (ret == 0)
            {
                return BadRequest("Informações não excluida.");
            }
            return Ok("As informações foram excluidas com sucesso.");
        }
    }
}
