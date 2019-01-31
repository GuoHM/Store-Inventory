using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using InventoryBusinessLogic;
using InventoryBusinessLogic.Entity;
using System.Web.Http.Cors;

namespace InventoryWebAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class DepartmentController : ApiController
    {
        DepartmentBusinessLogic dept = new DepartmentBusinessLogic();
        [HttpGet]
        [Route("api/Department")]

        public IEnumerable<Department> GetAllDepartments()
        {
            return dept.GetDepartments();
        }

        [HttpGet]
        [Route("api/Department/{id}")]

        public IEnumerable<Department> GetAllDepartmentByID(string id)
        {
            return dept.GetDepartmentByID(id);
        }

    }
}
