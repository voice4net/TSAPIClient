// ATTMonitorCallConfParser.cs
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using NLog;
using TSAPIClient.ATT;
using TSAPIClient.CSTA;
using TSAPIClient.Readers;
using Constants = TSAPIClient.ATT.Constants;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class ATTMonitorCallConfParser : IATTEventParser
    {
        private static readonly Logger logger = LogManager.GetLogger("TSAPIClient");

        public ATTEvent_t Parse(IStructReader reader)
        { 
            try
            {
                logger.Info("ATTMonitorCallConfParser.Parse: eventType=ATT_MONITOR_CALL_CONF");

                ATTMonitorCallConfEvent_t monitorCallStart = new ATTMonitorCallConfEvent_t
                {
                    usedFilter = ReadUsedFilter(reader),
                    snapshotCall = ReadSnapshotCall(reader)
                };

                ATTEvent_t attEvent = new ATTEvent_t() { eventType = (ushort)eventType };

                attEvent.u.monitorCallStart = monitorCallStart;

                return attEvent;
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in ATTMonitorCallConfParser.Parse: {0}", err));
            }

            return null;
        }

        private ATTPrivateFilter_t ReadUsedFilter(IStructReader reader)
        {
            ATTPrivateFilter_t userFilter = new ATTPrivateFilter_t();

            try
            {
                logger.Info("ATTMonitorCallConfParser.ReadUsedFilter: BaseStream.Position={0};BaseStream.Length={1};", reader.BaseStream.Position, reader.BaseStream.Length);

                if ((reader.BaseStream.Length - reader.BaseStream.Position) >= 1)
                {
                    logger.Info("ATTMonitorCallConfParser.ReadUsedFilter: read private filter from stream...");
                    byte filter = reader.ReadByte();

                    logger.Info("ATTMonitorCallConfParser.ReadUsedFilter: filter={0}", filter);

                    userFilter.filter = filter;
                }

                if ((reader.BaseStream.Length - reader.BaseStream.Position) >= 3)
                {
                    logger.Info("ATTMonitorCallConfParser.ReadUsedFilter: advance base stream 3 positions...");
                    reader.BaseStream.Position += 3;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in ATTMonitorCallConfParser.ReadUsedFilter: {0}", err));
            }

            return userFilter;
        }

        private ATTSnapshotCall_t ReadSnapshotCall(IStructReader reader)
        {
            try
            {
                if ((reader.BaseStream.Length - reader.BaseStream.Position) >= 4)
                {
                    logger.Info("ATTMonitorCallConfParser.ReadSnapshotCall: read count from stream...");
                    uint count = reader.ReadUInt32();

                    logger.Info("ATTMonitorCallConfParser.ReadSnapshotCall: count={0}", count);

                    List<CSTASnapshotCallResponseInfo_t> snapshotCalls = new List<CSTASnapshotCallResponseInfo_t>();

                    if (count > 0)
                    {
                        if ((reader.BaseStream.Length - reader.BaseStream.Position) >= 8)
                        {
                            logger.Info("ATTMonitorCallConfParser.ReadSnapshotCall: advance the base stream 8 positions...");
                            reader.BaseStream.Position += 8;
                        }

                        int size = Marshal.SizeOf(typeof(CSTASnapshotCallResponseInfo_t));

                        for (int i = 0; i < count; i++)
                        {
                            logger.Info("ATTMonitorCallConfParser.ReadSnapshotCall: read snap shot call response from the stream...");

                            if ((reader.BaseStream.Length - reader.BaseStream.Position) >= size)
                            {
                                object result;

                                if (reader.TryReadStruct(typeof(CSTASnapshotCallResponseInfo_t), out result))
                                {
                                    logger.Info("ATTMonitorCallConfParser.ReadSnapshotCall: successfully read snap shot call response from the stream!");

                                    CSTASnapshotCallResponseInfo_t snapshotCallInfo = (CSTASnapshotCallResponseInfo_t)result;

                                    logger.Info("ATTMonitorCallConfParser.ReadSnapshotCall: snapshotCallInfo.deviceOnCall.deviceID.device={0};snapshotCallInfo.deviceOnCall.deviceIDType={1};snapshotCallInfo.deviceOnCall.deviceIDStatus={2};snapshotCallInfo.callIdentifier.callID={3};snapshotCallInfo.callIdentifier.deviceID.device={4};snapshotCallInfo.callIdentifier.devIDType={5};snapshotCallInfo.localConnectionState={6}", snapshotCallInfo.deviceOnCall.deviceID.device, snapshotCallInfo.deviceOnCall.deviceIDType, snapshotCallInfo.deviceOnCall.deviceIDStatus, snapshotCallInfo.callIdentifier.callID, snapshotCallInfo.callIdentifier.deviceID.device, snapshotCallInfo.callIdentifier.devIDType, snapshotCallInfo.localConnectionState);

                                    snapshotCalls.Add(snapshotCallInfo);
                                }
                            }
                        }
                    }

                    return new ATTSnapshotCall_t() { count = count, pInfo = snapshotCalls.ToArray() };
                }                
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in ATTMonitorCallConfParser.ReadSnapshotCall: {0}", err));
            }

            return new ATTSnapshotCall_t();
        }

        public int eventType
        {
            get { return Constants.ATT_MONITOR_CALL_CONF; }
        }
    }
}
