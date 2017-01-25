
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;

namespace PacificCoral.Model
{
    public class OpcoSalesSummaries 
    {
        string id, opco;
        int period;
        double lbs, value;

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

        [JsonProperty(PropertyName = "period")]
        public int Period
        {
            get { return period; }
            set { period = value; }
        }

        [JsonProperty(PropertyName = "lbs")]
        public double LBS
        {
            get { return lbs; }
            set { lbs = value; }
        }

        [JsonProperty(PropertyName = "value")]
        public double Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        [Version]
        public string Version { get; set; }
    }
}
