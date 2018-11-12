using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SimpleGrayScale
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            btnConverter_Click(null, null);
        }

        private void btnConverter_Click(object sender, EventArgs e)
        {
            string novoNome = Path.GetDirectoryName(txtCaminho.Text) + @"\gray_" + Path.GetFileName(txtCaminho.Text);
            Bitmap c = new Bitmap(txtCaminho.Text);

            //Image d = ToolStripRenderer.CreateDisabledImage(c);

            //d.Save(Path.GetDirectoryName(txtCaminho.Text) + @"\grayRender_" + Path.GetFileName(txtCaminho.Text), System.Drawing.Imaging.ImageFormat.Tiff);

            Bitmap grayScaled;
            //Load an image frim you local file system
            Image normalImage = Image.FromFile(txtCaminho.Text);
            // Turn image into gray scale image
            using (var img = new Bitmap(normalImage))
            {
                grayScaled = GrayscaleMatrix.Executar(img);
            }

            //CodecInfo para imagens Jpeg
            ImageCodecInfo codec = ImageCodecInfo.GetImageEncoders().First(enc => enc.FormatID == ImageFormat.Jpeg.Guid);
            //EncoderParameters que vai setar o nível de qualidade (compressão)
            EncoderParameters imgParams = new EncoderParameters(1);
            //Qualidade em 100L = 100% de qualidade - sem compressão
            imgParams.Param = new[] { new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 55L) };
            
            //Salvar a imagem a imagem

            if (File.Exists(novoNome)) File.Delete(novoNome);

            grayScaled.Save(novoNome, codec, imgParams);
        }
    }
}
