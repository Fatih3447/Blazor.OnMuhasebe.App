﻿using System.Linq;
using AbcYazilim.OnMuhasebe.BankaHesaplar;
using AbcYazilim.OnMuhasebe.Bankalar;
using AbcYazilim.OnMuhasebe.BankaSubeler;
using AbcYazilim.OnMuhasebe.Birimler;
using AbcYazilim.OnMuhasebe.Cariler;
using AbcYazilim.OnMuhasebe.Depolar;
using AbcYazilim.OnMuhasebe.Donemler;
using AbcYazilim.OnMuhasebe.FaturaHareketler;
using AbcYazilim.OnMuhasebe.Faturalar;
using AbcYazilim.OnMuhasebe.Hizmetler;
using AbcYazilim.OnMuhasebe.Kasalar;
using AbcYazilim.OnMuhasebe.MakbuzHareketler;
using AbcYazilim.OnMuhasebe.Makbuzlar;
using AbcYazilim.OnMuhasebe.Masraflar;
using AbcYazilim.OnMuhasebe.OdemeBelgeleri;
using AbcYazilim.OnMuhasebe.OzelKodlar;
using AbcYazilim.OnMuhasebe.Parametreler;
using AbcYazilim.OnMuhasebe.Stoklar;
using AbcYazilim.OnMuhasebe.Subeler;
using AutoMapper;

namespace AbcYazilim.OnMuhasebe
{
    public class OnMuhasebeApplicationAutoMapperProfile : Profile
    {
        public OnMuhasebeApplicationAutoMapperProfile()
        {
            //banka
            CreateMap<Banka, SelectBankaDto>()
                .ForMember(x => x.OzelKod1Adi, y => y.MapFrom(z => z.OzelKod1.Ad))
                .ForMember(x => x.OzelKod2Adi, y => y.MapFrom(z => z.OzelKod2.Ad));

            CreateMap<Banka, ListBankaDto>()
                .ForMember(x => x.OzelKod1Adi, y => y.MapFrom(z => z.OzelKod1.Ad))
                .ForMember(x => x.OzelKod2Adi, y => y.MapFrom(z => z.OzelKod2.Ad));

            CreateMap<CreateBankaDto, Banka>();
            CreateMap<UpdateBankaDto, Banka>();
            CreateMap<SelectBankaDto, CreateBankaDto>();
            CreateMap<SelectBankaDto, UpdateBankaDto>();

            //bankasube
            CreateMap<BankaSube, SelectBankaSubeDto>()
                .ForMember(x => x.BankaAdi, y => y.MapFrom(z => z.Banka.Ad))
                .ForMember(x => x.OzelKod1Adi, y => y.MapFrom(z => z.OzelKod1.Ad))
                .ForMember(x => x.OzelKod2Adi, y => y.MapFrom(z => z.OzelKod2.Ad));

            CreateMap<BankaSube, ListBankaSubeDto>()
                .ForMember(x => x.BankaAdi, y => y.MapFrom(z => z.Banka.Ad))
                .ForMember(x => x.OzelKod1Adi, y => y.MapFrom(z => z.OzelKod1.Ad))
                .ForMember(x => x.OzelKod2Adi, y => y.MapFrom(z => z.OzelKod2.Ad));

            CreateMap<CreateBankaSubeDto, BankaSube>();
            CreateMap<UpdateBankaSubeDto, BankaSube>();
            CreateMap<SelectBankaSubeDto, CreateBankaSubeDto>();
            CreateMap<SelectBankaSubeDto, UpdateBankaSubeDto>();

            //banka hesap
            CreateMap<BankaHesap, SelectBankaHesapDto>()
                .ForMember(x => x.BankaId, y => y.MapFrom(z => z.BankaSube.Banka.Id))
                .ForMember(x => x.BankaAdi, y => y.MapFrom(z => z.BankaSube.Banka.Ad))
                .ForMember(x => x.BankaSubeAdi, y => y.MapFrom(z => z.BankaSube.Ad))
                .ForMember(x => x.OzelKod1Adi, y => y.MapFrom(z => z.OzelKod1.Ad))
                .ForMember(x => x.OzelKod2Adi, y => y.MapFrom(z => z.OzelKod2.Ad));

            CreateMap<BankaHesap, ListBankaHesapDto>()
                .ForMember(x => x.BankaAdi, y => y.MapFrom(z => z.BankaSube.Banka.Ad))
                .ForMember(x => x.BankaSubeAdi, y => y.MapFrom(z => z.BankaSube.Ad))
                .ForMember(x => x.OzelKod1Adi, y => y.MapFrom(z => z.OzelKod1.Ad))
                .ForMember(x => x.OzelKod2Adi, y => y.MapFrom(z => z.OzelKod2.Ad));

            CreateMap<CreateBankaHesapDto, BankaHesap>();
            CreateMap<UpdateBankaHesapDto, BankaHesap>();
            CreateMap<SelectBankaHesapDto, CreateBankaHesapDto>();
            CreateMap<SelectBankaHesapDto, UpdateBankaHesapDto>();

            //birim
            CreateMap<Birim, SelectBirimDto>()
                .ForMember(x => x.OzelKod1Adi, y => y.MapFrom(z => z.OzelKod1.Ad))
                .ForMember(x => x.OzelKod2Adi, y => y.MapFrom(z => z.OzelKod2.Ad));

            CreateMap<Birim, ListBirimDto>()
                .ForMember(x => x.OzelKod1Adi, y => y.MapFrom(z => z.OzelKod1.Ad))
                .ForMember(x => x.OzelKod2Adi, y => y.MapFrom(z => z.OzelKod2.Ad));

            CreateMap<CreateBirimDto, Birim>();
            CreateMap<UpdateBirimDto, Birim>();
            CreateMap<SelectBirimDto, CreateBirimDto>();
            CreateMap<SelectBirimDto, UpdateBirimDto>();

            //cari
            CreateMap<Cari, SelectCariDto>()
                .ForMember(x => x.OzelKod1Adi, y => y.MapFrom(z => z.OzelKod1.Ad))
                .ForMember(x => x.OzelKod2Adi, y => y.MapFrom(z => z.OzelKod2.Ad));

            CreateMap<Cari, ListCariDto>()
                .ForMember(x => x.OzelKod1Adi, y => y.MapFrom(z => z.OzelKod1.Ad))
                .ForMember(x => x.OzelKod2Adi, y => y.MapFrom(z => z.OzelKod2.Ad));

            CreateMap<CreateCariDto, Cari>();
            CreateMap<UpdateCariDto, Cari>();
            CreateMap<SelectCariDto, CreateCariDto>();
            CreateMap<SelectCariDto, UpdateCariDto>();

            //depo
            CreateMap<Depo, SelectDepoDto>()
                .ForMember(x => x.OzelKod1Adi, y => y.MapFrom(z => z.OzelKod1.Ad))
                .ForMember(x => x.OzelKod2Adi, y => y.MapFrom(z => z.OzelKod2.Ad));

            CreateMap<Depo, ListDepoDto>()
                .ForMember(x => x.OzelKod1Adi, y => y.MapFrom(z => z.OzelKod1.Ad))
                .ForMember(x => x.OzelKod2Adi, y => y.MapFrom(z => z.OzelKod2.Ad))
                .ForMember(x => x.Giren, y => y.MapFrom(z => z.FaturaHareketler
                    .Where(x => x.Fatura.FaturaTuru == FaturaTuru.Alis).Sum(x => x.Miktar)))
                .ForMember(x => x.Cikan, y => y.MapFrom(z => z.FaturaHareketler
                    .Where(x => x.Fatura.FaturaTuru == FaturaTuru.Satis).Sum(x => x.Miktar)));

            CreateMap<CreateDepoDto, Depo>();
            CreateMap<UpdateDepoDto, Depo>();
            CreateMap<SelectDepoDto, CreateDepoDto>();
            CreateMap<SelectDepoDto, UpdateDepoDto>();

            //donem
            CreateMap<Donem, SelectDonemDto>();
            CreateMap<Donem, ListDonemDto>();
            CreateMap<CreateDonemDto, Donem>();
            CreateMap<UpdateDonemDto, Donem>();
            CreateMap<SelectDonemDto, CreateDonemDto>();
            CreateMap<SelectDonemDto, UpdateDonemDto>();


            //fatura
            CreateMap<Fatura, SelectFaturaDto>()
                .ForMember(x => x.CariAdi, y => y.MapFrom(z => z.Cari.Ad))
                .ForMember(x => x.VergiDairesi, y => y.MapFrom(z => z.Cari.VergiDairesi))
                .ForMember(x => x.VergiNo, y => y.MapFrom(z => z.Cari.VergiNo))
                .ForMember(x => x.Adres, y => y.MapFrom(z => z.Cari.Adres))
                .ForMember(x => x.Telefon, y => y.MapFrom(z => z.Cari.Telefon))
                .ForMember(x => x.OzelKod1Adi, y => y.MapFrom(z => z.OzelKod1.Ad))
                .ForMember(x => x.OzelKod2Adi, y => y.MapFrom(z => z.OzelKod2.Ad));

            CreateMap<Fatura, ListFaturaDto>()
                .ForMember(x => x.CariAdi, y => y.MapFrom(z => z.Cari.Ad))
                .ForMember(x => x.OzelKod1Adi, y => y.MapFrom(z => z.OzelKod1.Ad))
                .ForMember(x => x.OzelKod2Adi, y => y.MapFrom(z => z.OzelKod2.Ad));

            CreateMap<CreateFaturaDto, Fatura>();
            CreateMap<UpdateFaturaDto, Fatura>()
                .ForMember(x => x.FaturaHareketler, y => y.Ignore());
            CreateMap<SelectFaturaDto, CreateFaturaDto>();
            CreateMap<SelectFaturaDto, UpdateFaturaDto>();
            CreateMap<SelectFaturaDto, FaturaReportDto>();

            //Fatura Hareket
            CreateMap<FaturaHareket, SelectFaturaHareketDto>()
                .ForMember(x => x.StokKodu, y => y.MapFrom(z => z.Stok.Kod))
                .ForMember(x => x.StokAdi, y => y.MapFrom(z => z.Stok.Ad))
                .ForMember(x => x.HizmetKodu, y => y.MapFrom(z => z.Hizmet.Kod))
                .ForMember(x => x.HizmetAdi, y => y.MapFrom(z => z.Hizmet.Ad))
                .ForMember(x => x.MasrafKodu, y => y.MapFrom(z => z.Masraf.Kod))
                .ForMember(x => x.MasrafAdi, y => y.MapFrom(z => z.Masraf.Ad))
                .ForMember(x => x.DepoKodu, y => y.MapFrom(z => z.Depo.Kod))
                .ForMember(x => x.DepoAdi, y => y.MapFrom(z => z.Depo.Ad))
                .ForMember(x => x.BirimAdi, y => y.MapFrom(z => z.Stok != null ? z.Stok.Birim.Ad :
                    z.Hizmet != null ? z.Hizmet.Birim.Ad :
                    z.Masraf != null ? z.Masraf.Birim.Ad : null))
                .ForMember(x => x.HareketKodu, y => y.MapFrom(z => z.Stok != null ? z.Stok.Kod :
                    z.Hizmet != null ? z.Hizmet.Kod :
                    z.Masraf != null ? z.Masraf.Kod : null))
                .ForMember(x => x.HareketAdi, y => y.MapFrom(z => z.Stok != null ? z.Stok.Ad :
                    z.Hizmet != null ? z.Hizmet.Ad :
                    z.Masraf != null ? z.Masraf.Ad : null));

            CreateMap<FaturaHareketDto, FaturaHareket>();
            CreateMap<SelectFaturaHareketDto, FaturaHareketDto>();
            CreateMap<SelectFaturaHareketDto, SelectFaturaHareketDto>();
            CreateMap<SelectFaturaHareketDto, FaturaHareketReportDto>();

            CreateMap<FaturaHareket, ListCariHareketDto>()
                .ForMember(x => x.CariId, y => y.MapFrom(z => z.Fatura.CariId))
                .ForMember(x => x.Tarih, y => y.MapFrom(z => z.Fatura.Tarih))
                .ForMember(x => x.BelgeNo, y => y.MapFrom(z => z.Fatura.FaturaNo))
                .ForMember(x => x.FaturaTuru, y => y.MapFrom(z => z.Fatura.FaturaTuru))
                .ForMember(x => x.Tutar, y => y.MapFrom(z => z.NetTutar))
                .ForMember(x => x.Aciklama,
                    y => y.MapFrom(z => string.IsNullOrEmpty(z.Fatura.Aciklama) ? z.Aciklama : z.Fatura.Aciklama));
            
            CreateMap<FaturaHareket, ListStokHareketDto>()
                .ForMember(x => x.Tarih, y => y.MapFrom(z => z.Fatura.Tarih))
                .ForMember(x => x.BelgeNo, y => y.MapFrom(z => z.Fatura.FaturaNo))
                .ForMember(x => x.FaturaTuru, y => y.MapFrom(z => z.Fatura.FaturaTuru))
                .ForMember(x => x.Tutar, y => y.MapFrom(z => z.Fatura.NetTutar))
                .ForMember(x => x.Aciklama,
                    y => y.MapFrom(z => string.IsNullOrEmpty(z.Fatura.Aciklama) ? z.Aciklama : z.Fatura.Aciklama));
            
            CreateMap<FaturaHareket, ListHizmetHareketDto>()
                .ForMember(x => x.Tarih, y => y.MapFrom(z => z.Fatura.Tarih))
                .ForMember(x => x.BelgeNo, y => y.MapFrom(z => z.Fatura.FaturaNo))
                .ForMember(x => x.FaturaTuru, y => y.MapFrom(z => z.Fatura.FaturaTuru))
                .ForMember(x => x.Tutar, y => y.MapFrom(z => z.Fatura.NetTutar))
                .ForMember(x => x.Aciklama,
                    y => y.MapFrom(z => string.IsNullOrEmpty(z.Fatura.Aciklama) ? z.Aciklama : z.Fatura.Aciklama));
            
            CreateMap<FaturaHareket, ListMasrafHareketDto>()
                .ForMember(x => x.Tarih, y => y.MapFrom(z => z.Fatura.Tarih))
                .ForMember(x => x.BelgeNo, y => y.MapFrom(z => z.Fatura.FaturaNo))
                .ForMember(x => x.FaturaTuru, y => y.MapFrom(z => z.Fatura.FaturaTuru))
                .ForMember(x => x.Tutar, y => y.MapFrom(z => z.Fatura.NetTutar))
                .ForMember(x => x.Aciklama,
                    y => y.MapFrom(z => string.IsNullOrEmpty(z.Fatura.Aciklama) ? z.Aciklama : z.Fatura.Aciklama));
            
            //Hizmet
            CreateMap<Hizmet, SelectHizmetDto>()
                .ForMember(x => x.BirimAdi, y => y.MapFrom(z => z.Birim.Ad))
                .ForMember(x => x.OzelKod1Adi, y => y.MapFrom(z => z.OzelKod1.Ad))
                .ForMember(x => x.OzelKod2Adi, y => y.MapFrom(z => z.OzelKod2.Ad));

            CreateMap<Hizmet, ListHizmetDto>()
                .ForMember(x => x.BirimAdi, y => y.MapFrom(z => z.Birim.Ad))
                .ForMember(x => x.OzelKod1Adi, y => y.MapFrom(z => z.OzelKod1.Ad))
                .ForMember(x => x.OzelKod2Adi, y => y.MapFrom(z => z.OzelKod2.Ad))
                .ForMember(x => x.Giren, y => y.MapFrom(z => z.FaturaHareketler
                    .Where(x => x.Fatura.FaturaTuru == FaturaTuru.Alis).Sum(x => x.Miktar)))
                .ForMember(x => x.Cikan, y => y.MapFrom(z => z.FaturaHareketler
                    .Where(x => x.Fatura.FaturaTuru == FaturaTuru.Satis).Sum(x => x.Miktar)));

            CreateMap<CreateHizmetDto, Hizmet>();
            CreateMap<UpdateHizmetDto, Hizmet>();
            CreateMap<SelectHizmetDto, CreateHizmetDto>();
            CreateMap<SelectHizmetDto, UpdateHizmetDto>();

            //Kasa
            CreateMap<Kasa, SelectKasaDto>()
                .ForMember(x => x.OzelKod1Adi, y => y.MapFrom(z => z.OzelKod1.Ad))
                .ForMember(x => x.OzelKod2Adi, y => y.MapFrom(z => z.OzelKod2.Ad));

            CreateMap<Kasa, ListKasaDto>()
                .ForMember(x => x.OzelKod1Adi, y => y.MapFrom(z => z.OzelKod1.Ad))
                .ForMember(x => x.OzelKod2Adi, y => y.MapFrom(z => z.OzelKod2.Ad))
                .ForMember(x => x.Borc, y => y.MapFrom(z => z.MakbuzHareketler
                    .Where(x => x.BelgeDurumu == BelgeDurumu.TahsilEdildi).Sum(x => x.Tutar)))
                .ForMember(x => x.Alacak, y => y.MapFrom(z => z.MakbuzHareketler
                    .Where(x => x.BelgeDurumu == BelgeDurumu.Odendi).Sum(x => x.Tutar)));

            CreateMap<CreateKasaDto, Kasa>();
            CreateMap<UpdateKasaDto, Kasa>();
            CreateMap<SelectKasaDto, CreateKasaDto>();
            CreateMap<SelectKasaDto, UpdateKasaDto>();

            //Makbuz
            CreateMap<Makbuz, SelectMakbuzDto>()
                .ForMember(x => x.CariKodu, y => y.MapFrom(z => z.Cari.Kod))
                .ForMember(x => x.CariAdi, y => y.MapFrom(z => z.Cari.Ad))
                .ForMember(x => x.KasaAdi, y => y.MapFrom(z => z.Kasa.Ad))
                .ForMember(x => x.BankaHesapAdi, y => y.MapFrom(z => z.BankaHesap.Ad))
                .ForMember(x => x.OzelKod1Adi, y => y.MapFrom(z => z.OzelKod1.Ad))
                .ForMember(x => x.OzelKod2Adi, y => y.MapFrom(z => z.OzelKod2.Ad))
                .ForMember(x => x.SubeAdi, y => y.MapFrom(z => z.Sube.Ad));

            CreateMap<Makbuz, ListMakbuzDto>()
                .ForMember(x => x.CariAdi, y => y.MapFrom(z => z.Cari.Ad))
                .ForMember(x => x.KasaAdi, y => y.MapFrom(z => z.Kasa.Ad))
                .ForMember(x => x.BankaHesapAdi, y => y.MapFrom(z => z.BankaHesap.Ad))
                .ForMember(x => x.OzelKod1Adi, y => y.MapFrom(z => z.OzelKod1.Ad))
                .ForMember(x => x.OzelKod2Adi, y => y.MapFrom(z => z.OzelKod2.Ad));

            CreateMap<CreateMakbuzDto, Makbuz>();
            CreateMap<UpdateMakbuzDto, Makbuz>()
                .ForMember(x => x.MakbuzTuru, y => y.Ignore())
                .ForMember(x => x.MakbuzHareketler, y => y.Ignore());
            CreateMap<SelectMakbuzDto, CreateMakbuzDto>();
            CreateMap<SelectMakbuzDto, UpdateMakbuzDto>();
            CreateMap<SelectMakbuzDto, MakbuzReportDto>();

            //Makbuz Hareket
            CreateMap<MakbuzHareket, SelectMakbuzHareketDto>()
                .ForMember(x => x.CekBankaAdi, y => y.MapFrom(z => z.CekBanka.Ad))
                .ForMember(x => x.CekBankaSubeAdi, y => y.MapFrom(z => z.CekBankaSube.Ad))
                .ForMember(x => x.KasaAdi, y => y.MapFrom(z => z.Kasa.Ad))
                .ForMember(x => x.BankaHesapAdi, y => y.MapFrom(z => z.BankaHesap.Ad));

            CreateMap<MakbuzHareketDto, MakbuzHareket>();

            CreateMap<MakbuzHareket, ListOdemeBelgesiHareketDto>()
                .ForMember(x => x.MakbuzNo, y => y.MapFrom(z => z.Makbuz.MakbuzNo))
                .ForMember(x => x.Tarih, y => y.MapFrom(z => z.Makbuz.Tarih))
                .ForMember(x => x.MakbuzTuru, y => y.MapFrom(z => z.Makbuz.MakbuzTuru))
                .ForMember(x => x.Aciklama,
                    y => y.MapFrom(z => string.IsNullOrEmpty(z.Makbuz.Aciklama) ? z.Aciklama : z.Makbuz.Aciklama));

            CreateMap<MakbuzHareket, ListCariHareketDto>()
                .ForMember(x => x.CariId, y => y.MapFrom(z => z.Makbuz.CariId))
                .ForMember(x => x.Tarih, y => y.MapFrom(z => z.Makbuz.Tarih))
                .ForMember(x => x.BelgeNo, y => y.MapFrom(z => z.Makbuz.MakbuzNo))
                .ForMember(x => x.MakbuzTuru, y => y.MapFrom(z => z.Makbuz.MakbuzTuru))
                .ForMember(x => x.Aciklama,
                    y => y.MapFrom(z => string.IsNullOrEmpty(z.Makbuz.Aciklama) ? z.Aciklama : z.Makbuz.Aciklama));

            CreateMap<SelectMakbuzHareketDto, MakbuzHareketDto>();
            CreateMap<SelectMakbuzHareketDto, SelectMakbuzHareketDto>();
            CreateMap<SelectMakbuzHareketDto, MakbuzHareketReportDto>();

            //Masraf
            CreateMap<Masraf, SelectMasrafDto>()
                .ForMember(x => x.BirimAdi, y => y.MapFrom(z => z.Birim.Ad))
                .ForMember(x => x.OzelKod1Adi, y => y.MapFrom(z => z.OzelKod1.Ad))
                .ForMember(x => x.OzelKod2Adi, y => y.MapFrom(z => z.OzelKod2.Ad));

            CreateMap<Masraf, ListMasrafDto>()
                .ForMember(x => x.BirimAdi, y => y.MapFrom(z => z.Birim.Ad))
                .ForMember(x => x.OzelKod1Adi, y => y.MapFrom(z => z.OzelKod1.Ad))
                .ForMember(x => x.OzelKod2Adi, y => y.MapFrom(z => z.OzelKod2.Ad))
                .ForMember(x => x.Giren, y => y.MapFrom(z => z.FaturaHareketler
                    .Where(x => x.Fatura.FaturaTuru == FaturaTuru.Alis).Sum(x => x.Miktar)))
                .ForMember(x => x.Cikan, y => y.MapFrom(z => z.FaturaHareketler
                    .Where(x => x.Fatura.FaturaTuru == FaturaTuru.Satis).Sum(x => x.Miktar)));

            CreateMap<CreateMasrafDto, Masraf>();
            CreateMap<UpdateMasrafDto, Masraf>();
            CreateMap<SelectMasrafDto, CreateMasrafDto>();
            CreateMap<SelectMasrafDto, UpdateMasrafDto>();

            //Odeme Belgesi
            CreateMap<OdemeBelgesi, ListOdemeBelgesiDto>();
            CreateMap<ListOdemeBelgesiDto, SelectMakbuzHareketDto>();

            //OzelKod
            CreateMap<OzelKod, SelectOzelKodDto>();
            CreateMap<OzelKod, ListOzelKodDto>();
            CreateMap<CreateOzelKodDto, OzelKod>();
            CreateMap<UpdateOzelKodDto, OzelKod>();
            CreateMap<SelectOzelKodDto, CreateOzelKodDto>();
            CreateMap<SelectOzelKodDto, UpdateOzelKodDto>();

            //Firma Parametre
            CreateMap<FirmaParametre, SelectFirmaParametreDto>()
                .ForMember(x => x.SubeAdi, y => y.MapFrom(z => z.Sube.Ad))
                .ForMember(x => x.DonemAdi, y => y.MapFrom(z => z.Donem.Ad))
                .ForMember(x => x.DepoAdi, y => y.MapFrom(z => z.Depo.Ad));

            CreateMap<CreateFirmaParametreDto, FirmaParametre>();
            CreateMap<UpdateFirmaParametreDto, FirmaParametre>();

            //Stok
            CreateMap<Stok, SelectStokDto>()
                .ForMember(x => x.BirimAdi, y => y.MapFrom(z => z.Birim.Ad))
                .ForMember(x => x.OzelKod1Adi, y => y.MapFrom(z => z.OzelKod1.Ad))
                .ForMember(x => x.OzelKod2Adi, y => y.MapFrom(z => z.OzelKod2.Ad));

            CreateMap<Stok, ListStokDto>()
                .ForMember(x => x.BirimAdi, y => y.MapFrom(z => z.Birim.Ad))
                .ForMember(x => x.OzelKod1Adi, y => y.MapFrom(z => z.OzelKod1.Ad))
                .ForMember(x => x.OzelKod2Adi, y => y.MapFrom(z => z.OzelKod2.Ad))
                .ForMember(x => x.Giren, y => y.MapFrom(z => z.FaturaHareketler
                    .Where(x => x.Fatura.FaturaTuru == FaturaTuru.Alis).Sum(x => x.Miktar)))
                .ForMember(x => x.Cikan, y => y.MapFrom(z => z.FaturaHareketler
                    .Where(x => x.Fatura.FaturaTuru == FaturaTuru.Satis).Sum(x => x.Miktar)));

            CreateMap<CreateStokDto, Stok>();
            CreateMap<UpdateStokDto, Stok>();
            CreateMap<SelectStokDto, CreateStokDto>();
            CreateMap<SelectStokDto, UpdateStokDto>();

            //sube
            CreateMap<Sube, SelectSubeDto>();
            CreateMap<Sube, ListSubeDto>();
            CreateMap<CreateSubeDto, Sube>();
            CreateMap<UpdateSubeDto, Sube>();
            CreateMap<SelectSubeDto, CreateSubeDto>();
            CreateMap<SelectSubeDto, UpdateSubeDto>();
        }
    }
}