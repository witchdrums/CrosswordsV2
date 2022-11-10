
using System.Runtime.Serialization;


namespace Domain
{
    [DataContract]
    public class GamesPlayers
    {
        public GamesPlayers() { }

        [DataMember]
        public int idPlayer { get; set; }
        [DataMember]
        public int idGame { get; set; }
        [DataMember]
        public int gameScore { get; set; }
        [DataMember]
        public int gameRank { get; set; }
        [DataMember]
        public Games Game { get; set; }
        [DataMember]
        public Domain.Players Player { get; set; }
    }
}
