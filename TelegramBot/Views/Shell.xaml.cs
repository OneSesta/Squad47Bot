namespace TelegramBot.Views
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using TelegramBot.Common;
    using TelegramBot.ViewModels;
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Shell : Window
    {
        public Shell(ShellViewModel wm)
        {
            DataContext = wm;
            InitializeComponent();
            Title = Utils.BotTitle;

            // Easy moving of window if dragged outside ony element
            ShellWindow.MouseDown += (o, e) =>
            {
                if (Mouse.LeftButton == MouseButtonState.Pressed)
                    this.DragMove();
            };

            // Same for menu DockPanel
            MenuDockPanel.MouseDown += (o, e) =>
            {
                if (Mouse.LeftButton == MouseButtonState.Pressed)
                    this.DragMove();
            };
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }


    }

}
