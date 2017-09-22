using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Sudoku_wpf
{
    public class Unit
    {
        public int value;
        public List<int> available;
        public List<int> available_temp;
        public bool IsFixed;
        public int row;
        public int colume;
        public Unit(int r,int c,int v)
        {
            row = r;
            colume = c;
            value = v;
            available = new List<int>();
            available.Add(1);
            available.Add(2);
            available.Add(3);
            available.Add(4);
            available.Add(5);
            available.Add(6);
            available.Add(7);
            available.Add(8);
            available.Add(9);
            available_temp = new List<int>();
            available_temp.Add(1);
            available_temp.Add(2);
            available_temp.Add(3);
            available_temp.Add(4);
            available_temp.Add(5);
            available_temp.Add(6);
            available_temp.Add(7);
            available_temp.Add(8);
            available_temp.Add(9);
            
            IsFixed = false;
        }
        public bool SetValue(int v,bool Fixed)
        {
            Console.WriteLine("SetValue row:" + row.ToString() + " colume:" + colume.ToString() + " value:" + v.ToString());
            if (v == 0)
            {
                value = v;
                available = new List<int>();
                available.Add(1);
                available.Add(2);
                available.Add(3);
                available.Add(4);
                available.Add(5);
                available.Add(6);
                available.Add(7);
                available.Add(8);
                available.Add(9);
                available_temp = new List<int>();
                available_temp.Add(1);
                available_temp.Add(2);
                available_temp.Add(3);
                available_temp.Add(4);
                available_temp.Add(5);
                available_temp.Add(6);
                available_temp.Add(7);
                available_temp.Add(8);
                available_temp.Add(9);
                IsFixed = false;
            }
            else
            {
                value = v;
                IsFixed = Fixed;
                if (IsFixed == true)
                {
                    available.Clear();
                    available.Add(v);
                    available_temp.Clear();
                    available_temp.Add(v);
                }
            }
            return true;
        }
        public bool RemoveAvailable(int v)
        {
            //Console.WriteLine("RemoveAvailable row:" + row.ToString() + " colume:" + colume.ToString()+" value:"+v.ToString());
            if (available.Contains(v))
            {
                available.Remove(v);
                available_temp.Remove(v);
                return true;
            }
            else
            {
                return false;
            }
        }
        public void ResetAvailableTemp()
        {
            available_temp.Clear();
            foreach (var v in available)
            {
                available_temp.Add(v);
            }
        }
        public bool RemoveAvailableTemp(int v)
        {
            //Console.WriteLine("RemoveAvailableTemp row:" + row.ToString() + " colume:" + colume.ToString() + " value:" + v.ToString());
            if (available_temp.Contains(v))
            {
                available_temp.Remove(v);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    public class PointEx
    {
        public PointEx(int r, int c)
        {
            row = r;
            colume = c;
        }
        public int row;
        public int colume;
    }
    public class SudokuDecrypt
    {
        Unit[][] data;
        public List<int[][]> AnswerList = new List<int[][]>();
        public SudokuDecrypt()
        {
            data = new Unit[9][];
            for (int i = 0; i < 9; i++)
            {
                data[i] = new Unit[9];
                for (int j = 0; j < 9; j++)
                {
                    data[i][j] = new Unit(i,j,0);
                }
            }
        }
        public Unit getUnit(int i, int j)
        {
            return data[i][j];
        }
        public List<PointEx> GetRelatedPoint(int i, int j)
        {
            List<PointEx> pointList = new List<PointEx>();
            for (int k = 0; k < 9; k++)
            {
                if (data[i][k].IsFixed == false)
                {
                    if (k != j)
                    {
                        pointList.Add(new PointEx(i, k));
                    }
                }
                if (data[k][j].IsFixed == false)
                {
                    if (k != i)
                    {
                        pointList.Add(new PointEx(k, j));
                    }
                }
            }
            int max_m = 0, max_n = 0;
            int min_m = 0, min_n = 0;
            min_m = (i / 3) * 3;
            max_m = (i / 3) * 3 + 2;
            min_n = (j / 3) * 3;
            max_n = (j / 3) * 3 + 2;
            for (int temp_i = min_m; temp_i <= max_m; temp_i++)
            {
                for (int temp_j = min_n; temp_j <= max_n; temp_j++)
                {
                    if (i != temp_i && j != temp_j)
                    {
                        pointList.Add(new PointEx(temp_i, temp_j));
                    }
                }
            }
            return pointList;
        }
        public bool RemoveDouble(int r, int c, int value)
        {
            List<PointEx> pointList = GetRelatedPoint(r, c);
            foreach (PointEx p in pointList)
            {
                if (data[p.row][p.colume].available.Count == 1)
                {
                    if (data[p.row][p.colume].available[0] == value)
                    {
                        return false;
                    }
                    else
                    {
                        continue;
                    }
                }
                data[p.row][p.colume].RemoveAvailable(value);
                if (data[p.row][p.colume].available.Count == 0)//此数独无解
                {
                    return false;
                }
                else if (data[p.row][p.colume].available.Count == 1)
                {
                    bool res = RemoveDouble(p.row, p.colume, data[p.row][p.colume].available[0]);
                    if (res == false)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public bool TryRemoveDouble(int r, int c)
        {
            if (c > 8)
            {
                r = r + 1;
                c = 0;
            }
            if (r > 8)
            {
                return true;
            }
            //StringBuilder sb = new StringBuilder();
            //sb.Append("row:");
            //sb.Append(r);
            //sb.Append(" colume:");
            //sb.Append(c);
            //Console.WriteLine(sb);
            bool value_result = false;
            foreach (var v in data[r][c].available)
            {
                data[r][c].available_temp.Clear();
                data[r][c].available_temp.Add(v);
                List<PointEx> pointList = GetRelatedPoint(r, c);
                bool check_result = true;
                foreach (var p in pointList)
                {
                    if (data[p.row][p.colume].available_temp.Count == 1)
                    {
                        if (data[p.row][p.colume].available_temp[0] == v)
                        {
                            check_result = false;
                        }
                    }
                }
                if (check_result == false)
                {
                    continue;
                }
                else
                {
                    bool res = TryRemoveDouble(r, c + 1);
                    if (res == true)
                    {
                        //value_result = false;
                        if (r == 8 && c == 8)
                        {
                            SaveAnswer();
                        }
                    }
                }
            }
            if (value_result == false)
            {
                data[r][c].ResetAvailableTemp();
                if (r == 0 && c == 0)
                {
                    if (AnswerList.Count > 0)
                    {
                        return true;
                    }
                }

                return false;
            }
            return true;
        }
        private void SaveAnswer()
        {
            int[][] answer = new int[9][];
            for (int i = 0; i < 9; i++)
            {
                answer[i] = new int[9];
                for (int j = 0; j < 9; j++)
                {
                    if (data[i][j].available_temp.Count == 1)
                    {
                        answer[i][j] = data[i][j].available_temp[0];
                    }
                    else
                    {
                        //还不能保存答案
                        MessageBox.Show("Some problems with answer.");
                    }

                }
            }
            AnswerList.Add(answer);
        }
        public bool CheckQuestion()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (data[i][j].IsFixed == true)
                    {
                        List<PointEx> pointList = GetRelatedPoint(i, j);
                        foreach (var point in pointList)
                        {
                            if (data[point.row][point.colume].IsFixed == true && data[point.row][point.colume].value == data[i][j].value)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }
        public bool StartDecrypt()
        {
            //先根据条件来筛选
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (data[i][j].IsFixed == true)
                    {
                        bool res = RemoveDouble(i, j, data[i][j].value);
                        if (res == false)
                        {
                            return false;
                        }
                    }
                }
            }
            //递归去试
            return TryRemoveDouble(0, 0);
        }
    }
}
