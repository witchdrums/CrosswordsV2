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
    public partial class GameRoom : Page, ServicesImplementation.IMessagesCallback, ServicesImplementation.IGameRoomManagementCallback, IPingCallback
    {
        public int IdRoom { set; get; }
        public ServicesImplementation.Players PlayerLogin { set; get; }
        public List<ServicesImplementation.Users> UsersRoom { set; get; }
        public List<ServicesImplementation.Players> PlayersRoom { set;get; }
        private InvitationWindow invitationWindow;

        private GameConfiguration gameConfiguration;
        private GameConfigurationWindow gameConfigurationWindow;
        public GameRoom(int idRoom, ServicesImplementation.Users userLogin, Players playerLogin)
        {
            InitializeComponent();
            this.TextBox_Message.MaxLength = 100;
            this.UsersRoom = new List<ServicesImplementation.Users>();
            this.PlayersRoom = new List<ServicesImplementation.Players>();
            this.IdRoom = idRoom;
            this.PlayerLogin = playerLogin;
            this.PlayerLogin.User = userLogin;
            this.Label_IdRoom.Content = this.IdRoom.ToString();
            InstanceContext context = new InstanceContext(this);
            ServicesImplementation.GameRoomManagementClient gameRoomClient = new ServicesImplementation.GameRoomManagementClient(context);
            ServicesImplementation.MessagesClient messagesClient = new ServicesImplementation.MessagesClient(context);
            ServicesImplementation.PingClient pingClient = new ServicesImplementation.PingClient(context);
            pingClient.ConnectPingManagement(userLogin);
            messagesClient.ConnectMessages(userLogin);
            gameRoomClient.ConnectGameRoomManagement(userLogin);
            gameRoomClient.JoinToRoom(this.IdRoom, PlayerLogin.User);

            this.gameConfiguration = new GameConfiguration();
            this.gameConfiguration.TurnSeconds = 30;
            this.gameConfiguration.TurnAmount = 30;
        }

        public void ReciveChatMessage(Users userOrigin, string message)
        {
            this.TextBlock_Chat.Text += userOrigin.username + " : " + message + "\n";
        }

        public void ReciveInvitationToRoom(int idRoom)
        {
        }

        public void UpdateRoom(Users[] usersInRoom)
        {
            InstanceContext context = new InstanceContext(this);
            this.UsersRoom.Clear();
            this.PlayersRoom.Clear();
            this.UsersRoom.AddRange(usersInRoom);
            this.LoadPlayersOfUsersInRoom(usersInRoom);
            this.RefreshListView();
        }

        public void LoadPlayersOfUsersInRoom(Users[] usersList)
        {
            
            InstanceContext context = new InstanceContext(this);
            UsersManagerClient usersManagerClient = new UsersManagerClient();
            foreach (Users user in usersList)
            {
                Players userPlayer = new Players();
                userPlayer.User = user;
                userPlayer = usersManagerClient.GetPlayerInformation(userPlayer);
                this.PlayersRoom.Add(userPlayer);
            }           
        }

        private void RefreshListView()
        {
            this.ListView_Users.Items.Clear();
            foreach (Players player in this.PlayersRoom)
            {
                ListViewItem playerDisplay = new ListViewItem();
                playerDisplay.Tag = player;
                playerDisplay.Content = player.playerName;
                this.ListView_Users.Items.Add(playerDisplay);
            }
        }
        private void Button_SendMessage_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(this.TextBox_Message.Text))
            {
                InstanceContext context = new InstanceContext(this);
                ServicesImplementation.MessagesClient messages = new ServicesImplementation.MessagesClient(context);
                messages.SendChatMessage(UsersRoom.ToArray(), this.PlayerLogin.User, this.TextBox_Message.Text);
            }
            this.TextBox_Message.Clear();
        }



        private void Button_Back_Click(object sender, RoutedEventArgs e)
        {
            InstanceContext context = new InstanceContext(this);
            ServicesImplementation.GameRoomManagementClient gameRoomClient = new ServicesImplementation.GameRoomManagementClient(context);
            if (this.IdRoom != this.PlayerLogin.User.idUser)
            {
                gameRoomClient.ExitToRoom(this.IdRoom, this.PlayerLogin.User);
                if (this.invitationWindow != null)
                {
                    this.invitationWindow.Close();
                }
                this.NavigationService.GoBack();
            }
            else
            {
                gameRoomClient.DeleteRoom(this.IdRoom);
            }
        }

        public void ForceExitToRoom()
        {
            if(this.invitationWindow != null){
                this.invitationWindow.Close();
            }
            
            this.NavigationService.GoBack();
            
        }

        private void TextBlock_Chat_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {
            
        }

        private void Button_StartGame_Click(object sender, RoutedEventArgs @event)
        {
            if (this.invitationWindow != null)
            {
                this.invitationWindow.Close();
            }
            if (this.gameConfigurationWindow != null)
            {
                this.gameConfigurationWindow.Close();
                this.gameConfigurationWindow = null;
            }
            InstanceContext instanceContext = new InstanceContext(this);
            GameRoomManagementClient gameRoomClient = new ServicesImplementation.GameRoomManagementClient(instanceContext);
            
            this.gameConfiguration.GamePlayerQueue = GetGamesPlayersQueue(this.UsersRoom);
            this.gameConfiguration.Board = gameRoomClient.GetBoardById(1);
            this.gameConfiguration.UsersRoom = this.UsersRoom.ToArray();
            
            gameRoomClient.LaunchGamePage(gameConfiguration, this.IdRoom);

        }

        public Queue<GamesPlayers> GetGamesPlayersQueue(List<Users> userRoom)
        {
            UsersManagerClient usersManagerClient = new UsersManagerClient();
            Queue<GamesPlayers> gamePlayersQueue = new Queue<GamesPlayers>();
            foreach (Users user in userRoom)
            {
                Players userPlayer = new Players();
                userPlayer.User = user;
                userPlayer = usersManagerClient.GetPlayerInformation(userPlayer);

                GamesPlayers gamePlayer = new GamesPlayers();
                gamePlayer.idPlayer = userPlayer.idPlayer;
                gamePlayer.Player = userPlayer;

                gamePlayersQueue.Enqueue(gamePlayer);           
            }
            return gamePlayersQueue;
        }

        public void EnterGame(GameConfiguration gameConfiguration)
        {
            GamePage gamePage = new GamePage(this.PlayerLogin.User, this.IdRoom, gameConfiguration);
            NavigationService.Navigate(gamePage);
        }

        private void Button_Invite_Click(object sender, RoutedEventArgs e)
        {
            this.invitationWindow = new InvitationWindow(this.IdRoom,this.PlayerLogin);
            this.invitationWindow.Show();
        }

        private void Button_SettingGame_Click(object sender, RoutedEventArgs e)
        {
            this.gameConfigurationWindow = new GameConfigurationWindow(this.gameConfiguration);
            this.gameConfigurationWindow.Show();
            
        }

        public void Alive()
        {
        }

        public void BackMenu()
        {
            NavigationService.GoBack();
            MessageBox.Show(Properties.Resources.General_Message_ErrorGame);
        }
    }
}
