using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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

        public Staff AddStaff(Staff staff)
        {
            EntityEntry<Staff> e = _dbContext.Staff.Add(staff);
            Staff s = e.Entity;
            _dbContext.SaveChanges();
            return s;
        }

        public void DeleteStaff(int id)
        {
            Staff Staff = _dbContext.Staff.FirstOrDefault(e => e.Id == id);
            if (Staff != null)
            {
                _dbContext.Staff.Remove(Staff);
                _dbContext.SaveChanges();
            }
        }

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


        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}