using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace convert1
{



    //1
    class Group_1_2_List_Struct  //класс для хранения структуры [Область][категория]
    {
        List<Group1_and_Group2_Struct> Group1List;



        public Group_1_2_List_Struct()
        {
            Group1List = new List<Group1_and_Group2_Struct>();
        }


        public bool Contain_Group1(string txt)  // метод, есть ли область в списке областей?
        {
            
            bool rez = false;

            foreach (Group1_and_Group2_Struct R in Group1List)
            {
                if (R.Group1Name == txt)
                {
                    rez = true;
                    return rez;
                }
            }
            return rez;

        }

        public bool Contain_Group1_and_Group2(string txt_gr1, string txt_gr2)  //
        {
            bool rez=false;

            if (!Contain_Group1(txt_gr1))
            {
                rez = false;
                return rez;
            }

            Group1_and_Group2_Struct temp_gr1 = GetGroup1(txt_gr1);

            foreach (string s in temp_gr1.Group2List)
            {
                if (s == txt_gr2)
                {
                    rez = true;
                    break;
                }
            }

            return rez;
        }


        public void AddOblast_and_Category(string txt_obl, string txt_cat)
        {
            Group1_and_Group2_Struct temp_ob = GetGroup1(txt_obl);
            temp_ob.Group2List.Add(txt_cat);

        }

        public void AddGroup1(string txt)
        {
            if (!Contain_Group1(txt))
            {
                Group1_and_Group2_Struct r = new Group1_and_Group2_Struct();
                r.Group1Name = txt;
                Group1List.Add(r);
            }

        }



        public Group1_and_Group2_Struct GetGroup1(string s)  //метод, возращает объект Область с заданым именем области (и категориями в нем)
        {
            Group1_and_Group2_Struct rez = new Group1_and_Group2_Struct();
            foreach (Group1_and_Group2_Struct r in Group1List)
            {
                if (r.Group1Name == s)
                {

                    return r;
                }
            }
            return rez;
        }
    }



    ///////////////////////////////////////////// Administrative_division
    //2
    class Group1_and_Group2_Struct
    {
        public string Group1Name;
        public List<string> Group2List;

        public Group1_and_Group2_Struct()
        {
            Group2List = new List<string>();
        }

        public bool Contain_Group2(string txt) //метод, есть ли категория в данном районе (области?)
        {
            bool rez = false;
            foreach (string  s in Group2List)
            {
                //GlobalVar.text_for_tb3 += s.RaionName + " - " + txt + Environment.NewLine;

                if (s == txt)
                {
                    rez = true;
                    return rez;
                }
            }
            return rez;
        }

        public void AddGroup2(string s)
        {
            if (!Contain_Group2(s))
            {
 
                Group2List.Add(s);
            }
        }


    }

   

    /////////////////////////////////////////////
    //3
  



}
