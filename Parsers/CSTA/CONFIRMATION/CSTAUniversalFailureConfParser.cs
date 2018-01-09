using System;
using NLog;
using TSAPIClient.CSTA;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class CSTAUniversalFailureConfParser : ICSTAConfirmationParser
    {
        public CSTAConfirmationEvent Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                object result;

                logger.Info("CSTAUniversalFailureConfParser.Parse: eventType=CSTA_UNIVERSAL_FAILURE_CONF");
                logger.Info("CSTAUniversalFailureConfParser.Parse: try to read the CSTAUniversalFailureConfEvent_t confirmation event...");

                if (reader.TryReadStruct(typeof(CSTAUniversalFailureConfEvent_t), out result))
                {
                    logger.Info("CSTAUniversalFailureConfParser.Parse: successfully read the CSTAUniversalFailureConfEvent_t confirmation event!");

                    CSTAUniversalFailureConfEvent_t universalFailure = (CSTAUniversalFailureConfEvent_t)result;

                    logger.Info("CSTAUniversalFailureConfParser.Parse: universalFailure={0}", universalFailure.error);

                    CSTAConfirmationEvent cstaConfirmation = new CSTAConfirmationEvent
                    {
                        u = {universalFailure = universalFailure}
                    };

                    return cstaConfirmation;
                }

                return null;
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in CSTAUniversalFailureConfParser.Parse: {0}", err));
            }

            return null;
        }

        public int eventType
        {
            get { return Constants.CSTA_UNIVERSAL_FAILURE_CONF; }
        }
    }
}
