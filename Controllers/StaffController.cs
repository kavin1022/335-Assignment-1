using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using A1.Models;
using A1.Data;
using A1.Dtos;

namespace A1.Controllers
{
    [Route("webapi")]
    [ApiController]
    public class StaffsController : Controller
    {
        private readonly IWebAPIRepo _repository;

        public StaffsController(IWebAPIRepo repository)
        {
            _repository = repository;
        }

        // GET /webapi/GetStaffs
        [HttpGet("GetStaffs")]
        public ActionResult<IEnumerable<StaffOutDto>> GetStaffs()
        {
            IEnumerable<Staff> staffs = _repository.GetAllStaffs();
            IEnumerable<StaffOutDto> c = staffs.Select(e => new StaffOutDto { Id = e.Id, FirstName = e.FirstName, LastName = e.LastName });
            return Ok(c);
        }

        // GET /webapi/GetStaff/{ID}
        [HttpGet("GetStaff/{ID}")]
        public ActionResult<StaffOutDto> GetStaff(int id)
        {
            Staff staff = _repository.GetStaffByID(id);
            if (staff == null)
                return NotFound();
            else
            {
                StaffOutDto c = new StaffOutDto { Id = staff.Id, FirstName = staff.FirstName, LastName = staff.LastName };
                return Ok(c);
            }

        }

        [HttpPost("AddStaff")]
        public ActionResult<StaffOutDto> AddStaff(StaffInputDto staff)
        {
            Staff c = new Staff { FirstName = staff.FirstName, LastName = staff.LastName, Email = staff.Email };
            Staff addedStaff = _repository.AddStaff(c);
            StaffOutDto co = new StaffOutDto { Id = addedStaff.Id, FirstName = addedStaff.FirstName, LastName = addedStaff.LastName };
            return CreatedAtAction(nameof(GetStaff), new { id = co.Id }, co);
        }

        // PUT /webapi/UpdateStaff/{id}
        [HttpPut("UpdateStaff/{id}")]
        public ActionResult UpdateStaff(int id, StaffInputDto staff)
        {
            Staff c = _repository.GetStaffByID(id);
            if (c == null)
                return NotFound();
            else
            {
                c.FirstName = staff.FirstName;
                c.LastName = staff.LastName;
                c.Email = staff.Email;
                _repository.SaveChanges();
                return NoContent();
            }
        }

        // DELETE /webapi/DeleteStaff/{id}
        [HttpDelete("DeleteStaff/{id}")]
        public ActionResult DeleteStaff(int id)
        {
            Staff c = _repository.GetStaffByID(id);
            if (c == null)
                return NotFound();
            else
            {
                _repository.DeleteStaff(id);
                return NoContent();
            }
        }
    }
}
