/* ***************************************************************************
 * Copyrights ® 2013 HCL Technologies Limited
 * ***************************************************************************
 * File Name    : CacheObjectSizer.cs
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Jelio Halleys J
 * Created on   : 24/06/2013
 * ***************************************************************************
 * Modification History
 * ***************************************************************************
 * Modified by       Modified on     Remarks
 * 
 *************************************************************************** 
 * Reviewed by       Modified on     Remarks 

 ****************************************************************************
 * Description  : Finds the Size of each object that is pushed into Cache 

 ****************************************************************************/

using System;
using System.Xml;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;


namespace GRCS.Utilities.Caching
{
    internal static class CacheObjectSizer
    {
        internal static long Size(object objToSize)
        {
            long length;

            try
            {
                MemoryStream stream1 = new MemoryStream();

                //AppFabric uses NetDataContractSerializer 
                using (XmlDictionaryWriter writer = XmlDictionaryWriter.CreateBinaryWriter(stream1))
                {
                    NetDataContractSerializer serializer = new NetDataContractSerializer();
                    serializer.WriteObject(writer, objToSize);
                    length = stream1.Length;
                }

                if (length == 0)
                {
                    MemoryStream stream2 = new MemoryStream();
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(stream2, objToSize);
                    length = stream2.Length;
                }
            }
            catch (Exception)
            {
                return 0;
            }
            return length;

        }

    }
}
