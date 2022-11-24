using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    [DataContract]
    public class Ranks
    {
        [DataMember]
        public int idRank { get; set; }
        [DataMember]
        public string rankName { get; set; }
        [DataMember]
        public int rankPoints { get; set; }
    }
}
