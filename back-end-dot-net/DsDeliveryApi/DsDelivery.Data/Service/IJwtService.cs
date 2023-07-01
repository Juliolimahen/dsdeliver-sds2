using DsDelivery.Core.Domain;


namespace DsDelivery.Data.Service
{
    public interface IJwtService
    {
        string GerarToken(User user);
    }
}
