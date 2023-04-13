using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AbcYazilim.OnMuhasebe.BankaHesaplar;
using AbcYazilim.OnMuhasebe.MakbuzHareketler;
using AbcYazilim.OnMuhasebe.Makbuzlar;
using AbcYazilim.OnMuhasebe.OdemeBelgeleri;

namespace AbcYazilim.OnMuhasebe.Raporlar;

public class FinansalDurumAppService : OnMuhasebeAppService, IFinansalDurumAppService
{
    private readonly IMakbuzHareketRepository _makbuzHareketRepository;
    private readonly IGirenCikanBakiyeRepository _girenCikanBakiyeRepository;
    private readonly IOdemeBelgeleriDagilimRepository _odemeBelgeriDagilimRepository;
    private readonly IOdemeBelgesiRepository _odemeBelgesiRepository;

    public FinansalDurumAppService(IMakbuzHareketRepository makbuzHareketRepository,
        IGirenCikanBakiyeRepository girenCikanBakiyeRepository,
        IOdemeBelgeleriDagilimRepository odemeBelgeriDagilimRepository,
        IOdemeBelgesiRepository odemeBelgesiRepository)
    {
        _makbuzHareketRepository = makbuzHareketRepository;
        _girenCikanBakiyeRepository = girenCikanBakiyeRepository;
        _odemeBelgeriDagilimRepository = odemeBelgeriDagilimRepository;
        _odemeBelgesiRepository = odemeBelgesiRepository;
    }

    public virtual async Task<IList<FinansalHareketDto>> KasaSonOnHareketListAsync(
        MakbuzHareketListParameterDto input)
    {
        var hareketler = await _makbuzHareketRepository.GetPagedLastListAsync(0, 10,
            x => x.Makbuz.SubeId == input.SubeId && x.Makbuz.DonemId == input.DonemId && x.KasaId != null,
            x => x.Id,
            x => x.Makbuz);

        var finansalHareketler = new List<FinansalHareketDto>();

        hareketler.ForEach(x =>
        {
            finansalHareketler.Add(new FinansalHareketDto
            {
                MakbuzNo = x.Makbuz.MakbuzNo,
                Tarih = x.Makbuz.Tarih,

                Borc = x.Makbuz.MakbuzTuru == MakbuzTuru.Tahsilat ||
                x.Makbuz.MakbuzTuru == MakbuzTuru.KasaIslem && !x.KendiBelgemiz ? x.Tutar : 0,

                Alacak = x.Makbuz.MakbuzTuru == MakbuzTuru.Odeme ||
                x.Makbuz.MakbuzTuru == MakbuzTuru.KasaIslem && x.KendiBelgemiz ? x.Tutar : 0,

                Aciklama = x.Aciklama
            });
        });

        return finansalHareketler;
    }

    public virtual async Task<IList<FinansalHareketDto>> BankaSonOnHareketListAsync(
        MakbuzHareketListParameterDto input)
    {
        var hareketler = await _makbuzHareketRepository.GetPagedLastListAsync(0, 10,
            x => x.Makbuz.SubeId == input.SubeId && x.Makbuz.DonemId == input.DonemId &&
            x.BankaHesapId != null && x.BankaHesap.HesapTuru == BankaHesapTuru.VadesizMevduatHesabi,
            x => x.Id,
            x => x.Makbuz, x => x.BankaHesap);

        var finansalHareketler = new List<FinansalHareketDto>();

        hareketler.ForEach(x =>
        {
            finansalHareketler.Add(new FinansalHareketDto
            {
                MakbuzNo = x.Makbuz.MakbuzNo,
                Tarih = x.Makbuz.Tarih,

                Borc = x.Makbuz.MakbuzTuru == MakbuzTuru.Tahsilat ||
                x.Makbuz.MakbuzTuru == MakbuzTuru.BankaIslem && !x.KendiBelgemiz ? x.Tutar : 0,

                Alacak = x.Makbuz.MakbuzTuru == MakbuzTuru.Odeme ||
                x.Makbuz.MakbuzTuru == MakbuzTuru.BankaIslem && x.KendiBelgemiz ? x.Tutar : 0,

                Aciklama = x.Aciklama
            });
        });

        return finansalHareketler;
    }

    public virtual async Task<IList<FinansalDurumDto>> KasaDurumListAsync(MakbuzHareketListParameterDto input)
    {
        var kasaDurum = await _girenCikanBakiyeRepository
            .FromSqlRawSingleAsync($"KasaDurum @SubeId='{input.SubeId}', @DonemId='{input.DonemId}'");

        var list = new List<FinansalDurumDto>
        {
            new FinansalDurumDto
            {
                Aciklama = L["Entered"],
                Tutar = kasaDurum.Giren
            },

            new FinansalDurumDto
            {
                Aciklama = L["Out"],
                Tutar = kasaDurum.Cikan
            },

            new FinansalDurumDto
            {
                Aciklama = L["Balance"],
                Tutar = kasaDurum.Bakiye
            }
        };

        return list;
    }

    public virtual async Task<IList<FinansalDurumDto>> BankaDurumListAsync(MakbuzHareketListParameterDto input)
    {
        var bankaDurum = await _girenCikanBakiyeRepository
            .FromSqlRawSingleAsync($"BankaDurum @SubeId='{input.SubeId}', @DonemId='{input.DonemId}'");

        var list = new List<FinansalDurumDto>
        {
            new FinansalDurumDto
            {
                Aciklama = L["Entered"],
                Tutar = bankaDurum.Giren
            },

            new FinansalDurumDto
            {
                Aciklama = L["Out"],
                Tutar = bankaDurum.Cikan
            },

            new FinansalDurumDto
            {
                Aciklama = L["Balance"],
                Tutar = bankaDurum.Bakiye
            }
        };

        return list;
    }

    public virtual async Task<IList<FinansalDurumDto>> OdemeBelgeleriDagilimListAsync(
        MakbuzHareketListParameterDto input)
    {
        var dagilim = await _odemeBelgeriDagilimRepository
            .FromSqlRawAsync($"OdemeBelgeleriDagilim @SubeId='{input.SubeId}', @DonemId='{input.DonemId}'");

        var list = new List<FinansalDurumDto>();

        if (dagilim.Count == 0)
        {
            dagilim.Add(new OdemeBelgeleriDagilim { OdemeTuru = OdemeTuru.Cek, Tutar = 0 });
            dagilim.Add(new OdemeBelgeleriDagilim { OdemeTuru = OdemeTuru.Senet, Tutar = 0 });
            dagilim.Add(new OdemeBelgeleriDagilim { OdemeTuru = OdemeTuru.Nakit, Tutar = 0 });
            dagilim.Add(new OdemeBelgeleriDagilim { OdemeTuru = OdemeTuru.Banka, Tutar = 0 });
            dagilim.Add(new OdemeBelgeleriDagilim { OdemeTuru = OdemeTuru.Pos, Tutar = 0 });
        }

        foreach (var item in dagilim)
        {
            list.Add(new FinansalDurumDto
            {
                Aciklama = L[$"Enum:OdemeTuru:{(byte)item.OdemeTuru}"],
                Tutar = item.Tutar
            });
        }

        return list;
    }

    public virtual async Task<IList<OdemeBelgesiDto>> GecikenAlacaklarListAsync(
        MakbuzHareketListParameterDto input)
    {
        var odemeTurleri = $"{(byte)OdemeTuru.Cek}, {(byte)OdemeTuru.Senet}, {(byte)OdemeTuru.Pos}";

        var odemeBelgeleri = await _odemeBelgesiRepository
            .FromSqlRawAsync($"GecikenAlacaklar @SubeId='{input.SubeId}', @DonemId='{input.DonemId}', " +
            $"@OdemeTurleri='{odemeTurleri}', @Tarih='{DateTime.Now.Date:yyyy.MM.dd}'");

        var list = new List<OdemeBelgesiDto>();

        foreach (var item in odemeBelgeleri)
        {
            list.Add(new OdemeBelgesiDto
            {
                OdemeTuruAdi = L[$"Enum:OdemeTuru:{(byte)item.OdemeTuru}"],
                Vade = item.Vade,
                Tutar = item.Tutar,
                Aciklama = item.Aciklama
            });
        }

        return list;
    }
}