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
    using TelegramBot.ViewModels;
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DataContext = new BotViewModel();
            InitializeComponent();
#if DEBUG
            Title = "Таможка Бот: DEBUG";
#else
            Title = "Таможка Бот: RELEASE";
#endif
        }
    }

}
