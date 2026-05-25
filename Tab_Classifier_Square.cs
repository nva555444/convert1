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
    static class Tab_Classifier_Square
    {
        static public DataTable tab;

        static Tab_Classifier_Square()
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
            my_column5.DataType = System.Type.GetType("convert1.MyStyle_Square");


            tab.Columns.Add(my_column1);
            tab.Columns.Add(my_column2);
            tab.Columns.Add(my_column3);
            tab.Columns.Add(my_column4);
            tab.Columns.Add(my_column5);

            //в конструкторе по умолчанию ширина линии =2, можно указать свою, но все ломается, переработать присвоение стиля //чинится сменой режима в гугл еарз, нужно сменить с опенджл на дайрект икс, код не причем

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

            //tab.Rows.Add("52", "0", "prochee_square", "прочие площадные", new MyStyle("ff00ffff", "ff00ff00"));

            tab.Rows.Add("52", "0", "prochee_square", "прочие площадные", new MyStyle_Square("ff000000", "8c0055ff"));

            tab.Rows.Add("21", "1001", "zem_uch_gch", "земельные участки", new MyStyle_Square("ff00aaff", "4000aaff"));

            tab.Rows.Add("16", "2242", "obrabatyvayemyye", "обрабатываемые", new MyStyle_Square("ff00ff00", "4000ff00", "2"));

            //tab.Rows.Add("52", "0", "gazoprovod", "газопровод", new MyStyle("ff54fcfc", "4054fcfc"));
           // tab.Rows.Add("54", "0", "lep", "лэп", new MyStyle("ffff00ff", "40ff00ff"));

            tab.Rows.Add("1","2279", "arenda_prodazha",     "продажа", new MyStyle_Square("ff0055ff", "400055ff"));
            
            tab.Rows.Add("2","2278", "vvod_v_2017", "ввод в 2017", new MyStyle_Square("ff7faaff", "407faaff"));
            tab.Rows.Add("3","2269", "vvod_v_2018", "ввод в 2018", new MyStyle_Square("ff7faaff", "407faaff"));
            tab.Rows.Add("4","2268", "vvod_posle_2018", "ввод после 2018", new MyStyle_Square("ff7faaff", "407faaff"));
            tab.Rows.Add("5","2280", "vvod_2020", "ввод в 2020", new MyStyle_Square("ff7faaff", "a6007c7c"));  //2020  //было new MyStyle("ff7faaff", "8c00aa00"));
            tab.Rows.Add("6","2297", "vvod_2019-2021",      "ввод в 2020-2022", new MyStyle_Square("ff7faaff", "407faaff"));
            tab.Rows.Add("7","2281", "vvod_2019", "ввод в 2019", new MyStyle_Square("ff7faaff", "407faaff"));
            tab.Rows.Add("8", "2282", "vvod_2021", "ввод в 2021", new MyStyle_Square("ff7faaff", "a6007c7c")); //2021
            tab.Rows.Add("9","2243", "neobrabatyvayemyye", "необрабатываемые", new MyStyle_Square("ff54fcfc", "4054fcfc"));
            tab.Rows.Add("10","2244", "neugodya_k_vvodu_ne_planiruyutsya", "неугодья (к вводу не планируются)", new MyStyle_Square("ffff0000", "40ff0000"));
            tab.Rows.Add("11","2266", "neugodya_korchevaniye", "неугодья (корчевание)", new MyStyle_Square("ffffff00", "40ffff00"));
            tab.Rows.Add("12","2272", "neudobnyye_po_logistike", "неудобные по логистике", new MyStyle_Square("ffffffaa", "40ffffaa", "1.5"));
            tab.Rows.Add("13","2246", "obyekty", "объекты", new MyStyle_Square("ff000000", "7f000000"));
            tab.Rows.Add("14","2247", "peredany_v_arendu", "переданы в аренду", new MyStyle_Square("ffff00ff", "40ff00ff"));
            
            tab.Rows.Add("15","2276", "plan_arenda", "план. Аренда", new MyStyle_Square("ffff00ff", "40ff00ff"));
            
            

            tab.Rows.Add("17","2245", "samozakhvat", "самозахват", new MyStyle_Square("ff0000ff", "400000ff"));
            tab.Rows.Add("18","2265", "samozakhvat_po_kadastru", "самозахват (по кадастру)", new MyStyle_Square("0.5", "19ffffff"));
            tab.Rows.Add("19","2298", "v_stadii_oformleniya", "в стадии оформления", new MyStyle_Square("ffffaaaa", "40ffaaaa"));
            tab.Rows.Add("20","2262", "samozakhvat_3_lic", "самозахват 3 лиц", new MyStyle_Square("ff0000ff", "400000ff"));
            
            tab.Rows.Add("22","2303", "neobr_menee_1_ga",   "необрабатываемые, менее 1 га", new MyStyle_Square("ffff5500", "40ff5500"));
            tab.Rows.Add("23","2304", "priobretenu",        "приобретены",                  new MyStyle_Square("ff7fff00", "407fff00"));
            tab.Rows.Add("24", "2305", "vvod_2020-2024", "ввод в 2020-2024", new MyStyle_Square("ff7faaff", "a67faaff"));  //2020-2024
            tab.Rows.Add("25", "2307", "otkaz_ot_isp", "отказ от использования", new MyStyle_Square("ff7fffff", "737fffff"));

            tab.Rows.Add("26", "2295", "vvod_2022", "ввод в 2022", new MyStyle_Square("ff7faaff", "737fffff")); //2022
            tab.Rows.Add("27", "2296", "vvod_2023", "ввод в 2023", new MyStyle_Square("ff7faaff", "8c0055ff")); //2023
            tab.Rows.Add("28", "2308", "vvod_2024", "ввод в 2024", new MyStyle_Square("ff7faaff", "407faaff")); //2024

            tab.Rows.Add("11111", "2309", "obrabotka_q", "обработка ?", new MyStyle_Square("ff00aa00", "a658ffbf")); //обработка ?

            tab.Rows.Add("11111", "2241", "obrabotka_po_gps", "обработка по gps", new MyStyle_Square("ff00ff00", "4000ff00")); //обработка по gps

            tab.Rows.Add("11111", "2310", "neugodya_celesoobrazno", "неугодья (отказ целесообразен)", new MyStyle_Square("ff0000ff", "8cffaa00")); //
            tab.Rows.Add("11111", "2311", "neugodya_necelesoobrazno", "неугодья (отказ нецелесообразен)", new MyStyle_Square("ff00aa00", "8cff0055")); //

            tab.Rows.Add("11111", "2358", "samozakhvat_vysokiy_risk", "самозахват (1 - высокий риск)", new MyStyle_Square("ff0000ff", "8c5050ff")); //
            tab.Rows.Add("11111", "2359", "samozakhvat_sredniy_risk", "самозахват (2 - средний риск)", new MyStyle_Square("ff0000ff", "bf0055ff")); //
            tab.Rows.Add("11111", "2360", "samozakhvat_nizkiy_risk", "самозахват (3 - низкий риск)", new MyStyle_Square("ff0000ff", "d945e0ff")); //
            


            /*
            tab.Rows.Add("29", "6001", "svinokomplex_real", "объекты", new MyStyle("P0000006001.png")); //свинокомплекс
            tab.Rows.Add("30", "6002", "svinokomplex_str", "объекты", new MyStyle("P0000006002.png")); //свинокомплекс
            tab.Rows.Add("31", "6003", "svinokomplex_plan", "объекты", new MyStyle("P0000006003.png")); //свинокомплекс

            tab.Rows.Add("32", "6004", "ptitsekompleks_real", "объекты", new MyStyle("P0000006004.png")); //
            tab.Rows.Add("33", "6005", "ptitsekompleks_str", "объекты", new MyStyle("P0000006005.png")); //
            tab.Rows.Add("34", "6006", "ptitsekompleks_plan", "объекты", new MyStyle("P0000006006.png")); //свинокомплекс

            tab.Rows.Add("35", "6007", "kkz_real", "объекты", new MyStyle("P0000006007.png")); //
            tab.Rows.Add("36", "6008", "kkz_str", "объекты", new MyStyle("P0000006008.png")); //
            tab.Rows.Add("37", "6009", "kkz_plan", "объекты", new MyStyle("P0000006009.png")); //

            tab.Rows.Add("38", "6016", "baza_real", "объекты", new MyStyle("P0000006016.png")); //
            tab.Rows.Add("39", "6017", "baza_str", "объекты", new MyStyle("P0000006017.png")); //
            tab.Rows.Add("40", "6018", "baza_plan", "объекты", new MyStyle("P0000006018.png")); //

            tab.Rows.Add("41", "6010", "elevator_real", "объекты", new MyStyle("P0000006010.png")); //
            tab.Rows.Add("42", "6011", "elevator_str", "объекты", new MyStyle("P0000006011.png")); //
            tab.Rows.Add("43", "6021", "elevator_plan", "объекты", new MyStyle("P0000006012.png")); //

            tab.Rows.Add("44", "6013", "hpp_real", "объекты", new MyStyle("P0000006013.png")); //
            tab.Rows.Add("45", "6014", "hpp_str", "объекты", new MyStyle("P0000006014.png")); //
            tab.Rows.Add("46", "6015", "hpp_plan", "объекты", new MyStyle("P0000006015.png")); //

            tab.Rows.Add("47", "6030", "zavod", "объекты", new MyStyle("P0000006030.png")); //

            tab.Rows.Add("48", "6032", "office", "объекты", new MyStyle("P0000006032.png")); //

            tab.Rows.Add("49", "6029", "obyekt", "объекты", new MyStyle("P0000006029.png")); //

            tab.Rows.Add("50", "6025", "uboynyy_tsekh", "объекты", new MyStyle("P0000006025.png")); //

            tab.Rows.Add("51", "2267", "zernosushilny_complex", "объекты", new MyStyle("P0000002267.png")); //
            */

            
            

            //string m = tab.Rows["23"]["23"].ToString();

            //tab.Rows.Add(



        }

        static public string Get_Rus_Name_by_kod_txf(string code)
        {
            string rez = "";
            rez = "прочие_площадные";
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
            rez = "prochee_square";
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



        static public bool Get_Contain_Cat(string code)
        {
            bool rez = false;
            for (int i = 0; i < tab.Rows.Count; i++)
            {
                if (tab.Rows[i]["code_in_txf"].ToString() == code)
                {
                    rez = true;
                    break;
                }

            }

            return rez;
        }



    }//Generalizing_Object_Struct



}
