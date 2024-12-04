namespace BananaGame.Domain.Primitives
{
    public class BaseEntity : BaseAuditableEntity
    {
        public Guid Id { get; protected set; }

        public BaseEntity()
        {
            Id = Guid.NewGuid();
        }
    }
}
