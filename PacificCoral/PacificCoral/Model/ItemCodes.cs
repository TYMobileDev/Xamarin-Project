using System;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;

namespace PacificCoral.Model

{
    public class ItemCodes 
    {
        string id;
        string itemCode, description, pack, brand;


        [JsonProperty(PropertyName = "id")]
        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        [Required]
        [StringLength(10)]
        public string ItemCode { get { return itemCode; } set { itemCode = value; } }
        [StringLength(50)]
        public string Description { get { return description; } set { description = value; } }
        [StringLength(30)]
        public string Pack { get { return pack; } set { pack = value; } }
        [StringLength(50)]
        public string Brand { get { return brand; } set { brand = value; } }
        [Version]
        public string Version { get; set; }
    }
}