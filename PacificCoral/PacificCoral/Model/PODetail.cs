using System;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;

namespace PacificCoral.Model

{
    public class PODetail 
    {

        string id;
        string itemCode, description, status, pack;
        int masterKey, _key;
        double? quantityOrdered, quantityFilled, price, priceFilled, qpc;


        [JsonProperty(PropertyName = "id")]
        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        [Required]
        public int MasterKey { get { return masterKey; } set { masterKey = value; } }
        [StringLength(50)]
        [Required]
        public string ItemCode { get { return itemCode; } set { itemCode = value; } }
        [StringLength(255)]
        [Required]
        public string Description { get { return description; } set { description = value; } }

        public double? QuantityOrdered { get { return quantityOrdered; } set { quantityOrdered = value; } }

        public double? QuantityFilled { get { return quantityFilled; } set { quantityFilled = value; } }

        public double? Price { get { return price; } set { price = value; } }

        public double? PriceFilled { get { return priceFilled; } set { priceFilled = value; } }
        [StringLength(255)]
        [Required]
        public string Status { get { return status; } set { status = value; } }
        [Required]
        public int key { get { return _key; } set { _key = value; } }
        public string Pack { get { return pack; } set { pack = value; } }
        public double? QPC { get { return qpc; } set { qpc = value; } }

        [Version]
        public string Version { get; set; }
    }
}
