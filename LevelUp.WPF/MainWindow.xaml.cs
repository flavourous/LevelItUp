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
using Xamarin.Forms;
using Xamarin.Forms.Platform.WPF;
using System.Windows.Shapes;
using MvvmCross.Forms.Platforms.Wpf.Views;

namespace LevelUp.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MvxFormsWindowsPage
    {
        public MainWindow()
        {
            InitializeComponent();
            Forms.Init();
            //LoadApplication(new LevelItUp.XamarinForms.App());
        }
    }
}
