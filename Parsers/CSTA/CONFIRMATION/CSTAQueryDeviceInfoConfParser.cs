using System;
using NLog;
using TSAPIClient.CSTA;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class CSTAQueryDeviceInfoConfParser : ICSTAConfirmationParser
    {
        public CSTAConfirmationEvent Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                object result;

                logger.Info("CSTAQueryDeviceInfoConfParser.Parse: eventType=CSTA_QUERY_DEVICE_INFO_CONF");
                logger.Info("CSTAQueryDeviceInfoConfParser.Parse: try to read the CSTAQueryDeviceInfoConfEvent_t confirmation event...");

                if (reader.TryReadStruct(typeof(CSTAQueryDeviceInfoConfEvent_t), out result))
                {
                    logger.Info("CSTAQueryDeviceInfoConfParser.Parse: successfully read the CSTAQueryDeviceInfoConfEvent_t confirmation event!");

                    CSTAQueryDeviceInfoConfEvent_t queryDeviceInfo = (CSTAQueryDeviceInfoConfEvent_t)result;

                    logger.Info("CSTAQueryDeviceInfoConfParser.Parse: deviceType={0}", queryDeviceInfo.deviceType);

                    CSTAConfirmationEvent cstaConfirmation = new CSTAConfirmationEvent
                    {
                        u = { queryDeviceInfo = queryDeviceInfo }
                    };

                    return cstaConfirmation;
                }

                return null;
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in CSTAQueryDeviceInfoConfParser.Parse: {0}", err));
            }

            return null;
        }

        public int eventType
        {
            get { return Constants.CSTA_QUERY_DEVICE_INFO_CONF; }
        }
    }
}
