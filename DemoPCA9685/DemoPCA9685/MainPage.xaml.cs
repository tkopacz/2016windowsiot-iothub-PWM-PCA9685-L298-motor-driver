using Microsoft.IoT.Lightning.Providers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Devices.Pwm;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DemoPCA9685
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private PwmController m_pwmController;
        private PwmPin m_servo0, m_servo1;
        private PwmPin m_motor0, m_motor1;
        private DispatcherTimer m_timer;

        public MainPage()
        {
            this.InitializeComponent();
            setup();
        }

        private async Task setup()
        {
            try
            {
                //PCA9685
                m_pwmController = (await PwmController.GetControllersAsync(LightningPwmProvider.GetPwmProvider()))[0];
                //24 - 1000
                m_pwmController.SetDesiredFrequency(50); //For Servo
                //m_pwmController.SetDesiredFrequency(25); //For L298
                //m_pwmController.SetDesiredFrequency(1000);
                m_servo0 = m_pwmController.OpenPin(0);
                m_servo1 = m_pwmController.OpenPin(1);

                m_motor0 = m_pwmController.OpenPin(4);
                m_motor1 = m_pwmController.OpenPin(5);

                //m_servo0.SetActiveDutyCyclePercentage(0.5);
                //m_servo0.Start();
                //m_servo1.SetActiveDutyCyclePercentage(0.5);
                //m_servo1.Start();

                m_motor0.SetActiveDutyCyclePercentage(0.5);
                m_motor0.Start();
                m_motor1.SetActiveDutyCyclePercentage(0.5);
                m_motor1.Start();

                m_timer = new DispatcherTimer();
                m_timer.Tick += M_timer_Tick;
                m_timer.Interval = TimeSpan.FromMilliseconds(2500);
                m_timer.Start();
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
                throw;
            }

        }

        bool m_switch = false;
        private void M_timer_Tick(object sender, object e)
        {
            if (m_switch)
            {
                //m_servo0.SetActiveDutyCyclePercentage(0.5);
                //m_servo1.SetActiveDutyCyclePercentage(0.5);

                m_motor0.SetActiveDutyCyclePercentage(0.5);
                m_motor1.SetActiveDutyCyclePercentage(0.5);
            } else
            {
                //m_servo0.SetActiveDutyCyclePercentage(0.2);
                //m_servo1.SetActiveDutyCyclePercentage(0.2);

                m_motor0.SetActiveDutyCyclePercentage(0.999);
                m_motor1.SetActiveDutyCyclePercentage(0.999);
            }
            m_switch = !m_switch;
        }
    }
}
