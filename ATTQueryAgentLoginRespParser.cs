// ATTQueryAgentLoginRespParser.cs
using System;
using NLog;
using TSAPIClient.ATT;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class ATTQueryAgentLoginRespParser : IATTEventParser
    {
        public ATTEvent_t Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                logger.Info("ATTQueryAgentLoginRespParser.Parse: eventType=ATT_QUERY_AGENT_LOGIN_RESP");

                object result;

                if (reader.TryReadStruct(typeof(ATTQueryAgentLoginResp_t), out result))
                {
                    ATTQueryAgentLoginResp_t queryAgentLoginResp = (ATTQueryAgentLoginResp_t)result;

                    ATTEvent_t attEvent = new ATTEvent_t() { eventType = (ushort)eventType };

                    attEvent.u.queryAgentLoginResp = queryAgentLoginResp;

                    return attEvent;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in ATTQueryAgentLoginRespParser.Parse: {0}", err));
            }

            return null;
        }

        public int eventType
        {
            get { return Constants.ATT_QUERY_AGENT_LOGIN_RESP; }
        }
    }
}
