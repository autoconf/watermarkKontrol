using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace watermarkKontrol
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int uzunluk = 0;
        private void goruntuSecPictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog open = new OpenFileDialog();
                open.Filter = "Image Files(*.bmp)|*.bmp";
                if (open.ShowDialog() == DialogResult.OK)
                {
                    goruntuSecPictureBox.Image = new Bitmap(open.FileName);
                    if (goruntuSecPictureBox.Image.Height < goruntuSecPictureBox.Height || goruntuSecPictureBox.Image.Width < goruntuSecPictureBox.Width)
                    {
                        goruntuSecPictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
                        /// görüntü içeriği okunacak
                        gorselOku(open.SafeFileName);

                    }
                    else
                    {
                        goruntuSecPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Görüntü Seçiminde Hata");
            }
        }

        private void gorselOku(string dosyaadi)
        {
            char[] silinecek = { '.', 'b', 'm', 'p', ' ' };
            uzunluk = Convert.ToInt32(dosyaadi.TrimEnd(silinecek));
            Bitmap gorsel = new Bitmap(goruntuSecPictureBox.Image);
        }

        private void metniAyirButon_Click(object sender, EventArgs e)
        {
            Bitmap gorsel = new Bitmap(goruntuSecPictureBox.Image);

            string ilk6piksel = string.Empty;
            for (int x = 0; x < 6; x++)
            {
                Color piksel = gorsel.GetPixel(x, 0);
                ilk6piksel += Convert.ToString((int)piksel.B, 2).Last();
            }
            MessageBox.Show("İlk 6 pikselden okunan bilgi : " + ilk6piksel);

            ///Koda bağlı binary metin okuma işlemleri 
            ///
            string metin = string.Empty;

            //ilk 6 bitten sonra renk paletinden metin çıkarılıyor..
            if (ilk6piksel[0] == '0' && ilk6piksel[1] == '0')
            {
                //şifreleme yok
                if (ilk6piksel[2] == '1' && ilk6piksel[3] == '0')
                {
                    //tüm piksel
                    if (ilk6piksel[4] == '0' && ilk6piksel[5] == '0')
                    {
                        //tüm palet
                        int i = 0;
                        string alpha = string.Empty;
                        for (int y = 0; y < gorsel.Height; y++) //gorsel.Height
                        {
                            for (int x = 0; x < gorsel.Width; x++) //gorsel.Width
                            {
                                if (y == 0 && x == 0)
                                {
                                    x = x + 7;
                                }
                                if (i < uzunluk)
                                {
                                    Color piksel = gorsel.GetPixel(x, y);
                                    string binaryA = string.Empty;
                                    string binaryR = string.Empty;
                                    string binaryG = string.Empty;
                                    string binaryB = string.Empty;
                                    binaryA = Convert.ToString((int)piksel.A, 2);
                                    binaryR = Convert.ToString((int)piksel.R, 2);
                                    binaryG = Convert.ToString((int)piksel.G, 2);
                                    binaryB = Convert.ToString((int)piksel.B, 2);
                                    metin += binaryR.Last().ToString() + binaryG.Last().ToString() + binaryB.Last().ToString();
                                    i += 3;
                                }
                            }
                        }
                        metniMetinKutusunaYaz("sifresiz", metin);
                    }
                    else if (ilk6piksel[4] == '0' && ilk6piksel[5] == '1')
                    {
                        //kirmizi palet
                        int i = 0;
                        string alpha = string.Empty;
                        for (int y = 0; y < gorsel.Height; y++) //gorsel.Height
                        {
                            for (int x = 0; x < gorsel.Width; x++) //gorsel.Width
                            {
                                if (y == 0 && x == 0)
                                {
                                    x = x + 7;
                                }
                                if (i < uzunluk)
                                {
                                    Color piksel = gorsel.GetPixel(x, y);
                                    string binaryR = string.Empty;
                                    binaryR = Convert.ToString((int)piksel.R, 2);
                                    metin += binaryR.Last().ToString();
                                    i += 1;
                                }
                            }
                        }
                        metniMetinKutusunaYaz("sifresiz", metin);
                    }
                    else if (ilk6piksel[4] == '1' && ilk6piksel[5] == '0')
                    {
                        //yesil palet
                        int i = 0;
                        string alpha = string.Empty;
                        for (int y = 0; y < gorsel.Height; y++) //gorsel.Height
                        {
                            for (int x = 0; x < gorsel.Width; x++) //gorsel.Width
                            {
                                if (y == 0 && x == 0)
                                {
                                    x = x + 7;
                                }
                                if (i < uzunluk)
                                {
                                    Color piksel = gorsel.GetPixel(x, y);
                                    string binaryG = string.Empty;
                                    binaryG = Convert.ToString((int)piksel.G, 2);
                                    metin += binaryG.Last().ToString();
                                    i += 1;
                                }
                            }
                        }
                        metniMetinKutusunaYaz("sifresiz", metin);
                    }
                    else if (ilk6piksel[4] == '1' && ilk6piksel[5] == '1')
                    {
                        //mavi palet
                        int i = 0;
                        string alpha = string.Empty;
                        for (int y = 0; y < gorsel.Height; y++) //gorsel.Height
                        {
                            for (int x = 0; x < gorsel.Width; x++) //gorsel.Width
                            {
                                if (y == 0 && x == 0)
                                {
                                    x = x + 7;
                                }
                                if (i < uzunluk)
                                {
                                    Color piksel = gorsel.GetPixel(x, y);
                                    string binaryB = string.Empty;
                                    binaryB = Convert.ToString((int)piksel.B, 2);
                                    metin += binaryB.Last().ToString();
                                    i += 1;
                                }
                            }
                        }
                        metniMetinKutusunaYaz("sifresiz", metin);
                    }
                }
                else if (ilk6piksel[2] == '0' && ilk6piksel[3] == '0')
                {
                    //tek piksel
                    int tekheight = 0, tekwidth = 0;
                    if (gorsel.Height % 2 == 0)
                    {
                        tekheight = gorsel.Height - 1;
                    }
                    if (gorsel.Width % 2 == 0)
                    {
                        tekwidth = gorsel.Width - 1;
                    }
                    if (ilk6piksel[4] == '0' && ilk6piksel[5] == '0')
                    {
                        //tüm palet
                        int i = 0;
                        string alpha = string.Empty;
                        for (int y = 1; y < tekheight; y++) //gorsel.Height
                        {
                            for (int x = 1; x < tekwidth; x++) //gorsel.Width
                            {
                                if (y == 0 && x == 0)
                                {
                                    x = x + 7;
                                }
                                if (i < uzunluk)
                                {
                                    Color piksel = gorsel.GetPixel(x, y);
                                    string binaryA = string.Empty;
                                    string binaryR = string.Empty;
                                    string binaryG = string.Empty;
                                    string binaryB = string.Empty;
                                    binaryA = Convert.ToString((int)piksel.A, 2);
                                    binaryR = Convert.ToString((int)piksel.R, 2);
                                    binaryG = Convert.ToString((int)piksel.G, 2);
                                    binaryB = Convert.ToString((int)piksel.B, 2);
                                    metin += binaryR.Last().ToString() + binaryG.Last().ToString() + binaryB.Last().ToString();
                                    i += 3;
                                }
                            }
                        }
                        metniMetinKutusunaYaz("sifresiz", metin);
                    }
                    else if (ilk6piksel[4] == '0' && ilk6piksel[5] == '1')
                    {
                        //kirmizi palet
                        int i = 0;
                        string alpha = string.Empty;
                        for (int y = 1; y < tekheight; y++) //gorsel.Height
                        {
                            for (int x = 1; x < tekwidth; x++) //gorsel.Width
                            {
                                if (y == 0 && x == 0)
                                {
                                    x = x + 7;
                                }
                                if (i < uzunluk)
                                {
                                    Color piksel = gorsel.GetPixel(x, y);
                                    string binaryR = string.Empty;
                                    binaryR = Convert.ToString((int)piksel.R, 2);
                                    metin += binaryR.Last().ToString();
                                    i += 1;
                                }
                            }
                        }
                        metniMetinKutusunaYaz("sifresiz", metin);
                    }
                    else if (ilk6piksel[4] == '1' && ilk6piksel[5] == '0')
                    {
                        //yesil palet
                        int i = 0;
                        string alpha = string.Empty;
                        for (int y = 1; y < tekheight; y++) //gorsel.Height
                        {
                            for (int x = 1; x < tekwidth; x++) //gorsel.Width
                            {
                                if (y == 0 && x == 0)
                                {
                                    x = x + 7;
                                }
                                if (i < uzunluk)
                                {
                                    Color piksel = gorsel.GetPixel(x, y);
                                    string binaryG = string.Empty;
                                    binaryG = Convert.ToString((int)piksel.G, 2);
                                    metin += binaryG.Last().ToString();
                                    i += 1;
                                }
                            }
                        }
                        metniMetinKutusunaYaz("sifresiz", metin);
                    }
                    else if (ilk6piksel[4] == '1' && ilk6piksel[5] == '1')
                    {
                        //mavi palet
                        int i = 0;
                        string alpha = string.Empty;
                        for (int y = 1; y < tekheight; y++) //gorsel.Height
                        {
                            for (int x = 1; x < tekwidth; x++) //gorsel.Width
                            {
                                if (y == 0 && x == 0)
                                {
                                    x = x + 7;
                                }
                                if (i < uzunluk)
                                {
                                    Color piksel = gorsel.GetPixel(x, y);
                                    string binaryB = string.Empty;
                                    binaryB = Convert.ToString((int)piksel.B, 2);
                                    metin += binaryB.Last().ToString();
                                    i += 1;
                                }
                            }
                        }
                        metniMetinKutusunaYaz("sifresiz", metin);
                    }
                }
                else if (ilk6piksel[2] == '0' && ilk6piksel[3] == '1')
                {
                    //çift piksel
                    int ciftheight = 0, ciftwidth = 0;
                    if (gorsel.Height % 2 == 0)
                    {
                        ciftheight = gorsel.Height - 1;
                    }
                    if (gorsel.Width % 2 == 0)
                    {
                        ciftwidth = gorsel.Width - 1;
                    }
                    if (ilk6piksel[4] == '0' && ilk6piksel[5] == '0')
                    {
                        //tüm palet
                        int i = 0;
                        string alpha = string.Empty;
                        for (int y = 0; y < ciftheight; y++) //gorsel.Height
                        {
                            for (int x = 0; x < ciftwidth; x++) //gorsel.Width
                            {
                                if (y == 0 && x == 0)
                                {
                                    x = x + 7;
                                }
                                if (i < uzunluk - 1)
                                {
                                    Color piksel = gorsel.GetPixel(x, y);
                                    string binaryA = string.Empty;
                                    string binaryR = string.Empty;
                                    string binaryG = string.Empty;
                                    string binaryB = string.Empty;
                                    binaryA = Convert.ToString((int)piksel.A, 2);
                                    binaryR = Convert.ToString((int)piksel.R, 2);
                                    binaryG = Convert.ToString((int)piksel.G, 2);
                                    binaryB = Convert.ToString((int)piksel.B, 2);
                                    metin += binaryR.Last().ToString() + binaryG.Last().ToString() + binaryB.Last().ToString();
                                    i += 3;
                                }
                            }
                        }
                        metniMetinKutusunaYaz("sifresiz", metin);
                    }
                    else if (ilk6piksel[4] == '0' && ilk6piksel[5] == '1')
                    {
                        //kirmizi palet
                        int i = 0;
                        string alpha = string.Empty;
                        for (int y = 0; y < ciftheight; y++) //gorsel.Height
                        {
                            for (int x = 0; x < ciftwidth; x++) //gorsel.Width
                            {
                                if (y == 0 && x == 0)
                                {
                                    x = x + 7;
                                }
                                if (i < uzunluk)
                                {
                                    Color piksel = gorsel.GetPixel(x, y);
                                    string binaryR = string.Empty;
                                    binaryR = Convert.ToString((int)piksel.R, 2);
                                    metin += binaryR.Last().ToString();
                                    i += 1;
                                }
                            }
                        }
                        metniMetinKutusunaYaz("sifresiz", metin);
                    }
                    else if (ilk6piksel[4] == '1' && ilk6piksel[5] == '0')
                    {
                        //yesil palet
                        int i = 0;
                        string alpha = string.Empty;
                        for (int y = 0; y < ciftheight; y++) //gorsel.Height
                        {
                            for (int x = 0; x < ciftwidth; x++) //gorsel.Width
                            {
                                if (y == 0 && x == 0)
                                {
                                    x = x + 7;
                                }
                                if (i < uzunluk)
                                {
                                    Color piksel = gorsel.GetPixel(x, y);
                                    string binaryG = string.Empty;
                                    binaryG = Convert.ToString((int)piksel.G, 2);
                                    metin += binaryG.Last().ToString();
                                    i += 1;
                                }
                            }
                        }
                        metniMetinKutusunaYaz("sifresiz", metin);
                    }
                    else if (ilk6piksel[4] == '1' && ilk6piksel[5] == '1')
                    {
                        //mavi palet
                        int i = 0;
                        string alpha = string.Empty;
                        for (int y = 0; y < ciftheight; y++) //gorsel.Height
                        {
                            for (int x = 0; x < ciftwidth; x++) //gorsel.Width
                            {
                                if (y == 0 && x == 0)
                                {
                                    x = x + 7;
                                }
                                if (i < uzunluk)
                                {
                                    Color piksel = gorsel.GetPixel(x, y);
                                    string binaryB = string.Empty;
                                    binaryB = Convert.ToString((int)piksel.B, 2);
                                    metin += binaryB.Last().ToString();
                                    i += 1;
                                }
                            }
                        }
                        metniMetinKutusunaYaz("sifresiz", metin);
                    }
                }
            }
            else if (ilk6piksel[0] == '1' && ilk6piksel[1] == '0')
            {
                //reverse ascii
                if (ilk6piksel[2] == '1' && ilk6piksel[3] == '0')
                {
                    //tüm piksel
                    if (ilk6piksel[4] == '0' && ilk6piksel[5] == '0')
                    {
                        //tüm palet
                        int i = 0;
                        string alpha = string.Empty;
                        for (int y = 0; y < gorsel.Height; y++) //gorsel.Height
                        {
                            for (int x = 0; x < gorsel.Width; x++) //gorsel.Width
                            {
                                if (y == 0 && x == 0)
                                {
                                    x = x + 7;
                                }
                                if (i < uzunluk)
                                {
                                    Color piksel = gorsel.GetPixel(x, y);
                                    string binaryA = string.Empty;
                                    string binaryR = string.Empty;
                                    string binaryG = string.Empty;
                                    string binaryB = string.Empty;
                                    binaryA = Convert.ToString((int)piksel.A, 2);
                                    binaryR = Convert.ToString((int)piksel.R, 2);
                                    binaryG = Convert.ToString((int)piksel.G, 2);
                                    binaryB = Convert.ToString((int)piksel.B, 2);
                                    metin += binaryR.Last().ToString() + binaryG.Last().ToString() + binaryB.Last().ToString();
                                    i += 3;
                                }
                            }
                        }
                        metniMetinKutusunaYaz("rAscii", metin);
                    }
                    else if (ilk6piksel[4] == '0' && ilk6piksel[5] == '1')
                    {
                        //kirmizi palet
                        int i = 0;
                        string alpha = string.Empty;
                        for (int y = 0; y < gorsel.Height; y++) //gorsel.Height
                        {
                            for (int x = 0; x < gorsel.Width; x++) //gorsel.Width
                            {
                                if (y == 0 && x == 0)
                                {
                                    x = x + 7;
                                }
                                if (i < uzunluk)
                                {
                                    Color piksel = gorsel.GetPixel(x, y);
                                    string binaryR = string.Empty;
                                    binaryR = Convert.ToString((int)piksel.R, 2);
                                    metin += binaryR.Last().ToString();
                                    i += 1;
                                }
                            }
                        }
                        metniMetinKutusunaYaz("rAscii", metin);
                    }
                    else if (ilk6piksel[4] == '1' && ilk6piksel[5] == '0')
                    {
                        //yesil palet
                        int i = 0;
                        string alpha = string.Empty;
                        for (int y = 0; y < gorsel.Height; y++) //gorsel.Height
                        {
                            for (int x = 0; x < gorsel.Width; x++) //gorsel.Width
                            {
                                if (y == 0 && x == 0)
                                {
                                    x = x + 7;
                                }
                                if (i < uzunluk)
                                {
                                    Color piksel = gorsel.GetPixel(x, y);
                                    string binaryG = string.Empty;
                                    binaryG = Convert.ToString((int)piksel.G, 2);
                                    metin += binaryG.Last().ToString();
                                    i += 1;
                                }
                            }
                        }
                        metniMetinKutusunaYaz("rAscii", metin);
                    }
                    else if (ilk6piksel[4] == '1' && ilk6piksel[5] == '1')
                    {
                        //mavi palet
                        int i = 0;
                        string alpha = string.Empty;
                        for (int y = 0; y < gorsel.Height; y++) //gorsel.Height
                        {
                            for (int x = 0; x < gorsel.Width; x++) //gorsel.Width
                            {
                                if (y == 0 && x == 0)
                                {
                                    x = x + 7;
                                }
                                if (i < uzunluk)
                                {
                                    Color piksel = gorsel.GetPixel(x, y);
                                    string binaryB = string.Empty;
                                    binaryB = Convert.ToString((int)piksel.B, 2);
                                    metin += binaryB.Last().ToString();
                                    i += 1;
                                }
                            }
                        }
                        metniMetinKutusunaYaz("rAscii", metin);
                    }
                }
                else if (ilk6piksel[2] == '0' && ilk6piksel[3] == '0')
                {
                    //tek piksel
                    int tekheight = 0, tekwidth = 0;
                    if (gorsel.Height % 2 == 0)
                    {
                        tekheight = gorsel.Height - 1;
                    }
                    if (gorsel.Width % 2 == 0)
                    {
                        tekwidth = gorsel.Width - 1;
                    }
                    if (ilk6piksel[4] == '0' && ilk6piksel[5] == '0')
                    {
                        //tüm palet
                        int i = 0;
                        string alpha = string.Empty;
                        for (int y = 1; y < tekheight; y++) //gorsel.Height
                        {
                            for (int x = 1; x < tekwidth; x++) //gorsel.Width
                            {
                                if (y == 0 && x == 0)
                                {
                                    x = x + 7;
                                }
                                if (i < uzunluk)
                                {
                                    Color piksel = gorsel.GetPixel(x, y);
                                    string binaryA = string.Empty;
                                    string binaryR = string.Empty;
                                    string binaryG = string.Empty;
                                    string binaryB = string.Empty;
                                    binaryA = Convert.ToString((int)piksel.A, 2);
                                    binaryR = Convert.ToString((int)piksel.R, 2);
                                    binaryG = Convert.ToString((int)piksel.G, 2);
                                    binaryB = Convert.ToString((int)piksel.B, 2);
                                    metin += binaryR.Last().ToString() + binaryG.Last().ToString() + binaryB.Last().ToString();
                                    i += 3;
                                }
                            }
                        }
                        metniMetinKutusunaYaz("rAscii", metin);
                    }
                    else if (ilk6piksel[4] == '0' && ilk6piksel[5] == '1')
                    {
                        //kirmizi palet
                        int i = 0;
                        string alpha = string.Empty;
                        for (int y = 1; y < tekheight; y++) //gorsel.Height
                        {
                            for (int x = 1; x < tekwidth; x++) //gorsel.Width
                            {
                                if (y == 0 && x == 0)
                                {
                                    x = x + 7;
                                }
                                if (i < uzunluk)
                                {
                                    Color piksel = gorsel.GetPixel(x, y);
                                    string binaryR = string.Empty;
                                    binaryR = Convert.ToString((int)piksel.R, 2);
                                    metin += binaryR.Last().ToString();
                                    i += 1;
                                }
                            }
                        }
                        metniMetinKutusunaYaz("rAscii", metin);
                    }
                    else if (ilk6piksel[4] == '1' && ilk6piksel[5] == '0')
                    {
                        //yesil palet
                        int i = 0;
                        string alpha = string.Empty;
                        for (int y = 1; y < tekheight; y++) //gorsel.Height
                        {
                            for (int x = 1; x < tekwidth; x++) //gorsel.Width
                            {
                                if (y == 0 && x == 0)
                                {
                                    x = x + 7;
                                }
                                if (i < uzunluk)
                                {
                                    Color piksel = gorsel.GetPixel(x, y);
                                    string binaryG = string.Empty;
                                    binaryG = Convert.ToString((int)piksel.G, 2);
                                    metin += binaryG.Last().ToString();
                                    i += 1;
                                }
                            }
                        }
                        metniMetinKutusunaYaz("rAscii", metin);
                    }
                    else if (ilk6piksel[4] == '1' && ilk6piksel[5] == '1')
                    {
                        //mavi palet
                        int i = 0;
                        string alpha = string.Empty;
                        for (int y = 1; y < tekheight; y++) //gorsel.Height
                        {
                            for (int x = 1; x < tekwidth; x++) //gorsel.Width
                            {
                                if (y == 0 && x == 0)
                                {
                                    x = x + 7;
                                }
                                if (i < uzunluk)
                                {
                                    Color piksel = gorsel.GetPixel(x, y);
                                    string binaryB = string.Empty;
                                    binaryB = Convert.ToString((int)piksel.B, 2);
                                    metin += binaryB.Last().ToString();
                                    i += 1;
                                }
                            }
                        }
                        metniMetinKutusunaYaz("rAscii", metin);
                    }
                }
                else if (ilk6piksel[2] == '0' && ilk6piksel[3] == '1')
                {
                    //çift piksel
                    int ciftheight = 0, ciftwidth = 0;
                    if (gorsel.Height % 2 == 0)
                    {
                        ciftheight = gorsel.Height - 1;
                    }
                    if (gorsel.Width % 2 == 0)
                    {
                        ciftwidth = gorsel.Width - 1;
                    }
                    if (ilk6piksel[4] == '0' && ilk6piksel[5] == '0')
                    {
                        //tüm palet
                        int i = 0;
                        string alpha = string.Empty;
                        for (int y = 0; y < ciftheight; y++) //gorsel.Height
                        {
                            for (int x = 0; x < ciftwidth; x++) //gorsel.Width
                            {
                                if (y == 0 && x == 0)
                                {
                                    x = x + 7;
                                }
                                if (i < uzunluk - 1)
                                {
                                    Color piksel = gorsel.GetPixel(x, y);
                                    string binaryA = string.Empty;
                                    string binaryR = string.Empty;
                                    string binaryG = string.Empty;
                                    string binaryB = string.Empty;
                                    binaryA = Convert.ToString((int)piksel.A, 2);
                                    binaryR = Convert.ToString((int)piksel.R, 2);
                                    binaryG = Convert.ToString((int)piksel.G, 2);
                                    binaryB = Convert.ToString((int)piksel.B, 2);
                                    metin += binaryR.Last().ToString() + binaryG.Last().ToString() + binaryB.Last().ToString();
                                    i += 3;
                                }
                            }
                        }
                        metniMetinKutusunaYaz("rAscii", metin);
                    }
                    else if (ilk6piksel[4] == '0' && ilk6piksel[5] == '1')
                    {
                        //kirmizi palet
                        int i = 0;
                        string alpha = string.Empty;
                        for (int y = 0; y < ciftheight; y++) //gorsel.Height
                        {
                            for (int x = 0; x < ciftwidth; x++) //gorsel.Width
                            {
                                if (y == 0 && x == 0)
                                {
                                    x = x + 7;
                                }
                                if (i < uzunluk)
                                {
                                    Color piksel = gorsel.GetPixel(x, y);
                                    string binaryR = string.Empty;
                                    binaryR = Convert.ToString((int)piksel.R, 2);
                                    metin += binaryR.Last().ToString();
                                    i += 1;
                                }
                            }
                        }
                        metniMetinKutusunaYaz("rAscii", metin);
                    }
                    else if (ilk6piksel[4] == '1' && ilk6piksel[5] == '0')
                    {
                        //yesil palet
                        int i = 0;
                        string alpha = string.Empty;
                        for (int y = 0; y < ciftheight; y++) //gorsel.Height
                        {
                            for (int x = 0; x < ciftwidth; x++) //gorsel.Width
                            {
                                if (y == 0 && x == 0)
                                {
                                    x = x + 7;
                                }
                                if (i < uzunluk)
                                {
                                    Color piksel = gorsel.GetPixel(x, y);
                                    string binaryG = string.Empty;
                                    binaryG = Convert.ToString((int)piksel.G, 2);
                                    metin += binaryG.Last().ToString();
                                    i += 1;
                                }
                            }
                        }
                        metniMetinKutusunaYaz("rAscii", metin);
                    }
                    else if (ilk6piksel[4] == '1' && ilk6piksel[5] == '1')
                    {
                        //mavi palet
                        int i = 0;
                        string alpha = string.Empty;
                        for (int y = 0; y < ciftheight; y++) //gorsel.Height
                        {
                            for (int x = 0; x < ciftwidth; x++) //gorsel.Width
                            {
                                if (y == 0 && x == 0)
                                {
                                    x = x + 7;
                                }
                                if (i < uzunluk)
                                {
                                    Color piksel = gorsel.GetPixel(x, y);
                                    string binaryB = string.Empty;
                                    binaryB = Convert.ToString((int)piksel.B, 2);
                                    metin += binaryB.Last().ToString();
                                    i += 1;
                                }
                            }
                        }
                        metniMetinKutusunaYaz("rAscii", metin);
                    }
                }
            }
            else if (ilk6piksel[0] == '1' && ilk6piksel[1] == '1')
            {
                //steganografi
                if (ilk6piksel[2] == '1' && ilk6piksel[3] == '0')
                {
                    //tüm piksel
                    if (ilk6piksel[4] == '0' && ilk6piksel[5] == '0')
                    {
                        //tüm palet
                        int i = 0;
                        string alpha = string.Empty;
                        for (int y = 0; y < gorsel.Height; y++) //gorsel.Height
                        {
                            for (int x = 0; x < gorsel.Width; x++) //gorsel.Width
                            {
                                if (y == 0 && x == 0)
                                {
                                    x = x + 7;
                                }
                                if (i < uzunluk)
                                {
                                    Color piksel = gorsel.GetPixel(x, y);
                                    string binaryA = string.Empty;
                                    string binaryR = string.Empty;
                                    string binaryG = string.Empty;
                                    string binaryB = string.Empty;
                                    binaryA = Convert.ToString((int)piksel.A, 2);
                                    binaryR = Convert.ToString((int)piksel.R, 2);
                                    binaryG = Convert.ToString((int)piksel.G, 2);
                                    binaryB = Convert.ToString((int)piksel.B, 2);
                                    metin += binaryR.Last().ToString() + binaryG.Last().ToString() + binaryB.Last().ToString();
                                    i += 3;
                                }
                            }
                        }
                        metniMetinKutusunaYaz("steg", metin);
                    }
                    else if (ilk6piksel[4] == '0' && ilk6piksel[5] == '1')
                    {
                        //kirmizi palet
                        int i = 0;
                        string alpha = string.Empty;
                        for (int y = 0; y < gorsel.Height; y++) //gorsel.Height
                        {
                            for (int x = 0; x < gorsel.Width; x++) //gorsel.Width
                            {
                                if (y == 0 && x == 0)
                                {
                                    x = x + 7;
                                }
                                if (i < uzunluk)
                                {
                                    Color piksel = gorsel.GetPixel(x, y);
                                    string binaryR = string.Empty;
                                    binaryR = Convert.ToString((int)piksel.R, 2);
                                    metin += binaryR.Last().ToString();
                                    i += 1;
                                }
                            }
                        }
                        metniMetinKutusunaYaz("steg", metin);
                    }
                    else if (ilk6piksel[4] == '1' && ilk6piksel[5] == '0')
                    {
                        //yesil palet
                        int i = 0;
                        string alpha = string.Empty;
                        for (int y = 0; y < gorsel.Height; y++) //gorsel.Height
                        {
                            for (int x = 0; x < gorsel.Width; x++) //gorsel.Width
                            {
                                if (y == 0 && x == 0)
                                {
                                    x = x + 7;
                                }
                                if (i < uzunluk)
                                {
                                    Color piksel = gorsel.GetPixel(x, y);
                                    string binaryG = string.Empty;
                                    binaryG = Convert.ToString((int)piksel.G, 2);
                                    metin += binaryG.Last().ToString();
                                    i += 1;
                                }
                            }
                        }
                        metniMetinKutusunaYaz("steg", metin);
                    }
                    else if (ilk6piksel[4] == '1' && ilk6piksel[5] == '1')
                    {
                        //mavi palet
                        int i = 0;
                        string alpha = string.Empty;
                        for (int y = 0; y < gorsel.Height; y++) //gorsel.Height
                        {
                            for (int x = 0; x < gorsel.Width; x++) //gorsel.Width
                            {
                                if (y == 0 && x == 0)
                                {
                                    x = x + 7;
                                }
                                if (i < uzunluk)
                                {
                                    Color piksel = gorsel.GetPixel(x, y);
                                    string binaryB = string.Empty;
                                    binaryB = Convert.ToString((int)piksel.B, 2);
                                    metin += binaryB.Last().ToString();
                                    i += 1;
                                }
                            }
                        }
                        metniMetinKutusunaYaz("steg", metin);
                    }
                }
                else if (ilk6piksel[2] == '0' && ilk6piksel[3] == '0')
                {
                    //tek piksel
                    int tekheight = 0, tekwidth = 0;
                    if (gorsel.Height % 2 == 0)
                    {
                        tekheight = gorsel.Height - 1;
                    }
                    if (gorsel.Width % 2 == 0)
                    {
                        tekwidth = gorsel.Width - 1;
                    }
                    if (ilk6piksel[4] == '0' && ilk6piksel[5] == '0')
                    {
                        //tüm palet
                        int i = 0;
                        string alpha = string.Empty;
                        for (int y = 1; y < tekheight; y++) //gorsel.Height
                        {
                            for (int x = 1; x < tekwidth; x++) //gorsel.Width
                            {
                                if (y == 0 && x == 0)
                                {
                                    x = x + 7;
                                }
                                if (i < uzunluk)
                                {
                                    Color piksel = gorsel.GetPixel(x, y);
                                    string binaryA = string.Empty;
                                    string binaryR = string.Empty;
                                    string binaryG = string.Empty;
                                    string binaryB = string.Empty;
                                    binaryA = Convert.ToString((int)piksel.A, 2);
                                    binaryR = Convert.ToString((int)piksel.R, 2);
                                    binaryG = Convert.ToString((int)piksel.G, 2);
                                    binaryB = Convert.ToString((int)piksel.B, 2);
                                    metin += binaryR.Last().ToString() + binaryG.Last().ToString() + binaryB.Last().ToString();
                                    i += 3;
                                }
                            }
                        }
                        metniMetinKutusunaYaz("steg", metin);
                    }
                    else if (ilk6piksel[4] == '0' && ilk6piksel[5] == '1')
                    {
                        //kirmizi palet
                        int i = 0;
                        string alpha = string.Empty;
                        for (int y = 1; y < tekheight; y++) //gorsel.Height
                        {
                            for (int x = 1; x < tekwidth; x++) //gorsel.Width
                            {
                                if (y == 0 && x == 0)
                                {
                                    x = x + 7;
                                }
                                if (i < uzunluk)
                                {
                                    Color piksel = gorsel.GetPixel(x, y);
                                    string binaryR = string.Empty;
                                    binaryR = Convert.ToString((int)piksel.R, 2);
                                    metin += binaryR.Last().ToString();
                                    i += 1;
                                }
                            }
                        }
                        metniMetinKutusunaYaz("steg", metin);
                    }
                    else if (ilk6piksel[4] == '1' && ilk6piksel[5] == '0')
                    {
                        //yesil palet
                        int i = 0;
                        string alpha = string.Empty;
                        for (int y = 1; y < tekheight; y++) //gorsel.Height
                        {
                            for (int x = 1; x < tekwidth; x++) //gorsel.Width
                            {
                                if (y == 0 && x == 0)
                                {
                                    x = x + 7;
                                }
                                if (i < uzunluk)
                                {
                                    Color piksel = gorsel.GetPixel(x, y);
                                    string binaryG = string.Empty;
                                    binaryG = Convert.ToString((int)piksel.G, 2);
                                    metin += binaryG.Last().ToString();
                                    i += 1;
                                }
                            }
                        }
                        metniMetinKutusunaYaz("steg", metin);
                    }
                    else if (ilk6piksel[4] == '1' && ilk6piksel[5] == '1')
                    {
                        //mavi palet
                        int i = 0;
                        string alpha = string.Empty;
                        for (int y = 1; y < tekheight; y++) //gorsel.Height
                        {
                            for (int x = 1; x < tekwidth; x++) //gorsel.Width
                            {
                                if (y == 0 && x == 0)
                                {
                                    x = x + 7;
                                }
                                if (i < uzunluk)
                                {
                                    Color piksel = gorsel.GetPixel(x, y);
                                    string binaryB = string.Empty;
                                    binaryB = Convert.ToString((int)piksel.B, 2);
                                    metin += binaryB.Last().ToString();
                                    i += 1;
                                }
                            }
                        }
                        metniMetinKutusunaYaz("steg", metin);
                    }
                }
                else if (ilk6piksel[2] == '0' && ilk6piksel[3] == '1')
                {
                    //çift piksel
                    int ciftheight = 0, ciftwidth = 0;
                    if (gorsel.Height % 2 == 0)
                    {
                        ciftheight = gorsel.Height - 1;
                    }
                    if (gorsel.Width % 2 == 0)
                    {
                        ciftwidth = gorsel.Width - 1;
                    }
                    if (ilk6piksel[4] == '0' && ilk6piksel[5] == '0')
                    {
                        //tüm palet
                        int i = 0;
                        string alpha = string.Empty;
                        for (int y = 0; y < ciftheight; y++) //gorsel.Height
                        {
                            for (int x = 0; x < ciftwidth; x++) //gorsel.Width
                            {
                                if (y == 0 && x == 0)
                                {
                                    x = x + 7;
                                }
                                if (i < uzunluk - 1)
                                {
                                    Color piksel = gorsel.GetPixel(x, y);
                                    string binaryA = string.Empty;
                                    string binaryR = string.Empty;
                                    string binaryG = string.Empty;
                                    string binaryB = string.Empty;
                                    binaryA = Convert.ToString((int)piksel.A, 2);
                                    binaryR = Convert.ToString((int)piksel.R, 2);
                                    binaryG = Convert.ToString((int)piksel.G, 2);
                                    binaryB = Convert.ToString((int)piksel.B, 2);
                                    metin += binaryR.Last().ToString() + binaryG.Last().ToString() + binaryB.Last().ToString();
                                    i += 3;
                                }
                            }
                        }
                        metniMetinKutusunaYaz("steg", metin);
                    }
                    else if (ilk6piksel[4] == '0' && ilk6piksel[5] == '1')
                    {
                        //kirmizi palet
                        int i = 0;
                        string alpha = string.Empty;
                        for (int y = 0; y < ciftheight; y++) //gorsel.Height
                        {
                            for (int x = 0; x < ciftwidth; x++) //gorsel.Width
                            {
                                if (y == 0 && x == 0)
                                {
                                    x = x + 7;
                                }
                                if (i < uzunluk)
                                {
                                    Color piksel = gorsel.GetPixel(x, y);
                                    string binaryR = string.Empty;
                                    binaryR = Convert.ToString((int)piksel.R, 2);
                                    metin += binaryR.Last().ToString();
                                    i += 1;
                                }
                            }
                        }
                        metniMetinKutusunaYaz("steg", metin);
                    }
                    else if (ilk6piksel[4] == '1' && ilk6piksel[5] == '0')
                    {
                        //yesil palet
                        int i = 0;
                        string alpha = string.Empty;
                        for (int y = 0; y < ciftheight; y++) //gorsel.Height
                        {
                            for (int x = 0; x < ciftwidth; x++) //gorsel.Width
                            {
                                if (y == 0 && x == 0)
                                {
                                    x = x + 7;
                                }
                                if (i < uzunluk)
                                {
                                    Color piksel = gorsel.GetPixel(x, y);
                                    string binaryG = string.Empty;
                                    binaryG = Convert.ToString((int)piksel.G, 2);
                                    metin += binaryG.Last().ToString();
                                    i += 1;
                                }
                            }
                        }
                        metniMetinKutusunaYaz("steg", metin);
                    }
                    else if (ilk6piksel[4] == '1' && ilk6piksel[5] == '1')
                    {
                        //mavi palet
                        int i = 0;
                        string alpha = string.Empty;
                        for (int y = 0; y < ciftheight; y++) //gorsel.Height
                        {
                            for (int x = 0; x < ciftwidth; x++) //gorsel.Width
                            {
                                if (y == 0 && x == 0)
                                {
                                    x = x + 7;
                                }
                                if (i < uzunluk)
                                {
                                    Color piksel = gorsel.GetPixel(x, y);
                                    string binaryB = string.Empty;
                                    binaryB = Convert.ToString((int)piksel.B, 2);
                                    metin += binaryB.Last().ToString();
                                    i += 1;
                                }
                            }
                        }
                        metniMetinKutusunaYaz("steg", metin);
                    }
                }
            }
        }
        private void metniMetinKutusunaYaz(string sifre, string metin)
        {
            if (sifre == "rAscii")
            {
                try
                {
                    if (metin.Length > uzunluk)
                    {
                        metin = metin.Substring(0, uzunluk);
                    }
                    MessageBox.Show(metin + "\n" + metin.Length);
                    int fark = metin.Length - uzunluk;
                    //metne çevir
                    List<Byte> byteListSifreli = new List<Byte>();
                    List<Byte> byteList = new List<Byte>();
                    //8den başlıyor --> 8bit-1byte
                    for (int x = 0; x < metin.Length - fark; x += 8)
                    {
                        byteListSifreli.Add(Convert.ToByte(metin.Substring(x, 8), 2));
                        byteList.Add(Convert.ToByte(255 - Convert.ToByte(metin.Substring(x, 8), 2)));
                    }
                    metinRichTextBox.Text = Encoding.ASCII.GetString(byteList.ToArray());
                    string sifreliMetin = Encoding.ASCII.GetString(byteListSifreli.ToArray());
                    MessageBox.Show("Okunan Şifreli Metin\n" + sifreliMetin + "\nÇevirilen Metin\n" + metinRichTextBox.Text + "\nToplam Karakter Sayısı : " + sifreliMetin.Length);
                }
                catch
                {
                    MessageBox.Show($"Reverse Ascii Metin okuma hatası {metin.Length} karakter çevirilemiyor.");
                }
                //Binary Olarak Karşılaştırma için aşağıda yorum haline getirilen kod çalıştırılmalı
                /*
                try
                {
                    string sifreliMetin = string.Empty;
                    for (int i = 0; i < metin.Length; i++)
                    {
                        if (metin[i] == '0')
                        {
                            sifreliMetin += "1";
                        }
                        else
                        {
                            sifreliMetin += "0";
                        }
                    }
                    if (sifreliMetin.Length > uzunluk)
                    {
                        sifreliMetin = sifreliMetin.Substring(0, uzunluk);
                    }
                    MessageBox.Show("Okunan Binary\n" + metin + "\nÇevirilen Binary\n" + sifreliMetin + "\nToplam Karakter Sayısı : " + sifreliMetin.Length);
                    int fark = sifreliMetin.Length - uzunluk;
                    //metne çevir
                    List<Byte> byteList = new List<Byte>();
                    //8den başlıyor --> 8bit-1byte
                    for (int x = 0; x < sifreliMetin.Length - fark; x += 8)
                    {
                        byteList.Add(Convert.ToByte(sifreliMetin.Substring(x, 8), 2));
                    }

                    metinRichTextBox.Text = Encoding.ASCII.GetString(byteList.ToArray());
                }
                catch
                {
                    MessageBox.Show($"Reverse Ascii Metin okuma hatası {metin.Length} karakter çevirilemiyor.");
                }
                */
            }
            else if (sifre == "steg")
            {
                try
                {
                    if (metin.Length > uzunluk)
                    {
                        metin = metin.Substring(0, uzunluk);
                    }
                    MessageBox.Show(metin + "\n" + metin.Length);
                    int fark = metin.Length - uzunluk;
                    //metne çevir
                    List<Byte> byteListSifreli = new List<Byte>();
                    List<Byte> byteList = new List<Byte>();
                    //8den başlıyor --> 8bit-1byte
                    for (int x = 0; x < metin.Length - fark; x += 8)
                    {
                        byteListSifreli.Add(Convert.ToByte(metin.Substring(x, 8), 2));
                        byteList.Add(Convert.ToByte(Convert.ToByte(metin.Substring(x, 8), 2) + 5));
                    }
                    metinRichTextBox.Text = Encoding.ASCII.GetString(byteList.ToArray());
                    string sifreliMetin = Encoding.ASCII.GetString(byteListSifreli.ToArray());
                    MessageBox.Show("Okunan Şifreli Metin\n" + sifreliMetin + "\nÇevirilen Metin\n" + metinRichTextBox.Text + "\nToplam Karakter Sayısı : " + sifreliMetin.Length);
                }
                catch
                {
                    MessageBox.Show($"Steganografi Metin okuma hatası {metin.Length} karakter çevirilemiyor.");
                }
            }
            else
            {
                try
                {
                    if (metin.Length > uzunluk)
                    {
                        metin = metin.Substring(0, uzunluk);
                    }
                    MessageBox.Show("Okunan Binary\n" + metin + "\nToplam Karakter Sayısı : " + metin.Length);
                    int fark = metin.Length - uzunluk;
                    //metne çevir
                    List<Byte> byteList = new List<Byte>();
                    //8den başlıyor --> 8bit-1byte
                    for (int x = 0; x < metin.Length - fark; x += 8)
                    {
                        byteList.Add(Convert.ToByte(metin.Substring(x, 8), 2));
                    }
                    metinRichTextBox.Text = Encoding.ASCII.GetString(byteList.ToArray());
                }
                catch
                {
                    MessageBox.Show($"Metin okuma hatası {metin.Length} karakter çevirilemiyor.");
                }
            }
        }
    }
}
