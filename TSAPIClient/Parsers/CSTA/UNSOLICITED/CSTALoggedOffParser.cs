// CSTALoggedOffParser.cs
using System;
using NLog;
using TSAPIClient.CSTA;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class CSTALoggedOffParser : ICSTAUnsolicitedParser
    {
        public CSTAUnsolicitedEvent Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                object result;

                logger.Info("CSTALoggedOffParser.Parse: eventType=CSTA_LOGGED_OFF");
                logger.Info("CSTALoggedOffParser.Parse: try to read the CSTALoggedOffEvent_t unsolicited event...");

                if (reader.TryReadStruct(typeof(CSTALoggedOffEvent_t), out result))
                {
                    logger.Info("CSTALoggedOffParser.Parse: successfully read the CSTALoggedOffEvent_t unsolicited event!");

                    CSTALoggedOffEvent_t loggedOff = (CSTALoggedOffEvent_t)result;

                    CSTAUnsolicitedEvent cstaUnsolicited = new CSTAUnsolicitedEvent { u = { loggedOff = loggedOff } };

                    return cstaUnsolicited;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in CSTALoggedOffParser.Parse: {0}", err));
            }

            return null;
        }

        public int eventType
        {
            get { return Constants.CSTA_LOGGED_OFF; }
        }
    }
}
