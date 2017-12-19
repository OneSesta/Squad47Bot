using PersonsInfoModule.ViewModels;
using System;
using System.Collections.Generic;
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

namespace PersonsInfoModule.Views
{
    /// <summary>
    /// Логика взаимодействия для PersonsInfoView.xaml
    /// </summary>
    public partial class PersonsInfoView : UserControl
    {
        public PersonsInfoView(PersonsInfoViewModel vm)
        {
            DataContext = vm;
            InitializeComponent();
        }
    }
}
