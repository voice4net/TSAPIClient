// ATTQueryAgentLoginConfParser.cs
using System;
using NLog;
using TSAPIClient.ATT;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class ATTQueryAgentLoginConfParser : IATTEventParser
    {
        public ATTEvent_t Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                logger.Info("ATTQueryAgentLoginConfParser.Parse: eventType=ATT_QUERY_AGENT_LOGIN_CONF");

                object result;

                if (reader.TryReadStruct(typeof(ATTQueryAgentLoginConfEvent_t), out result))
                {
                    ATTQueryAgentLoginConfEvent_t queryAgentLogin = (ATTQueryAgentLoginConfEvent_t)result;

                    ATTEvent_t attEvent = new ATTEvent_t() { eventType = (ushort)eventType };

                    attEvent.u.queryAgentLogin = queryAgentLogin;

                    return attEvent;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in ATTQueryAgentLoginConfParser.Parse: {0}", err));
            }

            return null;
        }

        public int eventType
        {
            get { return Constants.ATT_QUERY_AGENT_LOGIN_CONF; }
        }
    }
}
