using ModernWpf.Controls;
using Ookii.Dialogs.Wpf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
using Path = System.IO.Path;

namespace LauncherV
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SolidColorBrush _greenSolidColorBrush;
        private SolidColorBrush _redSolidColorBrush;

        public MainWindow()
        {
            InitializeComponent();

            _greenSolidColorBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#389B13");
            _redSolidColorBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#BD1010");

            UpdateInstallLabels();
            UpdateUIPathNotSet();
            HandleFirstRun();

            if (IsInstallPathSet())
            {
                // do this check: if either scripthv, scripthvdotnet, asiloader or openiv is installed -> show disable btn, else enable
                // use a dedicated folder, like LauncherVDisabled

                //1. read install dir
                //2. check above comment
                //3. update btn ui accordingly
                //4. based on toggle do shit
            }
        }

        private async void HandleFirstRun()
        {
            const string STEAM_INSTALL_LOCATION = @"C:\Program Files (x86)\Steam\steamapps\common\Grand Theft Auto V";

            if (!Properties.Settings.Default.FirstRun)
                return;

            if (IsValidPath(STEAM_INSTALL_LOCATION))
            {
                ContentDialog contentDialog = new ContentDialog()
                {
                    Title = "Detected GTA V install location (steam version)",
                    Content = "Do you want this app to use this directory?",
                    IsPrimaryButtonEnabled = true,
                    DefaultButton = ContentDialogButton.Primary,
                    PrimaryButtonText = "Yes",
                    CloseButtonText = "No"
                };
                var result = await contentDialog.ShowAsync();
                if (result == ContentDialogResult.Primary)
                {
                    SetInstallPath(STEAM_INSTALL_LOCATION);
                    UpdateInstallLabels();
                    UpdateUIGameRunning();
                    UpdateUIPathNotSet();
                }
            }

            Properties.Settings.Default.FirstRun = false;
            Properties.Settings.Default.Save();
        }

        private string GetInstallPath() => Properties.Settings.Default.InstallPath;

        private bool IsInstallPathSet() => !string.IsNullOrEmpty(GetInstallPath());

        private void SetInstallPath(string path)
        {
            Properties.Settings.Default.InstallPath = path;
            Properties.Settings.Default.Save();
        }

        private void SelectPathButton_Click(object sender, RoutedEventArgs e)
        {
<<<<<<< HEAD

=======
            // Extremely nice (original) Windows folder browser achieved with Ookii Dialogs
            // Github: https://github.com/ookii-dialogs/ookii-dialogs-wpf

            VistaFolderBrowserDialog dialog = new VistaFolderBrowserDialog
            {
                Description = "Select GTA V location",
                UseDescriptionForTitle = true
            };

            if ((bool)dialog.ShowDialog(this))
            {
                if (IsValidPath(dialog.SelectedPath))
                {
                    SetInstallPath(dialog.SelectedPath);
                    UpdateInstallLabels();
                    UpdateUIPathNotSet();
                    UpdateUIGameRunning();
                }
                else
                {
                    MessageBox.Show(this, "GTA5.exe not found. Make sure you selected the right directory.\n\n" + "Selected Directory: " + dialog.SelectedPath, "Notice");
                }
                //if probblem, these were here before
                //UpdateUIPathNotSet();
                //UpdateUIGameRunning();
            }
>>>>>>> parent of 3d4c8be (Implemented row and column definitions)
        }

        private bool IsValidPath(string path)
        {
            return Directory.GetFiles(path, "*.*", SearchOption.TopDirectoryOnly).Contains(path + "\\GTA5.exe");
        }

        private void UpdateUIPathNotSet()
        {
            if (IsInstallPathSet())
            {
                pathNotSetTextBlock.Text = "";
                selectInstallPathButton.Content = "Change";
                toggleModsToggleButton.IsEnabled = true;
<<<<<<< HEAD
                openInstallPathAppBarButton.IsEnabled = true;
                //resetSettingsAppBarButton.IsEnabled = true;
=======
                openInstallPathButton.IsEnabled = true;
                settingsAppBarButton.IsEnabled = true;
>>>>>>> parent of 3d4c8be (Implemented row and column definitions)
                launchGameButton.IsEnabled = true;
                installStatusTextBlock.Visibility = Visibility.Visible;
                openInstallPathButton.Visibility = Visibility.Visible;
                string toggleButtonText = Properties.Settings.Default.ModsEnabled ? "Disable" : "Enable";
                toggleModsToggleButton.Content = toggleButtonText;
            }
            else
            {
                pathNotSetTextBlock.Text = "Please select your GTA V install location first";
                selectInstallPathButton.Content = "Select";
                toggleModsToggleButton.IsEnabled = false;
<<<<<<< HEAD
                openInstallPathAppBarButton.IsEnabled = false;
                //resetSettingsAppBarButton.IsEnabled = false;
=======
                openInstallPathButton.IsEnabled = false;
                settingsAppBarButton.IsEnabled = false;
>>>>>>> parent of 3d4c8be (Implemented row and column definitions)
                launchGameButton.IsEnabled = false;
                installStatusTextBlock.Visibility = Visibility.Hidden;
                openInstallPathButton.Visibility = Visibility.Hidden;
                toggleModsToggleButton.Content = "Disable";
            }
        }

        private void UpdateInstallLabels()
        {
            // OMG I didn't know this could be used... wtf, my whole life I've nested code inside if's when
            // these simple 'guards' can be used #blessed

            // TODO ask teacher if this is ok to use
            if (!IsInstallPathSet())
            {
                ClearInstallLabels();
                return;
            }

            var extensions = new List<string> { ".asi", ".xml", ".dll" };
            string[] files = Directory.GetFiles(GetInstallPath(), "*.*", SearchOption.TopDirectoryOnly)
                .Where(file => extensions.IndexOf(Path.GetExtension(file)) >= 0).ToArray();

            if (files.Contains(GetInstallPath() + "\\dinput8.dll"))
            {
<<<<<<< HEAD
                asiLoaderInstallStatusTextBlock.Text = "\u2714";
=======
                asiLoaderInstallStatusTextBlock.Text = "\u2714 ASI Loader (dinput8.dll)";
>>>>>>> parent of 3d4c8be (Implemented row and column definitions)
                asiLoaderInstallStatusTextBlock.Foreground = _greenSolidColorBrush;
            }
            else
            {
                asiLoaderInstallStatusTextBlock.Text = "\u274C ASI Loader (dinput8.dll)";
                asiLoaderInstallStatusTextBlock.Foreground = _redSolidColorBrush;
            }

            if (files.Contains(GetInstallPath() + "\\ScriptHookV.dll"))
            {
                scriptHookVInstallStatusTextBlock.Text = "\u2714 ScriptHookV (ScriptHookV.dll)";
                scriptHookVInstallStatusTextBlock.Foreground = _greenSolidColorBrush;
            }
            else
            {
                scriptHookVInstallStatusTextBlock.Text = "\u274C ScriptHookV (ScriptHookV.dll)";
                scriptHookVInstallStatusTextBlock.Foreground = _redSolidColorBrush;
            }

            if (files.Contains(GetInstallPath() + "\\ScriptHookVDotNet.asi"))
            {
                scriptHookVDotNetInstallStatusTextBlock.Text = "\u2714 ScriptHookVDotNet (ScriptHookVDotNet.asi)";
                scriptHookVDotNetInstallStatusTextBlock.Foreground = _greenSolidColorBrush;
            }
            else
            {
                scriptHookVDotNetInstallStatusTextBlock.Text = "\u274C ScriptHookVDotNet (ScriptHookVDotNet.asi)";
                scriptHookVDotNetInstallStatusTextBlock.Foreground = _redSolidColorBrush;
            }

            if (files.Contains(GetInstallPath() + "\\OpenIV.asi"))
            {
                openIvInstallStatusTextBlock.Text = "\u2714 OpenIV ASI Plugin (OpenIV.asi)";
                openIvInstallStatusTextBlock.Foreground = _greenSolidColorBrush;
            }
            else
            {
                openIvInstallStatusTextBlock.Text = "\u274C OpenIV ASI Plugin (OpenIV.asi)";
                openIvInstallStatusTextBlock.Foreground = _redSolidColorBrush;
            }
        }

        private void ClearInstallLabels()
        {
            // TODO ask teacher of het beter op dees manier is, dus de UI een placeholder geven en clearen als nodig bij opstarten.
            asiLoaderInstallStatusTextBlock.Text = "";
            scriptHookVInstallStatusTextBlock.Text = "";
            scriptHookVDotNetInstallStatusTextBlock.Text = "";
            openIvInstallStatusTextBlock.Text = "";

            //toolsStackPanel.Visibility = Visibility.Hidden;
        }

        private void UpdateUIGameRunning()
        {
            if (!GameIsRunning())
            {
                launchGameButton.IsEnabled = true;
                launchGameButton.Content = "Launch GTA";
                //selectInstallPathButton.IsEnabled = true;
                toggleModsToggleButton.IsEnabled = true;
            }
            else
            {
                launchGameButton.IsEnabled = false;
                launchGameButton.Content = "Running...";
                //selectInstallPathButton.IsEnabled = false;
                toggleModsToggleButton.IsEnabled = false;
            }
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            UpdateInstallLabels();
            UpdateUIGameRunning();
            UpdateUIPathNotSet();
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            //Title = "Inactive Window";
        }
        private void ToggleModsToggleButton_Click(object sender, RoutedEventArgs e)
        {
            //string folderName = @"c:\Top-Level Folder";
            //string pathString = System.IO.Path.Combine(folderName, "SubFolder");
            //Directory.CreateDirectory(pathString);

            switch (toggleModsToggleButton.IsChecked)
            {
                case true:
                    MessageBox.Show("toggle is checked");
                    break;
                case false:
                    MessageBox.Show("toggle is not checked");
                    break;
            }
        }

        private void OpenInstallPathButton_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(GetInstallPath());
        }

        private void ResetUserSettings()
        {
            Properties.Settings.Default.Reset();
        }

<<<<<<< HEAD
        private async void ResetSettingsButton_Click(object sender, RoutedEventArgs e)
=======
        private void SettingsButton_Click(object sender, RoutedEventArgs e)
>>>>>>> parent of 3d4c8be (Implemented row and column definitions)
        {
            try
            {
                ContentDialog contentDialog = new ContentDialog()
                {
                    Title = "Confirmation",
                    Content = "You're about to reset LauncherV\'s settings",
                    IsPrimaryButtonEnabled = true,
                    DefaultButton = ContentDialogButton.Primary,
                    PrimaryButtonText = "Reset",
                    CloseButtonText = "Dismiss"
                };
                var result = await contentDialog.ShowAsync();
                if (result == ContentDialogResult.Primary)
                {
                    ResetUserSettings();
                    UpdateInstallLabels();
                    UpdateUIPathNotSet();
                    HandleFirstRun();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Settings couldn't be reset.");
            }
        }

        private void LaunchGameButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start("steam://rungameid/271590");
            }
            catch (Exception ex)
            {
                // TODO change the messageboxes to modernUI
                MessageBox.Show($"Couldn't start GTA V.\n\n{ex.Message}");
            }
        }

        private bool GameIsRunning()
        {
            return Process.GetProcessesByName("GTA5").Length > 0;
        }

<<<<<<< HEAD
        private void AboutAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("simple about me dialog");
        }

        private void ExitAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void SelectInstallPathAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            // Extremely nice (original) Windows folder browser achieved with Ookii Dialogs
            // Github: https://github.com/ookii-dialogs/ookii-dialogs-wpf

            VistaFolderBrowserDialog dialog = new VistaFolderBrowserDialog
            {
                Description = "Select GTA V location",
                UseDescriptionForTitle = true
            };

            if ((bool)dialog.ShowDialog(this))
            {
                if (IsValidPath(dialog.SelectedPath))
                {
                    SetInstallPath(dialog.SelectedPath);
                    UpdateInstallLabels();
                    UpdateUIGameRunning();
                    UpdateUIPathNotSet();
                }
                else
                {
                    MessageBox.Show(this, "GTA5.exe not found. Make sure you selected the right directory.\n\n" + "Selected Directory: " + dialog.SelectedPath, "Notice");
                }
                //if probblem, these were here before
                //UpdateUIGameRunning();
                //UpdateUIPathNotSet();
            }
        }

        private void OpenInstallPathAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(GetInstallPath());
        }
=======
>>>>>>> parent of 3d4c8be (Implemented row and column definitions)
    }
}
