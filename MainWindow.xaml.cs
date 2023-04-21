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

        //string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        /*string _jsonfile = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName)
             + "\\JsonFiles\\Saldos.json";*/
        string _jsonZicht = @"C:\Users\tdesp\OneDrive\Documenten\Vives Fase 1\OOP\Project OOP\JsonFiles\ZichtSaldos.json";
        string _jsonSpaar = @"C:\Users\tdesp\OneDrive\Documenten\Vives Fase 1\OOP\Project OOP\JsonFiles\SpaarSaldos.json";

        public MainWindow()
        {
            InitializeComponent();
            zichtrekening.Saldo = zichtrekening.readJson(_jsonZicht);
            lblSaldoZicht.Content = zichtrekening.VisualSaldo();
            spaarrekening.Saldo = spaarrekening.readJson(_jsonSpaar);
            lblSaldoSpaar.Content = spaarrekening.VisualSaldo();

            zichtrekening.UpdateGrafiek(cnvs_grafiek);
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
                    zichtrekening.Saldo += Convert.ToDouble(tbxZicht.Text.Replace('.', ','));
                    zichtrekening.WriteJson(_jsonZicht);
                    lblSaldoZicht.Content = zichtrekening.VisualSaldo();
                    zichtrekening.UpdateGrafiek(cnvs_grafiek);
                }
                else
                {
                    zichtrekening.Saldo -= Convert.ToDouble(tbxZicht.Text.Replace('.', ','));
                    zichtrekening.WriteJson(_jsonZicht);
                    lblSaldoZicht.Content = zichtrekening.VisualSaldo();
                    zichtrekening.UpdateGrafiek(cnvs_grafiek);
                }                
            }
            else
            {
                if (transactie[1] == "Storten")
                {
                    spaarrekening.Saldo += Convert.ToDouble(tbxSpaar.Text.Replace('.', ','));
                    spaarrekening.WriteJson(_jsonSpaar);
                    lblSaldoSpaar.Content = spaarrekening.VisualSaldo();
                    spaarrekening.UpdateGrafiek(cnvs_grafiek);
                }
                else
                {
                    spaarrekening.Saldo -= Convert.ToDouble(tbxSpaar.Text.Replace('.', ','));
                    spaarrekening.WriteJson(_jsonSpaar);
                    lblSaldoSpaar.Content = spaarrekening.VisualSaldo();
                    spaarrekening.UpdateGrafiek(cnvs_grafiek);
                }
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbx_rekeningen.SelectedIndex == 0)
            {
                Rekening.ToonZichtGrafiek = true;
                Rekening.ToonSpaarGrafiek = false;
                zichtrekening.UpdateGrafiek(cnvs_grafiek);
            }
            else
            {
                Rekening.ToonZichtGrafiek = false;
                Rekening.ToonSpaarGrafiek = true;
                spaarrekening.UpdateGrafiek(cnvs_grafiek);
            }
        }
    }
}
