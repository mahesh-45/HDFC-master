using System;
using HDFC.Core.SharedKernel;

namespace HDFC.Core.Entities.Masters.Vendor
{
    public class VendorAttachment : BaseEntity
    {
        private VendorAttachment() { }

        public VendorAttachment(string fileName, string filePath, decimal fileSize, DateTime uploadedDate)
        {
            FileName = fileName;
            FilePath = filePath;
            FileSize = fileSize;
            UploadedDate = uploadedDate;
        }

        public string FileName { get; private set; }
        public string FilePath { get; private set; }
        public decimal FileSize { get; private set; }
        public DateTime UploadedDate { get; private set; }

        public long VendorId { get; private set; }
        public Vendor Vendor { get; private set; }

        
   

        public void Update(string fileName, string filePath, decimal fileSize, DateTime uploadedDate)
        {
           
            FileName = fileName;
            FilePath = filePath;
            FileSize = fileSize;
            UploadedDate = uploadedDate;
        }
    }
}
