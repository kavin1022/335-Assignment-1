
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using A1.Data;
using A1.Dtos;
using A1.Models;
using Microsoft.AspNetCore.Mvc;


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
        //Endpoint 1
        [HttpGet("GetLogo")]
        public ActionResult GetLogo() {
            string path = Directory.GetCurrentDirectory();
            string imgDir = Path.Combine(path, "StaffPhotos");
            string fileName = Path.Combine(imgDir, "logo" + ".png");
            string respHeader = "image/png";

            return PhysicalFile(fileName, respHeader);
        }

        //Endpoint 2
        [HttpGet("GetVersion")]
        public ActionResult GetVersion(){
            string version = "<p>v1</p>";
            ContentResult c = new ContentResult
            {
                Content = version,
                ContentType = "text/html",
                StatusCode = (int)HttpStatusCode.OK,
            };
            return c;
        }

        //Endpoint 3
        [HttpGet("GetAllStaff")]
        public ActionResult<IEnumerable<StaffOutDto>> GetAllStaff()
        {
            IEnumerable<Staff> staffs = _repository.GetAllStaffs();
            IEnumerable<StaffOutDto> c = staffs.Select(e => new StaffOutDto { Id = e.Id, FirstName = e.FirstName, LastName = e.LastName, Title = e.Title, Email = e.Email, Tel = e.Tel, Url = e.Url, Research = e.Research });
            return Ok(c);
        }

        //Endpoint 4
        [HttpGet("GetStaffPhoto/{id}")]
        public ActionResult GetStaffPhoto(string id)
        {
            string path = Directory.GetCurrentDirectory();
            string imgDir = Path.Combine(path, "StaffPhotos");

            string staffPhoto = Path.Combine(imgDir, id + ".jpg");
            string notFound = Path.Combine(imgDir, "default" + ".png");
            string respHeader = "";
            string fileName = "";
            if (System.IO.File.Exists(staffPhoto))
            {
                respHeader = "image/jpg";
                fileName = staffPhoto;
            }
            else {
                respHeader = "image/png";
                fileName = notFound;
            }
            return PhysicalFile(fileName, respHeader);

        }
    }
}
