﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BisaprişÇekirdek.Valıklar.Esansiyel
{
    public class İcraSonuç
    {
        #region Data Members (Veri Üyeler)
        #endregion

        #region Constructors (Yapıcılar)
        #endregion

        #region Properties (Özellikler)
        public bool BaşarılıMı { get; set; }
        public string Mesaj { get; set; }
        public int YeniEklediId { get; set; }
        public static İcraSonuç Başarılı { get; } = new İcraSonuç() { BaşarılıMı = true }; 
        #endregion

        #region Methods (Metotlar) (Yöntemler)
        #endregion
    }
}
