using System;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;

namespace PacificCoral.Model
{
    public class DeviationDetails 
    {

        string id;
        string opco, representative;
        string deviationNumber, itemCode, discountType;
        double? pcsPrice, syscoPrice, qtyApplied, amountApplied, qpc, pcsPriceLBS, syscoPriceLBS, fobPrice, discounts, publishedPrice, maxProgramMoney;
        int? estQty;

        [JsonProperty(PropertyName = "id")]
        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        [Required]
        [StringLength(9)]
        public string DeviationNumber { get { return deviationNumber; } set { deviationNumber = value; } }

        [Required]
        [StringLength(7)]
        public string ItemCode { get { return itemCode; } set { itemCode = value; } }

        public double? PCSPrice { get { return pcsPrice; } set { pcsPrice = value; } }

        [StringLength(2)]
        public string DiscountType { get { return discountType; } set { discountType = value; } }

        public double? SyscoPrice { get { return syscoPrice; } set { syscoPrice = value; } }

        public int? EstQTY { get { return estQty; } set { estQty = value; } }

        public double? QtyApplied { get { return qtyApplied; } set { qtyApplied = value; } }

        public double? AmountApplied { get { return amountApplied; } set { amountApplied = value; } }

        public double? QPC { get { return qpc; } set { qpc = value; } }

        public double? PCSPriceLBS { get { return pcsPriceLBS; } set { pcsPriceLBS = value; } }

        public double? SyscoPriceLBS { get { return syscoPriceLBS; } set { syscoPriceLBS = value; } }

        public double? FOBPrice { get { return fobPrice; } set { fobPrice = value; } }

        public double? Discounts { get { return discounts; } set { discounts = value; } }

        public double? PublishedPrice { get { return publishedPrice; } set { publishedPrice = value; } }

        public double? MaxProgramMoney { get { return maxProgramMoney; } set { maxProgramMoney = value; } }


        [Version]
        public string Version { get; set; }
    }
}