using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
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
        Zichtrekening zichtrekening = new Zichtrekening();
        List<Zichtrekening> zichtSaldos = new List<Zichtrekening>();

        Spaarrekening spaarrekening = new Spaarrekening();
        List<Zichtrekening> spaarSaldos = new List<Zichtrekening>();

        public MainWindow()
        {
            InitializeComponent();
            zichtrekening.Saldo = readJson();
            lblSaldoZicht.Content = zichtrekening.VisualSaldo();
            spaarrekening.Saldo = 0;
            lblSaldoSpaar.Content = spaarrekening.VisualSaldo();
        }

        private void btnStortenAfhalen_Click(object sender, RoutedEventArgs e)
        {
            Button? zender = sender as Button;
            string[] transactie = zender.Name.Split("_");
            if (transactie[2] == "Zicht")
            {
                if (transactie[1] == "Storten")
                {
                    zichtrekening.Saldo += Convert.ToDouble(tbxZicht.Text);
                    lblSaldoZicht.Content = zichtrekening.VisualSaldo();
                }
                else
                {
                    zichtrekening.Saldo -= Convert.ToDouble(tbxZicht.Text);
                    lblSaldoZicht.Content = zichtrekening.VisualSaldo();
                }                
            }
            else
            {
                if (transactie[1] == "Storten")
                {
                    spaarrekening.Saldo += Convert.ToDouble(tbxSpaar.Text);
                    lblSaldoSpaar.Content = spaarrekening.VisualSaldo();
                }
                else
                {
                    spaarrekening.Saldo -= Convert.ToDouble(tbxSpaar.Text);
                    lblSaldoSpaar.Content = spaarrekening.VisualSaldo();
                }
            }
        }

        private double readJson()
        {
            using (StreamReader r = new StreamReader("Saldos.json"))
            {
                string json = r.ReadToEnd();
                zichtSaldos = JsonSerializer.Deserialize<List<Zichtrekening>>(json);
            }
            return zichtSaldos.Last().Saldo;
        }
    }
}
