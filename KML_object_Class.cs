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

using System.Reflection;

namespace convert1
{
    /// <summary>
    /// 1. создание файла kml со стилями
    /// 2. метод создания объекта
    /// </summary>
    class KML_object_class

       
    {
        static XmlDocument xDoc;
        static XmlNode InsertNode;
        static XmlNode xRoot;
        public static string mypath_kml;

        static Group_1_2_List_Struct all_obl;

        static XmlNode DocumentNode;



        public static void SAVE_KML_FILE()
        {
            xDoc.Save(mypath_kml);
        }

        public static void CREATE_KML_FILE(string s)
        {


            mypath_kml = s;
           
            DateTime dt = DateTime.Now;


            all_obl = new Group_1_2_List_Struct();

            File.WriteAllText(mypath_kml, Properties.Settings.Default.MyStr);
           
            xDoc = new XmlDocument();
            xDoc.Load(mypath_kml);


            ////////////////////////////////////////////////////
            //записать стили для категорий
            ////////////////////////////////////////////////////
            //General_use1

            XElement XElem_Style;
            XmlNode NodeStyleTemp;
            XmlAttribute attr_style;
            XmlNode NodeStyle;
            XmlNode Node_Style;

            //записать стили площадных объектов
            //
            for (int i = 0; i < Tab_Classifier_Square.tab.Rows.Count; i++)   
            {

   


                XElem_Style = new XElement("Style", //атрибут добавляется далее
                    new XElement("LineStyle",
                        new XElement("color", ((MyStyle_Square)Tab_Classifier_Square.tab.Rows[i]["Google_Style"]).ColorLine),
                        new XElement("width", ((MyStyle_Square)Tab_Classifier_Square.tab.Rows[i]["Google_Style"]).WidthLine)),
                    new XElement("PolyStyle",
                        new XElement("color", ((MyStyle_Square)Tab_Classifier_Square.tab.Rows[i]["Google_Style"]).ColorPoly)));

 

                Node_Style = MyExtensions.ConvertToXmlNode(XElem_Style);

                xRoot = xDoc.DocumentElement;  // корневой элемент
                InsertNode = xRoot.SelectSingleNode("Document"); // элемент Document

                NodeStyle = xDoc.CreateElement("Style");

                NodeStyleTemp = MyExtensions.ConvertToXmlNode(XElem_Style);

                NodeStyle.InnerXml = NodeStyleTemp.InnerXml;

                attr_style = xDoc.CreateAttribute("id");
                attr_style.Value = Tab_Classifier_Square.tab.Rows[i]["eng_name"].ToString();


               
                NodeStyle.Attributes.Append(attr_style);

                InsertNode.AppendChild(NodeStyle);
                
            }
            //
            //записать стили площадных объектов



            //записать стили линейных объектов (цикл)+++
            //
            for (int i = 0; i < Tab_Classifier_Line.tab.Rows.Count; i++)
            {

                //если в строке стиль для точки, то пропустить итерацию
              //  if (((MyStyle_Square)Tab_Classifier_Line.tab.Rows[i]["Google_Style"]).PointStyle != null)
              //  {

              //      continue;
              //  }


                XElem_Style = new XElement("Style", //атрибут добавляется далее
                    new XElement("LineStyle",
                        new XElement("color", ((MyStyle_Line)Tab_Classifier_Line.tab.Rows[i]["Google_Style"]).ColorLine),
                        new XElement("width", ((MyStyle_Line)Tab_Classifier_Line.tab.Rows[i]["Google_Style"]).WidthLine)));
                        

   

                Node_Style = MyExtensions.ConvertToXmlNode(XElem_Style);

                xRoot = xDoc.DocumentElement;  // корневой элемент
                InsertNode = xRoot.SelectSingleNode("Document"); // элемент Document

                NodeStyle = xDoc.CreateElement("Style");

                NodeStyleTemp = MyExtensions.ConvertToXmlNode(XElem_Style);

                NodeStyle.InnerXml = NodeStyleTemp.InnerXml;

                attr_style = xDoc.CreateAttribute("id");
                attr_style.Value = Tab_Classifier_Line.tab.Rows[i]["eng_name"].ToString();



                NodeStyle.Attributes.Append(attr_style);

                InsertNode.AppendChild(NodeStyle);

            }



            //
            //записать стили линейных объектов (цикл)+++




            //записать стили точечных объектов (цикл)+++
            //
       
            
           
            for (int i = 0; i < Tab_Classifier_Point.tab.Rows.Count; i++)
            {
                XElem_Style = new XElement("Style", //атрибут добавляется далее
                  new XElement("IconStyle",
                    new XElement("color", ((MyStyle_Point)Tab_Classifier_Point.tab.Rows[i]["Google_Style_Point"]).ColorPoint),
                      new XElement("scale", ((MyStyle_Point)Tab_Classifier_Point.tab.Rows[i]["Google_Style_Point"]).ScalePoint),
                      new XElement("Icon",
                          new XElement("href", ((MyStyle_Point)Tab_Classifier_Point.tab.Rows[i]["Google_Style_Point"]).LinkPoint))));  

                Node_Style = MyExtensions.ConvertToXmlNode(XElem_Style);

                xRoot = xDoc.DocumentElement;  // корневой элемент
                InsertNode = xRoot.SelectSingleNode("Document"); // элемент Document
                //XmlNode Node_Style;
                NodeStyle = xDoc.CreateElement("Style");

                NodeStyleTemp = MyExtensions.ConvertToXmlNode(XElem_Style);

                NodeStyle.InnerXml = NodeStyleTemp.InnerXml;

                attr_style = xDoc.CreateAttribute("id");


                attr_style.Value = Tab_Classifier_Point.tab.Rows[i]["eng_name"].ToString();

                NodeStyle.Attributes.Append(attr_style);

                InsertNode.AppendChild(NodeStyle);


            }

    


            SAVE_KML_FILE();
        }




        public static void CREATE_NODE(TXF_Object_Struct o)
        {
            string select_group_str1 = Tab_Classifier_Attributes.Get_Eng_Name_by_Rus_Name(GlobalVar.group_str1);
            string select_group_str2 = Tab_Classifier_Attributes.Get_Eng_Name_by_Rus_Name(GlobalVar.group_str2);

            string group_str1 ="";
            string group_str2 ="";
            
            //создание атрибутов XCData (семантика метки)
            string txt_XCData = Creat_Attributes(o);

            //создание  XCData (атрибуты)
            XCData xcd = new XCData(txt_XCData);

            // создание узла с атрибутами и координатами
            XElement NodePlacemarkXE = Creat_Xml_NodeXE(o, xcd);
            
            
           DocumentNode = xRoot.SelectSingleNode("Document");
           XmlNode FolderNode = xDoc.CreateElement("Folder");// создаем новый узел Folder
           XmlNode NameNode = xDoc.CreateElement("name");// создаем новый узел Name


           // 1 группировка
           //all_obl.ContainsOblast(o.oblast);

           Type myType1 = typeof(convert1.TXF_Object_Struct);
           FieldInfo name1 = myType1.GetField(select_group_str1);  // FieldInfo name1 = myType1.GetField("oblast");

           Type myType2 = typeof(convert1.TXF_Object_Struct);
           FieldInfo name2 = myType2.GetField(select_group_str2);  //FieldInfo name2 = myType2.GetField("category_txt_rus");

   

           //поля через рефлексию
           try
           {
               group_str1 = name1.GetValue(o).ToString(); //группировка1
               
           }
           catch
           {
               group_str1 = "";
           }


           try
           {
               group_str2 = name2.GetValue(o).ToString(); //группировка2   
              
           }
           catch
           {
               group_str2 = "";
           }
          
 
           //group_str1 = o.oblast; //группировка
           //group_str2 = o.pravoobladatel; //группировка2   group_str2 = o.category_txt_rus; //группировка2



           //добавить область в структуру (если нет)
           //
           if (!all_obl.Contain_Group1(group_str1)) 
           {
               //MessageBox.Show("A");
               NameNode.InnerText = group_str1;
               FolderNode.AppendChild(NameNode);

               if ( (GlobalVar.group == "1") | (GlobalVar.group == "2") )
               {
                   DocumentNode.AppendChild(FolderNode); //!!
               }

               all_obl.AddGroup1(group_str1); 
               

           }
           //
           //добавить область в структуру (если нет)
            
           
            
          string str_temp="";


          //добавить категорию в структуру (если нет в заданной области)
          //

              if (!all_obl.Contain_Group1_and_Group2(group_str1, group_str2))  //новая версия
              {
                  FolderNode = xDoc.CreateElement("Folder");// создаем новый узел Folder
                  NameNode = xDoc.CreateElement("name");// создаем новый узел Name
                  NameNode.InnerText = group_str2;
                  FolderNode.AppendChild(NameNode);

                  str_temp = "Document/Folder[name='" + group_str1 + "']";
                  InsertNode = xRoot.SelectSingleNode(str_temp);
                  
                  if (GlobalVar.group == "2")
                  {
                      InsertNode.AppendChild(FolderNode); //!!
                  }


                  all_obl.AddOblast_and_Category(group_str1, group_str2);   //новая версия++
              }
          

          //
          //добавить категорию в структуру (если нет в заданной области)






          if (GlobalVar.group == "0")
          {
            InsertNode = xRoot.SelectSingleNode("Document"); // выбрать узел для вставки
          }
          if (GlobalVar.group == "1")
          {
              InsertNode = xRoot.SelectSingleNode("Document/Folder[name='" + group_str1 + "']"); // выбрать узел для вставки
          }
          if (GlobalVar.group == "2")
          {
              InsertNode = xRoot.SelectSingleNode("Document/Folder[name='" + group_str1 + "']/Folder[name='" + group_str2 + "']"); // выбрать узел для вставки
          }

          XmlNode NodePlacemark = xDoc.CreateElement("Placemark");
          XmlNode NodePlacemarkTemp = MyExtensions.ConvertToXmlNode(NodePlacemarkXE);
          NodePlacemark.InnerXml = NodePlacemarkTemp.InnerXml;
          InsertNode.AppendChild(NodePlacemark);


          //GlobalVar.text_for_tb7 += "Document/Folder[name='" + group_str1 + "']/Folder[name='" + group_str2 + "']" + Environment.NewLine;

          //
          //сформировать XCData и узел



         
        }  //сформировать узел, вставить и сохранить


        ///////////////////////////////

        /// <summary>
        /// создание атрибутов XCData
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static string Creat_Attributes(TXF_Object_Struct o)
        {
            string txt_XCData = "";
            //формирование XCData,  проверка на пустое значение
            //

            //if (o.category_kod != null) txt_XCData += "<b>категория</b> = " + Tab_Classifier_Square.Get_Rus_Name(o.category_kod) + " <br>";
            if (o.category_kod != null) txt_XCData += "<b>category_txt_rus</b> = " + o.category_txt_rus + " <br>";
            if (o.category_kod != null) txt_XCData += "<b>category_txt_eng</b> = " + o.category_txt_eng + " <br>";
            if (o.category_kod != null) txt_XCData += "<b>категория2_kod</b> = " + o.category_kod + " <br>";

            //if (o.category_kod != null) txt_XCData += "<b>категория4</b> = " + Tab_Classifier_Square.Get_Eng_Name_by_kod_txf(o.category_kod) + " <br>";
            if (o.obj != null) txt_XCData += "<b>объект</b> = " + o.obj + " <br>";

            if (o.id != null) txt_XCData += "<b>id</b> = " + o.id + " <br>";
            if (o.oblast != null) txt_XCData += "<b>Область</b> = " + o.oblast + " <br>";
            if (o.raion != null) txt_XCData += "<b>Район</b> = " + o.raion + " <br>";
            if (o.kadastr_number != null) txt_XCData += "<b>КАДАСТРОВЫЙ НОМЕР</b> = " + o.kadastr_number + " <br>";
            if (o.kadastr_number_part != null) txt_XCData += "<b>Кадастр номер отд уч</b> = " + o.kadastr_number_part + " <br>";
            if (o.vid_prava != null) txt_XCData += "<b>ВИД ПРАВА</b> = " + o.vid_prava + " <br>";
            if (o.pravoobladatel != null) txt_XCData += "<b>Правообладатель</b> =" + o.pravoobladatel + "<br>";

            
            if (o.area != null) txt_XCData += "<b>Площадь (га)</b> = " + String.Format("{0:N2}", Convert.ToDouble(o.area)) + "<br>";

            if (o.area_all != null) txt_XCData += "<b>Общая площадь (га)</b> = " + String.Format("{0:N2}", Convert.ToDouble(o.area_all)) + "<br>";
            if (o.area_object != null) txt_XCData += "<b>Площадь по объекту (га)</b> = " + String.Format("{0:N2}", Convert.ToDouble(o.area_object)) + " <br>";


            if (o.nomer_polya != null) txt_XCData += "<b>номер поля</b> = " + o.nomer_polya + " <br>";

            if (o.arendodatel != null) txt_XCData += "<b>арендодатель</b> = " + o.arendodatel + " <br>";
            if (o.date_begin != null) txt_XCData += "<b>дата начала аренды (гч)</b> = " + o.date_begin + " <br>";
            if (o.date_end != null) txt_XCData += "<b>дата окончания аренды (гч)</b> = " + o.date_end + " <br>";


            if (o.arendator != null) txt_XCData += "<b>арендатор</b> = " + o.arendator + " <br>";
            if (o.date_begin_atl != null) txt_XCData += "<b>дата начала аренды (атл)</b> = " + o.date_begin_atl + " <br>";
            if (o.date_end_atl != null) txt_XCData += "<b>дата окончания аренды (атл)</b> = " + o.date_end_atl + " <br>";

            if (o.risk != null) txt_XCData += "<b>Риск утраты урожая</b> = " + o.risk + " <br>";
            if (o.perspective != null) txt_XCData += "<b>Перспектива оформления</b> = " + o.perspective + " <br>";

            if (o.comments1 != null) txt_XCData += "<b>Комментарий 1</b> = " + o.comments1 + " <br>";
            if (o.comments2 != null) txt_XCData += "<b>Комментарий 2</b> = " + o.comments2 + " <br>";
            if (o.comments3 != null) txt_XCData += "<b>Комментарий 3</b> = " + o.comments3 + " <br>";
            if (o.comments4 != null) txt_XCData += "<b>Комментарий 4</b> = " + o.comments4 + " <br>";

            if (o.vvod_v_sev != null) txt_XCData += "<b>ввод в севооборот</b> = " + o.vvod_v_sev + " <br>";
            if (o.data_vvoda != null) txt_XCData += "<b>дата ввода</b> = " + o.data_vvoda + " <br>";
            if (o.prichina_nevozmojnosti_vvoda != null) txt_XCData += "<b>причина невозможности ввода</b> = " + o.prichina_nevozmojnosti_vvoda + " <br>";
            if (o.area_vvod != null) txt_XCData += "<b>площадь ввода</b> = " + o.area_vvod + " <br>";
            if (o.neobh_vunosa_tochek != null) txt_XCData += "<b>необходимость выноса точек</b> = " + o.neobh_vunosa_tochek + " <br>";
            if (o.kol_tochek != null) txt_XCData += "<b>количество точек</b> = " + o.kol_tochek + " <br>";
            if (o.srok_mej_rabot != null) txt_XCData += "<b>срок межевых работ</b> = " + o.srok_mej_rabot + " <br>";
            if (o.data_sbora != null) txt_XCData += "<b>дата сбора урожая</b> = " + o.data_sbora + " <br>";
            if (o.daln_isp != null) txt_XCData += "<b>дальнейшее использование</b> = " + o.daln_isp + " <br>";

            if (o.meropriyatiye != null) txt_XCData += "<b>мероприятия</b> = " + o.meropriyatiye + " <br>"; //мероприятия

            if (o.name_object != null) txt_XCData += "<b>название</b> = " + o.name_object + " <br>";

            if (o.bonitet != null) txt_XCData += "<b>бонитет</b> = " + o.bonitet + " <br>";
            if (o.budjet_na_vvod != null) txt_XCData += "<b>бюджет на ввод в севооборот</b> = " + o.budjet_na_vvod + " <br>";

            if (o.prichina_nevozmozhnosti_vvoda_2 != null) txt_XCData += "<b>причина невозможности ввода 2</b> = " + o.prichina_nevozmozhnosti_vvoda_2 + " <br>";
            if (o.zaklyuchenie_o_celesoobraznosti_otkaza != null) txt_XCData += "<b>Заключение о целесообразности отказа от права</b> = " + o.zaklyuchenie_o_celesoobraznosti_otkaza + " <br>";
            if (o.raskhody_v_god_na_neisp != null) txt_XCData += "<b>Расходы в год на неиспользуемую чзу</b> = " + o.raskhody_v_god_na_neisp + " <br>";
            if (o.soputstvuyushchie_raskhody != null) txt_XCData += "<b>Сопутствующие расходы</b> = " + o.soputstvuyushchie_raskhody + " <br>";
            if (o.procent_neispol != null) txt_XCData += "<b>Процент неиспользуемого</b> = " + o.procent_neispol + " <br>";
            if (o.kadastr_stoimost != null) txt_XCData += "<b>Кадастровая стоимость</b> = " + o.kadastr_stoimost + " <br>";

            if (o.tekushchee_sostoyanie != null) txt_XCData += "<b>Текущее состояние</b> = " + o.tekushchee_sostoyanie + " <br>";
            if (o.plany != null) txt_XCData += "<b>Планы</b> = " + o.plany + " <br>";

            if (o.obosnovanie_nevozmozhnosti_vvoda != null) txt_XCData += "<b>обоснование невозможности ввода</b> = " + o.obosnovanie_nevozmozhnosti_vvoda + " <br>";
            if (o.area_jou != null) txt_XCData += "<b>площадь ЖОУ</b> = " + o.area_jou + " <br>";
            if (o.plan_god_oformlenia != null) txt_XCData += "<b>Планируемый год оформления</b> = " + o.plan_god_oformlenia + " <br>";


            if (o.oks_nomer != null) txt_XCData += "<b>номер</b> = " + o.oks_nomer + " <br>";
            if (o.oks_naimenovanie_ob_nedv != null) txt_XCData += "<b>наименование объекта недвижимости</b> = " + o.oks_naimenovanie_ob_nedv + " <br>";
            if (o.oks_naznachenie != null) txt_XCData += "<b>назначение</b> = " + o.oks_naznachenie + " <br>";
            if (o.oks_kad_nomer != null) txt_XCData += "<b>кадастровый номер</b> = " + o.oks_kad_nomer + " <br>";
            if (o.oks_pravoobladatel != null) txt_XCData += "<b>правообладатель</b> = " + o.oks_pravoobladatel + " <br>";
            if (o.sost_oks != null) txt_XCData += "<b>состояние объекта недвижимости</b> = " + o.sost_oks + " <br>";
            if (o.area_len_other != null) txt_XCData += "<b>площадь, длина, глубина, объем (м)</b> = " + o.area_len_other + " <br>";
            //if (o.sobstvennoe_imya_obekta != null) txt_XCData += "<b>имя объекта</b> = " + o.sobstvennoe_imya_obekta + " <br>";




            //
            //формирование XCData,  проверка на пустое значение
            return txt_XCData;
        }


        public static XElement Creat_Xml_NodeXE(TXF_Object_Struct o, XCData xcd)
        {
            XElement NodePlacemarkXE = new XElement("tempname");
            string coord_object = "";


            //узел точечного объекта
            //

            string style_point = "";
            style_point = o.category_txt_eng;

            if (o.localization_object == localization_enum.point)
            {

                coord_object = o.polygons[0].LY[0].ToString() + "," + o.polygons[0].BX[0].ToString() + ",0 ";

                NodePlacemarkXE = new XElement(
                    new XElement("Placemark",
                            new XElement("description", xcd),
                            new XElement("styleUrl", "#" + style_point),  //присвоение стиля
                            new XElement("Point",
                                new XElement("coordinates", coord_object)
                                )

                            )
                        );

            }
            //
            //узел точечного объекта


            //узел линейного объекта
            //
            string style_line = "";
            style_line = o.category_txt_eng;



            if (o.localization_object == localization_enum.line)
            {
                for (int m = 0; m < o.polygons[0].LY.Count; m++)
                {
                    coord_object += o.polygons[0].LY[m].ToString() + "," + o.polygons[0].BX[m].ToString() + ",0 ";

                }


                NodePlacemarkXE = new XElement(
                    new XElement("Placemark",
                        new XElement("name", o.own_name),   //СОБСТВЕННОЕ НАЗВАНИЕ  new XElement("name", o.own_name),
                            new XElement("description", xcd),
                                new XElement("styleUrl", "#" + style_line),   //стиль  
                                    new XElement("LineString",
                                                new XElement("coordinates", coord_object)

                        )
                   )
                );

            }
            //
            //узел линейного объекта



            //узел площадного объекта
            //

            string style_square = "";
            style_square = o.category_txt_eng;



            //MessageBox.Show(o.kadastr_number);
            if (o.localization_object == localization_enum.area)
            {
                for (int m = 0; m < o.polygons[0].LY.Count; m++)
                {
                    coord_object += o.polygons[0].LY[m].ToString() + "," + o.polygons[0].BX[m].ToString() + ",0 ";

                }


                //
                //MessageBox.Show(Tab_Classifier.Get_Eng_Name(o.category));
                NodePlacemarkXE = new XElement(
                    new XElement("Placemark",
                        new XElement("name", o.own_name),   //СОБСТВЕННОЕ НАЗВАНИЕ  new XElement("name", o.own_name),
                            new XElement("description", xcd),
                                new XElement("styleUrl", "#" + style_square),   //стиль 
                                    new XElement("Polygon",
                                        new XElement("outerBoundaryIs",
                                            new XElement("LinearRing",
                                                new XElement("coordinates", coord_object)
                                )
                            )
                        )
                   )
                );



                //добавление подобъекта
                XElement XEinnerBoundaryIs = new XElement("innerBoundaryIs");

                for (int j = 1; j < o.polygons.Count; j++)
                {

                    string coord_sub_obj = "";

                    for (int k = 0; k < o.polygons[j].BX.Count; k++)
                    {
                        coord_sub_obj += o.polygons[j].LY[k].ToString() + "," + o.polygons[j].BX[k].ToString() + ",0 ";
                    }

                    XElement LinearRingXE = new XElement(
                        new XElement("LinearRing",
                            new XElement("coordinates", coord_sub_obj)));

                    XEinnerBoundaryIs.Add(LinearRingXE);

                }


                if (o.polygons.Count > 1)  //добавить узлы подобъектов
                {
                    NodePlacemarkXE.Element("Polygon").Add(XEinnerBoundaryIs);
                }
                //
                //координаты для площадного объекта


            }
            //
            //узел площадного объекта



            return NodePlacemarkXE;
        }


    }
}
