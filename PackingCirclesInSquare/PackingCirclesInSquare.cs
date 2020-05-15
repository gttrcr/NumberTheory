using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PackingCirclesInSquare
{
    public partial class PackingCirclesInSquare : Form
    {
        private static readonly double scale = 1190;

        public PackingCirclesInSquare()
        {
            InitializeComponent();
            buttonStart.Click += (sender, e) =>
             {
                 List<PackProperties> packProperties = new List<PackProperties>();
                 LoadData(out packProperties);
                 Calculate(packProperties);
             };
        }

        private struct PointD
        {
            public double X;
            public double Y;

            public PointD(double x, double y)
            {
                X = x;
                Y = y;
            }

            public double Distance(PointD pointD)
            {
                return Math.Sqrt(Math.Pow(pointD.X - X, 2) + Math.Pow(pointD.Y - Y, 2));
            }
        }

        private struct PackProperties
        {
            public int NumberOfCircles;
            public double CircleRadius;
            public List<PointD> CirclesCenter;

            public PackProperties(int numberOfCircles, double circleRadius, List<PointD> circlesCenter)
            {
                NumberOfCircles = numberOfCircles;
                CircleRadius = circleRadius;
                CirclesCenter = circlesCenter;
            }
        }

        private void LoadData(out List<PackProperties> packProperties)
        {
            packProperties = new List<PackProperties>();
            string folder = "..\\..\\csq_coords\\";
            string[] radii = File.ReadAllLines(folder + "radii.txt");
            for (int i = 3100; i < radii.Length; i++)
            {
                string[] split = radii[i].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                int numberOfCircles = Convert.ToInt32(split[0]);
                double circleRadius = Convert.ToDouble(split[1].Replace('.', ','));
                string[] centers = File.ReadAllLines(folder + "csq" + split[0] + ".txt");
                if (centers.Length == numberOfCircles)
                {
                    List<PointD> circlesCenter = new List<PointD>();
                    for (int c = 0; c < centers.Length; c++)
                    {
                        string[] coord = centers[c].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                        double x = Convert.ToDouble(coord[1].Replace('.', ','));
                        double y = Convert.ToDouble(coord[2].Replace('.', ','));
                        circlesCenter.Add(new PointD(x, y));
                    }
                    packProperties.Add(new PackProperties(numberOfCircles, circleRadius, circlesCenter));
                }
                else
                    throw new Exception("File not ok error");
            }
        }

        private void Point(double x, double y, double height, double width, Graphics graphics)
        {
            Circle(x, y, 0.005, height, width, graphics);
        }

        private void Circle(double x, double y, double radius, double height, double width, Graphics graphics)
        {
            x *= scale / 2;
            y *= scale / 2;
            radius *= scale;
            RectangleF rectangle = new RectangleF((float)(width / 2) + (float)x - (float)(radius / 2), (float)(height / 2) - (float)y - (float)(radius / 2), (float)radius, (float)radius);
            graphics.DrawEllipse(Pens.Black, rectangle);
        }

        private void Calculate(List<PackProperties> packProperties)
        {
            Graphics graphics = pictureBox.CreateGraphics();
            int height = pictureBox.Height;
            int width = pictureBox.Width;

            Dictionary<int, int> exagonalPack = new Dictionary<int, int>();
            for (int i = 0; i < packProperties.Count; i++)
            {
                //Draw every circle
                //for (int c = 0; c < packProperties[i].CirclesCenter.Count; c++)
                //    Circle(packProperties[i].CirclesCenter[c].X, packProperties[i].CirclesCenter[c].Y, packProperties[i].CircleRadius, height, width, graphics);

                //Draw every reference points
                //for (int a = 0; a < packProperties[i].CirclesCenter.Count; a++)
                Parallel.For(0, packProperties[i].CirclesCenter.Count, a =>
                {
                    for (int n = 0; n < 6; n++)
                    {
                        PointD center = new PointD(packProperties[i].CirclesCenter[a].X + 2 * packProperties[i].CircleRadius * Math.Cos(Math.PI / 6 + n * Math.PI / 3), packProperties[i].CirclesCenter[a].Y + 2 * packProperties[i].CircleRadius * Math.Sin(Math.PI / 6 + n * Math.PI / 3));
                        //Point(center.X, center.Y, height, width, graphics);
                        List<PointD> orderedList = packProperties[i].CirclesCenter.FindAll(x => x.Distance(center) < Math.Pow(10, -4));
                        if (orderedList.Count == 1)
                        {
                            if (exagonalPack.ContainsKey(packProperties[i].NumberOfCircles))
                                exagonalPack[packProperties[i].NumberOfCircles]++;
                            else
                                exagonalPack.Add(packProperties[i].NumberOfCircles, 1);
                        }
                        else if (orderedList.Count > 1)
                            throw new Exception("More than one circles candidate");
                    }
                });
            }

            string output = "";
            for (int i = 0; i < exagonalPack.Count; i++)
                output += exagonalPack.ElementAt(i).Key + " " + exagonalPack.ElementAt(i).Value + Environment.NewLine;

            File.WriteAllText("output.txt", output);
        }
    }
}