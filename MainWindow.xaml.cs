using System;
using System.Collections.Generic;
using System.Drawing;
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
using static System.Net.Mime.MediaTypeNames;

namespace Project_OOP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Zichtrekening zichtrekening = new Zichtrekening();

        Spaarrekening spaarrekening = new Spaarrekening();
        
        string _jsonZicht = "";
        string _jsonSpaar = "";

        public MainWindow()
        {
            InitializeComponent();

            string directoryName = Environment.CurrentDirectory;

            for (int i = 0; i < 3; i++)
            {
                directoryName = System.IO.Path.GetDirectoryName(directoryName);
            }
            _jsonZicht = directoryName + @"\JsonFiles\ZichtSaldos.json";
            _jsonSpaar = directoryName + @"\JsonFiles\SpaarSaldos.json";

            zichtrekening.Saldo = zichtrekening.readJson(_jsonZicht);
            lblSaldoZicht.Content = zichtrekening.VisualSaldo();
            spaarrekening.Saldo = spaarrekening.readJson(_jsonSpaar);
            lblSaldoSpaar.Content = spaarrekening.VisualSaldo();

            Grafiek.UpdateGrafiek(cnvs_grafiek, zichtrekening, spaarrekening);
            cbx_rekeningen.SelectedIndex = 0;
        }

        private void btnStortenAfhalen_Click(object sender, RoutedEventArgs e)
        {
            Button? zender = sender as Button;
            string[] transactie = zender.Name.Split("_");
            if (transactie[2] == "Zicht")
            {
                if (transactie[1] == "Storten")
                {
                    zichtrekening.Saldo += checkinput(tbxZicht.Text);
                    zichtrekening.WriteJson(_jsonZicht);
                    lblSaldoZicht.Content = zichtrekening.VisualSaldo();
                    Grafiek.UpdateGrafiek(cnvs_grafiek, zichtrekening, spaarrekening);
                }
                else
                {
                    zichtrekening.Saldo -= checkinput(tbxZicht.Text);
                    zichtrekening.WriteJson(_jsonZicht);
                    lblSaldoZicht.Content = zichtrekening.VisualSaldo();
                    Grafiek.UpdateGrafiek(cnvs_grafiek, zichtrekening, spaarrekening);
                }                
            }
            else
            {
                if (transactie[1] == "Storten")
                {
                    spaarrekening.Saldo += checkinput(tbxSpaar.Text);
                    spaarrekening.WriteJson(_jsonSpaar);
                    lblSaldoSpaar.Content = spaarrekening.VisualSaldo();
                    Grafiek.UpdateGrafiek(cnvs_grafiek, zichtrekening, spaarrekening); ;
                }
                else
                {
                    spaarrekening.Saldo -= checkinput(tbxSpaar.Text);
                    spaarrekening.WriteJson(_jsonSpaar);
                    lblSaldoSpaar.Content = spaarrekening.VisualSaldo();
                    Grafiek.UpdateGrafiek(cnvs_grafiek, zichtrekening, spaarrekening);
                }
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbx_rekeningen.SelectedIndex == 0)
            {
                Grafiek.ToonZichtGrafiek = true;
                Grafiek.ToonSpaarGrafiek = false;
                Grafiek.UpdateGrafiek(cnvs_grafiek, zichtrekening, spaarrekening);
            }
            else
            {
                Grafiek.ToonZichtGrafiek = false;
                Grafiek.ToonSpaarGrafiek = true;
                Grafiek.UpdateGrafiek(cnvs_grafiek, zichtrekening, spaarrekening);
            }
        }

        public double checkinput(string input)
        {
            double saldo;

            try
            {
                saldo = Convert.ToDouble(input);
            }
            catch (FormatException)
            {
                saldo = Convert.ToDouble(input.Replace('.', ','));
            }

            if (saldo <= 0)
            {
                MessageBox.Show("Je moet een getal groter dan nul ingeven");
                return 0;
            }
            return saldo;
        }

        private void btn_detail_Click(object sender, RoutedEventArgs e)
        {
            Grafiek.gedetaileerdenummers = !Grafiek.gedetaileerdenummers;
            if (Grafiek.gedetaileerdenummers == true)
            {
                btn_detail.Icon = "X";
            }
            else
            {
                btn_detail.Icon = "";
            }
            Grafiek.UpdateGrafiek(cnvs_grafiek, zichtrekening, spaarrekening);
        }
    }
}
