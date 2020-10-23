using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Back_End.Entities;
using Back_End.Models.Estate___Dto;
using Back_End.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Back_End.Controllers
{
    [Route("api/resources/[controller]")]
    [ApiController]
    public class EstateController : BaseApiController
    {
        private readonly ICruzRojaRepository<Estate> _cruzRojaRepository;

        public EstateController(ICruzRojaRepository<Estate> cruzRojaRepository)

        {
            _cruzRojaRepository = cruzRojaRepository ??
                throw new ArgumentNullException(nameof(UsersRepository));

        }
    

    [HttpGet]
    //[Authorize(Roles = "Coordinador General, Admin")]  //Autorizo unicamente los usuarios que tenga el permiso de listar los usuarios
    public ActionResult<IEnumerable<EstateDto>> GetEstate()
    {
        {
            var usersFromRepo = _cruzRojaRepository.GetList();
        return Ok(usersFromRepo);
    }

    }
        //[HttpDelete("{EstateID}")]
        ////[Authorize(Roles = "Coordinador General, Admin")]
        //public ActionResult Delete(int UserID)
        //{

        //    //var userFromRepo = _cruzRojaRepository.GetListId(EstateID);


        //    // si el Id del Usuario no existe de retorna Error.
        //    if (userFromRepo == null)
        //    {
        //        return NotFound();
        //    }

        //    _cruzRojaRepository.Delete(userFromRepo);

        //    _cruzRojaRepository.save();

        //    // Se retorna con exito la eliminacion del Usuario especificado
        //    return Ok();
        //}
    }

    
}

