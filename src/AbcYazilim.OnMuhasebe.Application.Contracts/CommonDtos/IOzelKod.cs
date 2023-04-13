using System;

namespace AbcYazilim.OnMuhasebe.CommonDtos;

public interface IOzelKod
{
    public Guid? OzelKod1Id { get; set; }
    public string OzelKod1Adi { get; set; }
    public Guid? OzelKod2Id { get; set; }
    public string OzelKod2Adi { get; set; }
}