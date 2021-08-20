using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Globalization;
using A1.Models;

namespace A1.Data
{
    public class DBWebAPIRepo : IWebAPIRepo
    {
        private readonly WebAPIDBContext _dbContext;

        public DBWebAPIRepo(WebAPIDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        /*Staff Functions*/
        public IEnumerable<Staff> GetAllStaffs()
        {
            IEnumerable<Staff> Staffs = _dbContext.Staff.ToList<Staff>();
            return Staffs;
        }

        public Staff GetStaffByID(int id)
        {
            Staff Staff = _dbContext.Staff.FirstOrDefault(e => e.Id == id);
            return Staff;
        }

        /*Product Functions*/

        public IEnumerable<Product> GetAllProducts()
        {
            IEnumerable<Product> Products = _dbContext.Product.ToList<Product>();
            return Products;
        }

        public IEnumerable<Product> GetProductByString(string letter)
        {
            IEnumerable<Product> Products = _dbContext.Product.ToList<Product>();
            IEnumerable<Product> Product = Products.Where(e => e.Name.ToLower().Contains(letter));
            return Product;
        }

        /*Comment Functions*/
        public IEnumerable<SiteComments> GetAllComments()
        {
            IEnumerable<SiteComments> Comments = _dbContext.Comments.ToList<SiteComments>();
            return Comments;
        }

        public SiteComments AddComment(SiteComments comment)
        {
            EntityEntry<SiteComments> e = _dbContext.Comments.Add(comment);
            SiteComments s = e.Entity;
            _dbContext.SaveChanges();
            return s;
        }


        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}