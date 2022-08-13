using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtikToplamaOyunu
{
    class MetalAtikKutusu : IAtikKutusu
    {
        public List<Atik> metalAtiklar = new List<Atik>();
        private int _kapasite = 2300;
        public int BosaltmaPuani
        {
            get => 800;
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
                //Metal Atiklar listesindeki her atiğin hacmini topluyor.
                foreach (var item in metalAtiklar)
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
                metalAtiklar.Add(atik);
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
                metalAtiklar.Clear();
                return true;
            }
            else
                return false;
        }
    }
}
