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

public enum localization_enum { area, point, line, unknown}



namespace convert1
{
    class PolygonType
    {
        public List<decimal> BX;
        public List<decimal> LY;

    }


    class TXF_Object_Struct
    {

        public string own_name;

        public string category_kod;
        public string category_txt_rus;
        public string category_txt_eng;
        //public string category_style;

        public string obj;

        public string raion;
        public string oblast;
        public string kadastr_number;
        public string vid_prava;
        public string pravoobladatel;
        public string area;
        public string kadastr_number_part;
        public string area_all;
        public string area_object;
        public string nomer_polya;
        public string id;
        public string arendator;
        public string arendodatel;
        public string date_begin;
        public string date_end;

        public string comments1;
        public string risk;
        public string perspective;
        public string meropriyatiye;

        public string date_begin_atl;
        public string date_end_atl;

        public string vvod_v_sev;
        public string data_vvoda;
        public string prichina_nevozmojnosti_vvoda;
        public string area_vvod;
        public string neobh_vunosa_tochek;
        public string kol_tochek;
        public string srok_mej_rabot;

        public string data_sbora;
        public string daln_isp;

        public string comments2;

        public string name_object;
        public string bonitet;
        public string budjet_na_vvod;

        public string comments3;
        public string comments4;

        public string prichina_nevozmozhnosti_vvoda_2;
        public string zaklyuchenie_o_celesoobraznosti_otkaza;
        public string raskhody_v_god_na_neisp;
        public string soputstvuyushchie_raskhody;
        public string procent_neispol;

        public string kadastr_stoimost;

        public string tekushchee_sostoyanie;
        public string plany;

        public string obosnovanie_nevozmozhnosti_vvoda;
        public string area_jou;

        public string plan_god_oformlenia;  //Планируемый год оформления


        public string oks_nomer;  //номер 33000
        public string oks_naimenovanie_ob_nedv; //наименование ОН 33001 //№1
        public string oks_kad_nomer;  //кадастровый номер 33002
        public string oks_naznachenie;  //назначение 33010

        public string sobstvennoe_imya_obekta;  //собственное имя объекта 32811

        public string oks_pravoobladatel;  //правообладатель окс 33003
        public string area_len_other;  //площадь, длина, глубина, объем 33004
        public string sost_oks;  //состояние объекта недвижимости 33005



        public localization_enum localization_object = localization_enum.unknown;


        public List<PolygonType> polygons;  //лист подобъектов с координатами
        /*
        public TXF_Object_Struct()
        {
            category_kod = "";
            category_txt_rus = "";
            category_txt_eng = "";

            obj = "";

            raion = "";
            oblast = "";
            kadastr_number = "";
            vid_prava = "";
            pravoobladatel = "";
            area = "";
            kadastr_number_part = "";
            area_all = "";
            area_object = "";
            nomer_polya = "";
            id = "";
            arendator = "";
            arendodatel = "";
            date_begin = "";
            date_end = "";

            comments1 = "";
            risk = "";
            perspective = "";
            meropriyatiye = "";

            date_begin_atl = "";
            date_end_atl = "";

            vvod_v_sev = "";
            data_vvoda = "";
            prichina_nevozmojnosti_vvoda = "";
            area_vvod = "";
            neobh_vunosa_tochek = "";
            kol_tochek = "";
            srok_mej_rabot = "";

            data_sbora = "";
            daln_isp = "";

            comments2 = "";

            name_object = "";
            bonitet = "";
            budjet_na_vvod = "";

            comments3 = "";
            comments4 = "";

            prichina_nevozmozhnosti_vvoda_2 = "";
            zaklyuchenie_o_celesoobraznosti_otkaza = "";
            raskhody_v_god_na_neisp = "";
            soputstvuyushchie_raskhody = "";
            procent_neispol = "";

            kadastr_stoimost = "";

            tekushchee_sostoyanie = "";
            plany = "";

            obosnovanie_nevozmozhnosti_vvoda = "";
            area_jou = "";

            plan_god_oformlenia = "";  //Планируемый год оформления


            oks_nomer = "";  //номер 33000
            oks_naimenovanie_ob_nedv = ""; //наименование ОН 33001 //№1
            oks_kad_nomer = "";  //кадастровый номер 33002
            oks_naznachenie = "";  //назначение 33010

            sobstvennoe_imya_obekta = "";  //собственное имя объекта 32811

            oks_pravoobladatel = "";  //правообладатель окс 33003
            area_len_other = "";  //площадь, длина, глубина, объем 33004
            sost_oks = "";  //состояние объекта недвижимости 33005
        }
        */


        public void Create_One_Object(List<string> List_TXF_String)  //получить объект структуры TXF_Object_Struct
        {


            string phase = "";

            //polygons = new List<PolygonType>();



            polygons = new List<PolygonType>();
            polygons.Clear();

            //MessageBox.Show(polygons.Count.ToString());

            string tempcoord = "";
            int i = 0;
            phase = "блок начальных атрибутов";

            PolygonType polygon1 = new PolygonType();

            polygon1.BX = new List<decimal>();
            polygon1.LY = new List<decimal>();



            foreach (string s in List_TXF_String)
            {

                i++;

                //GlobalVar.text_for_tb5 += s + Environment.NewLine;


                //////////////////////////////////////
                //* записать локализацию, код, тип объекта в объект
                //////////////////////////////////////
                //
                try
                {
                    if (s.Substring(0, 4) == ".OBJ") //тип объекта
                    {
                        Regex reg = new Regex(@"(-{0,1}[0-9]{1,}[.]{0,1}[0-9]{1,})");  // .OBJ   2244   SQR
                        category_kod = reg.Match(s).Value;
                        



                        if (s.Contains("SQR"))
                        {
                            localization_object = localization_enum.area;
                            category_txt_rus = Tab_Classifier_Square.Get_Rus_Name_by_kod_txf(category_kod);
                            category_txt_eng = Tab_Classifier_Square.Get_Eng_Name_by_kod_txf(category_kod);
                        }
                        if (s.Contains("DOT"))
                        {
                            localization_object = localization_enum.point;
                            category_txt_rus = Tab_Classifier_Point.Get_Rus_Name_by_kod_txf(category_kod);
                            category_txt_eng = Tab_Classifier_Point.Get_Eng_Name_by_kod_txf(category_kod);
                        }
                        if (s.Contains("LIN"))
                        {
                            localization_object = localization_enum.line;
                            category_txt_rus = Tab_Classifier_Line.Get_Rus_Name_by_kod_txf(category_kod);
                            category_txt_eng = Tab_Classifier_Line.Get_Eng_Name_by_kod_txf(category_kod);

                        }

                        //MessageBox.Show(localization_object.ToString());

                      
                    }
                }
                catch
                {

                }
                //
                //////////////////////////////////////
                //* записать тип объекта в объект
                //////////////////////////////////////





                //////////////////////////////////////
                //* записать координаты
                //////////////////////////////////////
                try
                {
                    Regex reg = new Regex(@"(-{0,1}[0-9]{1,}[.]{1}[0-9]{1,}   -{0,1}[0-9]{1,}[.]{1}[0-9]{1,})");
                    if (reg.IsMatch(s))
                    {
                        phase = "блок координат объекта";


                        reg = new Regex(@"(-{0,1}[0-9]{1,}[.]{1}[0-9]{1,})");
                        MatchCollection matchs = reg.Matches(s);


                        polygon1.BX.Add(Convert.ToDecimal(matchs[0].Value));
                        polygon1.LY.Add(Convert.ToDecimal(matchs[1].Value));

                        //MessageBox.Show(polygon1.BX[1].ToString());


                        //ob.BX.Add(Convert.ToDecimal(matchs[0].Value));
                        //ob.LY.Add(Convert.ToDecimal(matchs[1].Value));

                        tempcoord += " k1 " + matchs[0].Value + " k2 " + matchs[1].Value + " enter ";


                        //textBox6.AppendText("запись координат" + Environment.NewLine);

                    }
                }
                catch
                {
                    //textBox6.AppendText("error запись координат" + Environment.NewLine);
                }


                //подобъект
                if (phase == "блок координат объекта")
                {

                    Regex rg = new Regex(@"^[0-9]+$");  //1 цифра

                    MatchCollection m_col = rg.Matches(s);



                    if ((rg.IsMatch(s)))    //идет блок координат и одна цифра
                    {

                        polygons.Add(polygon1);

                        polygon1 = new PolygonType();


                        polygon1.BX = new List<decimal>();
                        polygon1.LY = new List<decimal>();


                        tempcoord += " suobj ";
                    }

                    //регулярное выражение 1 цифра

                }
                //
                ////
                //////////////////////////////////////
                //* записать координаты
                //////////////////////////////////////


                //////////////////////////////////////
                //* семантика
                //////////////////////////////////////
                //
                try
                {
                    string pattern;
                    pattern = @"(^[0-9]{1,}   .{1,})";  //сначала проверить, соответствует ли вся строка данному шаблону

                    Regex reg = new Regex(pattern);


                    MatchCollection matchs = reg.Matches(s);

                    if (reg.IsMatch(s))
                    {
                        if (phase == "блок координат объекта")
                        {
                            polygons.Add(polygon1); // добавить последний подобъект
                        }


                        phase = "блок семантики";

                        string rez1; //код семантики
                        string rez2; //значение семантики

                        pattern = @"([0-9]{1,}   )|(.{1,})";
                        reg = new Regex(pattern);
                        matchs = reg.Matches(s);

                        rez1 = matchs[0].Value;
                        rez2 = matchs[1].Value;

                        switch (Convert.ToInt32(rez1))
                        { 
                            case 1001: kadastr_number = rez2; break;

                            case 1002: vid_prava = rez2; break;
                            case 1003: pravoobladatel = rez2; break;
                            case 1004: area = rez2; break;
                            case 1005: obj = rez2; break;
                            case 1010: kadastr_number_part = rez2; break;
                            case 1011: area_all = rez2; break;
                            case 112: area_object = rez2; break;
                            case 9: own_name = rez2; break;
                            case 110: oblast = rez2; break;
                            case 111: raion = rez2; break;
                            case 1026: nomer_polya = rez2; break;
                            case 131: id = rez2; break;
                            case 1029: date_begin = rez2; break;
                            case 1025: date_end = rez2; break;
                            case 1023: arendator = rez2; break;
                            case 1024: arendodatel = rez2; break;
                            case 1032: comments1 = rez2; break;
                            case 1033: risk = rez2; break;
                            case 1034: perspective = rez2; break;
                            case 1035: meropriyatiye = rez2; break;
                            case 1051: date_begin_atl = rez2; break;
                            case 1052: date_end_atl = rez2; break;
                            case 1038: vvod_v_sev = rez2; break;
                            case 1039: data_vvoda = rez2; break;
                            case 1040: prichina_nevozmojnosti_vvoda = rez2; break;
                            case 1041: area_vvod = rez2; break;
                            case 1042: neobh_vunosa_tochek = rez2; break;
                            case 1043: kol_tochek = rez2; break;
                            case 1044: srok_mej_rabot = rez2; break;
                            case 1045: data_sbora = rez2; break;
                            case 1046: daln_isp = rez2; break;
                            case 1054: comments2 = rez2; break;
                            case 1027: name_object = rez2; break;
                            case 1059: bonitet = rez2; break;
                            case 1060: budjet_na_vvod = rez2; break;
                            case 1055: comments3 = rez2; break;
                            case 1056: comments4 = rez2; break;
                            ////
                            case 1075: prichina_nevozmozhnosti_vvoda_2 = rez2; break;
                            case 1076: zaklyuchenie_o_celesoobraznosti_otkaza = rez2; break;
                            case 1077: raskhody_v_god_na_neisp = rez2; break;
                            case 1078: soputstvuyushchie_raskhody = rez2; break;
                            case 1079: procent_neispol = rez2; break;
                            case 1081: kadastr_stoimost = rez2; break;
                            case 1082: tekushchee_sostoyanie = rez2; break;
                            case 1083: plany = rez2; break;
                            case 1084: obosnovanie_nevozmozhnosti_vvoda = rez2; break;
                            case 1085: area_jou = rez2; break;
                            case 1088: plan_god_oformlenia = rez2; break;

                            case 32811: sobstvennoe_imya_obekta = rez2; break;

                            case 33000: oks_nomer = rez2; break;
                            case 33001: oks_naimenovanie_ob_nedv = rez2; break;
                            case 33002: oks_kad_nomer = rez2; break;
                            case 33003: oks_pravoobladatel = rez2; break;
                            case 33004: area_len_other = rez2; break;
                            case 33010: oks_naznachenie = rez2; break;
                            case 33005: sost_oks = rez2; break;


 
                        }
                    }
                }
                catch
                {
                }


                //category = "prochee";

                //присвоение категории по семантике
                //

                if (GlobalVar.Colour_by_kod==false)
                {

                    if (localization_object == localization_enum.point)
                    {
                        category_kod = "0";
                        category_txt_rus = Tab_Classifier_Point.Get_Rus_Name_by_kod_txf(category_kod);
                        category_txt_eng = Tab_Classifier_Point.Get_Eng_Name_by_kod_txf(category_kod);
                    }

                    if (localization_object == localization_enum.line)
                    {
                        category_kod = "0";
                        category_txt_rus = Tab_Classifier_Line.Get_Rus_Name_by_kod_txf(category_kod);
                        category_txt_eng = Tab_Classifier_Line.Get_Eng_Name_by_kod_txf(category_kod);
                        //category_txt_rus = "prochee_line";
                        //MessageBox.Show("A");
                    }

                    if (localization_object == localization_enum.area)
                    {
                        category_kod = "0";
                        category_txt_rus = Tab_Classifier_Square.Get_Rus_Name_by_kod_txf(category_kod);
                        category_txt_eng = Tab_Classifier_Square.Get_Eng_Name_by_kod_txf(category_kod);
                        //category_txt_rus = "prochee_square";
                        //MessageBox.Show("A");
                    }



                    if ((localization_object == localization_enum.line) & (oks_naimenovanie_ob_nedv == "газопровод"))
                    {
                        category_kod="0_gaz";
                        category_txt_rus = Tab_Classifier_Line.Get_Rus_Name_by_kod_txf(category_kod);
                        category_txt_eng = Tab_Classifier_Line.Get_Eng_Name_by_kod_txf(category_kod);
                        //MessageBox.Show("A");
                    }


                    if ((localization_object == localization_enum.line) & (oks_naimenovanie_ob_nedv == "лэп"))
                    {
                        category_kod = "0_lep";
                        category_txt_rus = Tab_Classifier_Line.Get_Rus_Name_by_kod_txf(category_kod);
                        category_txt_eng = Tab_Classifier_Line.Get_Eng_Name_by_kod_txf(category_kod);
                    }



                    if ((localization_object == localization_enum.point) & (oks_naimenovanie_ob_nedv == "скважина"))
                    {
                        category_kod = "0_skvajina";
                        category_txt_rus = Tab_Classifier_Point.Get_Rus_Name_by_kod_txf(category_kod);
                        category_txt_eng = Tab_Classifier_Point.Get_Eng_Name_by_kod_txf(category_kod);
                    }
                


                }

                //
                //присвоение категории по семантике

                //
                ////
                //////////////////////////////////////
                //* семантика
                //////////////////////////////////////
            }  //foreach 1
        }
    }
}

/*

        public string oks_pravoobladatel;  //правообладатель окс 33003
        public string area_len_other;  //площадь, длина, глубина, объем 33004
        public string sost_oks;  //состояние объекта недвижимости 33005
 * */


