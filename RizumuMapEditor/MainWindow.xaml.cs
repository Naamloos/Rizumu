#region Using Statements

using System;
using System.Windows;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


#endregion

namespace RizumuMapEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        // We use a Stopwatch to track our total time for cube animation
        private Stopwatch watch = new Stopwatch();

        public MainWindow()
        {
            InitializeComponent();
        }

        #region Control Loaded

        private void MonoGameControl_Loaded(object sender, MerjTek.WpfIntegration.GraphicsDeviceEventArgs e)
        {
            // Because this same event is hooked for all 4 controls, we check if the Stopwatch
            // is running to avoid loading our content 4 times.
            if (!watch.IsRunning)
            {

                // Start the watch now that we're going to be starting our draw loop
                watch.Start();
                spriteBatch = new SpriteBatch(e.GraphicsDevice);
            }
        }

        #endregion
        #region Render Red Control

        public static SpriteBatch spriteBatch;
        private void Red_Render(object sender, MerjTek.WpfIntegration.GraphicsDeviceEventArgs e)
        {
            spriteBatch.Begin();
            
            spriteBatch.End();
        }

        #endregion
 
    }
}