// ATTMakeCallConfParser.cs
using System;
using NLog;
using TSAPIClient.ATT;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class ATTMakeCallConfParser : IATTEventParser
    {
        public ATTEvent_t Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                logger.Info("ATTMakeCallConfParser.Parse: eventType=ATT_MAKE_CALL_CONF");

                object result;

                if (reader.TryReadStruct(typeof(ATTMakeCallConfEvent_t), out result))
                {
                    logger.Info("ATTMakeCallConfParser.Parse: successfully read Make Call confirmation event!");
                    
                    ATTMakeCallConfEvent_t makeCall = (ATTMakeCallConfEvent_t)result;

                    logger.Info("ATTMakeCallConfParser.Parse: makeCall.ucid.value={0}", makeCall.ucid.value);

                    ATTEvent_t attEvent = new ATTEvent_t() { eventType = (ushort)eventType };

                    attEvent.u.makeCall = makeCall;

                    return attEvent;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in ATTMakeCallConfParser.Parse: {0}", err));
            }

            return null;
        }

        public int eventType
        {
            get { return Constants.ATT_MAKE_CALL_CONF; }
        }
    }
}
