using System;
using System.Collections.Generic;
using System.Text;

#region additional namespaces
using MauiAppDemo.Models;
#endregion

namespace MauiAppDemo.Services
{
    public interface IProductServices
    {
        /*
         * an interface is a collection of rules (promised methods)
         * it does NOT implement the method itself
         */
        IEnumerable<Product> GetProducts();
        Product? GetProductById(int id);
    }
}
