// ATTMonitorConfParser.cs
using System;
using NLog;
using TSAPIClient.ATT;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class ATTMonitorConfParser : IATTEventParser
    {
        public ATTEvent_t Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                logger.Info("ATTMonitorConfParser.Parse: eventType=ATT_MONITOR_CONF");

                object result;

                if (reader.TryReadStruct(typeof(ATTMonitorConfEvent_t), out result))
                {
                    logger.Info("ATTMonitorConfParser.Parse: successfully read monitor confirmation event!");

                    ATTMonitorConfEvent_t monitorStart = (ATTMonitorConfEvent_t)result;

                    logger.Info("ATTMonitorConfParser.Parse: monitorStart.usedFilter.filter={0}", monitorStart.usedFilter.filter);

                    ATTEvent_t attEvent = new ATTEvent_t() { eventType = (ushort)eventType };

                    attEvent.u.monitorStart = monitorStart;

                    return attEvent;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in ATTMonitorConfParser.Parse: {0}", err));
            }

            return null;
        }

        public int eventType
        {
            get { return Constants.ATT_MONITOR_CONF; }
        }
    }
}
