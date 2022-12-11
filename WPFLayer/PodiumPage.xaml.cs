using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFLayer.ServicesImplementation;

namespace WPFLayer
{
    public partial class PodiumPage : Page
    {
        public PodiumPage(List<GamesPlayers> players)
        {
            InitializeComponent();
            InitializePlayerNamesAndScores(players);
            InitializePlayerAvatars(players);
        }

        private void InitializePlayerNamesAndScores(List<GamesPlayers> players)
        {
            List<TextBlock> playerNames = new List<TextBlock>();
            playerNames.Add(TextBlock_PlayerName1);
            playerNames.Add(TextBlock_PlayerName2);
            playerNames.Add(TextBlock_PlayerName3);
            playerNames.Add(TextBlock_PlayerName4);

            List<TextBlock> playerScores = new List<TextBlock>();
            playerScores.Add(TextBlock_PlayerScore1);
            playerScores.Add(TextBlock_PlayerScore2);
            playerScores.Add(TextBlock_PlayerScore3);
            playerScores.Add(TextBlock_PlayerScore4);

            int playerCount = players.Count;
            for (int playerIndex = 0; playerIndex < playerCount; playerIndex++)
            {
                playerNames[playerIndex].Text = players[playerIndex].Player.playerName;
                playerScores[playerIndex].Text = players[playerIndex].gameScore.ToString();
            }
        }

        private void InitializePlayerAvatars(List<GamesPlayers> players)
        {
            List<StackPanel> playerPanels = new List<StackPanel>();
            playerPanels.Add(this.StackPanel_Winner);
            playerPanels.Add(this.StackPanel_2ndPlace);
            playerPanels.Add(this.StackPanel_3rdPlace);
            playerPanels.Add(this.StackPanel_4thPlace);

            List<ImageBrush> playerAvatars = new List<ImageBrush>();
            playerAvatars.Add(this.ImageBrush_PlayerAvatar1);
            playerAvatars.Add(this.ImageBrush_PlayerAvatar2);
            playerAvatars.Add(this.ImageBrush_PlayerAvatar3);
            playerAvatars.Add(this.ImageBrush_PlayerAvatar4);

            int playerCount = players.Count;
            for (int playerIndex = 0; playerIndex < playerCount; playerIndex++)
            {
                playerPanels[playerIndex].Visibility = Visibility.Visible; ;
                GamesPlayers currenPlayer = players[playerIndex];
                playerAvatars[playerIndex].ImageSource = 
                    Assets.ImageHelper.GetBitmapImageFor(currenPlayer.Player.ProfileImage.profileImageName);

            }
        }

        private void Button_ReturnToMenu_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.RemoveBackEntry();
            NavigationService.RemoveBackEntry();
            NavigationService.GoBack();
        }
    }
}
