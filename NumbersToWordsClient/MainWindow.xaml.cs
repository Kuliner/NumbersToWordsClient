using System.ComponentModel;
using System.Windows;

namespace NumbersToWordsClient
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private string moneyText;

        public string MoneyInput { get; set; }

        public string MoneyText
        {
            get => moneyText; set
            {
                moneyText = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MoneyText)));
            }
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(MoneyInput))
            {
                MoneyText = "There's no input provided.";
                return;
            }

            var parseResult = decimal.TryParse(MoneyInput, out decimal money);

            if (!parseResult)
            {
                MoneyText = "Input provided is faulty.";
                return;
            }

            using (var service = new CurrencyConversionService.CurrencyConversionServiceClient())
            {
                try
                {
                    MoneyText = service.ConvertCurrencyToText(money);
                }
                catch (System.Exception)
                {
                    MoneyText = "There's some issue with webservice, most likely it's not up.";
                }
            }
        }
    }
}
