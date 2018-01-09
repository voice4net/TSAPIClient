using System;
using NLog;
using TSAPIClient.CSTA;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class ACSOpenStreamConfParser: IACSConfirmationParser
    {
        public ACSConfirmationEvent Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                logger.Info("ACSOpenStreamConfParser.Parse: eventType=ACS_OPEN_STREAM_CONF");
                logger.Info("ACSOpenStreamConfParser.Parse: try to read the ACSOpenStreamConfEvent_t confirmation event...");

                object result;

                if (reader.TryReadStruct(typeof(ACSOpenStreamConfEvent_t), out result))
                {
                    logger.Info("ACSOpenStreamConfParser.Parse: successfully read the ACSOpenStreamConfEvent_t confirmation event!");

                    ACSOpenStreamConfEvent_t acsopen = (ACSOpenStreamConfEvent_t)result;

                    ACSConfirmationEvent acsConfirmation = new ACSConfirmationEvent();

                    acsConfirmation.u.acsopen = acsopen;

                    return acsConfirmation;
                }

                return null;
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in ACSOpenStreamConfParser.Parse: {0}", err));
            }

            return null;
        }

        public int eventType
        {
            get { return Constants.ACS_OPEN_STREAM_CONF; }
        }
    }
}
