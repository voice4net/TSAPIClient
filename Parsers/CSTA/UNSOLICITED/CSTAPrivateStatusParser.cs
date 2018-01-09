// CSTAPrivateStatusParser.cs
using System;
using NLog;
using TSAPIClient.CSTA;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class CSTAPrivateStatusParser : ICSTAUnsolicitedParser
    {
        public CSTAUnsolicitedEvent Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                object result;

                logger.Info("CSTAPrivateStatusParser.Parse: eventType=CSTA_PRIVATE_STATUS");
                logger.Info("CSTAPrivateStatusParser.Parse: try to read the CSTAPrivateStatusEvent_t unsolicited event...");

                if (reader.TryReadStruct(typeof(CSTAPrivateStatusEvent_t), out result))
                {
                    logger.Info("CSTAPrivateStatusParser.Parse: successfully read the CSTAPrivateStatusEvent_t unsolicited event!");

                    CSTAPrivateStatusEvent_t privateStatus = (CSTAPrivateStatusEvent_t)result;

                    CSTAUnsolicitedEvent cstaUnsolicited = new CSTAUnsolicitedEvent
                    {
                        u = { privateStatus = privateStatus }
                    };

                    return cstaUnsolicited;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in CSTAPrivateStatusParser.Parse: {0}", err));
            }

            return null;
        }


        public int eventType
        {
            get { return Constants.CSTA_PRIVATE_STATUS; }
        }
    }
}
