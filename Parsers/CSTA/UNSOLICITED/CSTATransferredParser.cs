// CSTATransferredParser.cs
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using NLog;
using TSAPIClient.CSTA;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class CSTATransferredParser : ICSTAUnsolicitedParser
    {
        private static readonly Logger logger = LogManager.GetLogger("TSAPIClient");

        public CSTAUnsolicitedEvent Parse(IStructReader reader)
        {
            try
            {
                logger.Info("CSTATransferredParser.Parse: eventType=CSTA_TRANSFERRED");

                CSTATransferredEvent_t transferred = new CSTATransferredEvent_t
                {
                    primaryOldCall = ReadPrimaryOldCall(reader),
                    secondaryOldCall = ReadSecondaryOldCall(reader),
                    transferringDevice = ReadTransferringDevice(reader),
                    transferredDevice = ReadTransferredDevice(reader),
                    transferredConnections = ReadTransferredConnections(reader),
                    localConnectionInfo = ReadLocalConnectionInfo(reader),
                    cause = ReadCause(reader)
                };

                CSTAUnsolicitedEvent cstaUnsolicited = new CSTAUnsolicitedEvent { u = { transferred = transferred } };

                return cstaUnsolicited;
            }
            catch (Exception err)
            {
                logger.Error(string.Concat("Error in CSTATransferredParser.Parse: ", err.ToString()));
            }

            return null;
        }

        private ConnectionID_t ReadPrimaryOldCall(IStructReader reader)
        {
            try
            {
                object result;

                logger.Info("CSTATransferredParser.ReadPrimaryOldCall: try to read primary old call from stream...");

                if (reader.TryReadStruct(typeof(ConnectionID_t), out result))
                {
                    logger.Info("CSTATransferredParser.ReadPrimaryOldCall: successfully read primary old call from stream!");

                    ConnectionID_t primaryOldCall = (ConnectionID_t)result;

                    logger.Info("CSTATransferredParser.ReadPrimaryOldCall: primaryOldCall.callID={0};primaryOldCall.deviceID.device={1};primaryOldCall.devIDType={2};", primaryOldCall.callID, primaryOldCall.deviceID.device, primaryOldCall.devIDType);

                    return primaryOldCall;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Concat("Error in CSTATransferredParser.ReadPrimaryOldCall: ", err.ToString()));
            }

            return new ConnectionID_t();
        }

        private ConnectionID_t ReadSecondaryOldCall(IStructReader reader)
        {
            try
            {
                object result;

                logger.Info("CSTATransferredParser.ReadSecondaryOldCall: try to read secondary old call from stream...");

                if (reader.TryReadStruct(typeof(ConnectionID_t), out result))
                {
                    logger.Info("CSTATransferredParser.ReadSecondaryOldCall: successfully read secondary old call from stream!");

                    ConnectionID_t secondaryOldCall = (ConnectionID_t)result;

                    logger.Info("CSTATransferredParser.ReadSecondaryOldCall: secondaryOldCall.callID={0};secondaryOldCall.deviceID.device={1};secondaryOldCall.devIDType={2};", secondaryOldCall.callID, secondaryOldCall.deviceID.device, secondaryOldCall.devIDType);

                    return secondaryOldCall;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Concat("Error in CSTATransferredParser.ReadSecondaryOldCall: ", err.ToString()));
            }

            return new ConnectionID_t();
        }

        private ExtendedDeviceID_t ReadTransferringDevice(IStructReader reader)
        {
            try
            {
                object result;

                logger.Info("CSTATransferredParser.ReadTransferringDevice: try to read transferring device from stream...");

                if (reader.TryReadStruct(typeof(ExtendedDeviceID_t), out result))
                {
                    logger.Info("CSTATransferredParser.ReadTransferringDevice: successfully read transferring device from stream!");

                    ExtendedDeviceID_t transferringDevice = (ExtendedDeviceID_t)result;

                    logger.Info("CSTATransferredParser.ReadTransferringDevice: transferringDevice.deviceID.device={0};transferringDevice.deviceIDType={1};transferringDevice.deviceIDStatus={2}", transferringDevice.deviceID.device, transferringDevice.deviceIDType, transferringDevice.deviceIDStatus);

                    return transferringDevice;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Concat("Error in CSTATransferredParser.ReadTransferringDevice: ", err.ToString()));
            }

            return new ExtendedDeviceID_t();
        }

        private ExtendedDeviceID_t ReadTransferredDevice(IStructReader reader)
        {
            try
            {
                logger.Info("CSTATransferredParser.ReadTransferredDevice: try to read transferred device from stream...");

                object result;

                if (reader.TryReadStruct(typeof(ExtendedDeviceID_t), out result))
                {
                    logger.Info("CSTATransferredParser.ReadTransferredDevice: successfully read transferred device from stream!");

                    ExtendedDeviceID_t transferredDevice = (ExtendedDeviceID_t)result;

                    logger.Info("CSTATransferredParser.ReadTransferringDevice: transferredDevice.deviceID.device={0};transferredDevice.deviceIDType={1};transferredDevice.deviceIDStatus={2}", transferredDevice.deviceID.device, transferredDevice.deviceIDType, transferredDevice.deviceIDStatus);

                    return transferredDevice;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Concat("Error in CSTATransferredParser.ReadTransferredDevice: ", err.ToString()));
            }

            return new ExtendedDeviceID_t();
        }

        private ConnectionList_t ReadTransferredConnections(IStructReader reader)
        {
            try
            {
                logger.Info("CSTATransferredParser.ReadTransferredConnections: BaseStream.Position={0};BaseStream.Length={1};", reader.BaseStream.Position, reader.BaseStream.Length);

                if ((reader.BaseStream.Length - reader.BaseStream.Position) >= 4)
                {
                    logger.Info("CSTATransferredParser.ReadTransferredConnections: read count of transferred connections...");

                    uint count = reader.ReadUInt32();

                    logger.Info("CSTATransferredParser.ReadTransferredConnections: count={0}", count);

                    List<Connection_t> connections = new List<Connection_t>();

                    if (count > 0)
                    {
                        if ((reader.BaseStream.Length - reader.BaseStream.Position) >= 12)
                        {
                            logger.Info("CSTATransferredParser.ReadTransferredConnections: advance the base stream 12 positions...");
                            reader.BaseStream.Position += 12;
                        }

                        int size = Marshal.SizeOf(typeof(Connection_t));

                        for (int i = 0; i < count; i++)
                        {
                            logger.Info("CSTATransferredParser.ReadTransferredConnections: try to read connection from stream...");

                            if ((reader.BaseStream.Length - reader.BaseStream.Position) >= size)
                            {
                                object result;

                                if (reader.TryReadStruct(typeof(Connection_t), out result))
                                {
                                    logger.Info("CSTATransferredParser.ReadTransferredConnections: successfully read connection from stream!");

                                    Connection_t connection = (Connection_t)result;

                                    logger.Info("CSTATransferredParser.ReadTransferredConnections: connection.party.callID={0};connection.party.deviceID.device={1};connection.party.devIDType={2};connection.staticDevice.deviceID.device={3};connection.staticDevice.deviceIDType={4};connection.staticDevice.deviceIDType={4};", connection.party.callID, connection.party.deviceID.device, connection.party.devIDType, connection.staticDevice.deviceID.device, connection.staticDevice.deviceIDType);
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
                logger.Error(string.Concat("Error in CSTATransferredParser.ReadTransferredConnections: ", err.ToString()));
            }

            return new ConnectionList_t() { count = 0, connections = new Connection_t[] { }};
        }

        private LocalConnectionState_t ReadLocalConnectionInfo(IStructReader reader)
        {
            try
            {
                logger.Info("CSTATransferredParser.ReadLocalConnectionInfo: BaseStream.Position={0};BaseStream.Length={1};", reader.BaseStream.Position, reader.BaseStream.Length);

                if ((reader.BaseStream.Length - reader.BaseStream.Position) >= 4)
                {
                    logger.Info("CSTATransferredParser.ReadLocalConnectionInfo: read local connection state from stream...");
                    int value = reader.ReadInt32();

                    logger.Info("CSTATransferredParser.ReadLocalConnectionInfo: value={0}", value);

                    if (Enum.IsDefined(typeof(LocalConnectionState_t), value))
                    {
                        LocalConnectionState_t localConnectionInfo = (LocalConnectionState_t)value;

                        logger.Info("CSTATransferredParser.ReadLocalConnectionInfo: localConnectionInfo={0}", localConnectionInfo);

                        return localConnectionInfo;
                    }
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in CSTATransferredParser.ReadLocalConnectionInfo: {0}", err));
            }

            return LocalConnectionState_t.CS_NONE;
        }

        private CSTAEventCause_t ReadCause(IStructReader reader)
        {
            try
            {
                logger.Info("CSTATransferredParser.ReadCause: BaseStream.Position={0};BaseStream.Length={1};", reader.BaseStream.Position, reader.BaseStream.Length);

                if ((reader.BaseStream.Length - reader.BaseStream.Position) >= 4)
                {
                    logger.Info("CSTATransferredParser.ReadCause: read event cause from stream...");
                    int value = reader.ReadInt32();

                    logger.Info("CSTATransferredParser.ReadCause: value={0}", value);

                    if (Enum.IsDefined(typeof(CSTAEventCause_t), value))
                    {
                        CSTAEventCause_t cause = (CSTAEventCause_t)value;

                        logger.Info("CSTATransferredParser.ReadCause: cause={0}", cause);

                        return cause;
                    }
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in CSTATransferredParser.ReadCause: {0}", err));
            }

            return CSTAEventCause_t.EC_NONE;
        }

        public int eventType
        {
            get { return Constants.CSTA_TRANSFERRED; }
        }
    }
}
