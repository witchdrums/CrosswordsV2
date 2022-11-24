using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic;

namespace Domain
{
    [DataContract]
    public class Players
    {
        public Players()
        {
            User = new Users();
            Rank = new Ranks();
            ProfileImage = new ProfileImages();
        }
        [DataMember]
        public int idPlayer { get; set; }
        [DataMember]
        public string playerName { get; set; }
        [DataMember]
        public string playerDescription { get; set; }
        [DataMember]
        public int playerLevel { get; set; }
        [DataMember]
        public string playerRank { get; set; }

        [DataMember]
        public int idRank { get; set; }

        [DataMember]
        public int idProfileImage { get; set; }

        [DataMember]
        public int gamesPlayed { get; set; }

        [DataMember]
        public int gamesWon { get; set; }

        [DataMember]
        public Domain.Users User { get; set; }

        [DataMember]
        public Domain.Ranks Rank { get; set; }

        [DataMember]
        public Domain.ProfileImages ProfileImage { get; set; }
    }
}
