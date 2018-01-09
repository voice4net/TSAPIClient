// ATTQueryTodConfParser.cs
using System;
using NLog;
using TSAPIClient.ATT;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class ATTQueryTodConfParser : IATTEventParser
    {
        public ATTEvent_t Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                logger.Info("ATTQueryTodConfParser.Parse: eventType=ATT_QUERY_TOD_CONF");

                object result;

                if (reader.TryReadStruct(typeof(ATTQueryTodConfEvent_t), out result))
                {
                    ATTQueryTodConfEvent_t queryTOD = (ATTQueryTodConfEvent_t)result;

                    ATTEvent_t attEvent = new ATTEvent_t() { eventType = (ushort)eventType };

                    attEvent.u.queryTOD = queryTOD;

                    return attEvent;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in ATTQueryTodConfParser.Parse: {0}", err));
            }

            return null;
        }

        public int eventType
        {
            get { return Constants.ATT_QUERY_TOD_CONF; }
        }
    }
}
