// CSTACallInformationParser.cs
using System;
using NLog;
using TSAPIClient.CSTA;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class CSTACallInformationParser : ICSTAUnsolicitedParser
    {
        public CSTAUnsolicitedEvent Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                object result;

                logger.Info("CSTACallInformationParser.Parse: eventType=CSTA_CALL_INFORMATION");
                logger.Info("CSTACallInformationParser.Parse: try to read the CSTACallInformationEvent_t unsolicited event...");

                if (reader.TryReadStruct(typeof(CSTACallInformationEvent_t), out result))
                {
                    logger.Info("CSTACallInformationParser.Parse: successfully read the CSTACallInformationEvent_t unsolicited event!");

                    CSTACallInformationEvent_t callInformation = (CSTACallInformationEvent_t)result;

                    CSTAUnsolicitedEvent cstaUnsolicited = new CSTAUnsolicitedEvent
                    {
                        u = {callInformation = callInformation}
                    };

                    return cstaUnsolicited;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in CSTACallInformationParser.Parse: {0}", err));
            }

            return null;
        }

        public int eventType
        {
            get { return Constants.CSTA_CALL_INFORMATION; }
        }
    }
}
