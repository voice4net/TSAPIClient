// CSTAFailedParser.cs
using System;
using NLog;
using TSAPIClient.CSTA;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class CSTAFailedParser : ICSTAUnsolicitedParser
    {
        public CSTAUnsolicitedEvent Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                object result;

                logger.Info("CSTAFailedParser.Parse: eventType=CSTA_FAILED");
                logger.Info("CSTAFailedParser.Parse: try to read the CSTAFailedEvent_t unsolicited event...");

                if (reader.TryReadStruct(typeof(CSTAFailedEvent_t), out result))
                {
                    logger.Info("CSTAFailedParser.Parse: successfully read the CSTAFailedEvent_t unsolicited event!");

                    CSTAFailedEvent_t failed = (CSTAFailedEvent_t)result;

                    logger.Info("CSTAFailedParser.Parse: Cause: {0}", failed.cause);

                    CSTAUnsolicitedEvent cstaUnsolicited = new CSTAUnsolicitedEvent { u = { failed = failed } };

                    return cstaUnsolicited;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in CSTAFailedParser.Parse: {0}", err));
            }

            return null;
        }

        public int eventType
        {
            get { return Constants.CSTA_FAILED; }
        }
    }
}
