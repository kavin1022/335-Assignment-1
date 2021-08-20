
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using A1.Data;
using A1.Dtos;
using A1.Models;
using A1.Helper;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;


namespace A1.Controllers
{
    [Route("api")]
    [ApiController]
    public class StaffsController : Controller
    {
        private readonly IWebAPIRepo _repository;

        public StaffsController(IWebAPIRepo repository)
        {
            _repository = repository;
        }

        /*[HttpPost("AddStaff")]
        public ActionResult<StaffOutDto> AddStaff(StaffInputDto staff)
        {
            Staff c = new Staff { FirstName = staff.FirstName, LastName = staff.LastName, Email = staff.Email };
            Staff addedStaff = _repository.AddStaff(c);
            StaffOutDto co = new StaffOutDto { Id = addedStaff.Id, FirstName = addedStaff.FirstName, LastName = addedStaff.LastName };
            return CreatedAtAction(nameof(GetStaff), new { id = co.Id }, co);
        }*/

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

        //Endpoint 5
        [HttpGet("GetCard/{id}")]
        public ActionResult GetCard(int id)
        {
            Staff staff = _repository.GetStaffByID(id);
            string path = Directory.GetCurrentDirectory();
            string fileName = Path.Combine(path, "StaffPhotos/" + id + ".jpg");
            string photoString, photoType;
            ImageFormat imageFormat;
            
            //Logo settings
            string logoFileName = Path.Combine(path, "StaffPhotos/" + "logo" + ".png");
            string logoString, logoPhotoType;
            ImageFormat logoImageFormat;
            Image logo = Image.FromFile(logoFileName);
            logoImageFormat = logo.RawFormat;
            logo = ImageHelper.Resize(logo, new Size(100, 100), out logoPhotoType);
            logoString = ImageHelper.ImageToString(logo, logoImageFormat);

            CardOut cardOut = new CardOut();
            cardOut.Logo = logoString;
            cardOut.LogoPhotoType = logoPhotoType;

            if (System.IO.File.Exists(fileName))
            {
                Image image = Image.FromFile(fileName);
                imageFormat = image.RawFormat;
                image = ImageHelper.Resize(image, new Size(100, 100), out photoType);
                photoString = ImageHelper.ImageToString(image, imageFormat);

                cardOut.Title = staff.Title;
                cardOut.FirstName = staff.FirstName;
                cardOut.LastName = staff.LastName;
                cardOut.Uid = staff.Id;
                cardOut.Email = staff.Email;
                cardOut.Categories = staff.Research;
                cardOut.Photo = photoString;
                cardOut.PhotoType = photoType;
                cardOut.Categories = Helper.ResearchFilter.Filter(staff.Research);
                cardOut.Tel = staff.Tel;
                cardOut.Url = staff.Url;
                cardOut.Org = "Southern Hemisphere Institue of Technology";
            }

            Response.Headers.Add("Content-Type", "text/vcard");

            return Ok(cardOut);
        }

        //Endpoint 6
        [HttpGet("GetItems")]
        [HttpGet("GetItems/{name}")]
        public ActionResult<IEnumerable<ProductOutDto>> GetItems(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                IEnumerable<Product> Products = _repository.GetAllProducts();
                IEnumerable<ProductOutDto> c = Products.Select(e => new ProductOutDto { Id = e.Id, Name = e.Name, Description = e.Description, Price = e.Price });
                return Ok(c);
            }
            else {
                IEnumerable<Product> Products = _repository.GetProductByString(name);
                IEnumerable<ProductOutDto> c = Products.Select(e => new ProductOutDto { Id = e.Id, Name = e.Name, Description = e.Description, Price = e.Price });
                return Ok(c);
            }

        }

        //Endpoint 7
        [HttpGet("GetItemPhoto/{id}")]
        public ActionResult GetItemPhoto(string id)
        {
            string path = Directory.GetCurrentDirectory();
            string imgDir = Path.Combine(path, "ItemsImages");

            string productPhoto = Path.Combine(imgDir, id + ".jpg");
            string productPhoto2 = Path.Combine(imgDir, id + ".png");
            string notFound = Path.Combine(imgDir, "default" + ".png");
            string respHeader = "";
            string fileName = "";
            if (System.IO.File.Exists(productPhoto))
            {
                respHeader = "image/jpg";
                fileName = productPhoto;
            }
            else if (System.IO.File.Exists(productPhoto2)) {
                respHeader = "image/png";
                fileName = productPhoto2;
            }
            else
            {
                respHeader = "image/png";
                fileName = notFound;
            }
            return PhysicalFile(fileName, respHeader);
        }

    }
}
