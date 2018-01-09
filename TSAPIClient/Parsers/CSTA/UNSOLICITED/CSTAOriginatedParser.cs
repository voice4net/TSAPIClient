// CSTAOriginatedParser.cs
using System;
using NLog;
using TSAPIClient.CSTA;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class CSTAOriginatedParser : ICSTAUnsolicitedParser
    {
        public CSTAUnsolicitedEvent Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                object result;

                logger.Info("CSTAOriginatedParser.Parse: eventType=CSTA_ORIGINATED");
                logger.Info("CSTAOriginatedParser.Parse: try to read the CSTAOriginatedEvent_t unsolicited event...");

                if (reader.TryReadStruct(typeof(CSTAOriginatedEvent_t), out result))
                {
                    logger.Info("CSTAOriginatedParser.Parse: successfully read the CSTAOriginatedEvent_t unsolicited event!");

                    CSTAOriginatedEvent_t originated = (CSTAOriginatedEvent_t)result;

                    CSTAUnsolicitedEvent cstaUnsolicited = new CSTAUnsolicitedEvent { u = { originated = originated } };

                    return cstaUnsolicited;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in CSTAOriginatedParser.Parse: {0}", err));
            }

            return null;
        }

        public int eventType
        {
            get { return Constants.CSTA_ORIGINATED; }
        }
    }
}
