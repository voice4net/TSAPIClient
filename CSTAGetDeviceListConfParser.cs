using System;
using NLog;
using TSAPIClient.CSTA;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class CSTAGetDeviceListConfParser : ICSTAConfirmationParser
    {
        public CSTAConfirmationEvent Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                logger.Info("CSTAGetDeviceListConfParser.Parse: eventType=CSTA_GET_DEVICE_LIST_CONF");
                logger.Info("CSTAGetDeviceListConfParser.Parse: try to read the CSTAGetDeviceListConfEvent_t confirmation event...");

                object result;

                if (reader.TryReadStruct(typeof(CSTAGetDeviceListConfEvent_t), out result))
                {
                    logger.Info("CSTAGetDeviceListConfParser.Parse: successfully read the CSTAGetDeviceListConfEvent_t confirmation event!");

                    CSTAGetDeviceListConfEvent_t getDeviceList = (CSTAGetDeviceListConfEvent_t)result;

                    logger.Info("CSTAGetDeviceListConfParser.Parse: driverSdbLevel={0};level={1};index={2};devList={3};count={4};", getDeviceList.driverSdbLevel, getDeviceList.level, getDeviceList.index, getDeviceList.devList, getDeviceList.devList.count);

                    CSTAConfirmationEvent cstaConfirmation = new CSTAConfirmationEvent
                    {
                        u = { getDeviceList = getDeviceList }
                    };

                    return cstaConfirmation;
                }

                return null;
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in CSTAGetDeviceListConfParser.Parse: {0}", err));
            }

            return null;
        }

        public int eventType
        {
            get { return Constants.CSTA_GET_DEVICE_LIST_CONF; }
        }
    }
}
