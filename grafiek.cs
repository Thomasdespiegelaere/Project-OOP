using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;
using Microsoft.VisualBasic;

namespace Vives
{
    internal class Grafiek
    {
        public static bool ToonZichtGrafiek { get; set; }
        public static bool ToonSpaarGrafiek { get; set; }
        public static bool gedetaileerdenummers { get; set; }
        public static Brush? kleur { get; set; }

        public static void UpdateGrafiek(Canvas cnvs_grafiek, Zichtrekening sichtrekening, Spaarrekening spaarrekening)
        {
            dynamic rekening;
            if (ToonZichtGrafiek == true)
            {
                rekening = sichtrekening;
            }
            else
            {
                rekening = spaarrekening;
            }
            double grootsteSaldo = 0;

            cnvs_grafiek.Children.Clear();
            double breedteinterval = cnvs_grafiek.Width / rekening.Saldos.Count;

            for (int i = 0; i < rekening.Saldos.Count; i++)
            {
                if (rekening.Saldos[i].Saldo > grootsteSaldo)
                {
                    grootsteSaldo = rekening.Saldos[i].Saldo;
                }
            }
            double hoogteinterval = cnvs_grafiek.Height / grootsteSaldo;

            for (int i = 0; i < rekening.Saldos.Count; i++)
            {
                Line lijn = new Line();
                lijn.X1 = breedteinterval * i;
                if (i > 0)
                {
                    lijn.Y1 = rekening.Saldos[i - 1].Saldo * hoogteinterval;
                }
                else
                {
                    lijn.Y1 = rekening.Saldos[i].Saldo * hoogteinterval;
                }
                lijn.X2 = lijn.X1 + breedteinterval;
                lijn.Y2 = rekening.Saldos[i].Saldo * hoogteinterval;
                lijn.Stroke = kleur;

                if (gedetaileerdenummers == true)
                {
                    TextBlock textBlock = new TextBlock();
                    textBlock.FontSize = 10;
                    textBlock.Text = rekening.Saldos[i].Saldo.ToString();
                    textBlock.Foreground = new SolidColorBrush(Colors.Black);
                    textBlock.Margin = new Thickness(lijn.X2, lijn.Y2 + 20, 0, 0);
                    textBlock.RenderTransform = new ScaleTransform(1, -1);
                    cnvs_grafiek.Children.Add(textBlock);

                    Ellipse ellipse = new Ellipse();
                    ellipse.Margin = new Thickness(lijn.X2 - 2, lijn.Y2 - 2, 0, 0);
                    ellipse.Height = 5;
                    ellipse.Width = 5;
                    ellipse.StrokeThickness = 1;
                    ellipse.Fill = kleur;
                    cnvs_grafiek.Children.Add(ellipse);
                }

                TextBlock X_waarde = new TextBlock();
                X_waarde.Text = i.ToString();
                X_waarde.Foreground = new SolidColorBrush(Colors.Black);
                X_waarde.Margin = new Thickness(lijn.X2 - 2, 0, 0, 0);
                X_waarde.RenderTransform = new ScaleTransform(1, -1);
                cnvs_grafiek.Children.Add(X_waarde);

                cnvs_grafiek.Children.Add(lijn);
            }
            Assen(cnvs_grafiek, hoogteinterval);                       
        }

        public static void Assen(Canvas cnvs_grafiek, double hoogteinterval)
        {
            Line Y = new Line();
            Y.X1 = 0;
            Y.Y1 = 0;
            Y.X2 = 0;
            Y.Y2 = cnvs_grafiek.Height;
            Y.Stroke = kleur;
            cnvs_grafiek.Children.Add(Y);

            Line X = new Line();
            X.X1 = 0;
            X.Y1 = 0;
            X.X2 = cnvs_grafiek.Width;
            X.Y2 = 0;
            X.Stroke = kleur;
            cnvs_grafiek.Children.Add(X);

            for (double i = 4; i >= 1; i--)
            {
                double y = (cnvs_grafiek.Height * i) / 4;
                string value = Math.Round(((cnvs_grafiek.Height / hoogteinterval) * (i / (double)4)), 0).ToString();

                TextBlock Y_waarde = new TextBlock();
                Y_waarde.Text = value + "€";
                Y_waarde.Foreground = new SolidColorBrush(Colors.Black);
                Y_waarde.Margin = new Thickness(0, y, 0, 0);
                Y_waarde.RenderTransform = new ScaleTransform(1, -1);
                cnvs_grafiek.Children.Add(Y_waarde);
            }
        }
    }
}
