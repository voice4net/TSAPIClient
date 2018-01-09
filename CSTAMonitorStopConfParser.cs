using System;
using NLog;
using TSAPIClient.CSTA;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class CSTAMonitorStopConfParser : ICSTAConfirmationParser
    {
        public CSTAConfirmationEvent Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");
            try
            {
                object result;

                logger.Info("CSTAMonitorStopConfParser.Parse: eventType=CSTA_MONITOR_STOP_CONF");
                logger.Info("CSTAMonitorStopConfParser.Parse: try to read the CSTAMonitorStopConfEvent_t confirmation event...");

                if (reader.TryReadStruct(typeof(CSTAMonitorStopConfEvent_t), out result))
                {
                    logger.Info("CSTAMonitorStopConfParser.Parse: successfully read the CSTAMonitorStopConfEvent_t confirmation event!");

                    CSTAMonitorStopConfEvent_t monitorStop = (CSTAMonitorStopConfEvent_t)result;

                    CSTAConfirmationEvent cstaConfirmation = new CSTAConfirmationEvent { u = { monitorStop = monitorStop } };

                    return cstaConfirmation;
                }

                return null;
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in CSTAMonitorStopConfParser.Parse: {0}", err));
            }

            return null;
        }

        public int eventType
        {
            get { return Constants.CSTA_MONITOR_STOP_CONF; }
        }
    }
}
