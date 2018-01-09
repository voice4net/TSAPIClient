using System;
using NLog;
using TSAPIClient.CSTA;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class CSTAClearCallConfParser : ICSTAConfirmationParser
    {
        public CSTAConfirmationEvent Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                object result;

                logger.Info("CSTAClearCallConfParser.Parse: eventType=CSTA_CLEAR_CALL_CONF");
                logger.Info("CSTAClearCallConfParser.Parse: try to read the CSTAClearCallConfEvent_t confirmation event...");

                if (reader.TryReadStruct(typeof(CSTAClearCallConfEvent_t), out result))
                {
                    logger.Info("CSTAClearCallConfParser.Parse: successfully read the CSTAClearCallConfEvent_t confirmation event!");

                    CSTAClearCallConfEvent_t clearCall = (CSTAClearCallConfEvent_t)result;

                    CSTAConfirmationEvent cstaConfirmation = new CSTAConfirmationEvent { u = { clearCall = clearCall } };

                    return cstaConfirmation;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in CSTAClearCallConfParser.Parse: {0}", err));
            }          

            return null;
        }

        public int eventType
        {
            get { return Constants.CSTA_CLEAR_CALL_CONF; }
        }
    }
}
