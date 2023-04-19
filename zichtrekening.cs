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
            zichtSaldos.Add(new Zichtrekening { Saldo = Saldo });

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
    }
}
