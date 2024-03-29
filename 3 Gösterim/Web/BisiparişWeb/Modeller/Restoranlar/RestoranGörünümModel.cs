﻿using BisiparişÇekirdek.Valıklar.Erzak;
using BisiparişÇekirdek.Valıklar.VeriGünlüğü;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BisiparişWeb.Modeller.Restoranlar
{
    public class RestoranGörünümModel : Restoran
    {
        public RestoranGörünümModel(Restoran rst)
        {
            Id = rst.Id; İsim = rst.İsim; Tür = rst.Tür; İletişimId = rst.İletişimId;

            RestoranTürler = new Dictionary<RestoranTürler, string>()
            {
                { BisiparişÇekirdek.Valıklar.Erzak.RestoranTürler.CafeVeİçecek, "Cafe ve İçecek" },
                { BisiparişÇekirdek.Valıklar.Erzak.RestoranTürler.Kahvaltı, "Kahvaltı" },
                { BisiparişÇekirdek.Valıklar.Erzak.RestoranTürler.Lokanta, "Lokanta" },
                { BisiparişÇekirdek.Valıklar.Erzak.RestoranTürler.LüksYemek, "Lüks Yemek" },
                { BisiparişÇekirdek.Valıklar.Erzak.RestoranTürler.Pastaneler, "Pastaneler" },
                { BisiparişÇekirdek.Valıklar.Erzak.RestoranTürler.RomantikMekanlar, "Romantik Mekanlar" },
                { BisiparişÇekirdek.Valıklar.Erzak.RestoranTürler.SokakLezzetleri, "Sokak Lezzetleri" },
                { BisiparişÇekirdek.Valıklar.Erzak.RestoranTürler.Yemek, "Yemek" },
                { BisiparişÇekirdek.Valıklar.Erzak.RestoranTürler.YeVeKalk, "Ye ve Kalk" },
                { BisiparişÇekirdek.Valıklar.Erzak.RestoranTürler.Tatlıcı, "Tatlıcı" },
            };
        }

        private static Dictionary<RestoranTürler, string> RestoranTürler { get; set; }
        public string DizTür { get; set; }
        public string DizHizmetler { get; set; }
        public string İlAd { get; set; }
        public string İlçeAd { get; set; }
        public string SemtAd { get; set; }
        public string MahalleAd { get; set; }
        public byte[] Fotoğraf { get; set; }
        public string ResimKaynak { get; set; }

        public async Task VerilerDoldur()
        {
            try
            {
                var dizHizmetlerSB = new StringBuilder("");

                await BisiparişWebYardımcı.GünlükKaydetme(OlaySeviye.Uyarı, "Into Restaurant Veri Doldur...");

                DizTür = RestoranTürler[Tür];

                if (Hizmetler != RestoranHizmetler.Hiçbiri)
                {
                    foreach (var birHizmet in Enum.GetValues(typeof(RestoranHizmetler)))
                    {
                        var enmHizmet = (RestoranHizmetler)Enum.Parse(typeof(RestoranHizmetler), birHizmet.ToString());

                        if ((Hizmetler & enmHizmet) == enmHizmet)
                            dizHizmetlerSB.Append($"{Yardımcılar.RestoranlarYardımcı.RestoranHizmetleri[enmHizmet]} |");
                    }

                    //TODO: Remove the last extra separator
                }

                DizHizmetler = dizHizmetlerSB.ToString();

                await BisiparişWebYardımcı.GünlükKaydetme(OlaySeviye.Uyarı, $"Hizmetler: {Hizmetler} -- Diz: {DizHizmetler}");

                //await BisiparişWebYardımcı.GünlükKaydetme(OlaySeviye.Uyarı, "Getting restaurant comm...");

                İletişim = await BisiparişWebYardımcı.İşyeriİletişimAl(İletişimId);

                //İlAd = İletişim.Adres; İlAd = İletişim.Adres.İlId;

                //await BisiparişWebYardımcı.GünlükKaydetme(OlaySeviye.Uyarı, "Getting restaurant photos...");

                var fotolr = await Yardımcılar.RestoranlarYardımcı.RestoranFotoğraflarAl(Id);

                if (fotolr != null && fotolr.Any())
                {
                    Fotoğraflar = fotolr.Select(f => f.Fotoğraf).ToList();

                    Fotoğraf = Fotoğraflar.First();

                    //await BisiparişWebYardımcı.GünlükKaydetme(OlaySeviye.Uyarı, "Converting restaurant photo...");

                    ResimKaynak = $"data:image/png;base64,{Convert.ToBase64String(Fotoğraf)}";
                }

                //await BisiparişWebYardımcı.GünlükKaydetme(OlaySeviye.Uyarı, "Got all requirements");

                //Menüler
                //ÇalışmaZamanlamalar
            }
            catch (Exception ex)
            {
                await BisiparişWebYardımcı.GünlükKaydetme(OlaySeviye.Uyarı, $"Restaurant Veri Doldur Exp: {ex.Message}");
                throw ex;
            }
        }
    }
}
