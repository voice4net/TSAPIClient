using System;
using NLog;
using TSAPIClient.CSTA;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class CSTAQueryAgentStateConfParser : ICSTAConfirmationParser
    {
        public CSTAConfirmationEvent Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                object result;

                logger.Info("CSTAQueryAgentStateConfParser.Parse: eventType=CSTA_QUERY_AGENT_STATE_CONF");
                logger.Info("CSTAQueryAgentStateConfParser.Parse: try to read the CSTAQueryAgentStateConfEvent_t confirmation event...");

                if (reader.TryReadStruct(typeof(CSTAQueryAgentStateConfEvent_t), out result))
                {
                    logger.Info("CSTAQueryAgentStateConfParser.Parse: successfully read the CSTAQueryAgentStateConfEvent_t confirmation event!");

                    CSTAQueryAgentStateConfEvent_t queryAgentState = (CSTAQueryAgentStateConfEvent_t)result;

                    CSTAConfirmationEvent cstaConfirmation = new CSTAConfirmationEvent
                    {
                        u = {queryAgentState = queryAgentState}
                    };

                    return cstaConfirmation;
                }

                return null;
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in CSTAQueryAgentStateConfParser.Parse: {0}", err));
            }

            return null;
        }

        public int eventType
        {
            get { return Constants.CSTA_QUERY_AGENT_STATE_CONF; }
        }
    }
}
