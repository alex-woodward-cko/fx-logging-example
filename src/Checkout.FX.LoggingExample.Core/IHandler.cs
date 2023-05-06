using System.Threading;
using System.Threading.Tasks;

namespace Checkout.FX.LoggingExample.Core
{
    /// <summary>
    /// Reserve Entry point
    /// </summary>
    public interface IHandler
    {
        /// <summary>
        /// 
        /// </summary>
        Task HandleAsync(CancellationToken cancellationToken);
    }
}
