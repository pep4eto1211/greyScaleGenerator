using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;

namespace greyScaleGenerator
{
    /// <summary>
    /// I came up with something, which is really slow, but it works :D
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        Bitmap convertToGreyscale(string imgPath)
        {
            Bitmap originalImage = (Bitmap)(Bitmap.FromFile(imgPath));

            Bitmap grayImage = new Bitmap(originalImage.Width, originalImage.Height);

            for (int i = 1; i < originalImage.Width; i++)
            {
                for (int j = 1; j < originalImage.Height; j++)
                {
                    int newColor = (int)((originalImage.GetPixel(i, j).R * 0.3) + (originalImage.GetPixel(i, j).G * 0.59) + (originalImage.GetPixel(i, j).B * 0.11));
                    grayImage.SetPixel(i, j, System.Drawing.Color.FromArgb(newColor, newColor, newColor));
                }
            }

            return grayImage;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openImageDialog = new System.Windows.Forms.OpenFileDialog();
            openImageDialog.Filter = "Images|*.jpg;*.png;*.bmp";
            if (openImageDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.Windows.Forms.SaveFileDialog saveFile = new System.Windows.Forms.SaveFileDialog();
                saveFile.Filter = "PNG|*.png;";
                if (saveFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Bitmap generatedImage = convertToGreyscale(openImageDialog.FileName);
                    generatedImage.Save(saveFile.FileName);
                    originalNameBox.Text = "Image " + openImageDialog.SafeFileName + " saved to:";
                    newImageNameBox.Text = saveFile.FileName;
                }
            }
        }
    }
}
