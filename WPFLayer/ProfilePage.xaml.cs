using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFLayer
{
    /// <summary>
    /// Interaction logic for ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page, ServicesImplementation.IUsersManagerCallback
    {
        public ServicesImplementation.Players playerLogin { get; set; }
        public ProfilePage(ServicesImplementation.Players player)
        {
            this.playerLogin = player;
            InitializeComponent();
            playerLogin = GetPlayerProfileInformation();
            ShowProfileView();
        }

        private ServicesImplementation.Players GetPlayerProfileInformation()
        {
            InstanceContext context = new InstanceContext(this);
            ServicesImplementation.UsersManagerClient client = new ServicesImplementation.UsersManagerClient(context);
            ServicesImplementation.Players player = new ServicesImplementation.Players();
            player = playerLogin;
            player = client.GetPlayerInformation(player);
            return player;
        }

        public void ShowProfileView()
        {
            this.TextBox_IdPlayer.Text = "ID:  " + playerLogin.idPlayer.ToString();
            this.TextBlock_PlayerLevelNumber.Text = playerLogin.playerLevel.ToString();
            this.TextBlock_PlayerProfileDescription.Text = playerLogin.playerDescription;
            this.TextBlock_GamesPlayedNumber.Text = playerLogin.gamesPlayed.ToString();
            this.TextBlock_GamesWonNumber.Text = playerLogin.gamesWon.ToString();
            Bitmap bitmapProfileImage = (Bitmap)Properties.ResourcesProfileImages.ResourceManager.GetObject(playerLogin.ProfileImage.profileImageName);
            BitmapSource bitmapProfileImageSource = Imaging.CreateBitmapSourceFromHBitmap(bitmapProfileImage.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            this.Image_PlayerProfile.Source = bitmapProfileImageSource;
            this.Image_PlayerProfile.Stretch = Stretch.Fill;
            Bitmap bitmapRankImage = (Bitmap)Properties.ResourcesPlayerRankImages.ResourceManager.GetObject(playerLogin.Rank.rankName);
            BitmapSource bitmapRankImageSource = Imaging.CreateBitmapSourceFromHBitmap(bitmapRankImage.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            this.Image_PlayerRank.Source = bitmapRankImageSource;
            this.Label_PlayerRank.Content = playerLogin.Rank.rankName;
        }

        private void Button_EditProfile_Click(object sender, RoutedEventArgs e)
        {
            SignUpPage signUpPage = new SignUpPage();
            this.NavigationService.Navigate(signUpPage);
        }

        private void Button_Exit_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        public void Response([MessageParameter(Name = "response")] bool response1)
        {
            throw new NotImplementedException();
        }
    }
}
