using System.ComponentModel;
using System.Windows;

namespace Calculator_.src
{
    public partial class Shop : Window
    {
        public class ShopArgs : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler? PropertyChanged;
            public void OnPropertyChanged(string PropertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
            }
            private double firstoffset;
            public double FirstOffset
            {
                get
                {
                    return this.firstoffset;

                }
                set
                {
                    if (this.firstoffset != value)
                    {
                        this.firstoffset = value;
                        OnPropertyChanged("FirstOffset");
                    }
                }
            }
            private double secondoffset;
            public double SecondOffset
            {
                get
                {
                    return this.secondoffset;

                }
                set
                {
                    if (this.secondoffset != value)
                    {
                        this.secondoffset = value;
                        OnPropertyChanged("SecondOffset");
                    }
                }
            }
            private double thirdoffset;
            public double ThirdOffset
            {
                get
                {
                    return this.thirdoffset;

                }
                set
                {
                    if (this.thirdoffset != value)
                    {
                        this.thirdoffset = value;
                        OnPropertyChanged("ThirdOffset");
                    }
                }
            }
            private double fourthoffset;
            public double FourthOffset
            {
                get
                {
                    return this.fourthoffset;

                }
                set
                {
                    if (this.fourthoffset != value)
                    {
                        this.fourthoffset = value;
                        OnPropertyChanged("FourthOffset");
                    }
                }
            }
        }
        private readonly BackgroundWorker rotator = new();
        public static ShopArgs sa = new();
        private readonly User u;
        public Shop(User u)
        {
            this.u = u;
            InitializeComponent();
            
            this.DataContext = sa;
            sa.FirstOffset = 0.0;
            sa.SecondOffset = 0.25;
            sa.ThirdOffset = 0.75;
            sa.FourthOffset = 0.95;
            rotator.DoWork += RotateOffsets;
            rotator.RunWorkerAsync();
        }
        private void RotateOffsets(object? sender, DoWorkEventArgs e)
        {
            while (true) {
                
                if (sa.FirstOffset >= 1.0)
                {
                    sa.FirstOffset = 0;
                }
                if (sa.SecondOffset >= 1.0)
                {
                    sa.SecondOffset = 0;
                }
                if (sa.ThirdOffset >= 1.0)
                {
                    sa.ThirdOffset = 0;
                }
                if (sa.FourthOffset >= 1.0)
                {
                    sa.FourthOffset = 0;
                }
                sa.FirstOffset += 0.000001;
                sa.SecondOffset += 0.000001;
                sa.ThirdOffset += 0.000001;
                sa.FourthOffset += 0.000001;
            }
        }

        private void OperatorClick(object sender, RoutedEventArgs e)
        {

        }

        private void DigitClick(object sender, RoutedEventArgs e)
        {

        }
        private void BigBuyClick(object sender, RoutedEventArgs e)
        {

        }
        private void SaveSlotClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
