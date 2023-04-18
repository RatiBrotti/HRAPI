using AutoMapper;
using HRAPI.DataAccess;
using HRAPI.Entities;
using HRAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HRMVC.Tools;

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
        [ProducesResponseType(typeof(List<EmployeeModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> CreateEmployee(EmployeeModel employee)
        {
            var checkEmployee = _context.Employees.FirstOrDefault(x=>x.IdNumber==employee.IdNumber);
            if (checkEmployee != null)
            {
                return BadRequest("ასეთი თანამშრომელი უკვე არსებობს");
            }
            var employeeEntity = _mapper.Map<Employee>(employee);

            _context.Employees.Add(employeeEntity);
            _context.SaveChanges();

            var employeeResponse = _mapper.Map<EmployeeModel>(employeeEntity);

            return Ok(employeeResponse);

        }

        /// <summary>
        /// Makes Emploee entity admin
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>Employee</returns>
        [ProducesResponseType(typeof(List<EmployeeModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost]
        public async Task<IActionResult> MakeEmployeeAdmin(string idNumber)
        {
            var checkEmployee = _context.Employees.FirstOrDefault(x => x.IdNumber == idNumber);
            if (checkEmployee == null)
            {
                return NotFound("ასეთი თანამშრომელი არ არსებობს");
            }
            Administrator administrator = new();
            administrator.Email = checkEmployee.IdNumber + "@hr.ge";
            administrator.Password = PasswordTools.MD5Hash(checkEmployee.Name.ToUpper()+checkEmployee.LastName.ToLower()+checkEmployee.IdNumber+"!@#");
            _context.Administrators.Add(administrator);
            _context.SaveChanges();
            checkEmployee.AdministratorId = administrator.Id;
            _context.SaveChanges();


            var employeeResponse = _mapper.Map<AdministratorModel>(checkEmployee);
            employeeResponse = _mapper.Map<AdministratorModel>(administrator);

            return Ok(employeeResponse);

        }

        /// <summary>
        /// Updates Emploee entity and returns Employee
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>Employee</returns>
        [ProducesResponseType(typeof(List<EmployeeModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut]
        public async Task<IActionResult> UpdateEmployee(EmployeeModel employee)
        {
            var employeeEntity = _context.Employees.FirstOrDefault(x => x.IdNumber == employee.IdNumber);
            if (employeeEntity == null)
            {
                return BadRequest("ასეთი თანამშრომელი არ არსებობს");
            }
            employeeEntity.Name = employee.Name;
            employeeEntity.LastName = employee.LastName;
            employeeEntity.Gender = employee.Gender;
            employeeEntity.BirthDate = employee.BirthDate;
            employeeEntity.JobTitle = employee.JobTitle;
            employeeEntity.Status = employee.Status;
            employeeEntity.DismissalDate = employee.DismissalDate;
            employeeEntity.Mobile = employee.Mobile;

            _context.SaveChanges();

            var employeeResponse = _mapper.Map<EmployeeModel>(employeeEntity);

            return Ok(employeeResponse);

        }

        /// <summary>
        /// Returns all Employees
        /// </summary>
        /// <returns>List Of Employees</returns>
        /// <response code="200">Returns List Of authors</response>
        /// <response code="404">If the item is null</response>
        [ProducesResponseType(typeof(List<EmployeeModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var employeeEntities = _context.Employees.ToList();
            if (employeeEntities == null) return NotFound();
            var employees = _mapper.Map<IEnumerable<EmployeeModel>>(employeeEntities).ToList();
            return Ok(employees);
        }

        /// <summary>
        /// Returns Employee conteining id
        /// </summary>
        /// <param name="idNumber"></param>
        /// <returns>Employee</returns>
        [ProducesResponseType(typeof(List<EmployeeModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{idNumber}")]
        public async Task<IActionResult> GetEmployee(string idNumber)
        {
            var employeeEntity = _context.Employees.FirstOrDefault(x => x.IdNumber == idNumber);
            if (employeeEntity == null) return NotFound();

            var employee = _mapper.Map<EmployeeModel>(employeeEntity);

            return Ok(employee);
        }

        /// <summary>
        /// returns Emplyees conteining search word
        /// </summary>
        /// <param name="searchWord"></param>
        /// <returns>Employees</returns>
        [ProducesResponseType(typeof(List<EmployeeModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{searchWord}")]
        public async Task<ActionResult> Find(string searchWord)
        {
            var employeeEntities = _context.Employees.Where(a => a.Name.Contains(searchWord) || a.LastName.Contains(searchWord) || a.IdNumber.Contains(searchWord) || a.JobTitle.Contains(searchWord) || a.Status.Contains(searchWord));
            if (employeeEntities == null) return NotFound(searchWord);
            var eployees = _mapper.Map<IEnumerable<EmployeeModel>>(employeeEntities);
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
        public async Task<IActionResult> DeleteEmployee(string idNumber)
        {
            var employeeEntity = _context.Employees.FirstOrDefault(x => x.IdNumber == idNumber);

            if (employeeEntity == null) return BadRequest();

            _context.Employees.Remove(employeeEntity);
            _context.SaveChanges();
            var employee = _mapper.Map<EmployeeModel>(employeeEntity);

            return Ok(employee);
        }
    }
}
