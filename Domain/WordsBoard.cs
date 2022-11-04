using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    [DataContract]
    public class WordsBoard
    {
        [DataMember]
        public int idWord { get; set; }
        [DataMember]
        public int idBoard { get; set; }
        [DataMember]
        public byte xStart { get; set; }
        [DataMember]
        public byte xEnd { get; set; }
        [DataMember]
        public byte yStart { get; set; }
        [DataMember]
        public byte yEnd { get; set; }
        [DataMember]
        public virtual Domain.Board Board { get; set; }
        [DataMember]
        public virtual Domain.Word Word { get; set; }

    }
}
