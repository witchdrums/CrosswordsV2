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
    /// <summary>
    /// Interaction logic for GameMenu.xaml
    /// </summary>
    public partial class GameMenu : Page
    {
        public GameMenu()
        {
            InitializeComponent();
        }

        private void NewGameRoom_Click(object sender, RoutedEventArgs e)
        {
            ServicesImplementation.Users users = new ServicesImplementation.Users();//Singletone
            InstanceContext context = new InstanceContext(this);
            ServicesImplementation.GameRoomManagementClient gameRoom = new GameRoomManagementClient(context);
            int idNewRoom = gameRoom.CreateRoom(users);
            GameRoom gameRoomPage = new GameRoom();
            gameRoomPage.IdRoom = idNewRoom;
            this.NavigationService.Navigate(gameRoomPage);
            
        }
    }
}
