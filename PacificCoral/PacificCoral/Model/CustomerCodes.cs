using System;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace PacificCoral.Model
{
    public class CustomerCodes
    {
        string id, customerCode, masterID, address, city,state,zip,telephone,email, contactName, contactPosition;
        int customerNumber, sqlKey;

        [JsonProperty(PropertyName = "id")]
        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        [Required]
        public int CustomerNumber { get { return customerNumber; } set { customerNumber = value; } }
        [Required]
        [StringLength(10)]
        public string CustomerCode { get { return customerCode; } set { customerCode = value; } }
        public int SQLKey { get { return sqlKey; } set { sqlKey = value; } }  // this is the sysco ID column
        [StringLength(128)]
        public string MasterID { get { return masterID; } set { masterID = value; } }  // this is the ID col from Mobile.Customers
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
        [StringLength(255)]
        public string ContactName { get { return contactName; } set { contactName = value; } }
        [StringLength(255)]
        public string ContactPosition { get { return contactPosition; } set { contactPosition = value; } }

        [Version]
        public string Version { get; set; }
    }
}