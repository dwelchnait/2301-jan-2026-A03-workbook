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
    public class CategoryServices
    {
        #region setup constructor and private local context variable
        private readonly WestWindContext _context;

        public CategoryServices(WestWindContext context)
        {
            _context = context;
        }
        #endregion

        #region Queries
        public async Task<List<Category>> Category_GetAll()
        {
            return await _context.Categories
                                .OrderBy(x => x.CategoryName)
                                .ToListAsync();
        }
        #endregion
    }
}
