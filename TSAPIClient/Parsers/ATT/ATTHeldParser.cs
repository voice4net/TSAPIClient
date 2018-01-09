// ATTHeldParser.cs
using System;
using NLog;
using TSAPIClient.ATT;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class ATTHeldParser : IATTEventParser
    {
        public ATTEvent_t Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                logger.Info("ATTHeldParser.Parse: eventType=ATT_HELD");

                object result;

                if (reader.TryReadStruct(typeof(ATTHeldEvent_t), out result))
                {
                    logger.Info("ATTHeldParser.Parse: successfully read Held event!");

                    ATTHeldEvent_t heldEvent = (ATTHeldEvent_t)result;

                    logger.Info("ATTHeldParser.Parse: heldEvent.consultMode={0}", heldEvent.consultMode);

                    ATTEvent_t attEvent = new ATTEvent_t() { eventType = (ushort)eventType };

                    attEvent.u.heldEvent = heldEvent;

                    return attEvent;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in ATTHeldParser.Parse: {0}", err));
            }

            return null;
        }

        public int eventType
        {
            get { return Constants.ATT_HELD; }
        }
    }
}
