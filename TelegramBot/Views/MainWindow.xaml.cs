namespace TelegramBot.Views
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using TelegramBot.Models;
    using TelegramBot.ViewModels;
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DataContext = new MainWindowViewModel();
            InitializeComponent();
            Title = Utils.BotTitle;

            // Easy moving of window if dragged outside ony element
            MainWindowView.MouseDown += (o, e) =>
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

            // Scroll to end on new log entry
            LogTextBox.TextChanged += (o, e) =>
            {
                (o as TextBox)?.ScrollToEnd();
            };
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }


    }

}
