using Project_OOP;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Vives
{
    abstract class Rekening
    { 
        public abstract double Saldo { get; set; }

        public abstract string VisualSaldo();

        public abstract void WriteJson(string _jsonfile);

        public abstract double readJson(string _jsonZicht);
    }
}
