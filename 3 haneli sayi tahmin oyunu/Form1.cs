using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _3_haneli_sayi_tahmin_oyunu
{
    public partial class Form1 : Form
    {
        int tahmin_sayisi = 10; string dogrusayi;
        public Form1()
        {
            InitializeComponent();
            dogrusayi = rastgelesayiüret();
            this.AcceptButton = btnTahmin;
        }
        private string rastgelesayiüret()
        {
            Random rnd = new Random();
            int sayi;
            int birler, onlar, yuzler;
            bool rakamlarfarkli;

            do
            {
                sayi=rnd.Next(100,1000);
                birler = sayi % 10;
                onlar = (sayi / 10)%10;
                yuzler = sayi / 100;
                if(birler!=onlar && birler!=yuzler && onlar!=yuzler)
                {
                    rakamlarfarkli = true;
                }
                else
                {
                    rakamlarfarkli = false;
                }

            }while(rakamlarfarkli==false);
            return dogrusayi=sayi.ToString();
        }
       
        private void btnTahmin_Click(object sender, EventArgs e)
        {
            string tahmin=txtTahmin.Text;
            txtTahmin.Clear();
            listBox1.Visible = true;
            label4.Visible = true;
            panel2.Visible = true;
            
            if (tahmin.Length != 3)
            {
                lblSonuc.Text = "lütfen 3 basamakli bir sayi giriniz";
                lblYazi.Text = "girdiginiz sayı 3 haneli olmalı";
                return;
            }
            if (tahmin.StartsWith("0"))
            {
                lblSonuc.Text = "sayı sıfır ile başlayamaz";
                lblYazi.Text = "ilk rakam 1-9 arasında olmalı";
                return;
            }
            if(tahmin.Length!=tahmin.Distinct().Count())
            {
                lblSonuc.Text = "rakamlar birbirinden farkli olmali";
                lblYazi.Text = "örnek=123,789 gibi (tekrar eden rakam yok)";
                return;
            }
            if (tahmin_sayisi > 0)
            {

                tahmin_sayisi--;
                lblKalanHak.Text = "kalan hak:" + tahmin_sayisi.ToString();
            }
            else
            {
                lblSonuc.Text = "oyun bitti! dogru sayı:" + dogrusayi;
                lblYazi.Text = "yeniden başlamak için başlat butonuna tıklayın";
                return;
            }
            if(tahmin==dogrusayi)
            {
                lblSonuc.Text = "dogru sayıyı buldunuz..:)";
            }
            int dogruyer = 0;
            int yanlisyer = 0;

            bool[] dogrukullanildi = new bool[3];
            bool[] tahminkullanildi = new bool[3];

            for (int i = 0; i < 3; i++)
            {
                if (tahmin[i] == dogrusayi[i])
                {
                    dogruyer++;
                    dogrukullanildi[i] = true;
                    tahminkullanildi[i] = true;
                }
            }
            for (int i = 0; i < 3; i++)
            {
                if (tahminkullanildi[i]) continue;
                for (int j = 0; j < 3; j++)
                {
                    if (dogrukullanildi[j]) continue;
                    if (tahmin[i] == dogrusayi[j])
                    {
                        yanlisyer++;
                        dogrukullanildi[j] = true;
                        break;
                    }
                }

            }

            if(dogruyer == 0 && yanlisyer == 0)
            {
                lblSonuc.Visible=true;
                lblYazi.Text = "hiç bir rakam dogru degil";
            }
            if(dogruyer>0 && yanlisyer > 0)
            {
                lblSonuc.Visible=true ;
                lblYazi.Text =dogruyer+ "rakamin yeri dogru,"+yanlisyer+"rakam dogru ancak yeri yanlış";
            }
            else if(dogruyer>0 )
            {
                lblSonuc.Visible=true ;
                lblYazi.Text = dogruyer + "rakamin yeri dogru";
            }
            else if(yanlisyer>0 )
            {
                lblSonuc.Visible=true ;
                lblYazi.Text = yanlisyer + "rakam dogru ama yeri yanlış";
            }



            string sonuc = $"+{dogruyer}-{yanlisyer}";
            //lblYazi.Text = $"+{dogruyer} rakam dogru yerde,{yanlisyer} rakam dogru ama yeri yanlış";
            lblSonuc.Text = $"sonuç:{sonuc}";
            listBox1.Items.Add($"tahmin{tahmin}->{sonuc}");







        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtTahmin.Enabled = false;
            lblSonuc.Text = "";
            lblYazi.Text = "";
            lblKalanHak.Text = "";
            listBox1.Visible = false;
            label4.Visible = false;
            panel2.Visible = false;
          
        }

        private void btnBasla_Click(object sender, EventArgs e)
        {
            panel2.Visible=true;
            lblSonuc.Text = "Tahmininizi Giriniz!";
            
            tahmin_sayisi = 10;
            lblKalanHak.Text="kalan Hak:"+tahmin_sayisi;


            txtTahmin.Enabled=true;
            txtTahmin.Focus();
            

            txtTahmin.Clear();
            listBox1.Items.Clear();
            lblYazi.Text = "";
           
        }
    }
}
