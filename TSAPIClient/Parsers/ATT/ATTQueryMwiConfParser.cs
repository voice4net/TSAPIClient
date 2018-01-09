// ATTQueryMwiConfParser.cs
using System;
using NLog;
using TSAPIClient.ATT;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class ATTQueryMwiConfParser : IATTEventParser
    {
        public ATTEvent_t Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                logger.Info("ATTQueryMwiConfParser.Parse: eventType=ATT_QUERY_MWI_CONF");

                object result;

                if (reader.TryReadStruct(typeof(ATTQueryMwiConfEvent_t), out result))
                {
                    ATTQueryMwiConfEvent_t queryMwi = (ATTQueryMwiConfEvent_t)result;

                    ATTEvent_t attEvent = new ATTEvent_t() { eventType = (ushort)eventType };

                    attEvent.u.queryMwi = queryMwi;

                    return attEvent;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in ATTQueryMwiConfParser.Parse: {0}", err));
            }

            return null;
        }

        public int eventType
        {
            get { return Constants.ATT_QUERY_MWI_CONF; }
        }
    }
}
