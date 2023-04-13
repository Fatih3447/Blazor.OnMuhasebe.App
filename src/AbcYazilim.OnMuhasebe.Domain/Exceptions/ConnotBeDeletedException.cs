using Volo.Abp;

namespace AbcYazilim.OnMuhasebe.Exceptions;

public class ConnotBeDeletedException : BusinessException
{
    public ConnotBeDeletedException() : base(OnMuhasebeDomainErrorCodes.ConnotBeDeleted)
    {
    }
}