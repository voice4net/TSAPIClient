// CSTAConferencedParser.cs
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using NLog;
using TSAPIClient.CSTA;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class CSTAConferencedParser : ICSTAUnsolicitedParser
    {
        private static readonly Logger logger = LogManager.GetLogger("TSAPIClient");

        public CSTAUnsolicitedEvent Parse(IStructReader reader)
        {
            try
            {    
                logger.Info("CSTAConferencedParser.Parse: eventType=CSTA_CONFERENCED");

                CSTAConferencedEvent_t conferenced = new CSTAConferencedEvent_t
                {
                    primaryOldCall = ReadPrimaryOldCall(reader),
                    secondaryOldCall = ReadSecondaryOldCall(reader),
                    confController = ReadConfController(reader),
                    addedParty = ReadAddedParty(reader),
                    conferenceConnections = ReadConferenceConnections(reader),
                    localConnectionInfo = ReadLocalConnectionInfo(reader),
                    cause = ReadCause(reader)
                };

                CSTAUnsolicitedEvent cstaUnsolicited = new CSTAUnsolicitedEvent { u = { conferenced = conferenced } };

                return cstaUnsolicited;                
            }
            catch (Exception err)
            {
                logger.Error(string.Concat("Error in CSTAConferencedParser.Parse: ", err.ToString()));
            }

            return null;
        }

        private ConnectionID_t ReadPrimaryOldCall(IStructReader reader)
        {
            try
            {
                object result;

                logger.Info("CSTAConferencedParser.ReadPrimaryOldCall: try to read primary old call from stream...");

                if (reader.TryReadStruct(typeof(ConnectionID_t), out result))
                {
                    logger.Info("CSTAConferencedParser.ReadPrimaryOldCall: successfully read primary old call from stream!");

                    ConnectionID_t primaryOldCall = (ConnectionID_t)result;

                    logger.Info("CSTAConferencedParser.ReadPrimaryOldCall: primaryOldCall.callID={0};primaryOldCall.deviceID.device={1};primaryOldCall.devIDType={2};", primaryOldCall.callID, primaryOldCall.deviceID.device, primaryOldCall.devIDType);

                    return primaryOldCall;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Concat("Error in CSTAConferencedParser.ReadPrimaryOldCall: ", err.ToString()));
            }

            return new ConnectionID_t();
        }

        private ConnectionID_t ReadSecondaryOldCall(IStructReader reader)
        {
            try
            {
                object result;

                logger.Info("CSTAConferencedParser.ReadSecondaryOldCall: try to read secondary old call from stream...");

                if (reader.TryReadStruct(typeof(ConnectionID_t), out result))
                {
                    logger.Info("CSTAConferencedParser.ReadSecondaryOldCall: successfully read secondary old call from stream!");

                    ConnectionID_t secondaryOldCall = (ConnectionID_t)result;

                    logger.Info("CSTAConferencedParser.ReadSecondaryOldCall: secondaryOldCall.callID={0};secondaryOldCall.deviceID.device={1};secondaryOldCall.devIDType={2};", secondaryOldCall.callID, secondaryOldCall.deviceID.device, secondaryOldCall.devIDType);

                    return secondaryOldCall;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Concat("Error in CSTAConferencedParser.ReadSecondaryOldCall: ", err.ToString()));
            }

            return new ConnectionID_t();
        }

        private ExtendedDeviceID_t ReadConfController(IStructReader reader)
        {
            try
            {
                object result;

                logger.Info("CSTAConferencedParser.ReadConfController: try to read conf controller from stream...");

                if (reader.TryReadStruct(typeof(ExtendedDeviceID_t), out result))
                {
                    logger.Info("CSTAConferencedParser.ReadConfController: successfully read conf controller from stream!");

                    ExtendedDeviceID_t confController = (ExtendedDeviceID_t)result;

                    logger.Info("CSTAConferencedParser.ReadConfController: confController.deviceID.device={0};confController.deviceIDType={1};confController.deviceIDStatus={2}", confController.deviceID.device, confController.deviceIDType, confController.deviceIDStatus);

                    return confController;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in CSTAConferencedParser.ReadConfController: {0}", err));
            }

            return new ExtendedDeviceID_t();
        }

        private ExtendedDeviceID_t ReadAddedParty(IStructReader reader)
        {
            try
            {
                logger.Info("CSTAConferencedParser.ReadAddedParty: try to read added party from stream...");

                object result;

                if (reader.TryReadStruct(typeof(ExtendedDeviceID_t), out result))
                {
                    logger.Info("CSTAConferencedParser.ReadAddedParty: successfully read added party from stream!");

                    ExtendedDeviceID_t addedParty = (ExtendedDeviceID_t)result;

                    logger.Info("CSTAConferencedParser.ReadAddedParty: addedParty.deviceID.device={0};addedParty.deviceIDType={1};addedParty.deviceIDStatus={2}", addedParty.deviceID.device, addedParty.deviceIDType, addedParty.deviceIDStatus);

                    return addedParty;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in CSTAConferencedParser.ReadAddedParty: {0}", err));
            }

            return new ExtendedDeviceID_t();
        }

        private ConnectionList_t ReadConferenceConnections(IStructReader reader)
        {
            try
            {
                logger.Info("CSTAConferencedParser.ReadConferenceConnections: BaseStream.Position={0};BaseStream.Length={1};", reader.BaseStream.Position, reader.BaseStream.Length);

                if ((reader.BaseStream.Length - reader.BaseStream.Position) >= 4)
                {
                    logger.Info("CSTAConferencedParser.ReadConferenceConnections: read count of conference connections...");

                    uint count = reader.ReadUInt32();

                    logger.Info("CSTAConferencedParser.ReadConferenceConnections: count={0}", count);

                    List<Connection_t> connections = new List<Connection_t>();

                    if (count > 0)
                    {
                        if ((reader.BaseStream.Length - reader.BaseStream.Position) >= 12)
                        {
                            logger.Info("CSTAConferencedParser.ReadConferenceConnections: advance the base stream 12 positions...");
                            reader.BaseStream.Position += 12;
                        }

                        int size = Marshal.SizeOf(typeof(Connection_t));

                        for (int i = 0; i < count; i++)
                        {
                            logger.Info("CSTAConferencedParser.ReadConferenceConnections: try to read connection from stream...");

                            if ((reader.BaseStream.Length - reader.BaseStream.Position) >= size)
                            {
                                object result;

                                if (reader.TryReadStruct(typeof(Connection_t), out result))
                                {
                                    logger.Info("CSTAConferencedParser.ReadConferenceConnections: successfully read connection from stream!");

                                    Connection_t connection = (Connection_t)result;

                                    logger.Info("CSTAConferencedParser.ReadConferenceConnections: connection.party.callID={0};connection.party.deviceID.device={1};connection.party.devIDType={2};connection.staticDevice.deviceID.device={3};connection.staticDevice.deviceIDType={4};connection.staticDevice.deviceIDType={4};", connection.party.callID, connection.party.deviceID.device, connection.party.devIDType, connection.staticDevice.deviceID.device, connection.staticDevice.deviceIDType);
                                    connections.Add(connection);
                                }
                            }
                        }
                    }

                    return new ConnectionList_t() { count = count, connections = connections.ToArray() };
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Concat("Error in CSTAConferencedParser.ReadConferenceConnections: ", err.ToString()));
            }

            return new ConnectionList_t() { count = 0, connections = new Connection_t[] { } };
        }

        private LocalConnectionState_t ReadLocalConnectionInfo(IStructReader reader)
        {
            try
            {
                logger.Info("CSTAConferencedParser.ReadLocalConnectionInfo: BaseStream.Position={0};BaseStream.Length={1};", reader.BaseStream.Position, reader.BaseStream.Length);

                if ((reader.BaseStream.Length - reader.BaseStream.Position) >= 4)
                {
                    logger.Info("CSTAConferencedParser.ReadLocalConnectionInfo: read local connection state from stream...");
                    int value = reader.ReadInt32();

                    logger.Info("CSTAConferencedParser.ReadLocalConnectionInfo: value={0}", value);

                    if (Enum.IsDefined(typeof(LocalConnectionState_t), value))
                    {
                        LocalConnectionState_t localConnectionInfo = (LocalConnectionState_t)value;

                        logger.Info("CSTAConferencedParser.ReadLocalConnectionInfo: localConnectionInfo={0}", localConnectionInfo);

                        return localConnectionInfo;
                    }
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in CSTAConferencedParser.ReadLocalConnectionInfo: {0}", err));
            }

            return LocalConnectionState_t.CS_NONE;
        }

        private CSTAEventCause_t ReadCause(IStructReader reader)
        {
            try
            {
                logger.Info("CSTAConferencedParser.ReadCause: BaseStream.Position={0};BaseStream.Length={1};", reader.BaseStream.Position, reader.BaseStream.Length);

                if ((reader.BaseStream.Length - reader.BaseStream.Position) >= 4)
                {
                    logger.Info("CSTAConferencedParser.ReadCause: read event cause from stream...");
                    int value = reader.ReadInt32();

                    logger.Info("CSTAConferencedParser.ReadCause: value={0}", value);

                    if (Enum.IsDefined(typeof(CSTAEventCause_t), value))
                    {
                        CSTAEventCause_t cause = (CSTAEventCause_t)value;

                        logger.Info("CSTAConferencedParser.ReadCause: cause={0}", cause);

                        return cause;
                    }
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in CSTAConferencedParser.ReadCause: {0}", err));
            }

            return CSTAEventCause_t.EC_NONE;
        }

        public int eventType
        {
            get { return Constants.CSTA_CONFERENCED; }
        }
    }
}
