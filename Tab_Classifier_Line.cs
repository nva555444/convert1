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
    class Tab_Classifier_Line
    {
        static public DataTable tab;

        static Tab_Classifier_Line()
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
            my_column5.ColumnName = "Google_Style";
            my_column5.DataType = System.Type.GetType("convert1.MyStyle_Line");


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
            //new MyStyle("цвет контура", "цвет заливки"));

            tab.Rows.Add("52", "0", "prochee_line", "прочие линейные", new MyStyle_Line("ff000000",  "1"));

            tab.Rows.Add("52", "5101", "line01", "линия01", new MyStyle_Line("ffff0000",  "2"));
            tab.Rows.Add("52", "5102", "line02", "линия02", new MyStyle_Line("ff005500",  "2"));
            tab.Rows.Add("52", "5103", "line03", "линия03", new MyStyle_Line("ffffffaa",  "2"));

            tab.Rows.Add("52", "0_gaz", "gazoprovod", "газопровод", new MyStyle_Line("ff00ffff", "2"));
            tab.Rows.Add("52", "0_lep", "lep", "лэп", new MyStyle_Line("ffff00ff",  "2"));


        }

        static public string Get_Rus_Name_by_kod_txf(string code)
        {
            string rez = "";
            rez = "прочие_линейные";
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
            rez = "prochee_line";
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
