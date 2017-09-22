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

namespace Sudoku_wpf
{
    /// <summary>
    /// UserControl.xaml 的交互逻辑
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        public UserControl1(int r,int c)
        {
            row = r;
            colume = c;
            InitializeComponent();
        }
        public int row = 0;
        public int colume = 0;
        Unit unit = null;
        public void setUint(Unit u)
        {
            unit = u;
            Refresh(false);
        }
        public void Refresh(bool showList)
        {
            if (unit.IsFixed == true)
            {
                textBlock.FontSize = 20;
                textBox.FontSize = 20;
                textBox.Text = unit.value.ToString();
                textBlock.Text = unit.value.ToString();
            }
            else 
            {
                if (showList)
                {
                    try
                    {
                        StringBuilder sb = new StringBuilder();
                        foreach (int i in unit.available_temp)
                        {
                            sb.Append(i.ToString());
                        }
                        textBlock.FontSize = 12;
                        textBox.FontSize = 12;
                        textBlock.TextWrapping = TextWrapping.Wrap;
                        textBox.Text = sb.ToString();
                        textBlock.Text = textBox.Text;
                    }
                    catch (Exception)
                    { }

                }
                else
                {
                    textBox.Text = "";
                    textBlock.Text = textBox.Text;
                }
            }
        }
        public void SetNotFocus()
        {
            textBox.Visibility = Visibility.Collapsed;
            textBlock.Visibility = Visibility.Visible;
            if (textBox.Text.Length > 0)
            {
                char c = textBox.Text.ToCharArray()[0];
                if (c >= '1' && c <= '9')
                {
                    unit?.SetValue(int.Parse(textBox.Text.Substring(0, 1)), true);
                }
            }
        }
        private void textBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            char c = e.Text.ToCharArray()[0];
            if (c >= '0' && c <= '9')
            {
                textBox.Text = c.ToString();
                SetNotFocus();
                e.Handled = true;
            }
            else
            {
                e.Handled = true;
            }
        }
        private void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            textBox.Visibility = Visibility.Visible;
            textBlock.Visibility = Visibility.Collapsed;
            textBox.Focus();

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Thickness th = new Thickness(0.5,0.5,0,0);
            if (colume == 0)
            {
                th.Left = 1;
            }
            else if (colume == 2)
            {
                th.Right = 1;
            }
            else if (colume == 5)
            {
                th.Right = 1;
            }

            if (row == 0)
            {
                th.Top = 1;
            }
            else if (row == 2)
            {
                th.Bottom = 1;
            }
            else if (row == 5)
            {
                th.Bottom = 1;
            }
            this.BorderThickness = th;
            this.BorderBrush = new SolidColorBrush(Colors.Black);
        }
    }
}
