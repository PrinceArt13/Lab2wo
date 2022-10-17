using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Lab2
{
    public class AppViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<Numbers> Numbers { get; set; } = new ObservableCollection<Numbers>();

        Random rnd = new Random();
        private int usermovecounter = 0;
        private int amountofbuttons = 0;
        public int AmountOfButtons
        {
            get => amountofbuttons;
            set
            {
                amountofbuttons = value;
                OnPropertyChanged("AmountContent");
            }
        }
        public string AmountContent => Convert.ToString(amountofbuttons);

        public string Content => Convert.ToString(usermovecounter);

        public void NumbersButtonsCreate(int size)
        {
            bool isTruebutton;
            int randomhere;
            for (int x = 0; x < size; x++)
            {
                randomhere = rnd.Next(0, 20);
                if (randomhere > 15) { isTruebutton = true; AmountOfButtons++; }
                else isTruebutton = false;
                Numbers.Add(new Numbers(isTruebutton, true, x + 1));
            }
        }

        private Command startGame;
        public Command StartGame
        {
            get
            {
                return startGame ?? (startGame = new Command(obj =>
                {
                    int count;
                    if (int.TryParse(obj.ToString(), out count))
                        NumbersButtonsCreate(count);
                    else MessageBox.Show("неверное значение, перезапустите программу");
                }
                    ));
            }
        }

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
