// CSTADivertedParser.cs
using System;
using NLog;
using TSAPIClient.CSTA;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class CSTADivertedParser : ICSTAUnsolicitedParser
    {
        public CSTAUnsolicitedEvent Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                object result;

                logger.Info("CSTADivertedParser.Parse: eventType=CSTA_DIVERTED");
                logger.Info("CSTADivertedParser.Parse: try to read the CSTADivertedEvent_t unsolicited event...");

                if (reader.TryReadStruct(typeof(CSTADivertedEvent_t), out result))
                {
                    logger.Info("CSTADivertedParser.Parse: successfully read the CSTADivertedEvent_t unsolicited event!");

                    CSTADivertedEvent_t diverted = (CSTADivertedEvent_t)result;

                    CSTAUnsolicitedEvent cstaUnsolicited = new CSTAUnsolicitedEvent { u = { diverted = diverted } };

                    return cstaUnsolicited;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in CSTADivertedParser.Parse: {0}", err));
            }

            return null;
        }

        public int eventType
        {
            get { return Constants.CSTA_DIVERTED; }
        }
    }
}
