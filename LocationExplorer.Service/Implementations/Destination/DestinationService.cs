namespace LocationExplorer.Service.Implementations.Destination
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Interfaces.Destination;

    public class DestinationService : IDestinationService
    {
        public Task<int> AddAsync(string name, int regionId, IEnumerable<int> tags)
        {
            throw new System.NotImplementedException();
        }
    }
}
