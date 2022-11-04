using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public partial class Board
    {

        public Board()
        {
            this.WordsBoards = new ObservableCollection<Domain.WordsBoard>();
        }

        public int idBoard { get; set; }
        public string boardMatrix { get; set; }


        public ObservableCollection<Domain.WordsBoard> WordsBoards { get; set; }
    }
}
