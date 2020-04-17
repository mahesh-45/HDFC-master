using System;
using System.Collections.Generic;
using System.Text;

namespace HDFC.Core.Dtos.Common
{
    public class SearchDto
    {
        public string Sort { get; set; }
        public string Order { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string Search { get; set; }
    }
}
