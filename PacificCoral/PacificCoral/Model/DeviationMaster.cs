using System;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;

namespace PacificCoral.Model
{
    public class DeviationMaster 
    {

        string id;
        string  deviationNumber, syscoHouse,maName,memo, customer, submittedBy, syscoCustomerName ;
        DateTime startDate, endDate, sendDate;
        int? sendStatus;

        [JsonProperty(PropertyName = "id")]
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        [StringLength(9)]
        public string DeviationNumber { get { return deviationNumber; } set { deviationNumber = value; } }

        [StringLength(50)]
        public string SyscoHouse { get { return syscoHouse; } set { syscoHouse = value; } }

        [StringLength(50)]
        public string MAName { get { return maName; } set { maName = value; } }

        [StringLength(50)]
        public string SyscoCustomerName { get { return syscoCustomerName; } set { syscoCustomerName = value; } }

        public DateTime StartDate { get { return startDate; } set { startDate = value; } }

        public DateTime EndDate { get { return endDate; } set { endDate = value; } }


        public string Memo { get { return memo; } set { memo = value; } }


        [StringLength(50)]
        public string Customer { get { return customer; } set { customer = value; } }

        public DateTime SendDate { get { return sendDate; } set { sendDate = value; } }

        public int? SendStatus { get { return sendStatus; } set { sendStatus = value; } }

        [StringLength(255)]
        public string SubmittedBy { get { return submittedBy; } set { submittedBy = value; } }

        [Version]
        public string Version { get; set; }
    }
}

