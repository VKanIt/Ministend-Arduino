﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;
using MySql.Data.MySqlClient;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Windows.Forms.DataVisualization.Charting;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        bool flag_light = false, flag_meteo = false, flag_vibr=false, flag_sound=false, flag_servo=false, flag_all=false;
        string name, fam, number_exp, group;
        double[] Massiv_Atom = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        DateTime date_current = new DateTime();
        string date = DateTime.Today.ToString();
        double t1=0,t2=0,t3=0,t4=0,t_all=0;
        double x1=0,x2=0,x3=0,x4 = 0,x_all=0;
        int number_light = 0, number_sound = 0, number_meteo = 0, number_vibr = 0,number_all=0,number_servo=0;
        int x_center = 0, y_center = 0;
        int radius = 70;
        int count = 0;
        float angle0 = 0;
        int n1 = 0,n2=0,n3=0,n4=0,n5=0;
        List<float> angles = new List<float>();
        SerialPort port = new SerialPort("COM5", 9600, Parity.None, 8, StopBits.One);
        MySqlConnection conn = new MySqlConnection("server=localhost;user=root;database=schema1;password=1234;");

        void Write_Command_Detector(double q1, double q2, double q3, double q4, double q5, double q6, double q7, double q8, double q9, double q10)
        {
            Massiv_Atom[0] = q1;
            Massiv_Atom[1] = q2;
            Massiv_Atom[2] = q3;
            Massiv_Atom[3] = q4;
            Massiv_Atom[4] = q5;
            Massiv_Atom[5] = q6;
            Massiv_Atom[6] = q7;
            Massiv_Atom[7] = q8;
            Massiv_Atom[8] = q9;
            Massiv_Atom[9] = q10;
        }
        void ServoStartingPosition()
        {
            string Command_Line = "";
            Write_Command_Detector(4, 1, 0, 0, 0, 0, 0, 0, 0, 0);
            for (int i = 0; i < Massiv_Atom.Length - 4; i++)
            {
                Command_Line += Massiv_Atom[i] + ";";
            }
            port.Write(Command_Line);
        }
        void check_number(KeyPressEventArgs e)
        {
                char number = e.KeyChar;
                if (!Char.IsDigit(number) && number != 8)
                {
                    e.Handled = true;
                }
        }
        void check_number_backspace(KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && number != 8 && (e.KeyChar <= 39 || e.KeyChar >= 46) && number != 47 && number != 61) //калькулятор
            {
                e.Handled = true;
            }
        }
        public Form1()
        {
            InitializeComponent();
            port.Open();
            conn.Open();
            string sql;
            MySqlCommand commandsql;
            
            CirclePaint(pictureBox3, false);
            CirclePaint(pictureBox6,false);
            CirclePaint(pictureBox12, false);
            CirclePaint(pictureBox13, false);
            CirclePaint(pictureBox15, false);
            CirclePaint(pictureBox17, false);
            //Кнопки выключения датчиков
            button3.Enabled = false;
            button1.Enabled = false;
            button4.Enabled = false;
            button6.Enabled = false;
            button40.Enabled = false;
            //Кнопки изменения периода датчиков
            button11.Enabled = false;
            button17.Enabled = false;
            button20.Enabled = false;
            button23.Enabled = false;
            button42.Enabled = false;
            //Кнопки запуска записи в БД
            button13.Enabled = false;
            button15.Enabled = false;
            button19.Enabled = false;
            button22.Enabled = false;
            button44.Enabled = false;
            //Кнопки остановки записи в БД
            button12.Enabled = false;
            button14.Enabled = false;
            button18.Enabled = false;
            button21.Enabled = false;
            button43.Enabled = false;
            //Поля для характеристик датчиков и сервопривода
            textBox1.ReadOnly = true;
            textBox3.ReadOnly = true;
            textBox5.ReadOnly = true;
            textBox7.ReadOnly = true;
            textBox13.ReadOnly = true;
            comboBox2.SelectedIndex = 0;
            string[] Separators = new string[] { ";" };
            //Очистка данных графика
            chart1.Series[0].Points.Clear();
            chart2.Series[0].Points.Clear();
            chart3.Series[0].Points.Clear();
            chart4.Series[0].Points.Clear();
            chart5.Series[0].Points.Clear();

            sql = "SELECT light FROM information;";
            commandsql = new MySqlCommand(sql, conn);
            string info_light = commandsql.ExecuteScalar().ToString();
            string[] info = info_light.Split(Separators, StringSplitOptions.None);
            foreach (string s in info)
            {
                textBox1.Text += s+ Environment.NewLine;
            }
            
            sql = "SELECT vibr FROM information;";
            commandsql = new MySqlCommand(sql, conn);
            string info_vibr = commandsql.ExecuteScalar().ToString();
            info = info_vibr.Split(Separators, StringSplitOptions.None);
            foreach (string s in info)
            {
                textBox3.Text += s + Environment.NewLine;
            }

            sql = "SELECT sound FROM information;";
            commandsql = new MySqlCommand(sql, conn);
            string info_sound = commandsql.ExecuteScalar().ToString();
            info = info_sound.Split(Separators, StringSplitOptions.None);
            foreach (string s in info)
            {
                textBox5.Text += s + Environment.NewLine;
            }

            sql = "SELECT meteo FROM information;";
            commandsql = new MySqlCommand(sql, conn);
            string info_meteo = commandsql.ExecuteScalar().ToString();
            info = info_meteo.Split(Separators, StringSplitOptions.None);
            foreach (string s in info)
            {
                textBox7.Text += s + Environment.NewLine;
            }

            sql = "SELECT servo FROM information;";
            commandsql = new MySqlCommand(sql, conn);
            string info_servo = commandsql.ExecuteScalar().ToString();
            info = info_servo.Split(Separators, StringSplitOptions.None);
            foreach (string s in info)
            {
                textBox13.Text += s + Environment.NewLine;
            }

            
            ServoStartingPosition();
            x_center = pictureBox1.Width / 2;
            y_center = pictureBox1.Height / 2+50;
            ServoPaint(0);
            angles.Add(0);
            comboBox1.SelectedIndex = 1;
            comboBox3.SelectedIndex = 3;

            //Принятие данных с министенда
            port.DataReceived += serialPort1_DataReceived;
            
        }
        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            string command = port.ReadLine();
            string[] stringSeparators = new string[] { ";" };
            string[] result;
            result = command.Split(stringSeparators, StringSplitOptions.None);
            date_current = DateTime.Now;
            string dc = date_current.ToString("s");
            string[] dateSeparators = new string[] { "T" };
            string[] datetime = dc.ToString().Split(dateSeparators, StringSplitOptions.None);
            int index_light = 0, index_sound = 0, index_meteo_1 = 0, index_meteo_2 = 0, index_vibr = 0;
            bool flagservo = false;

            //Если включен датчик света
            if (Massiv_Atom[0] == 1 && Massiv_Atom[1] == 0 && Massiv_Atom[2] == 0 && Massiv_Atom[3] == 1 && Massiv_Atom[4] == 0)
            {  
                
                if (flag_light == true)
                {
                    string sql = "insert into light values (default, " + result[4] + ",'" + textBox2.Text + "','" + datetime[0].Replace(".","-")+"','"+ datetime[1]+"',"+number_exp+ ",'" + name + "','" + fam + "','"+group+"'); ";
                    MySqlCommand commandsql = new MySqlCommand(sql, conn);
                    commandsql.ExecuteNonQuery();
                }
                index_light = 4;
                
            }
            //Если включен датчик звука
            if (Massiv_Atom[0] == 1 && Massiv_Atom[1] == 0 && Massiv_Atom[2] == 1 && Massiv_Atom[3] == 0 && Massiv_Atom[4] == 0)
            {
                if (flag_sound == true)
                {
                    string sql = "insert into sound values (default, " + result[3] + ",'" + textBox6.Text + "','" + datetime[0].Replace(".", "-") + "','" + datetime[1] + "'," + number_exp + ",'" + name + "','" + fam + "','" + group + "'); ";
                    MySqlCommand commandsql = new MySqlCommand(sql, conn);
                    commandsql.ExecuteNonQuery();
                }
                index_sound = 3;
            }
            //Если включен метеодатчик
            if (Massiv_Atom[0] == 1 && Massiv_Atom[1] == 1 && Massiv_Atom[2] == 0 && Massiv_Atom[3] == 0 && Massiv_Atom[4] == 0)
            {
                if (flag_meteo == true)
                {
                    string sql = "insert into meteo values (default," + result[1] + ", " + result[2] + ",'" + textBox8.Text + "','" + datetime[0].Replace(".", "-") + "','" + datetime[1] + "'," + number_exp + ",'" + name + "','" + fam + "','" + group + "'); ";
                    MySqlCommand commandsql = new MySqlCommand(sql, conn);
                    commandsql.ExecuteNonQuery();
                }
                index_meteo_1 = 1;
                index_meteo_2 = 2;
            }
            //Если включен датчик вибрации
            if (Massiv_Atom[0] == 1 && Massiv_Atom[1] == 0 && Massiv_Atom[2] == 0 && Massiv_Atom[3] == 0 && Massiv_Atom[4] == 1)
            {
                if (flag_vibr == true)
                {
                    string sql = "insert into vibration values (default, " + result[5] + ",'" + textBox4.Text + "','" + datetime[0].Replace(".", "-") + "','" + datetime[1] + "'," + number_exp + ",'" + name + "','" + fam + "','" + group + "'); ";
                    MySqlCommand commandsql = new MySqlCommand(sql, conn);
                    commandsql.ExecuteNonQuery();
                }
                index_vibr = 5;
            }
            //Если включены все датчики
            if (Massiv_Atom[0] == 1 && Massiv_Atom[1] == 1 && Massiv_Atom[2] == 1 && Massiv_Atom[3] == 1 && Massiv_Atom[4] == 1)
            {
                if (flag_all == true)
                {
                    string sql = "insert into light values (default, " + result[4] + ",'" + textBox33.Text + "','" + datetime[0].Replace(".", "-") + "','" + datetime[1] + "'," + number_exp + ",'" + name + "','" + fam + "','" + group + "');"+
                        "insert into sound values (default, " + result[3] + ",'" + textBox33.Text + "','" + datetime[0].Replace(".", "-") + "','" + datetime[1] + "'," + number_exp + ",'" + name + "','" + fam + "','" + group + "');"+
                        "insert into meteo values (default," + result[1] + ", " + result[2] + ",'" + textBox33.Text + "','" + datetime[0].Replace(".", "-") + "','" + datetime[1] + "'," + number_exp + ",'" + name + "','" + fam + "','" + group + "');"+
                        "insert into vibration values (default, " + result[5] + ",'" + textBox33.Text + "','" + datetime[0].Replace(".", "-") + "','" + datetime[1] + "'," + number_exp + ",'" + name + "','" + fam + "','" + group + "');";
                    MySqlCommand commandsql = new MySqlCommand(sql, conn);
                    commandsql.ExecuteNonQuery();
                }
                index_light = 4;
                index_sound = 3;
                index_meteo_1 = 1;
                index_meteo_2 = 2;
                index_vibr = 5;
            }
            //Если включен сервопривод
            if ((Massiv_Atom[0] == 2 || Massiv_Atom[0] == 3) && Massiv_Atom[1] == 1)
            {
                if (flag_servo == true)
                { 
                    string sql = "insert into servo values (default, " + result[1] + "," + result[2] + "," + result[3] + "," + result[4] + ",'" + datetime[0] + "','" + datetime[1] + "'," + number_exp + ",'" + name + "','" + fam + "','" + group + "'); ";
                    MySqlCommand commandsql = new MySqlCommand(sql, conn);
                    commandsql.ExecuteNonQuery();
                }
                flagservo = true;
            }
            if (IsHandleCreated)
            {
                BeginInvoke(new LineReceivedEvent(LineReceived), command, index_light, index_sound, index_meteo_1, index_meteo_2, index_vibr, result);
                BeginInvoke(new LineReceivedEvent1(LineReceived1), command, index_light, index_sound, index_meteo_1, index_meteo_2, index_vibr, datetime, result,flagservo);
                return;
            }
        }

        private delegate void LineReceivedEvent(string command, int index_light, int index_sound, int index_meteo_1, int index_meteo_2, int index_vibr, string[] result);
        private void LineReceived(string command, int index_light, int index_sound, int index_meteo_1, int index_meteo_2, int index_vibr, string[] result)
        {
            if (index_light == 4 && index_sound == 0 && index_meteo_1==0 && index_meteo_2 == 0 && index_vibr == 0)
            {
                n1++;
                chart1.Series[0].Points.AddXY(x1, result[index_light]);
                chart1.Series[0].Points[number_light].Label = "    "+(number_light+1).ToString();
                x1 += t1;

                if (chart1.ChartAreas[0].AxisX.Maximum >= 15)
                {
                    chart1.ChartAreas[0].AxisX.Minimum += t1;
                    chart1.ChartAreas[0].AxisX.Maximum += t1;

                }
            }
            if (index_light == 0 && index_sound == 3 && index_meteo_1 == 0 && index_meteo_2 == 0 && index_vibr == 0)
            {
                n2++;
                chart3.Series[0].Points.AddXY(x3, result[index_sound]);
                chart3.Series[0].Points[number_sound].Label = "    " + (number_sound + 1).ToString();
                x3 += t3;
                if (chart3.ChartAreas[0].AxisX.Maximum >= 15)
                {
                    chart3.ChartAreas[0].AxisX.Minimum += t3;
                    chart3.ChartAreas[0].AxisX.Maximum += t3;
                }
            }
            if (index_light == 0 && index_sound == 0 && index_meteo_1 == 1 && index_meteo_2 == 2 && index_vibr == 0)
            {
                n3++;
                chart4.Series[0].Points.AddXY(x4, result[index_meteo_1]);
                chart5.Series[0].Points.AddXY(x4, result[index_meteo_2]);
                chart4.Series[0].Points[number_meteo].Label = "    " + (number_meteo + 1).ToString();
                chart5.Series[0].Points[number_meteo].Label = "    " + (number_meteo + 1).ToString();

                x4 += t4;

                if (chart4.ChartAreas[0].AxisX.Maximum >= 15 && chart5.ChartAreas[0].AxisX.Maximum >= 15)
                {
                    chart4.ChartAreas[0].AxisX.Minimum += t4;
                    chart4.ChartAreas[0].AxisX.Maximum += t4;
                    chart5.ChartAreas[0].AxisX.Minimum += t4;
                    chart5.ChartAreas[0].AxisX.Maximum += t4;
                }
            }
            if (index_light == 0 && index_sound == 0 && index_meteo_1 == 0 && index_meteo_2 == 0 && index_vibr == 5)
            {
                n4++;
                chart2.Series[0].Points.AddXY(x2, result[index_vibr]);
                chart2.Series[0].Points[number_vibr].Label = "    " + (number_vibr + 1).ToString();
                x2 += t2;
                if (chart2.ChartAreas[0].AxisX.Maximum >= 15)
                {
                    chart2.ChartAreas[0].AxisX.Minimum += t2;
                    chart2.ChartAreas[0].AxisX.Maximum += t2;
                }
            }
            if (index_light == 4 && index_sound == 3 && index_meteo_1 == 1 && index_meteo_2 == 2 && index_vibr == 5)
            {
                n5++;
                chart8.Series[0].Points.AddXY(x_all, result[index_light]);
                chart7.Series[0].Points.AddXY(x_all, result[index_vibr]);
                chart9.Series[0].Points.AddXY(x_all, result[index_sound]);
                chart10.Series[0].Points.AddXY(x_all, result[index_meteo_1]);
                chart11.Series[0].Points.AddXY(x_all, result[index_meteo_2]);

                chart8.Series[0].Points[number_all].Label = "    " + (number_all + 1).ToString();
                chart7.Series[0].Points[number_all].Label = "    " + (number_all + 1).ToString();
                chart9.Series[0].Points[number_all].Label = "    " + (number_all + 1).ToString();
                chart10.Series[0].Points[number_all].Label = "    " + (number_all + 1).ToString();
                chart11.Series[0].Points[number_all].Label = "    " + (number_all + 1).ToString();

                x_all += t_all;

                if (chart8.ChartAreas[0].AxisX.Maximum >= 10 && chart9.ChartAreas[0].AxisX.Maximum >= 10 && chart7.ChartAreas[0].AxisX.Maximum >= 10 && chart10.ChartAreas[0].AxisX.Maximum >= 10 && chart11.ChartAreas[0].AxisX.Maximum >= 10)
                {
                    chart8.ChartAreas[0].AxisX.Minimum += t_all;
                    chart8.ChartAreas[0].AxisX.Maximum += t_all;
                    chart9.ChartAreas[0].AxisX.Minimum += t_all;
                    chart9.ChartAreas[0].AxisX.Maximum += t_all;
                    chart7.ChartAreas[0].AxisX.Minimum += t_all;
                    chart7.ChartAreas[0].AxisX.Maximum += t_all;
                    chart10.ChartAreas[0].AxisX.Minimum += t_all;
                    chart10.ChartAreas[0].AxisX.Maximum += t_all;
                    chart11.ChartAreas[0].AxisX.Minimum += t_all;
                    chart11.ChartAreas[0].AxisX.Maximum += t_all;
                }
            }
        }
        private delegate void LineReceivedEvent1(string command, int index_light, int index_sound, int index_meteo_1, int index_meteo_2, int index_vibr,string []datetime, string[] result, bool flagservo);

        private void LineReceived1(string command, int index_light, int index_sound, int index_meteo_1, int index_meteo_2, int index_vibr, string[] datetime, string []result, bool flagservo)
        {
            
            if (index_light == 4 && index_sound == 0 && index_meteo_1 == 0 && index_meteo_2 == 0 && index_vibr == 0)
            {
                number_light++;
                dataGridView1.Rows.Add(number_light.ToString(), result[index_light].ToString(), t1, datetime[0], datetime[1], textBox11.Text, textBox10.Text, textBox9.Text, textBox12.Text);
                if (dataGridView1.Rows.Count > 16)
                {
                    dataGridView1.Rows.RemoveAt(0);
                }
                
            }
            if (index_light == 0 && index_sound == 3 && index_meteo_1 == 0 && index_meteo_2 == 0 && index_vibr == 0)
            {
                number_sound++;
                dataGridView3.Rows.Add(number_sound.ToString(), result[index_sound].ToString(), t3, datetime[0], datetime[1], textBox24.Text, textBox23.Text, textBox22.Text, textBox21.Text);
                if (dataGridView3.Rows.Count > 16)
                {
                    dataGridView3.Rows.RemoveAt(0);
                }
            }
            if (index_light == 0 && index_sound == 0 && index_meteo_1 == 1 && index_meteo_2 == 2 && index_vibr == 0)
            {
                number_meteo++;
                dataGridView4.Rows.Add(number_meteo.ToString(), result[index_meteo_1].ToString(), result[index_meteo_2].ToString(), t4, datetime[0], datetime[1], textBox28.Text, textBox27.Text, textBox26.Text, textBox25.Text);
                if (dataGridView4.Rows.Count > 16)
                {
                    dataGridView4.Rows.RemoveAt(0);
                }
            }
            if (index_light == 0 && index_sound == 0 && index_meteo_1 == 0 && index_meteo_2 == 0 && index_vibr == 5)
            {
                number_vibr++;
                dataGridView2.Rows.Add(number_vibr.ToString(), result[index_vibr].ToString(), t2, datetime[0], datetime[1], textBox20.Text, textBox19.Text, textBox18.Text, textBox17.Text);
                if (dataGridView2.Rows.Count > 16)
                {
                    dataGridView2.Rows.RemoveAt(0);
                }
            }
            if (index_light == 4 && index_sound == 3 && index_meteo_1 == 1 && index_meteo_2 == 2 && index_vibr == 5)
            {
                number_all++;
                dataGridView6.Rows.Add(number_all.ToString(), result[index_light].ToString(), result[index_sound].ToString(), result[index_vibr].ToString(), result[index_meteo_1].ToString(), result[index_meteo_2].ToString(), t_all, datetime[0], datetime[1], textBox34.Text, textBox36.Text, textBox37.Text, textBox35.Text);
                if (dataGridView6.Rows.Count > 11)
                {
                    dataGridView6.Rows.RemoveAt(0);
                }
            }
            if (flagservo == true)
            {
                string[] napr = { "По часовой", "Против часовой" };
                count++;
                if (count > 1)
                {
                    number_servo++;
                    if(result[2].ToString() == "0")
                        dataGridView5.Rows.Add(number_servo.ToString(), result[1].ToString(), napr[0], result[3].ToString(), result[4].ToString(), datetime[0], datetime[1], textBox29.Text, textBox31.Text, textBox32.Text, textBox30.Text);
                    else
                        dataGridView5.Rows.Add(number_servo.ToString(), result[1].ToString(), napr[1], result[3].ToString(), result[4].ToString(), datetime[0], datetime[1], textBox29.Text, textBox31.Text, textBox32.Text, textBox30.Text);
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            chart1.Series[0].Points.Clear();
            chart1.ChartAreas[0].AxisX.Minimum = Double.NaN;
            chart1.ChartAreas[0].AxisX.Maximum = Double.NaN;
            x1 = 0;
            dataGridView1.Rows.Clear();
            number_light = 0;
            
            //chart1.Series[0]["PixelPointWidth"] = "30";
            if (textBox11.Text == "" || textBox10.Text == "" || textBox9.Text == "" || textBox12.Text == "")
            {
                MessageBox.Show("Введены не все данные эксперимента!");
                return;
            }
            if (textBox2.Text == "")
            {
                MessageBox.Show("Не введен период измерения!");
                return;
            }
            string Command_Line = "";
            button8.Enabled = false;
            button3.Enabled = true;
            button11.Enabled = true;
            button13.Enabled = true;
            t1 = double.Parse(textBox2.Text);
                
            chart1.Titles[0].Text = "График измерений датчика света (T = " + textBox2.Text + ")";
            label19.Text = date.Substring(0, date.Length - 7);
            CirclePaint(pictureBox6, true);
            CirclePaint(pictureBox12, false);
            CirclePaint(pictureBox13, false);
            CirclePaint(pictureBox15, false);
            name = textBox10.Text;
            fam = textBox9.Text;
            group = textBox12.Text;
            number_exp = textBox11.Text;
            Write_Command_Detector(1, 0, 0, 1, 0, 0, 0, double.Parse(textBox2.Text), 0, 0);
            for (int i = 0; i < Massiv_Atom.Length; i++)
            {
                Command_Line += Massiv_Atom[i] + ";";
            }
            port.Write(Command_Line);
        }

        private void CirclePaint(PictureBox picture, bool t)
        {
            picture.Image = new Bitmap(picture.Width, picture.Height);
            using (Graphics g = Graphics.FromImage(picture.Image))
            {
                int x = 0;
                int y = 0;
                int width = 30;
                int height = 30;
                g.DrawEllipse(Pens.Black, x, y, width, height);
                SolidBrush solidBrushRed = new SolidBrush(Color.FromArgb(255, 255, 0, 0));
                SolidBrush solidBrushGreen = new SolidBrush(Color.FromArgb(255, 0, 255, 0));
                if (t)
                    g.FillEllipse(solidBrushGreen, 0, 0, width, height);
                else
                    g.FillEllipse(solidBrushRed, 0, 0, width, height);
            }
        }
       
        private void button3_Click(object sender, EventArgs e)
        {
            string Command_Line = "";
            CirclePaint(pictureBox6, false);
            button8.Enabled = true;
            button3.Enabled = false;
            button11.Enabled = false;
            button13.Enabled = false;
            button12.Enabled = false;
            Write_Command_Detector(1, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            for (int i = 0; i < Massiv_Atom.Length; i++)
            {
                Command_Line += Massiv_Atom[i] + ";";
            }
            port.Write(Command_Line);
            n1 = 0;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            flag_light = false;
            button13.Enabled = true;
            button12.Enabled = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            chart3.Series[0].Points.Clear();
            chart3.ChartAreas[0].AxisX.Minimum = Double.NaN;
            chart3.ChartAreas[0].AxisX.Maximum = Double.NaN;
            x3 = 0;
            dataGridView3.Rows.Clear();
            number_sound = 0;

            //chart3.Series[0]["PixelPointWidth"] = "30";
            if (textBox24.Text == "" || textBox23.Text == "" || textBox22.Text == "" || textBox21.Text == "")
            {
                MessageBox.Show("Введены не все данные эксперимента!");
                return;
            }
            if (textBox6.Text == "")
            {
                MessageBox.Show("Не введен период измерения!");
                return;
            }
            string Command_Line = "";
            button4.Enabled = true;
            button5.Enabled = false;
            button20.Enabled = true;
            button19.Enabled = true;
            t3 = double.Parse(textBox6.Text);
            chart3.Titles[0].Text = "График измерений датчика звука (T = " + textBox6.Text+")";
            label34.Text = date.Substring(0, date.Length - 7);
            CirclePaint(pictureBox13, true);
            CirclePaint(pictureBox12, false);
            CirclePaint(pictureBox6, false);
            CirclePaint(pictureBox15, false);
            name = textBox23.Text;
            fam = textBox22.Text;
            group = textBox21.Text;
            number_exp = textBox24.Text;
            Write_Command_Detector(1, 0, 1, 0, 0, 0, double.Parse(textBox6.Text), 0, 0, 0);
            for (int i = 0; i < Massiv_Atom.Length; i++)
            {
                Command_Line += Massiv_Atom[i] + ";";
            }
            port.Write(Command_Line);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string Command_Line = "";
            button4.Enabled = false;
            button5.Enabled = true;
            button20.Enabled = false;
            button19.Enabled = false;
            button18.Enabled = false;
            CirclePaint(pictureBox13, false);
            Write_Command_Detector(1, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            for (int i = 0; i < Massiv_Atom.Length; i++)
            {
                Command_Line += Massiv_Atom[i] + ";";
            }
            port.Write(Command_Line);
            n2 = 0;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            chart4.Series[0].Points.Clear();
            chart5.Series[0].Points.Clear();
            chart4.ChartAreas[0].AxisX.Minimum = Double.NaN;
            chart4.ChartAreas[0].AxisX.Maximum = Double.NaN;
            chart5.ChartAreas[0].AxisX.Minimum = Double.NaN;
            chart5.ChartAreas[0].AxisX.Maximum = Double.NaN;
            x4 = 0;
            dataGridView4.Rows.Clear();
            number_meteo = 0;

            //chart4.Series[0]["PixelPointWidth"] = "30";
            //chart5.Series[0]["PixelPointWidth"] = "30";
            if (textBox28.Text == "" || textBox27.Text == "" || textBox26.Text == "" || textBox25.Text == "")
            {
                MessageBox.Show("Введены не все данные эксперимента!");
                return;
            }
            if (textBox8.Text == "")
            {
                MessageBox.Show("Не введен период измерения!");
                return;
            }
            string Command_Line = "";
            button6.Enabled = true;
            button7.Enabled = false;
            button23.Enabled = true;
            button22.Enabled = true;
            t4 = double.Parse(textBox8.Text);
            chart4.Titles[0].Text = "График измерений температуры метеодатчика (T = " + textBox8.Text+")";
            chart5.Titles[0].Text = "График измерений влажности метеодатчика (T = " + textBox8.Text + ")";
            label41.Text = date.Substring(0, date.Length - 7);
            CirclePaint(pictureBox3, false);
            CirclePaint(pictureBox15, true);
            CirclePaint(pictureBox12, false);
            CirclePaint(pictureBox13, false);
            CirclePaint(pictureBox6, false);
            name = textBox27.Text;
            fam = textBox26.Text;
            group = textBox25.Text;
            number_exp = textBox28.Text;
            Write_Command_Detector(1, 1, 0, 0, 0, double.Parse(textBox8.Text), 0, 0, 0, 0);
            for (int i = 0; i < Massiv_Atom.Length; i++)
            {
                Command_Line += Massiv_Atom[i] + ";";
            }
            port.Write(Command_Line);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string Command_Line = "";
            button6.Enabled = false;
            button7.Enabled = true;
            button23.Enabled = false;
            button22.Enabled = false;
            button21.Enabled = false;
            CirclePaint(pictureBox15, false);
            Write_Command_Detector(1, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            for (int i = 0; i < Massiv_Atom.Length; i++)
            {
                Command_Line += Massiv_Atom[i] + ";";
            }
            port.Write(Command_Line);
            n3 = 0;
        }

        private void button17_Click(object sender, EventArgs e)
        {
            string Command_Line = "";
            chart2.Titles[0].Text = "График измерений датчика вибрации (T = " + textBox4.Text+")";
            t2 = double.Parse(textBox4.Text);
            Write_Command_Detector(1, 0, 0, 0, 1, 0, 0, 0, double.Parse(textBox4.Text),  0);
            for (int i = 0; i < Massiv_Atom.Length; i++)
            {
                Command_Line += Massiv_Atom[i] + ";";
            }
            port.Write(Command_Line);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            flag_vibr = true;
            button14.Enabled = true;
            button15.Enabled = false;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            flag_vibr = false;
            button15.Enabled = true;
            button14.Enabled = false;
        }

        private void button20_Click(object sender, EventArgs e)
        {
            string Command_Line = "";
            chart3.Titles[0].Text = "График измерений датчика звука (T = " + textBox6.Text+")";
            t3 = double.Parse(textBox6.Text);
            Write_Command_Detector(1, 0, 1, 0, 0, 0, double.Parse(textBox6.Text), 0, 0, 0);
            for (int i = 0; i < Massiv_Atom.Length; i++)
            {
                Command_Line += Massiv_Atom[i] + ";";
            }
            port.Write(Command_Line);
        }

        private void button23_Click(object sender, EventArgs e)
        {
            string Command_Line = "";
            chart4.Titles[0].Text = "График измерений температуры метеодатчика (T = " + textBox8.Text + ")";
            chart5.Titles[0].Text = "График измерений влажности метеодатчика (T = " + textBox8.Text + ")";
            t4 = double.Parse(textBox8.Text);
            Write_Command_Detector(1, 1, 0, 0, 0, double.Parse(textBox8.Text), 0, 0, 0, 0);
            for (int i = 0; i < Massiv_Atom.Length; i++)
            {
                Command_Line += Massiv_Atom[i] + ";";
            }
            port.Write(Command_Line);
        }

        private void button33_Click(object sender, EventArgs e)
        {
            chart1.Series[0].Points.Clear();
            chart1.ChartAreas[0].AxisX.Minimum = Double.NaN;
            chart1.ChartAreas[0].AxisX.Maximum = Double.NaN;
            x1 = 0;
            dataGridView1.Rows.Clear();
            number_light = 0;
        }

        private void button35_Click(object sender, EventArgs e)
        {
            chart2.Series[0].Points.Clear();
            chart2.ChartAreas[0].AxisX.Minimum = Double.NaN;
            chart2.ChartAreas[0].AxisX.Maximum = Double.NaN;
            x2 = 0;
            dataGridView2.Rows.Clear();
            number_vibr = 0;
        }
        private void button37_Click(object sender, EventArgs e)
        {
            chart3.Series[0].Points.Clear();
            chart3.ChartAreas[0].AxisX.Minimum = Double.NaN;
            chart3.ChartAreas[0].AxisX.Maximum = Double.NaN;
            x3 = 0;
            dataGridView3.Rows.Clear();
            number_sound = 0;
        }

        private void button39_Click(object sender, EventArgs e)
        {
            chart4.Series[0].Points.Clear();
            chart5.Series[0].Points.Clear();
            chart4.ChartAreas[0].AxisX.Minimum = Double.NaN;
            chart4.ChartAreas[0].AxisX.Maximum = Double.NaN;
            chart5.ChartAreas[0].AxisX.Minimum = Double.NaN;
            chart5.ChartAreas[0].AxisX.Maximum = Double.NaN;
            x4 = 0;
            dataGridView4.Rows.Clear();
            number_meteo = 0;
        }

        private void button41_Click(object sender, EventArgs e)
        {
            
            chart7.Series[0].Points.Clear();
            chart8.Series[0].Points.Clear();
            chart9.Series[0].Points.Clear();
            chart10.Series[0].Points.Clear();
            chart11.Series[0].Points.Clear();

            chart7.ChartAreas[0].AxisX.Minimum = Double.NaN;
            chart7.ChartAreas[0].AxisX.Maximum = Double.NaN;
            chart8.ChartAreas[0].AxisX.Minimum = Double.NaN;
            chart8.ChartAreas[0].AxisX.Maximum = Double.NaN;
            chart9.ChartAreas[0].AxisX.Minimum = Double.NaN;
            chart9.ChartAreas[0].AxisX.Maximum = Double.NaN;
            chart10.ChartAreas[0].AxisX.Minimum = Double.NaN;
            chart10.ChartAreas[0].AxisX.Maximum = Double.NaN;
            chart11.ChartAreas[0].AxisX.Minimum = Double.NaN;
            chart11.ChartAreas[0].AxisX.Maximum = Double.NaN;
            x_all = 0;
            dataGridView6.Rows.Clear();
            number_all = 0;

            /*chart7.Series[0]["PixelPointWidth"] = "30";
            chart8.Series[0]["PixelPointWidth"] = "30";
            chart9.Series[0]["PixelPointWidth"] = "30";
            chart10.Series[0]["PixelPointWidth"] = "30";
            chart11.Series[0]["PixelPointWidth"] = "30";*/

            if (textBox34.Text == "" || textBox36.Text == "" || textBox37.Text == "" || textBox35.Text == "")
            {
                MessageBox.Show("Введены не все данные эксперимента!");
                return;
            }
            if (textBox33.Text == "")
            {
                MessageBox.Show("Не введен период измерения!");
                return;
            }
            string Command_Line = "";
            button41.Enabled = false;
            button40.Enabled = true;
            button42.Enabled = true;
            button44.Enabled = true;
            t_all = double.Parse(textBox33.Text);
            chart8.Titles[0].Text = "График измерений датчика света (T = " + textBox33.Text + ")";
            chart9.Titles[0].Text = "График измерений датчика звука (T = " + textBox33.Text + ")";
            chart7.Titles[0].Text = "График измерений датчика вибрации (T = " + textBox33.Text + ")";
            chart10.Titles[0].Text = "График измерений температуры метеодатчика (T = " + textBox33.Text + ")";
            chart11.Titles[0].Text = "График измерений влажности метеодатчика (T = " + textBox33.Text + ")";
            label67.Text = date.Substring(0, date.Length - 7);
            CirclePaint(pictureBox3, true);
            CirclePaint(pictureBox6, false);
            CirclePaint(pictureBox12, false);
            CirclePaint(pictureBox13, false);
            CirclePaint(pictureBox15, false);
            name = textBox36.Text;
            fam = textBox37.Text;
            group = textBox35.Text;
            number_exp = textBox34.Text;
            Write_Command_Detector(1, 1, 1, 1, 1, 0, 0, 0, 0, double.Parse(textBox33.Text));
            for (int i = 0; i < Massiv_Atom.Length; i++)
            {
                Command_Line += Massiv_Atom[i] + ";";
            }
            port.Write(Command_Line);
        }
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            check_number_backspace(e);
        }

        private void button40_Click(object sender, EventArgs e)
        {
            string Command_Line = "";
            CirclePaint(pictureBox3, false);
            button41.Enabled = true;
            button40.Enabled = false;
            button42.Enabled = false;
            button44.Enabled = false;
            button43.Enabled = false;
            Write_Command_Detector(1, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            for (int i = 0; i < Massiv_Atom.Length; i++)
            {
                Command_Line += Massiv_Atom[i] + ";";
            }
            port.Write(Command_Line);
            n5 = 0;
        }

        private void button42_Click(object sender, EventArgs e)
        {
            string Command_Line = "";
            t_all = double.Parse(textBox33.Text);
            chart8.Titles[0].Text = "График измерений датчика света (T = " + textBox33.Text + ")";
            chart9.Titles[0].Text = "График измерений датчика звука (T = " + textBox33.Text + ")";
            chart7.Titles[0].Text = "График измерений датчика вибрации (T = " + textBox33.Text + ")";
            chart10.Titles[0].Text = "График измерений температуры метеодатчика (T = " + textBox33.Text + ")";
            chart11.Titles[0].Text = "График измерений влажности метеодатчика (T = " + textBox33.Text + ")";
            Write_Command_Detector(1, 1, 1, 1, 1, 0, 0, 0, 0, double.Parse(textBox33.Text));
            for (int i = 0; i < Massiv_Atom.Length; i++)
            {
                Command_Line += Massiv_Atom[i] + ";";
            }
            port.Write(Command_Line);
        }

        
        private void button44_Click(object sender, EventArgs e)
        {
            flag_all = true;
            button44.Enabled = false;
            button43.Enabled = true;
        }

        private void button43_Click(object sender, EventArgs e)
        {
            flag_all = false;
            button43.Enabled = false;
            button44.Enabled = true;
        }
        private void DrawArrow(Color color, Graphics gr)
        {
            int line = 230;
            Point[] pts =
            {
                new Point( x_center,  y_center-radius/2),
                new Point(x_center+line,  y_center),
                new Point( x_center,  y_center+radius/2)
            };
            SolidBrush Brush = new SolidBrush(color);
            gr.FillPolygon(Brush, pts);
        }

        private Matrix RotateAroundPoint(float angle, Point center)
        {
            Matrix result = new Matrix();
            result.RotateAt(angle, center);
            return result;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == 0)
            {
                panel11.Visible = true;
                panel34.Visible = false;
            }
            if (comboBox2.SelectedIndex == 1)
            {
                panel34.Visible = true;
                panel11.Visible = false;
            }
        }

        private void button36_Click(object sender, EventArgs e)
        {
            flag_servo = true;
            button34.Enabled = true;
            button36.Enabled = false;
        }

        private void button34_Click(object sender, EventArgs e)
        {
            flag_servo = false;
            button36.Enabled = true;
            button34.Enabled = false;
        }

        private void button38_Click(object sender, EventArgs e)
        {
            dataGridView5.Rows.Clear();
            number_servo = 0;
        }

        private void ServoPaint(float angle)
        {
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            using (Graphics g = Graphics.FromImage(pictureBox1.Image))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                Point center = new Point(x_center, y_center);
                g.Transform = RotateAroundPoint(angle, center);
                g.Clear(Color.White);
                using (Graphics g1 = Graphics.FromImage(pictureBox1.Image))
                {
                    Pen pen = new Pen(Color.FromArgb(255, 0, 0, 0));
                    g1.DrawLine(pen, 0, y_center, pictureBox1.Width, y_center);
                    g1.DrawString("0°", new Font("Arial", (float)18), new SolidBrush(Color.Black), pictureBox1.Width - 50, y_center + 30);
                    g1.DrawString("180°", new Font("Arial", (float)18), new SolidBrush(Color.Black), 30, y_center + 30);
                }
                
                DrawArrow(Color.Red, g);
                g.FillEllipse(Brushes.Green, center.X - radius/2, center.Y- radius / 2, radius, radius);
                g.FillEllipse(Brushes.Red, center.X - radius / 6, center.Y - radius / 6, radius/3, radius/3);

            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (textBox14.Text == "" || Massiv_Atom[0]==3)
            {
                if (comboBox1.SelectedIndex == 0)
                    label24.Text = "Угол поворота (0° - " + angles[angles.Count - 1].ToString() + "°)";
                else
                {
                    float t = 180 - angles[angles.Count - 1];
                    label24.Text = "Угол поворота (0° - " + t.ToString() + "°)";
                }
            }
            else
            {
                if (comboBox1.SelectedIndex == 0)
                    label24.Text = "Угол поворота (0° - " + textBox14.Text + "°)";
                else
                {
                    float t = 180 - float.Parse(textBox14.Text);
                    label24.Text = "Угол поворота (0° - " + t.ToString() + "°)";
                }
            }
        }

        private void textBox14_TextChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
                label24.Text = "Угол поворота (0° - " + textBox14.Text + "°)";
            else
            {
                float t = 180 - float.Parse(textBox14.Text);
                label24.Text = "Угол поворота (0° - " + t.ToString() + "°)";
            }
        }

        int datagrid_fill(string sql, DataGridView datagrid, int column_date)
        {
            DataSet ds = new DataSet();
            MySqlCommand commandsql = new MySqlCommand(sql, conn);
            int columnsNumber;
            int rowsNumber;
            using (MySqlDataAdapter sAdapter = new MySqlDataAdapter(commandsql))
            {
                ds.Clear();
                sAdapter.Fill(ds);
                /*if (ds.Tables[0].Rows.Count == 0)
                {
                    MessageBox.Show("Нет измерений во введенном периоде!");
                    return;
                }*/
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    datagrid.Rows.Add(row.ItemArray);
                }
                columnsNumber = datagrid.Columns.Count;
                rowsNumber = datagrid.Rows.Count;
                string str;
                for (int i = 0; i < rowsNumber - 1; i++)
                {
                    str = datagrid[column_date, i].Value.ToString();
                    datagrid[column_date, i].Value = str.Substring(0, 10);
                }
            }
            return rowsNumber;
        }
        void chart_fill(Chart chart, DataGridView datagrid, int column_value, int column_period, int rowsNumber)
        {
            float t = 0;
            for (int i = 0; i < rowsNumber - 1; i++)
            {
                chart.Series[0].Points.AddXY(t, datagrid[column_value, i].Value);
                t += float.Parse(datagrid[column_period, i].Value.ToString());
            }
        } 
        private void button47_Click_1(object sender, EventArgs e)
        {
            chart14.Series[0].Points.Clear();
            chart13.Series[0].Points.Clear();
            chart15.Series[0].Points.Clear();
            chart6.Series[0].Points.Clear();
            chart12.Series[0].Points.Clear();
            dataGridView7.Rows.Clear();
            dataGridView8.Rows.Clear();
            dataGridView9.Rows.Clear();
            dataGridView10.Rows.Clear();
            if (maskedTextBox1.Text == maskedTextBox2.Text)
            {
                MessageBox.Show("Время начала измерений не должно равняться времени окончанию измерений!");
                return;
            }

            string date1 = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            string date2 = dateTimePicker2.Value.ToString("yyyy-MM-dd");
            string sql_light = "SELECT * FROM light as l WHERE (CONCAT(l.date,' ',l.time) between '" + date1+" "+ maskedTextBox1.Text + "' and '"+date2+" "+ maskedTextBox2.Text + "')";
            string sql_sound = "SELECT * FROM sound as s WHERE (CONCAT(s.date,' ',s.time) between '" + date1 + " " + maskedTextBox1.Text + "' and '" + date2 + " " + maskedTextBox2.Text + "')";
            string sql_vibr = "SELECT * FROM vibration as v WHERE (CONCAT(v.date,' ',v.time) between '" + date1 + " " + maskedTextBox1.Text + "' and '" + date2 + " " + maskedTextBox2.Text + "')";
            string sql_meteo = "SELECT * FROM meteo as m WHERE (CONCAT(m.date,' ',m.time) between '" + date1 + " " + maskedTextBox1.Text + "' and '" + date2 + " " + maskedTextBox2.Text + "')";
            if (radioButton1.Checked)
            {
                sql_light += "and (l.number_exp=" + textBox38.Text + " and l.name='" + textBox40.Text + "' and l.fam='" + textBox41.Text + "' and l.group='" + textBox39.Text + "')";
                sql_sound += "and (s.number_exp=" + textBox38.Text + " and s.name='" + textBox40.Text + "' and s.fam='" + textBox41.Text + "' and s.group='" + textBox39.Text + "')";
                sql_vibr += "and (v.number_exp=" + textBox38.Text + " and v.name='" + textBox40.Text + "' and v.fam='" + textBox41.Text + "' and v.group='" + textBox39.Text + "')";
                sql_meteo += "and (m.number_exp=" + textBox38.Text + " and m.name='" + textBox40.Text + "' and m.fam='" + textBox41.Text + "' and m.group='" + textBox39.Text + "')";
            }
            int rowsNumber_light = datagrid_fill(sql_light, dataGridView8, 3);
            int rowsNumber_sound = datagrid_fill(sql_sound, dataGridView10, 3);
            int rowsNumber_vibr = datagrid_fill(sql_vibr, dataGridView9, 3);
            int rowsNumber_meteo = datagrid_fill(sql_meteo, dataGridView7, 4);

            chart_fill(chart14, dataGridView8, 1, 2, rowsNumber_light);
            chart_fill(chart15, dataGridView10, 1, 2, rowsNumber_sound);
            chart_fill(chart13, dataGridView9, 1, 2, rowsNumber_vibr);
            chart_fill(chart6, dataGridView7, 1, 3, rowsNumber_meteo);
            chart_fill(chart12, dataGridView7, 2, 3, rowsNumber_meteo);
            
            DataPoint max = chart14.Series[0].Points.FindMaxByValue();
            chart14.ChartAreas[0].AxisY.Maximum = max.YValues[0]+20;
            max = chart15.Series[0].Points.FindMaxByValue();
            chart15.ChartAreas[0].AxisY.Maximum = max.YValues[0] + 20;
            max = chart13.Series[0].Points.FindMaxByValue();
            chart13.ChartAreas[0].AxisY.Maximum = max.YValues[0] + 20;
            max = chart6.Series[0].Points.FindMaxByValue();
            chart6.ChartAreas[0].AxisY.Maximum = max.YValues[0] + 20;
            max = chart12.Series[0].Points.FindMaxByValue();
            chart12.ChartAreas[0].AxisY.Maximum = max.YValues[0] + 20;

            /*chart14.ChartAreas[0].CursorX.IsUserEnabled = true;
            chart14.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            chart14.ChartAreas[0].CursorY.IsUserEnabled = true;
            chart14.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;
            chart14.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chart14.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;
            chart14.ChartAreas[0].AxisY.ScaleView.Zoomable = true;
            chart14.ChartAreas[0].AxisY.ScrollBar.IsPositionedInside = true;*/
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (textBox29.Text == "" || textBox31.Text == "" || textBox32.Text == "" || textBox30.Text == "")
            {
                MessageBox.Show("Введены не все данные эксперимента!");
                return;
            }
            string Command_Line = "";
            CirclePaint(pictureBox17, true);
            label48.Text = date.Substring(0, date.Length - 7);
            name = textBox31.Text;
            fam = textBox32.Text;
            group = textBox30.Text;
            number_exp = textBox29.Text;
            Write_Command_Detector(3, 1, 0, 0, 0, 0, 0, 0, 0, 0);
            for (int i = 0; i < Massiv_Atom.Length - 4; i++)
            {
                Command_Line += Massiv_Atom[i] + ";";
            }
            
            port.Write(Command_Line);
            ServoAnimation(angles[angles.Count - 1], 0, 0);
            Wait(0.7);
            ServoAnimation(0, 140, 1);
            Wait(0.7);
            ServoAnimation(140, 30, 0);
            Wait(0.7);
            ServoAnimation(30, 180, 1);
            Wait(0.7);
            ServoAnimation(180, 0, 0);
            Wait(0.7);
            ServoAnimation(0, 60, 1);
            angles.Add(60);
            using (Graphics g1 = Graphics.FromImage(pictureBox1.Image))
            {
                g1.DrawString("Текущий угол = 60°", new Font("Arial", (float)16), new SolidBrush(Color.Black), x_center - 110, y_center + 100);
            }
        }

        private void button32_Click(object sender, EventArgs e)
        {
            string Command_Line = "";
            CirclePaint(pictureBox17, false);
            Write_Command_Detector(3, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            for (int i = 0; i < Massiv_Atom.Length - 4; i++)
            {
                Command_Line += Massiv_Atom[i] + ";";
            }
            port.Write(Command_Line);
            float angle_last = angles[angles.Count - 1];
            angles.Clear();
            angles.Add(angle_last);
        }

        private void ServoAnimation(float angle_0, float angle_1, int napr)
        {
            if (napr == 0)
            {
                for (float iangle = angle_0; iangle >= angle_1; iangle--)
                {
                    Wait(0.01);
                    ServoPaint(-iangle);
                }
            }
            else
            {
                for (float iangle = angle_0; iangle <= angle_1; iangle++)
                {
                    Wait(0.01);
                    ServoPaint(-iangle);
                }
            }
        }
        private void Wait(double seconds)
        {
            int ticks = System.Environment.TickCount + (int)Math.Round(seconds * 1000.0);
            while (System.Environment.TickCount < ticks)
            {
                Application.DoEvents();
            }
        }
        private void button16_Click(object sender, EventArgs e)
        {
            if (textBox29.Text == "" || textBox31.Text == "" || textBox32.Text == "" || textBox30.Text == "")
            {
                MessageBox.Show("Введены не все данные эксперимента!");
                return;
            }
            if (float.Parse(textBox14.Text) == float.Parse(textBox15.Text) && float.Parse(textBox14.Text) == 0 && comboBox1.SelectedIndex == 0)
            {
                MessageBox.Show("При заданном направлении вращения по часовой стрелки: задано начальное положение, равное граничному положению - нулю градусов!");
                return;
            }
            if (float.Parse(textBox14.Text) == float.Parse(textBox15.Text) && float.Parse(textBox14.Text) == 180 && comboBox1.SelectedIndex == 1)
            {
                MessageBox.Show("При заданном направлении вращения против часовой стрелки: задано начальное положение, равное граничному положению - 180 градусам!");
                return;
            }
            if (float.Parse(textBox15.Text) > 0 && comboBox1.SelectedIndex == 0 && (float.Parse(textBox14.Text) < float.Parse(textBox15.Text)))
            {
                MessageBox.Show("При заданном направлении вращения по часовой стрелки: задан угол поворота больше начального положения!");
                return;
            }
            if (float.Parse(textBox15.Text) > 0 && comboBox1.SelectedIndex == 1 && ((float.Parse(textBox14.Text) + float.Parse(textBox15.Text)) > 180))
            {
                MessageBox.Show("При заданном направлении вращения против часовой стрелки: задан угол поворота больше 180 градусов!");
                return;
            }
            if (float.Parse(textBox15.Text)== 0)
            {
                MessageBox.Show("Задан угол поворота, равный нулю градусов!");
                return;
            }
            string Command_Line = "";
            CirclePaint(pictureBox17, true);
            label48.Text = date.Substring(0, date.Length - 7);
            name = textBox31.Text;
            fam = textBox32.Text;
            group = textBox30.Text;
            number_exp = textBox29.Text;
            Write_Command_Detector(2, 1, float.Parse(textBox14.Text), comboBox1.SelectedIndex, float.Parse(textBox15.Text), float.Parse(textBox16.Text), 0, 0, 0, 0);
            for (int i = 0; i < Massiv_Atom.Length - 4; i++)
            {
                Command_Line += Massiv_Atom[i] + ";";
            }
            port.Write(Command_Line);

                if (float.Parse(textBox14.Text) >= 0 && comboBox1.SelectedIndex == 1)
                {
                    if (float.Parse(textBox14.Text) == 0 && angles[angles.Count - 1] == 0)
                    {
                        ServoAnimation(angles[angles.Count - 1], float.Parse(textBox15.Text), 1);
                    }
                    if (float.Parse(textBox14.Text) > angles[angles.Count - 1])
                    {
                        ServoAnimation(angles[angles.Count - 1], float.Parse(textBox14.Text), 1);
                        Wait(0.5);
                        ServoAnimation(float.Parse(textBox14.Text), float.Parse(textBox14.Text) + float.Parse(textBox15.Text), 1);
                    }
                    if (float.Parse(textBox14.Text) < angles[angles.Count - 1])
                    {

                        ServoAnimation(angles[angles.Count - 1], float.Parse(textBox14.Text), 0);
                        Wait(0.5);
                        ServoAnimation(float.Parse(textBox14.Text), float.Parse(textBox14.Text) + float.Parse(textBox15.Text), 1);
                    }
                    if (float.Parse(textBox14.Text) == angles[angles.Count - 1] && float.Parse(textBox14.Text) != 0 && float.Parse(textBox14.Text) != 180)
                    {
                        ServoAnimation(float.Parse(textBox14.Text), float.Parse(textBox14.Text) + float.Parse(textBox15.Text), 1);
                    }
                    angle0 = float.Parse(textBox15.Text) + float.Parse(textBox14.Text);
                    angles.Add(angle0);
                }
                if (float.Parse(textBox14.Text) <= 180 && comboBox1.SelectedIndex == 0)
                {
                    if (float.Parse(textBox14.Text) == 180 && angles[angles.Count - 1] == 180)
                    {
                        ServoAnimation(angles[angles.Count - 1], float.Parse(textBox15.Text), 0);
                    }
                    if (float.Parse(textBox14.Text) == angles[angles.Count - 1] && float.Parse(textBox14.Text) != 180)
                    {

                        ServoAnimation(angles[angles.Count - 1], Math.Abs(float.Parse(textBox14.Text) - float.Parse(textBox15.Text)), 0);

                    }
                    if (float.Parse(textBox14.Text) < angles[angles.Count - 1])
                    {
                        ServoAnimation(angles[angles.Count - 1], float.Parse(textBox14.Text), 0);
                        Wait(0.5);
                        ServoAnimation(float.Parse(textBox14.Text), Math.Abs(float.Parse(textBox14.Text) - float.Parse(textBox15.Text)), 0);
                    }
                    if (float.Parse(textBox14.Text) > angles[angles.Count - 1])
                    {

                        ServoAnimation(angles[angles.Count - 1], float.Parse(textBox14.Text), 1);
                        Wait(0.5);
                        ServoAnimation(float.Parse(textBox14.Text), Math.Abs(float.Parse(textBox14.Text) - float.Parse(textBox15.Text)), 0);
                    }
                    angle0 = Math.Abs(float.Parse(textBox14.Text) - float.Parse(textBox15.Text));
                    angles.Add(angle0);
                }
            using (Graphics g1 = Graphics.FromImage(pictureBox1.Image))
            {
                g1.DrawString("Текущий угол = "+ angle0 + "°", new Font("Arial", (float)16), new SolidBrush(Color.Black), x_center-110, y_center + 100);
            }
        }

        private void button48_Click(object sender, EventArgs e)
        {
            chart7.Series[0].Points.Clear();
            chart8.Series[0].Points.Clear();
            chart9.Series[0].Points.Clear();
            chart10.Series[0].Points.Clear();
            chart11.Series[0].Points.Clear();

            chart7.ChartAreas[0].AxisX.Minimum = Double.NaN;
            chart7.ChartAreas[0].AxisX.Maximum = Double.NaN;
            chart8.ChartAreas[0].AxisX.Minimum = Double.NaN;
            chart8.ChartAreas[0].AxisX.Maximum = Double.NaN;
            chart9.ChartAreas[0].AxisX.Minimum = Double.NaN;
            chart9.ChartAreas[0].AxisX.Maximum = Double.NaN;
            chart10.ChartAreas[0].AxisX.Minimum = Double.NaN;
            chart10.ChartAreas[0].AxisX.Maximum = Double.NaN;
            chart11.ChartAreas[0].AxisX.Minimum = Double.NaN;
            chart11.ChartAreas[0].AxisX.Maximum = Double.NaN;
            x_all = 0;
            dataGridView6.Rows.Clear();
            number_all = 0;

        }

        
        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            check_number_backspace(e);
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            check_number_backspace(e);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            string Command_Line = "";
            Write_Command_Detector(1, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            for (int i = 0; i < Massiv_Atom.Length; i++)
            {
                Command_Line += Massiv_Atom[i] + ";";
            }
            port.Write(Command_Line);
            Command_Line = "";
            Write_Command_Detector(2, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            for (int i = 0; i < Massiv_Atom.Length; i++)
            {
                Command_Line += Massiv_Atom[i] + ";";
            }
            port.Write(Command_Line);
            port.Close();
            conn.Close();
        }

        private void button25_Click(object sender, EventArgs e)
        {
            string sql;
            MySqlCommand commandsql;
            sql = "truncate vibration;";
            commandsql = new MySqlCommand(sql, conn);
            commandsql.ExecuteNonQuery();
            MessageBox.Show("Данные удалены!");
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.SelectedIndex == 0)
            {
                dataGridView7.Visible = false;
                dataGridView8.Visible = true;
                dataGridView9.Visible = false;
                dataGridView10.Visible = false;
            }
            if (comboBox3.SelectedIndex == 1)
            {
                dataGridView7.Visible = false;
                dataGridView8.Visible = false;
                dataGridView9.Visible = false;
                dataGridView10.Visible = true;
            }
            if (comboBox3.SelectedIndex == 2)
            {
                dataGridView7.Visible = false;
                dataGridView8.Visible = false;
                dataGridView9.Visible = true;
                dataGridView10.Visible = false;
            }
            if (comboBox3.SelectedIndex == 3)
            {
                dataGridView7.Visible = true;
                dataGridView8.Visible = false;
                dataGridView9.Visible = false;
                dataGridView10.Visible = false;
            }
        }

        private void button26_Click(object sender, EventArgs e)
        {
            string sql;
            MySqlCommand commandsql;
            sql = "truncate sound;";
            commandsql = new MySqlCommand(sql, conn);
            commandsql.ExecuteNonQuery();
            MessageBox.Show("Данные удалены!");
        }

        private void button27_Click(object sender, EventArgs e)
        {
            string sql;
            MySqlCommand commandsql;
            sql = "truncate meteo;";
            commandsql = new MySqlCommand(sql, conn);
            commandsql.ExecuteNonQuery();
            MessageBox.Show("Данные удалены!");
        }

        private void button29_Click(object sender, EventArgs e)
        {
            string sql;
            MySqlCommand commandsql;
            sql = "truncate servo;";
            commandsql = new MySqlCommand(sql, conn);
            commandsql.ExecuteNonQuery();
            MessageBox.Show("Данные удалены!");
        }

        private void button28_Click(object sender, EventArgs e)
        {
            string sql;
            MySqlCommand commandsql;
            sql = "truncate servo;truncate light;truncate meteo;truncate sound;truncate vibration;";
            commandsql = new MySqlCommand(sql, conn);
            commandsql.ExecuteNonQuery();
            MessageBox.Show("Данные удалены!");
        }

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            check_number_backspace(e);
        }

        private void textBox33_KeyPress(object sender, KeyPressEventArgs e)
        {
            check_number_backspace(e);
        }
        private void textBox14_KeyPress(object sender, KeyPressEventArgs e)
        {
            check_number(e);
        }

        private void button24_Click_1(object sender, EventArgs e)
        {
            string sql;
            MySqlCommand commandsql;
            sql = "truncate light;";
            commandsql = new MySqlCommand(sql, conn);
            commandsql.ExecuteNonQuery();
            MessageBox.Show("Данные удалены!");
        }

        private void textBox15_KeyPress(object sender, KeyPressEventArgs e)
        {
            check_number(e);
        }
        private void textBox16_KeyPress(object sender, KeyPressEventArgs e)
        {
            check_number(e);
        }
        private void textBox11_KeyPress(object sender, KeyPressEventArgs e)
        {
            check_number(e);
        }
        private void textBox20_KeyPress(object sender, KeyPressEventArgs e)
        {
            check_number(e);
        }
        private void textBox24_KeyPress(object sender, KeyPressEventArgs e)
        {
            check_number(e);
        }
        private void textBox28_KeyPress(object sender, KeyPressEventArgs e)
        {
            check_number(e);
        }
        private void textBox29_KeyPress(object sender, KeyPressEventArgs e)
        {
            check_number(e);
        }
        private void textBox34_KeyPress(object sender, KeyPressEventArgs e)
        {
            check_number(e);
        }
        private void textBox38_KeyPress(object sender, KeyPressEventArgs e)
        {
            check_number(e);
        }
        
        private void button19_Click(object sender, EventArgs e)
        {
            flag_sound = true;
            button18.Enabled = true;
            button19.Enabled = false;
        }

        private void button18_Click(object sender, EventArgs e)
        {
            flag_sound = false;
            button19.Enabled = true;
            button18.Enabled = false;
        }

        private void button22_Click(object sender, EventArgs e)
        {
            flag_meteo = true;
            button21.Enabled = true;
            button22.Enabled = false;
        }

        private void button21_Click(object sender, EventArgs e)
        {
            flag_meteo = false;
            button22.Enabled = true;
            button21.Enabled = false;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            string Command_Line = "";
            chart1.Titles[0].Text = "График измерений датчика света (T = " + textBox2.Text+")";
            t1 = double.Parse(textBox2.Text);
            Write_Command_Detector(1, 0, 0, 1, 0, 0, 0, double.Parse(textBox2.Text), 0, 0);
            for (int i = 0; i < Massiv_Atom.Length; i++)
            {
                Command_Line += Massiv_Atom[i] + ";";
            }
            port.Write(Command_Line);
            
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string Command_Line = "";
            CirclePaint(pictureBox17, false);
            Write_Command_Detector(2, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            for (int i = 0; i < Massiv_Atom.Length - 4; i++)
            {
                Command_Line += Massiv_Atom[i] + ";";
            }
            port.Write(Command_Line);
            float angle_last = angles[angles.Count - 1];
            angles.Clear();
            angles.Add(angle_last);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            chart2.Series[0].Points.Clear();
            chart2.ChartAreas[0].AxisX.Minimum = Double.NaN;
            chart2.ChartAreas[0].AxisX.Maximum = Double.NaN;
            x2 = 0;
            dataGridView2.Rows.Clear();
            number_vibr = 0;

            //chart2.Series[0]["PixelPointWidth"] = "30";
            if (textBox20.Text == "" || textBox19.Text == "" || textBox18.Text == "" || textBox17.Text == "")
            {
                MessageBox.Show("Введены не все данные эксперимента!");
                return;
            }
            if (textBox4.Text == "")
            {
                MessageBox.Show("Не введен период измерения!");
                return;
            }
            string Command_Line = "";
            button1.Enabled = true;
            button2.Enabled = false;
            button17.Enabled = true;
            button15.Enabled = true;
            t2 = double.Parse(textBox4.Text);
            chart2.Titles[0].Text = "График измерений датчика вибрации (T = " + textBox4.Text+")";
            label27.Text = date.Substring(0, date.Length - 7);
            CirclePaint(pictureBox12, true);
            CirclePaint(pictureBox6, false);
            CirclePaint(pictureBox13, false);
            CirclePaint(pictureBox15, false);
            name = textBox19.Text;
            fam = textBox18.Text;
            group = textBox17.Text;
            number_exp = textBox20.Text;
            Write_Command_Detector(1, 0, 0, 0, 1, 0, 0, 0, double.Parse(textBox4.Text),  0);
            for (int i = 0; i < Massiv_Atom.Length; i++)
            {
                Command_Line += Massiv_Atom[i] + ";";
            }
            port.Write(Command_Line);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string Command_Line = "";
            button1.Enabled = false;
            button2.Enabled = true;
            button17.Enabled = false;
            button15.Enabled = false;
            button14.Enabled = false;
            CirclePaint(pictureBox12, false);
            Write_Command_Detector(1, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            for (int i = 0; i < Massiv_Atom.Length; i++)
            {
                Command_Line += Massiv_Atom[i] + ";";
            }
            port.Write(Command_Line);
            n4 = 0;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            flag_light = true;
            button13.Enabled = false;
            button12.Enabled = true;
        }
    }
}
