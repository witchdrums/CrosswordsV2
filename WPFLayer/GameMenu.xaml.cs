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
using System.Windows.Shapes;
using System.ServiceModel;
using WPFLayer.ServicesImplementation;
using System.Text.RegularExpressions;
using Validation;
using WPFLayer.Properties;


namespace WPFLayer
{
    public partial class GameMenu : Page, ServicesImplementation.IGameRoomManagementCallback, ServicesImplementation.IPingCallback
    {
        public ServicesImplementation.Users UserLogin { get; set; }
        public ServicesImplementation.Players PlayerLogin { get; set; }
        public GameMenu(ServicesImplementation.Players player)
        {
            InitializeComponent();
            this.TextBox_IdGameToJoin.MaxLength = 8;
            this.PlayerLogin = player;
            this.UserLogin = player.User;
            InstanceContext context = new InstanceContext(this);
            ServicesImplementation.PingClient ping = new PingClient(context);
            ping.ConnectPingManagement(UserLogin);
            HideBanUsersButton();
        }

        private void HideBanUsersButton()
        { 
            bool userIsAdmin = 
                this.UserLogin.idUserType == (int)UserTypes.ADMIN;
            if (!userIsAdmin)
            { 
                this.Button_BanUsers.Visibility = Visibility.Hidden;
            }
        }

        private void Button_NewGame_Click(object sender, RoutedEventArgs e)
        {
            InstanceContext context = new InstanceContext(this);
            ServicesImplementation.GameRoomManagementClient gameRoomClient = new ServicesImplementation.GameRoomManagementClient(context);
            int idNewRoom = gameRoomClient.CreateRoom(UserLogin);
            GameRoom gameRoomPage = new GameRoom(idNewRoom,this.UserLogin,this.PlayerLogin);
            this.NavigationService.Navigate(gameRoomPage);
        }
        private void JoinGame_Click(object sender, RoutedEventArgs e)
        {
            InstanceContext context = new InstanceContext(this);
            ServicesImplementation.GameRoomManagementClient gameRoomClient = new ServicesImplementation.GameRoomManagementClient(context);
            if(!string.IsNullOrEmpty(this.TextBox_IdGameToJoin.Text))
            {
                int idNewRoom = int.Parse(this.TextBox_IdGameToJoin.Text);
                if (gameRoomClient.CheckRoomAvailability(idNewRoom))
                {
                    GameRoom gameRoomPage = new GameRoom(idNewRoom, this.UserLogin, this.PlayerLogin);
                    this.NavigationService.Navigate(gameRoomPage);
                }
                else
                {
                    MessageBox.Show(Properties.Resources.Menu_Message_RoomUnviable);
                }
            }
        }

        private void Button_MyProfile_Click(object sender, RoutedEventArgs e)
        {
            ProfilePage profilePage = new ProfilePage(this.PlayerLogin);
            this.NavigationService.Navigate(profilePage);
        }

        public void ReciveInvitationToRoom(int idRoom)
        {
            
        }

        public void UpdateRoom(Users[] usersInRoom)
        {
            
        }

        public void ForceExitToRoom()
        {
        }

        public void EnterGame()
        {
        }

        public void EnterGame(GameConfiguration gameConfiguration)
        {
        }

        private void Button_Friends_Click(object sender, RoutedEventArgs e)
        {
            FriendsPage friendsPage = new FriendsPage(PlayerLogin);
            this.NavigationService.Navigate(friendsPage);
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Button_BanUsers_Click(object sender, RoutedEventArgs e)
        {
            BanUsersPage banUsersPage = new BanUsersPage();
            this.NavigationService.Navigate(banUsersPage);
        }

        private void textBox_PreviewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Command == ApplicationCommands.Copy ||
                e.Command == ApplicationCommands.Cut ||
                e.Command == ApplicationCommands.Paste)
            {
                e.Handled = true;
            }
        }

        public void Alive()
        {
        }

        public void BackMenu()
        {
        }
    }
}
