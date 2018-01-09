// ATTDeliveredParser.cs
using System;
using System.Text;
using NLog;
using TSAPIClient.ATT;
using TSAPIClient.CSTA;
using TSAPIClient.Readers;
using Constants = TSAPIClient.ATT.Constants;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class ATTDeliveredParser : IATTEventParser
    {
        private static readonly Logger logger = LogManager.GetLogger("TSAPIClient");

        public ATTEvent_t Parse(IStructReader reader)
        {  
            try
            {
                logger.Info("ATTDeliveredParser.Parse: eventType=ATT_DELIVERED");

                ATTDeliveredEvent_t deliveredEvent = new ATTDeliveredEvent_t
                {
                    deliveredType = ReadDeliveredType(reader),
                    trunkGroup = ReadTrunkGroup(reader),
                    trunkMember = ReadTrunkMember(reader),
                    split = ReadSplit(reader),
                    lookaheadInfo = ReadLookaheadInfo(reader),
                    userEnteredCode = ReadUserEnteredCode(reader),
                    userInfo = ReadUserToUserInfo(reader),
                    reason = ReadReason(reader),
                    originalCallInfo = ReadOriginalCallInfo(reader),
                    distributingDevice = ReadDistributingDevice(reader),
                    ucid = ReadUCID(reader),
                    callOriginatorInfo = ReadCallOriginatorInfo(reader),
                    flexibleBilling = ReadFlexibleBilling(reader),
                    deviceHistory = ReadDeviceHistory(reader),
                    distributingVDN = ReadDistributingVDN(reader)
                };

                ATTEvent_t attEvent = new ATTEvent_t() { eventType = (ushort)eventType };

                attEvent.u.deliveredEvent = deliveredEvent;

                return attEvent;                
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in ATTDeliveredParser.Parse: {0}", err));
            }

            return null;
        }

        private ATTDeliveredType_t ReadDeliveredType(IStructReader reader)
        {
            try
            {
                logger.Info("ATTDeliveredParser.ReadDeliveredType: read delivered type from stream...");

                ATTDeliveredType_t deliveredType = new ATTDeliveredType_t();

                int intDeliveredType = reader.ReadInt32();

                logger.Info("ATTDeliveredParser.ReadDeliveredType: DeliveredType={0}", intDeliveredType);

                if (Enum.IsDefined(typeof(ATTDeliveredType_t), intDeliveredType))
                {
                    deliveredType = (ATTDeliveredType_t)intDeliveredType;

                    logger.Info("ATTDeliveredParser.ReadDeliveredType: deliveredType={0}", deliveredType);                   
                }

                return deliveredType;
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in ATTDeliveredParser.ReadDeliveredType: {0}", err));
            }

            return new ATTDeliveredType_t();
        }

        private DeviceID_t ReadTrunkGroup(IStructReader reader)
        {
            try
            {
                object result;

                logger.Info("ATTDeliveredParser.ReadTrunkGroup: read DeviceID_t from stream...");

                if (reader.TryReadStruct(typeof(DeviceID_t), out result))
                {
                    logger.Info("ATTDeliveredParser.ReadTrunkGroup: successfully read the DeviceID_t!");

                    DeviceID_t trunkGroup = (DeviceID_t)result;

                    logger.Info("ATTDeliveredParser.ReadTrunkGroup: trunkGroup.Device={0}", trunkGroup.device);

                    return trunkGroup;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in ATTDeliveredParser.ReadTrunkGroup: {0}", err));
            }

            return new DeviceID_t();
        }

        private DeviceID_t ReadTrunkMember(IStructReader reader)
        {
            try
            {
                object result;

                logger.Info("ATTDeliveredParser.ReadTrunkMember: read DeviceID_t from stream...");

                if (reader.TryReadStruct(typeof(DeviceID_t), out result))
                {
                    logger.Info("ATTDeliveredParser.ReadTrunkMember: successfully read the DeviceID_t!");

                    DeviceID_t trunkMember = (DeviceID_t)result;

                    logger.Info("ATTDeliveredParser.ReadTrunkMember: trunkMember.Device={0}", trunkMember.device);

                    return trunkMember;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in ATTDeliveredParser.ReadTrunkMember: {0}", err));
            }

            return new DeviceID_t();
        }

        private DeviceID_t ReadSplit(IStructReader reader)
        {
            try
            {
                object result;

                logger.Info("ATTDeliveredParser.ReadSplit: read DeviceID_t from stream...");

                if (reader.TryReadStruct(typeof(DeviceID_t), out result))
                {
                    logger.Info("ATTDeliveredParser.ReadSplit: successfully read the DeviceID_t!");

                    DeviceID_t split = (DeviceID_t)result;

                    logger.Info("ATTDeliveredParser.ReadSplit: trunkMember.Device={0}", split.device);

                    return split;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in ATTDeliveredParser.ReadSplit: {0}", err));
            }

            return new DeviceID_t();
        }

        private ATTLookaheadInfo_t ReadLookaheadInfo(IStructReader reader)
        {
            try
            {
                object result;

                logger.Info("ATTDeliveredParser.ReadLookaheadInfo: read look ahead info from stream...");
                if (reader.TryReadStruct(typeof(ATTLookaheadInfo_t), out result))
                {
                    ATTLookaheadInfo_t lookaheadInfo = (ATTLookaheadInfo_t)result;

                    logger.Info("ATTDeliveredParser.ReadLookaheadInfo: lookaheadInfo.type={0};lookaheadInfo.priority={1};lookaheadInfo.hours={2};lookaheadInfo.minutes={3};lookaheadInfo.seconds={4};lookaheadInfo.sourceVDN={5};lookaheadInfo.uSourceVDN.count={6};lookaheadInfo.uSourceVDN.value={7};", lookaheadInfo.type, lookaheadInfo.priority, lookaheadInfo.hours, lookaheadInfo.minutes, lookaheadInfo.seconds, lookaheadInfo.sourceVDN.device, lookaheadInfo.uSourceVDN.count, lookaheadInfo.uSourceVDN.value);

                    return lookaheadInfo;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in ATTDeliveredParser.ReadLookaheadInfo: {0}", err));
            }

            return new ATTLookaheadInfo_t();
        }

        private ATTUserEnteredCode_t ReadUserEnteredCode(IStructReader reader)
        {
            try
            {
                object result;

                logger.Info("ATTDeliveredParser.ReadUserEnteredCode: read user entered code from stream...");
                if (reader.TryReadStruct(typeof(ATTUserEnteredCode_t), out result))
                {
                    ATTUserEnteredCode_t userEnteredCode = (ATTUserEnteredCode_t)result;

                    logger.Info("ATTDeliveredParser.ReadUserEnteredCode: userEnteredCode.type={0};userEnteredCode.indicator={1};userEnteredCode.data={2};userEnteredCode.collectVDN={3};", userEnteredCode.type, userEnteredCode.indicator, userEnteredCode.data, userEnteredCode.collectVDN.device);

                    return userEnteredCode;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in ATTDeliveredParser.ReadUserEnteredCode: {0}", err));
            }

            return new ATTUserEnteredCode_t();
        }

        private ATTUserToUserInfo_t ReadUserToUserInfo(IStructReader reader)
        {
            try
            {
                object result;

                logger.Info("ATTDeliveredParser.ReadUserToUserInfo: try to read user to user info from stream...");

                if (reader.TryReadStruct(typeof(ATTUserToUserInfo_t), out result))
                {
                    logger.Info("ATTDeliveredParser.ReadUserToUserInfo: successfully read user to user info from stream!");

                    ATTUserToUserInfo_t userInfo = (ATTUserToUserInfo_t)result;

                    logger.Info("ATTDeliveredParser.ReadUserToUserInfo: userInfo.type={0};userInfo.length={1};userInfo.data={2};", userInfo.type, userInfo.length, Encoding.Default.GetString(userInfo.data));

                    return userInfo;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in ATTDeliveredParser.ReadUserToUserInfo: {0}", err));
            }

            return new ATTUserToUserInfo_t();
        }

        private ATTReasonCode_t ReadReason(IStructReader reader)
        {
            try
            {
                logger.Info("ATTDeliveredParser.ReadReason: read reason code from stream...");
                int code = reader.ReadInt32();

                logger.Info("ATTDeliveredParser.ReadReason: code={0}", code);

                if (Enum.IsDefined(typeof(ATTReasonCode_t), code))
                {
                    ATTReasonCode_t reason = (ATTReasonCode_t)code;

                    logger.Info("ATTDeliveredParser.ReadReason: reason={0}", reason);

                    return reason;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in ATTDeliveredParser.ReadReason: {0}", err));
            }

            return ATTReasonCode_t.AR_NONE;
        }

        private ATTOriginalCallInfo_t ReadOriginalCallInfo(IStructReader reader)
        {
            try
            {
                object result;

                ATTOriginalCallInfo_t originalCallInfo = new ATTOriginalCallInfo_t();

                logger.Info("ATTDeliveredParser.ReadOriginalCallInfo: read reason code from stream...");
                int intReason = reader.ReadInt32();

                logger.Info("ATTDeliveredParser.ReadOriginalCallInfo: ReasonCode={0}", intReason);

                if (Enum.IsDefined(typeof(ATTReasonForCallInfo_t), intReason))
                {
                    ATTReasonForCallInfo_t reason = (ATTReasonForCallInfo_t)intReason;

                    logger.Info("ATTDeliveredParser.ReadOriginalCallInfo: reason={0}", reason);

                    originalCallInfo.reason = reason;
                }

                logger.Info("ATTDeliveredParser.ReadOriginalCallInfo: read calling device from stream...");
                if (reader.TryReadStruct(typeof(CallingDeviceID_t), out result))
                {
                    CallingDeviceID_t callingDevice = (CallingDeviceID_t)result;

                    logger.Info("ATTDeliveredParser.ReadOriginalCallInfo: callingDevice.deviceID={0};callingDevice.deviceIDType={1};callingDevice.deviceIDStatus={2};", callingDevice.value.deviceID.device, callingDevice.value.deviceIDType, callingDevice.value.deviceIDStatus);

                    originalCallInfo.callingDevice = callingDevice;
                }

                logger.Info("ATTDeliveredParser.ReadOriginalCallInfo: read called device from stream...");
                if (reader.TryReadStruct(typeof(CalledDeviceID_t), out result))
                {
                    CalledDeviceID_t calledDevice = (CalledDeviceID_t)result;

                    logger.Info("ATTDeliveredParser.ReadOriginalCallInfo: calledDevice.deviceID={0};calledDevice.deviceIDType={1};calledDevice.deviceIDStatus={2};", calledDevice.value.deviceID.device, calledDevice.value.deviceIDType, calledDevice.value.deviceIDStatus);

                    originalCallInfo.calledDevice = calledDevice;
                }

                logger.Info("ATTDeliveredParser.ReadOriginalCallInfo: read trunk group from stream...");
                if (reader.TryReadStruct(typeof(DeviceID_t), out result))
                {
                    DeviceID_t trunkGroup = (DeviceID_t)result;

                    logger.Info("ATTDeliveredParser.ReadOriginalCallInfo: trunkGroup={0}", trunkGroup.device);

                    originalCallInfo.trunkGroup = trunkGroup;
                }

                logger.Info("ATTDeliveredParser.ReadOriginalCallInfo: read trunk member from stream...");
                if (reader.TryReadStruct(typeof(DeviceID_t), out result))
                {
                    DeviceID_t trunkMember = (DeviceID_t)result;

                    logger.Info("ATTDeliveredParser.ReadOriginalCallInfo: trunkMember={0}", trunkMember.device);

                    originalCallInfo.trunkMember = trunkMember;
                }

                logger.Info("ATTDeliveredParser.ReadOriginalCallInfo: read look ahead info from stream...");
                if (reader.TryReadStruct(typeof(ATTLookaheadInfo_t), out result))
                {
                    ATTLookaheadInfo_t lookaheadInfo = (ATTLookaheadInfo_t)result;

                    logger.Info("ATTDeliveredParser.ReadOriginalCallInfo: lookaheadInfo.type={0};lookaheadInfo.priority={1};lookaheadInfo.hours={2};lookaheadInfo.minutes={3};lookaheadInfo.seconds={4};lookaheadInfo.sourceVDN={5};lookaheadInfo.uSourceVDN.count={6};lookaheadInfo.uSourceVDN.value={7};", lookaheadInfo.type, lookaheadInfo.priority, lookaheadInfo.hours, lookaheadInfo.minutes, lookaheadInfo.seconds, lookaheadInfo.sourceVDN.device, lookaheadInfo.uSourceVDN.count, lookaheadInfo.uSourceVDN.value);

                    originalCallInfo.lookaheadInfo = lookaheadInfo;
                }

                logger.Info("ATTDeliveredParser.ReadOriginalCallInfo: read user entered code from stream...");
                if (reader.TryReadStruct(typeof(ATTUserEnteredCode_t), out result))
                {
                    ATTUserEnteredCode_t userEnteredCode = (ATTUserEnteredCode_t)result;

                    logger.Info("ATTDeliveredParser.ReadOriginalCallInfo: userEnteredCode.type={0};userEnteredCode.indicator={1};userEnteredCode.data={2};userEnteredCode.collectVDN={3};", userEnteredCode.type, userEnteredCode.indicator, userEnteredCode.data, userEnteredCode.collectVDN.device);

                    originalCallInfo.userEnteredCode = userEnteredCode;
                }

                logger.Info("ATTDeliveredParser.ReadOriginalCallInfo: read user to user info from stream...");
                if (reader.TryReadStruct(typeof(ATTUserToUserInfo_t), out result))
                {
                    ATTUserToUserInfo_t userInfo = (ATTUserToUserInfo_t)result;

                    logger.Info("ATTDeliveredParser.ReadOriginalCallInfo: userInfo.type={0};userInfo.length={1};userInfo.data={2};", userInfo.type, userInfo.length, Encoding.Default.GetString(userInfo.data));

                    originalCallInfo.userInfo = userInfo;
                }

                logger.Info("ATTDeliveredParser.ReadOriginalCallInfo: read ucid from stream...");
                if (reader.TryReadStruct(typeof(ATTUCID_t), out result))
                {
                    ATTUCID_t ucid = (ATTUCID_t)result;

                    logger.Info("ATTDeliveredParser.ReadOriginalCallInfo: ucid={0}", ucid.value);

                    originalCallInfo.ucid = ucid;
                }

                logger.Info("ATTDeliveredParser.ReadOriginalCallInfo: read call originator info from stream...");
                if (reader.TryReadStruct(typeof(ATTCallOriginatorInfo_t), out result))
                {
                    ATTCallOriginatorInfo_t callOriginatorInfo = (ATTCallOriginatorInfo_t)result;

                    logger.Info("ATTDeliveredParser.ReadOriginalCallInfo: callOriginatorInfo.hasInfo={0};callOriginatorInfo.callOriginatorType={1};", callOriginatorInfo.hasInfo, callOriginatorInfo.callOriginatorType);

                    originalCallInfo.callOriginatorInfo = callOriginatorInfo;
                }

                logger.Info("ATTDeliveredParser.ReadOriginalCallInfo: read flexible billing from stream...");
                bool flexibleBilling = reader.ReadBoolean();

                logger.Info("ATTDeliveredParser.ReadOriginalCallInfo: flexibleBilling={0}", flexibleBilling);

                originalCallInfo.flexibleBilling = flexibleBilling;

                logger.Info("ATTDeliveredParser.ReadOriginalCallInfo: advance base stream 3 positions due to pack size of 4...");
                reader.BaseStream.Position += 3;

                DeviceHistory_t deviceHistory = new DeviceHistory_t();

                logger.Info("ATTDeliveredParser.ReadOriginalCallInfo: read device history count from stream...");
                uint count = reader.ReadUInt32();

                logger.Info("ATTDeliveredParser.ReadOriginalCallInfo: count={0}", count);

                if (count == 1)
                {
                    reader.BaseStream.Position += 4;

                    logger.Info("ATTDeliveredParser.ReadDeviceHistory: try to read a device history entry from the stream...");

                    if (reader.TryReadStruct(typeof(DeviceHistoryEntry_t), out result))
                    {
                        logger.Info("ATTDeliveredParser.ReadDeviceHistory: successfully read device history entry from stream!");

                        DeviceHistoryEntry_t deviceHistoryEntry = (DeviceHistoryEntry_t)result;

                        logger.Info("ATTDeliveredParser.ReadDeviceHistory: deviceHistoryEntry.olddeviceID={0};deviceHistoryEntry.cause={1};deviceHistoryEntry.oldconnectionID.callID={2};deviceHistoryEntry.oldconnectionID.deviceID={3};", deviceHistoryEntry.olddeviceID.device, deviceHistoryEntry.cause, deviceHistoryEntry.oldconnectionID.callID, deviceHistoryEntry.oldconnectionID.deviceID.device);

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
                logger.Error(string.Format("Error in ATTDeliveredParser.ReadOriginalCallInfo: {0}", err));
            }

            return new ATTOriginalCallInfo_t();
        }

        private CalledDeviceID_t ReadDistributingDevice(IStructReader reader)
        {
            try
            {
                object result;

                logger.Info("ATTDeliveredParser.ReadDistributingDevice: read CalledDeviceID_t from stream...");

                if (reader.TryReadStruct(typeof(CalledDeviceID_t), out result))
                {
                    logger.Info("ATTDeliveredParser.ReadDistributingDevice: successfully read the CalledDeviceID_t!");

                    CalledDeviceID_t distributingDevice = (CalledDeviceID_t)result;

                    logger.Info("ATTDeliveredParser.ReadDistributingDevice: distributingDevice.deviceID={0};distributingDevice.deviceIDType={1};distributingDevice.deviceIDStatus={2};", distributingDevice.value.deviceID.device, distributingDevice.value.deviceIDType, distributingDevice.value.deviceIDStatus);

                    return distributingDevice;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in ATTDeliveredParser.ReadDistributingDevice: {0}", err));
            }

            return new CalledDeviceID_t();
        }

        private ATTUCID_t ReadUCID(IStructReader reader)
        {
            try
            {
                object result;

                logger.Info("ATTDeliveredParser.ReadUCID: read ATTUCID_t from stream...");

                if (reader.TryReadStruct(typeof(ATTUCID_t), out result))
                {
                    logger.Info("ATTDeliveredParser.ReadUCID: successfully read the ATTUCID_t!");

                    ATTUCID_t ucid = (ATTUCID_t)result;

                    logger.Info("ATTDeliveredParser.ReadUCID: ucid={0};", ucid.value);

                    return ucid;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in ATTDeliveredParser.ReadUCID: {0}", err));
            }

            return new ATTUCID_t();
        }

        private ATTCallOriginatorInfo_t ReadCallOriginatorInfo(IStructReader reader)
        {
            try
            {
                object result;

                logger.Info("ATTDeliveredParser.ReadCallOriginatorInfo: read call originator info from stream...");

                if (reader.TryReadStruct(typeof(ATTCallOriginatorInfo_t), out result))
                {
                    ATTCallOriginatorInfo_t callOriginatorInfo = (ATTCallOriginatorInfo_t)result;

                    logger.Info("ATTDeliveredParser.ReadCallOriginatorInfo: callOriginatorInfo.hasInfo={0};callOriginatorInfo.callOriginatorType={1};", callOriginatorInfo.hasInfo, callOriginatorInfo.callOriginatorType);

                    return callOriginatorInfo;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in ATTDeliveredParser.ReadCallOriginatorInfo: {0}", err));
            }

            return new ATTCallOriginatorInfo_t();
        }

        private bool ReadFlexibleBilling(IStructReader reader)
        {
            try
            {
                logger.Info("ATTDeliveredParser.ReadFlexibleBilling: read flexible billing from stream...");
                bool flexibleBilling = reader.ReadBoolean();

                logger.Info("ATTDeliveredParser.ReadFlexibleBilling: flexibleBilling={0}", flexibleBilling);

                logger.Info("ATTDeliveredParser.ReadFlexibleBilling: advance base stream 3 positions due to pack size of 4...");
                reader.BaseStream.Position += 3;

                return flexibleBilling;
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in ATTDeliveredParser.ReadFlexibleBilling: {0}", err));
            }

            return false;
        }

        private DeviceHistory_t ReadDeviceHistory(IStructReader reader)
        {
            try
            {
                DeviceHistory_t deviceHistory = new DeviceHistory_t();

                logger.Info("ATTDeliveredParser.ReadDeviceHistory: read device history count from stream...");
                uint count = reader.ReadUInt32();

                logger.Info("ATTDeliveredParser.ReadDeviceHistory: count={0}", count);

                if (count == 1)
                {
                    reader.BaseStream.Position += 4;

                    object result;

                    logger.Info("ATTDeliveredParser.ReadDeviceHistory: try to read a device history entry from the stream...");

                    if (reader.TryReadStruct(typeof(DeviceHistoryEntry_t), out result))
                    {
                        logger.Info("ATTDeliveredParser.ReadDeviceHistory: successfully read device history entry from stream!");

                        DeviceHistoryEntry_t deviceHistoryEntry = (DeviceHistoryEntry_t)result;

                        logger.Info("ATTDeliveredParser.ReadDeviceHistory: deviceHistoryEntry.olddeviceID={0};deviceHistoryEntry.oldconnectionID.callID={1};deviceHistoryEntry.oldconnectionID.deviceID={2};", deviceHistoryEntry.olddeviceID.device, deviceHistoryEntry.oldconnectionID.callID, deviceHistoryEntry.oldconnectionID.deviceID.device);

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
                logger.Error(string.Format("Error in ATTDeliveredParser.ReadDeviceHistory: {0}", err));
            }

            return new DeviceHistory_t() { count = 0, deviceHistoryList = new DeviceHistoryEntry_t[] { } };
        }

        private CalledDeviceID_t ReadDistributingVDN(IStructReader reader)
        {
            try
            {
                object result;

                logger.Info("ATTDeliveredParser.ReadDistributingVDN: read CalledDeviceID_t from stream...");

                if (reader.TryReadStruct(typeof(CalledDeviceID_t), out result))
                {
                    logger.Info("ATTDeliveredParser.ReadDistributingVDN: successfully read the CalledDeviceID_t!");

                    CalledDeviceID_t distributingVDN = (CalledDeviceID_t)result;

                    logger.Info("ATTDeliveredParser.ReadDistributingVDN: distributingVDN.deviceID={0};distributingVDN.deviceIDType={1};distributingVDN.deviceIDStatus={2};", distributingVDN.value.deviceID.device, distributingVDN.value.deviceIDType, distributingVDN.value.deviceIDStatus);

                    return distributingVDN;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in ATTDeliveredParser.ReadDistributingVDN: {0}", err));
            }

            return new CalledDeviceID_t();
        }

        public int eventType
        {
            get { return Constants.ATT_DELIVERED; }
        }
    }
}
