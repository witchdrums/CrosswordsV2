using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;
using System.Windows.Interop;
using System.ServiceModel;

namespace WPFLayer
{
    /// <summary>
    /// Interaction logic for UpdateProfilePage.xaml
    /// </summary>
    public partial class UpdateProfilePage : Page, ServicesImplementation.IUsersManagerCallback
    {
        public ServicesImplementation.Players PlayerLogin { get; set; }

        private List<ServicesImplementation.ProfileImages> profileImages;

        private int profileImageNumber = 0;

        private ServicesImplementation.ProfileImages imageSelectedName;
        public UpdateProfilePage(ServicesImplementation.Players player)
        {
            this.PlayerLogin = player;
            InitializeComponent();
            LoadPreviousInformation();
        }

        private void LoadPreviousInformation()
        {
            Bitmap bitmapProfileImage = (Bitmap)Properties.ResourcesProfileImages.ResourceManager.GetObject(PlayerLogin.ProfileImage.profileImageName);
            BitmapSource bitmapProfileImageSource = Imaging.CreateBitmapSourceFromHBitmap(bitmapProfileImage.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            this.Image_AddNewImagePlayerProfile.Source = bitmapProfileImageSource;
            this.Image_AddNewImagePlayerProfile.Stretch = Stretch.Fill;
            InstanceContext context = new InstanceContext(this);
            ServicesImplementation.UsersManagerClient client = new ServicesImplementation.UsersManagerClient(context);
            profileImages = client.GetProfileImages().ToList();
            imageSelectedName = PlayerLogin.ProfileImage;
        }

        private void Button_ImageProfile_Click(object sender, RoutedEventArgs e)
        {
            if (sender.Equals(this.Button_BackImageProfile))
            {
                if (profileImageNumber != 0)
                {
                    profileImageNumber--;
                    ShowAvailableImage();
                }
            }
            if (sender.Equals(this.Button_NextImageProfile))
            {
                if (profileImageNumber != profileImages.Count)
                {
                    ShowAvailableImage();
                    profileImageNumber++;
                }
            }
        }

        private void ShowAvailableImage()
        {
            Bitmap bitmapProfileImage = (Bitmap)Properties.ResourcesProfileImages.ResourceManager.GetObject(profileImages[profileImageNumber].profileImageName);
            BitmapSource bitmapProfileImageSource = Imaging.CreateBitmapSourceFromHBitmap(bitmapProfileImage.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            this.Image_AddNewImagePlayerProfile.Source = bitmapProfileImageSource;
            this.Image_AddNewImagePlayerProfile.Stretch = Stretch.Fill;
            imageSelectedName = profileImages[profileImageNumber];
        }

        private void Button_Save_Click(object sender, RoutedEventArgs e)
        {
            GetNewPlayerProfileInformation();
        }

        private void GetNewPlayerProfileInformation()
        {
            if (imageSelectedName != null && !String.IsNullOrWhiteSpace(this.TextBox_PlayerName.Text) && !String.IsNullOrWhiteSpace(this.TextBox_PlayerDescription.Text))
            {
                UpdatePlayerProfileInformation();
            }
            else
            {
                MessageBox.Show("Por favor verifique todos los campos esten llenos",
                                "Campos vacíos", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void UpdatePlayerProfileInformation()
        {
            ServicesImplementation.PlayersManagementClient client = new ServicesImplementation.PlayersManagementClient();
            PlayerLogin.idProfileImage = imageSelectedName.idProfileImage;
            PlayerLogin.playerName = this.TextBox_PlayerName.Text;
            PlayerLogin.playerDescription = this.TextBox_PlayerDescription.Text;
            if (client.UpdatePlayerProfileInformation(PlayerLogin))
            {
                MessageBox.Show("¡Perfil actualizado con éxito!",
                                "Perfil actualizado", MessageBoxButton.OK, MessageBoxImage.Information);
                GameMenu gameMenuPage = new GameMenu(PlayerLogin);
                this.NavigationService.Navigate(gameMenuPage);
            }
        }

        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        public void Response([MessageParameter(Name = "response")] bool response1)
        {
            throw new NotImplementedException();
        }
    }
}
