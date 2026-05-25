using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace convert1
{
    class Obl_Raion_List_Struct
    {
        
        List<Oblast_and_Raions> OblastList;



        public Obl_Raion_List_Struct()
        {
            OblastList = new List<Oblast_and_Raions>();
        }


        public bool Contain_Oblast(string txt)  // метод, есть ли область в списке областей?
        {
            
            bool rez = false;

            foreach (Oblast_and_Raions R in OblastList)
            {
                if (R.OblastName == txt)
                {
                    rez = true;
                    return rez;
                }
            }
            return rez;

        }

        public bool Contain_Oblast_and_Raion(string txt_obl, string txt_cat)  //
        {
            bool rez=false;

            if (!Contain_Oblast(txt_obl))
            {
                rez = false;
                return rez;
            }

            Oblast_and_Raions temp_ob = GetOblast(txt_obl);

            foreach (string s in temp_ob.RaionList)
            {
                if (s == txt_cat)
                {
                    rez = true;
                    break;
                }
            }

            return rez;
        }


        public void AddOblast_and_Raion(string txt_obl, string txt_cat)
        {
            Oblast_and_Raions temp_ob = GetOblast(txt_obl);
            temp_ob.RaionList.Add(txt_cat);

        }

        public void AddOblast(string txt)
        {
            if (!Contain_Oblast(txt))
            {
                Oblast_and_Raions r = new Oblast_and_Raions();
                r.OblastName = txt;
                OblastList.Add(r);
            }

        }



        public Oblast_and_Raions GetOblast(string s)  //метод, возращает объект Область с заданым именем области (и районами в нем)
        {
            Oblast_and_Raions rez = new Oblast_and_Raions();
            foreach (Oblast_and_Raions r in OblastList)
            {
                if (r.OblastName == s)
                {

                    return r;
                }
            }
            return rez;
        }


    }


/////////////////////////////

    class Oblast_and_Raions
    {
        public string OblastName;
        public List<string> RaionList;

        public Oblast_and_Raions()
        {
            RaionList = new List<string>();
        }

        public bool Contain_Raion(string txt) //метод, есть ли район в данном области
        {
            bool rez = false;
            foreach (string s in RaionList)
            {
               

                if (s == txt)
                {
                    rez = true;
                    return rez;
                }
            }
            return rez;
        }

        public void AddRaion(string s)
        {
            if (!Contain_Raion(s))
            {

                RaionList.Add(s);
            }
        }


    }
}
