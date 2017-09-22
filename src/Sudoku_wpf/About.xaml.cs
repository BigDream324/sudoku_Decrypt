using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Shapes;

namespace Sudoku_wpf
{
    /// <summary>
    /// About.xaml 的交互逻辑
    /// </summary>
    public partial class About : Window
    {
        public About()
        {
            InitializeComponent();
        }
        

        private void Hyperlink_Github_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://github.com/wmx313880747");

        }

        private void Hyperlink_Email_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("mailto:313880747@qq.com");
        }
    }
}
