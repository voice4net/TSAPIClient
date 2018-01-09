using System;
using NLog;
using TSAPIClient.CSTA;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class CSTAEscapeSvcConfParser : ICSTAConfirmationParser
    {
        public CSTAConfirmationEvent Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                object result;

                logger.Info("CSTAEscapeSvcConfParser.Parse: eventType=CSTA_ESCAPE_SVC_CONF");
                logger.Info("CSTAEscapeSvcConfParser.Parse: try to read the CSTAEscapeSvcConfEvent_t confirmation event...");

                if (reader.TryReadStruct(typeof(CSTAEscapeSvcConfEvent_t), out result))
                {
                    logger.Info("CSTAEscapeSvcConfParser.Parse: successfully read the CSTAEscapeSvcConfEvent_t confirmation event!");

                    CSTAEscapeSvcConfEvent_t escapeService = (CSTAEscapeSvcConfEvent_t)result;

                    CSTAConfirmationEvent cstaConfirmation = new CSTAConfirmationEvent
                    {
                        u = {escapeService = escapeService}
                    };

                    return cstaConfirmation;
                }

                return null;
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in CSTAEscapeSvcConfParser.Parse: {0}", err));
            }

            return null;
        }

        public int eventType
        {
            get { return Constants.CSTA_ESCAPE_SVC_CONF; }
        }
    }
}
