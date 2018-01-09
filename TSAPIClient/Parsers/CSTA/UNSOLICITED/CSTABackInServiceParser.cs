// CSTABackInServiceParser.cs
using System;
using NLog;
using TSAPIClient.CSTA;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class CSTABackInServiceParser : ICSTAUnsolicitedParser
    {
        public CSTAUnsolicitedEvent Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                object result;

                logger.Info("CSTABackInServiceParser.Parse: eventType=CSTA_BACK_IN_SERVICE");
                logger.Info("CSTABackInServiceParser.Parse: try to read the CSTABackInServiceEvent_t unsolicited event...");

                if (reader.TryReadStruct(typeof(CSTABackInServiceEvent_t), out result))
                {
                    logger.Info("CSTABackInServiceParser.Parse: successfully read the CSTABackInServiceEvent_t unsolicited event!");

                    CSTABackInServiceEvent_t backInService = (CSTABackInServiceEvent_t)result;

                    CSTAUnsolicitedEvent cstaUnsolicited = new CSTAUnsolicitedEvent
                    {
                        u = {backInService = backInService}
                    };

                    return cstaUnsolicited;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in CSTABackInServiceParser.Parse: {0}", err));
            }

            return null;
        }

        public int eventType
        {
            get { return Constants.CSTA_BACK_IN_SERVICE; }
        }
    }
}
