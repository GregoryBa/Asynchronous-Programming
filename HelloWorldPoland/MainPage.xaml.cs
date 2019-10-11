using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Devices.Gpio;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Playback;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace HelloWorldPoland
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private const int LED = 17;     // Pin nr. 11
        private const int GPIO = 7;
        private const int LED_TEMP = 4; // Pin nr. 
        private static double  VOLUME_VERDI= 0.3;
        private GpioPin _pin;


        public MainPage()
        {
            this.InitializeComponent();
            fuktighetMetode();
            PlayMusica();
            tidna();
            RandomTing();       
            
        }


        private void PlayMusica()  
        {
            mediaElement.Volume = VOLUME_VERDI;            
            mediaElement.AutoPlay = true;
            mediaElement.Play();
            
        }

        private async void fuktighetMetode()
        {
            var controller = GpioController.GetDefault();
            if (controller != null)
            {
                _pin = controller.OpenPin(LED);
                _pin.SetDriveMode(GpioPinDriveMode.Input);
                while (true)
                {
                    if(_pin.Read().ToString() == "Low")
                    {
                        LampeFuktighet.Fill = new SolidColorBrush(Colors.Red);
                        tb_fuktighet.Text = "High moisture levels!";
                        
                    }
                      
                    if (_pin.Read().ToString() == "High")
                    {
                        LampeFuktighet.Fill = new SolidColorBrush(Colors.Black);
                        tb_fuktighet.Text = "";
                    }                     

                    await Task.Delay(10);
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Target device does not have GPIO pins.");
            }
        }


        private async void tidna()
        {
            DateTime tid = DateTime.Now;
            while (true)
            {
                tid = DateTime.Now;
                tb_timer.Text = tid.ToString("HH:mm");
                tb_sek.Text = tid.ToString("ss");
                tb_dato.Text = tid.ToString("dd MMM yyyy");
                tb_dag.Text = tid.ToString("dddd");
                await Task.Delay(10);
            }
        }

        private async void RandomTing()
        {
            Random r = new Random();

            while (true)
            {
                switch(r.Next(1,8))
                {
                    case 1:
                        tb_quotes.Text = "You can do anything, but not everything. \n—David Allen";
                        break;
                    case 2:
                        tb_quotes.Text = "The richest man is not he who has the most, but he who needs the least.\n—Unknown Author";
                        break;
                    case 3:
                        tb_quotes.Text = "You miss 100 percent of the shots you never take.\n—Wayne Gretzky";
                        break;
                    case 4:
                        tb_quotes.Text = "You must be the change you wish to see in the world.\n—Gandhi";
                        break;
                    case 5:
                        tb_quotes.Text = "When I was a boy I was told that anybody could become President. Now I’m beginning to believe it.\n—Clarence Darrow";
                        break;
                    case 6:
                        tb_quotes.Text = "The best time to plant a tree was 20 years ago. The second best time is now.\n–Chinese Proverb";
                        break;
                    case 7:
                        tb_quotes.Text = " I am not a product of my circumstances. I am a product of my decisions. \n–Stephen Covey";
                        break;
                    case 8:
                        tb_quotes.Text = "Every strike brings me closer to the next home run. \n–Babe Ruth";
                        break;
                    case 9:
                        tb_quotes.Text = "Life is what happens to you while you’re busy making other plans. \n–John Lennon";
                        break;
                };
                await Task.Delay(30000);
            }

        }

        private void btn_VolumeHigh_Click(object sender, RoutedEventArgs e)
        {
            if(VOLUME_VERDI < 1.0)
            {
                VOLUME_VERDI = VOLUME_VERDI + 0.1;
                mediaElement.Volume = VOLUME_VERDI;
            }

        }

        private void btn_VolumeLow_Click(object sender, RoutedEventArgs e)
        {
            if (0.0 < VOLUME_VERDI)
            {
                VOLUME_VERDI = VOLUME_VERDI - 0.1;
                mediaElement.Volume = VOLUME_VERDI;
            }
        }

        private void media_MediaEnded(object sender, RoutedEventArgs e)
        {
            mediaElement.Play();
        }

        private void btn_Play_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Play();
        }

        private void btn_Pause_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Pause();

        }

        private void btn_Stop_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Stop();
        }
    }
}
