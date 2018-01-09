// CSTAQueuedParser.cs
using System;
using System.Text;
using NLog;
using TSAPIClient.CSTA;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class CSTAQueuedParser : ICSTAUnsolicitedParser
    {
        public CSTAUnsolicitedEvent Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                object result;

                logger.Info("CSTAQueuedParser.Parse: eventType=CSTA_QUEUED");
                logger.Info("CSTAQueuedParser.Parse: try to read the CSTAQueuedEvent_t unsolicited event...");

                if (reader.TryReadStruct(typeof(CSTAQueuedEvent_t), out result))
                {
                    logger.Info("CSTAQueuedParser.Parse: successfully read the CSTAQueuedEvent_t unsolicited event!");

                    CSTAQueuedEvent_t queued = (CSTAQueuedEvent_t)result;

                    StringBuilder bldr = new StringBuilder();

                    bldr.Append("CSTAQueuedParser.Parse: ");
                    bldr.AppendFormat("queuedConnection.callID={0};", queued.queuedConnection.callID);
                    bldr.AppendFormat("queuedConnection.deviceID={0};", queued.queuedConnection.deviceID.device);
                    bldr.AppendFormat("queuedConnection.devIDType={0};", queued.queuedConnection.devIDType);
                    bldr.AppendFormat("queue.deviceID={0};", queued.queue.deviceID.device);
                    bldr.AppendFormat("queue.deviceIDType={0};", queued.queue.deviceIDType);
                    bldr.AppendFormat("queue.deviceIDStatus={0};", queued.queue.deviceIDStatus);
                    bldr.AppendFormat("callingDevice.deviceID={0};", queued.callingDevice.deviceID.device);
                    bldr.AppendFormat("callingDevice.deviceIDType={0};", queued.callingDevice.deviceIDType);
                    bldr.AppendFormat("callingDevice.deviceIDStatus={0};", queued.callingDevice.deviceIDStatus);
                    bldr.AppendFormat("calledDevice.deviceID={0};", queued.calledDevice.deviceID.device);
                    bldr.AppendFormat("calledDevice.deviceIDType={0};", queued.calledDevice.deviceIDType);
                    bldr.AppendFormat("calledDevice.deviceIDStatus={0};", queued.calledDevice.deviceIDStatus);
                    bldr.AppendFormat("lastRedirectionDevice.deviceID={0};", queued.lastRedirectionDevice.deviceID.device);
                    bldr.AppendFormat("lastRedirectionDevice.deviceIDType={0};", queued.lastRedirectionDevice.deviceIDType);
                    bldr.AppendFormat("lastRedirectionDevice.deviceIDStatus={0};", queued.lastRedirectionDevice.deviceIDStatus);
                    bldr.AppendFormat("numberQueued={0};", queued.numberQueued);
                    bldr.AppendFormat("localConnectionInfo={0};", queued.localConnectionInfo);
                    bldr.AppendFormat("cause={0};", queued.cause);

                    logger.Info(bldr.ToString());

                    CSTAUnsolicitedEvent cstaUnsolicited = new CSTAUnsolicitedEvent { u = { queued = queued } };

                    return cstaUnsolicited;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in CSTAQueuedParser.Parse: {0}", err));
            }

            return null;
        }

        public int eventType
        {
            get { return Constants.CSTA_QUEUED; }
        }
    }
}
