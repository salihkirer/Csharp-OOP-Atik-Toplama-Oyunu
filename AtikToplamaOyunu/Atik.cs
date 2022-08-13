using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtikToplamaOyunu
{
    class Atik : IAtik
    {
        public string Ad { get; }
        public int Hacim { get; }
        public Image Image { get; }
        public Atik(string _ad, int _hacim, Image _image)
        {
            Ad = _ad;
            Hacim = _hacim;
            Image = _image;
        }
    }
}
