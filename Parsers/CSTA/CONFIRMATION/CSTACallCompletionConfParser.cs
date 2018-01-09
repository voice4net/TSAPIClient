using System;
using NLog;
using TSAPIClient.CSTA;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class CSTACallCompletionConfParser : ICSTAConfirmationParser
    {
        public CSTAConfirmationEvent Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                object result;
                
                logger.Info("CSTACallCompletionConfParser.Parse: eventType=CSTA_CALL_COMPLETION_CONF");
                logger.Info("CSTACallCompletionConfParser.Parse: try to read the CSTACallCompletionConfEvent_t confirmation event...");

                if (reader.TryReadStruct(typeof(CSTACallCompletionConfEvent_t), out result))
                {
                    logger.Info("CSTACallCompletionConfParser.Parse: successfully read the CSTACallCompletionConfEvent_t confirmation event!");

                    CSTACallCompletionConfEvent_t callCompletion = (CSTACallCompletionConfEvent_t)result;

                    CSTAConfirmationEvent cstaConfirmation = new CSTAConfirmationEvent
                    {
                        u = {callCompletion = callCompletion}
                    };

                    return cstaConfirmation;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in CSTACallCompletionConfParser.Parse: {0}", err));
            }          

            return null;
        }

        public int eventType
        {
            get { return Constants.CSTA_CALL_COMPLETION_CONF; }
        }
    }
}
