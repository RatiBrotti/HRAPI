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
        [ProducesResponseType(typeof(AdministratorModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> CreateAdministrator(AdministratorModel administrator)
        {
            var employeeEntityCheck = _context.Employees.FirstOrDefault(x => x.IdNumber == administrator.IdNumber);
            if (employeeEntityCheck!=null && employeeEntityCheck.AdministratorId > 0 )
            {
                var admin = _context.Administrators.FirstOrDefault(x=>x.Id==employeeEntityCheck.AdministratorId);
                admin.Password = administrator.Password;
                admin.Email = administrator.Email;
                _context.SaveChanges();
                return Ok(administrator);
            }

            if (_context.Administrators.AsQueryable().Count() == 0)
            {
                var administratorEntity = _mapper.Map<Administrator>(administrator);
                var employeeEntity = _mapper.Map<Employee>(administrator);
                _context.Administrators.Add(administratorEntity);
                _context.Employees.Add(employeeEntity);
                _context.SaveChanges();
                employeeEntity.AdministratorId = administratorEntity.Id;
                _context.SaveChanges();

                return Ok(administrator);
            }
            if(employeeEntityCheck.AdministratorId == 0)
            {
                return BadRequest("თქვენ არ გაქვთ რეგისტრაციის უფლება");
            }

            return BadRequest("მოხდა შეცდომა ადმინისტრატორის რეგისტრაციისას");

        }

        /// <summary>
        /// Returns all Administrators
        /// </summary>
        /// <returns>List Of Administrators</returns>
        /// <response code="200">Returns List Of authors</response>
        /// <response code="404">If the item is null</response>
        [ProducesResponseType(typeof(List<AdministratorModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var administratorsEntities = _context.Administrators.Include(x => x.Employee).ToList();
            if (administratorsEntities == null) return NotFound();

            var administrators = administratorsEntities
        .Select(administratorEntity => new AdministratorModel
        {
            IdNumber = administratorEntity.Employee.IdNumber,
            Name = administratorEntity.Employee.Name,
            LastName = administratorEntity.Employee.LastName,
            Gender = administratorEntity.Employee.Gender,
            BirthDate = administratorEntity.Employee.BirthDate,
            Email = administratorEntity.Email,
            Password = administratorEntity.Password,
            JobTitle = administratorEntity.Employee.JobTitle,
            Status = administratorEntity.Employee.Status,
            Mobile = administratorEntity.Employee.Mobile,
            DismissalDate = administratorEntity.Employee.DismissalDate
        }).ToList();
            if (administrators == null) return NotFound();

            return Ok(administrators);
        }

        /// <summary>
        /// Returns administrator conteining id
        /// </summary>
        /// <param name = "idNumber" ></ param >
        /// < returns > administrator </ returns >
        [ProducesResponseType(typeof(List<AdministratorModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{idNumber}")]
        public async Task<IActionResult> GetAdministrator(string idNumber)
        {
            var administratorEntity = _context.Administrators.Include(x=>x.Employee).FirstOrDefault(x=>x.Employee.IdNumber==idNumber);
           
            if (administratorEntity == null) return NotFound();

            var administrator = _mapper.Map<AdministratorModel>(administratorEntity);
            administrator = _mapper.Map<AdministratorModel>(administratorEntity.Employee);

            return Ok(administrator);
        }

        /// <summary>
        /// Returns administrator with userName
        /// </summary>
        /// <param name="userName"></param>
        /// <returns>administrator</returns>
        [ProducesResponseType(typeof(List<AdministratorModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{userName}")]
        public async Task<IActionResult> GetAdministratorByUserName(string userName)
        {
            var administratorEntity = _context.Administrators.Include(x => x.Employee).FirstOrDefault(x => x.Email == userName);

            if (administratorEntity == null) return NotFound();

            var administrator = _mapper.Map<AdministratorModel>(administratorEntity);
            administrator = _mapper.Map<AdministratorModel>(administratorEntity.Employee);

            return Ok(administrator);
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
            var employeeEntity = _context.Employees.FirstOrDefault(x => x.IdNumber == idNumber);

            if (employeeEntity == null) return BadRequest();

            employeeEntity.AdministratorId = 0;
            _context.SaveChanges();
            var administrator = _mapper.Map<AdministratorModel>(employeeEntity);

            return Ok(administrator);
            }
        }
}
