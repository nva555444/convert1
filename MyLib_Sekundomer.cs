using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace convert1
{
    class MyLib_Sekundomer
    {
        DateTime dt1, dt2;

        System.Timers.Timer MyTimer;

        SynchronizationContext _context;

        public TimeSpan ts, tsdx;
        public string ts_string;

        //public event Action EventChangeTime;

        Label label;



        public MyLib_Sekundomer(SynchronizationContext _context, Label label)
        {
            this._context = _context;
            this.label = label;

            ts = new TimeSpan(0, 0, 0, 0, 0);
            ts_string = ts.ToString(@"hh\:mm\:ss");

            MyTimer = new System.Timers.Timer(10);
            MyTimer.Elapsed += metod1_SekundomerTick;

        }


        public void Start()
        {
            if (MyTimer.Enabled == false)
            {
                MyTimer.Start();
                dt1 = DateTime.Now;
            }
            else
            {
                tsdx = ts;  //запомнить значение таймера на время паузы
                MyTimer.Stop();
            }

        }


        public void Stop()
        {
            MyTimer.Stop();

            ts = new TimeSpan(0, 0, 0, 0, 0);
            tsdx = new TimeSpan(0, 0, 0, 0, 0);
            dt1 = DateTime.Now;
            dt2 = DateTime.Now;

            ts_string = ts.ToString(@"hh\:mm\:ss");
            obertEvent(null);
        }

        public void Pause()
        {
            tsdx = ts;  //запомнить значение таймера на время паузы
            MyTimer.Stop();
        }

        public void Reset()
        {
            ts = new TimeSpan(0, 0, 0, 0, 0);
            tsdx = new TimeSpan(0, 0, 0, 0, 0);
            dt1 = DateTime.Now;
            dt2 = DateTime.Now;

            ts_string = ts.ToString(@"hh\:mm\:ss");

            obertEvent(null);  //обновление текстбокса в соответствии с текущим значением времени
        }

        private void metod1_SekundomerTick(Object source, System.Timers.ElapsedEventArgs e)
        {
            dt2 = DateTime.Now;
            ts = dt2 - dt1 + tsdx;

            //ts = ts.Add(new TimeSpan(0, 0, 0, 0, 10));

            ts_string = ts.ToString(@"hh\:mm\:ss");

            _context.Send(obertEvent, null);
            //EventChangeTime();  // из за этой фигни иногда вылетала ошибка
        }

        public void EndWork()
        {
            MyTimer.Elapsed -= metod1_SekundomerTick;
            MyTimer.Stop();
            MyTimer.Dispose();

        }

        public void obertEvent(object n) //обновление текстбокса в соответствии с текущим значением времени
        {
            label.Text = ts_string;
            
            //EventChangeTime();
        }



    }
}
