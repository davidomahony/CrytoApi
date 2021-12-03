using CrytpoInfo.Models;
using System;

namespace CrytpoInfo.Core.Repositories
{
    public interface IHistoricalDataRepository<T>
    {
        public T AcquireHistoricalData(HistoricalDataRequest information);
    }
}
