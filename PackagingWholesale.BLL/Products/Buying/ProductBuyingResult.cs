using System;
using System.Collections.Generic;

namespace PackagingWholesale.BLL.Products.Buying
{
    public class ProductBuyingResult
    {
        public string FirstName { get; internal set; }
        public string LastName { get; internal set; }
        public string Email { get; internal set; }
        public DateTime Date { get; internal set; }
        public bool IsStatusOk { get; set; }
        public List<string> Errors { get; set; }
    }
}