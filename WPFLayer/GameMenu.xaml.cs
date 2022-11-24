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

namespace WPFLayer
{
    /// <summary>
    /// Interaction logic for GameMenu.xaml
    /// </summary>
    public partial class GameMenu : Page, ServicesImplementation.IGameRoomManagementCallback
    {
        public ServicesImplementation.Users UserLogin { get; set; }
        public ServicesImplementation.Players PlayerLogin { get; set; }
        public GameMenu(ServicesImplementation.Players player)
        {
            InitializeComponent();
            this.PlayerLogin = player;
            this.UserLogin = player.User;

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
            int idNewRoom = int.Parse(this.TextBox_IdGameToJoin.Text);
            if(gameRoomClient.CheckRoomAvailability(idNewRoom))
            {
                GameRoom gameRoomPage = new GameRoom(idNewRoom, this.UserLogin,this.PlayerLogin);
                this.NavigationService.Navigate(gameRoomPage);
            }else
            {
                MessageBox.Show("La sala no está disponible intente mas tarde");
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
            throw new NotImplementedException();
        }

        public void EnterGame()
        {
            throw new NotImplementedException();
        }

        public void EnterGame(GameConfiguration gameConfiguration)
        {
            throw new NotImplementedException();
        }

        private void Friends_Button_Click(object sender, RoutedEventArgs e)
        {
            FriendsPage friendsPage = new FriendsPage(PlayerLogin);
            this.NavigationService.Navigate(friendsPage);
        }
    }
}
