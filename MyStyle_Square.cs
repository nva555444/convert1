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
    //[Serializable]
    public class MyStyle_Square
    {

        public string ColorLine;
        public string ColorPoly;
        public string WidthLine;
        




        public MyStyle_Square(string ColorLine, string ColorPoly, string WidthLine = "2")
        {
            this.ColorLine = ColorLine;
            this.ColorPoly = ColorPoly;
            this.WidthLine = WidthLine;

        }


    }



}
