﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using BisiparişÇekirdek.Valıklar.Esansiyel;
using System.Linq;
using System.Threading.Tasks;
using BisiparişÇekirdek.Valıklar.Erzak;
using BisiparişÇekirdek.Valıklar.VeriGünlüğü;
using BisiparişÇekirdek.Valıklar.Güvenlik;

namespace BisiparişVeriAltYapı
{
    public class MenülerVeriYardımcı
    {
        #region Data Members (Veri Üyeler)
        #endregion

        #region Constructors (Oluşturucular) (Yapıcılar)
        #endregion

        #region Properties (Özellikler)
        #endregion

        #region Methods (Metotlar) (Yöntemler)
        public static async Task<Menü> MenüAl(int menüId)
        {
            try
            {
                using (var vtBğlm = new BisiparişVeriBağlam() { BağlantıDizesi = BisiparişVeriYardımcı.BağlantıDizesi })
                {
                    return await vtBğlm.Menüler.FirstAsync(m => m.Id == menüId);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static async Task<İcraSonuç> YeniMenüEkle(Menü yeniMenü)
        {
            try
            {
                using (var vtBğlm = new BisiparişVeriBağlam() { BağlantıDizesi = BisiparişVeriYardımcı.BağlantıDizesi })
                {
                    var aynaMnu = await vtBğlm.Menüler.FirstOrDefaultAsync(mn =>
                                                mn.Ad.Equals(yeniMenü.Ad, StringComparison.OrdinalIgnoreCase));

                    if (aynaMnu == null)
                    {
                        var mnEkledi = await vtBğlm.Menüler.AddAsync(yeniMenü); await vtBğlm.SaveChangesAsync();

                        if (mnEkledi != null && mnEkledi.Entity.Id > 0)
                        {
                            foreach (var mnuÖğ in yeniMenü.MenüÖğeler)
                            {
                                mnuÖğ.MenüId = yeniMenü.Id; await vtBğlm.MenülerÖğeler.AddAsync(mnuÖğ);
                            }

                            await vtBğlm.SaveChangesAsync();

                            return new İcraSonuç() { BaşarılıMı = true, YeniEklediId = yeniMenü.Id };
                        }
                        else
                            return İcraSonuç.BaşarıSız;
                    }
                    else
                        return new İcraSonuç() { BaşarılıMı = false, Mesaj = "Bu restoran zaten var." };
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static async Task<İcraSonuç> MenüDeğiştir(Menü menü)
        {
            try
            {
                using (var vtBğlm = new BisiparişVeriBağlam() { BağlantıDizesi = BisiparişVeriYardımcı.BağlantıDizesi })
                {
                    var öncekiMnu = await vtBğlm.Menüler.FirstAsync(mn => mn.Id == menü.Id);

                    //öncekiRstr.ÇalışmaSaatleri
                    öncekiMnu.AktifMi = menü.AktifMi;

                    await vtBğlm.SaveChangesAsync();

                    return İcraSonuç.Başarılı;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static async Task MenülerKaydetme(BisiparişVeriBağlam vtBğlm, int yerId, List<Menü> menüler)
        {
            try
            {
                if (menüler != null && menüler.Any())
                {
                    foreach (var mn in menüler)
                    {
                        mn.YerId = yerId;

                        var mnEkldi = await vtBğlm.Menüler.AddAsync(mn);

                        if (mnEkldi != null && mnEkldi.Entity.Id > 0 && mn.MenüÖğeler != null && mn.MenüÖğeler.Any())
                            foreach (var mnuÖğ in mn.MenüÖğeler)
                            {
                                mnuÖğ.MenüId = mn.Id;

                                await vtBğlm.MenülerÖğeler.AddAsync(mnuÖğ);
                            }
                    }

                    await vtBğlm.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion
    }
}
