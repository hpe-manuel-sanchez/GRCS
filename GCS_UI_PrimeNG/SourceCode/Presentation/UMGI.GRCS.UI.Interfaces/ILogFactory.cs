using UMGI.GRCS.Core.Utilities.logger;
namespace UMGI.GRCS.UI.Interfaces
{
    public interface ILogFactory
    {
        ILog LogWriter { get; set; }
        
    }
}
