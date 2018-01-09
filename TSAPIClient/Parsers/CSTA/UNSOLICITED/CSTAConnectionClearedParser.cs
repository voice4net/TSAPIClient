// CSTAConnectionClearedParser.cs
using System;
using NLog;
using TSAPIClient.CSTA;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class CSTAConnectionClearedParser : ICSTAUnsolicitedParser
    {
        public CSTAUnsolicitedEvent Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                object result;

                logger.Info("CSTAConnectionClearedParser.Parse: eventType=CSTA_CONNECTION_CLEARED");
                logger.Info("CSTAConnectionClearedParser.Parse: try to read the CSTAConnectionClearedEvent_t unsolicited event...");

                if (reader.TryReadStruct(typeof(CSTAConnectionClearedEvent_t), out result))
                {
                    logger.Info("CSTAConnectionClearedParser.Parse: successfully read the CSTAConnectionClearedEvent_t unsolicited event!");

                    CSTAConnectionClearedEvent_t connectionCleared = (CSTAConnectionClearedEvent_t)result;

                    CSTAUnsolicitedEvent cstaUnsolicited = new CSTAUnsolicitedEvent();

                    cstaUnsolicited.u.connectionCleared = connectionCleared;

                    return cstaUnsolicited;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in CSTAConnectionClearedParser.Parse: {0}", err));
            }

            return null;
        }

        public int eventType
        {
            get { return Constants.CSTA_CONNECTION_CLEARED; }
        }
    }
}
