using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    [DataContract]
    public class ProfileImages
    {
        [DataMember]
        public int idProfileImage { get; set; }
        [DataMember]
        public string profileImageName { get; set; }
    }
}
