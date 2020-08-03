﻿using AutoMapper;
using EmployeeManagement.Data;
using EmployeeManagement.Data.Repository;
using EmployeeManagement.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace EmployeeManagement.Controllers
{
    public class EmployeeController : ApiController
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<IHttpActionResult> Get()
        {
            try
            {
                var result = await _employeeRepository.GetEmployees();
                var data = _mapper.Map<IEnumerable<EmployeeModel>>(result);
                return Ok(data);
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route(Name = "GetEmployee")]
        public async Task<IHttpActionResult> Get(int employeeId)
        {
            try
            {
                if (employeeId < 1)
                {
                    ModelState.AddModelError("EmployeeId", "Please provide employee Id");
                }
                if (ModelState.IsValid)
                {
                    var result = await _employeeRepository.GetEmployee(employeeId);
                    if (result == null) return NotFound();
                    return Ok(_mapper.Map<EmployeeModel>(result));
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        
        [HttpPost]
        [Route(Name = "AddEmployee")]
        public async Task<IHttpActionResult> PostAsync(EmployeeModel model)
        {
            try
            {
                if (model.FirstName.Equals(model.LastName))
                {
                    ModelState.AddModelError("Name", "First name and Last name can not be same");
                }
                if (ModelState.IsValid)
                {
                    var result = await _employeeRepository.CreateEmployee(_mapper.Map<Employee>(model));
                    if(result)
                        return CreatedAtRoute("GetEmployee", new { employeeId = model.EmployeeId }, model);
                    else
                        return InternalServerError();
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPut]
        public async Task<IHttpActionResult> PutAsync(EmployeeModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (await _employeeRepository.GetEmployee(model.EmployeeId) == null) return NotFound();
                    var result = await _employeeRepository.UpdateEmployee(_mapper.Map<Employee>(model));
                    if(result)
                        return Ok("Employee details updated");
                    else
                        return InternalServerError();
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpDelete]
        public async Task<IHttpActionResult> DeleteAsync(int employeeId)
        {
            try
            {
                if (employeeId < 1)
                {
                    ModelState.AddModelError("EmployeeId", "Please provide valid employee Id");
                }
                if (ModelState.IsValid)
                {
                    var employee = await _employeeRepository.GetEmployee(employeeId);
                    if (employee == null) return NotFound();
                    var result = await _employeeRepository.DeleteEmployee(_mapper.Map<Employee>(employee));
                    if(result)
                        return StatusCode(HttpStatusCode.NoContent); //OK("Employee record deleted successfully");
                    else
                        return InternalServerError();
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}