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

    public partial class FriendsPage : Page
    {
        public ServicesImplementation.Players PlayerLogin { set; get; }
        private List<ServicesImplementation.Players> FriendsList = new List<Players>();
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
                    MessageBox.Show(Properties.Resources.FriendList_Message_Unfriend);
                    this.UpdateFriends();
                }else
                    {
                    MessageBox.Show(Properties.Resources.General_Message_CannotOperation);
                }
            }
            else
            {
                MessageBox.Show(Properties.Resources.FriendList_Message_FirstSelect);
            }
        }
    }
}
