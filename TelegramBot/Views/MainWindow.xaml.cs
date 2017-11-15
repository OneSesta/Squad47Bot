namespace TelegramBot.Views
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Input;
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
#if DEBUG
            Title = "Squad 47 Bot: DEBUG";
#else
            Title = "Squad 47 Bot: RELEASE"; 
#endif

            // Easy moving of window if dragged outside ony element
            MainWindowView.MouseDown += (o, e) =>
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
        
    }

}
