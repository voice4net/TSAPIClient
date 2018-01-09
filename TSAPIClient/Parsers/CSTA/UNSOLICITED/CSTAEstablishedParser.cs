// CSTAEstablishedParser.cs
using System;
using NLog;
using TSAPIClient.CSTA;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class CSTAEstablishedParser : ICSTAUnsolicitedParser
    {
        public CSTAUnsolicitedEvent Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                object result;

                logger.Info("CSTAEstablishedParser.Parse: eventType=CSTA_ESTABLISHED");
                logger.Info("CSTAEstablishedParser.Parse: try to read the CSTAEstablishedEvent_t unsolicited event...");

                if (reader.TryReadStruct(typeof(CSTAEstablishedEvent_t), out result))
                {
                    logger.Info("CSTAEstablishedParser.Parse: successfully read the CSTAEstablishedEvent_t unsolicited event!");

                    CSTAEstablishedEvent_t established = (CSTAEstablishedEvent_t)result;

                    logger.Info("CSTAEstablishedParser.Parse: answeringDevice=" + established.answeringDevice.deviceID.device + ";callingDevice=" + established.callingDevice.deviceID.device + ";calledDevice=" + established.calledDevice.deviceID.device + ";");

                    CSTAUnsolicitedEvent cstaUnsolicited = new CSTAUnsolicitedEvent { u = { established = established } };

                    return cstaUnsolicited;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in CSTAEstablishedParser.Parse: {0}", err));
            }

            return null;
        }

        public int eventType
        {
            get { return Constants.CSTA_ESTABLISHED; }
        }
    }
}
