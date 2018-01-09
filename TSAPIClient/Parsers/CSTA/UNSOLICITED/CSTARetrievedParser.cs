// CSTARetrievedParser.cs
using System;
using NLog;
using TSAPIClient.CSTA;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class CSTARetrievedParser : ICSTAUnsolicitedParser
    {
        public CSTAUnsolicitedEvent Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                object result;

                logger.Info("CSTARetrievedParser.Parse: eventType=CSTA_RETRIEVED");
                logger.Info("CSTARetrievedParser.Parse: try to read the CSTARetrievedEvent_t unsolicited event...");

                if (reader.TryReadStruct(typeof(CSTARetrievedEvent_t), out result))
                {
                    logger.Info("CSTARetrievedParser.Parse: successfully read the CSTARetrievedEvent_t unsolicited event!");

                    CSTARetrievedEvent_t retrieved = (CSTARetrievedEvent_t)result;

                    CSTAUnsolicitedEvent cstaUnsolicited = new CSTAUnsolicitedEvent { u = { retrieved = retrieved } };

                    return cstaUnsolicited;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in CSTARetrievedParser.Parse: {0}", err));
            }

            return null;
        }

        public int eventType
        {
            get { return Constants.CSTA_RETRIEVED; }
        }
    }
}
