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
        #endregion
    }
}
