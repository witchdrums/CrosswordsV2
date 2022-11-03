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
    public class Word
    {
        public Word()
        {
            this.WordsBoards = new ObservableCollection<WordsBoard>();
        }
        [DataMember]
        public string term { get; set; }

        [DataMember]
        public string clue { get; set; }

        [DataMember]
        public bool isSolved { get; set; }

        [DataMember]
        public  ObservableCollection<WordsBoard> WordsBoards { get; set; }



    }
}
