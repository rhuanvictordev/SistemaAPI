using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaAPI.API.Services;

namespace SistemaAPI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BaseController : ControllerBase
    {
        
        public UsuarioService Usuarioservice;
        public FornecedorService FornecedorService;
        public GrupoUsuarioService GrupoUsuarioService;
        public ProdutoService ProdutoService;
        public MesaService MesaService;
        public EstoqueService EstoqueService;
        public ConsumoService ConsumoService;


        public BaseController()
        {
            Usuarioservice = new UsuarioService();
            FornecedorService = new FornecedorService();
            GrupoUsuarioService = new GrupoUsuarioService();
            ProdutoService = new ProdutoService();
            MesaService = new MesaService();
            EstoqueService = new EstoqueService();
            ConsumoService = new ConsumoService();
        }


    }
}
