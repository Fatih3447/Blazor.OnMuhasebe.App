﻿using System;
using System.Linq;
using AbcYazilim.Blazor.Core.Models;
using AbcYazilim.OnMuhasebe.Blazor.Helpers;
using AbcYazilim.OnMuhasebe.Blazor.Services.Base;
using AbcYazilim.OnMuhasebe.MakbuzHareketler;
using AbcYazilim.OnMuhasebe.Makbuzlar;
using Volo.Abp.DependencyInjection;
using static AbcYazilim.Blazor.Core.Helpers.Functions;

namespace AbcYazilim.OnMuhasebe.Blazor.Services;

public class MakbuzHareketService : BaseHareketService<SelectMakbuzHareketDto>, IScopedDependency
{
    public AppService AppService { get; set; } //Property injection
    public MakbuzService MakbuzService { get; set; } //Property injection

    public override void BeforeUpdate()
    {
        if (MakbuzService.MakbuzTuru == MakbuzTuru.Tahsilat ||
            (SelectedItem.BelgeDurumu != BelgeDurumu.CiroEdildi &&
             SelectedItem.BelgeDurumu != BelgeDurumu.TahsilEdildi))
        {
            DataSource = SelectedItem;
            EditPageVisible = true;
        }
    }

    public override void BeforeInsert()
    {
        DataSource = new SelectMakbuzHareketDto
        {
            OdemeTuru = OdemeTuru.Cek, Vade = DateTime.Now.Date, TakipNo = CreateId(),
        };

        EditPageVisible = true;
    }

    public void MakbuzHareketTuruSelectedItemChanged(ComboBoxEnumItem<OdemeTuru> selectedItem, Action hasChanged)
    {
        TempDataSource.OdemeTuru = selectedItem.Value;
        hasChanged();

        TempDataSource.CekBankaId = null;
        TempDataSource.CekBankaAdi = null;
        TempDataSource.CekBankaSubeId = null;
        TempDataSource.CekBankaSubeAdi = null;
        TempDataSource.CekHesapNo = null;
        TempDataSource.BelgeNo = null;
        TempDataSource.AsilBorclu = null;
        TempDataSource.Ciranta = null;
        TempDataSource.KasaId = null;
        TempDataSource.KasaAdi = null;
        TempDataSource.BankaHesapId = null;
        TempDataSource.BankaHesapAdi = null;
    }

    public override void OnSubmit()
    {
        var validator = new SelectMakbuzHareketDtoValidator(L);
        var result = validator.Validate(TempDataSource);

        if (result.IsValid)
        {
            DataSource = TempDataSource;
            DataSource.OdemeTuruAdi = L[$"Enum:OdemeTuru:{(byte)DataSource.OdemeTuru}"];
            DataSource.BelgeDurumu = MakbuzService.MakbuzTuru == MakbuzTuru.Tahsilat &&
                                     (DataSource.OdemeTuru == OdemeTuru.Nakit ||
                                      DataSource.OdemeTuru == OdemeTuru.Banka)
                ? BelgeDurumu.TahsilEdildi
                : MakbuzService.MakbuzTuru == MakbuzTuru.Tahsilat && (DataSource.OdemeTuru == OdemeTuru.Senet ||
                                                                      DataSource.OdemeTuru == OdemeTuru.Cek ||
                                                                      DataSource.OdemeTuru == OdemeTuru.Pos)
                    ? BelgeDurumu.Portfoyde
                    : MakbuzService.MakbuzTuru == MakbuzTuru.Odeme && (DataSource.OdemeTuru == OdemeTuru.Nakit ||
                                                                       DataSource.OdemeTuru == OdemeTuru.Banka)
                        ? BelgeDurumu.Odendi
                        : MakbuzService.MakbuzTuru == MakbuzTuru.Odeme && (DataSource.OdemeTuru == OdemeTuru.Senet ||
                                                                           DataSource.OdemeTuru == OdemeTuru.Cek ||
                                                                           DataSource.OdemeTuru == OdemeTuru.Pos)
                            ? BelgeDurumu.Odenecek
                            : BelgeDurumu.CiroEdildi;
            DataSource.BelgeDurumuAdi = L[$"Enum:BelgeDurumu:{(byte)DataSource.BelgeDurumu}"];
            DataSource.KendiBelgemiz = DataSource.BelgeDurumu == BelgeDurumu.Odenecek;
            InsertOrUpdate();
            HasChanged();
        }
        else
            MessageService.Error(result.Errors.CreateValidationErrorMessage(L));
    }

    public override void GetTotal()
    {
        MakbuzService.DataSource.CekToplam = ListDataSource.Where(x => x.OdemeTuru == OdemeTuru.Cek)
            .Sum(x => x.Tutar);
        MakbuzService.DataSource.SenetToplam = ListDataSource.Where(x => x.OdemeTuru == OdemeTuru.Senet)
            .Sum(x => x.Tutar);
        MakbuzService.DataSource.NakitToplam = ListDataSource.Where(x => x.OdemeTuru == OdemeTuru.Nakit)
            .Sum(x => x.Tutar);
        MakbuzService.DataSource.BankaToplam = ListDataSource.Where(x => x.OdemeTuru == OdemeTuru.Banka)
            .Sum(x => x.Tutar);
        MakbuzService.DataSource.PosToplam = ListDataSource.Where(x => x.OdemeTuru == OdemeTuru.Pos)
            .Sum(x => x.Tutar);
        MakbuzService.DataSource.HareketSayisi = ListDataSource.Count;
        HasChanged();
    }
}