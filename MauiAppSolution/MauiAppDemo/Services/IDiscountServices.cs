using System;
using System.Collections.Generic;
using System.Text;

namespace MauiAppDemo.Services
{
    public interface IDiscountServices
    {
        decimal ApplyDiscount(decimal originalPrice);
    }
}
