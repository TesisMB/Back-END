using AutoMapper;
using Back_End.Entities;
using Back_End.Helpers;
using Back_End.Models;
using Back_End.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseApiController
    {

        private readonly ICruzRojaRepository<Users> _cruzRojaRepository;
        private readonly IMapper _mapper;

        /*Este metodo va a permitir despues poder conectarme tanto para mapear, como para obtener 
         las funciones que se establecieron repositorios correspondientes*/

        public UsersController(ICruzRojaRepository<Users> UsersRepository, IMapper mapper)

        {
            _cruzRojaRepository = UsersRepository ??
                throw new ArgumentNullException(nameof(UsersRepository));

            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public ActionResult<IEnumerable<UsersDto>> GetPersons()
        {
            {
                var usersFromRepo = _cruzRojaRepository.GetList();
                return Ok(_mapper.Map<IEnumerable<UsersDto>>(usersFromRepo));
            }
        }

         [HttpGet("{userId}", Name = "GetUser")]
         public IActionResult GetUser(int userId)
         {
             var usersFromRepo = _cruzRojaRepository.GetListId(userId);

            //Si el userID no existe se retorna NotFound.
            if (usersFromRepo == null)
             {
                 return NotFound();
             }

             //Al momento de mapear utilizo UsersDto para devolver aquellos valores imprecidibles
             return Ok(_mapper.Map<UsersDto>(usersFromRepo));
         }

        // GET: api/Authors/5
        /*[HttpGet("{userID}")]
        public IActionResult Get(int userID)
        {
            var usersFromRepo = _cruzRojaRepository.GetDto(userID);
            if (usersFromRepo == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<UsersDto>(usersFromRepo));
        } */



        //Agregar un nuevo Usuario y devolve el Id Creado del Usuario
        [HttpPost("{Register}")]
        //[Authorize(Roles = "Coordinador General, Admin")]
        public ActionResult<UsersDto> CreateUser(UsersForCreationDto user)
        {
            var userEntity = _mapper.Map<Users>(user);

            //Al crear un Usuario se encripta dicha contraseña para mayor seguridad.
            userEntity.UserPassword = Encrypt.GetSHA256(userEntity.UserPassword);

            _cruzRojaRepository.Add(userEntity);
            _cruzRojaRepository.save();

            /*Una vez comprobado con exito todo se procede a realizar el mapeo 
            que va a permitir manipular como se devuelven los datos */
            var authorToReturn = _mapper.Map<UsersDto>(userEntity);

            return Ok();

        }
    }

}
