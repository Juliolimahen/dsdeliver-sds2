using DsDelivery.Core.Domain;


namespace DsDelivery.Data.Service
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}
