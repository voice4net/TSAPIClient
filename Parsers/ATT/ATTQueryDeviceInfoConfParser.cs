// ATTQueryDeviceInfoConfParser.cs

using System;
using NLog;
using TSAPIClient.ATT;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class ATTQueryDeviceInfoConfParser : IATTEventParser
    {
        public ATTEvent_t Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                logger.Info("ATTQueryDeviceInfoConfParser.Parse: eventType=ATT_QUERY_DEVICE_INFO_CONF");

                object result;

                if (reader.TryReadStruct(typeof(ATTQueryDeviceInfoConfEvent_t), out result))
                {
                    ATTQueryDeviceInfoConfEvent_t queryDeviceInfo = (ATTQueryDeviceInfoConfEvent_t)result;

                    ATTEvent_t attEvent = new ATTEvent_t() { eventType = (ushort)eventType };

                    attEvent.u.queryDeviceInfo = queryDeviceInfo;

                    return attEvent;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in ATTQueryDeviceInfoConfParser.Parse: {0}", err));
            }

            return null;
        }

        public int eventType
        {
            get { return Constants.ATT_QUERY_DEVICE_INFO_CONF; }
        }
    }
}
