using System;
using System.Collections.Generic;
using System.Text;

namespace MauiAppDemo.Services
{
    public class MockDiscountServices : IDiscountServices
    {
        private const decimal DiscountBase = 0.10m; //10% off

        public decimal ApplyDiscount(decimal originalPrice)
        {
            return originalPrice * (1 - DiscountBase);
        }
    }
}
