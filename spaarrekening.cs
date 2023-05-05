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
using System.Windows.Threading;
using System.Windows.Documents;

namespace Vives
{
    internal class Spaarrekening:Rekening
    {
        public override double Saldo { get; set; }

        public List<Spaarrekening> Saldos = new List<Spaarrekening>();

        public override string VisualSaldo()
        {
            return $"{Math.Round(Saldo, 2)} \u20AC";
        }
        public override void WriteJson(string _jsonfile)
        {
            if (Saldo != Saldos.Last().Saldo)
                Saldos.Add(new Spaarrekening { Saldo = Math.Round(Saldo, 2) });

            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(Saldos, options);
            using (StreamWriter sw = new StreamWriter(_jsonfile))
            {
                sw.Write(json);
                sw.Close();
            }
        }
        public override double readJson(string _jsonSpaar)
        {
            using (StreamReader r = new StreamReader(_jsonSpaar))
            {
                string json = r.ReadToEnd();
                Saldos = JsonSerializer.Deserialize<List<Spaarrekening>>(json);
                r.Close();
            }
            return Saldos.Last().Saldo;
        }

        public void RenteSpaarRekening(string _jsonfile)
        {
            Saldo *= 1.01;
            WriteJson(_jsonfile);            
        }

        public void UpdateTransacties(Spaarrekening spaarrekening, Geschiedenis geschiedenis)
        {
            double previous = -1;
            for (int i = spaarrekening.Saldos.Count - 1; i >= 0; i--)
            {
                double x = spaarrekening.Saldos.ElementAt(i).Saldo;
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
                        geschiedenis.tbx_spaar.Document.Blocks.Add(paragraph);
                    }
                    else
                    {
                        Paragraph paragraph = new Paragraph();
                        Run run = new Run(Math.Round(transactie, 2).ToString());
                        run.Foreground = Brushes.Red;
                        paragraph.Inlines.Add(run);
                        geschiedenis.tbx_spaar.Document.Blocks.Add(paragraph);
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
