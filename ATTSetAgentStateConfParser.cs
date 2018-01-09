// ATTSetAgentStateConfParser.cs
using System;
using NLog;
using TSAPIClient.ATT;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class ATTSetAgentStateConfParser : IATTEventParser
    {
        public ATTEvent_t Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                logger.Info("ATTSetAgentStateConfParser.Parse: eventType=ATT_SET_AGENT_STATE_CONF");

                object result;

                if (reader.TryReadStruct(typeof(ATTSetAgentStateConfEvent_t), out result))
                {
                    logger.Info("ATTSetAgentStateConfParser.Parse: successfully read set agent state confirmation event!");

                    ATTSetAgentStateConfEvent_t setAgentState = (ATTSetAgentStateConfEvent_t)result;

                    logger.Info("ATTSetAgentStateConfParser.Parse:setAgentState.isPending={0};", setAgentState.isPending);

                    ATTEvent_t attEvent = new ATTEvent_t() { eventType = (ushort)eventType };

                    attEvent.u.setAgentState = setAgentState;

                    return attEvent;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in ATTSetAgentStateConfParser.Parse: {0}", err));
            }

            return null;
        }

        public int eventType
        {
            get { return Constants.ATT_SET_AGENT_STATE_CONF; }
        }
    }
}
