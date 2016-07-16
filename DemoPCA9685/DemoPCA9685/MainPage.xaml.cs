﻿using Microsoft.IoT.Lightning.Providers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Devices;
using Windows.Devices.Gpio;
using Windows.Devices.I2c;
using Windows.Devices.Pwm;
using Windows.Devices.Spi;
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
        private PwmPin[] m_servo = new PwmPin[3];
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

                if (LightningProvider.IsLightningEnabled) {
                    LowLevelDevicesController.DefaultProvider = LightningProvider.GetAggregateProvider();
                }


                //PCA9685
                m_pwmController = (await PwmController.GetControllersAsync(LightningPwmProvider.GetPwmProvider()))[0];
                //24 - 1000
                m_pwmController.SetDesiredFrequency(300); //For Servo
                Debug.WriteLine($"Freq: {m_pwmController.ActualFrequency}");
                //m_pwmController.SetDesiredFrequency(25); //For L298
                //m_pwmController.SetDesiredFrequency(1000);
                m_servo[0] = m_pwmController.OpenPin(0);
                m_servo[1] = m_pwmController.OpenPin(1);
                m_servo[2] = m_pwmController.OpenPin(3);

                m_servo[2].SetActiveDutyCyclePercentage(0.5);
                m_servo[2].Start();
                m_servo[2].SetActiveDutyCyclePercentage(0.2);
                m_servo[2].SetActiveDutyCyclePercentage(0.9);
                m_servo[2].SetActiveDutyCyclePercentage(0.5);
                m_servo[2].Stop();


                m_motor0 = m_pwmController.OpenPin(4);
                m_motor1 = m_pwmController.OpenPin(5);

                m_servo[0].Start();
                Servo(0,0.5);
                m_servo[0].Stop();
                m_servo[0].Start();
                Servo(0,1);
                m_servo[0].Stop();
                m_servo[0].Start();
                Servo(0,0.9);
                m_servo[0].Stop();
                m_servo[0].Start();
                Servo(0,0.8);
                m_servo[0].Stop();
                m_servo[0].Start();
                Servo(0,0.7);
                m_servo[0].Stop();
                m_servo[0].Start();
                Servo(0,0.6);
                m_servo[0].Stop();
                m_servo[0].Start();
                Servo(0,0.5);
                m_servo[0].Stop();
                m_servo[0].Start();
                Servo(0,0.4);
                m_servo[0].Stop();
                m_servo[0].Start();
                Servo(0,0.3);
                m_servo[0].Stop();
                m_servo[0].Start();
                Servo(0,0.2);
                m_servo[0].Stop();
                m_servo[0].Start();
                Servo(0,0.1);
                m_servo[0].Stop();
                m_servo[0].Start();
                Servo(0,0.0);
                m_servo[0].Stop();


                m_servo[1].Start();
                Servo(1, 0.5);
                m_servo[1].Stop();
                m_servo[1].Start();
                Servo(1, 1);
                m_servo[1].Stop();
                m_servo[1].Start();
                Servo(1, 0.9);
                m_servo[1].Stop();
                m_servo[1].Start();
                Servo(1, 0.8);
                m_servo[1].Stop();
                m_servo[1].Start();
                Servo(1, 0.7);
                m_servo[1].Stop();
                m_servo[1].Start();
                Servo(1, 0.6);
                m_servo[1].Stop();
                m_servo[1].Start();
                Servo(1, 0.5);
                m_servo[1].Stop();
                m_servo[1].Start();
                Servo(1, 0.4);
                m_servo[1].Stop();
                m_servo[1].Start();
                Servo(1, 0.3);
                m_servo[1].Stop();
                m_servo[1].Start();
                Servo(1, 0.2);
                m_servo[1].Stop();
                m_servo[1].Start();
                Servo(1, 0.1);
                m_servo[1].Stop();
                m_servo[1].Start();
                Servo(1, 0.0);
                m_servo[1].Stop();

                //m_servo1.SetActiveDutyCyclePercentage(0.5);
                //m_servo1.Start();

                //m_motor0.SetActiveDutyCyclePercentage(0.5);
                //m_motor0.Start();
                //m_motor1.SetActiveDutyCyclePercentage(0.5);
                //m_motor1.Start();
                //m_motor0.SetActiveDutyCyclePercentage(0.9);
                //m_motor1.SetActiveDutyCyclePercentage(0.9);

                var gpioController = await GpioController.GetDefaultAsync();
                var i2cController = await I2cController.GetDefaultAsync();
                var spiController = await SpiController.GetDefaultAsync();

                m_pin0 = gpioController.OpenPin(19);
                m_pin1 = gpioController.OpenPin(26);
                m_pin0.SetDriveMode(GpioPinDriveMode.Output);
                m_pin1.SetDriveMode(GpioPinDriveMode.Output);

                m_pin0.Write(GpioPinValue.High);
                m_pin0.Write(GpioPinValue.Low);
                m_pin1.Write(GpioPinValue.High);
                m_pin0.Write(GpioPinValue.Low);

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

        private void Servo(int servo, double v)
        {
            var lv = LinMap(v, 0.2, 0.8);
            m_servo[servo].SetActiveDutyCyclePercentage(lv);
        }
        /// <summary>
        /// Lin map
        /// </summary>
        /// <param name="val"></param>
        /// <param name="rmin"></param>
        /// <param name="rmax"></param>
        /// <returns></returns>
        private double LinMap(double val,double rmin=0.2, double rmax = 0.8)
        {
            if (val < 0 || val > 1 || rmin < 0 || rmin> 1 || rmax<0 || rmax>1) throw new ArgumentException();
            if (rmin>=rmax ) throw new ArgumentException();
            var o = rmin + val * (rmax - rmin);
            return o;

        }

        bool m_switch = false;
        private GpioPin m_pin0;
        private GpioPin m_pin1;

        private void M_timer_Tick(object sender, object e)
        {
            if (m_switch)
            {
                //m_servo[0].SetActiveDutyCyclePercentage(0.5);
                //m_servo1.SetActiveDutyCyclePercentage(0.5);

                m_motor0.SetActiveDutyCyclePercentage(0.5);
                m_motor1.SetActiveDutyCyclePercentage(0.5);
            } else
            {
                //m_servo[0].SetActiveDutyCyclePercentage(0.2);
                //m_servo1.SetActiveDutyCyclePercentage(0.2);

                m_motor0.SetActiveDutyCyclePercentage(0.999);
                m_motor1.SetActiveDutyCyclePercentage(0.999);
            }
            m_switch = !m_switch;
        }
    }
}
