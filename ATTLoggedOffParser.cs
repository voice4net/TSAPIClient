// ATTLoggedOffParser.cs
using System;
using NLog;
using TSAPIClient.ATT;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class ATTLoggedOffParser : IATTEventParser
    {
        public ATTEvent_t Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                logger.Info("ATTLoggedOffParser.Parse: eventType=ATT_LOGGED_OFF");

                object result;

                if (reader.TryReadStruct(typeof(ATTLoggedOffEvent_t), out result))
                {
                    logger.Info("ATTLoggedOffParser.Parse: successfully read LoggedOff event!");

                    ATTLoggedOffEvent_t loggedOffEvent = (ATTLoggedOffEvent_t)result;

                    logger.Info("ATTLoggedOffParser.Parse: loggedOffEvent.reasonCode={0}", loggedOffEvent.reasonCode);

                    ATTEvent_t attEvent = new ATTEvent_t() { eventType = (ushort)eventType };

                    attEvent.u.loggedOffEvent = loggedOffEvent;

                    return attEvent;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in ATTLoggedOffParser.Parse: {0}", err));
            }

            return null;
        }

        public int eventType
        {
            get { return Constants.ATT_LOGGED_OFF; }
        }
    }
}
