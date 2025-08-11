using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Helpers
{
    public class QueryObject
    {
        public string? Symbol { get; set; } = null;
        public string? CompanyName { get; set; } = null; // since it is nullable, it means that its basically optional
        public string? SortBy { get; set; } = null; // default value is null, conditionally renders, powerful

        public bool IsDecending { get; set; } = false; // not optional, descedning will automatically be the choice

        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 5;
    }
}