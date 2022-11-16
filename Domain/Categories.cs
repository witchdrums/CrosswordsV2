using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    [DataContract]
    public class Categories
    {
        public Categories()
        {
            this.Reports = new ObservableCollection<Reports>();
        }
        [DataMember]
        public int idCategory { get; set; }
        [DataMember]
        public string description { get; set; }
        [DataMember]
        public virtual ObservableCollection<Reports> Reports { get; set; }
    }
}
