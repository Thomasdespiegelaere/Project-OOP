using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Vives;

namespace Project_OOP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        zichtrekening zichtrekening;
        spaarrekening spaarrekening;

        public MainWindow()
        {
            InitializeComponent();
            zichtrekening = new zichtrekening();
            zichtrekening.Saldo = 0;
            spaarrekening = new spaarrekening();
            spaarrekening.Saldo = 0;
        }

        private void btnStortenAfhalen_Click(object sender, RoutedEventArgs e)
        {
            Button zender = sender as Button;
            string[] transactie = zender.Name.Split("_");
            if (transactie[2] == "Zicht")
            {
                if (transactie[1] == "Storten")
                {
                    zichtrekening.Saldo += Convert.ToDouble(tbxZicht.Text);
                    lblSaldoZicht.Content = zichtrekening.Saldo;
                }
                else
                {
                    zichtrekening.Saldo -= Convert.ToDouble(tbxZicht.Text);
                    lblSaldoZicht.Content = zichtrekening.Saldo;
                }                
            }
            else
            {
                if (transactie[1] == "Storten")
                {
                    spaarrekening.Saldo += Convert.ToDouble(tbxSpaar.Text);
                    lblSaldoSpaar.Content = spaarrekening.Saldo;
                }
                else
                {
                    spaarrekening.Saldo -= Convert.ToDouble(tbxSpaar.Text);
                    lblSaldoSpaar.Content = spaarrekening.Saldo;
                }
            }
        }
    }
}
