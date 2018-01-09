// ATTTransferCallConfParser.cs
using System;
using NLog;
using TSAPIClient.ATT;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class ATTTransferCallConfParser : IATTEventParser
    {
        public ATTEvent_t Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                logger.Info("ATTTransferCallConfParser.Parse: eventType=ATT_TRANSFER_CALL_CONF");

                object result;

                if (reader.TryReadStruct(typeof(ATTTransferCallConfEvent_t), out result))
                {
                    logger.Info("ATTTransferCallConfParser.Parse: successfully read transfer call confirmation event!");

                    ATTTransferCallConfEvent_t transferCall = (ATTTransferCallConfEvent_t)result;

                    logger.Info("ATTTransferCallConfParser.Parse: transferCall.ucid.value={0};", transferCall.ucid.value);

                    ATTEvent_t attEvent = new ATTEvent_t() { eventType = (ushort)eventType };

                    attEvent.u.transferCall = transferCall;

                    return attEvent;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in ATTTransferCallConfParser.Parse: {0}", err));
            }

            return null;
        }

        public int eventType
        {
            get { return Constants.ATT_TRANSFER_CALL_CONF; }
        }
    }
}
