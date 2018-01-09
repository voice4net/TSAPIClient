using System;
using NLog;
using TSAPIClient.CSTA;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class ACSUniversalFailureParser : IACSUnsolicitedParser
    {
        public ACSUnsolicitedEvent Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                logger.Info("ACSUniversalFailureParser.Parse: eventType=ACS_UNIVERSAL_FAILURE");
                logger.Info("ACSUniversalFailureParser.Parse: try to read the ACSUniversalFailureEvent_t unsolicited event...");

                object result;

                if (reader.TryReadStruct(typeof(ACSUniversalFailureEvent_t), out result))
                {
                    logger.Info("ACSUniversalFailureParser.Parse: successfully read the ACSUniversalFailureEvent_t unsolicited event!");

                    ACSUniversalFailureEvent_t failureEvent = (ACSUniversalFailureEvent_t)result;

                    logger.Info("ACSUniversalFailureParser.Parse: failureEvent={0}", failureEvent.error);

                    ACSUnsolicitedEvent acsUnsolicited = new ACSUnsolicitedEvent { failureEvent = failureEvent };

                    return acsUnsolicited;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in ACSUniversalFailureParser.Parse: {0}", err));
            }

            return null;
        }

        public int eventType
        {
            get { return Constants.ACS_UNIVERSAL_FAILURE; }
        }
    }
}
