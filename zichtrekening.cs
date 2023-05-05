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
using System.Windows.Documents;

namespace Vives
{
    internal class Zichtrekening:Rekening
    {
        public override double Saldo { get; set; }

        public List<Zichtrekening> Saldos = new List<Zichtrekening>();

        public override string VisualSaldo()
        {
            return $"{Math.Round(Saldo, 2)} \u20AC";
        }
        public override void WriteJson(string _jsonfile)
        {
            if (Saldo != Saldos.Last().Saldo)
                Saldos.Add(new Zichtrekening { Saldo = Math.Round(Saldo, 2) });

            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(Saldos, options);
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
                Saldos = JsonSerializer.Deserialize<List<Zichtrekening>>(json);
                r.Close();
            }
            return Saldos.Last().Saldo;
        }

        public void UpdateTransacties(Zichtrekening zichtrekening, Geschiedenis geschiedenis)
        {
            double previous = -1;
            for (int i = zichtrekening.Saldos.Count - 1; i >= 0; i--)
            {
                double x = zichtrekening.Saldos.ElementAt(i).Saldo;
                if (previous > 0)
                {
                    double transactie = previous - x;
                    if (transactie > 0)
                    {
                        string text = "+" + Math.Round(transactie, 2).ToString();
                        Paragraph paragraph = new Paragraph();
                        Run run = new Run(text);
                        run.Foreground = Brushes.Green;
                        paragraph.Inlines.Add(run);
                        geschiedenis.tbx_zicht.Document.Blocks.Add(paragraph);
                    }
                    else
                    {
                        Paragraph paragraph = new Paragraph();
                        Run run = new Run(Math.Round(transactie, 2).ToString());
                        run.Foreground = Brushes.Red;
                        paragraph.Inlines.Add(run);
                        geschiedenis.tbx_zicht.Document.Blocks.Add(paragraph);
                    }
                    previous = x;
                }
                else
                {
                    previous = x;
                }
            }
        }
    }
}
