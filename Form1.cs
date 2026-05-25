using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

using System.Xml;
using System.Xml.Linq;

using System.Runtime.InteropServices; // для ddl
using System.Collections.Specialized;
using System.Globalization;
//button2



//программа предназначена для конвертации файла txf в файл kml
namespace convert1
{
    public partial class Form1 : Form
    {

        Thread thread;
        SynchronizationContext _context;

        DateTime t1;

        MyLib_Sekundomer ob_sek1;

        public Form1()
        {
            InitializeComponent();

            


            //tabControl1.SelectTab(tabPage2); 
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            //в LauncClass MAIN_PROC1
            //в txf в цифрах разделитетель целой и дробной части "." в виндус в настройках может быть указана ,
            //System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");  //!!! ru-RU---    en-US+++


            _context = SynchronizationContext.Current;

            string curdir = Directory.GetCurrentDirectory();
            string[] files=Directory.GetFiles(curdir, "*.txf");

            if (files.Length != 0)
            {
                textBox1.Text = files[0];
            }
            else
            {
                textBox1.Text = curdir;
            }


            notifyIcon1.Visible = false;

            GlobalVar.Colour_by_kod = false;
            GlobalVar.Colour_by_kod = true;

            radioButton1.Select();


            comboBox1.Items.AddRange(Tab_Classifier_Attributes.Get_Array_Rus_Name());
            comboBox1.SelectedIndex = 3;

            comboBox2.Items.AddRange(Tab_Classifier_Attributes.Get_Array_Rus_Name());
            comboBox2.SelectedIndex = 1;

            checkBox1.Checked = true;
            checkBox2.Checked = true;

           // btnStart.PerformClick(); //нажатие кнопки
            

            //mypath_txf = @"C:\Prelogs\ПРОГРАММИРОВАНИЕ\С Sharp\КОД\ПРОЕКТЫ\14-convert1\неисп (все области) (градусы) (23.08.2018)+++.txf";
            //mypath_txf = @"C:\Prelogs\ПРОГРАММИРОВАНИЕ\С Sharp\КОД\ПРОЕКТЫ\14-convert1\вся Московская область.txf";  
            //mypath_txf = @"C:\Prelogs\ПРОГРАММИРОВАНИЕ\С Sharp\КОД\ПРОЕКТЫ\14-convert1\неисп (Пензенская область).txf";  
            //mypath_txf = @"C:\Prelogs\ПРОГРАММИРОВАНИЕ\С Sharp\КОД\ПРОЕКТЫ\14-convert1\157.txf";
        }



        private void btnStart_Click(object sender, EventArgs e)
        {

            GlobalVar.group_str1 = comboBox1.Text;
            GlobalVar.group_str2 = comboBox2.Text;

            GlobalVar.group = "0";
            if (checkBox1.Checked == true) GlobalVar.group = "1";
            if (checkBox2.Checked == true) GlobalVar.group = "2";

        

            //MessageBox.Show(GlobalVar.group_str1);
            //MessageBox.Show(GlobalVar.group_str2);


            textBox2.AppendText("Начало процесса конвертации" + Environment.NewLine);
                
            LauncClass.WorkCompleted += _worker_WorkCompleted; // подписаться на событие
            LauncClass.ProcessChanged += _worker_ProcessChanged; //подписаться на событие, после которого должен изменяться прогрессбар, метод, который будет изменять прогресс бар

            //СЕКУНДОМЕР
            ob_sek1 = new MyLib_Sekundomer(SynchronizationContext.Current, label_time1);

            ob_sek1.Start();
            //СЕКУНДОМЕР


            t1 = DateTime.Now; //время, начало

            List<object> obj_arr = new List<object>();
            obj_arr.Add(_context);
            obj_arr.Add(textBox1.Text);

            thread = new Thread(LauncClass.MAIN_PROC1); // создать поток
            thread.IsBackground = true;
            thread.Start(obj_arr);  //запустить поток


            progressBar1.Style = ProgressBarStyle.Marquee;

            btnStart.Enabled = false;

        }


        private void _worker_ProcessChanged(string progress) //метод изм прогрессбара, вызывается при событии ProcessChanged (подписан)
        {
  

        }

        private void _worker_WorkCompleted(bool canceled)  //метод заверешени,  вызывается при событии WorkCompleted (подписан)
        {
            
            ob_sek1.Pause(); //таймер в лэйбле
            
            string message = canceled ? "Процесс отменен пользователем " : "Процесс завершен успешно ";

            progressBar1.Style = ProgressBarStyle.Blocks;

            TimeSpan ts = DateTime.Now - t1; //время, конец



            textBox2.AppendText(Environment.NewLine);
            textBox2.AppendText(message + Environment.NewLine);
            textBox2.AppendText("прошло времени (минут, секунды, доли секунд): " + ts.ToString((@"mm\:ss\:ff")) + Environment.NewLine);
            textBox2.AppendText("прошло времени (тактов): " + ts.Ticks.ToString() + Environment.NewLine);

            LauncClass.WorkCompleted -= _worker_WorkCompleted; // отписаться на событие
            LauncClass.ProcessChanged -= _worker_ProcessChanged; //отписаться на событие, после которого должен изменяться прогрессбар, метод, который будет изменять прогресс бар
            
            btnStart.Enabled = true;


            //progressBar1.Style = ProgressBarStyle.Blocks;

            progressBar1.Maximum = 1;
            progressBar1.Value = 1;

            //textBox3.AppendText(GlobalVar.text_for_tb3);

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            LauncClass.Cancel();
            ob_sek1.Pause();
            
      

        }




        private void button1_Click(object sender, EventArgs e)
        {


            //MessageBox.Show(comboBox1.Text);

            //var ob = c.GetType();
           // foreach (var a in t.GetFields(BindingFlags.Instance | BindingFlags.NonPublic))
           // {
                
        //    }
        //


        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            bool b1;
            
            try
            {
                b1 = thread.IsAlive;
            }
            catch
            {
                b1 = false;
            }


            if ((b1 == true) & (WindowState == FormWindowState.Minimized))
            {
                // прячем наше окно из панели
                this.ShowInTaskbar = false;
                // делаем нашу иконку в трее активной
                notifyIcon1.Visible = true;
            }

        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
                        // делаем нашу иконку скрытой
            notifyIcon1.Visible = false;
            // возвращаем отображение окна в панели
            this.ShowInTaskbar = true;
            //разворачиваем окно
            WindowState = FormWindowState.Normal;
        
        }

       // private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
       // {

      //  }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                comboBox1.Enabled = true;
                //comboBox2.Enabled = true;
                checkBox2.Enabled = true;
            }
            else
            {
                comboBox1.Enabled = false;
                comboBox2.Enabled = false;

                checkBox2.Checked = false;
                checkBox2.Enabled = false;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            GlobalVar.Colour_by_kod = true;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            GlobalVar.Colour_by_kod = false;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                comboBox2.Enabled = true;
            }
            else
            {
                comboBox2.Enabled = false;
            }
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.ShowDialog();
        }

 



        //////////////////////////////// readonly




        ////////////////////////////////
    


       



    }  //public partial class Form1 : Form








    public static class MyExtensions
    {

        public static XmlNode ConvertToXmlNode(this XElement element)
        {
            using (XmlReader xmlReader = element.CreateReader())
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlReader);

                return xmlDoc.FirstChild;
            }
        }
    }

}

//доделать классификацию по семантике

//блокировка кнопок/контролов при запуске

//выбор семаники для названия (верхней строчки)

//последнее

//public static void ANALISIS_ALL_TXF(string[] str)

//определение "по категориям", "по кадастру" - добавление соответствующей подписи

//выбор семантик, отображаемых в метке

//добавить тэг open в основную карту.

//порядок категорий, областей (сортировка)

//перевод в kmz

//изображения в файле ехе

//сделать OblastList только для чтения

//расширенное сворачивание в трей

//phase - сделать перечислитель




//+

//+ошибка если нет семантики !!!
//+без группировки, 1 группировка, 2 группировка
//+таблицу-классификатор для семантики (аналогично кодам объектов и стилей)
//+добавить в стиль толщину
//+отдельный классификатор для площадных и точечных объектов !!!!!!!
//+оддержка объектов+

//+упорядочить классы
//+упрощенное добавление объектов (атрибутов)
//+разработать новую структуру для стилей и кодов типов, начало в  TXF_Object_Struct

//+экспорт по областям
//+оптимизация, сохранение сначала в память, потом в файл
//+сворачивание в трей
//+добавить часы
//+сделать кнопку отмена
//+формат для площади 1004 (2 знака после запятой)
//+упростить область-район-категория в область-категория, + перегрузка
//+сделать отдельный класс для KML
//+создать многопоточную версию
//+ обработка, когда нет данного типа объекта в TXF
//+ доработать категории
//+ обработка, когда нет данной семантики
//+ присвоение атрибутов в стиль (или заново создать узел стиля)
//+ создать полную структуру - Область-район-категория
//+ сделать отдельный класс для TXF
//+ добавить поддержку подобъектов
