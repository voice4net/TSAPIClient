using System;
using NLog;
using TSAPIClient.CSTA;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class CSTAGetAPICapsConfParser : ICSTAConfirmationParser
    {
        public CSTAConfirmationEvent Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                object result;

                logger.Info("CSTAGetAPICapsConfParser.Parse: eventType=CSTA_GETAPI_CAPS_CONF");
                logger.Info("CSTAGetAPICapsConfParser.Parse: try to read the CSTAGetAPICapsConfEvent_t confirmation event...");

                if (reader.TryReadStruct(typeof(CSTAGetAPICapsConfEvent_t), out result))
                {
                    logger.Info("CSTAGetAPICapsConfParser.Parse: successfully read the CSTAGetAPICapsConfEvent_t confirmation event!");

                    CSTAGetAPICapsConfEvent_t getAPICaps = (CSTAGetAPICapsConfEvent_t)result;

                    CSTAConfirmationEvent cstaConfirmation = new CSTAConfirmationEvent { u = { getAPICaps = getAPICaps } };

                    return cstaConfirmation;
                }

                return null;
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in CSTAGetAPICapsConfParser.Parse: {0}", err));
            }

            return null;
        }

        public int eventType
        {
            get { return Constants.CSTA_GETAPI_CAPS_CONF; }
        }
    }
}
