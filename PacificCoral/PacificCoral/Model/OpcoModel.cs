using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PacificCoral.Model
{
    class OpcoModel
    {
        private string _opco;

        [JsonProperty(PropertyName = "opco")]
        public string OPCO
        {
            get
            {
                return _opco;
            }

            set
            {
                _opco = value;
            }
        }
    }
}
