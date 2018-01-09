using System;
using NLog;
using TSAPIClient.CSTA;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class CSTADeflectCallConfParser : ICSTAConfirmationParser
    {
        public CSTAConfirmationEvent Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                object result;

                logger.Info("CSTADeflectCallConfParser.Parse: eventType=CSTA_DEFLECT_CALL_CONF");
                logger.Info("CSTADeflectCallConfParser.Parse: try to read the CSTADeflectCallConfEvent_t confirmation event...");

                if (reader.TryReadStruct(typeof(CSTADeflectCallConfEvent_t), out result))
                {
                    logger.Info("CSTADeflectCallConfParser.Parse: successfully read the CSTADeflectCallConfEvent_t confirmation event!");

                    CSTADeflectCallConfEvent_t deflectCall = (CSTADeflectCallConfEvent_t)result;

                    CSTAConfirmationEvent cstaConfirmation = new CSTAConfirmationEvent { u = { deflectCall = deflectCall } };

                    return cstaConfirmation;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in CSTADeflectCallConfParser.Parse: {0}", err));
            }          

            return null;
        }

        public int eventType
        {
            get { return Constants.CSTA_DEFLECT_CALL_CONF; }
        }
    }
}
