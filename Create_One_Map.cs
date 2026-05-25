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

namespace convert1
{
    class Create_One_Map
    {
        public static void Create_One_Map1()
        {
            string path = KML_object_class.mypath_kml;


            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(path);

            XmlNode xRoot = xDoc.DocumentElement;
            XmlNodeList childnodes = xRoot.SelectNodes("Placemark"); // 
            //MessageBox.Show(childnodes.Count.ToString());


        }
    }
}
