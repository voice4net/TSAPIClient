using System;
using NLog;
using TSAPIClient.CSTA;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class CSTASetAgentStateConfParser : ICSTAConfirmationParser
    {
        public CSTAConfirmationEvent Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                object result;

                logger.Info("CSTASetAgentStateConfParser.Parse: eventType=CSTA_SET_AGENT_STATE_CONF");
                logger.Info("CSTASetAgentStateConfParser.Parse: try to read the CSTASetAgentStateConfEvent_t confirmation event...");

                if (reader.TryReadStruct(typeof(CSTASetAgentStateConfEvent_t), out result))
                {
                    logger.Info("CSTASetAgentStateConfParser.Parse: successfully read the CSTASetAgentStateConfEvent_t confirmation event!");

                    CSTASetAgentStateConfEvent_t setAgentState = (CSTASetAgentStateConfEvent_t)result;

                    CSTAConfirmationEvent cstaConfirmation = new CSTAConfirmationEvent
                    {
                        u = {setAgentState = setAgentState}
                    };

                    return cstaConfirmation;
                }

                return null;
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in CSTASetAgentStateConfParser.Parse: {0}", err));
            }

            return null;
        }

        public int eventType
        {
            get { return Constants.CSTA_SET_AGENT_STATE_CONF; }
        }
    }
}
