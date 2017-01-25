using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;

namespace PacificCoral.Model
{
    public class RepOpcoMap
    {
        string id;
        string opco, representative;

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

        [JsonProperty(PropertyName = "representative")]
        public string Representative
        {
            get { return representative; }
            set { representative = value; }
        }

        [Version]
        public string Version { get; set; }
    }
}

