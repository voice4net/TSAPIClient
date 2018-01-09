using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NLog;
using TSAPIClient.ATT;
using TSAPIClient.CSTA;
using TSAPIClient.Parsers;
using TSAPIClient.Readers;

namespace TSAPIClient
{
    public class Client
    {
        public event EventHandler<TSAPIEventArgs> TSAPIEvent;
        private EsrFunc esr;
        private readonly AutoResetEvent m_EventsPending = new AutoResetEvent(true);
        private IntPtr acsHandle;
        private static readonly Logger logger = LogManager.GetLogger("TSAPIClient");
        private readonly IATTEventParserFactory m_ATTEventParserFactory = new ATTEventParserFactory();
        private readonly ICSTAConfirmationParserFactory m_CSTAConfParserFactory = new CSTAConfirmationParserFactory();
        private readonly ICSTAUnsolicitedParserFactory m_CSTAUnsolicitedParserFactory = new CSTAUnsolicitedParserFactory();
        private readonly IACSConfirmationParserFactory m_ACSConfParserFactory = new ACSConfirmationParserFactory();
        private readonly IACSUnsolicitedParserFactory m_ACSUnsolicitedParserFactory = new ACSUnsolicitedParserFactory();

        public static string[] acsEnumServerNames()
        {
            try
            {
                logger.Info("Client.acsEnumServerNames: Get the server names from the avaya...");

                List<string> serverNames = new List<string>();

                EnumServerNamesCB callback = (ptr, lParam) =>
                {
                    string serverName = Marshal.PtrToStringAnsi(ptr);

                    logger.Info("Client.acsEnumServerNames: ServerName={0}", serverName);

                    serverNames.Add(serverName);

                    return true;
                };

                int RetCode_t = CSTA.Proxy.acsEnumServerNames(StreamType_t.ST_CSTA, Marshal.GetFunctionPointerForDelegate(callback), 0);

                logger.Info("Client.acsEnumServerNames: RetCode_t={0}", RetCode_t);

                if (RetCode_t == CSTA.Constants.ACSPOSITIVE_ACK)
                {
                    logger.Info("Client.acsEnumServerNames: The function completed successfully as requested by the application. No errors were detected.");
                }
                else if (RetCode_t == CSTA.Constants.ACSERR_UNKNOWN)
                {
                    logger.Info("Client.acsEnumServerNames: The request has failed due to unknown network problems.");
                }
                else if (RetCode_t == CSTA.Constants.ACSERR_NOSERVER)
                {
                    logger.Info("Client.acsEnumServerNames: The request has failed because the client cannot communicate with the TSAPI Service. Perhaps the IP address or hostname of the AE Services server is not configured properly in the TSAPI client configuration file; perhaps there is a network issue; or perhaps the TSAPI Service is not running.");
                }

                return serverNames.ToArray();
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in acsEnumServerNames: {0}", err));
            }

            return new string[] { };
        }

        public int acsOpenStream(TSAPIOpenStreamRequest request)
        {
            try
            {
                logger.Info("Client.acsOpenStream: ServerID={0};LoginID={1};Password={2};ApplicationName={3};", request.ServerID, request.LoginID, request.Passwd, request.ApplicationName);

                ServerID_t serverID = new ServerID_t() { server = request.ServerID };
                LoginID_t loginID = new LoginID_t() { login = request.LoginID };
                Passwd_t passwd = new Passwd_t() { passwd = request.Passwd };
                AppName_t applicationName = new AppName_t() { appName = request.ApplicationName };
                Version_t apiVer = new Version_t() {version = "TS1-2"};
                PrivateData_t privateData = new PrivateData_t { vendor = "VERSION" };
                const ushort sendQSize = 0;
                const ushort sendExtraBufs = 5;
                const ushort recvQSize = 0;
                const ushort recvExtraBufs = 5;

                StringBuilder bldr = new StringBuilder();

                logger.Info("Client.acsOpenStream: invoke the attMakeVersionString function with requested version numbers '3-9'...");
                int RetCode_t = ATT.Proxy.attMakeVersionString("3-9", bldr);

                logger.Info("Client.acsOpenStream: ReturnCode={0}", RetCode_t);

                if (RetCode_t > 0)
                {
                    logger.Info("Client.acsOpenStream: attMakeVersionString succeeded!");

                    string supportedVersion = bldr.ToString();

                    logger.Info("Client.acsOpenStream: supportedVersion={0}", supportedVersion);

                    short length = (short)(supportedVersion.Length + 2);

                    logger.Info("Client.acsOpenStream: Setting private data length to {0}...", length);
                    privateData.length = (ushort)length;

                    string data = string.Format("{0}{1}{2}", (char)CSTA.Constants.PRIVATE_DATA_ENCODING, supportedVersion, (char)CSTA.Constants.PRIVATE_DATA_ENCODING);

                    logger.Info("Client.acsOpenStream: Setting private data {0}...", data);
                    privateData.data = data.PadRight(ATT.Constants.ATT_MAX_PRIVATE_DATA, '\0').ToCharArray();
                }
                else
                {
                    logger.Info("Client.acsOpenStream: attMakeVersionString failed. Setting private data length to zero...");
                    privateData.length = 0;
                }

                logger.Info("Client.acsOpenStream: invoke the acsOpenStream function...");
                RetCode_t = CSTA.Proxy.acsOpenStream(ref acsHandle, InvokeIDType_t.APP_GEN_ID, (uint)request.InvokeID, StreamType_t.ST_CSTA, ref serverID, ref loginID, ref passwd, ref applicationName, Level_t.ACS_LEVEL1, ref apiVer, sendQSize, sendExtraBufs, recvQSize, recvExtraBufs, ref privateData);

                logger.Info("Client.acsOpenStream: ReturnCode={0}", RetCode_t);

                if (RetCode_t > 0)
                {
                    // this is the invoke id generated by the pbx
                    return RetCode_t;
                }

                switch (RetCode_t)
                {
                    case CSTA.Constants.ACSPOSITIVE_ACK:

                        logger.Info("Client.acsOpenStream: Successfully opened the ACS stream!");
                        logger.Info("Client.acsOpenStream: acsHandle={0}", acsHandle);

                        acsSetESR();

                        break;

                    case CSTA.Constants.ACSERR_BADPARAMETER:

                        logger.Info("Client.acsOpenStream: Failed to open the ACS stream. One or more of the parameters is invalid.");
                        break;

                    case CSTA.Constants.ACSERR_NODRIVER:

                        logger.Info("Client.acsOpenStream: Failed to open the ACS stream. No TSAPI Client Library Driver was found or installed on the system.");
                        break;

                    case CSTA.Constants.ACSERR_NOSERVER:

                        logger.Info("Client.acsOpenStream: Failed to open the ACS stream. The advertised service (serverID) is not available on the network.");
                        break;

                    case CSTA.Constants.ACSERR_NORESOURCE:

                        logger.Info("Client.acsOpenStream: Failed to open the ACS stream. There are insufficient resources to open an ACS stream.");
                        break;

                    case CSTA.Constants.ACSERR_SSL_INIT_FAILED:

                        logger.Info("Client.acsOpenStream: Failed to open the ACS stream. A secure connection could not be opened because there was a problem initializing the OpenSSL library.");
                        break;

                    case CSTA.Constants.ACSERR_SSL_CONNECT_FAILED:

                        logger.Info("Client.acsOpenStream: Failed to open the ACS stream. A stream could not be opened because there was a problem establishing an SSL connection to the server. It may be that the server failed to provide a certificate, or that the server certificate is not signed by a trusted Certificate Authority.");
                        break;

                    case CSTA.Constants.ACSERR_SSL_FQDN_MISMATCH:

                        logger.Info("Client.acsOpenStream: Failed to open the ACS stream. This return value indicates that a stream could not be opened because the fully qualified domain name (FQDN) in the server certificate does not match the expected FQDN.");
                        break;

                    case CSTA.Constants.ACSERR_STREAM_FAILED:

                        logger.Info("Client.acsOpenStream: Failed to open the ACS stream. the application attempted to open a stream to a secure (encrypted) Tlink, but the TSAPI client library (Release 4.0.x or earlier) does not support secure client connections.");
                        break;
                }

                return RetCode_t;
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in Client.acsOpenStream: {0}", err));
            }

            return -1;
        }

        private void acsSetESR()
        {
            try
            {
                esr = (uint esrParam) =>
                {
                    logger.Info("Client.acsSetESR: an incoming event is available!");
                    m_EventsPending.Set();
                };

                logger.Info("Client.acsSetESR: set a callback function to listen for events...");

                int RetCode_t = CSTA.Proxy.acsSetESR(acsHandle, Marshal.GetFunctionPointerForDelegate(esr), esrParam: 0, notifyAll: false);

                logger.Info("Client.acsSetESR: ReturnCode={0}", RetCode_t);

                new Thread(Poll).Start();
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in acsSetESR: {0}", err));
            }
        }

        public int acsSetHeartbeatInterval(TSAPISetHeartbeatIntervalRequest request)
        {
            try
            {
                logger.Info("Client.acsSetHeartbeatInterval: InvokeID={0};HeartbeatInterval={1};", request.InvokeID, request.HeartbeatInterval);

                uint invokeID = (uint)request.InvokeID;
                ushort heartbeatInterval = request.HeartbeatInterval;

                int RetCode_t = CSTA.Proxy.acsSetHeartbeatInterval(acsHandle, invokeID, heartbeatInterval, IntPtr.Zero);

                logger.Info("Client.acsSetHeartbeatInterval: ReturnCode={0}", RetCode_t);

                return RetCode_t;
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in Client.acsSetHeartbeatInterval: {0}", err));
            }

            return -1;
        }

        private void Poll()
        {
            try
            {
                while (true)
                {
                    m_EventsPending.WaitOne();

                    EventBuf_t eventBuf = new EventBuf_t();
                    ushort eventBufSize = 512;
                    PrivateData_t privateData = new PrivateData_t() { length = ATT.Constants.ATT_MAX_PRIVATE_DATA };
                    ushort numEvents = 0;

                    while (true)
                    {
                        logger.Info("Client.Poll: poll the stream with a buffer size of {0}...", eventBufSize);

                        int RetCode_t = CSTA.Proxy.acsGetEventPoll(IntPtr.Zero, ref eventBuf, ref eventBufSize, ref privateData, ref numEvents);

                        logger.Info("Client.Poll: ReturnCode={0}", RetCode_t);

                        if (RetCode_t == CSTA.Constants.ACSPOSITIVE_ACK) // 0
                        {
                            logger.Info("Client.Poll: The function completed successfully as requested by the application, and an event has been copied to the application data space. No errors were detected.");

                            logger.Info("Client.Poll: PrivateDataLength={0}", privateData.length);

                            // create a byte array to hold the event buffer
                            byte[] buffer = new byte[eventBufSize];

                            // copy the event buffer to the new byte array
                            Buffer.BlockCopy(eventBuf.data, 0, buffer, 0, eventBufSize);

                            // create an object to pass to the parse method
                            ACSParseEventArgs acsEvent = new ACSParseEventArgs() { EventBuffer = buffer, PrivateData = privateData };

                            // parse the event on a seperate thread
                            Task.Run(() => Parse(acsEvent));

                            // reinitialize the variables
                            eventBuf = new EventBuf_t();
                            eventBufSize = 512;
                            privateData = new PrivateData_t() { length = ATT.Constants.ATT_MAX_PRIVATE_DATA };
                            numEvents = 0;
                        }
                        else if (RetCode_t == CSTA.Constants.ACSERR_UBUFSMALL) // -7
                        {
                            logger.Info("Client.Poll: The buffer size is too small! Poll again with buffer of size {0}...", eventBufSize);

                            /*
                            The user buffer size indicated in the eventBufSize parameter was smaller than the
                            size of the next available event for the application on the ACS stream. The
                            eventBufSize variable has been reset by the API Library to the size of the next
                            message on the ACS stream. The application should call acsGetEventPoll()
                            again with a larger event buffer. The ACS event is still on the API Library queue.
                            Alternatively, this return value may indicate that the private data length indicated in
                            the privateData parameter was smaller than the size of the private data
                            accompanying the next available event for the application on the ACS stream. The
                            API library does not update the value of the eventBufSize variable in this case.
                            The application should call acsGetEventPoll() again with a larger private data
                            buffer. The ACS event is still on the API library queue. 
                            */
                        }
                        else if (RetCode_t == CSTA.Constants.ACSERR_NOMESSAGE) // -8
                        {
                            logger.Info("Client.Poll: There were no messages available to return to the application.");

                            // the stream is empty. stop reading from the stream for now...
                            break;
                        }
                        else if (RetCode_t == CSTA.Constants.ACSERR_BADHDL) // -10
                        {
                            logger.Info("Client.Poll: The ACS handle is not a valid handle for an active ACS stream.");

                            /*
                            This indicates that the acsHandle being used is not a valid handle for an active
                            ACS stream. No changes occur in any existing streams if a bad handle is passed
                            with this function.
                            */

                            // shouldn't happen since we are passing IntPtr.Zero as the handle, which means "any open stream"
                            break;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in Client.Poll: {0}", err));
            }
        }

        public int cstaGetDeviceList(TSAPIGetDeviceListRequest request)
        {
            try
            {
                logger.Info("Client.cstaGetDeviceList: obtain the list of Devices that can be controlled, monitored, queried or routed...");
                logger.Info("Client.cstaGetDeviceList: InvokeID={0};Index={1};Level={2};", request.InvokeID, request.Index, request.Level);

                uint invokeID = (uint)request.InvokeID;

                int RetCode_t = CSTA.Proxy.cstaGetDeviceList(acsHandle, invokeID, request.Index, request.Level);

                logger.Info("Client.cstaGetDeviceList: ReturnCode={0}", RetCode_t);

                return RetCode_t;
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in Client.cstaGetDeviceList: {0}", err));
            }

            return -1;
        }

        public int cstaMonitorDevice(TSAPIMonitorDeviceRequest request)
        {
            try
            {
                logger.Info("Client.cstaMonitorDevice: InvokeID={0};DN={1};", request.InvokeID, request.DeviceID);

                CSTAMonitorFilter_t monitorFilter = new CSTAMonitorFilter_t() { call = request.CallFilter, feature = request.FeatureFilter, agent = request.AgentFilter, maintenance = request.MaintenanceFilter, privateFilter = Convert.ToInt32(request.FilterPrivateData) };
                uint invokeID = (uint)request.InvokeID;
                DeviceID_t device = new DeviceID_t() { device = request.DeviceID };
                PrivateData_t privateData = new PrivateData_t();
                ATTPrivateFilter_t privateFilter = new ATTPrivateFilter_t() { filter = request.PrivateFilter };

                logger.Info("Client.cstaMonitorDevice: prepare private data...");

                int RetCode_t = ATT.Proxy.attMonitorFilterExt(ref privateData, ref privateFilter);

                if (RetCode_t == 0)
                {
                    logger.Info("Client.cstaMonitorDevice: privateData.length={0};privateData.vendor={1};", privateData.length, privateData.vendor);
                }

                logger.Info("Client.cstaMonitorDevice: invoke the cstaMonitorDevice function...");
                RetCode_t = CSTA.Proxy.cstaMonitorDevice(acsHandle, invokeID, ref device, ref monitorFilter, ref privateData);

                logger.Info("Client.cstaMonitorDevice: ReturnCode={0}", RetCode_t);

                return RetCode_t;
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in Client.cstaMonitorDevice: {0}", err));
            }

            return -1;
        }

        public int cstaMonitorCallsViaDevice(TSAPIMonitorCallsViaDeviceRequest request)
        {
            try
            {
                logger.Info("Client.cstaMonitorCallsViaDevice: InvokeID={0};DN={1};", request.InvokeID, request.DeviceID);

                CSTAMonitorFilter_t monitorFilter = new CSTAMonitorFilter_t() { call = request.CallFilter, feature = request.FeatureFilter, agent = request.AgentFilter, maintenance = request.MaintenanceFilter, privateFilter = Convert.ToInt32(request.FilterPrivateData) };
                uint invokeID = (uint)request.InvokeID;
                DeviceID_t device = new DeviceID_t() { device = request.DeviceID };
                PrivateData_t privateData = new PrivateData_t();
                ATTPrivateFilter_t privateFilter = new ATTPrivateFilter_t() { filter = request.PrivateFilter };

                logger.Info("Client.cstaMonitorCallsViaDevice: prepare private data...");

                int RetCode_t = ATT.Proxy.attMonitorCallsViaDevice(ref privateData, ref privateFilter, flowPredictiveCallEvents: false);

                if (RetCode_t == 0)
                {
                    logger.Info("Client.cstaMonitorCallsViaDevice: privateData.length={0};privateData.vendor={1};", privateData.length, privateData.vendor);
                }

                logger.Info("Client.cstaMonitorCallsViaDevice: invoke the cstaMonitorCallsViaDevice function...");
                RetCode_t = CSTA.Proxy.cstaMonitorCallsViaDevice(acsHandle, invokeID, ref device, ref monitorFilter, ref privateData);

                logger.Info("Client.cstaMonitorCallsViaDevice: ReturnCode={0}", RetCode_t);

                return RetCode_t;
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in Client.cstaMonitorCallsViaDevice: {0}", err));
            }

            return -1;
        }

        public int cstaSnapshotDeviceReq(TSAPISnapshotDeviceRequest request)
        {
            try
            {
                logger.Info("Client.cstaSnapshotDeviceReq: InvokeID={0};Device={1};", request.InvokeID, request.Device);

                uint invokeID = (uint)request.InvokeID;
                DeviceID_t device = new DeviceID_t() { device = request.Device };

                logger.Info("Client.cstaSnapshotDeviceReq: Invoke the cstaSnapshotDeviceReq function...");
                int RetCode_t = CSTA.Proxy.cstaSnapshotDeviceReq(acsHandle, invokeID, ref device, IntPtr.Zero);

                logger.Info("Client.cstaSnapshotDeviceReq: ReturnCode={0}", RetCode_t);

                return RetCode_t;
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in Client.cstaSnapshotDeviceReq: {0}", err));
            }

            return -1;
        }

        public int cstaSnapshotCallReq(TSAPISnapshotCallRequest request)
        {
            try
            {
                logger.Info("Client.cstaSnapshotCallReq: InvokeID={0};CallID={1};Device={2};", request.InvokeID, request.CallID, request.DeviceID);

                uint invokeID = (uint)request.InvokeID;
                ConnectionID_t snapshotObj = new ConnectionID_t()
                {
                    callID = request.CallID,
                    deviceID = new DeviceID_t() { device = request.DeviceID },
                    devIDType = ConnectionID_Device_t.STATIC_ID
                };
                PrivateData_t privateData = new PrivateData_t();

                logger.Info("Client.cstaSnapshotCallReq: Invoke the cstaSnapshotCallReq function...");
                int RetCode_t = CSTA.Proxy.cstaSnapshotCallReq(acsHandle, invokeID, ref snapshotObj, ref privateData);

                logger.Info("Client.cstaSnapshotCallReq: ReturnCode={0}", RetCode_t);

                return RetCode_t;
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in Client.cstaSnapshotCallReq: {0}", err));
            }

            return -1;
        }

        public int cstaMakeCall(TSAPIMakeCallRequest request)
        {
            try
            {
                logger.Info("Client.cstaMakeCall: InvokeID={0};CallingDevice={1};CalledDevice={2};", request.InvokeID, request.CallingDevice, request.CalledDevice);

                uint invokeID = (uint)request.InvokeID;
                DeviceID_t callingDevice = new DeviceID_t() { device = request.CallingDevice };
                DeviceID_t calledDevice = new DeviceID_t() { device = request.CalledDevice };
                PrivateData_t privateData = new PrivateData_t();
                DeviceID_t destRoute = new DeviceID_t();

                string data = request.UserInfo.PadRight(ATT.Constants.ATT_MAX_USER_INFO, '\0');

                ATTUserToUserInfo_t userInfo = new ATTUserToUserInfo_t() { type = ATTUUIProtocolType_t.UUI_IA5_ASCII, data = Encoding.Default.GetBytes(data), length = (ushort)request.UserInfo.Length };

                logger.Info("Client.cstaMakeCall: prepare private data...");
                int RetCode_t = ATT.Proxy.attV6MakeCall(ref privateData, ref destRoute, false, ref userInfo);

                if (RetCode_t == 0)
                {
                    logger.Info("Client.cstaMakeCall: privateData.length={0};privateData.vendor={1};", privateData.length, privateData.vendor);
                }

                logger.Info("Client.cstaMakeCall: invoke the cstaMakeCall function...");
                RetCode_t = CSTA.Proxy.cstaMakeCall(acsHandle, invokeID, ref callingDevice, ref calledDevice, ref privateData);

                logger.Info("Client.cstaMakeCall: ReturnCode={0}", RetCode_t);

                return RetCode_t;
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in Client.cstaMakeCall: {0}", err));
            }

            return -1;
        }

        public int cstaAnswerCall(TSAPIAnswerCallRequest request)
        {
            try
            {
                logger.Info("Client.cstaAnswerCall: InvokeID={0};CallID={1};DeviceID={2};DeviceType={3};", request.InvokeID, request.CallID, request.DeviceID, request.DeviceType);

                uint invokeID = (uint)request.InvokeID;
                int callID = request.CallID;
                string device = request.DeviceID;
                ConnectionID_Device_t devIDType = request.DeviceType;
                DeviceID_t deviceID = new DeviceID_t() { device = device };
                ConnectionID_t alertingCall = new ConnectionID_t() { callID = callID, deviceID = deviceID, devIDType = devIDType };

                logger.Info("Client.cstaAnswerCall: Invoke the cstaAnswerCall function...");
                int RetCode_t = CSTA.Proxy.cstaAnswerCall(acsHandle, invokeID, ref alertingCall, IntPtr.Zero);

                logger.Info("Client.cstaAnswerCall: ReturnCode={0}", RetCode_t);

                return RetCode_t;
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in Client.cstaAnswerCall: {0}", err));
            }

            return -1;
        }

        public int cstaClearConnection(TSAPIClearConnectionRequest request)
        {
            try
            {
                logger.Info("Client.cstaClearConnection: InvokeID={0};CallID={1};DeviceID={2};DeviceType={3};", request.InvokeID, request.CallID, request.DeviceID, request.DeviceType);

                uint invokeID = (uint)request.InvokeID;
                int callID = request.CallID;
                string device = request.DeviceID;
                ConnectionID_Device_t devIDType = request.DeviceType;
                PrivateData_t privateData = new PrivateData_t();
                DeviceID_t deviceID = new DeviceID_t() { device = device };
                ConnectionID_t call = new ConnectionID_t() { callID = callID, deviceID = deviceID, devIDType = devIDType };

                string data = new string('\0', ATT.Constants.ATT_MAX_USER_INFO);

                ATTUserToUserInfo_t userInfo = new ATTUserToUserInfo_t() { type = ATTUUIProtocolType_t.UUI_IA5_ASCII, data = Encoding.Default.GetBytes(data), length = 0 };

                // format private data for the subsequent cstaClearConnection() request
                ATT.Proxy.attV6ClearConnection(ref privateData, ATTDropResource_t.DR_NONE, ref userInfo);

                logger.Info("Client.cstaClearConnection: Invoke the cstaClearConnection function...");
                int RetCode_t = CSTA.Proxy.cstaClearConnection(acsHandle, invokeID, ref call, ref privateData);

                logger.Info("Client.cstaClearConnection: ReturnCode={0}", RetCode_t);

                return RetCode_t;
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in Client.cstaClearConnection: {0}", err));
            }

            return -1;
        }

        public int cstaMonitorCall(TSAPIMonitorCallRequest request)
        {
            try
            {
                logger.Info("Client.cstaMonitorCall: InvokeID={0};CallID={1};DeviceID={2};", request.InvokeID, request.CallID, request.DeviceID);

                uint invokeID = (uint)request.InvokeID;
                int callID = request.CallID;
                string device = request.DeviceID;
                DeviceID_t deviceID = new DeviceID_t() { device = device };
                ConnectionID_t call = new ConnectionID_t() { callID = callID, deviceID = deviceID, devIDType = ConnectionID_Device_t.DYNAMIC_ID };               
                CSTAMonitorFilter_t monitorFilter = new CSTAMonitorFilter_t() { call = request.CallFilter, feature = request.FeatureFilter, agent = request.AgentFilter, maintenance = request.MaintenanceFilter, privateFilter = Convert.ToInt32(request.FilterPrivateData) };
                PrivateData_t privateData = new PrivateData_t();
                ATTPrivateFilter_t privateFilter = new ATTPrivateFilter_t() { filter = request.PrivateFilter };

                logger.Info("Client.cstaMonitorCall: prepare private data...");

                int RetCode_t = ATT.Proxy.attMonitorFilterExt(ref privateData, ref privateFilter);

                if (RetCode_t == 0)
                {
                    logger.Info("Client.cstaMonitorCall: privateData.length={0};privateData.vendor={1};", privateData.length, privateData.vendor);
                }

                logger.Info("Client.cstaMonitorCall: Invoke the cstaMonitorCall function...");
                RetCode_t = CSTA.Proxy.cstaMonitorCall(acsHandle, invokeID, ref call, ref monitorFilter, ref privateData);

                logger.Info("Client.cstaMonitorCall: ReturnCode={0}", RetCode_t);

                return RetCode_t;
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in Client.cstaMonitorCall: {0}", err));
            }

            return -1;
        }

        public int cstaHoldCall(TSAPIHoldCallRequest request)
        {
            try
            {
                logger.Info("Client.cstaHoldCall: InvokeID={0};CallID={1};DeviceID={2};DeviceType={3};", request.InvokeID, request.CallID, request.DeviceID, request.DeviceType);

                uint invokeID = (uint)request.InvokeID;
                int callID = request.CallID;
                string device = request.DeviceID;
                ConnectionID_Device_t devIDType = request.DeviceType;
                DeviceID_t deviceID = new DeviceID_t() { device = device };
                ConnectionID_t call = new ConnectionID_t() { callID = callID, deviceID = deviceID, devIDType = devIDType };

                logger.Info("Client.cstaHoldCall: Invoke the cstaHoldCall function...");
                int RetCode_t = CSTA.Proxy.cstaHoldCall(acsHandle, invokeID, ref call, true, IntPtr.Zero);

                logger.Info("Client.cstaHoldCall: ReturnCode={0}", RetCode_t);

                return RetCode_t;
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in Client.cstaHoldCall: {0}", err));
            }

            return -1;
        }

        public int cstaConsultationCall(TSAPIConsultationCallRequest request)
        {
            try
            {
                logger.Info("Client.cstaConsultationCall: InvokeID={0};CallID={1};DeviceID={2};DeviceType={3};CalledDevice={4};", request.InvokeID, request.CallID, request.DeviceID, request.DeviceType, request.CalledDevice);

                uint invokeID = (uint)request.InvokeID;
                int callID = request.CallID;
                string device = request.DeviceID;                
                DeviceID_t calledDevice = new DeviceID_t() { device = request.CalledDevice };
                ConnectionID_Device_t devIDType = request.DeviceType;
                PrivateData_t privateData = new PrivateData_t();
                DeviceID_t deviceID = new DeviceID_t() { device = device };
                ConnectionID_t activeCall = new ConnectionID_t() { callID = callID, deviceID = deviceID, devIDType = devIDType };

                DeviceID_t destRoute = new DeviceID_t() { device = device };

                string data = request.UserInfo.PadRight(ATT.Constants.ATT_MAX_USER_INFO, '\0');

                ATTUserToUserInfo_t userInfo = new ATTUserToUserInfo_t
                {
                    type = ATTUUIProtocolType_t.UUI_IA5_ASCII,
                    data = Encoding.Default.GetBytes(data),
                    length = (ushort) request.UserInfo.Length
                };

                logger.Info("Client.cstaConsultationCall: prepare private data...");
                int RetCode_t = ATT.Proxy.attV6ConsultationCall(ref privateData, ref destRoute, false, ref userInfo);

                if (RetCode_t == 0)
                {
                    logger.Info("Client.cstaConsultationCall: privateData.length={0};privateData.vendor={1};", privateData.length, privateData.vendor);
                }

                logger.Info("Client.cstaConsultationCall: Invoke the cstaConsultationCall function...");
                RetCode_t = CSTA.Proxy.cstaConsultationCall(acsHandle, invokeID, ref activeCall, ref calledDevice, ref privateData);

                logger.Info("Client.cstaConsultationCall: ReturnCode={0}", RetCode_t);

                return RetCode_t;
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in Client.cstaConsultationCall: {0}", err));
            }

            return -1;
        }

        public int cstaDeflectCall(TSAPIDeflectCallRequest request)
        {
            try
            {
                logger.Info("Client.cstaDeflectCall: InvokeID={0};CallID={1};DeviceID={2};DeviceType={3};CalledDevice={4};", request.InvokeID, request.CallID, request.DeviceID, request.DeviceType, request.CalledDevice);

                uint invokeID = (uint)request.InvokeID;
                DeviceID_t calledDevice = new DeviceID_t() { device = request.CalledDevice };
                ConnectionID_t deflectCall = new ConnectionID_t()
                {
                    callID = request.CallID,
                    deviceID = new DeviceID_t() { device = request.DeviceID },
                    devIDType = request.DeviceType
                };

                logger.Info("Client.cstaDeflectCall: Invoke the cstaDeflectCall function...");
                int RetCode_t = CSTA.Proxy.cstaDeflectCall(acsHandle, invokeID, ref deflectCall, ref calledDevice, IntPtr.Zero);

                logger.Info("Client.cstaDeflectCall: ReturnCode={0}", RetCode_t);

                return RetCode_t;
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in Client.cstaConsultationCall: {0}", err));
            }

            return -1;
        }

        public int cstaAlternateCall(TSAPIAlternateCallRequest request)
        {
            try
            {
                logger.Info("Client.cstaAlternateCall: InvokeID={0};ActiveCallID={1};OtherCallID={2};DeviceID={3};DeviceType={4};", request.InvokeID, request.ActiveCallID, request.OtherCallID, request.DeviceID, request.DeviceType);

                uint invokeID = (uint)request.InvokeID;
                int activeCallID = request.ActiveCallID;
                int otherCallID = request.OtherCallID;
                string device = request.DeviceID;
                ConnectionID_Device_t devIDType = request.DeviceType;
                DeviceID_t deviceID = new DeviceID_t() { device = device };
                ConnectionID_t activeCall = new ConnectionID_t() { callID = activeCallID, deviceID = deviceID, devIDType = devIDType };
                ConnectionID_t otherCall = new ConnectionID_t() { callID = otherCallID, deviceID = deviceID, devIDType = devIDType };

                logger.Info("Client.cstaAlternateCall: Invoke the cstaAlternateCall function...");
                int RetCode_t = CSTA.Proxy.cstaAlternateCall(acsHandle, invokeID, ref activeCall, ref otherCall, IntPtr.Zero);

                logger.Info("Client.cstaAlternateCall: ReturnCode={0}", RetCode_t);

                return RetCode_t;
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in Client.cstaAlternateCall: {0}", err));
            }

            return -1;
        }

        public int cstaPickupCall(TSAPIPickupCallRequest request)
        {
            try
            {
                logger.Info("Client.cstaPickupCall: InvokeID={0};CallID={1};DeviceID={2};DeviceType={3};CalledDevice={4};", request.InvokeID, request.CallID, request.DeviceID, request.DeviceType, request.CalledDevice);

                uint invokeID = (uint) request.InvokeID;
                DeviceID_t calledDevice = new DeviceID_t() { device = request.CalledDevice };
                ConnectionID_t deflectCall = new ConnectionID_t()
                {
                    callID = request.CallID,
                    deviceID = new DeviceID_t() { device = request.DeviceID },
                    devIDType = request.DeviceType
                };

                logger.Info("Client.cstaPickupCall: Invoke the cstaPickupCall function...");
                int RetCode_t = CSTA.Proxy.cstaPickupCall(acsHandle, invokeID, ref deflectCall, ref calledDevice, IntPtr.Zero);

                logger.Info("Client.cstaPickupCall: ReturnCode={0}", RetCode_t);

                return RetCode_t;
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in Client.cstaPickupCall: {0}", err));
            }

            return -1;
        }

        public int cstaRetrieveCall(TSAPIRetrieveCallRequest request)
        {
            try
            {
                logger.Info("Client.cstaRetrieveCall: InvokeID={0};CallID={1};DeviceID={2};DeviceType={3};", request.InvokeID, request.CallID, request.DeviceID, request.DeviceType);

                uint invokeID = (uint)request.InvokeID;
                ConnectionID_t heldCall = new ConnectionID_t()
                {
                    callID = request.CallID,
                    deviceID = new DeviceID_t() { device = request.DeviceID },
                    devIDType = request.DeviceType
                };

                logger.Info("Client.cstaRetrieveCall: Invoke the cstaRetrieveCall function...");
                int RetCode_t = CSTA.Proxy.cstaRetrieveCall(acsHandle, invokeID, ref heldCall, IntPtr.Zero);

                logger.Info("Client.cstaRetrieveCall: ReturnCode={0}", RetCode_t);

                return RetCode_t;
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in Client.cstaRetrieveCall: {0}", err));
            }

            return -1;
        }

        public int cstaTransferCall(TSAPITransferCallRequest request)
        {
            try
            {
                logger.Info("Client.cstaTransferCall: InvokeID={0};HeldCallID={1};ActiveCallID={2};DeviceID={3};DeviceType={4};", request.InvokeID, request.HeldCallID, request.ActiveCallID, request.DeviceID, request.DeviceType);

                uint invokeID = (uint)request.InvokeID;
                int heldCallID = request.HeldCallID;
                int activeCallID = request.ActiveCallID;
                string device = request.DeviceID;
                ConnectionID_Device_t devIDType = request.DeviceType;
                DeviceID_t deviceID = new DeviceID_t() { device = device };
                ConnectionID_t heldCall = new ConnectionID_t() { callID = heldCallID, deviceID = deviceID, devIDType = devIDType };
                ConnectionID_t activeCall = new ConnectionID_t() { callID = activeCallID, deviceID = deviceID, devIDType = devIDType };

                logger.Info("Client.cstaTransferCall: Invoke the cstaTransferCall function...");
                int RetCode_t = CSTA.Proxy.cstaTransferCall(acsHandle, invokeID, ref heldCall, ref activeCall, IntPtr.Zero);

                logger.Info("Client.cstaTransferCall: ReturnCode={0}", RetCode_t);

                return RetCode_t;
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in Client.cstaTransferCall: {0}", err));
            }

            return -1;
        }

        public int cstaConferenceCall(TSAPIConferenceCallRequest request)
        {
            try
            {
                logger.Info("Client.cstaConferenceCall: InvokeID={0};HeldCallID={1};ActiveCallID={2};DeviceID={3};DeviceType={4};", request.InvokeID, request.HeldCallID, request.ActiveCallID, request.DeviceID, request.DeviceType);

                uint invokeID = (uint)request.InvokeID;
                int heldCallID = request.HeldCallID;
                int activeCallID = request.ActiveCallID;
                string device = request.DeviceID;
                ConnectionID_Device_t devIDType = request.DeviceType;
                DeviceID_t deviceID = new DeviceID_t() { device = device };
                ConnectionID_t heldCall = new ConnectionID_t() { callID = heldCallID, deviceID = deviceID, devIDType = devIDType };
                ConnectionID_t activeCall = new ConnectionID_t() { callID = activeCallID, deviceID = deviceID, devIDType = devIDType };

                logger.Info("Client.cstaConferenceCall: Invoke the cstaConferenceCall function...");
                int RetCode_t = CSTA.Proxy.cstaConferenceCall(acsHandle, invokeID, ref heldCall, ref activeCall, IntPtr.Zero);

                logger.Info("Client.cstaConferenceCall: ReturnCode={0}", RetCode_t);

                return RetCode_t;
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in Client.cstaConferenceCall: {0}", err));
            }

            return -1;
        }

        public int cstaClearCall(TSAPIClearCallRequest request)
        {
            try
            {
                logger.Info("Client.cstaClearCall: InvokeID={0};CallID={1};DeviceID={2};DeviceType={3};", request.InvokeID, request.CallID, request.DeviceID, request.DeviceType);

                uint invokeID = (uint)request.InvokeID;
                int callID = request.CallID;
                string device = request.DeviceID;
                ConnectionID_Device_t devIDType = request.DeviceType;
                DeviceID_t deviceID = new DeviceID_t() { device = device };
                ConnectionID_t call = new ConnectionID_t() { callID = callID, deviceID = deviceID, devIDType = devIDType };

                logger.Info("Client.cstaClearCall: Invoke the cstaClearCall function...");
                int RetCode_t = CSTA.Proxy.cstaClearCall(acsHandle, invokeID, ref call, IntPtr.Zero);

                logger.Info("Client.cstaClearCall: ReturnCode={0}", RetCode_t);

                return RetCode_t;                
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in Client.cstaClearCall: {0}", err));
            }

            return -1;
        }

        public int cstaSetDoNotDisturb(TSAPISetDoNotDisturbRequest request)
        {
            try
            {
                logger.Info("Client.cstaSetDoNotDisturb: InvokeID={0};DeviceID={1};DoNotDisturb={2};", request.InvokeID, request.DeviceID, request.DoNotDisturb);

                uint invokeID = (uint)request.InvokeID;
                DeviceID_t device = new DeviceID_t() { device = request.DeviceID };
                bool doNotDisturb = request.DoNotDisturb;

                logger.Info("Client.cstaSetDoNotDisturb: Invoke the cstaClearCall function...");
                int RetCode_t = CSTA.Proxy.cstaSetDoNotDisturb(acsHandle, invokeID, ref device, doNotDisturb, IntPtr.Zero);

                logger.Info("Client.cstaSetDoNotDisturb: ReturnCode={0}", RetCode_t);

                return RetCode_t;
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in Client.cstaSetDoNotDisturb: {0}", err));
            }

            return -1;
        }

        public int cstaSetAgentState(TSAPISetAgentStateRequest request)
        {
            try
            {
                logger.Info("Client.cstaSetAgentState: InvokeID={0};DeviceID={1};AgentID={2};AgentGroup={3};AgentPassword={4};AgentMode={5};WorkMode={6};ReasonCode={7};EnablePending={8};", request.InvokeID, request.DeviceID, request.AgentID, request.AgentGroup, request.AgentPassword, request.AgentMode, request.WorkMode, request.ReasonCode, request.EnablePending);

                uint invokeID = (uint)request.InvokeID;
                DeviceID_t device = new DeviceID_t() { device = request.DeviceID };
                AgentID_t agentID = new AgentID_t() { agent = request.AgentID };
                DeviceID_t agentGroup = new DeviceID_t() { device = request.AgentGroup };
                AgentPassword_t agentPassword = new AgentPassword_t() { password = (String.IsNullOrWhiteSpace(request.AgentPassword) ? '\0'.ToString() : request.AgentPassword) };
                AgentMode_t agentMode = request.AgentMode;
                ATTWorkMode_t workMode = request.WorkMode;
                int reasonCode = request.ReasonCode;
                bool enablePending = request.EnablePending;
                PrivateData_t privateData = new PrivateData_t();  

                logger.Info("Client.cstaSetAgentState: prepare private data...");
                int RetCode_t = ATT.Proxy.attV6SetAgentState(ref privateData, workMode, reasonCode, enablePending);

                if (RetCode_t == 0)
                {
                    logger.Info("Client.cstaSetAgentState: privateData.length={0};privateData.vendor={1};", privateData.length, privateData.vendor);
                }

                logger.Info("Client.cstaSetAgentState: Invoke the cstaSetAgentState function...");
                RetCode_t = CSTA.Proxy.cstaSetAgentState(acsHandle, invokeID, ref device, agentMode, ref agentID, ref agentGroup, ref agentPassword, IntPtr.Zero);

                logger.Info("Client.cstaSetAgentState: ReturnCode={0}", RetCode_t);

                return RetCode_t;
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in Client.cstaSetAgentState: {0}", err));
            }

            return -1;
        }

        public int cstaEscapeService(TSAPIEscapeServiceRequest request)
        {
            try
            {
                logger.Info("Client.cstaEscapeService: invokeID={0}", request.InvokeID);

                uint invokeID = (uint)request.InvokeID;

                PrivateData_t privateData = new PrivateData_t();

                int RetCode_t = -1;

                logger.Info("Client.cstaEscapeService: prepare private data...");

                switch (request.RequestType)
                {
                    case EscapeServiceType.QUERY_ACD_SPLIT:
                        {
                            DeviceID_t deviceID = new DeviceID_t() { device = request.DeviceID };

                            logger.Info("Client.cstaEscapeService.QUERY_UCID: device={0};", request.DeviceID);

                            RetCode_t = ATT.Proxy.attQueryAcdSplit(ref privateData, ref deviceID);
                        }

                        break;

                    case EscapeServiceType.QUERY_UCID:
                        {
                            DeviceID_t deviceID = new DeviceID_t() { device = request.DeviceID };
                            ConnectionID_t call = new ConnectionID_t() { callID = request.CallID, deviceID = deviceID, devIDType = ConnectionID_Device_t.STATIC_ID };

                            logger.Info("Client.cstaEscapeService.QUERY_UCID: device={0};callID={1};", request.DeviceID, request.CallID);

                            RetCode_t = ATT.Proxy.attQueryUCID(ref privateData, ref call);
                        }

                        break;

                    case EscapeServiceType.SINGLE_STEP_TRANSFER:
                        {
                            var singleStepTransferCallRequest = request as TSAPISingleStepTransferCallRequest;

                            if (singleStepTransferCallRequest != null)
                            {
                                ConnectionID_t activeCall = new ConnectionID_t() { callID = request.CallID, deviceID = new DeviceID_t() { device = request.DeviceID }, devIDType = ConnectionID_Device_t.STATIC_ID };
                                DeviceID_t transferredTo = new DeviceID_t() { device = singleStepTransferCallRequest.TransferredTo };

                                logger.Info("Client.cstaEscapeService.SINGLE_STEP_TRANSFER: device={0};callID={1};transferredTo={2};", request.DeviceID, request.CallID, singleStepTransferCallRequest.TransferredTo);

                                RetCode_t = ATT.Proxy.attSingleStepTransferCall(ref privateData, ref activeCall, ref transferredTo);
                            }
                        }

                        break;

                    case EscapeServiceType.SEND_DTMF_TONE:
                        {
                            TSAPISendDTMFToneRequest sendDtmfToneRequest = request as TSAPISendDTMFToneRequest;

                            if (sendDtmfToneRequest != null)
                            {
                                string dtmf = System.Text.RegularExpressions.Regex.Replace(sendDtmfToneRequest.Tones, @"[^0-9*#]", string.Empty);

                                if (String.IsNullOrEmpty(dtmf))
                                {
                                    return -1;
                                }

                                if (dtmf.Length > 32)
                                {
                                    return -1;
                                }

                                logger.Info("Client.cstaEscapeService.SEND_DTMF_TONE: device={0};callID={1};tones={2};toneDuration={3};pauseDuration={4};", sendDtmfToneRequest.DeviceID, sendDtmfToneRequest.CallID, sendDtmfToneRequest.Tones, sendDtmfToneRequest.ToneDuration, sendDtmfToneRequest.PauseDuration);

                                ConnectionID_t sender = new ConnectionID_t() { callID = request.CallID, deviceID = new DeviceID_t() { device = request.DeviceID }, devIDType = ConnectionID_Device_t.STATIC_ID };

                                RetCode_t = ATT.Proxy.attSendDTMFToneExt(ref privateData, ref sender, IntPtr.Zero, dtmf, sendDtmfToneRequest.ToneDuration, sendDtmfToneRequest.PauseDuration);
                            }
                        }

                        break;
                }

                if (RetCode_t == 0)
                {
                    logger.Info("Client.cstaEscapeService: privateData.length={0};privateData.vendor={1};", privateData.length, privateData.vendor);
                }

                logger.Info("Client.cstaEscapeService: Invoke the cstaEscapeService function...");
                RetCode_t = CSTA.Proxy.cstaEscapeService(acsHandle, invokeID, ref privateData);

                logger.Info("Client.cstaEscapeService: ReturnCode={0}", RetCode_t);

                return RetCode_t;
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in Client.cstaEscapeService: {0}", err));
            }

            return -1;
        }

        public int cstaReconnectCall(TSAPIReconnectCallRequest request)
        {
            try
            {
                logger.Info("Client.cstaReconnectCall: InvokeID={0};Device={1};DeviceType={2};ActiveCallID={3};HeldCallID={4};", request.InvokeID, request.DeviceID, request.DeviceType, request.ActiveCallID, request.HeldCallID);

                uint invokeID = (uint)request.InvokeID;
                int activeCallID = request.ActiveCallID;
                int heldCallID = request.HeldCallID;
                DeviceID_t deviceID = new DeviceID_t() { device = request.DeviceID };
                ConnectionID_Device_t devIDType = request.DeviceType;
                ConnectionID_t activeCall = new ConnectionID_t() { callID = activeCallID, deviceID = deviceID, devIDType = devIDType };
                ConnectionID_t heldCall = new ConnectionID_t() { callID = heldCallID, deviceID = deviceID, devIDType = devIDType };
                PrivateData_t privateData = new PrivateData_t();

                string data = request.UserInfo.PadRight(ATT.Constants.ATT_MAX_USER_INFO, '\0');

                ATTUserToUserInfo_t userInfo = new ATTUserToUserInfo_t() { type = ATTUUIProtocolType_t.UUI_IA5_ASCII, data = Encoding.Default.GetBytes(data), length = (ushort)request.UserInfo.Length };

                logger.Info("Client.cstaReconnectCall: prepare private data...");
                int RetCode_t = ATT.Proxy.attV6ReconnectCall(ref privateData, ATTDropResource_t.DR_NONE, ref userInfo);

                if (RetCode_t == 0)
                {
                    logger.Info("Client.cstaReconnectCall: privateData.length={0};privateData.vendor={1};", privateData.length, privateData.vendor);
                }

                logger.Info("Client.cstaReconnectCall: invoke the cstaReconnectCall function...");
                RetCode_t = CSTA.Proxy.cstaReconnectCall(acsHandle, invokeID, ref activeCall, ref heldCall, ref privateData);

                logger.Info("Client.cstaReconnectCall: ReturnCode={0}", RetCode_t);

                return RetCode_t;
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in Client.cstaReconnectCall: {0}", err));
            }

            return -1;
        }

        public int cstaQueryDeviceInfo(TSAPIQueryDeviceInfoRequest request)
        {
            try
            {
                logger.Info("Client.cstaQueryDeviceInfo: InvokeID={0};DeviceID={1};", request.InvokeID, request.DeviceID);

                uint invokeID = (uint)request.InvokeID;
                DeviceID_t device = new DeviceID_t() { device = request.DeviceID };

                logger.Info("Client.cstaQueryDeviceInfo: invoke the cstaQueryDeviceInfo function...");
                int RetCode_t = CSTA.Proxy.cstaQueryDeviceInfo(acsHandle, invokeID, ref device, IntPtr.Zero);

                logger.Info("Client.cstaQueryDeviceInfo: ReturnCode={0}", RetCode_t);

                return RetCode_t;
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in Client.cstaQueryDeviceInfo: {0}", err));
            }

            return -1;
        }

        public int cstaSysStatReq(TSAPISystemStatusRequest request)
        {
            try
            {
                logger.Info("Client.cstaSysStatReq: InvokeID={0};", request.InvokeID);

                uint invokeID = (uint)request.InvokeID;

                logger.Info("Client.cstaSysStatReq: invoke the cstaSysStatReq function...");
                int RetCode_t = CSTA.Proxy.cstaSysStatReq(acsHandle, invokeID, IntPtr.Zero);

                logger.Info("Client.cstaSysStatReq: ReturnCode={0}", RetCode_t);

                return RetCode_t;
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in Client.cstaSysStatReq: {0}", err));
            }

            return -1;
        }

        private void Parse(ACSParseEventArgs acsEvent)
        {
            try
            {
                if (acsEvent.EventBuffer == null)
                {
                    return;
                }

                if (acsEvent.EventBuffer.Length == 0)
                {
                    return;
                }

                logger.Info("Client.Parse: EventBuffer={0}", Encoding.UTF8.GetString(acsEvent.EventBuffer));

                EventReader eventReader = new EventReader(new StructReader(acsEvent.EventBuffer), m_CSTAConfParserFactory, m_CSTAUnsolicitedParserFactory, m_ACSConfParserFactory, m_ACSUnsolicitedParserFactory);
                CSTAEvent_t cstaEvent = eventReader.Read();

                PrivateDataReader privateDataReader = new PrivateDataReader(acsEvent.PrivateData, m_ATTEventParserFactory);
                ATTEvent_t attEvent = privateDataReader.Read();

                if (cstaEvent != null)
                {
                    TSAPIEventArgs args = new TSAPIEventArgs() { cstaEvent = cstaEvent, attEvent = attEvent, privateData = acsEvent.PrivateData };

                    logger.Info("Client.Parse: Event={0}", args);
                    OnTSAPIEvent(args);
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in Client.Parse: {0}", err));
            }
        }

        private void OnTSAPIEvent(TSAPIEventArgs args)
        {
            if (TSAPIEvent != null)
            {
                args.SequenceNumber = SequenceNumberGenerator.Generate();
                TSAPIEvent(this, args);
            }
        }
    }
}