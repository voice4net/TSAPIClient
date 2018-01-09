using System;
using NLog;
using TSAPIClient.CSTA;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class CSTARetrieveCallConfParser : ICSTAConfirmationParser
    {
        public CSTAConfirmationEvent Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                object result;

                logger.Info("CSTARetrieveCallConfParser.Parse: eventType=CSTA_RETRIEVE_CALL_CONF");
                logger.Info("CSTARetrieveCallConfParser.Parse: try to read the CSTARetrieveCallConfEvent_t confirmation event...");

                if (reader.TryReadStruct(typeof(CSTARetrieveCallConfEvent_t), out result))
                {
                    logger.Info("CSTARetrieveCallConfParser.Parse: successfully read the CSTARetrieveCallConfEvent_t confirmation event!");

                    CSTARetrieveCallConfEvent_t retrieveCall = (CSTARetrieveCallConfEvent_t)result;

                    CSTAConfirmationEvent cstaConfirmation = new CSTAConfirmationEvent
                    {
                        u = {retrieveCall = retrieveCall}
                    };

                    return cstaConfirmation;
                }

                return null;
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in CSTARetrieveCallConfParser.Parse: {0}", err));
            }

            return null;
        }

        public int eventType
        {
            get { return Constants.CSTA_RETRIEVE_CALL_CONF; }
        }
    }
}
