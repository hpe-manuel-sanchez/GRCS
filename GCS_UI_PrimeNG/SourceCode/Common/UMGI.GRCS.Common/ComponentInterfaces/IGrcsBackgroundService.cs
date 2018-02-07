namespace UMGI.GRCS.Common.ComponentInterfaces
{
    public interface IGrcsBackgroundService
    {
        void ProcessAllPendingItems();

        void Start();

        void Stop();
    }
}
