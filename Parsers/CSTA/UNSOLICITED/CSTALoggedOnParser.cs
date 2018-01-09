// CSTALoggedOnParser.cs
using System;
using NLog;
using TSAPIClient.CSTA;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class CSTALoggedOnParser : ICSTAUnsolicitedParser
    {
        public CSTAUnsolicitedEvent Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                object result;

                logger.Info("CSTALoggedOnParser.Parse: eventType=CSTA_LOGGED_ON");
                logger.Info("CSTALoggedOnParser.Parse: try to read the CSTALoggedOnEvent_t unsolicited event...");

                if (reader.TryReadStruct(typeof(CSTALoggedOnEvent_t), out result))
                {
                    logger.Info("CSTALoggedOnParser.Parse: successfully read the CSTALoggedOnEvent_t unsolicited event!");

                    CSTALoggedOnEvent_t loggedOn = (CSTALoggedOnEvent_t)result;

                    CSTAUnsolicitedEvent cstaUnsolicited = new CSTAUnsolicitedEvent { u = { loggedOn = loggedOn } };

                    return cstaUnsolicited;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in CSTALoggedOnParser.Parse: {0}", err));
            }

            return null;
        }

        public int eventType
        {
            get { return Constants.CSTA_LOGGED_ON; }
        }
    }
}
