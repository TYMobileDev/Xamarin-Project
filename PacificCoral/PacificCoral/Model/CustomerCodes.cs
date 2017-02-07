using System;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace PacificCoral.Model
{
    public class CustomerCodes
    {
        string id, customerCode, masterID;
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

        [Version]
        public string Version { get; set; }
    }
}