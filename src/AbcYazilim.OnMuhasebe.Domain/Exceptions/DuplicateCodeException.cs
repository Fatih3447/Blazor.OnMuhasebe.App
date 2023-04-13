using Volo.Abp;

namespace AbcYazilim.OnMuhasebe.Exceptions;

public class DuplicateCodeException : BusinessException
{
    public DuplicateCodeException(string kod) : base(OnMuhasebeDomainErrorCodes.DuplicateKod)
    {
        WithData("kod", kod);
    }
}