// ATTSendDTMFToneConfParser.cs
using System;
using NLog;
using TSAPIClient.ATT;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class ATTSendDTMFToneConfParser : IATTEventParser
    {
        public ATTEvent_t Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                logger.Info("ATTSendDTMFToneConfParser.Parse: eventType=ATT_SENDDTMF_TONE_CONF");

                object result;

                if (reader.TryReadStruct(typeof(ATTSendDTMFToneConfEvent_t), out result))
                {
                    ATTSendDTMFToneConfEvent_t sendDTMFTone = (ATTSendDTMFToneConfEvent_t)result;

                    ATTEvent_t attEvent = new ATTEvent_t() { eventType = (ushort)eventType };

                    attEvent.u.sendDTMFTone = sendDTMFTone;

                    return attEvent;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in ATTSendDTMFToneConfParser.Parse: {0}", err));
            }

            return null;
        }

        public int eventType
        {
            get { return Constants.ATT_SENDDTMF_TONE_CONF; }
        }
    }
}