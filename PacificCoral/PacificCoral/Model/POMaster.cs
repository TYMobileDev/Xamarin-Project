using System;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;

namespace PacificCoral.Model

{
    public class POMaster 
    {


        string id;
        string opco, po, status, deliveryType;
        int _key;
        DateTime shipDate, poDate, deliveryDate;
        int? pickingNumber;
        double? freight, totalLBS, totalValue;

        [JsonProperty(PropertyName = "id")]
        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        [StringLength(255)]
        [Required]
        public string OPCO { get { return opco; } set { opco = value; } }
        [Required]
        public string PO { get { return po; } set { po = value; } }
        [Required]
        public int key { get { return _key; } set { _key = value; } }
        [Required]
        public DateTime ShipDate { get { return shipDate; } set { shipDate = value; } }
        [Required]
        public DateTime PODate { get { return poDate; } set { poDate = value; } }
        [Required]
        public DateTime DeliveryDate { get { return deliveryDate; } set { deliveryDate = value; } }
        [StringLength(255)]
        [Required]
        public string Status { get
            { return status; } set
            { status = value; } }
        public int? PickingNumber { get { return pickingNumber; } set { pickingNumber = value; } }
        public string DeliveryType{ get
            { return deliveryType; } set
            { deliveryType = value; } }
        public double? Freight { get { return freight; } set { freight = value; } }
        public double? TotalLBS { get { return totalLBS; } set { totalLBS = value; } }
        public double? TotalValue { get { return totalValue; } set { totalValue = value; } }

        [Version]
        public string Version { get; set; }
    }
}
