using System.ServiceModel;

namespace UMGI.GRCS.BusinessEntities.Interfaces
{
    [ServiceContract]
	public interface IRoutingService
	{
        [OperationContract]
        void ProcessNextRoutedItem();

        //[OperationContract(Name = "GetWorkGroupRequestTypeBySalesChannelType")]
        //List<Int16> GetWorkGroupRequestTypeBySalesChannelType(List<Int16> salesChannel);
        //[OperationContract(Name = "GetWorkGroupRequestTypeBySalesChannel")]
        //List<KeyValuePair<short, short>> GetWorkGroupRequestTypeBySalesChannel(List<Int16> salesChannel);
	}
}
