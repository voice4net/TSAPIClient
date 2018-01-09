// CSTAMonitorEndedParser.cs
using System;
using NLog;
using TSAPIClient.CSTA;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class CSTAMonitorEndedParser : ICSTAUnsolicitedParser
    {
        public CSTAUnsolicitedEvent Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                object result;

                logger.Info("CSTAMonitorEndedParser.Parse: eventType=CSTA_MONITOR_ENDED");
                logger.Info("CSTAMonitorEndedParser.Parse: try to read the CSTAMonitorEndedEvent_t unsolicited event...");

                if (reader.TryReadStruct(typeof(CSTAMonitorEndedEvent_t), out result))
                {
                    logger.Info("CSTAMonitorEndedParser.Parse: successfully read the CSTAMonitorEndedEvent_t unsolicited event!");

                    CSTAMonitorEndedEvent_t monitorEnded = (CSTAMonitorEndedEvent_t)result;

                    CSTAUnsolicitedEvent cstaUnsolicited = new CSTAUnsolicitedEvent { u = { monitorEnded = monitorEnded } };


                    return cstaUnsolicited;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in CSTAMonitorEndedParser.Parse: {0}", err));
            }

            return null;
        }

        public int eventType
        {
            get { return Constants.CSTA_MONITOR_ENDED; }
        }
    }
}
