// CSTAWorkReadyParser.cs
using System;
using NLog;
using TSAPIClient.CSTA;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class CSTAWorkReadyParser : ICSTAUnsolicitedParser
    {
        public CSTAUnsolicitedEvent Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                object result;

                logger.Info("CSTAWorkReadyParser.Parse: eventType=CSTA_WORK_READY");
                logger.Info("CSTAWorkReadyParser.Parse: try to read the CSTAWorkReadyEvent_t unsolicited event...");

                if (reader.TryReadStruct(typeof(CSTAWorkReadyEvent_t), out result))
                {
                    logger.Info("CSTAWorkReadyParser.Parse: successfully read the CSTAWorkReadyEvent_t unsolicited event!");

                    CSTAWorkReadyEvent_t workReady = (CSTAWorkReadyEvent_t)result;

                    CSTAUnsolicitedEvent cstaUnsolicited = new CSTAUnsolicitedEvent { u = { workReady = workReady } };

                    return cstaUnsolicited;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in CSTAWorkReadyParser.Parse: {0}", err));
            }

            return null;
        }

        public int eventType
        {
            get { return Constants.CSTA_WORK_READY; }
        }
    }
}
