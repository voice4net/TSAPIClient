// CSTANetworkReachedParser.cs
using System;
using NLog;
using TSAPIClient.CSTA;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class CSTANetworkReachedParser : ICSTAUnsolicitedParser
    {
        public CSTAUnsolicitedEvent Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                object result;

                logger.Info("CSTANetworkReachedParser.Parse: eventType=CSTA_NETWORK_REACHED");
                logger.Info("CSTANetworkReachedParser.Parse: try to read the CSTANetworkReachedEvent_t unsolicited event...");

                if (reader.TryReadStruct(typeof(CSTANetworkReachedEvent_t), out result))
                {
                    logger.Info("CSTANetworkReachedParser.Parse: successfully read the CSTANetworkReachedEvent_t unsolicited event!");

                    CSTANetworkReachedEvent_t networkReached = (CSTANetworkReachedEvent_t)result;

                    CSTAUnsolicitedEvent cstaUnsolicited = new CSTAUnsolicitedEvent
                    {
                        u = { networkReached = networkReached }
                    };

                    return cstaUnsolicited;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in CSTANetworkReachedParser.Parse: {0}", err));
            }

            return null;
        }

        public int eventType
        {
            get { return Constants.CSTA_NETWORK_REACHED; }
        }
    }
}
