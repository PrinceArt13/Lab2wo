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

        private ObservableCollection<string> numHistory = new ObservableCollection<string>();
        public ReadOnlyObservableCollection<string> NumHistory { get; }
        public AppViewModel()
        {
            NumHistory = new ReadOnlyObservableCollection<string>(numHistory);
        }

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
        public int UserMoveCounter
        {
            get => usermovecounter;
            set
            {
                usermovecounter = value;
                OnPropertyChanged("UserMoveContent");
            }
        }
        public string AmountContent => Convert.ToString(amountofbuttons);

        public string UserMoveContent => Convert.ToString(usermovecounter);

        public void NumbersButtonsCreate(int size)
        {
            AmountOfButtons = 0;
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
                    Numbers.Clear();
                    int count;
                    if (int.TryParse(obj.ToString(), out count))
                        NumbersButtonsCreate(count);
                    else MessageBox.Show("неверное значение, перезапустите программу");
                }
                    ));
            }
        }

        private Command pickCell;
        public Command PickCell
        {
            get
            {
                return pickCell ?? (pickCell = new Command(obj =>
                {
                    int mama = int.Parse(obj.ToString());
                    Numbers[(mama-1)].DeleteThisCell();
                    numHistory.Add((mama).ToString());
                }));
            }
        }

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
