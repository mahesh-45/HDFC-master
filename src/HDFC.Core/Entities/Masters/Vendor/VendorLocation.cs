using System;
using HDFC.Core.Enums;
using HDFC.Core.SharedKernel;

namespace HDFC.Core.Entities.Masters.Vendor
{
    public class VendorLocation : BaseEntity
    {
        public string AddressLine1 { get; private set; }
        public string AddressLine2 { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string Country { get; private set; }
        public string ZipCode { get; private set; }
        public string TaxCode { get; private set; }
        public int LocationTypeId { get; private set; }   //master
        public int? RegionId { get; private set; }     //master
        public long VendorId { get; private set; }
        public Vendor Vendor { get; private set; }

        private VendorLocation()
        { }

        public VendorLocation(int locationTypeId, int regionId, string city, string state, string country)
        {
            LocationTypeId = locationTypeId;
            RegionId = regionId;
            City = city;
            State = state;
            Country = country;
        }

        public void SetAddress(string addressline1, string addressline2, string zipCode, string taxCode)
        {
            AddressLine1 = addressline1;
            AddressLine2 = addressline2;
            ZipCode = zipCode;
            TaxCode = taxCode;
        }

        public void Update(int locationTypeId, int regionId, string city, string state, string country)
        {
            LocationTypeId = locationTypeId;
            RegionId = regionId;
            City = city;
            State = state;
            Country = country;
        }
    }
}
