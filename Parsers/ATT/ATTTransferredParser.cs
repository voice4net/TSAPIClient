// ATTTransferredParser.cs
using System;
using System.Collections.Generic;
using System.Text;
using NLog;
using TSAPIClient.ATT;
using TSAPIClient.CSTA;
using TSAPIClient.Readers;
using Constants = TSAPIClient.ATT.Constants;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class ATTTransferredParser : IATTEventParser
    {
        private static readonly Logger logger = LogManager.GetLogger("TSAPIClient");

        public ATTEvent_t Parse(IStructReader reader)
        {
            try
            {
                logger.Info("ATTTransferredParser.Parse: eventType=ATT_TRANSFERRED");

                ATTTransferredEvent_t transferredEvent = new ATTTransferredEvent_t
                {
                    originalCallInfo = ReadOriginalCallInfo(reader),
                    distributingDevice = ReadDistributingDevice(reader),
                    ucid = ReadUCID(reader),
                    trunkList = ReadTrunkList(reader),
                    deviceHistory = ReadDeviceHistory(reader),
                    distributingVDN = ReadDistributingVDN(reader)
                };

                ATTEvent_t attEvent = new ATTEvent_t() { eventType = (ushort)eventType };

                attEvent.u.transferredEvent = transferredEvent;

                return attEvent;
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in ATTTransferredParser.Parse: {0}", err));
            }

            return null;
        }

        private ATTOriginalCallInfo_t ReadOriginalCallInfo(IStructReader reader)
        {
            try
            {
                object result;

                ATTOriginalCallInfo_t originalCallInfo = new ATTOriginalCallInfo_t();

                logger.Info("ATTTransferredParser.ReadOriginalCallInfo: read reason code from stream...");
                int intReason = reader.ReadInt32();

                logger.Info("ATTTransferredParser.ReadOriginalCallInfo: ReasonCode={0}", intReason);

                if (Enum.IsDefined(typeof(ATTReasonForCallInfo_t), intReason))
                {
                    ATTReasonForCallInfo_t reason = (ATTReasonForCallInfo_t)intReason;

                    logger.Info("ATTTransferredParser.ReadOriginalCallInfo: reason={0}", reason);

                    originalCallInfo.reason = reason;
                }

                logger.Info("ATTTransferredParser.ReadOriginalCallInfo: read calling device from stream...");
                if (reader.TryReadStruct(typeof(CallingDeviceID_t), out result))
                {
                    CallingDeviceID_t callingDevice = (CallingDeviceID_t)result;

                    logger.Info("ATTTransferredParser.ReadOriginalCallInfo: callingDevice.deviceID={0};callingDevice.deviceIDType={1};callingDevice.deviceIDStatus={2};", callingDevice.value.deviceID.device, callingDevice.value.deviceIDType, callingDevice.value.deviceIDStatus);

                    originalCallInfo.callingDevice = callingDevice;
                }

                logger.Info("ATTTransferredParser.ReadOriginalCallInfo: read called device from stream...");
                if (reader.TryReadStruct(typeof(CalledDeviceID_t), out result))
                {
                    CalledDeviceID_t calledDevice = (CalledDeviceID_t)result;

                    logger.Info("ATTTransferredParser.ReadOriginalCallInfo: calledDevice.deviceID={0};calledDevice.deviceIDType={1};calledDevice.deviceIDStatus={2};", calledDevice.value.deviceID.device, calledDevice.value.deviceIDType, calledDevice.value.deviceIDStatus);

                    originalCallInfo.calledDevice = calledDevice;
                }

                logger.Info("ATTTransferredParser.ReadOriginalCallInfo: read trunk group from stream...");
                if (reader.TryReadStruct(typeof(DeviceID_t), out result))
                {
                    DeviceID_t trunkGroup = (DeviceID_t)result;

                    logger.Info("ATTTransferredParser.ReadOriginalCallInfo: trunkGroup={0}", trunkGroup.device);

                    originalCallInfo.trunkGroup = trunkGroup;
                }

                logger.Info("ATTTransferredParser.ReadOriginalCallInfo: read trunk member from stream...");
                if (reader.TryReadStruct(typeof(DeviceID_t), out result))
                {
                    DeviceID_t trunkMember = (DeviceID_t)result;

                    logger.Info("ATTTransferredParser.ReadOriginalCallInfo: trunkMember={0}", trunkMember.device);

                    originalCallInfo.trunkMember = trunkMember;
                }

                logger.Info("ATTTransferredParser.ReadOriginalCallInfo: read look ahead info from stream...");
                if (reader.TryReadStruct(typeof(ATTLookaheadInfo_t), out result))
                {
                    ATTLookaheadInfo_t lookaheadInfo = (ATTLookaheadInfo_t)result;

                    logger.Info("ATTTransferredParser.ReadOriginalCallInfo: lookaheadInfo.type={0};lookaheadInfo.priority={1};lookaheadInfo.hours={2};lookaheadInfo.minutes={3};lookaheadInfo.seconds={4};lookaheadInfo.sourceVDN={5};lookaheadInfo.uSourceVDN.count={6};lookaheadInfo.uSourceVDN.value={7};", lookaheadInfo.type, lookaheadInfo.priority, lookaheadInfo.hours, lookaheadInfo.minutes, lookaheadInfo.seconds, lookaheadInfo.sourceVDN.device, lookaheadInfo.uSourceVDN.count, lookaheadInfo.uSourceVDN.value);

                    originalCallInfo.lookaheadInfo = lookaheadInfo;
                }

                logger.Info("ATTTransferredParser.ReadOriginalCallInfo: read user entered code from stream...");
                if (reader.TryReadStruct(typeof(ATTUserEnteredCode_t), out result))
                {
                    ATTUserEnteredCode_t userEnteredCode = (ATTUserEnteredCode_t)result;

                    logger.Info("ATTTransferredParser.ReadOriginalCallInfo: userEnteredCode.type={0};userEnteredCode.indicator={1};userEnteredCode.data={2};userEnteredCode.collectVDN={3};", userEnteredCode.type, userEnteredCode.indicator, userEnteredCode.data, userEnteredCode.collectVDN.device);

                    originalCallInfo.userEnteredCode = userEnteredCode;
                }

                logger.Info("ATTTransferredParser.ReadOriginalCallInfo: read user to user info from stream...");
                if (reader.TryReadStruct(typeof(ATTUserToUserInfo_t), out result))
                {
                    ATTUserToUserInfo_t userInfo = (ATTUserToUserInfo_t)result;

                    logger.Info("ATTTransferredParser.ReadOriginalCallInfo: userInfo.type={0};userInfo.length={1};userInfo.data={2};", userInfo.type, userInfo.length, Encoding.Default.GetString(userInfo.data));

                    originalCallInfo.userInfo = userInfo;
                }

                logger.Info("ATTTransferredParser.ReadOriginalCallInfo: read ucid from stream...");
                if (reader.TryReadStruct(typeof(ATTUCID_t), out result))
                {
                    ATTUCID_t ucid = (ATTUCID_t)result;

                    logger.Info("ATTTransferredParser.ReadOriginalCallInfo: ucid={0}", ucid.value);

                    originalCallInfo.ucid = ucid;
                }

                logger.Info("ATTTransferredParser.ReadOriginalCallInfo: read call originator info from stream...");
                if (reader.TryReadStruct(typeof(ATTCallOriginatorInfo_t), out result))
                {
                    ATTCallOriginatorInfo_t callOriginatorInfo = (ATTCallOriginatorInfo_t)result;

                    logger.Info("ATTTransferredParser.ReadOriginalCallInfo: callOriginatorInfo.hasInfo={0};callOriginatorInfo.callOriginatorType={1};", callOriginatorInfo.hasInfo, callOriginatorInfo.callOriginatorType);

                    originalCallInfo.callOriginatorInfo = callOriginatorInfo;
                }

                logger.Info("ATTTransferredParser.ReadOriginalCallInfo: read flexible billing from stream...");
                bool flexibleBilling = reader.ReadBoolean();

                logger.Info("ATTTransferredParser.ReadOriginalCallInfo: flexibleBilling={0}", flexibleBilling);

                originalCallInfo.flexibleBilling = flexibleBilling;

                logger.Info("ATTTransferredParser.ReadOriginalCallInfo: advance base stream 3 positions due to pack size of 4...");
                reader.BaseStream.Position += 3;

                DeviceHistory_t deviceHistory = new DeviceHistory_t();

                logger.Info("ATTTransferredParser.ReadOriginalCallInfo: read device history count from stream...");
                uint count = reader.ReadUInt32();

                logger.Info("ATTTransferredParser.ReadOriginalCallInfo: count={0}", count);

                if (count == 1)
                {
                    reader.BaseStream.Position += 4;

                    logger.Info("ATTTransferredParser.ReadDeviceHistory: try to read a device history entry from the stream...");

                    if (reader.TryReadStruct(typeof(DeviceHistoryEntry_t), out result))
                    {
                        logger.Info("ATTTransferredParser.ReadDeviceHistory: successfully read device history entry from stream!");

                        DeviceHistoryEntry_t deviceHistoryEntry = (DeviceHistoryEntry_t)result;

                        logger.Info("ATTTransferredParser.ReadDeviceHistory: deviceHistoryEntry.olddeviceID={0};deviceHistoryEntry.oldconnectionID.callID={1};deviceHistoryEntry.oldconnectionID.deviceID={2};", deviceHistoryEntry.olddeviceID.device, deviceHistoryEntry.oldconnectionID.callID, deviceHistoryEntry.oldconnectionID.deviceID.device);

                        deviceHistory = new DeviceHistory_t()
                        {
                            count = 1,
                            deviceHistoryList = new DeviceHistoryEntry_t[] { deviceHistoryEntry }
                        };
                    }
                }
                else
                {
                    deviceHistory = new DeviceHistory_t()
                    {
                        count = 0,
                        deviceHistoryList = new DeviceHistoryEntry_t[] { }
                    };
                }

                originalCallInfo.deviceHistory = deviceHistory;

                return originalCallInfo;
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in ATTTransferredParser.ReadOriginalCallInfo: {0}", err));
            }

            return new ATTOriginalCallInfo_t();
        }

        private CalledDeviceID_t ReadDistributingDevice(IStructReader reader)
        {
            try
            {
                object result;

                logger.Info("ATTTransferredParser.ReadDistributingDevice: read CalledDeviceID_t from stream...");

                if (reader.TryReadStruct(typeof(CalledDeviceID_t), out result))
                {
                    logger.Info("ATTTransferredParser.ReadDistributingDevice: successfully read the CalledDeviceID_t!");

                    CalledDeviceID_t distributingDevice = (CalledDeviceID_t)result;

                    logger.Info("ATTTransferredParser.ReadDistributingDevice: distributingDevice.deviceID={0};distributingDevice.deviceIDType={1};distributingDevice.deviceIDStatus={2};", distributingDevice.value.deviceID.device, distributingDevice.value.deviceIDType, distributingDevice.value.deviceIDStatus);

                    return distributingDevice;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in ATTTransferredParser.ReadDistributingDevice: {0}", err));
            }

            return new CalledDeviceID_t();
        }

        private ATTUCID_t ReadUCID(IStructReader reader)
        {
            try
            {
                object result;

                logger.Info("ATTTransferredParser.ReadUCID: read ATTUCID_t from stream...");

                if (reader.TryReadStruct(typeof(ATTUCID_t), out result))
                {
                    logger.Info("ATTTransferredParser.ReadUCID: successfully read the ATTUCID_t!");

                    ATTUCID_t ucid = (ATTUCID_t)result;

                    logger.Info("ATTTransferredParser.ReadUCID: ucid={0};", ucid.value);

                    return ucid;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in ATTTransferredParser.ReadUCID: {0}", err));
            }

            return new ATTUCID_t();
        }

        private ATTTrunkList_t ReadTrunkList(IStructReader reader)
        {
            try
            {
                logger.Info("ATTTransferredParser.ReadTrunkList: read trunk list count from stream...");
                short count = reader.ReadInt16();

                logger.Info("ATTTransferredParser.ReadTrunkList: count={0}", count);

                logger.Info("ATTTransferredParser.ReadTrunkList: advance the base stream 2 positions...");
                reader.BaseStream.Position += 2;

                List<ATTTrunkInfo_t> trunks = new List<ATTTrunkInfo_t>();

                for (int i = 0; i < count; i++)
                {
                    object result;

                    logger.Info("ATTTransferredParser.ReadTrunkList: read trunk info from the stream...");

                    if (reader.TryReadStruct(typeof(ATTTrunkInfo_t), out result))
                    {
                        logger.Info("ATTTransferredParser.ReadTrunkList: successfully read trunk info from the stream!");

                        ATTTrunkInfo_t trunkInfo = (ATTTrunkInfo_t)result;

                        logger.Info("ATTTransferredParser.ReadTrunkList: trunkInfo.connection.callID={0};trunkInfo.connection.deviceID.device={1};trunkInfo.connection.devIDType={2};trunkInfo.trunkGroup.device={3};trunkInfo.trunkMember.device={4};", trunkInfo.connection.callID, trunkInfo.connection.deviceID.device, trunkInfo.connection.devIDType, trunkInfo.trunkGroup.device, trunkInfo.trunkMember.device);

                        trunks.Add(trunkInfo);
                    }
                }

                return new ATTTrunkList_t() { count = count, trunks = trunks.ToArray() };
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in ATTTransferredParser.ReadTrunkList: {0}", err));
            }

            return new ATTTrunkList_t() { count = 0, trunks = new ATTTrunkInfo_t[] { } };
        }

        private DeviceHistory_t ReadDeviceHistory(IStructReader reader)
        {
            try
            {
                DeviceHistory_t deviceHistory = new DeviceHistory_t();

                logger.Info("ATTTransferredParser.ReadDeviceHistory: read device history count from stream...");
                uint count = reader.ReadUInt32();

                logger.Info("ATTTransferredParser.ReadDeviceHistory: count={0}", count);

                if (count == 1)
                {
                    reader.BaseStream.Position += 4;

                    object result;

                    logger.Info("ATTTransferredParser.ReadDeviceHistory: try to read a device history entry from the stream...");

                    if (reader.TryReadStruct(typeof(DeviceHistoryEntry_t), out result))
                    {
                        logger.Info("ATTTransferredParser.ReadDeviceHistory: successfully read device history entry from stream!");

                        DeviceHistoryEntry_t deviceHistoryEntry = (DeviceHistoryEntry_t)result;

                        logger.Info("ATTTransferredParser.ReadDeviceHistory: deviceHistoryEntry.olddeviceID={0};deviceHistoryEntry.cause={1};deviceHistoryEntry.oldconnectionID.callID={2};deviceHistoryEntry.oldconnectionID.deviceID={3};", deviceHistoryEntry.olddeviceID.device, deviceHistoryEntry.cause, deviceHistoryEntry.oldconnectionID.callID, deviceHistoryEntry.oldconnectionID.deviceID.device);

                        deviceHistory = new DeviceHistory_t()
                        {
                            count = 1,
                            deviceHistoryList = new DeviceHistoryEntry_t[] { deviceHistoryEntry }
                        };
                    }
                }
                else
                {
                    deviceHistory = new DeviceHistory_t()
                    {
                        count = 0,
                        deviceHistoryList = new DeviceHistoryEntry_t[] { }
                    };
                }

                return deviceHistory;
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in ATTTransferredParser.ReadDeviceHistory: {0}", err));
            }

            return new DeviceHistory_t() { count = 0, deviceHistoryList = new DeviceHistoryEntry_t[] { }};
        }

        private CalledDeviceID_t ReadDistributingVDN(IStructReader reader)
        {
            try
            {
                object result;

                logger.Info("ATTTransferredParser.ReadDistributingVDN: read CalledDeviceID_t from stream...");

                if (reader.TryReadStruct(typeof(CalledDeviceID_t), out result))
                {
                    logger.Info("ATTTransferredParser.ReadDistributingVDN: successfully read the CalledDeviceID_t!");

                    CalledDeviceID_t distributingVDN = (CalledDeviceID_t)result;

                    logger.Info("ATTTransferredParser.ReadDistributingVDN: distributingVDN.deviceID={0};distributingVDN.deviceIDType={1};distributingVDN.deviceIDStatus={2};", distributingVDN.value.deviceID.device, distributingVDN.value.deviceIDType, distributingVDN.value.deviceIDStatus);

                    return distributingVDN;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in ATTTransferredParser.ReadDistributingVDN: {0}", err));
            }

            return new CalledDeviceID_t();
        }

        public int eventType
        {
            get { return Constants.ATT_TRANSFERRED; }
        }
    }
}
