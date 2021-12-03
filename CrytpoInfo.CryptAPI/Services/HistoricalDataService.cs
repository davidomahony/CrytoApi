using CrytpoInfo.Core.Repositories;
using CrytpoInfo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrytpoInfo.CryptAPI.Services
{
    public class HistoricalDataService
    {
        private readonly IHistoricalDataRepository<HistoricalDataResults> repository;


        public HistoricalDataService(IHistoricalDataRepository<HistoricalDataResults> repository)
        {
            this.repository = repository;
        }

        public HistoricalDataResults AcquireHistoricalData(HistoricalDataRequest requestInformation)
        {
            var historicalDataResult = this.repository.AcquireHistoricalData(requestInformation);

            if (historicalDataResult is null)
            {
                // will decide later how to handle
            }

            return historicalDataResult;
        }
    }
}
