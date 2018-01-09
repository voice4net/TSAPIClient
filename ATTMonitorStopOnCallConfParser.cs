// ATTMonitorStopOnCallConfParser.cs
using System;
using NLog;
using TSAPIClient.ATT;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class ATTMonitorStopOnCallConfParser : IATTEventParser
    {
        public ATTEvent_t Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                logger.Info("ATTMonitorStopOnCallConfParser.Parse: eventType=ATT_MONITOR_STOP_ON_CALL_CONF");

                object result;

                if (reader.TryReadStruct(typeof(ATTMonitorStopOnCallConfEvent_t), out result))
                {
                    ATTMonitorStopOnCallConfEvent_t monitorStopOnCall = (ATTMonitorStopOnCallConfEvent_t)result;

                    ATTEvent_t attEvent = new ATTEvent_t() { eventType = (ushort)eventType };

                    attEvent.u.monitorStopOnCall = monitorStopOnCall;

                    return attEvent;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in ATTMonitorStopOnCallConfParser.Parse: {0}", err));
            }

            return null;
        }

        public int eventType
        {
            get { return Constants.ATT_MONITOR_STOP_ON_CALL_CONF; }
        }
    }
}
