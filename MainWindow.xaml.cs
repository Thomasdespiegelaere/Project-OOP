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

        string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
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

            double breedteinterval = cnvs_grafiek.Width / zichtrekening.zichtSaldos.Count;
            for (int i = 0; i < zichtrekening.zichtSaldos.Count; i++)
            {
                Line lijn = new Line();
                lijn.X1 = breedteinterval * i;
                if (i > 0)
                {
                    lijn.Y1 = zichtrekening.zichtSaldos.ElementAt(i - 1).Saldo;
                }
                else
                {
                    lijn.Y1 = zichtrekening.zichtSaldos.ElementAt(i).Saldo;
                }
                lijn.X2 = lijn.X1 + breedteinterval;
                lijn.Y2 = zichtrekening.zichtSaldos.ElementAt(i).Saldo;
                lijn.Stroke = new SolidColorBrush(Colors.Blue);

                TextBlock textBlock = new TextBlock();
                textBlock.Text = zichtrekening.zichtSaldos.ElementAt(i).Saldo.ToString();
                textBlock.Foreground = new SolidColorBrush(Colors.Black);
                textBlock.Margin = new Thickness(lijn.X2, lijn.Y2, 0, 0);
                textBlock.RenderTransform = new ScaleTransform(1, -1);
                cnvs_grafiek.Children.Add(textBlock);

                cnvs_grafiek.Children.Add(lijn);
            }            
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
                    zichtrekening.WriteJson(_jsonZicht);
                    lblSaldoZicht.Content = zichtrekening.VisualSaldo();
                }
                else
                {
                    zichtrekening.Saldo -= Convert.ToDouble(tbxZicht.Text);
                    zichtrekening.WriteJson(_jsonZicht);
                    lblSaldoZicht.Content = zichtrekening.VisualSaldo();
                }                
            }
            else
            {
                if (transactie[1] == "Storten")
                {
                    spaarrekening.Saldo += Convert.ToDouble(tbxSpaar.Text);
                    spaarrekening.WriteJson(_jsonSpaar);
                    lblSaldoSpaar.Content = spaarrekening.VisualSaldo();
                }
                else
                {
                    spaarrekening.Saldo -= Convert.ToDouble(tbxSpaar.Text);
                    spaarrekening.WriteJson(_jsonSpaar);
                    lblSaldoSpaar.Content = spaarrekening.VisualSaldo();
                }
            }
        }
    }
}
