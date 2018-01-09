// ATTLoggedOnParser.cs
using System;
using NLog;
using TSAPIClient.ATT;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class ATTLoggedOnParser : IATTEventParser
    {
        public ATTEvent_t Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                logger.Info("ATTLoggedOnParser.Parse: eventType=ATT_LOGGED_ON");

                object result;

                if (reader.TryReadStruct(typeof(ATTLoggedOnEvent_t), out result))
                {
                    logger.Info("ATTLoggedOnParser.Parse: successfully read LoggedOn event!");
                    
                    ATTLoggedOnEvent_t loggedOnEvent = (ATTLoggedOnEvent_t)result;

                    logger.Info("ATTLoggedOnParser.Parse: loggedOnEvent.workMode={0}", loggedOnEvent.workMode);

                    ATTEvent_t attEvent = new ATTEvent_t() { eventType = (ushort)eventType };

                    attEvent.u.loggedOnEvent = loggedOnEvent;

                    return attEvent;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in ATTLoggedOnParser.Parse: {0}", err));
            }

            return null;
        }

        public int eventType
        {
            get { return Constants.ATT_LOGGED_ON; }
        }
    }
}
