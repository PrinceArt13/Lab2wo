using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    public class Numbers : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private bool ispush;
        private bool TrueFalse;
        private string color;
        private string buttoncontent;
        private int usermove = 0;
        public int X { get; set; }

        public bool Truefalse
        {
            get
            {
                return this.TrueFalse;
            }
            set
            {
                this.TrueFalse = value;
                OnPropertyChanged("TrueFalse");
                OnPropertyChanged("Color");
            }
        }

        public bool IsPush
        {
            get => ispush;
            set
            {
                ispush = value;
                usermove++;
                OnPropertyChanged("IsPush");
                OnPropertyChanged("Color");
                OnPropertyChanged("ButtonContent");
            }
        }

        public string Color
        {
            get => this.TrueFalse ? "Black" : "Red";
            set => color = value;
        }
        public string ButtonContent
        {
            get
            {
                return this.TrueFalse == true && this.ispush == false ? (buttoncontent = "УГАДАНО") : X.ToString();
            }
            set { buttoncontent = value; OnPropertyChanged("ButtonContent"); }
        }

        public void DeleteThisCell()
        {
            this.IsPush = false;
        }

        public Numbers(bool TrueFalse, bool ispush, int x)
        {
            this.TrueFalse = TrueFalse;
            this.ispush = ispush;
            this.X = x;
        }

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
