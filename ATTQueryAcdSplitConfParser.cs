// ATTQueryAcdSplitConfParser.cs
using System;
using NLog;
using TSAPIClient.ATT;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class ATTQueryAcdSplitConfParser : IATTEventParser
    {
        public ATTEvent_t Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                logger.Info("ATTQueryAcdSplitConfParser.Parse: eventType=ATT_QUERY_ACD_SPLIT_CONF");

                object result;

                if (reader.TryReadStruct(typeof(ATTQueryAcdSplitConfEvent_t), out result))
                {
                    logger.Info("ATTQueryAcdSplitConfParser.Parse: successfully parsed ATTQueryAcdSplitConfEvent_t!");

                    ATTQueryAcdSplitConfEvent_t queryAcdSplit = (ATTQueryAcdSplitConfEvent_t)result;

                    logger.Info("ATTQueryAcdSplitConfParser.Parse: queryAcdSplit.agentsLoggedIn={0};queryAcdSplit.availableAgents={1};queryAcdSplit.callsInQueue={2};", queryAcdSplit.agentsLoggedIn, queryAcdSplit.availableAgents, queryAcdSplit.callsInQueue);

                    ATTEvent_t attEvent = new ATTEvent_t() { eventType = (ushort)eventType };

                    attEvent.u.queryAcdSplit = queryAcdSplit;

                    return attEvent;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in ATTQueryAcdSplitConfParser.Parse: {0}", err));
            }

            return null;
        }

        public int eventType
        {
            get { return Constants.ATT_QUERY_ACD_SPLIT_CONF; }
        }
    }
}
