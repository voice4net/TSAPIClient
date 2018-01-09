// ATTQueryDeviceNameConfParser.cs
using System;
using NLog;
using TSAPIClient.ATT;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class ATTQueryDeviceNameConfParser : IATTEventParser
    {
        public ATTEvent_t Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                logger.Info("ATTQueryDeviceNameConfParser.Parse: eventType=ATT_QUERY_DEVICE_NAME_CONF");

                object result;

                if (reader.TryReadStruct(typeof(ATTQueryDeviceNameConfEvent_t), out result))
                {
                    ATTQueryDeviceNameConfEvent_t queryDeviceName = (ATTQueryDeviceNameConfEvent_t)result;

                    ATTEvent_t attEvent = new ATTEvent_t() { eventType = (ushort)eventType };

                    attEvent.u.queryDeviceName = queryDeviceName;

                    return attEvent;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in ATTQueryDeviceNameConfParser.Parse: {0}", err));
            }

            return null;
        }

        public int eventType
        {
            get { return Constants.ATT_QUERY_DEVICE_NAME_CONF; }
        }
    }
}
