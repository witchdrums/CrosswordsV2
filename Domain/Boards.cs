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
    public partial class Boards
    {

        public Boards()
        {
            this.WordsBoards = new ObservableCollection<Domain.WordsBoard>();
        }

        [DataMember]
        public int idBoard { get; set; }
        [DataMember]
        public string boardMatrix { get; set; }

        [DataMember]
        public ObservableCollection<Domain.WordsBoard> WordsBoards { get; set; }
    }
}
