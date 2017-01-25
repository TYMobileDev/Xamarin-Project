using System;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;

namespace PacificCoral.Model
{
    public class LostSalesPCS
    {
        string id, opco, itemCode, description;
        DateTime period1BeginDate, period1EndDate, period2BeginDate, period2EndDate;
        double gainLoss, period1Quantity, period2Quantity;

        [JsonProperty(PropertyName = "id")]
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        [JsonProperty(PropertyName = "opco")]
        public string OPCO
        {
            get { return opco; }
            set { opco = value; }
        }

        [JsonProperty(PropertyName = "itemcode")]
        public string ItemCode
        {
            get { return itemCode; }
            set { itemCode = value; }
        }
        [JsonProperty(PropertyName = "description")]
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        [JsonProperty(PropertyName = "period1begindate")]
        public DateTime Period1BeginDate
        {
            get { return period1BeginDate; }
            set { period1BeginDate = value; }
        }
        [JsonProperty(PropertyName = "period1enddate")]
        public DateTime Period1EndDate
        {
            get { return period1EndDate; }
            set { period1EndDate = value; }
        }
        [JsonProperty(PropertyName = "period2begindate")]
        public DateTime Period2BeginDate
        {
            get { return period2BeginDate; }
            set { period2BeginDate = value; }
        }
        [JsonProperty(PropertyName = "period2enddate")]
        public DateTime Period2EndDate
        {
            get { return period2EndDate; }
            set { period2EndDate = value; }
        }


        [JsonProperty(PropertyName = "period1quantity")]
        public double Period1Quantity
        {
            get { return period1Quantity ; }
            set { period1Quantity = value; }
        }
        [JsonProperty(PropertyName = "period2quantity")]
        public double Period2Quantity
        {
            get { return period2Quantity ; }
            set { period2Quantity  = value; }
        }

        [JsonProperty(PropertyName = "gainloss")]
        public double GainLoss
        {
            get { return gainLoss; }
            set { gainLoss = value; }
        }

        [Version]
        public string Version { get; set; }
    }
}
