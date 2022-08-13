using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AtikToplamaOyunu
{
    public partial class AnaForm : Form
    {
        private int Puan = 0;
        private int sayac = 60;
        private int gonder = 0;
        Random rastgele;
        private int rastgeleSayi = 0;
        public event EventHandler AtikEkle;
        public event EventHandler KutuBosalt;

        private OrganikAtikKutusu _organikAtikKutusu;
        private KagitAtikKutusu _kagitAtikKutusu;
        private CamAtikKutusu _camAtikKutusu;
        private MetalAtikKutusu _metalAtikKutusu;

        private Atik _camSise;
        private Atik _bardak;
        private Atik _gazete;
        private Atik _dergi;
        private Atik _domates;
        private Atik _salatalik;
        private Atik _kolaKutusu;
        private Atik _salcaKutusu;

        Atik[] atiklar;
        ListBox[] listboxlar;
        IAtikKutusu[] atikKutulari;
        Button[] butonlar;
        ProgressBar[] progressbarlar;

        public AnaForm()
        {
            InitializeComponent();
            AtikEkle += Atik_Ekle;
            KutuBosalt += Kutu_Bosalt;

            _organikAtikKutusu = new OrganikAtikKutusu();
            _kagitAtikKutusu = new KagitAtikKutusu();
            _camAtikKutusu = new CamAtikKutusu();
            _metalAtikKutusu = new MetalAtikKutusu();

            _camSise = new Atik("Cam Şişe", 600, Image.FromFile("camsise.png"));
            _bardak = new Atik("Bardak", 250, Image.FromFile("bardak.png"));
            _gazete = new Atik("Gazete", 250, Image.FromFile("gazete.png"));
            _dergi = new Atik("Dergi", 200, Image.FromFile("dergi.png"));
            _domates = new Atik("Domates", 150, Image.FromFile("domates.png"));
            _salatalik = new Atik("Salatalık", 120, Image.FromFile("salatalik.png"));
            _kolaKutusu = new Atik("Kola Kutusu", 350, Image.FromFile("kolakutusu.png"));
            _salcaKutusu = new Atik("Salça Kutusu", 550,Image.FromFile("salcakutusu.png"));

            atiklar = new Atik[] { _camSise, _bardak, _gazete, _dergi, _domates, _salatalik, _kolaKutusu, _salcaKutusu };
            listboxlar = new ListBox[] { lbCam, lbCam, lbKagit, lbKagit, lbOrganikAtik, lbOrganikAtik, lbMetal, lbMetal };
            atikKutulari = new IAtikKutusu[] { _camAtikKutusu, _camAtikKutusu, _kagitAtikKutusu, _kagitAtikKutusu, _organikAtikKutusu, _organikAtikKutusu, _metalAtikKutusu, _metalAtikKutusu };
            butonlar = new Button[] { btnCamBosalt, btnKagitBosalt, btnMetalBosalt, btnOAtikBosalt, btnCam, btnKagit, btnMetal, btnOrganikAtik };
            progressbarlar = new ProgressBar[] { prbCam, prbCam, prbKagit, prbKagit, prbOrganikAtik, prbOrganikAtik, prbMetal, prbMetal };
            rastgele = new Random();
        }
        public void ResimDegistir()
        {
            rastgeleSayi = rastgele.Next(0, 8);
            pbRastgeleResim.Image = atiklar[rastgeleSayi].Image;
        }
        public void Atik_Ekle(object sender, EventArgs e)
        {
            //Atik Kutusu sınıfından true degeri gelirse atigi listboxa yazdirir Puanı hesaplar ve resmi degistirir.
            if (atikKutulari[rastgeleSayi].Ekle(atiklar[rastgeleSayi]) == true)
            {
                listboxlar[rastgeleSayi].Items.Add(atiklar[rastgeleSayi].Ad + " (" + atiklar[rastgeleSayi].Hacim + ")");
                PuanHesapla(atiklar[rastgeleSayi].Hacim);
                progressbarlar[rastgeleSayi].Value = atikKutulari[rastgeleSayi].DolulukOrani;
                ResimDegistir();
            }
        }
        public void Kutu_Bosalt(object sender, EventArgs e)
        {
            //Atık Kutusu sınıfında ture degeri gelirse puanı hesaplar sayac 3 saniye ekler listboxı temizler.
            if (atikKutulari[gonder].Bosalt() == true)
            {
                PuanHesapla(atikKutulari[gonder].BosaltmaPuani);
                sayac += 3;
                listboxlar[gonder].Items.Clear();
                progressbarlar[gonder].Value = atikKutulari[gonder].DolulukOrani;
            }
        }
        public string PuanHesapla(int _puan)
        {
            return lblPuan.Text = (Puan += _puan).ToString();
        }
        private void BtnYeniOyun_Click(object sender, EventArgs e)
        {
            BtnYeniOyun.Enabled = false;
            //Butonları kullanılabilir yapar.
            foreach (Button item in butonlar)
            {
                item.Enabled = true;
            }
            //listboxları temizler kullanılabilir yapar.
            foreach (ListBox item in listboxlar)
            {
                item.Items.Clear();
                item.Enabled = true;
            }
            //progressbarlarin degerini sifirlar.
            foreach (ProgressBar item in progressbarlar)
            {
                item.Value = 0;
            }
            ResimDegistir();
            timer1.Start();
            Puan = 0;
            lblPuan.Text = 0.ToString();
        }
        private void btnOrganikAtik_Click(object sender, EventArgs e)
        {
            //pictureboxtaki resim domates veya salataliksa kutuya ekler
            if (pbRastgeleResim.Image == _domates.Image || pbRastgeleResim.Image == _salatalik.Image)
            {
                AtikEkle?.Invoke(this, new EventArgs());
            }
        }
        private void btnOAtikBosalt_Click(object sender, EventArgs e)
        {
            gonder = 4;
            KutuBosalt?.Invoke(this, new EventArgs());  
        }
        private void btnKagit_Click(object sender, EventArgs e)
        {
            //pictureboxtaki resim gazete veya dergiyse kutuya ekler
            if (pbRastgeleResim.Image == (_gazete.Image) || pbRastgeleResim.Image == _dergi.Image)
            {
                AtikEkle?.Invoke(this, new EventArgs());
            }
        }
        private void btnKagitBosalt_Click(object sender, EventArgs e)
        {
            gonder = 2;
            KutuBosalt?.Invoke(this, new EventArgs());
        }
        private void btnCam_Click(object sender, EventArgs e)
        {
            //pictureboxtaki resim domates veya salataliksa kutuya ekler
            if (pbRastgeleResim.Image == _camSise.Image || pbRastgeleResim.Image == _bardak.Image)
            {
                AtikEkle?.Invoke(this, new EventArgs());
            }
        }
        private void btnCamBosalt_Click(object sender, EventArgs e)
        {
            gonder = 0;
            KutuBosalt?.Invoke(this, new EventArgs());
        }
        private void btnMetal_Click(object sender, EventArgs e)
        {
            //pictureboxtaki resim domates veya salataliksa kutuya ekler
            if (pbRastgeleResim.Image == _kolaKutusu.Image || pbRastgeleResim.Image ==_salcaKutusu.Image)
            {
                AtikEkle?.Invoke(this, new EventArgs());
            }
        }
        private void btnMetalBosalt_Click(object sender, EventArgs e)
        {
            gonder = 6;
            KutuBosalt?.Invoke(this, new EventArgs());
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            sayac--;
            lblSure.Text = sayac.ToString();
            //sayac 0 olursa oyunu durdurur.
            if (sayac == 0)
            {
                timer1.Stop();
                BtnYeniOyun.Enabled = true;
                pbRastgeleResim.Image = null;
                //butonları kullanılmaz yapar.
                foreach (Button item in butonlar)
                {
                    item.Enabled = false;
                }
                //listboxları kullanılmaz yapar.
                foreach (ListBox item in listboxlar)
                {
                    item.Enabled = false;
                }
                _organikAtikKutusu.organikAtiklar.Clear();
                _kagitAtikKutusu.kagitAtiklar.Clear();
                _camAtikKutusu.camAtiklar.Clear();
                _metalAtikKutusu.metalAtiklar.Clear();
                sayac = 60;
            }
        }
        private void btnCikis_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}
