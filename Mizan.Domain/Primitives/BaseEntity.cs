namespace Mizan.Domain.Primitives
{
    public abstract class BaseEntity
    {
        public Guid Id { get; protected set; }

        protected BaseEntity(Guid id)
        {
            Id = id;
        }

        protected BaseEntity() { }
    }
}
