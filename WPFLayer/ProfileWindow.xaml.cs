using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPFLayer.ServicesImplementation;

namespace WPFLayer
{
    public partial class ProfileWindow : Window
    {
        private Players chosenPlayer;
        private GamePage gamePage;
        
        public ProfileWindow(GamePage gamePage, Players chosenPlayer )
        {
            InitializeComponent();
            this.chosenPlayer = chosenPlayer;
            this.gamePage = gamePage;
            
            this.TextBlock_PlayerName.Text = chosenPlayer.playerName;
            this.TextBlock_UserID.Text = chosenPlayer.User.idUser.ToString();
            this.TextBlock_Username.Text = chosenPlayer.User.username;
            this.TextBlock_PlayerDescription.Text = chosenPlayer.playerDescription;
            this.Label_PlayedGames.Content = this.chosenPlayer.gamesPlayed;
            this.Label_WonGames.Content = this.chosenPlayer.gamesWon;
            if (gamePage.IdRoom == gamePage.Player.Player.User.idUser)
            { 
                this.Button_Kick.Visibility = Visibility.Visible;
            }

            GetReportCategories();


        }

        private void GetReportCategories()
        {
            UsersManagerClient usersManagerClient = new UsersManagerClient();
            Categories[] reportCategories = usersManagerClient.GetReportCategories();
            foreach (Categories category in reportCategories)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = category.description;
                item.Tag = category;
                this.Combobox_ReportCategories.Items.Add(item);
            }
        }

        private void Button_SendReport_Click(object sender, RoutedEventArgs e)
        {
            UsersManagerClient usersManagerClient = new UsersManagerClient();

            Categories selectedCategory = 
                ((ComboBoxItem)this.Combobox_ReportCategories.SelectedItem).Tag as Categories;
            Reports report = new Reports();
            report.idCategory = selectedCategory.idCategory;
            report.idUser = this.chosenPlayer.User.idUser;
            report.chatLog = ";";

            if (usersManagerClient.RegisterReport(report))
            {
                MessageBox.Show(Properties.Resources.ProfileWindow_Message_Submitted);
            }
        }

        private void Button_Kick_Click(object sender, RoutedEventArgs e)
        {
            gamePage.KickPlayer(this.chosenPlayer);
        }

        private void Combobox_ReportCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Button_SendReport.IsEnabled = true;
        }
    }
}
