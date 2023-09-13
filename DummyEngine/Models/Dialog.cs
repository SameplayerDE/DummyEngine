using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DummyEngine.Models
{
    public class Dialog
    {
        public string ID { get; set; }
        public string Content { get; set; }
        public string SpeakerID { get; set; }
        [JsonIgnore]
        public Character Speaker { get; set; }
    }
}
