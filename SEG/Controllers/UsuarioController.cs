using Microsoft.AspNetCore.Mvc;
using SEG.Filters;
using SEG.Models;
using SEG.Repositories;

namespace SEG.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;

        public UsuarioController(IUnitOfWork unitOfWork, ILogger<UsuarioController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [HttpPost("/ValidarUsuarioLogin/{usuario}/{senha}")]
        //[ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<ActionResult<Usuario>>ValidarUsuarioLogin(string usuario, string senha)
        {
            var login = await _unitOfWork.UsuarioRepository.ValidarUsuarioLogin(usuario, senha);

            return login ? Ok() : BadRequest();
        }


    }
}
