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
using Email;
using WPFLayer.Properties;

namespace WPFLayer
{
    /// <summary>
    /// Interaction logic for InvitationWindow.xaml
    /// </summary>
    public partial class InvitationWindow : Window, IUsersManagerCallback
    {
        public ServicesImplementation.Players PlayerLogin { get; set; }
        public int IdRoom { get; set; }
        private List<ServicesImplementation.Players> FriendsList = new List<Players>();
        public InvitationWindow(int idGameRoom,ServicesImplementation.Players player)
        {
            InitializeComponent();
            this.IdRoom = idGameRoom;
            this.PlayerLogin = player;
            ServicesImplementation.PlayersManagementClient playersManagementClient = new ServicesImplementation.PlayersManagementClient();
            this.FriendsList.AddRange(playersManagementClient.GetFriendList(PlayerLogin));
            this.LoadFriendListView();
        }

        public void LoadFriendListView()
        {
            this.ListView_Friends.Items.Clear();
            foreach(ServicesImplementation.Players friend in this.FriendsList)
            {
                ListViewItem friendDisplay = new ListViewItem();
                friendDisplay.Tag = friend;
                friendDisplay.Content = friend.playerName;
                this.ListView_Friends.Items.Add(friendDisplay);
            }
        }

        private void Button_SendInvitation_Click(object sender, RoutedEventArgs e)
        {
            InstanceContext context = new InstanceContext(this);
            ServicesImplementation.UsersManagerClient usersManagerClient = new UsersManagerClient(context);
            ListViewItem itemSelected = (ListViewItem)this.ListView_Friends.SelectedItem;
            if(itemSelected == null)
            {
                MessageBox.Show(Properties.Resources.Invitation_Message_Unselected);
            }else if(true == true)
                {
                    ServicesImplementation.Players playerTarget = (Players)itemSelected.Tag;
                    playerTarget.User = usersManagerClient.GetUserByPlayer(playerTarget);
                    this.SendEmail(playerTarget.User.email);
                }
        }

        private void Button_Search_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SendEmail(string userEmail)
        {
            IEmailService emailService = new EmailService();
            emailService.SendEmailToUser
           (
            userEmail,
            "Subject -- CrossWords Game invitation",
            "Header --- Te han invitado a una partida boludo",
            "Body ---- Te invito a jugar una partida mi codigo de sala de juego es..." + this.IdRoom
           );
        }

        public void Response([MessageParameter(Name = "response")] bool response1)
        {
        }

        private void Button_Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
