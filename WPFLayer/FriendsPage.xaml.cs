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
    /// Interaction logic for FriendsPage.xaml
    /// </summary>
    public partial class FriendsPage : Page
    {
        public ServicesImplementation.Players PlayerLogin { set; get; }
        public List<ServicesImplementation.Players> FriendsList = new List<Players>();
        public FriendsPage(ServicesImplementation.Players player)
        {
            InitializeComponent();
            this.PlayerLogin = player;
            this.UpdateFriends();
        }

        private void RefreshFriendsListView()
        {
            this.ListView_Friends.Items.Clear();
            foreach(ServicesImplementation.Players friendPlayer in this.FriendsList)
            {
                ListViewItem friendDisplay = new ListViewItem();
                friendDisplay.Tag = friendPlayer;
                friendDisplay.Content = friendPlayer.playerName;
                this.ListView_Friends.Items.Add(friendDisplay);
            }
        }

        private void UpdateFriends()
        {
            InstanceContext context = new InstanceContext(this);
            ServicesImplementation.PlayersManagementClient playersManagementClient = new ServicesImplementation.PlayersManagementClient();
            this.FriendsList.Clear();
            this.FriendsList.AddRange(playersManagementClient.GetFriendList(this.PlayerLogin));
            this.RefreshFriendsListView();
        }

        private void Button_Back_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void Button_AddFriend_Click(object sender, RoutedEventArgs e)
        {
            AddFriendWindow addFriendWindow = new AddFriendWindow(PlayerLogin);
            addFriendWindow.ShowDialog();
            this.UpdateFriends();
        }

        private void Button_DeleteFriend_Click(object sender, RoutedEventArgs e)
        {
            ServicesImplementation.PlayersManagementClient playersManagementClient = new ServicesImplementation.PlayersManagementClient();
            ListViewItem itemSelected = (ListViewItem)this.ListView_Friends.SelectedItem;
            if(itemSelected != null)
            {
                ServicesImplementation.Players playerTarget = (Players)itemSelected.Tag;
                if (playersManagementClient.RemoveFriend(PlayerLogin, playerTarget))
                {
                    MessageBox.Show("Se ha eliminado el jugador " + playerTarget.playerName + " de su lista de amigos");
                    this.UpdateFriends();
                }else
                    {
                    MessageBox.Show("No se ha podido realizar la operación");
                }
            }
            else
            {
                MessageBox.Show("Seleccione Primero al Jugador que desea remover de amigo");
            }
        }
    }
}
