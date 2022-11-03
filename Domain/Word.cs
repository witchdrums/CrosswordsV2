using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    [DataContract]
    public class Word
    {
        public Word()
        { 
        }
        [DataMember]
        public string term { get; set; }

        [DataMember]
        public string clue { get; set; }
        [DataMember]
        public int xStart { get; set; }
        [DataMember]
        public int xEnd { get; set; }
        [DataMember]
        public int yStart { get; set; }
        [DataMember]
        public int yEnd { get; set; }
    }
}
