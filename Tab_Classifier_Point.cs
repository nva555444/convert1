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
using System.Xml.Serialization;

namespace convert1
{
    class Tab_Classifier_Point
    {
        static public DataTable tab;
        static public string qwe = "111";
        
        static Tab_Classifier_Point()
        {
            
            tab = new DataTable();

            //добавить еще нумерацию
            
            DataColumn my_column1 = new DataColumn();
            my_column1.ColumnName = "Nomer";
            my_column1.DataType = System.Type.GetType("System.String");
            
            DataColumn my_column2 = new DataColumn();
            my_column2.ColumnName = "code_in_txf";
            my_column2.DataType = System.Type.GetType("System.String");

            DataColumn my_column3 = new DataColumn();
            my_column3.ColumnName = "eng_name";
            my_column3.DataType = System.Type.GetType("System.String");

            DataColumn my_column4 = new DataColumn();
            my_column4.ColumnName = "rus_name";
            my_column4.DataType = System.Type.GetType("System.String");

            DataColumn my_column5 = new DataColumn();
            my_column5.ColumnName = "Google_Style_Point";
            my_column5.DataType = System.Type.GetType("convert1.MyStyle_Point");


            tab.Columns.Add(my_column1);
            tab.Columns.Add(my_column2);
            tab.Columns.Add(my_column3);
            tab.Columns.Add(my_column4);
            tab.Columns.Add(my_column5);

            //ff0000ff красный
            //ff00aaff оранжевый
            //ff00ffff желтый
            //ff00ff00 зеленый
            //ffffff00 голубой
            //ffff0000 синий
            //ffff00ff фиолетовый (розовый)

            //fffeffff белый
            //ff000000 черный
            //в конструкторе по умолчанию ширина линии =2, можно указать свою, но все ломается, переработать присвоение стиля 

            tab.Rows.Add("52", "0", "prochee_point", "прочие_точечные", new MyStyle_Point("fffffffe", "0.5", "http://maps.google.com/mapfiles/kml/shapes/placemark_circle.png")); //кружок белый 0.5

            tab.Rows.Add("52", "5201", "tochka01", "точка01", new MyStyle_Point("ff0000ff", "1", "http://maps.google.com/mapfiles/kml/shapes/placemark_circle.png")); //квадрат голубой
            tab.Rows.Add("52", "5202", "tochka02", "точка02", new MyStyle_Point("ff00aaff", "1", "http://maps.google.com/mapfiles/kml/shapes/placemark_circle.png")); //кружок голубой
            tab.Rows.Add("52", "5203", "tochka03", "точка03", new MyStyle_Point("ff00ffff", "1", "http://maps.google.com/mapfiles/kml/shapes/placemark_circle.png")); //желтая метка
            tab.Rows.Add("52", "5204", "tochka04", "точка04", new MyStyle_Point("ff00ff00", "1", "http://maps.google.com/mapfiles/kml/shapes/placemark_circle.png")); //кружок белый 1
            tab.Rows.Add("52", "5205", "tochka05", "точка05", new MyStyle_Point("ffffff00", "1", "http://maps.google.com/mapfiles/kml/shapes/placemark_square.png"));
            tab.Rows.Add("52", "5206", "tochka06", "точка06", new MyStyle_Point("ffffff00", "1", "http://maps.google.com/mapfiles/kml/shapes/placemark_square.png"));
            tab.Rows.Add("52", "5207", "tochka07", "точка07", new MyStyle_Point("ffffff00", "1", "http://maps.google.com/mapfiles/kml/shapes/placemark_square.png"));
            tab.Rows.Add("52", "5208", "tochka08", "точка08", new MyStyle_Point("ffffff00", "1", "http://maps.google.com/mapfiles/kml/shapes/placemark_square.png"));
            tab.Rows.Add("52", "2509", "tochka09", "точка09", new MyStyle_Point("ffffff00", "1", "http://maps.google.com/mapfiles/kml/pushpin/ylw-pushpin.png"));
            tab.Rows.Add("52", "5210", "tochka10", "точка10", new MyStyle_Point("fffffffe", "1", "http://maps.google.com/mapfiles/kml/pushpin/ylw-pushpin.png"));//желтая метка

            tab.Rows.Add("52", "0_skvajina", "skvajina", "скважина", new MyStyle_Point("ffffff00", "1", "http://maps.google.com/mapfiles/kml/shapes/placemark_square.png"));

            

            //0_skvajina

            
        }


        static public string Get_Rus_Name_by_kod_txf(string code)
        {
            string rez = "";
            rez = "прочие_точечные";
            //rez = "";

            for (int i = 0; i < tab.Rows.Count; i++)
            {
                if (tab.Rows[i]["code_in_txf"].ToString() == code)
                {
                    rez = tab.Rows[i]["rus_name"].ToString();
                    break;
                }

            }

            return rez;
        }




        //возвращает англ имя по коду классификатора
        static public string Get_Eng_Name_by_kod_txf(string code)
        {
            string rez = "";
            rez = "prochee_point";
            // rez = "";

            for (int i = 0; i < tab.Rows.Count; i++)
            {
                if (tab.Rows[i]["code_in_txf"].ToString() == code)
                {

                    rez = tab.Rows[i]["eng_name"].ToString();
                    break;
                }

            }

            return rez;
        }




    }
}
