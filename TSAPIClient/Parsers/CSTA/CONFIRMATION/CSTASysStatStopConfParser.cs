using System;
using NLog;
using TSAPIClient.CSTA;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class CSTASysStatStopConfParser : ICSTAConfirmationParser
    {
        public CSTAConfirmationEvent Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                object result;

                logger.Info("CSTASysStatStopConfParser.Parse: eventType=CSTA_SYS_STAT_STOP_CONF");
                logger.Info("CSTASysStatStopConfParser.Parse: try to read the CSTASysStatStopConfEvent_t confirmation event...");

                if (reader.TryReadStruct(typeof(CSTASysStatStopConfEvent_t), out result))
                {
                    logger.Info("CSTASysStatStopConfParser.Parse: successfully read the CSTASysStatStopConfEvent_t confirmation event!");

                    CSTASysStatStopConfEvent_t sysStatStop = (CSTASysStatStopConfEvent_t)result;

                    CSTAConfirmationEvent cstaConfirmation = new CSTAConfirmationEvent { u = { sysStatStop = sysStatStop } };

                    return cstaConfirmation;
                }

                return null;
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in CSTASysStatStopConfParser.Parse: {0}", err));
            }

            return null;
        }

        public int eventType
        {
            get { return Constants.CSTA_SYS_STAT_STOP_CONF; }
        }
    }
}
