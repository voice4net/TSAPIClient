using System;
using NLog;
using TSAPIClient.CSTA;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class CSTAMonitorConfParser : ICSTAConfirmationParser
    {
        public CSTAConfirmationEvent Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                logger.Info("CSTAMonitorConfParser.Parse: eventType=CSTA_MONITOR_CONF");
                logger.Info("CSTAMonitorConfParser.Parse: try to read the CSTAMonitorConfEvent_t confirmation event...");

                object result;

                if (reader.TryReadStruct(typeof(CSTAMonitorConfEvent_t), out result))
                {
                    logger.Info("CSTAMonitorConfParser.Parse: successfully read the CSTAMonitorConfEvent_t confirmation event!");

                    CSTAMonitorConfEvent_t monitorStart = (CSTAMonitorConfEvent_t)result;

                    logger.Info("CSTAMonitorConfParser.Parse: xref={0};", monitorStart.monitorCrossRefID);

                    CSTAConfirmationEvent cstaConfirmation = new CSTAConfirmationEvent
                    {
                        u = {monitorStart = monitorStart}
                    };

                    return cstaConfirmation;
                }

                return null;
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in CSTAMonitorConfParser.Parse: {0}", err));
            }

            return null;
        }

        public int eventType
        {
            get { return Constants.CSTA_MONITOR_CONF; }
        }
    }
}
