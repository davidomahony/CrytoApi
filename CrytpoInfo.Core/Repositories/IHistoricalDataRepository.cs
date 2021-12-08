using CrytpoInfo.Models;

namespace CrytpoInfo.Core.Repositories
{
    public interface IHistoricalDataRepository<T>
    {
        public T AcquireHistoricalData(HistoricalDataRequestInternal information);
    }
}
