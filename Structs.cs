using System;
using System.Runtime.InteropServices;

namespace TSAPIClient.CSTA
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Nulltype
    {
        // null
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct DeviceID_t
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string device;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ServerID_t
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 49)]
        public string server;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct AppName_t
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
        public string appName;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Version_t
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
        public string version;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct LoginID_t
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 49)]
        public string login;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Passwd_t
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 49)]
        public string passwd;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct AgentID_t
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string agent;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct AgentPassword_t
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string password;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct AccountInfo_t
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string account;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct AuthCode_t
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string code;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct WinNTPipe_t
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 101)]
        public string pipe;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CryptPasswd_t
    {
        public short length;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 47)]
        public string value;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ACSOpenStream_t
    {
        public StreamType_t streamType;
        public ServerID_t serverID;
        public LoginID_t loginID;
        public CryptPasswd_t cryptPass;
        public AppName_t applicationName;
        public Level_t level;
        public Version_t apiVer;
        public Version_t libVer;
        public Version_t tsrvVer;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Ansi)]
    public struct PrivateData_t
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string vendor;
        public ushort length;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = ATT.Constants.ATT_MAX_PRIVATE_DATA)]
        public char[] data;
    }

    public struct EventBuf_t
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Constants.CSTA_EVENT_MAX_SIZE)]
        public byte[] data;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ConnectionID_t
    {
        public int callID;
        public DeviceID_t deviceID;
        public ConnectionID_Device_t devIDType;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ACSOpenStreamConfEvent_t
    {
        public Version_t apiVer;
        public Version_t libVer;
        public Version_t tsrvVer;
        public Version_t drvrVer;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ACSCloseStream_t
    {
        public Nulltype nil;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ACSCloseStreamConfEvent_t
    {
        public Nulltype nil;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ACSAbortStream_t
    {
        public Nulltype nil;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ACSUniversalFailureConfEvent_t
    {
        public ACSUniversalFailure_t error;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ACSUniversalFailureEvent_t
    {
        public ACSUniversalFailure_t error;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ChallengeKey_t
    {
        public short length;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public string value;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ACSKeyRequest_t
    {
        public LoginID_t loginID;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ACSKeyReply_t
    {
        public int objectID;
        public ChallengeKey_t key;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ACSNameSrvRequest_t
    {
        public StreamType_t streamType;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ACSAuthInfo_t
    {
        public ACSAuthType_t authType;
        public LoginID_t authLoginID;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ACSAuthReply_t
    {
        public int objectID;
        public ChallengeKey_t key;
        public ACSAuthInfo_t authInfo;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ExtendedDeviceID_t
    {
        public DeviceID_t deviceID;
        public DeviceIDType_t deviceIDType;
        public DeviceIDStatus_t deviceIDStatus;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Connection_t
    {
        public ConnectionID_t party;
        public ExtendedDeviceID_t staticDevice;
    }

    //[StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class ConnectionList_t
    {
        public uint count;
        public Connection_t[] connections;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTAMonitorFilter_t
    {
        public ushort call;
        public byte feature;
        public byte agent;
        public byte maintenance;
        public int privateFilter;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTACallState_t
    {
        public int count;
        public LocalConnectionState_t state;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTASnapshotDeviceResponseInfo_t
    {
        public ConnectionID_t callIdentifier;
        public CSTACallState_t localCallState;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTASnapshotDeviceData_t
    {
        public int count;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public CSTASnapshotDeviceResponseInfo_t[] info;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTASnapshotCallResponseInfo_t
    {
        public ExtendedDeviceID_t deviceOnCall;
        public ConnectionID_t callIdentifier;
        public LocalConnectionState_t localConnectionState;
    }

    public struct CSTASnapshotCallData_t
    {
        public uint count;
        public CSTASnapshotCallResponseInfo_t[] info;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ForwardingInfo_t
    {
        public ForwardingType_t forwardingType;
        public Boolean forwardingOn;
        public DeviceID_t forwardDN;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ListForwardParameters_t
    {
        public short count;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
        public ForwardingInfo_t[] param;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct SetUpValues_t
    {
        public int length;
        public byte value;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTAAlternateCall_t
    {
        public ConnectionID_t activeCall;
        public ConnectionID_t otherCall;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTAAlternateCallConfEvent_t
    {
        public Nulltype nil;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTAAnswerCall_t
    {
        public ConnectionID_t alertingCall;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTAAnswerCallConfEvent_t
    {
        public Nulltype nil;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTACallCompletion_t
    {
        public Feature_t feature;
        public ConnectionID_t call;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTACallCompletionConfEvent_t
    {
        public Nulltype nil;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTAClearCall_t
    {
        public ConnectionID_t call;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTAClearCallConfEvent_t
    {
        public Nulltype nil;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTAClearConnection_t
    {
        public ConnectionID_t call;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTAClearConnectionConfEvent_t
    {
        public Nulltype nil;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTAConferenceCall_t
    {
        public ConnectionID_t heldCall;
        public ConnectionID_t activeCall;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTAConferenceCallConfEvent_t
    {
        public ConnectionID_t newCall;
        public ConnectionList_t connList;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTAConsultationCall_t
    {
        public ConnectionID_t activeCall;
        public DeviceID_t calledDevice;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTAConsultationCallConfEvent_t
    {
        public ConnectionID_t newCall;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTADeflectCall_t
    {
        public ConnectionID_t deflectCall;
        public DeviceID_t calledDevice;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTADeflectCallConfEvent_t
    {
        public Nulltype nil;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTAPickupCall_t
    {
        public ConnectionID_t deflectCall;
        public DeviceID_t calledDevice;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTAPickupCallConfEvent_t
    {
        public Nulltype nil;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTAGroupPickupCall_t
    {
        public ConnectionID_t deflectCall;
        public DeviceID_t pickupDevice;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTAGroupPickupCallConfEvent_t
    {
        public Nulltype nil;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTAHoldCall_t
    {
        public ConnectionID_t activeCall;
        public Boolean reservation;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTAHoldCallConfEvent_t
    {
        public Nulltype nil;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTAMakeCall_t
    {
        public DeviceID_t callingDevice;
        public DeviceID_t calledDevice;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTAMakeCallConfEvent_t
    {
        public ConnectionID_t newCall;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTAMakePredictiveCall_t
    {
        public DeviceID_t callingDevice;
        public DeviceID_t calledDevice;
        public AllocationState_t allocationState;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTAMakePredictiveCallConfEvent_t
    {
        public ConnectionID_t newCall;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTAQueryMwi_t
    {
        public DeviceID_t device;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTAQueryMwiConfEvent_t
    {
        public Boolean messages;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTAQueryDnd_t
    {
        public DeviceID_t device;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTAQueryDndConfEvent_t
    {
        public Boolean doNotDisturb;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTAQueryFwd_t
    {
        public DeviceID_t device;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTAQueryFwdConfEvent_t
    {
        public ListForwardParameters_t forward;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTAQueryAgentState_t
    {
        public DeviceID_t device;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTAQueryAgentStateConfEvent_t
    {
        public AgentState_t agentState;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTAQueryLastNumber_t
    {
        public DeviceID_t device;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTAQueryLastNumberConfEvent_t
    {
        public DeviceID_t lastNumber;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTAQueryDeviceInfo_t
    {
        public DeviceID_t device;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTAQueryDeviceInfoConfEvent_t
    {
        public DeviceID_t device;
        public DeviceType_t deviceType;
        public byte deviceClass;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTAReconnectCall_t
    {
        public ConnectionID_t activeCall;
        public ConnectionID_t heldCall;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTAReconnectCallConfEvent_t
    {
        public Nulltype nil;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTARetrieveCall_t
    {
        public ConnectionID_t heldCall;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTARetrieveCallConfEvent_t
    {
        public Nulltype nil;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTASetMwi_t
    {
        public DeviceID_t device;
        public Boolean messages;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTASetMwiConfEvent_t
    {
        public Nulltype nil;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTASetDnd_t
    {
        public DeviceID_t device;
        public Boolean doNotDisturb;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTASetDndConfEvent_t
    {
        public Nulltype nil;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTASetFwd_t
    {
        public DeviceID_t device;
        public ForwardingInfo_t forward;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTASetFwdConfEvent_t
    {
        public Nulltype nil;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTASetAgentState_t
    {
        public DeviceID_t device;
        public AgentMode_t agentMode;
        public AgentID_t agentID;
        public DeviceID_t agentGroup;
        public AgentPassword_t agentPassword;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTASetAgentStateConfEvent_t
    {
        public Nulltype nil;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTATransferCall_t
    {
        public ConnectionID_t heldCall;
        public ConnectionID_t activeCall;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTATransferCallConfEvent_t
    {
        public ConnectionID_t newCall;
        public ConnectionList_t connList;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTAUniversalFailureConfEvent_t
    {
        public CSTAUniversalFailure_t error;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTACallClearedEvent_t
    {
        public ConnectionID_t clearedCall;
        public LocalConnectionState_t localConnectionInfo;
        public CSTAEventCause_t cause;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTAConferencedEvent_t
    {
        public ConnectionID_t primaryOldCall;
        public ConnectionID_t secondaryOldCall;
        public ExtendedDeviceID_t confController;
        public ExtendedDeviceID_t addedParty;
        public ConnectionList_t conferenceConnections;
        public LocalConnectionState_t localConnectionInfo;
        public CSTAEventCause_t cause;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTAConnectionClearedEvent_t
    {
        public ConnectionID_t droppedConnection;
        public ExtendedDeviceID_t releasingDevice;
        public LocalConnectionState_t localConnectionInfo;
        public CSTAEventCause_t cause;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTADeliveredEvent_t
    {
        public ConnectionID_t connection;
        public ExtendedDeviceID_t alertingDevice;
        public ExtendedDeviceID_t callingDevice;
        public ExtendedDeviceID_t calledDevice;
        public ExtendedDeviceID_t lastRedirectionDevice;
        public LocalConnectionState_t localConnectionInfo;
        public CSTAEventCause_t cause;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTADivertedEvent_t
    {
        public ConnectionID_t connection;
        public ExtendedDeviceID_t divertingDevice;
        public ExtendedDeviceID_t newDestination;
        public LocalConnectionState_t localConnectionInfo;
        public CSTAEventCause_t cause;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTAEstablishedEvent_t
    {
        public ConnectionID_t establishedConnection;
        public ExtendedDeviceID_t answeringDevice;
        public ExtendedDeviceID_t callingDevice;
        public ExtendedDeviceID_t calledDevice;
        public ExtendedDeviceID_t lastRedirectionDevice;
        public LocalConnectionState_t localConnectionInfo;
        public CSTAEventCause_t cause;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTAFailedEvent_t
    {
        public ConnectionID_t failedConnection;
        public ExtendedDeviceID_t failingDevice;
        public ExtendedDeviceID_t calledDevice;
        public LocalConnectionState_t localConnectionInfo;
        public CSTAEventCause_t cause;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTAHeldEvent_t
    {
        public ConnectionID_t heldConnection;
        public ExtendedDeviceID_t holdingDevice;
        public LocalConnectionState_t localConnectionInfo;
        public CSTAEventCause_t cause;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTANetworkReachedEvent_t
    {
        public ConnectionID_t connection;
        public ExtendedDeviceID_t trunkUsed;
        public ExtendedDeviceID_t calledDevice;
        public LocalConnectionState_t localConnectionInfo;
        public CSTAEventCause_t cause;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTAOriginatedEvent_t
    {
        public ConnectionID_t originatedConnection;
        public ExtendedDeviceID_t callingDevice;
        public ExtendedDeviceID_t calledDevice;
        public LocalConnectionState_t localConnectionInfo;
        public CSTAEventCause_t cause;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTAQueuedEvent_t
    {
        public ConnectionID_t queuedConnection;
        public ExtendedDeviceID_t queue;
        public ExtendedDeviceID_t callingDevice;
        public ExtendedDeviceID_t calledDevice;
        public ExtendedDeviceID_t lastRedirectionDevice;
        public short numberQueued;
        public LocalConnectionState_t localConnectionInfo;
        public CSTAEventCause_t cause;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTARetrievedEvent_t
    {
        public ConnectionID_t retrievedConnection;
        public ExtendedDeviceID_t retrievingDevice;
        public LocalConnectionState_t localConnectionInfo;
        public CSTAEventCause_t cause;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTAServiceInitiatedEvent_t
    {
        public ConnectionID_t initiatedConnection;
        public LocalConnectionState_t localConnectionInfo;
        public CSTAEventCause_t cause;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTATransferredEvent_t
    {
        public ConnectionID_t primaryOldCall;
        public ConnectionID_t secondaryOldCall;
        public ExtendedDeviceID_t transferringDevice;
        public ExtendedDeviceID_t transferredDevice;
        public ConnectionList_t transferredConnections;
        public LocalConnectionState_t localConnectionInfo;
        public CSTAEventCause_t cause;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTACallInformationEvent_t
    {
        public ConnectionID_t connection;
        public ExtendedDeviceID_t device;
        public AccountInfo_t accountInfo;
        public AuthCode_t authorisationCode;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTADoNotDisturbEvent_t
    {
        public ExtendedDeviceID_t device;
        public Boolean doNotDisturbOn;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTAForwardingEvent_t
    {
        public ExtendedDeviceID_t device;
        public ForwardingInfo_t forwardingInformation;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTAMessageWaitingEvent_t
    {
        public ExtendedDeviceID_t deviceForMessage;
        public ExtendedDeviceID_t invokingDevice;
        public Boolean messageWaitingOn;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTALoggedOnEvent_t
    {
        public ExtendedDeviceID_t agentDevice;
        public AgentID_t agentID;
        public DeviceID_t agentGroup;
        public AgentPassword_t password;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTALoggedOffEvent_t
    {
        public ExtendedDeviceID_t agentDevice;
        public AgentID_t agentID;
        public DeviceID_t agentGroup;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTANotReadyEvent_t
    {
        public ExtendedDeviceID_t agentDevice;
        public AgentID_t agentID;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTAReadyEvent_t
    {
        public ExtendedDeviceID_t agentDevice;
        public AgentID_t agentID;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTAWorkNotReadyEvent_t
    {
        public ExtendedDeviceID_t agentDevice;
        public AgentID_t agentID;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTAWorkReadyEvent_t
    {
        public ExtendedDeviceID_t agentDevice;
        public AgentID_t agentID;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTARouteRegisterReq_t
    {
        public DeviceID_t routingDevice;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTARouteRegisterReqConfEvent_t
    {
        public int registerReqID;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTARouteRegisterCancel_t
    {
        public int routeRegisterReqID;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTARouteRegisterCancelConfEvent_t
    {
        public int routeRegisterReqID;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTARouteRegisterAbortEvent_t
    {
        public int routeRegisterReqID;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTARouteRequestEvent_t
    {
        public int routeRegisterReqID;
        public int routingCrossRefID;
        public DeviceID_t currentRoute;
        public DeviceID_t callingDevice;
        public ConnectionID_t routedCall;
        public SelectValue_t routedSelAlgorithm;
        public Boolean priority;
        public SetUpValues_t setupInformation;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTARouteSelectRequest_t
    {
        public int routeRegisterReqID;
        public int routingCrossRefID;
        public DeviceID_t routeSelected;
        public short remainRetry;
        public SetUpValues_t setupInformation;
        public Boolean routeUsedReq;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTAReRouteRequest_t
    {
        public int routeRegisterReqID;
        public int routingCrossRefID;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTARouteUsedEvent_t
    {
        public int routeRegisterReqID;
        public int routingCrossRefID;
        public DeviceID_t routeUsed;
        public DeviceID_t callingDevice;
        public Boolean domain;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTARouteEndEvent_t
    {
        public int routeRegisterReqID;
        public int routingCrossRefID;
        public CSTAUniversalFailure_t errorValue;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTARouteEndRequest_t
    {
        public int routeRegisterReqID;
        public int routingCrossRefID;
        public CSTAUniversalFailure_t errorValue;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTAEscapeSvc_t
    {
        public Nulltype nil;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTAEscapeSvcConfEvent_t
    {
        public Nulltype nil;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTAEscapeSvcReqEvent_t
    {
        public Nulltype nil;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTAEscapeSvcReqConf_t
    {
        public CSTAUniversalFailure_t errorValue;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTAPrivateEvent_t
    {
        public Nulltype nil;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTAPrivateStatusEvent_t
    {
        public Nulltype nil;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTASendPrivateEvent_t
    {
        public Nulltype nil;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTABackInServiceEvent_t
    {
        public DeviceID_t device;
        public CSTAEventCause_t cause;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTAOutOfServiceEvent_t
    {
        public DeviceID_t device;
        public CSTAEventCause_t cause;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTAReqSysStat_t
    {
        public Nulltype nil;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTASysStatReqConfEvent_t
    {
        public SystemStatus_t systemStatus;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTASysStatStart_t
    {
        public byte statusFilter;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTASysStatStartConfEvent_t
    {
        public byte statusFilter;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTASysStatStop_t
    {
        public Nulltype nil;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTASysStatStopConfEvent_t
    {
        public Nulltype nil;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTAChangeSysStatFilter_t
    {
        public byte statusFilter;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTAChangeSysStatFilterConfEvent_t
    {
        public byte statusFilterSelected;
        public byte statusFilterActive;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTASysStatEvent_t
    {
        public SystemStatus_t systemStatus;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTASysStatEndedEvent_t
    {
        public Nulltype nil;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTASysStatReqEvent_t
    {
        public Nulltype nil;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTAReqSysStatConf_t
    {
        public SystemStatus_t systemStatus;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTASysStatEventSend_t
    {
        public SystemStatus_t systemStatus;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTAMonitorDevice_t
    {
        public DeviceID_t deviceID;
        public CSTAMonitorFilter_t monitorFilter;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTAMonitorCall_t
    {
        public ConnectionID_t call;
        public CSTAMonitorFilter_t monitorFilter;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTAMonitorCallsViaDevice_t
    {
        public DeviceID_t deviceID;
        public CSTAMonitorFilter_t monitorFilter;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTAMonitorConfEvent_t
    {
        public int monitorCrossRefID;
        public CSTAMonitorFilter_t monitorFilter;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTAChangeMonitorFilter_t
    {
        public int monitorCrossRefID;
        public CSTAMonitorFilter_t monitorFilter;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTAChangeMonitorFilterConfEvent_t
    {
        public CSTAMonitorFilter_t monitorFilter;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTAMonitorStop_t
    {
        public int monitorCrossRefID;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTAMonitorStopConfEvent_t
    {
        public Nulltype nil;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTAMonitorEndedEvent_t
    {
        public CSTAEventCause_t cause;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTASnapshotCall_t
    {
        public ConnectionID_t snapshotObject;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTASnapshotCallConfEvent_t
    {
        public CSTASnapshotCallData_t snapshotData;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTASnapshotDevice_t
    {
        public DeviceID_t snapshotObject;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTASnapshotDeviceConfEvent_t
    {
        public CSTASnapshotDeviceData_t snapshotData;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTAGetAPICaps_t
    {
        public Nulltype nil;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTAGetAPICapsConfEvent_t
    {
        public short alternateCall;
        public short answerCall;
        public short callCompletion;
        public short clearCall;
        public short clearConnection;
        public short conferenceCall;
        public short consultationCall;
        public short deflectCall;
        public short pickupCall;
        public short groupPickupCall;
        public short holdCall;
        public short makeCall;
        public short makePredictiveCall;
        public short queryMwi;
        public short queryDnd;
        public short queryFwd;
        public short queryAgentState;
        public short queryLastNumber;
        public short queryDeviceInfo;
        public short reconnectCall;
        public short retrieveCall;
        public short setMwi;
        public short setDnd;
        public short setFwd;
        public short setAgentState;
        public short transferCall;
        public short eventReport;
        public short callClearedEvent;
        public short conferencedEvent;
        public short connectionClearedEvent;
        public short deliveredEvent;
        public short divertedEvent;
        public short establishedEvent;
        public short failedEvent;
        public short heldEvent;
        public short networkReachedEvent;
        public short originatedEvent;
        public short queuedEvent;
        public short retrievedEvent;
        public short serviceInitiatedEvent;
        public short transferredEvent;
        public short callInformationEvent;
        public short doNotDisturbEvent;
        public short forwardingEvent;
        public short messageWaitingEvent;
        public short loggedOnEvent;
        public short loggedOffEvent;
        public short notReadyEvent;
        public short readyEvent;
        public short workNotReadyEvent;
        public short workReadyEvent;
        public short backInServiceEvent;
        public short outOfServiceEvent;
        public short privateEvent;
        public short routeRequestEvent;
        public short reRoute;
        public short routeSelect;
        public short routeUsedEvent;
        public short routeEndEvent;
        public short monitorDevice;
        public short monitorCall;
        public short monitorCallsViaDevice;
        public short changeMonitorFilter;
        public short monitorStop;
        public short monitorEnded;
        public short snapshotDeviceReq;
        public short snapshotCallReq;
        public short escapeService;
        public short privateStatusEvent;
        public short escapeServiceEvent;
        public short escapeServiceConf;
        public short sendPrivateEvent;
        public short sysStatReq;
        public short sysStatStart;
        public short sysStatStop;
        public short changeSysStatFilter;
        public short sysStatReqEvent;
        public short sysStatReqConf;
        public short sysStatEvent;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTAGetDeviceList_t
    {
        public int index;
        public CSTALevel_t level;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct DeviceList_t
    {
        public short count;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
        public DeviceID_t[] device;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTAGetDeviceListConfEvent_t
    {
        public SDBLevel_t driverSdbLevel;
        public CSTALevel_t level;
        public int index;
        public DeviceList_t devList;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTAQueryCallMonitor_t
    {
        public Nulltype nil;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTAQueryCallMonitorConfEvent_t
    {
        public Boolean callMonitor;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTARouteRequestExtEvent_t
    {
        public int routeRegisterReqID;
        public int routingCrossRefID;
        public ExtendedDeviceID_t currentRoute;
        public ExtendedDeviceID_t callingDevice;
        public ConnectionID_t routedCall;
        public SelectValue_t routedSelAlgorithm;
        public Boolean priority;
        public SetUpValues_t setupInformation;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTARouteUsedExtEvent_t
    {
        public int routeRegisterReqID;
        public int routingCrossRefID;
        public ExtendedDeviceID_t routeUsed;
        public ExtendedDeviceID_t callingDevice;
        public Boolean domain;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ACSEventHeader_t
    {
        public IntPtr acsHandle;
        public ushort eventClass;
        public ushort eventType;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CSTAMonitorCrossRefID_t
    {
        public int value;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct InvokeID_t
    {
        public int value;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ACSSetHeartbeatIntervalConfEvent_t
    {
        public ushort heartbeatInterval;
    }    
}
