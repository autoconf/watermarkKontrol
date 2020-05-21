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
            MessageBox.Show(ilk6piksel);

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
                        metniMetinKutusunaYaz(metin);
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
                        metniMetinKutusunaYaz(metin);
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
                        metniMetinKutusunaYaz(metin);
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
                        metniMetinKutusunaYaz(metin);
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
                        metniMetinKutusunaYaz(metin);
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
                        metniMetinKutusunaYaz(metin);
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
                        metniMetinKutusunaYaz(metin);
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
                        metniMetinKutusunaYaz(metin);
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
                        metniMetinKutusunaYaz(metin);
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
                        metniMetinKutusunaYaz(metin);
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
                        metniMetinKutusunaYaz(metin);
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
                        metniMetinKutusunaYaz(metin);
                    }
                }
            }
            else if (ilk6piksel[0] == '1' && ilk6piksel[1] == '0')
            {
                //reverse ascii
            }
            else if (ilk6piksel[0] == '1' && ilk6piksel[1] == '1')
            {
                //steganografi
            }
        }
        private void metniMetinKutusunaYaz(string metin)
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
