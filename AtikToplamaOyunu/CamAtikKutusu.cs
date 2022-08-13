using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtikToplamaOyunu
{
    class CamAtikKutusu : IAtikKutusu
    {
        public List<Atik> camAtiklar = new List<Atik>();
        private int _kapasite = 2200;
        public int BosaltmaPuani
        {
            get => 600;
        }
        public int Kapasite
        {
            get => _kapasite;
            set { _kapasite = value; }
        }
        public int DoluHacim
        {
            get
            {
                int _hacim = 0;
                //Cam Atiklar listesindeki her atiğin hacmini topluyor.
                foreach (var item in camAtiklar)
                {
                    _hacim += item.Hacim;
                }
                return _hacim;
            }
        }
        public int DolulukOrani
        {
            get => 100 * DoluHacim / Kapasite;
        }
        public bool Ekle(Atik atik)
        {
            //Atik kutusunda yer varsa atigi listeye ekliyor true degeri donduruyor.
            if (Kapasite - DoluHacim >= atik.Hacim)
            {
                camAtiklar.Add(atik);
                return true;
            }
            else
                return false;
        }
        public bool Bosalt()
        {
            //Atik kutusunun doluluk orani %75 den fazla ise listeyi temizliyor true degeri donduruyor.
            if (DolulukOrani >= 75)
            {
                camAtiklar.Clear();
                return true;
            }
            else
                return false;
        }
    }
}
