using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    [DataContract]
    public class Reports
    {
        [DataMember]
        public int idReport { get; set; }
        [DataMember]
        public int idUser { get; set; }
        [DataMember]
        public int idCategory { get; set; }
        [DataMember]
        public string chatLog { get; set; }
        [DataMember]
        public virtual Categories Category { get; set; }
        [DataMember]
        public virtual Users User { get; set; }
    }
}
