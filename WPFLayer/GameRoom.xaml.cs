﻿using System;
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
    /// Interaction logic for GameRoom.xaml
    /// </summary>
    public partial class GameRoom : Page, ServicesImplementation.IMessagesCallback, ServicesImplementation.IGameRoomManagementCallback
    {
        public int IdRoom { set; get; }
        public ServicesImplementation.Users UserLogin { set; get; }
        public List<ServicesImplementation.Users> UsersRoom { set; get; }
        public GameRoom(int idRoom,ServicesImplementation.Users userLogin)
        {
            InitializeComponent();
            this.UsersRoom = new List<ServicesImplementation.Users>();
            this.IdRoom = idRoom;
            this.UserLogin = userLogin;
            InstanceContext context = new InstanceContext(this);
            ServicesImplementation.GameRoomManagementClient gameRoomClient = new ServicesImplementation.GameRoomManagementClient(context);
            ServicesImplementation.MessagesClient messagesClient = new ServicesImplementation.MessagesClient(context);
            messagesClient.ConnectMessages(userLogin);
            gameRoomClient.ConnectGameRoomManagement(userLogin);
            gameRoomClient.JoinToRoom(this.IdRoom, UserLogin);
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
            this.UsersRoom.Clear();
            this.UsersRoom.AddRange(usersInRoom);
            this.RefreshListView();
        }

        private void RefreshListView()
        {
            this.ListView_Users.Items.Clear();
            foreach (Users user in this.UsersRoom)
            {
                ListViewItem usersDisplay = new ListViewItem();
                usersDisplay.Tag = user;
                usersDisplay.Content = user.username;
                this.ListView_Users.Items.Add(usersDisplay);
            }
        }
        private void Button_SendMessage_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(this.TextBox_Message.Text))
            {
                InstanceContext context = new InstanceContext(this);
                ServicesImplementation.MessagesClient messages = new ServicesImplementation.MessagesClient(context);
                messages.SendChatMessage(UsersRoom.ToArray(), this.UserLogin, this.TextBox_Message.Text);
            }
            this.TextBox_Message.Clear();
        }

        private void Button_InviteUser_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Back_Click(object sender, RoutedEventArgs e)
        {
            
            this.NavigationService.GoBack();

        }
    }
}
