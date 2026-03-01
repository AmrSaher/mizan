using Mizan.Domain.Primitives;

namespace Mizan.Domain.Entities.StockDomain
{
    // https://www.egx.com.eg/en/ContactPersonDirectory.aspx
    public sealed class StockIRContact : BaseEntity, IAggregateRoot
    {
        public string Name { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Email { get; private set; }

        public Guid StockId { get; private set; }
        public Stock Stock { get; }

        private StockIRContact() { }

        public StockIRContact(string name, string phoneNumber, string email, Guid stockId)
        {
            SetName(name);
            SetPhoneNumber(phoneNumber);
            SetEmail(email);
            SetStockId(stockId);
        }

        public void SetName(string name)
        {
            Name = name;
        }

        public void SetPhoneNumber(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
        }

        public void SetEmail(string email)
        {
            Email = email;
        }

        public void SetStockId(Guid stockId)
        {
            StockId = stockId;
        }
    }
}
