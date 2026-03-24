using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

#region Additional Namespaces
using WestWindSystem.DAL;
using WestWindSystem.Models;
#endregion

namespace WestWindSystem.BLL
{
    public class ProductServices
    {
        #region setup constructor and private local context variable
        private readonly WestWindContext _context;

        public ProductServices(WestWindContext context)
        {
            _context = context;
        }
        #endregion

        #region Queries
        public async Task<List<Product>> Product_GetAll()
        {
            return await _context.Products
                                .Include(x => x.Category)
                                .Include(x => x.Supplier)
                                .OrderBy(x => x.ProductName)
                                .ToListAsync();
        }

        public async Task<List<Product>> Product_GetByCategoryID(int categoryid)
        {
            return await _context.Products
                                .Include(x => x.Supplier)
                                .Where(x => x.CategoryID ==  categoryid)
                                .OrderBy(x => x.ProductName)
                                .ToListAsync();
        }

        public async Task<Product> Product_GetByID(int productid)
        {
            return await _context.Products
                                .FirstOrDefaultAsync(x => x.ProductID ==  productid);

        }
        #endregion

        #region CRUD (Add, Update and Delete)
        public async Task<int> Product_Add(Product item)
        {
            throw new NotImplementedException();
        }
        public async Task<int> Product_Update(Product item)
        {
            throw new NotImplementedException();
        }
        public async Task<int> Product_LogicalDelete(Product item)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
