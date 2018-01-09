// CSTACallClearedParser.cs
using System;
using System.Text;
using NLog;
using TSAPIClient.CSTA;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class CSTACallClearedParser : ICSTAUnsolicitedParser
    {
        public CSTAUnsolicitedEvent Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                object result;

                logger.Info("CSTACallClearedParser.Parse: eventType=CSTA_CALL_CLEARED");
                logger.Info("CSTACallClearedParser.Parse: try to read the CSTACallClearedEvent_t unsolicited event...");

                if (reader.TryReadStruct(typeof(CSTACallClearedEvent_t), out result))
                {
                    logger.Info("CSTACallClearedParser.Parse: successfully read the CSTACallClearedEvent_t unsolicited event!");

                    CSTACallClearedEvent_t callCleared = (CSTACallClearedEvent_t)result;

                    StringBuilder bldr = new StringBuilder();

                    bldr.Append("CSTACallClearedParser.Parse: ");
                    bldr.AppendFormat("clearedCall.callID={0};", callCleared.clearedCall.callID);
                    bldr.AppendFormat("clearedCall.deviceID={0};", callCleared.clearedCall.deviceID.device);
                    bldr.AppendFormat("clearedCall.devIDType={0};", callCleared.clearedCall.devIDType);
                    bldr.AppendFormat("localConnectionInfo={0};", callCleared.localConnectionInfo);
                    bldr.AppendFormat("cause={0};", callCleared.cause);

                    logger.Info(bldr.ToString());

                    CSTAUnsolicitedEvent cstaUnsolicited = new CSTAUnsolicitedEvent { u = { callCleared = callCleared } };

                    return cstaUnsolicited;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in CSTACallClearedParser.Parse: {0}", err));
            }

            return null;
        }

        public int eventType
        {
            get { return Constants.CSTA_CALL_CLEARED; }
        }
    }
}