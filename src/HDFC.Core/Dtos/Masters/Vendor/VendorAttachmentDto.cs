using System;
using System.Collections.Generic;
using System.Text;

namespace HDFC.Core.Dtos.Masters.Vendor
{
    public class VendorAttachmentDto: BaseEntityDto
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public decimal FileSize { get; set; }
        public DateTime UploadedDate { get; set; }

    }
}
