//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BusinessLogic
{
    using System;
    using System.Collections.ObjectModel;
    
    public partial class GamesPlayer
    {
        public int idPlayer { get; set; }
        public int idGame { get; set; }
        public int gameScore { get; set; }
        public int gameRank { get; set; }
    
        public virtual Game Game { get; set; }
        public virtual Player Player { get; set; }
    }
}
