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
using System.Windows.Threading;

namespace Sudoku_wpf
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        SudokuDecrypt sudo =null;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 11; i++)
            {
                RowDefinition r = new RowDefinition();
                r.Height = new GridLength(30);
                m_grid.RowDefinitions.Add(r);

                ColumnDefinition c = new ColumnDefinition();
                c.Width = new GridLength(30);
                m_grid.ColumnDefinitions.Add(c);
            }
            m_Btn_New_Click(null, null);
        }

        private void m_Btn_New_Click(object sender, RoutedEventArgs e)
        {
            comboBox.SelectedIndex = -1;
            comboBox.Items.Clear();
            sudo = new SudokuDecrypt();
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (i == 0 && j != 0)
                    {
                        TextBlock t = new TextBlock();
                        t.VerticalAlignment = VerticalAlignment.Center;
                        t.HorizontalAlignment = HorizontalAlignment.Center;
                        t.Text = j.ToString();
                        m_grid.Children.Add(t);
                        Grid.SetRow(t, i);
                        Grid.SetColumn(t, j);
                    }
                    else if (i != 0 && j == 0)
                    {
                        TextBlock t = new TextBlock();
                        t.VerticalAlignment = VerticalAlignment.Center;
                        t.HorizontalAlignment = HorizontalAlignment.Center;
                        t.Text = i.ToString();
                        m_grid.Children.Add(t);
                        Grid.SetRow(t, i);
                        Grid.SetColumn(t, j);
                    }
                    else if (i == 0 && j == 0)
                    {
                    }
                    else
                    {
                        UserControl1 uc = new UserControl1(i - 1, j - 1);
                        m_grid.Children.Add(uc);
                        uc.setUint(sudo.getUnit(uc.row, uc.colume));
                        Grid.SetRow(uc, i);
                        Grid.SetColumn(uc, j);
                    }
                }
            }
        }
        private void m_Btn_Decrypt_Click(object sender, RoutedEventArgs e)
        {
            if (sudo.CheckQuestion() == false)
            {
                MessageBox.Show("Question is not available.");
                return;
            }
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(0.01);
            timer.Tick += (p, q) => {
                m_Btn_Refresh_Click(null, null);
            };
            Task.Factory.StartNew(() => {
                bool res = sudo.StartDecrypt();
                this.Dispatcher.BeginInvoke( DispatcherPriority.Normal,new Action(() =>
                {
                    if (res == true)
                    {
                        for (int i=0;i<sudo.AnswerList.Count;i++)
                        {
                            comboBox.Items.Add("Answer" + (i + 1).ToString());
                        }
                        if (comboBox.Items.Count != 0)
                        {
                            comboBox.SelectedIndex = 0;
                        }
                    }
                    else
                    {
                        MessageBox.Show("This Sudoku has no answer.");
                    }
                    timer.Stop();
                }));
            });
            timer.Start();
        }

        private void m_Btn_Refresh_Click(object sender, RoutedEventArgs e)
        {
            foreach (UIElement elem in m_grid.Children)
            {
                if (elem is UserControl1)
                {
                    UserControl1 uc = elem as UserControl1;
                    uc.Refresh(true);
                }
            }
        }

        private void m_Btn_Refresh_Click2(object sender, RoutedEventArgs e)
        {
            foreach (UIElement elem in m_grid.Children)
            {
                if (elem is UserControl1)
                {
                    UserControl1 uc = elem as UserControl1;
                    uc.Refresh(false);
                }
            }
        }

        private void m_Btn_Test_Click(object sender, RoutedEventArgs e)
        {
            m_Btn_New_Click(null, null);
            sudo.getUnit(0, 0).SetValue(8, true);
            sudo.getUnit(0, 1).SetValue(6, true);
            sudo.getUnit(0, 4).SetValue(7, true);

            sudo.getUnit(1, 0).SetValue(5, true);
            sudo.getUnit(1, 7).SetValue(3, true);
            sudo.getUnit(1, 8).SetValue(2, true);

            sudo.getUnit(3, 0).SetValue(9, true);
            sudo.getUnit(3, 2).SetValue(4, true);
            sudo.getUnit(3, 3).SetValue(7, true);
            sudo.getUnit(3, 5).SetValue(6, true);
            sudo.getUnit(3, 6).SetValue(8, true);

            sudo.getUnit(4, 0).SetValue(3, true);
            sudo.getUnit(4, 1).SetValue(8, true);
            sudo.getUnit(4, 2).SetValue(7, true);
            sudo.getUnit(4, 3).SetValue(4, true);
            sudo.getUnit(4, 4).SetValue(1, true);
            sudo.getUnit(4, 5).SetValue(2, true);
            sudo.getUnit(4, 6).SetValue(9, true);
            sudo.getUnit(4, 8).SetValue(5, true);

            sudo.getUnit(5, 0).SetValue(1, true);
            sudo.getUnit(5, 5).SetValue(3, true);
            sudo.getUnit(5, 6).SetValue(2, true);

            sudo.getUnit(6, 0).SetValue(6, true);
            sudo.getUnit(6, 1).SetValue(9, true);
            sudo.getUnit(6, 2).SetValue(2, true);
            sudo.getUnit(6, 3).SetValue(5, true);
            sudo.getUnit(6, 4).SetValue(3, true);
            sudo.getUnit(6, 5).SetValue(7, true);
            sudo.getUnit(6, 7).SetValue(4, true);

            sudo.getUnit(7, 2).SetValue(5, true);
            sudo.getUnit(7, 3).SetValue(1, true);
            sudo.getUnit(7, 4).SetValue(8, true);
            sudo.getUnit(7, 5).SetValue(4, true);
            sudo.getUnit(7, 6).SetValue(6, true);
            sudo.getUnit(7, 8).SetValue(9, true);

            sudo.getUnit(8, 0).SetValue(4, true);
            sudo.getUnit(8, 2).SetValue(8, true);
            sudo.getUnit(8, 3).SetValue(6, true);
            sudo.getUnit(8, 4).SetValue(2, true);
            sudo.getUnit(8, 5).SetValue(9, true);
            sudo.getUnit(8, 6).SetValue(3, true);
            sudo.getUnit(8, 8).SetValue(7, true);
            m_Btn_Refresh_Click2(null, null);
        }

        private void m_Btn_Test2_Click(object sender, RoutedEventArgs e)
        {
            m_Btn_New_Click(null, null);
            sudo.getUnit(0, 1).SetValue(6, true);
            sudo.getUnit(0, 6).SetValue(7, true);

            sudo.getUnit(1, 2).SetValue(4, true);
            sudo.getUnit(1, 8).SetValue(1, true);

            sudo.getUnit(2, 0).SetValue(1, true);
            sudo.getUnit(2, 7).SetValue(6, true);

            sudo.getUnit(3, 3).SetValue(1, true);
            sudo.getUnit(3, 4).SetValue(2, true);
            sudo.getUnit(3, 6).SetValue(4, true);
            sudo.getUnit(3, 7).SetValue(3, true);

            sudo.getUnit(4, 1).SetValue(7, true);

            sudo.getUnit(5, 2).SetValue(9, true);
            sudo.getUnit(5, 4).SetValue(6, true);
            sudo.getUnit(5, 8).SetValue(5, true);

            sudo.getUnit(6, 4).SetValue(8, true);
            sudo.getUnit(6, 5).SetValue(7, true);
            sudo.getUnit(6, 8).SetValue(2, true);

            sudo.getUnit(7, 1).SetValue(9, true);
            sudo.getUnit(7, 2).SetValue(7, true);
            sudo.getUnit(7, 5).SetValue(4, true);

            sudo.getUnit(8, 0).SetValue(2, true);
            sudo.getUnit(8, 4).SetValue(9, true);
            sudo.getUnit(8, 6).SetValue(5, true);
            sudo.getUnit(8, 8).SetValue(4, true);
            m_Btn_Refresh_Click2(null, null);
        }

        private void m_Btn_Test3_Click(object sender, RoutedEventArgs e)
        {
            m_Btn_New_Click(null, null);
            sudo.getUnit(0, 0).SetValue(8, true);
            sudo.getUnit(0, 3).SetValue(9, true);
            sudo.getUnit(0, 4).SetValue(5, true);
            sudo.getUnit(0, 6).SetValue(4, true);

            sudo.getUnit(1, 2).SetValue(4, true);
            sudo.getUnit(1, 5).SetValue(3, true);
            sudo.getUnit(1, 7).SetValue(9, true);

            sudo.getUnit(2, 1).SetValue(9, true);
            sudo.getUnit(2, 8).SetValue(7, true);

            sudo.getUnit(3, 1).SetValue(4, true);
            sudo.getUnit(3, 3).SetValue(7, true);
            sudo.getUnit(3, 4).SetValue(1, true);
            sudo.getUnit(3, 8).SetValue(6, true);

            sudo.getUnit(4, 0).SetValue(5, true);
            sudo.getUnit(4, 5).SetValue(9, true);
            sudo.getUnit(4, 6).SetValue(7, true);

            sudo.getUnit(5, 0).SetValue(7, true);
            sudo.getUnit(5, 5).SetValue(5, true);
            sudo.getUnit(5, 6).SetValue(1, true);
            
            sudo.getUnit(6, 1).SetValue(3, true);
            sudo.getUnit(6, 8).SetValue(2, true);

            sudo.getUnit(7, 2).SetValue(6, true);
            sudo.getUnit(7, 5).SetValue(1, true);
            sudo.getUnit(7, 7).SetValue(3, true);

            sudo.getUnit(8, 0).SetValue(1, true);
            sudo.getUnit(8, 3).SetValue(3, true);
            sudo.getUnit(8, 4).SetValue(8, true);
            sudo.getUnit(8, 6).SetValue(6, true);
            m_Btn_Refresh_Click2(null, null);
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBox.SelectedIndex == -1)
                return;
            int[][] answer = sudo.AnswerList[comboBox.SelectedIndex];
            foreach (UIElement elem in m_grid.Children)
            {
                if (elem is UserControl1)
                {
                    UserControl1 uc = elem as UserControl1;
                    uc.DisplayAnswer(answer[uc.row][uc.colume]);
                }
            }
        }
        private void button_About_Click(object sender, RoutedEventArgs e)
        {
            new About() { Owner=this}.ShowDialog();
        }
    }
}
