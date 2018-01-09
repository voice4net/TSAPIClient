// CSTAServiceInitiatedParser.cs
using System;
using NLog;
using TSAPIClient.CSTA;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class CSTAServiceInitiatedParser : ICSTAUnsolicitedParser
    {
        public CSTAUnsolicitedEvent Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                object result;

                logger.Info("CSTAServiceInitiatedParser.Parse: eventType=CSTA_SERVICE_INITIATED");
                logger.Info("CSTAServiceInitiatedParser.Parse: try to read the CSTAServiceInitiatedEvent_t unsolicited event...");

                if (reader.TryReadStruct(typeof(CSTAServiceInitiatedEvent_t), out result))
                {
                    logger.Info("CSTAServiceInitiatedParser.Parse: successfully read the CSTAServiceInitiatedEvent_t unsolicited event!");

                    CSTAServiceInitiatedEvent_t serviceInitiated = (CSTAServiceInitiatedEvent_t)result;

                    CSTAUnsolicitedEvent cstaUnsolicited = new CSTAUnsolicitedEvent
                    {
                        u = {serviceInitiated = serviceInitiated}
                    };

                    return cstaUnsolicited;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in CSTAServiceInitiatedParser.Parse: {0}", err));
            }

            return null;
        }

        public int eventType
        {
            get { return Constants.CSTA_SERVICE_INITIATED; }
        }
    }
}
