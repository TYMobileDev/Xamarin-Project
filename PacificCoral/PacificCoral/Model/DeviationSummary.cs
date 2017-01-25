using System;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;

namespace PacificCoral.Model
{
    public class DeviationSummary 
    {
        string id;
        string opco, representative;
        int active, expiring, expired, submitted;

        [JsonProperty(PropertyName = "id")]
        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        [StringLength(255)]
        public string OPCO { get { return opco; } set { opco = value; } }
        public int Active { get { return active; } set { active = value; } }
        public int Expiring { get { return expiring; } set { expiring = value; } }
        public int Expired { get { return expired; } set { expired = value; } }
        public int Submitted { get { return submitted; } set { submitted = value; } }
        [StringLength(255)]
        public string Representative { get { return representative; } set { representative = value; } }

        [Version]
        public string Version { get; set; }
    }
}
