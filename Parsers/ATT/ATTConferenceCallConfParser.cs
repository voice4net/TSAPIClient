// ATTConferenceCallConfParser.cs
using System;
using NLog;
using TSAPIClient.ATT;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class ATTConferenceCallConfParser : IATTEventParser
    {
        public ATTEvent_t Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                logger.Info("ATTConferenceCallConfParser.Parse: eventType=ATT_CONFERENCE_CALL_CONF");

                object result;

                if (reader.TryReadStruct(typeof(ATTConferenceCallConfEvent_t), out result))
                {
                    logger.Info("ATTConferenceCallConfParser.Parse: successfully read conference call confirmation event!");

                    ATTConferenceCallConfEvent_t conferenceCall = (ATTConferenceCallConfEvent_t)result;

                    logger.Info("ATTConferenceCallConfParser.Parse: conferenceCall.Ucid.value={0};", conferenceCall.ucid.value);

                    ATTEvent_t attEvent = new ATTEvent_t() { eventType = (ushort)eventType };

                    attEvent.u.conferenceCall = conferenceCall;

                    return attEvent;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in ATTConferenceCallConfParser.Parse: {0}", err));
            }

            return null;
        }

        public int eventType
        {
            get { return Constants.ATT_CONFERENCE_CALL_CONF; }
        }
    }
}
