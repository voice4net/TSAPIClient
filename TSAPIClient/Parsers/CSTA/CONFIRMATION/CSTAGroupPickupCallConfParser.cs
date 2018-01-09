using System;
using NLog;
using TSAPIClient.CSTA;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class CSTAGroupPickupCallConfParser : ICSTAConfirmationParser
    {
        public CSTAConfirmationEvent Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                object result;

                logger.Info("CSTAGroupPickupCallConfParser.Parse: eventType=CSTA_GROUP_PICKUP_CALL_CONF");
                logger.Info("CSTAGroupPickupCallConfParser.Parse: try to read the CSTAGroupPickupCallConfEvent_t confirmation event...");

                if (reader.TryReadStruct(typeof(CSTAGroupPickupCallConfEvent_t), out result))
                {
                    logger.Info("CSTAGroupPickupCallConfParser.Parse: successfully read the CSTAGroupPickupCallConfEvent_t confirmation event!");

                    CSTAGroupPickupCallConfEvent_t groupPickupCall = (CSTAGroupPickupCallConfEvent_t)result;

                    CSTAConfirmationEvent cstaConfirmation = new CSTAConfirmationEvent
                    {
                        u = {groupPickupCall = groupPickupCall}
                    };

                    return cstaConfirmation;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in CSTAGroupPickupCallConfParser.Parse: {0}", err));
            }          

            return null;
        }

        public int eventType
        {
            get { return Constants.CSTA_GROUP_PICKUP_CALL_CONF; }
        }
    }
}
