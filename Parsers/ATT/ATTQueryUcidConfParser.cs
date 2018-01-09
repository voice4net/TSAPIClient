// ATTQueryUcidConfParser.cs
using System;
using NLog;
using TSAPIClient.ATT;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class ATTQueryUcidConfParser : IATTEventParser
    {
        public ATTEvent_t Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                object result;

                logger.Info("ATTQueryUcidConfParser.Parse: eventType=ATT_QUERY_UCID_CONF");
                logger.Info("ATTQueryUcidConfParser.Parse: try to read the ATTQueryUcidConfEvent_t event...");

                if (reader.TryReadStruct(typeof(ATTQueryUcidConfEvent_t), out result))
                {
                    logger.Info("ATTQueryUcidConfParser.Parse: successfully read the ATTQueryUcidConfEvent_t event!");

                    ATTQueryUcidConfEvent_t queryUCID = (ATTQueryUcidConfEvent_t)result;

                    logger.Info("ATTQueryUcidConfParser.Parse: ucid={0}", queryUCID.ucid.value);

                    ATTEvent_t attEvent = new ATTEvent_t() { eventType = (ushort)eventType };

                    attEvent.u.queryUCID = queryUCID;

                    return attEvent;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in ATTQueryUcidConfParser.Parse: {0}", err));
            }

            return null;
        }

        public int eventType
        {
            get { return Constants.ATT_QUERY_UCID_CONF; }
        }
    }
}
