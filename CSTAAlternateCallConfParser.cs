using System;
using NLog;
using TSAPIClient.CSTA;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class CSTAAlternateCallConfParser : ICSTAConfirmationParser
    {
        public CSTAConfirmationEvent Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");
            try
            {
                object result;

                logger.Info("CSTAAlternateCallConfParser.Parse: eventType=CSTA_ALTERNATE_CALL_CONF");
                logger.Info("CSTAAlternateCallConfParser.Parse: try to read the CSTAAlternateCallConfEvent_t confirmation event...");

                if (reader.TryReadStruct(typeof(CSTAAlternateCallConfEvent_t), out result))
                {
                    logger.Info("CSTAAlternateCallConfParser.Parse: successfully read the CSTAAlternateCallConfEvent_t confirmation event!");

                    CSTAAlternateCallConfEvent_t alternateCall = (CSTAAlternateCallConfEvent_t)result;

                    CSTAConfirmationEvent cstaConfirmation = new CSTAConfirmationEvent
                    {
                        u = {alternateCall = alternateCall}
                    };

                    return cstaConfirmation;
                }

                return null;
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in CSTAAlternateCallConfParser.Parse: {0}", err));
            }

            return null;
        }

        public int eventType
        {
            get { return Constants.CSTA_ALTERNATE_CALL_CONF; }
        }
    }
}
