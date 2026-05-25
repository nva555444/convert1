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
namespace convert1
{
    /// <summary>
    /// читает построчно текстовый файл txf, 
    /// формирует текстовый блок-объект, 
    /// после его формирования сразу создает объект по структуре TXF_Object_Struct (считываются семантики из тхт)
    /// с помощью класса KML_object_class формирует узел xml
    /// </summary>
    class ReadTXT
    {
        public static void PARSING_ALL_TXF(string[] str)
        {
            List<string> TextContentObject = new List<string>(); //стринг лист содержания объекта


            bool CreateObjPro = false;

            int step = 0;

            // создание стринг листов объектов
            //
            foreach (string s in str)
            {
                step++;

                if (LauncClass._canceled == true)
                {
                    break;
                }

                if (s.Length >= 4)
                {
                    //MessageBox.Show(s);
                    if (((s.Substring(0, 4) == ".OBJ") | (s.Substring(0, 4) == ".END")) & (CreateObjPro == true)) //следующее обнаружение  ".OBJ"
                    {

                        TXF_Object_Struct ob = new TXF_Object_Struct();
                        ob.Create_One_Object(TextContentObject); // получить объект по структуре TXF_Object_Struct
                        KML_object_class.CREATE_NODE(ob);  //сформировать узел  // + + + + + + + + 

                        TextContentObject = new List<string>(); //создать новый лист для нового объекта
                        TextContentObject.Clear();
                        CreateObjPro = false;

                        //MessageBox.Show("1");
                        //получить объект по структуре TXF_Object_Struct
                    }

                    if ((s.Substring(0, 4) == ".OBJ") & (CreateObjPro == false))
                    {
                        CreateObjPro = true;
                        TextContentObject = new List<string>(); //создать новый лист для нового объетка
                        //GlobalVar.text_for_tb3 += "- - - -" + Environment.NewLine;
                        //MessageBox.Show("2");
                    }
                }


                TextContentObject.Add(s);  //добавление строчки в лист

                //GlobalVar.text_for_tb3 += "[" + step.ToString() + "] " + s + Environment.NewLine;



            }//foreach

            //
            // создание стринг листов объектов

            // return ListObjectsString;


        }
        /*
        //analysis
        public static void ANALISIS_ALL_TXF(string[] str)
        {

            List<string> TextContentObject = new List<string>(); //стринг лист содержания объекта


            bool CreateObjPro = false;

            int step = 0;

            // создание стринг листов объектов
            //
            foreach (string s in str)
            {
                step++;

                if (LauncClass._canceled == true)
                {
                    break;
                }

                if (s.Length >= 4)
                {
                    //MessageBox.Show(s);
                    if (((s.Substring(0, 4) == ".OBJ") | (s.Substring(0, 4) == ".END")) & (CreateObjPro == true)) //следующее обнаружение  ".OBJ"
                    {

                        TXF_Object_Struct ob = new TXF_Object_Struct();

                        ob.Create_One_Object(TextContentObject); // получить объект по структуре TXF_Object_Struct


                        if (ob.localization_object == localization_enum.area)
                        {
                            if (ob.category_kod == "1001") //==земельные участки ГЧ
                            {

                            }
                        }

                        //KML_object_class.CREATE_NODE(ob);  //сформировать узел  // + + + + + + + + 

                        TextContentObject = new List<string>(); //создать новый лист для нового объекта
                        TextContentObject.Clear();
                        CreateObjPro = false;

                        //MessageBox.Show("1");
                        //получить объект по структуре TXF_Object_Struct
                    }

                    if ((s.Substring(0, 4) == ".OBJ") & (CreateObjPro == false))
                    {
                        CreateObjPro = true;
                        TextContentObject = new List<string>(); //создать новый лист для нового объетка
 
                    }
                }


                TextContentObject.Add(s);  //добавление строчки в лист





            }
            //




        }
        */
    }



}
