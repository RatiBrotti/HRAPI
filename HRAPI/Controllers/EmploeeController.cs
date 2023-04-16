using AutoMapper;
using HRAPI.DataAccess;
using HRAPI.Entities;
using HRAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRAPI.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class EmploeeController : ControllerBase
    {
        private readonly HRContext _context;
        private readonly IMapper _mapper;

        public EmploeeController(HRContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        /// <summary>
        /// Creates Emploee entity and returns Employee
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>Employee</returns>
        [ProducesResponseType(typeof(List<Employee>), StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<IActionResult> CreateEmployee(Employee employee)
        {
            var employeeEntity = _mapper.Map<EmployeeEntity>(employee);

            _context.Employees.Add(employeeEntity);
            _context.SaveChanges();

            var employeeResponse = _mapper.Map<Employee>(employeeEntity);

            return Ok(employeeResponse);

        }

        /// <summary>
        /// Returns all Employees
        /// </summary>
        /// <returns>List Of Employees</returns>
        /// <response code="200">Returns List Of authors</response>
        /// <response code="404">If the item is null</response>
        [ProducesResponseType(typeof(List<Employee>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var employeeEntities = _context.Employees.ToList();
            if (employeeEntities == null) return NotFound();
            var employees = _mapper.Map<IEnumerable<Employee>>(employeeEntities).ToList();
            return Ok(employees);
        }

        /// <summary>
        /// Returns Employee conteining id
        /// </summary>
        /// <param name="idNumber"></param>
        /// <returns>Employee</returns>
        [ProducesResponseType(typeof(List<Employee>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{idNumber}")]
        public async Task<IActionResult> GetAdministrator(string idNumber)
        {
            var employeeEntity = _context.Employees.FirstOrDefault(x => x.IdNumber == idNumber);
            if (employeeEntity == null) return NotFound();

            var employee = _mapper.Map<Employee>(employeeEntity);

            return Ok(employee);
        }

        /// <summary>
        /// returns Emplyees conteining search word
        /// </summary>
        /// <param name="searchWord"></param>
        /// <returns>Employees</returns>
        [ProducesResponseType(typeof(List<Employee>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{searchWord}")]
        public async Task<ActionResult> Find(string searchWord)
        {
            searchWord = searchWord.ToUpper();
            var employeeEntities = _context.Employees.Where(a => a.Name.Contains(searchWord) || a.LastName.Contains(searchWord) || a.IdNumber.Contains(searchWord) || a.JobTitle.Contains(searchWord) || a.Status.Contains(searchWord));
            if (employeeEntities == null) return NotFound(searchWord);
            var eployees = _mapper.Map<IEnumerable<Employee>>(employeeEntities);
            return Ok(eployees);
        }

        /// <summary>
        /// deletes Emplyee entity conteining id
        /// </summary>
        /// <param name="idNumber"></param>
        /// <returns>Employee</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{idNumber}")]
        public async Task<IActionResult> DeleteAdministrator(string idNumber)
        {
            var employeeEntity = _context.Employees.FirstOrDefault(x => x.IdNumber == idNumber);

            if (employeeEntity == null) return BadRequest();

            _context.Employees.Remove(employeeEntity);
            _context.SaveChanges();
            var employee = _mapper.Map<Employee>(employeeEntity);

            return Ok(employee);
        }
    }
}
