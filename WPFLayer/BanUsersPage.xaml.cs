using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFLayer.ServicesImplementation;

namespace WPFLayer
{
    public partial class BanUsersPage : Page
    {
        private List<Users> reportedUsers;
        private Dictionary<int, Users> usersToUpdate = new Dictionary<int, Users>();

        public BanUsersPage()
        {
            InitializeComponent();
            reportedUsers = GetUsersManagerClient().GetReportedUsers().ToList();
            this.DataGrid_Users.DataContext = reportedUsers;
        }

        private UsersManagerClient GetUsersManagerClient()
        {
            return new ServicesImplementation.UsersManagerClient();
        }

        private void Button_GoBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Button_BanUser_Click(object sender, RoutedEventArgs e)
        {
            UsersManagerClient userClient = GetUsersManagerClient();
            foreach (KeyValuePair<int, Users> userPair in this.usersToUpdate)
            {
                userClient.UpdateUserBanStatus(userPair.Value);
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

            
            Users selectedUser = (this.DataGrid_Users.SelectedItem as Users);
            if (this.usersToUpdate.ContainsKey(selectedUser.idUser))
            {
                this.usersToUpdate.Remove(selectedUser.idUser);
            }
            else
            {
                this.usersToUpdate.Add(selectedUser.idUser, selectedUser);
            }
            this.Button_BanUser.IsEnabled = this.usersToUpdate.Count != 0;
        }
    }
}
