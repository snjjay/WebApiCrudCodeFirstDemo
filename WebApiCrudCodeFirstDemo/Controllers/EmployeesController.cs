using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApiCrudCodeFirstDemo.Data;
using WebApiCrudCodeFirstDemo.Models;

namespace WebApiCrudCodeFirstDemo.Controllers
{

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class EmployeesController : ApiController
    {
        public HttpResponseMessage Get()
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                return Request.CreateResponse(HttpStatusCode.OK, dbContext.Employees.ToList());
            }
        }

        public HttpResponseMessage Get(int id)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                var emp = dbContext.Employees.FirstOrDefault(e => e.Id == id);
                if (emp != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, emp);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with Id " + id + " not found in database");
                }
            }
        }

        public HttpResponseMessage Post(Employee employee)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                if (employee != null)
                {
                    dbContext.Employees.Add(employee);
                    dbContext.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.Created, employee);
                }
                else 
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Please provide proper input data to create an employee");
                }
            }
        }

        public HttpResponseMessage Put(int id, Employee employee)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                var emp = dbContext.Employees.FirstOrDefault(e => e.Id == id);
                if (emp != null)
                {
                    emp.FirstName = employee.FirstName;
                    emp.LastName = employee.LastName;
                    emp.Gender = employee.Gender;
                    emp.City = employee.City;
                    emp.IsActive = employee.IsActive;
                    dbContext.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, emp);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with Id " + id + " not found in database. Update failed.");
                }
            }
        }

        public HttpResponseMessage Patch(int id, [FromBody]string FistName) 
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                var emp = dbContext.Employees.FirstOrDefault(e => e.Id == id);
                if (emp != null)
                {
                    emp.FirstName = FistName;
                   
                    dbContext.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, emp);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with Id " + id + " not found in database. Update failed.");
                }
            }
        }



        public HttpResponseMessage Delete(int id)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                var emp = dbContext.Employees.FirstOrDefault(e => e.Id == id);
                if (emp != null)
                {
                    dbContext.Employees.Remove(emp);
                    dbContext.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, emp);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with Id " + id + " not found in database. Delete failed");
                }

            }
        }


        //// Disabled  authorization to following methods only 

        //ApplicationDbContext dbContext = new ApplicationDbContext();
        //[Authorize(Roles = ("User"))]
        //public HttpResponseMessage GetEmployeeById(int id)
        //{
        //    var user = dbContext.Employees.FirstOrDefault(e => e.Id == id);
        //    return Request.CreateResponse(HttpStatusCode.OK, user);
        //}

        //[Authorize(Roles = ("Admin, SuperAdmin"))]
        //[Route("api/Employee/GetSomeEmployees")]
        //public HttpResponseMessage GetSomeEmployees()
        //{
        //    var user = dbContext.Employees.Where(e => e.Id <= 10);
        //    return Request.CreateResponse(HttpStatusCode.OK, user);
        //}

        //[Authorize(Roles = ("SuperAdmin"))]
        //[Route("api/Employee/GetEmployees")]
        //public HttpResponseMessage GetEmployees()
        //{
        //    var user = dbContext.Employees.ToList();
        //    return Request.CreateResponse(HttpStatusCode.OK, user);
        //}



    }
}
