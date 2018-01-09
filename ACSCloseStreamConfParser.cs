using System;
using NLog;
using TSAPIClient.CSTA;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class ACSCloseStreamConfParser: IACSConfirmationParser
    {
        public ACSConfirmationEvent Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                logger.Info("ACSCloseStreamConfParser.Parse: eventType=ACS_CLOSE_STREAM_CONF");
                logger.Info("ACSCloseStreamConfParser.Parse: try to read the ACSCloseStreamConfEvent_t confirmation event...");

                object result;

                if (reader.TryReadStruct(typeof(ACSCloseStreamConfEvent_t), out result))
                {
                    logger.Info("ACSCloseStreamConfParser.Parse: successfully read the ACSCloseStreamConfEvent_t confirmation event!");

                    ACSCloseStreamConfEvent_t acsclose = (ACSCloseStreamConfEvent_t)result;

                    ACSConfirmationEvent acsConfirmation = new ACSConfirmationEvent();

                    acsConfirmation.u.acsclose = acsclose;

                    return acsConfirmation;
                }

                return null;
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in ACSCloseStreamConfParser.Parse: {0}", err));
            }

            return null;
        }

        public int eventType
        {
            get { return Constants.ACS_CLOSE_STREAM_CONF; }
        }
    }
}
