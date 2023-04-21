using Project_OOP;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;

namespace Vives
{
    internal class Zichtrekening:Rekening
    {
        public override double Saldo { get; set; }

        public List<Zichtrekening> zichtSaldos = new List<Zichtrekening>();

        public override string VisualSaldo()
        {
            return $"{Math.Round(Saldo, 2)} \u20AC";
        }
        public override void WriteJson(string _jsonfile)
        {
            zichtSaldos.Add(new Zichtrekening { Saldo = Math.Round(Saldo, 2) });

            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(zichtSaldos, options);
            using (StreamWriter sw = new StreamWriter(_jsonfile))
            {
                sw.Write(json);
                sw.Close();
            }
        }
        public override double readJson(string _jsonZicht)
        {
            using (StreamReader r = new StreamReader(_jsonZicht))
            {
                string json = r.ReadToEnd();
                zichtSaldos = JsonSerializer.Deserialize<List<Zichtrekening>>(json);
                r.Close();
            }
            return zichtSaldos.Last().Saldo;
        }

        public override void UpdateGrafiek(Canvas cnvs_grafiek)
        {
            if (Rekening.ToonZichtGrafiek == true)
            {
                double grootsteSaldo = 0;

                cnvs_grafiek.Children.Clear();
                double breedteinterval = cnvs_grafiek.Width / zichtSaldos.Count;

                for (int i = 0; i < zichtSaldos.Count; i++)
                {
                    if (zichtSaldos.ElementAt(i).Saldo > grootsteSaldo)
                    {
                        grootsteSaldo = zichtSaldos.ElementAt(i).Saldo;
                    }
                }
                double hoogteinterval = cnvs_grafiek.Height / grootsteSaldo;

                for (int i = 0; i < zichtSaldos.Count; i++)
                {
                    Line lijn = new Line();
                    lijn.X1 = breedteinterval * i;
                    if (i > 0)
                    {
                        lijn.Y1 = zichtSaldos.ElementAt(i - 1).Saldo * hoogteinterval;
                    }
                    else
                    {
                        lijn.Y1 = zichtSaldos.ElementAt(i).Saldo * hoogteinterval;
                    }
                    lijn.X2 = lijn.X1 + breedteinterval;
                    lijn.Y2 = zichtSaldos.ElementAt(i).Saldo * hoogteinterval;
                    lijn.Stroke = new SolidColorBrush(Colors.Blue);

                    /*TextBlock textBlock = new TextBlock();                         later gedetaleerde getallen toevoegen
                    textBlock.Text = zichtSaldos.ElementAt(i).Saldo.ToString();
                    textBlock.Foreground = new SolidColorBrush(Colors.Black);
                    textBlock.Margin = new Thickness(lijn.X2, lijn.Y2, 0, 0);
                    textBlock.RenderTransform = new ScaleTransform(1, -1);
                    cnvs_grafiek.Children.Add(textBlock);*/

                    TextBlock textBlock = new TextBlock();                         
                    textBlock.Text = i.ToString();
                    textBlock.Foreground = new SolidColorBrush(Colors.Black);
                    textBlock.Margin = new Thickness(lijn.X2, 0, 0, 0);
                    textBlock.RenderTransform = new ScaleTransform(1, -1);
                    cnvs_grafiek.Children.Add(textBlock);

                    cnvs_grafiek.Children.Add(lijn);
                }

                Rekening.Assen(cnvs_grafiek, hoogteinterval);                
            }            
        }
    }
}
