// CSTAHeldParser.cs
using System;
using NLog;
using TSAPIClient.CSTA;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class CSTAHeldParser : ICSTAUnsolicitedParser
    {
        public CSTAUnsolicitedEvent Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                object result;

                logger.Info("CSTAHeldParser.Parse: eventType=CSTA_HELD");
                logger.Info("CSTAHeldParser.Parse: try to read the CSTAHeldEvent_t unsolicited event...");

                if (reader.TryReadStruct(typeof(CSTAHeldEvent_t), out result))
                {
                    logger.Info("CSTAHeldParser.Parse: successfully read the CSTAHeldEvent_t unsolicited event!");

                    CSTAHeldEvent_t held = (CSTAHeldEvent_t)result;

                    CSTAUnsolicitedEvent cstaUnsolicited = new CSTAUnsolicitedEvent { u = { held = held } };

                    return cstaUnsolicited;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in CSTAHeldParser.Parse: {0}", err));
            }

            return null;
        }

        public int eventType
        {
            get { return Constants.CSTA_HELD; }
        }
    }
}
