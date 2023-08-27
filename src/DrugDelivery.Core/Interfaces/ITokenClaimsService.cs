using System.Threading.Tasks;

namespace DrugDelivery.Core.Interfaces;

public interface ITokenClaimsService
{
    Task<string> GetTokenAsync(string userName);
}
