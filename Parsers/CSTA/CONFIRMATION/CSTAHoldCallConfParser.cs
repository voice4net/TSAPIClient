using System;
using NLog;
using TSAPIClient.CSTA;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class CSTAHoldCallConfParser : ICSTAConfirmationParser
    {
        public CSTAConfirmationEvent Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                object result;

                logger.Info("CSTAHoldCallConfParser.Parse: eventType=CSTA_HOLD_CALL_CONF");
                logger.Info("CSTAHoldCallConfParser.Parse: try to read the CSTAHoldCallConfEvent_t confirmation event...");

                if (reader.TryReadStruct(typeof(CSTAHoldCallConfEvent_t), out result))
                {
                    logger.Info("CSTAHoldCallConfParser.Parse: successfully read the CSTAHoldCallConfEvent_t confirmation event!");

                    CSTAHoldCallConfEvent_t holdCall = (CSTAHoldCallConfEvent_t)result;

                    CSTAConfirmationEvent cstaConfirmation = new CSTAConfirmationEvent { u = { holdCall = holdCall } };

                    return cstaConfirmation;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in CSTAHoldCallConfParser.Parse: {0}", err));
            }          

            return null;
        }

        public int eventType
        {
            get { return Constants.CSTA_HOLD_CALL_CONF; }
        }
    }
}
