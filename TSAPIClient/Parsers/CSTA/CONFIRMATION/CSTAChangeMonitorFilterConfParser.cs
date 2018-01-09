using System;
using NLog;
using TSAPIClient.CSTA;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class CSTAChangeMonitorFilterConfParser : ICSTAConfirmationParser
    {
        public CSTAConfirmationEvent Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");
            try
            {
                object result;

                logger.Info("CSTAChangeMonitorFilterConfParser.Parse: eventType=CSTA_CHANGE_MONITOR_FILTER_CONF");
                logger.Info("CSTAChangeMonitorFilterConfParser.Parse: try to read the CSTAChangeMonitorFilterConfEvent_t confirmation event...");

                if (reader.TryReadStruct(typeof(CSTAChangeMonitorFilterConfEvent_t), out result))
                {
                    logger.Info("CSTAChangeMonitorFilterConfParser.Parse: successfully read the CSTAChangeMonitorFilterConfEvent_t confirmation event!");

                    CSTAChangeMonitorFilterConfEvent_t changeMonitorFilter = (CSTAChangeMonitorFilterConfEvent_t)result;

                    CSTAConfirmationEvent cstaConfirmation = new CSTAConfirmationEvent
                    {
                        u = {changeMonitorFilter = changeMonitorFilter}
                    };

                    return cstaConfirmation;
                }

                return null;
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in CSTAChangeMonitorFilterConfParser.Parse: {0}", err));
            }

            return null;
        }

        public int eventType
        {
            get { return Constants.CSTA_CHANGE_MONITOR_FILTER_CONF; }
        }
    }
}
