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


public enum EnumType_Map { po_kadastru, po_kategoriyam }

namespace convert1
{
     //public Enum wer {asd, qwe};

    class GlobalVar
    {
        //public static string text_for_tb2;
        public static string text_for_tb3;
        public static string text_for_tb4;
        public static string text_for_tb5;
        public static string text_for_tb6;

   
        public static string group;

        public static string group_str1;
        public static string group_str2;


        /// <summary>
        /// тру - цветовая классификация по коду классификатора, фалс - по семантике
        /// </summary>
        public static bool Colour_by_kod;
      


        //public static string LOG;


    }
}
