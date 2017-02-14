using System;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace PacificCoral.Model
{
    public class Customers
    {
        string id, customerName, opco, address, city, state, zip, telephone , email, primaryCustomerCode;
        int customerNumber;
        bool isProspect, hasDeviation;

        [JsonProperty(PropertyName = "id")]
        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        [Required]
        public int CustomerNumber { get { return customerNumber; } set { customerNumber = value; } }
        [Required]
        [StringLength(50)]
        public string CustomerName { get { return customerName; } set { customerName = value; } }
        [Required]
        [StringLength(50)]
        public string OPCO { get { return opco; } set { opco = value; } }
        [StringLength(250)]
        public string Address { get { return address; } set { address = value; } }
        [StringLength(50)]
        public string City { get { return city; } set { city = value; } }
        [StringLength(50)]
        public string State { get { return state; } set { state = value; } }
        [StringLength(50)]
        public string Zip { get { return zip; } set { zip = value; } }
        [StringLength(50)]
        public string Telephone { get { return telephone; } set { telephone = value; } }
        [StringLength(250)]
        public string Email { get { return email; } set { email = value; } }
        public bool IsProspect { get { return isProspect; } set { isProspect = value; } }
        public bool HasDeviation { get { return hasDeviation; } set { hasDeviation = value; } }

        [Version]
        public string Version { get; set; }

        public string PrimaryCustomerCode { get { return primaryCustomerCode; } set { primaryCustomerCode = value; } }

        public string NameSort
        {
            get
            {
                if (string.IsNullOrWhiteSpace(CustomerName) || CustomerName.Length == 0)
                    return "?";

                return CustomerName[0].ToString().ToUpper();
            }
        }
    }
}
