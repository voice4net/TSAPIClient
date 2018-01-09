// CSTAForwardingParser.cs
using System;
using NLog;
using TSAPIClient.CSTA;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class CSTAForwardingParser : ICSTAUnsolicitedParser
    {
        public CSTAUnsolicitedEvent Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                object result;

                logger.Info("CSTAForwardingParser.Parse: eventType=CSTA_FORWARDING");
                logger.Info("CSTAForwardingParser.Parse: try to read the CSTAForwardingEvent_t unsolicited event...");

                if (reader.TryReadStruct(typeof(CSTAForwardingEvent_t), out result))
                {
                    logger.Info("CSTAForwardingParser.Parse: successfully read the CSTAForwardingEvent_t unsolicited event!");

                    CSTAForwardingEvent_t forwarding = (CSTAForwardingEvent_t)result;

                    CSTAUnsolicitedEvent cstaUnsolicited = new CSTAUnsolicitedEvent { u = { forwarding = forwarding } };

                    return cstaUnsolicited;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in CSTAForwardingParser.Parse: {0}", err));
            }

            return null;
        }

        public int eventType
        {
            get { return Constants.CSTA_FORWARDING; }
        }
    }
}
