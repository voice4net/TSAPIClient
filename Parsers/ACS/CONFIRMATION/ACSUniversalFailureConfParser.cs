using System;
using NLog;
using TSAPIClient.CSTA;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class ACSUniversalFailureConfParser: IACSConfirmationParser
    {
        public ACSConfirmationEvent Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                logger.Info("ACSUniversalFailureConfParser.Parse: eventType=ACS_UNIVERSAL_FAILURE_CONF");
                logger.Info("ACSUniversalFailureConfParser.Parse: try to read the ACSUniversalFailureConfEvent_t confirmation event...");

                object result;

                if (reader.TryReadStruct(typeof(ACSUniversalFailureConfEvent_t), out result))
                {
                    logger.Info("ACSUniversalFailureConfParser.Parse: successfully read the ACSUniversalFailureConfEvent_t confirmation event!");

                    ACSUniversalFailureConfEvent_t failureEvent = (ACSUniversalFailureConfEvent_t)result;

                    logger.Info("ACSUniversalFailureConfParser.Parse: failureEvent={0}", failureEvent.error);

                    ACSConfirmationEvent acsConfirmation = new ACSConfirmationEvent();

                    acsConfirmation.u.failureEvent = failureEvent;

                    return acsConfirmation;
                }

                return null;
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in ACSUniversalFailureConfParser.Parse: {0}", err));
            }

            return null;
        }

        public int eventType
        {
            get { return Constants.ACS_UNIVERSAL_FAILURE_CONF; }
        }
    }
}
