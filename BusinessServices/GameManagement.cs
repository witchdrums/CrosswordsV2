using BusinessLogic;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices
{
    public class GameManagement
    {
        public int RegisterGame(Games game)
        {
            int insertedIdGame = 0;
            if (game != null
                && game.gameDate.Date == DateTime.Now.Date)
            {
                using (var context = new CrosswordsContext())
                {
                    Game businessGame = new Game();
                    businessGame.gameDate = game.gameDate;
                    businessGame.gameTime = game.gameTime;
                    context.Games.Add(businessGame);
                    context.SaveChanges();
                    insertedIdGame = businessGame.idGame;
                }
            }
            return insertedIdGame;
        }
    }
}
