using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ContainerDASample.Database.Model;
using ContainerDASample.Database.Repository;

namespace ContainerDASample.Controllers
{
    [Produces("application/json")]
    [Route("api/Employees")]
    public class EmployeesController : Controller
    {
        private IEmpRepository _empRespository;
        public EmployeesController(IEmpRepository empRepository)
        {
            _empRespository = empRepository;
        }
        // GET api/values
        [HttpGet]
        public async Task<List<Employees>> Get()
        {
            return await _empRespository.GetAllEmployees();
        }



        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Employees emp)
        {
            var employeeID = await _empRespository.AddEmployee(emp);
            emp.EmployeeID = employeeID;
            return Json(emp);

        }

        // PUT api/values/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody]Employees emp)
        {
            var result = await _empRespository.UpdateEmployee(emp);
            if (result)
                return Json(new { ActionCode = 200, Status = "success" });
            else
                return Json(new { ActionCode = 500, Status = "Update Failed" });
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _empRespository.DeleteEmployee(id);
            if (result)
                return Json(new { ActionCode = 200, Status = "success" });
            else
                return Json(new { ActionCode = 500, Status = "Update Failed" });
        }
    }
}
