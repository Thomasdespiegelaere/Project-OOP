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
        public static bool ToonZichtGrafiek { get; set; }    
        public static bool ToonSpaarGrafiek { get; set; }    
        public abstract double Saldo { get; set; }

        public abstract string VisualSaldo();

        public abstract void WriteJson(string _jsonfile);

        public abstract double readJson(string _jsonZicht);
        public abstract void UpdateGrafiek(Canvas cnvs_grafiek);

        public static void Assen(Canvas cnvs_grafiek, double hoogteinterval)
        {
            Line Y = new Line();
            Y.X1 = 0;
            Y.Y1 = 0;
            Y.X2 = 0;
            Y.Y2 = cnvs_grafiek.Height;
            Y.Stroke = new SolidColorBrush(Colors.Blue);
            cnvs_grafiek.Children.Add(Y);

            Line X = new Line();
            X.X1 = 0;
            X.Y1 = 0;
            X.X2 = cnvs_grafiek.Width;
            X.Y2 = 0;
            X.Stroke = new SolidColorBrush(Colors.Blue);
            cnvs_grafiek.Children.Add(X);

            for (double i = 4; i >= 1; i--)
            {
                double y = (cnvs_grafiek.Height * i) / 4;
                string value = Math.Round(((cnvs_grafiek.Height / hoogteinterval) * (i / (double)4)), 0).ToString();

                TextBlock textBlock1 = new TextBlock();
                textBlock1.Text = value;
                textBlock1.Foreground = new SolidColorBrush(Colors.Black);
                textBlock1.Margin = new Thickness(0, y, 0, 0);
                textBlock1.RenderTransform = new ScaleTransform(1, -1);
                cnvs_grafiek.Children.Add(textBlock1);
            }
        }

    }
}
