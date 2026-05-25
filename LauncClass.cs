using System;
using System.Collections;
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
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

using System.Xml;
using System.Xml.Linq;

using System.Runtime.InteropServices; // для ddl
using System.Collections.Specialized;


namespace convert1
{

    class LauncClass
    {
        
        static string mypath_txf;
        static string mypath_kml;

        static public bool _canceled = false;
        static public event Action<string> ProcessChanged;  //событие
        static public event Action<bool> WorkCompleted; //событие

        static public void Cancel() // метод
        {
            _canceled = true;
        }


        public static void MAIN_PROC1(object param)  //get  List_TXF_Object
        {
            //в txf в цифрах разделитетель целой и дробной части "." в виндус в настройках может быть указана ,
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");  //!!! ru-RU---    en-US+++


            _canceled = false;

            List<object> obj_arr2 = (List<object>)param;

            SynchronizationContext context = (SynchronizationContext)obj_arr2[0];
           

            //File.WriteAllText("text-8.txt", "");

            GlobalVar.text_for_tb3 = "";
            GlobalVar.text_for_tb4 = "";
            GlobalVar.text_for_tb5 = "";
            GlobalVar.text_for_tb6 = "";

            mypath_txf = (string)obj_arr2[1]; //путь к файлу


            if (!File.Exists(mypath_txf))
            {
                MessageBox.Show("ошибочный путь");
                context.Send(OnWorkCompleted, _canceled);
                return;
            }
            
            DateTime dt = DateTime.Now;
            mypath_kml = Path.GetFileNameWithoutExtension(mypath_txf)+" " + dt.ToString("(dd.MM.yyyy)") + ".kml";

            KML_object_class.CREATE_KML_FILE(mypath_kml);  //создать файл kml, записать стили

            string[] str = File.ReadAllLines(mypath_txf); //записать содержание файла TXF в переменную str

            ReadTXT.PARSING_ALL_TXF(str); // парсинг TXF

            KML_object_class.SAVE_KML_FILE();  //СОХРАНИТЬ KML

    

            
            

            context.Send(OnWorkCompleted, _canceled);  //МНОГОПОТОЧНОСТЬ
            


        }//MAIN_PROC1

        static public void OnWorkCompleted(object canceled) //обертки над событиями
        {
            WorkCompleted((bool)canceled);
        }

        static public void OnProcessChanged(object mes) //обертки над событиями
        {
            ProcessChanged((string)mes);
        }


    }



}


