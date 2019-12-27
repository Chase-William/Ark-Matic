using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Timers;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Interop;
using MouseCursor = System.Windows.Forms.Cursor;
using Point = System.Drawing.Point;
using System.Windows;

namespace Ark_Matic.Pages
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page, INotifyPropertyChanged
    {        
        // Delay between clicks
        const int DELAY_BETWEEN_CLICKS = 300;
        // Determines the interval of clicks (auto clicker)

        public static System.Timers.Timer clickTimer = new System.Timers.Timer(DELAY_BETWEEN_CLICKS);
        
        // Determine the steps performed when the window is deactivated
        bool captureSearchBarLoc, captureDropAllBtnLoc;

        #region Screen Point Properties

        public Point CurrentCursorLocation => MouseCursor.Position;

        private Point searchBarLocation;
        public Point SearchBarLocation
        {
            get { return searchBarLocation; }
            set
            {
                searchBarLocation = value;
                OnPropertyChanged(nameof(SearchBarLocation));
            }
        }

        private Point dropAllBtnLocation;
        public Point DropAllBtnLocation
        {
            get { return dropAllBtnLocation; }
            set
            {
                dropAllBtnLocation = value;
                OnPropertyChanged(nameof(DropAllBtnLocation));
            }
        }

        #endregion      

        public MainPage()
        {
            InitializeComponent();
            DataContext = this;
            clickTimer.Elapsed += TimerElapsed;
            MainWindow.OnDeactivatedExtension += delegate
            {
                if (captureDropAllBtnLoc)
                {
                    DropAllBtnLocation = CurrentCursorLocation;
                    captureDropAllBtnLoc = false;
                }
                else if (captureSearchBarLoc)
                {
                    SearchBarLocation = CurrentCursorLocation;
                    captureSearchBarLoc = false;
                }
            };
        }

        /// <summary>
        ///     Bindings required for WPF Interface 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string proprtyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(proprtyName));
        }

        /// <summary>
        ///     Triggers the simulation of a mouse click
        /// </summary>
        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            OnPropertyChanged(nameof(CurrentCursorLocation));
            VirtualKeyUtil.PerformMouseClick(CurrentCursorLocation);
        }

        

        #region ALL UI Event Handlers

        /// <summary>
        ///     Starts the auto clicker
        /// </summary>
        public void StartClicker(object sender, RoutedEventArgs e)
        {
            clickTimer.Start();
        }

        /// <summary>
        ///     Stops the auto clicker
        /// </summary>
        public void StopClicker(object sender, RoutedEventArgs e)
        {
            clickTimer.Stop();
        }

        /// <summary>
        ///     Drops all the non-desired items from the inventory
        /// </summary>
        public async void DropAllButMetalAsync(object sender, RoutedEventArgs e)
        {
            await Task.Run(() =>
            {
                OpenInventory();

                // Drop:
                DropAllOfAnItem("Hide");
                // Drop:
                DropAllOfAnItem("Polymer");
                // Drop:
                DropAllOfAnItem("Paste");
                // Drop:
                DropAllOfAnItem("Element");

                CloseInventory();
            });
        }

        private void OpenInventory()
        {
            VirtualKeyUtil.PerformKeyboardClick(Keys.F);

            // Loading the inventory takes time
            Thread.Sleep(500);
        }

        private void CloseInventory()
        {
            VirtualKeyUtil.PerformKeyboardClick(Keys.ESC);
        }

        /// <summary>
        ///     Drops every all instances of a specified item from an inventory
        /// </summary>
        private void DropAllOfAnItem(string _itemName)
        {
            VirtualKeyUtil.PerformMouseClick(SearchBarLocation);
            // Delay allows the game to have time to register the focus
            Thread.Sleep(500);

            // Writing each individual character into the search bar
            foreach (char character in _itemName.ToUpper())
            {
                VirtualKeyUtil.PerformKeyboardClick((Keys)character);
            }

            // Delay allows the game to register the key input
            Thread.Sleep(500);

            VirtualKeyUtil.PerformMouseClick(DropAllBtnLocation);
            // Delay allows the drop button click to be registered properly
            Thread.Sleep(500);
        }

        /// <summary>
        ///     Sets boolean state to capture a click location and store it as the dropAllBtn's Location       
        /// </summary>
        public void SetDropAllBtnLocation(object sender, EventArgs e)
        {
            captureDropAllBtnLoc = true;
        }

        /// <summary>
        ///     Sets a boolean state to capture a click location and store it as the SearchBar's Location
        /// </summary>
        public void SetSearchBarBtnLocation(object sender, EventArgs e)
        {
            captureSearchBarLoc = true;
        }

        #endregion   
    }
}
