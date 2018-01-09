// ATTMakePredictiveCallConfParser.cs
using System;
using NLog;
using TSAPIClient.ATT;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class ATTMakePredictiveCallConfParser : IATTEventParser
    {
        public ATTEvent_t Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                logger.Info("ATTMakePredictiveCallConfParser.Parse: eventType=ATT_MAKE_PREDICTIVE_CALL_CONF");

                object result;

                if (reader.TryReadStruct(typeof(ATTMakePredictiveCallConfEvent_t), out result))
                {
                    ATTMakePredictiveCallConfEvent_t makePredictiveCall = (ATTMakePredictiveCallConfEvent_t)result;

                    ATTEvent_t attEvent = new ATTEvent_t() { eventType = (ushort)eventType };

                    attEvent.u.makePredictiveCall = makePredictiveCall;

                    return attEvent;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in ATTMakePredictiveCallConfParser.Parse: {0}", err));
            }

            return null;
        }

        public int eventType
        {
            get { return Constants.ATT_MAKE_PREDICTIVE_CALL_CONF; }
        }
    }
}
