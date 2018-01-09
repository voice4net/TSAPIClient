using System.Runtime.InteropServices;
using TSAPIClient.CSTA;

namespace TSAPIClient.ATT
{
    [StructLayout(LayoutKind.Explicit)]
    public struct ATTEventBuf_t
    {
        [FieldOffset(0)]
        public ushort eventType;

        [FieldOffset(4)]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1932)]
        public byte[] data;

        [FieldOffset(1936)]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Constants.ATTPRIV_MAX_HEAP)]
        public byte[] heap;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ATTMonitorConfEvent_t
    {
        public ATTPrivateFilter_t usedFilter;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ATTPrivateFilter_t
    {
        public byte filter;
    }
    
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ATTLinkStatusEvent_t
    {
        public uint count;
        public ATTLinkStatus_t[] pLinkStatus; // supposed to be a pointer...ATTLinkStatus_t *pLinkStatus
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ATTLinkStatus_t
    {
        public short linkID;
        public ATTLinkState_t linkState;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ATTRouteUsedEvent_t
    {
        public DeviceID_t destRoute;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ATTRouteRequestEvent_t
    {
        public DeviceID_t trunkGroup;
        public ATTLookaheadInfo_t lookaheadInfo;
        public ATTUserEnteredCode_t userEnteredCode;
        public ATTUserToUserInfo_t userInfo;
        public ATTUCID_t ucid;
        public ATTCallOriginatorInfo_t callOriginatorInfo;
        public bool flexibleBilling;
        public DeviceID_t trunkMember;
        public DeviceHistory_t deviceHistory;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ATTTransferredEvent_t
    {
        public ATTOriginalCallInfo_t originalCallInfo;
        public CalledDeviceID_t distributingDevice;
        public ATTUCID_t ucid;
        public ATTTrunkList_t trunkList;
        public DeviceHistory_t deviceHistory;
        public CalledDeviceID_t distributingVDN;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ATTServiceInitiatedEvent_t
    {
        public ATTUCID_t ucid;
        public ATTConsultMode_t consultMode;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ATTQueuedEvent_t
    {
        public DeviceHistory_t deviceHistory;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ATTOriginatedEvent_t
    {
        public DeviceID_t logicalAgent;
        public ATTUserToUserInfo_t userInfo;
        public ATTConsultMode_t consultMode;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ATTNetworkReachedEvent_t
    {
        public ATTProgressLocation_t progressLocation;
        public ATTProgressDescription_t progressDescription;
        public DeviceID_t trunkGroup;
        public DeviceID_t trunkMember;
        public DeviceHistory_t deviceHistory;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ATTChargeAdviceEvent_t
    {
        public ConnectionID_t connection;
        public DeviceID_t calledDevice;
        public DeviceID_t chargingDevice;
        public DeviceID_t trunkGroup;
        public DeviceID_t trunkMember;
        public ATTChargeType_t chargeType;
        public int charge;
        public ATTChargeError_t error;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ATTMonitorStopOnCallConfEvent_t
    {
        public Nulltype NULL;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ATTMonitorCallConfEvent_t
    {
        public ATTPrivateFilter_t usedFilter;
        public ATTSnapshotCall_t snapshotCall;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ATTSnapshotCall_t
    {
        public uint count;
        public CSTASnapshotCallResponseInfo_t[] pInfo; // technically supposed to be a pointer...CSTASnapshotCallResponseInfo_t* pInfo
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ATTSnapshotDeviceConfEvent_t
    {
        public uint count;
        public ATTSnapshotDevice_t[] pSnapshotDevice; // technically supposed to be a pointer...ATTSnapshotDevice_t* pSnapshotDevice
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ATTSnapshotDevice_t
    {
        public ConnectionID_t call;
        public ATTLocalCallState_t state;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ATTSnapshotCallConfEvent_t
    {
        public DeviceHistory_t deviceHistory;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ATTQueryUcidConfEvent_t
    {
        public ATTUCID_t ucid;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ATTQueryTgConfEvent_t
    {
        public short idleTrunks; /* number of "idle" trunks in the group */
        public short usedTrunks; /* number of "in use" trunks in the group */
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ATTQueryTodConfEvent_t
    {
        public short year;
        public short month;
        public short day;
        public short hour;
        public short minute;
        public short second;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ATTQueryStationStatusConfEvent_t
    {
        public bool stationStatus; /* TRUE = busy, FALSE = idle */
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ATTQueryMwiConfEvent_t
    {
        public ATTMwiApplication_t applicationType;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ATTMwiApplication_t
    {
        public byte applicationType;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ATTQueryDeviceNameConfEvent_t
    {
        public ATTDeviceType_t deviceType;
        public DeviceID_t device;
        public DeviceID_t name; /* 1–27 ASCII character string */
        public ATTUnicodeDeviceID uname; /* name in Unicode */
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ATTUnicodeDeviceID
    {
        public ushort count;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
        public short[] value;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ATTQueryDeviceInfoConfEvent_t
    {
        public ATTExtensionClass_t extensionClass;
        public ATTExtensionClass_t associatedClass;
        public DeviceID_t associatedDevice;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ATTQueryCallClassifierConfEvent_t
    {
        public short numAvailPorts; /* number of available ports */
        public short numInUsePorts; /* number of ports in use */
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ATTQueryAgentStateConfEvent_t
    {
        public ATTWorkMode_t workMode;
        public ATTTalkState_t talkState;
        public int reasonCode;
        public ATTWorkMode_t pendingWorkMode;
        public int pendingReasonCode;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ATTQueryAgentLoginResp_t
    {
        public ATTPrivEventCrossRefID_t privEventCrossRefID;
        public ushort count; /* number of extensions in device[] */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public DeviceID_t[] device; /* up to 10 extensions */
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ATTQueryAcdSplitConfEvent_t
    {
        public short availableAgents; /* number of agents available to receive calls */
        public short callsInQueue; /* number of calls in queue */
        public short agentsLoggedIn; /* number of agents logged in */
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ATTQueryAgentLoginConfEvent_t
    {
        public ATTPrivEventCrossRefID_t privEventCrossRefID;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ATTPrivEventCrossRefID_t
    {
        public int crossRefID;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ATTSetAgentStateConfEvent_t
    {
        public bool isPending; /* TRUE if request is pending */
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ATTTransferCallConfEvent_t
    {
        public ATTUCID_t ucid;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ATTSingleStepTransferCallConfEvent_t
    {
        public ConnectionID_t transferredCall;
        public ATTUCID_t ucid;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ATTSingleStepConferenceCallConfEvent_t
    {
        public ConnectionID_t newCall;
        public ConnectionList_t connList;
        public ATTUCID_t ucid;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ATTMakeCallConfEvent_t
    {
        public ATTUCID_t ucid;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ATTMakePredictiveCallConfEvent_t
    {
        public ATTUCID_t ucid;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ATTConferenceCallConfEvent_t
    {
        public ATTUCID_t ucid;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ATTConsultationCallConfEvent_t
    {
        public ATTUCID_t ucid;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ATTLoggedOffEvent_t
    {
        public int reasonCode;  /* 0–99 for private data version 7 and later; 0–9 for private data versions 5 and 6. */
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ATTLoggedOnEvent_t
    {
        public ATTWorkMode_t workMode;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ATTHeldEvent_t
    {
        public ATTConsultMode_t consultMode;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ATTDivertedEvent_t
    {
        public DeviceHistory_t deviceHistory;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ATTFailedEvent_t
    {
        public DeviceHistory_t deviceHistory;
        public CallingDeviceID_t callingDevice;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ATTTrunkList_t
    {
        public short count;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Constants.MAX_TRUNKS)]
        public ATTTrunkInfo_t[] trunks;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ATTTrunkInfo_t
    {
        public ConnectionID_t connection;
        public DeviceID_t trunkGroup;
        public DeviceID_t trunkMember;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ATTConferencedEvent_t
    {
        public ATTOriginalCallInfo_t originalCallInfo;
        public CalledDeviceID_t distributingDevice;
        public ATTUCID_t ucid;
        public ATTTrunkList_t trunkList;
        public DeviceHistory_t deviceHistory;
        public CalledDeviceID_t distributingVDN;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ATTDeliveredEvent_t
    {
        public ATTDeliveredType_t deliveredType;
        public DeviceID_t trunkGroup;
        public DeviceID_t trunkMember;
        public DeviceID_t split;
        public ATTLookaheadInfo_t lookaheadInfo;
        public ATTUserEnteredCode_t userEnteredCode;
        public ATTUserToUserInfo_t userInfo;
        public ATTReasonCode_t reason;
        public ATTOriginalCallInfo_t originalCallInfo;
        public CalledDeviceID_t distributingDevice;
        public ATTUCID_t ucid;
        public ATTCallOriginatorInfo_t callOriginatorInfo;
        public bool flexibleBilling;
        public DeviceHistory_t deviceHistory;
        public CalledDeviceID_t distributingVDN;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ATTCallClearedEvent_t
    {
        public ATTReasonCode_t reason;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ATTConnectionClearedEvent_t
    {
        public ATTUserToUserInfo_t userInfo;
        public DeviceHistory_t deviceHistory;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ATTEstablishedEvent_t
    {
        public DeviceID_t trunkGroup;
        public DeviceID_t trunkMember;
        public DeviceID_t split;
        public ATTLookaheadInfo_t lookaheadInfo;
        public ATTUserEnteredCode_t userEnteredCode;
        public ATTUserToUserInfo_t userInfo;
        public ATTReasonCode_t reason;
        public ATTOriginalCallInfo_t originalCallInfo;
        public CalledDeviceID_t distributingDevice;
        public ATTUCID_t ucid;
        public ATTCallOriginatorInfo_t callOriginatorInfo;
        public bool flexibleBilling;
        public DeviceHistory_t deviceHistory;
        public CalledDeviceID_t distributingVDN;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ATTOriginalCallInfo_t
    {
        public ATTReasonForCallInfo_t reason;
        public CallingDeviceID_t callingDevice;
        public CalledDeviceID_t calledDevice;
        public DeviceID_t trunkGroup;
        public DeviceID_t trunkMember;
        public ATTLookaheadInfo_t lookaheadInfo;
        public ATTUserEnteredCode_t userEnteredCode;
        public ATTUserToUserInfo_t userInfo;
        public ATTUCID_t ucid;
        public ATTCallOriginatorInfo_t callOriginatorInfo;
        public bool flexibleBilling;
        public DeviceHistory_t deviceHistory;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CallingDeviceID_t
    {
        public ExtendedDeviceID_t value;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CalledDeviceID_t
    {
        public ExtendedDeviceID_t value;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct DeviceHistory_t
    {
        [FieldOffset(0)]
        public uint count; /* at most 1 */

        [FieldOffset(8)]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
        public DeviceHistoryEntry_t[] deviceHistoryList; // DeviceHistoryEntry_t // THIS IS SUPPOSED TO BE A POINTER TO DeviceHistoryEntry_t. When DeviceHistoryEntry_t the size of struct is WAY too big. When IntPtr the size if 4 bytes to big (I think)
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ATTCallOriginatorInfo_t
    {
        public bool hasInfo; /* if FALSE, no call originator info */
        public short callOriginatorType;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct DeviceHistoryEntry_t
    {
        public DeviceID_t olddeviceID;
        public CSTAEventCause_t cause;
        public ConnectionID_t oldconnectionID;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ATTUCID_t
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string value;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ATTUserToUserInfo_t
    {
        public ATTUUIProtocolType_t type;
        public ushort length; /* 0 indicates UUI not present */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Constants.ATT_MAX_USER_INFO)]
        public byte[] data;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ATTLookaheadInfo_t
    {
        public ATTInterflow_t type;
        public ATTPriority_t priority;
        public short hours;
        public short minutes;
        public short seconds;
        public DeviceID_t sourceVDN;
        public ATTUnicodeDeviceID_t uSourceVDN; /* sourceVDN in Unicode */
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ATTUserEnteredCode_t
    {
        public ATTUserEnteredCodeType_t type;
        public ATTUserEnteredCodeIndicator_t indicator;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Constants.ATT_MAX_USER_CODE)]
        public string data;
        public DeviceID_t collectVDN;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ATTUnicodeDeviceID_t
    {
        public ushort count;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
        public short[] value;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ATTSendDTMFToneConfEvent_t
    {
        public Nulltype NULL;
    }
}
