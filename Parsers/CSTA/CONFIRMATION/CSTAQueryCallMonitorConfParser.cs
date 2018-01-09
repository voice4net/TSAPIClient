using System;
using NLog;
using TSAPIClient.CSTA;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class CSTAQueryCallMonitorConfParser : ICSTAConfirmationParser
    {
        public CSTAConfirmationEvent Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                object result;

                logger.Info("CSTAQueryCallMonitorConfParser.Parse: eventType=CSTA_QUERY_CALL_MONITOR_CONF");
                logger.Info("CSTAQueryCallMonitorConfParser.Parse: try to read the CSTAQueryCallMonitorConfEvent_t confirmation event...");

                if (reader.TryReadStruct(typeof(CSTAQueryCallMonitorConfEvent_t), out result))
                {
                    logger.Info("CSTAQueryCallMonitorConfParser.Parse: successfully read the CSTAQueryCallMonitorConfEvent_t confirmation event!");

                    CSTAQueryCallMonitorConfEvent_t queryCallMonitor = (CSTAQueryCallMonitorConfEvent_t)result;

                    CSTAConfirmationEvent cstaConfirmation = new CSTAConfirmationEvent
                    {
                        u = {queryCallMonitor = queryCallMonitor}
                    };

                    return cstaConfirmation;
                }

                return null;
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in CSTAQueryCallMonitorConfParser.Parse: {0}", err));
            }

            return null;
        }

        public int eventType
        {
            get { return Constants.CSTA_QUERY_CALL_MONITOR_CONF; }
        }
    }
}
