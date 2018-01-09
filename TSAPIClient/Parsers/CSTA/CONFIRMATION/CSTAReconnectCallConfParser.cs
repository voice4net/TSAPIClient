using System;
using NLog;
using TSAPIClient.CSTA;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class CSTAReconnectCallConfParser : ICSTAConfirmationParser
    {
        public CSTAConfirmationEvent Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                object result;

                logger.Info("CSTAReconnectCallConfParser.Parse: eventType=CSTA_RECONNECT_CALL_CONF");
                logger.Info("CSTAReconnectCallConfParser.Parse: try to read the CSTAReconnectCallConfEvent_t confirmation event...");

                if (reader.TryReadStruct(typeof(CSTAReconnectCallConfEvent_t), out result))
                {
                    logger.Info("CSTAReconnectCallConfParser.Parse: successfully read the CSTAReconnectCallConfEvent_t confirmation event!");

                    CSTAReconnectCallConfEvent_t reconnectCall = (CSTAReconnectCallConfEvent_t)result;

                    CSTAConfirmationEvent cstaConfirmation = new CSTAConfirmationEvent
                    {
                        u = {reconnectCall = reconnectCall}
                    };

                    return cstaConfirmation;
                }

                return null;
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in CSTAReconnectCallConfParser.Parse: {0}", err));
            }

            return null;
        }


        public int eventType
        {
            get { return Constants.CSTA_RECONNECT_CALL_CONF; }
        }
    }
}
