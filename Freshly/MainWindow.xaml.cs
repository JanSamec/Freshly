using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Linq;

namespace Freshly
{
    public partial class MainWindow : Window
    {
        private const double BaseWidth = 550;
        private const double BaseHeight = 450;
        private const double MinScaleFactor = 0.4;
        private const double MaxScaleFactor = 1.2;
        private const double MinNavMenuWidth = 120;
        private const double MaxNavMenuWidth = 400;
        private const double MinLogoTitleSpacing = 15;
        private const double MaxLogoTitleSpacing = 15;

        public MainWindow()
        {
            InitializeComponent();
            var screenWidth = SystemParameters.PrimaryScreenWidth;
            var screenHeight = SystemParameters.PrimaryScreenHeight;
            this.Width = screenWidth * 0.4;
            this.Height = screenHeight * 0.4;
            UpdateResponsiveLayout(ActualWidth, ActualHeight);
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateResponsiveLayout(e.NewSize.Width, e.NewSize.Height);
        }

        private void UpdateResponsiveLayout(double width, double height)
        {
            double widthScaleFactor = Math.Max(MinScaleFactor, Math.Min(MaxScaleFactor, width / BaseWidth));
            double heightScaleFactor = Math.Max(MinScaleFactor, Math.Min(MaxScaleFactor, height / BaseHeight));
            double scaleFactor = (widthScaleFactor + heightScaleFactor) / 2;

            ScaleTransform logoScale = (ScaleTransform)this.Resources["LogoScaleTransform"];
            logoScale.ScaleX = logoScale.ScaleY = scaleFactor;

            ScaleTransform titleScale = (ScaleTransform)this.Resources["TitleScaleTransform"];
            titleScale.ScaleX = titleScale.ScaleY = scaleFactor;

            if (Resources.Contains("WindowControlsScaleTransform"))
            {
                var windowControlsTransform = (ScaleTransform)Resources["WindowControlsScaleTransform"];
                windowControlsTransform.ScaleX = windowControlsTransform.ScaleY =
                    Math.Max(MinScaleFactor, Math.Min(1.2, scaleFactor));
            }

            if (Resources.Contains("NavMenuScaleTransform"))
            {
                var navMenuTransform = (ScaleTransform)Resources["NavMenuScaleTransform"];
                navMenuTransform.ScaleX = navMenuTransform.ScaleY = scaleFactor;
            }

            if (this.FindName("NavColumnDefinition") is ColumnDefinition navColumnDefinition)
            {
                double navColumnWidth = MinNavMenuWidth + ((MaxNavMenuWidth - MinNavMenuWidth) * (scaleFactor - MinScaleFactor) / (MaxScaleFactor - MinScaleFactor));
                navColumnDefinition.Width = new GridLength(Math.Max(0.22 * 0.8, (0.18 + (scaleFactor - MinScaleFactor) * 0.1) * 0.8), GridUnitType.Star);
            }


            double baseSpacing = 50;
            double scaledSpacing = baseSpacing * scaleFactor;
            Resources["LogoTitleSpacing"] = new Thickness(scaledSpacing, 8, 0, 0);


            Resources["ButtonMargin"] = new Thickness(0, 0, 0, 0);


            Resources["WindowControlsMargin"] = new Thickness(0, 0, 2 * scaleFactor, 0);

            if (SideNavMenu != null && SideNavMenu.Child is StackPanel mainPanel)
            {
                SideNavMenu.MinWidth = MinNavMenuWidth * scaleFactor * 0.8;
                SideNavMenu.MaxWidth = MaxNavMenuWidth * scaleFactor * 0.8;
                SideNavMenu.MaxHeight = height * 0.68;
                
            }
            
        }
    }
}