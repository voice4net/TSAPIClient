// ATTRouteRequestParser.cs
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
    public class ATTRouteRequestParser : IATTEventParser
    {
        private static readonly Logger logger = LogManager.GetLogger("TSAPIClient");

        public ATTEvent_t Parse(IStructReader reader)
        {
            try
            {
                logger.Info("ATTRouteRequestParser.Parse: eventType=ATT_ROUTE_REQUEST");

                ATTRouteRequestEvent_t routeRequest = new ATTRouteRequestEvent_t
                {
                    trunkGroup = ReadTrunkGroup(reader),
                    lookaheadInfo = ReadLookaheadInfo(reader),
                    userEnteredCode = ReadUserEnteredCode(reader),
                    userInfo = ReadUserToUserInfo(reader),
                    ucid = ReadUCID(reader),
                    callOriginatorInfo = ReadCallOriginatorInfo(reader),
                    flexibleBilling = ReadFlexibleBilling(reader),
                    trunkMember = ReadTrunkMember(reader),
                    deviceHistory = ReadDeviceHistory(reader)
                };

                ATTEvent_t attEvent = new ATTEvent_t() { eventType = (ushort)eventType };

                attEvent.u.routeRequest = routeRequest;

                return attEvent;
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in ATTRouteRequestParser.Parse: {0}", err));
            }

            return null;
        }

        private DeviceID_t ReadTrunkGroup(IStructReader reader)
        {
            try
            {
                object result;

                logger.Info("ATTRouteRequestParser.ReadTrunkGroup: read trunk group from stream...");
                if (reader.TryReadStruct(typeof(DeviceID_t), out result))
                {
                    DeviceID_t trunkGroup = (DeviceID_t)result;

                    logger.Info("ATTRouteRequestParser.ReadTrunkGroup: trunkGroup={0}", trunkGroup.device);

                    return trunkGroup;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in ATTRouteRequestParser.ReadTrunkGroup: {0}", err));
            }

            return new DeviceID_t();
        }

        private ATTLookaheadInfo_t ReadLookaheadInfo(IStructReader reader)
        {
            try
            {
                object result;

                logger.Info("ATTRouteRequestParser.ReadLookaheadInfo: read look ahead info from stream...");
                if (reader.TryReadStruct(typeof(ATTLookaheadInfo_t), out result))
                {
                    ATTLookaheadInfo_t lookaheadInfo = (ATTLookaheadInfo_t)result;

                    logger.Info("ATTRouteRequestParser.ReadLookaheadInfo: lookaheadInfo.type={0};lookaheadInfo.priority{1};lookaheadInfo.hours={2};lookaheadInfo.minutes={3};lookaheadInfo.seconds={4};lookaheadInfo.sourceVDN={5};lookaheadInfo.uSourceVDN.count={6};lookaheadInfo.uSourceVDN.value={7};", lookaheadInfo.type, lookaheadInfo.priority, lookaheadInfo.hours, lookaheadInfo.minutes, lookaheadInfo.seconds, lookaheadInfo.sourceVDN.device, lookaheadInfo.uSourceVDN.count, lookaheadInfo.uSourceVDN.value);

                    return lookaheadInfo;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in ATTRouteRequestParser.ReadLookaheadInfo: {0}", err));
            }

            return new ATTLookaheadInfo_t();
        }

        private ATTUserEnteredCode_t ReadUserEnteredCode(IStructReader reader)
        {
            try
            {
                object result;

                logger.Info("ATTRouteRequestParser.ReadUserEnteredCode: read user entered code from stream...");
                if (reader.TryReadStruct(typeof(ATTUserEnteredCode_t), out result))
                {
                    ATTUserEnteredCode_t userEnteredCode = (ATTUserEnteredCode_t)result;

                    logger.Info("ATTRouteRequestParser.ReadUserEnteredCode: userEnteredCode.type={0};userEnteredCode.indicator={1};userEnteredCode.data={2};userEnteredCode.collectVDN={3};", userEnteredCode.type, userEnteredCode.indicator, userEnteredCode.data, userEnteredCode.collectVDN.device);

                    return userEnteredCode;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in ATTRouteRequestParser.ReadUserEnteredCode: {0}", err));
            }

            return new ATTUserEnteredCode_t();
        }

        private ATTUserToUserInfo_t ReadUserToUserInfo(IStructReader reader)
        {
            try
            {
                object result;

                logger.Info("ATTRouteRequestParser.ReadUserToUserInfo: try to read user to user info from stream...");

                if (reader.TryReadStruct(typeof(ATTUserToUserInfo_t), out result))
                {
                    logger.Info("ATTRouteRequestParser.ReadUserToUserInfo: successfully read user to user info from stream!");

                    ATTUserToUserInfo_t userInfo = (ATTUserToUserInfo_t)result;

                    logger.Info("ATTRouteRequestParser.ReadUserToUserInfo: userInfo.type={0};userInfo.length={1};userInfo.data={2};", userInfo.type, userInfo.length, Encoding.Default.GetString(userInfo.data));

                    return userInfo;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in ATTRouteRequestParser.ReadUserToUserInfo: {0}", err));
            }

            return new ATTUserToUserInfo_t();
        }

        private ATTUCID_t ReadUCID(IStructReader reader)
        {
            try
            {
                object result;

                logger.Info("ATTRouteRequestParser.ReadUCID: read ATTUCID_t from stream...");

                if (reader.TryReadStruct(typeof(ATTUCID_t), out result))
                {
                    logger.Info("ATTRouteRequestParser.ReadUCID: successfully read the ATTUCID_t!");

                    ATTUCID_t ucid = (ATTUCID_t)result;

                    logger.Info("ATTRouteRequestParser.ReadUCID: ucid={0};", ucid.value);

                    return ucid;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in ATTRouteRequestParser.ReadUCID: {0}", err));
            }

            return new ATTUCID_t();
        }

        private ATTCallOriginatorInfo_t ReadCallOriginatorInfo(IStructReader reader)
        {
            try
            {
                object result;

                logger.Info("ATTRouteRequestParser.ReadCallOriginatorInfo: read call originator info from stream...");
                
                if (reader.TryReadStruct(typeof(ATTCallOriginatorInfo_t), out result))
                {
                    ATTCallOriginatorInfo_t callOriginatorInfo = (ATTCallOriginatorInfo_t)result;

                    logger.Info("ATTRouteRequestParser.ReadCallOriginatorInfo: callOriginatorInfo.hasInfo={0};callOriginatorInfo.callOriginatorType={1};", callOriginatorInfo.hasInfo, callOriginatorInfo.callOriginatorType);

                    return callOriginatorInfo;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in ATTRouteRequestParser.ReadCallOriginatorInfo: {0}", err));
            }

            return new ATTCallOriginatorInfo_t();
        }

        private bool ReadFlexibleBilling(IStructReader reader)
        {
            try
            {
                logger.Info("ATTRouteRequestParser.ReadFlexibleBilling: read flexible billing from stream...");
                bool flexibleBilling = reader.ReadBoolean();

                logger.Info("ATTRouteRequestParser.ReadFlexibleBilling: flexibleBilling={0}", flexibleBilling);

                logger.Info("ATTRouteRequestParser.ReadFlexibleBilling: advance base stream 3 positions due to pack size of 4...");
                reader.BaseStream.Position += 3;

                return flexibleBilling;
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in ATTRouteRequestParser.ReadFlexibleBilling: {0}", err));
            }

            return false;
        }

        private DeviceID_t ReadTrunkMember(IStructReader reader)
        {
            try
            {
                object result;

                logger.Info("ATTRouteRequestParser.ReadTrunkMember: read trunk group from stream...");
                if (reader.TryReadStruct(typeof(DeviceID_t), out result))
                {
                    DeviceID_t trunkMember = (DeviceID_t)result;

                    logger.Info("ATTRouteRequestParser.ReadTrunkMember: trunkMember={0}", trunkMember.device);

                    return trunkMember;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in ATTRouteRequestParser.ReadTrunkMember: {0}", err));
            }

            return new DeviceID_t();
        }

        private DeviceHistory_t ReadDeviceHistory(IStructReader reader)
        {
            try
            {
                DeviceHistory_t deviceHistory = new DeviceHistory_t();

                logger.Info("ATTRouteRequestParser.ReadDeviceHistory: read device history count from stream...");
                uint count = reader.ReadUInt32();

                logger.Info("ATTRouteRequestParser.ReadDeviceHistory: count={0}", count);

                if (count == 1)
                {
                    reader.BaseStream.Position += 4;

                    object result;

                    logger.Info("ATTRouteRequestParser.ReadDeviceHistory: try to read a device history entry from the stream...");

                    if (reader.TryReadStruct(typeof(DeviceHistoryEntry_t), out result))
                    {
                        logger.Info("ATTRouteRequestParser.ReadDeviceHistory: successfully read device history entry from stream!");

                        DeviceHistoryEntry_t deviceHistoryEntry = (DeviceHistoryEntry_t)result;

                        logger.Info("ATTRouteRequestParser.ReadDeviceHistory: deviceHistoryEntry.olddeviceID={0};deviceHistoryEntry.oldconnectionID.callID={1};deviceHistoryEntry.oldconnectionID.deviceID={2};", deviceHistoryEntry.olddeviceID.device, deviceHistoryEntry.oldconnectionID.callID, deviceHistoryEntry.oldconnectionID.deviceID.device);

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
                logger.Error(string.Format("Error in ATTRouteRequestParser.ReadDeviceHistory: {0}", err));
            }

            return new DeviceHistory_t() { count = 0, deviceHistoryList = new DeviceHistoryEntry_t[] { } };
        }

        public int eventType
        {
            get { return Constants.ATT_ROUTE_REQUEST; }
        }
    }
}
