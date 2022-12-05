using System;
using System.Text.RegularExpressions;
using System.Windows;
using WPFLayer.ServicesImplementation;

namespace WPFLayer
{
    public partial class GameConfigurationWindow : Window
    {
        private GameConfiguration gameConfiguration;
        public GameConfigurationWindow(GameConfiguration gameConfiguration)
        {
            this.gameConfiguration = gameConfiguration;
            InitializeComponent();
            
            this.Slider_Seconds.Value = this.gameConfiguration.TurnSeconds;
            this.Slider_Turns.Value = this.gameConfiguration.TurnAmount;
        }

        private void Slider_Turns_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (this.IsInitialized)
            {
                this.gameConfiguration.TurnAmount = (int)this.Slider_Turns.Value;
            }
        }

        private void Slider_Seconds_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (this.IsInitialized)
            {
                this.gameConfiguration.TurnSeconds = (int)this.Slider_Seconds.Value;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


    }
}
