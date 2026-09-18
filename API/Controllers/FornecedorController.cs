using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestauranteAPI.API.DTOs.Response;
using RestauranteAPI.Data;
using SistemaAPI.API.DTOs.Requests;
using SistemaAPI.API.Services;
using SistemaAPI.Models;

namespace SistemaAPI.API.Controllers
{
    public class FornecedorController : BaseController
    {


        [HttpPost]
        public ActionResult CadastrarFornecedor([FromBody] CriarFornecedorDTO req)
        {
            Fornecedor f = new Fornecedor() { };
            RepositorioRetorno retorno = FornecedorService.Save(req);
            if (retorno.Success)
                return StatusCode(201, new APIResponseDTO { Status = 201, Data = retorno.Result, Message = "Fornecedor cadastrado com sucesso" });

            return BadRequest(retorno.Message);
        }




        [HttpGet]
        public ActionResult ListarFornecedores()
        {
            RepositorioRetorno retorno = FornecedorService.Query();
            if (retorno.Success)
                return Ok(new APIResponseDTO { Status = 200, Data = retorno.Result });

            return BadRequest(retorno.Message);
        }




        [HttpGet("{id}")]
        public ActionResult ObterFornecedor(long id)
        {
            Fornecedor f = FornecedorService.GetById(id);
            if (f == null)
                return NotFound("Fornecedor não encontrado");

            return Ok(new APIResponseDTO { Status = 200, Data = f });
        }




        [HttpDelete("{id}")]
        public ActionResult DeletarFornecedor(long id)
        {
            RepositorioRetorno retorno = FornecedorService.Delete(id);
            if (retorno.Success)
                return Ok("Fornecedor deletado com sucesso");

            return BadRequest(retorno.Message);
        }




        [HttpPut("{id}")]
        public ActionResult EditarFornecedor(long id, [FromBody] Fornecedor request)
        {
            Fornecedor f = FornecedorService.GetById(id);
            if (f == null)
                return NotFound(new APIResponseDTO { Message = "Fornecedor não encontrado" });

            f.Nome = request.Nome;
            f.Estado = request.Estado;
            f.Cidade = request.Cidade;
            f.Bairro = request.Bairro;
            f.Numero = request.Numero;
            f.CPF_CNPJ = request.CPF_CNPJ;
            f.Telefone1 = request.Telefone1;
            f.Telefone2 = request.Telefone2;
            f.Alterado = DateTime.Now;

            RepositorioRetorno retorno = FornecedorService.Update(f);
            if (retorno.Success)
                return Ok(new APIResponseDTO { Status = 200, Data = f, Message = "Fornecedor editado com sucesso" });

            return BadRequest(retorno.Message);
        }




        [HttpPatch("{id}")]
        public ActionResult AlterarFornecedor(long id, [FromBody] Fornecedor request)
        {
            Fornecedor f = FornecedorService.GetById(id);
            if (f == null)
                return NotFound(new APIResponseDTO { Message = "Fornecedor não encontrado" });

            if (request.IdFornecedor != null && request.IdFornecedor != f.IdFornecedor)
                return NotFound(new APIResponseDTO { Message = "Não é possível alterar o ID do fornecedor" });

            f.Nome = request.Nome == null ? f.Nome : request.Nome;
            f.Estado = request.Estado == null ? f.Estado : request.Estado;
            f.Cidade = request.Cidade == null ? f.Cidade : request.Cidade;
            f.Bairro = request.Bairro == null ? f.Bairro : request.Bairro;
            f.Numero = request.Numero == null ? f.Numero : request.Numero;
            f.CPF_CNPJ = request.CPF_CNPJ == null ? f.CPF_CNPJ : request.CPF_CNPJ;
            f.Telefone1 = request.Telefone1 == null ? f.Telefone1 : request.Telefone1;
            f.Telefone2 = request.Telefone2 == null ? f.Telefone2 : request.Telefone2;

            RepositorioRetorno retorno = FornecedorService.Update(f);
            if (retorno.Success)
                return Ok(new APIResponseDTO { Status = 200, Data = f, Message = "Fornecedor atualizado com sucesso" });

            return BadRequest(retorno.Message);
        }



    }
}
