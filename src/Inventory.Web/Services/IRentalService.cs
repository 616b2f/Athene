namespace Athene.Inventory.Web.Services
{
    public interface IRentalService
    {
        void RentBook(int studentId, int bookItemId);
    }
}
