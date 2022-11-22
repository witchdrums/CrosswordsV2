using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic;

namespace Domain
{
    [DataContract]
    public class Users
    {

        public Users()
        {
            
        }
        [DataMember]
        public int idUser { get; set; }
        [DataMember]
        public string username { get; set; }
        [DataMember]
        public string email { get; set; }
        [DataMember]
        public string password { get; set; }
        [DataMember]
        public bool credential { get; set; }

        [DataMember]
        public int idUserType { get; set; }

    }
}
