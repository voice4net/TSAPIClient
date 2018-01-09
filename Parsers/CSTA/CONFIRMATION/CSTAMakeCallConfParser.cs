using System;
using NLog;
using TSAPIClient.CSTA;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class CSTAMakeCallConfParser : ICSTAConfirmationParser
    {
        public CSTAConfirmationEvent Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                object result;

                logger.Info("CSTAMakeCallConfParser.Parse: eventType=CSTA_MAKE_CALL_CONF");
                logger.Info("CSTAMakeCallConfParser.Parse: try to read the CSTAMakeCallConfEvent_t confirmation event...");

                if (reader.TryReadStruct(typeof(CSTAMakeCallConfEvent_t), out result))
                {
                    logger.Info("CSTAMakeCallConfParser.Parse: successfully read the CSTAMakeCallConfEvent_t confirmation event!");

                    CSTAMakeCallConfEvent_t makeCall = (CSTAMakeCallConfEvent_t)result;

                    CSTAConfirmationEvent cstaConfirmation = new CSTAConfirmationEvent { u = { makeCall = makeCall } };

                    return cstaConfirmation;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in CSTAMakeCallConfParser.Parse: {0}", err));
            }          

            return null;
        }

        public int eventType
        {
            get { return Constants.CSTA_MAKE_CALL_CONF; }
        }
    }
}
