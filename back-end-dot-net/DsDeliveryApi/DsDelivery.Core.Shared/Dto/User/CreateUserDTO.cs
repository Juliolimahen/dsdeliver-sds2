namespace DsDelivery.Core.Shared.Dto.User;

public class CreateUserDTO
{
    public string Login { get; set; }
    public string Password { get; set; }

    public ICollection<ReferencePosition> Positions { get; set; }
}
