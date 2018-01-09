// CSTAWorkNotReadyParser.cs
using System;
using NLog;
using TSAPIClient.CSTA;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class CSTAWorkNotReadyParser : ICSTAUnsolicitedParser
    {
        public CSTAUnsolicitedEvent Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                object result;

                logger.Info("CSTAWorkNotReadyParser.Parse: eventType=CSTA_WORK_NOT_READY");
                logger.Info("CSTAWorkNotReadyParser.Parse: try to read the CSTAWorkNotReadyEvent_t unsolicited event...");

                if (reader.TryReadStruct(typeof(CSTAWorkNotReadyEvent_t), out result))
                {
                    logger.Info("CSTAWorkNotReadyParser.Parse: successfully read the CSTAWorkNotReadyEvent_t unsolicited event!");

                    CSTAWorkNotReadyEvent_t workNotReady = (CSTAWorkNotReadyEvent_t)result;

                    CSTAUnsolicitedEvent cstaUnsolicited = new CSTAUnsolicitedEvent { u = { workNotReady = workNotReady } };

                    return cstaUnsolicited;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in CSTAWorkNotReadyParser.Parse: {0}", err));
            }

            return null;
        }

        public int eventType
        {
            get { return Constants.CSTA_WORK_NOT_READY; }
        }
    }
}
