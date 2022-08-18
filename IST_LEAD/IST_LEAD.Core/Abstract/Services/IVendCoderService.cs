namespace IST_LEAD.Core.Abstract.Services;

public interface IVendCoderService
{
    public TProduct setVendCode<TProduct>(TProduct product, string VendCodeFieldName, int newVendNum);
    

}