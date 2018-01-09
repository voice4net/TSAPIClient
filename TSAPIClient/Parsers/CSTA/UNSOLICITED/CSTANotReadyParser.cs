// CSTANotReadyParser.cs
using System;
using NLog;
using TSAPIClient.CSTA;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class CSTANotReadyParser : ICSTAUnsolicitedParser
    {
        public CSTAUnsolicitedEvent Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                object result;

                logger.Info("CSTANotReadyParser.Parse: eventType=CSTA_NOT_READY");
                logger.Info("CSTANotReadyParser.Parse: try to read the CSTANotReadyEvent_t unsolicited event...");

                if (reader.TryReadStruct(typeof(CSTANotReadyEvent_t), out result))
                {
                    logger.Info("CSTANotReadyParser.Parse: successfully read the CSTANotReadyEvent_t unsolicited event!");

                    CSTANotReadyEvent_t notReady = (CSTANotReadyEvent_t)result;

                    CSTAUnsolicitedEvent cstaUnsolicited = new CSTAUnsolicitedEvent { u = { notReady = notReady } };

                    return cstaUnsolicited;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in CSTANotReadyParser.Parse: {0}", err));
            }

            return null;
        }

        public int eventType
        {
            get { return Constants.CSTA_NOT_READY; }
        }
    }
}
