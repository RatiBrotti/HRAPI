using AutoMapper;
using HRAPI.DataAccess;
using HRAPI.Entities;
using HRAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HRAPI.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class AdministratorController : ControllerBase
    {
        private readonly HRContext _context;
        private readonly IMapper _mapper;

        public AdministratorController(HRContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        /// <summary>
        /// Creates Administrator entity and returns Administrator
        /// </summary>
        /// <param name="administrator"></param>
        /// <returns>administrator</returns>
        [ProducesResponseType(typeof(List<Administrator>), StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<IActionResult> CreateAdministrator(Administrator administrator)
        {
            var administratorEntity = _mapper.Map<AdministratorEntity>(administrator);

            _context.Administrators.Add(administratorEntity);
            _context.SaveChanges();

            var AdministratorResponse = _mapper.Map<Administrator>(administratorEntity);

            return Ok(AdministratorResponse);

        }

        /// <summary>
        /// Returns all Administrators
        /// </summary>
        /// <returns>List Of Administrators</returns>
        /// <response code="200">Returns List Of authors</response>
        /// <response code="404">If the item is null</response>
        [ProducesResponseType(typeof(List<Administrator>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var administratorsEntities = _context.Administrators.ToList();
            if (administratorsEntities == null) return NotFound();
            var administrators = _mapper.Map<IEnumerable<Administrator>>(administratorsEntities).ToList();
            return Ok(administrators);
        }

        /// <summary>
        /// Returns administrator conteining id
        /// </summary>
        /// <param name="idNumber"></param>
        /// <returns>administrator</returns>
        [ProducesResponseType(typeof(List<Administrator>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{idNumber}")]
        public async Task<IActionResult> GetAdministrator(string idNumber)
        {
            var administratorEntity = _context.Administrators.FirstOrDefault(x=> x.IdNumber==idNumber);
            if (administratorEntity == null) return NotFound();

            var administrator = _mapper.Map<Administrator>(administratorEntity);

            return Ok(administrator);
        }

        /// <summary>
        /// returns administrators conteining search word
        /// </summary>
        /// <param name="searchWord"></param>
        /// <returns>administrators</returns>
        [ProducesResponseType(typeof(List<Administrator>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{searchWord}")]
        public async Task<ActionResult> Find(string searchWord)
        {
            searchWord = searchWord.ToUpper();
            var administratorEntities =  _context.Administrators.Where(a => a.Name.Contains(searchWord) || a.LastName.Contains(searchWord) || a.IdNumber.Contains(searchWord) || a.Email.Contains(searchWord));
            if (administratorEntities == null) return NotFound(searchWord);
            var administrators = _mapper.Map<IEnumerable<Administrator>>(administratorEntities);
            return Ok(administrators);
        }

        /// <summary>
        /// deletes administrator entity conteining id
        /// </summary>
        /// <param name="idNumber"></param>
        /// <returns>administrator</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{idNumber}")]
        public async Task<IActionResult> DeleteAdministrator(string idNumber)
        {
            var administartorEntity = _context.Administrators.FirstOrDefault(x=>x.IdNumber==idNumber);

            if (administartorEntity == null) return BadRequest();

            _context.Administrators.Remove(administartorEntity);
            _context.SaveChanges();
            var administrator = _mapper.Map<Administrator>(administartorEntity);

            return Ok(administrator);
        }
    }
}
