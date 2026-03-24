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
    public class SupplierServices
    {
        #region setup constructor and private local context variable
        private readonly WestWindContext _context;

        public SupplierServices(WestWindContext context)
        {
            _context = context;
        }
        #endregion

        #region Queries
        public async Task<List<Supplier>> Supplier_GetAll()
        {
            return await _context.Suppliers.ToListAsync();
        }
        #endregion
    }
}
