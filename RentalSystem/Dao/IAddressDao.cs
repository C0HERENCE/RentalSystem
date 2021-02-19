using RentalSystem.Models;

namespace RentalSystem.Dao
{
    public interface IAddressDao
    {
        AddressModel GetAddressByUserId(int id);
    }
}