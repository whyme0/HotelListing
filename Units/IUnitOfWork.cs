using HotelListing.Repositories;
using HotelListing.Data;

namespace HotelListing.Units
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Country> Countries { get; }
        IGenericRepository<Hotel> Hotels { get; }

        Task Save();
    }
}
