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
    static class Tab_Classifier_Attributes
    {
        static public DataTable tab;

        static Tab_Classifier_Attributes()
        {
            tab = new DataTable();



            DataColumn my_column1 = new DataColumn();
            my_column1.ColumnName = "code_in_txf";
            my_column1.DataType = System.Type.GetType("System.String");

            DataColumn my_column2 = new DataColumn();
            my_column2.ColumnName = "eng_name";
            my_column2.DataType = System.Type.GetType("System.String");

            DataColumn my_column3= new DataColumn();
            my_column3.ColumnName = "rus_name";
            my_column3.DataType = System.Type.GetType("System.String");

            tab.Columns.Add(my_column1);
            tab.Columns.Add(my_column2);
            tab.Columns.Add(my_column3);

            //tab.Rows.Add("1001", "own_name", "собственное название");

            tab.Rows.Add("9", "own_name", "собственное название");
            tab.Rows.Add("0", "category_txt_rus", "категория");

            tab.Rows.Add("110", "raion", "район");
            tab.Rows.Add("111", "oblast", "область");
            tab.Rows.Add("112", "area_object", "Площадь по объекту (га)");
            tab.Rows.Add("131", "id", "id");
            tab.Rows.Add("1001", "kadastr_number", "кадастровый номер");
            tab.Rows.Add("1002", "vid_prava", "вид права");
            tab.Rows.Add("1003", "pravoobladatel", "правообладатель");
            tab.Rows.Add("1004", "area", "площадь");
            tab.Rows.Add("1010", "kadastr_number_part", "кадастровый номер части");
            tab.Rows.Add("1011", "area_all", "Общая площадь (га)");
            tab.Rows.Add("1023", "arendator", "арендатор");
            tab.Rows.Add("1024", "arendodatel", "арендодатель");
            tab.Rows.Add("1026", "nomer_polya", "Номер поля");
            tab.Rows.Add("32811", "sobstvennoe_imya_obekta", "собственное имя объекта");
            tab.Rows.Add("33000", "oks_nomer ", "номер");
            tab.Rows.Add("33001", "oks_naimenovanie_ob_nedv ", "наименование ОН");
            tab.Rows.Add("33002", "oks_kad_nomer ", "кадастровый номер окс");
            tab.Rows.Add("33003", "oks_pravoobladatel  ", "правообладатель окс ");
            tab.Rows.Add("33004", "area_len_other", "площадь, длина, глубина, объем");
            tab.Rows.Add("33005", "sost_oks ", "состояние объекта недвижимости");
            tab.Rows.Add("33010", "oks_naznachenie ", "назначение");

       



        }

        public static string[] Get_Array_Rus_Name() {

            List<string> list = new List<string>();

            for (int i = 0; i < tab.Rows.Count; i++)
            {
                list.Add(tab.Rows[i]["rus_name"].ToString());
           

            }

            return list.ToArray();


        }



        static public string Get_Rus_Name_by_kod_txf(string code)
        {
            string rez = "";
            rez = "not_sem";
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
            rez = "not_sem";
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


        static public string Get_Eng_Name_by_Rus_Name(string rusname)
        {
            string rez = "";
            rez = "not_sem";
            //rez = "";

            for (int i = 0; i < tab.Rows.Count; i++)
            {
                if (tab.Rows[i]["rus_name"].ToString() == rusname)
                {
                    rez = tab.Rows[i]["eng_name"].ToString();
                    break;

                }

            }

            return rez;
        }


    }
}
