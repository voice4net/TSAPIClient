// CSTADoNotDisturbParser.cs
using System;
using NLog;
using TSAPIClient.CSTA;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class CSTADoNotDisturbParser : ICSTAUnsolicitedParser
    {
        public CSTAUnsolicitedEvent Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                object result;

                logger.Info("CSTADoNotDisturbParser.Parse: eventType=CSTA_DO_NOT_DISTURB");
                logger.Info("CSTADoNotDisturbParser.Parse: try to read the CSTADoNotDisturbEvent_t unsolicited event...");

                if (reader.TryReadStruct(typeof(CSTADoNotDisturbEvent_t), out result))
                {
                    logger.Info("CSTADoNotDisturbParser.Parse: successfully read the CSTADoNotDisturbEvent_t unsolicited event!");

                    CSTADoNotDisturbEvent_t doNotDisturb = (CSTADoNotDisturbEvent_t)result;

                    CSTAUnsolicitedEvent cstaUnsolicited = new CSTAUnsolicitedEvent { u = { doNotDisturb = doNotDisturb } };

                    return cstaUnsolicited;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in CSTADoNotDisturbParser.Parse: {0}", err));
            }

            return null;
        }

        public int eventType
        {
            get { return Constants.CSTA_DO_NOT_DISTURB; }
        }
    }
}
