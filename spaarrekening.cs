using Project_OOP;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Vives
{
    internal class Spaarrekening:Rekening
    {
        public override double Saldo { get; set; }
        public List<Spaarrekening> spaarSaldos = new List<Spaarrekening>();

        public override string VisualSaldo()
        {
            return $"{Math.Round(Saldo, 2)} \u20AC";
        }
        public override void WriteJson(string _jsonfile)
        {
            spaarSaldos.Add(new Spaarrekening { Saldo = Saldo });

            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(spaarSaldos, options);
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
                spaarSaldos = JsonSerializer.Deserialize<List<Spaarrekening>>(json);
                r.Close();
            }
            return spaarSaldos.Last().Saldo;
        }
    }
}
