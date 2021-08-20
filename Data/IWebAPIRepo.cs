using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using A1.Models;

namespace A1.Data {
    public interface IWebAPIRepo
    {
        IEnumerable<Staff> GetAllStaffs();
        Staff GetStaffByID(int id);
        IEnumerable<Product> GetProductByString(String letter);
        IEnumerable<Product> GetAllProducts();
        IEnumerable<SiteComments> GetAllComments();
        SiteComments AddComment(SiteComments comment);
        void SaveChanges();
    }
}



