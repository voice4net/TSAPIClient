// CSTAOutOfServiceParser.cs
using System;
using NLog;
using TSAPIClient.CSTA;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class CSTAOutOfServiceParser : ICSTAUnsolicitedParser
    {
        public CSTAUnsolicitedEvent Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                object result;

                logger.Info("CSTAOutOfServiceParser.Parse: eventType=CSTA_OUT_OF_SERVICE");
                logger.Info("CSTAOutOfServiceParser.Parse: try to read the CSTAOutOfServiceEvent_t unsolicited event...");

                if (reader.TryReadStruct(typeof(CSTAOutOfServiceEvent_t), out result))
                {
                    logger.Info("CSTAOutOfServiceParser.Parse: successfully read the CSTAOutOfServiceEvent_t unsolicited event!");

                    CSTAOutOfServiceEvent_t outOfService = (CSTAOutOfServiceEvent_t)result;

                    CSTAUnsolicitedEvent cstaUnsolicited = new CSTAUnsolicitedEvent { u = { outOfService = outOfService } };

                    return cstaUnsolicited;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in CSTAOutOfServiceParser.Parse: {0}", err));
            }

            return null;
        }

        public int eventType
        {
            get { return Constants.CSTA_OUT_OF_SERVICE; }
        }
    }
}
