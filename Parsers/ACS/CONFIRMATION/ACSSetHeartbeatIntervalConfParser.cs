using System;
using NLog;
using TSAPIClient.CSTA;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class ACSSetHeartbeatIntervalConfParser: IACSConfirmationParser
    {
        public ACSConfirmationEvent Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                logger.Info("ACSSetHeartbeatIntervalConfParser.Parse: eventType=ACS_SET_HEARTBEAT_INTERVAL_CONF");
                logger.Info("ACSSetHeartbeatIntervalConfParser.Parse: try to read the ACSSetHeartbeatIntervalConfEvent_t confirmation event...");

                object result;

                if (reader.TryReadStruct(typeof(ACSSetHeartbeatIntervalConfEvent_t), out result))
                {
                    logger.Info("ACSSetHeartbeatIntervalConfParser.Parse: successfully read the ACSSetHeartbeatIntervalConfEvent_t confirmation event!");

                    ACSSetHeartbeatIntervalConfEvent_t acssetheartbeatinterval = (ACSSetHeartbeatIntervalConfEvent_t)result;

                    ACSConfirmationEvent acsConfirmation = new ACSConfirmationEvent();

                    acsConfirmation.u.acssetheartbeatinterval = acssetheartbeatinterval;

                    return acsConfirmation;
                }

                return null;
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in ACSSetHeartbeatIntervalConfParser.Parse: {0}", err));
            }

            return null;
        }

        public int eventType
        {
            get { return Constants.ACS_SET_HEARTBEAT_INTERVAL_CONF; }
        }
    }
}
