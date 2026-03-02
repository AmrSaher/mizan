namespace Mizan.Application.Primitives
{
    public interface IInquiryDTO<E, DTO>
    {
        public static abstract DTO FromEntity(E entity);
    }
}
