namespace LocationExplorer.Service.Interfaces.Destination
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IDestinationService
    {
        Task<int> AddAsync(string name, int regionId, IEnumerable<int> tags);
    }
}
