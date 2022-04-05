namespace TicketApp.Base
{
    public abstract class Entity<TEntity, TId> where TId : struct where TEntity : Entity<TEntity, TId>
    {
        public TId Id { get; set; }

        public override bool Equals(object obj)
        {
            TEntity comparedEntity = obj as TEntity;

            if (ReferenceEquals(this, comparedEntity))
                return true;

            if (comparedEntity is null)
                return false;

            return Id.Equals(comparedEntity.Id);
        }

        public static bool operator ==(Entity<TEntity, TId> a, Entity<TEntity, TId> b)
        {
            if (a is null && b is null)
                return true;

            if (a is null || b is null)
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(Entity<TEntity, TId> a, Entity<TEntity, TId> b) => !(a == b);
        public override int GetHashCode() => (GetType().GetHashCode() * 907) + Id.GetHashCode();
        public override string ToString() => GetType().Name + "[Id=" + Id + "]";
    }
}
