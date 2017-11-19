namespace TelegramBot.Views
{
    using System;
    using System.Windows;
    using System.Windows.Input;
    /// <summary>
    /// Логика взаимодействия для AboutWindow.xaml
    /// </summary>
    public partial class AboutWindow : Window
    {
        public AboutWindow()
        {
            InitializeComponent();
            IsClosed = false;

            AboutWindowView.MouseDown += (o, e) =>
            {
                if (Mouse.LeftButton == MouseButtonState.Pressed)
                    this.DragMove();
            };
            CloseButton.Click += (o, e) =>
            {
                Close();
            };
        }

        public bool IsClosed { get; private set; }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            IsClosed = true;
        }
    }
}
