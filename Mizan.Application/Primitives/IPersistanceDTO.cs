namespace Mizan.Application.Primitives
{
    public interface IPersistanceDTO<E>
    {
        public E ToEntity();
    }
}
