// CSTAMessageWaitingParser.cs
using System;
using NLog;
using TSAPIClient.CSTA;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class CSTAMessageWaitingParser : ICSTAUnsolicitedParser
    {
        public CSTAUnsolicitedEvent Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                object result;

                logger.Info("CSTAMessageWaitingParser.Parse: eventType=CSTA_MESSAGE_WAITING");
                logger.Info("CSTAMessageWaitingParser.Parse: try to read the CSTAMessageWaitingEvent_t unsolicited event...");

                if (reader.TryReadStruct(typeof(CSTAMessageWaitingEvent_t), out result))
                {
                    logger.Info("CSTAMessageWaitingParser.Parse: successfully read the CSTAMessageWaitingEvent_t unsolicited event!");

                    CSTAMessageWaitingEvent_t messageWaiting = (CSTAMessageWaitingEvent_t)result;

                    CSTAUnsolicitedEvent cstaUnsolicited = new CSTAUnsolicitedEvent
                    {
                        u = { messageWaiting = messageWaiting }
                    };

                    return cstaUnsolicited;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in CSTAMessageWaitingParser.Parse: {0}", err));
            }

            return null;
        }

        public int eventType
        {
            get { return Constants.CSTA_MESSAGE_WAITING; }
        }
    }
}
