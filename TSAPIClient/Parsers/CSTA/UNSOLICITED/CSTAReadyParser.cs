// CSTAReadyParser.cs
using System;
using NLog;
using TSAPIClient.CSTA;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class CSTAReadyParser : ICSTAUnsolicitedParser
    {
        public CSTAUnsolicitedEvent Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                object result;

                logger.Info("CSTAReadyParser.Parse: eventType=CSTA_READY");
                logger.Info("CSTAReadyParser.Parse: try to read the CSTAReadyEvent_t unsolicited event...");

                if (reader.TryReadStruct(typeof(CSTAReadyEvent_t), out result))
                {
                    logger.Info("CSTAReadyParser.Parse: successfully read the CSTAReadyEvent_t unsolicited event!");

                    CSTAReadyEvent_t ready = (CSTAReadyEvent_t)result;

                    CSTAUnsolicitedEvent cstaUnsolicited = new CSTAUnsolicitedEvent { u = { ready = ready } };

                    return cstaUnsolicited;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in CSTAReadyParser.Parse: {0}", err));
            }

            return null;
        }

        public int eventType
        {
            get { return Constants.CSTA_READY; }
        }
    }
}
