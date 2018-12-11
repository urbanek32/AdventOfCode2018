using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AoC_Day10_Visualization
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Coord> Coords { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            var regex = new Regex(@"^position=<\s*(?<posx>-?\d+),\s*(?<posy>-?\d+)> velocity=<\s*(?<velx>-?\d+),\s*(?<vely>-?\d+)>$");
            Coords =
                //File.ReadAllLines("../../input.txt")
                new List<string>
                {
                    @"position=< 9,  1> velocity=< 0,  2>",
                    @"position=< 7,  0> velocity=<-1,  0>",
                    @"position=< 3, -2> velocity=<-1,  1>",
                    @"position=< 6, 10> velocity=<-2, -1>",
                    @"position=< 2, -4> velocity=< 2,  2>",
                    @"position=<-6, 10> velocity=< 2, -2>",
                    @"position=< 1,  8> velocity=< 1, -1>",
                    @"position=< 1,  7> velocity=< 1,  0>",
                    @"position=<-3, 11> velocity=< 1, -2>",
                    @"position=< 7,  6> velocity=<-1, -1>",
                    @"position=<-2,  3> velocity=< 1,  0>",
                    @"position=<-4,  3> velocity=< 2,  0>",
                    @"position=<10, -3> velocity=<-1,  1>",
                    @"position=< 5, 11> velocity=< 1, -2>",
                    @"position=< 4,  7> velocity=< 0, -1>",
                    @"position=< 8, -2> velocity=< 0,  1>",
                    @"position=<15,  0> velocity=<-2,  0>",
                    @"position=< 1,  6> velocity=< 1,  0>",
                    @"position=< 8,  9> velocity=< 0, -1>",
                    @"position=< 3,  3> velocity=<-1,  1>",
                    @"position=< 0,  5> velocity=< 0, -1>",
                    @"position=<-2,  2> velocity=< 2,  0>",
                    @"position=< 5, -2> velocity=< 1,  2>",
                    @"position=< 1,  4> velocity=< 2,  1>",
                    @"position=<-2,  7> velocity=< 2, -2>",
                    @"position=< 3,  6> velocity=<-1, -1>",
                    @"position=< 5,  0> velocity=< 1,  0>",
                    @"position=<-6,  0> velocity=< 2,  0>",
                    @"position=< 5,  9> velocity=< 1, -2>",
                    @"position=<14,  7> velocity=<-2,  0>",
                    @"position=<-3,  6> velocity=< 2, -1>"
                }
                .Select(l => regex.Match(l))
                .Select(m => new Coord
                {
                    PosX = int.Parse(m.Groups["posx"].Value),
                    PosY = int.Parse(m.Groups["posy"].Value),
                    VelX = int.Parse(m.Groups["velx"].Value),
                    VelY = int.Parse(m.Groups["vely"].Value)
                })
                .ToList();
        }

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            GoNext();
        }

        private void GoNext()
        {
            /*var minX1 = Coords.Min(c => c.PosX);
            var minY1 = Coords.Min(c => c.PosY);
            foreach (var item in Coords)
            {
                item.PosX += Math.Abs(minX1);
                item.PosY += Math.Abs(minY1);
            }*/

            foreach (var item in Coords)
            {
                item.PosX += item.VelX;
                item.PosY += item.VelY;
            }

            var minX = Coords.Min(c => c.PosX);
            var minY = Coords.Min(c => c.PosY);
            var maxX = Coords.Max(c => c.PosX);
            var maxY = Coords.Max(c => c.PosY);

            const int width = 400;
            const int height = 400;

            WriteableBitmap wbitmap = new WriteableBitmap(width, height, 96, 96, PixelFormats.Bgra32, null);
            byte[,,] pixels = new byte[height, width, 4];

            // Clear to black.
            for (int row = 0; row <= maxX; row++)
            {
                for (int col = 0; col < maxY; col++)
                {
                    for (int i = 0; i < 3; i++)
                        pixels[row, col, i] = 0;
                    pixels[row, col, 2] = 255;
                }
            }

            // Blue.
            for (int row = 0; row < 80; row++)
            {
                for (int col = 0; col <= row; col++)
                {
                    pixels[row, col, 0] = 255;
                }
            }

            // Copy the data into a one-dimensional array.
            byte[] pixels1d = new byte[height * width * 4];
            int index = 0;
            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    for (int i = 0; i < 4; i++)
                        pixels1d[index++] = pixels[row, col, i];
                }
            }

            // Update writeable bitmap with the colorArray to the image.
            Int32Rect rect = new Int32Rect(0, 0, width, height);
            int stride = 4 * width;
            wbitmap.WritePixels(rect, pixels1d, stride, 0);

            imageArea.Source = wbitmap;
        }

        private class Coord
        {
            public int PosX { get; set; }
            public int PosY { get; set; }
            public int VelX { get; set; }
            public int VelY { get; set; }
        }
    }
}
