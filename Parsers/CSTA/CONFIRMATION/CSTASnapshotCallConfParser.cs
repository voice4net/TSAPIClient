using System;
using System.Collections.Generic;
using NLog;
using TSAPIClient.CSTA;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class CSTASnapshotCallConfParser : ICSTAConfirmationParser
    {
        private static readonly Logger logger = LogManager.GetLogger("TSAPIClient");

        public CSTAConfirmationEvent Parse(IStructReader reader)
        {
            try
            {
                logger.Info("CSTASnapshotCallConfParser.Parse: eventType=CSTA_SNAPSHOT_CALL_CONF");

                CSTASnapshotCallConfEvent_t snapshotCall = new CSTASnapshotCallConfEvent_t
                {
                    snapshotData = ReadSnapshotCallData(reader)
                };

                CSTAConfirmationEvent cstaConfirmation = new CSTAConfirmationEvent { u = { snapshotCall = snapshotCall } };

                return cstaConfirmation;
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in CSTASnapshotCallConfParser.Parse: {0}", err));
            }

            return null;
        }

        private CSTASnapshotCallData_t ReadSnapshotCallData(IStructReader reader)
        {
            try
            {
                logger.Info("CSTASnapshotCallConfParser.ReadSnapshotCallData: read count from stream...");
                uint count = reader.ReadUInt32();

                logger.Info("CSTASnapshotCallConfParser.ReadSnapshotCallData: count={0}", count);

                logger.Info("CSTASnapshotCallConfParser.ReadSnapshotCallData: advance the base stream 4 positions...");
                reader.BaseStream.Position = 20;

                List<CSTASnapshotCallResponseInfo_t> infoList = new List<CSTASnapshotCallResponseInfo_t>();

                for (int i = 0; i < count; i++)
                {
                    object result;

                    logger.Info("CSTASnapshotCallConfParser.ReadSnapshotCallData: read snapshot call info from the stream...");

                    if (reader.TryReadStruct(typeof(CSTASnapshotCallResponseInfo_t), out result))
                    {
                        logger.Info("CSTASnapshotCallConfParser.ReadSnapshotCallData: successfully read snapshot call info from the stream!");

                        CSTASnapshotCallResponseInfo_t info = (CSTASnapshotCallResponseInfo_t)result;

                        logger.Info("CSTASnapshotCallConfParser.ReadSnapshotCallData: deviceOnCall.deviceID.device={0};deviceOnCall.deviceIDType={1};deviceOnCall.deviceIDStatus={2};callIdentifier.callID={3};callIdentifier.deviceID.device={4};callIdentifier.devIDType={5};localConnectionState={6};", info.deviceOnCall.deviceID.device, info.deviceOnCall.deviceIDType, info.deviceOnCall.deviceIDStatus, info.callIdentifier.callID, info.callIdentifier.deviceID.device, info.callIdentifier.devIDType, info.localConnectionState);

                        infoList.Add(info);
                    }
                }

                return new CSTASnapshotCallData_t() { count = count, info = infoList.ToArray() };
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in CSTASnapshotCallConfParser.ReadSnapshotCallData: {0}", err));
            }

            return new CSTASnapshotCallData_t() { count = 0, info = new CSTASnapshotCallResponseInfo_t[] { } };
        }

        public int eventType
        {
            get { return Constants.CSTA_SNAPSHOT_CALL_CONF; }
        }
    }
}
