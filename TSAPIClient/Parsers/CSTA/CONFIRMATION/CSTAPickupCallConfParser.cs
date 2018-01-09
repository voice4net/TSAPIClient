using System;
using NLog;
using TSAPIClient.CSTA;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class CSTAPickupCallConfParser : ICSTAConfirmationParser
    {
        public CSTAConfirmationEvent Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                object result;

                logger.Info("CSTAPickupCallConfParser.Parse: eventType=CSTA_PICKUP_CALL_CONF");
                logger.Info("CSTAPickupCallConfParser.Parse: try to read the CSTAPickupCallConfEvent_t confirmation event...");

                if (reader.TryReadStruct(typeof(CSTAPickupCallConfEvent_t), out result))
                {
                    logger.Info("CSTAPickupCallConfParser.Parse: successfully read the CSTAPickupCallConfEvent_t confirmation event!");

                    CSTAPickupCallConfEvent_t pickupCall = (CSTAPickupCallConfEvent_t)result;

                    CSTAConfirmationEvent cstaConfirmation = new CSTAConfirmationEvent { u = { pickupCall = pickupCall } };

                    return cstaConfirmation;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in CSTAPickupCallConfParser.Parse: {0}", err));
            }          

            return null;
        }

        public int eventType
        {
            get { return Constants.CSTA_PICKUP_CALL_CONF; }
        }
    }
}
