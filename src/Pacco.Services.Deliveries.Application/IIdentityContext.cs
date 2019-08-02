using System.Collections.Generic;

namespace Pacco.Services.Deliveries.Application
{
    public interface IIdentityContext
    {
        string Id { get; }
        string Role { get; }
        bool IsAuthenticated { get; }
        IDictionary<string, string> Claims { get; }
    }
}