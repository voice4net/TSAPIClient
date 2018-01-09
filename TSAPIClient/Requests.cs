using TSAPIClient.ATT;
using TSAPIClient.CSTA;

namespace TSAPIClient
{
    public abstract class TSAPIRequest
    {
        private int m_InvokeID = -1;
        public int InvokeID
        {
            get { return m_InvokeID; }
            set { m_InvokeID = value; }
        }
    }

    public class TSAPIOpenStreamRequest : TSAPIRequest
    {
        private string m_ServerID = string.Empty;
        public string ServerID
        {
            get { return m_ServerID; }
            set { m_ServerID = value; }
        }

        private string m_LoginID = string.Empty;
        public string LoginID
        {
            get { return m_LoginID; }
            set { m_LoginID = value; }
        }

        private string m_Passwd = string.Empty;
        public string Passwd
        {
            get { return m_Passwd; }
            set { m_Passwd = value; }
        }

        private string m_ApplicationName = "Voice4Net";
        public string ApplicationName
        {
            get { return m_ApplicationName; }
            set { m_ApplicationName = value; }
        }

        private string m_PrivateDataVersion = "3-9";
        public string PrivateDataVersion
        {
            get { return m_PrivateDataVersion; }
            set { m_PrivateDataVersion = value; }
        }
    }

    public class TSAPIGetDeviceListRequest : TSAPIRequest
    {
        private int m_Index = -1;
        public int Index
        {
            get { return m_Index; }
            set { m_Index = value; }
        }

        private CSTALevel_t m_Level = CSTALevel_t.CSTA_DEVICE_DEVICE_MONITOR;
        public CSTALevel_t Level
        {
            get { return m_Level; }
            set { m_Level = value; }
        }
    }

    public class TSAPISetHeartbeatIntervalRequest : TSAPIRequest
    {
        private ushort m_HeartbeatInterval = 20;
        public ushort HeartbeatInterval
        {
            get { return m_HeartbeatInterval; }
            set { m_HeartbeatInterval = value; }
        }
    }

    public class TSAPIMonitorDeviceRequest : TSAPIRequest
    {
        private string m_DeviceID = string.Empty;
        public string DeviceID
        {
            get { return m_DeviceID; }
            set { m_DeviceID = value; }
        }

        public ushort CallFilter { get; set; }

        public byte FeatureFilter { get; set; }

        public byte AgentFilter { get; set; }

        public byte MaintenanceFilter { get; set; }

        public bool FilterPrivateData { get; set; }

        public byte PrivateFilter { get; set; }
    }

    public class TSAPIMonitorCallsViaDeviceRequest : TSAPIRequest
    {
        private string m_DeviceID = string.Empty;
        public string DeviceID
        {
            get { return m_DeviceID; }
            set { m_DeviceID = value; }
        }

        public ushort CallFilter { get; set; }

        public byte FeatureFilter { get; set; }

        public byte AgentFilter { get; set; }

        public byte MaintenanceFilter { get; set; }

        public bool FilterPrivateData { get; set; }

        public byte PrivateFilter { get; set; }
    }

    public class TSAPIQueryDeviceInfoRequest : TSAPIRequest
    {
        private string m_DeviceID = string.Empty;
        public string DeviceID
        {
            get { return m_DeviceID; }
            set { m_DeviceID = value; }
        }
    }

    public class TSAPISystemStatusRequest : TSAPIRequest
    {
    }

    public class TSAPIMakeCallRequest : TSAPIRequest
    {
        private string m_CallingDevice = string.Empty;
        public string CallingDevice
        {
            get { return m_CallingDevice; }
            set { m_CallingDevice = value; }
        }

        private string m_CalledDevice = string.Empty;
        public string CalledDevice
        {
            get { return m_CalledDevice; }
            set { m_CalledDevice = value; }
        }

        private string m_UserInfo = string.Empty;
        public string UserInfo
        {
            get { return m_UserInfo; }
            set { m_UserInfo = value; }
        }
    }

    public class TSAPISnapshotCallRequest : TSAPIRequest
    {
        private int m_CallID = -1;
        public int CallID
        {
            get { return m_CallID; }
            set { m_CallID = value; }
        }

        private string m_DeviceID = string.Empty;
        public string DeviceID
        {
            get { return m_DeviceID; }
            set { m_DeviceID = value; }
        }
    }

    public class TSAPISnapshotDeviceRequest : TSAPIRequest
    {
        private string m_Device = string.Empty;
        public string Device
        {
            get { return m_Device; }
            set { m_Device = value; }
        }
    }

    public class TSAPIAnswerCallRequest : TSAPIRequest
    {
        private int m_CallID = -1;
        public int CallID
        {
            get { return m_CallID; }
            set { m_CallID = value; }
        }

        private string m_DeviceID = string.Empty;
        public string DeviceID
        {
            get { return m_DeviceID; }
            set { m_DeviceID = value; }
        }

        private ConnectionID_Device_t m_DeviceType = ConnectionID_Device_t.STATIC_ID;
        public ConnectionID_Device_t DeviceType
        {
            get { return m_DeviceType; }
            set { m_DeviceType = value; }
        }
    }

    public class TSAPIMonitorCallRequest : TSAPIRequest
    {
        private int m_CallID = -1;
        public int CallID
        {
            get { return m_CallID; }
            set { m_CallID = value; }
        }

        private string m_DeviceID = string.Empty;
        public string DeviceID
        {
            get { return m_DeviceID; }
            set { m_DeviceID = value; }
        }

        public ushort CallFilter { get; set; }

        public byte FeatureFilter { get; set; }

        public byte AgentFilter { get; set; }

        public byte MaintenanceFilter { get; set; }

        public bool FilterPrivateData { get; set; }

        public byte PrivateFilter { get; set; }
    }

    public class TSAPIClearConnectionRequest : TSAPIRequest
    {
        private int m_CallID = -1;
        public int CallID
        {
            get { return m_CallID; }
            set { m_CallID = value; }
        }

        private string m_DeviceID = string.Empty;
        public string DeviceID
        {
            get { return m_DeviceID; }
            set { m_DeviceID = value; }
        }

        private ConnectionID_Device_t m_DeviceType = ConnectionID_Device_t.STATIC_ID;
        public ConnectionID_Device_t DeviceType
        {
            get { return m_DeviceType; }
            set { m_DeviceType = value; }
        }
    }

    public class TSAPIHoldCallRequest : TSAPIRequest
    {
        private int m_CallID = -1;
        public int CallID
        {
            get { return m_CallID; }
            set { m_CallID = value; }
        }

        private string m_DeviceID = string.Empty;
        public string DeviceID
        {
            get { return m_DeviceID; }
            set { m_DeviceID = value; }
        }

        private ConnectionID_Device_t m_DeviceType = ConnectionID_Device_t.STATIC_ID;
        public ConnectionID_Device_t DeviceType
        {
            get { return m_DeviceType; }
            set { m_DeviceType = value; }
        }
    }

    public class TSAPIConsultationCallRequest : TSAPIRequest
    {
        private int m_CallID = -1;
        public int CallID
        {
            get { return m_CallID; }
            set { m_CallID = value; }
        }

        private string m_DeviceID = string.Empty;
        public string DeviceID
        {
            get { return m_DeviceID; }
            set { m_DeviceID = value; }
        }

        private ConnectionID_Device_t m_DeviceType = ConnectionID_Device_t.STATIC_ID;
        public ConnectionID_Device_t DeviceType
        {
            get { return m_DeviceType; }
            set { m_DeviceType = value; }
        }

        private string m_CalledDevice = string.Empty;
        public string CalledDevice
        {
            get { return m_CalledDevice; }
            set { m_CalledDevice = value; }
        }

        private string m_UserInfo = string.Empty;
        public string UserInfo
        {
            get { return m_UserInfo; }
            set { m_UserInfo = value; }
        }
    }

    public class TSAPIDeflectCallRequest : TSAPIRequest
    {
        private int m_CallID = -1;
        public int CallID
        {
            get { return m_CallID; }
            set { m_CallID = value; }
        }

        private string m_DeviceID = string.Empty;
        public string DeviceID
        {
            get { return m_DeviceID; }
            set { m_DeviceID = value; }
        }

        private ConnectionID_Device_t m_DeviceType = ConnectionID_Device_t.STATIC_ID;
        public ConnectionID_Device_t DeviceType
        {
            get { return m_DeviceType; }
            set { m_DeviceType = value; }
        }

        private string m_CalledDevice = string.Empty;
        public string CalledDevice
        {
            get { return m_CalledDevice; }
            set { m_CalledDevice = value; }
        }
    }

    public class TSAPIAlternateCallRequest : TSAPIRequest
    {
        private int m_ActiveCallID = -1;
        public int ActiveCallID
        {
            get { return m_ActiveCallID; }
            set { m_ActiveCallID = value; }
        }

        private int m_OtherCallID = -1;
        public int OtherCallID
        {
            get { return m_OtherCallID; }
            set { m_OtherCallID = value; }
        }

        private string m_DeviceID = string.Empty;
        public string DeviceID
        {
            get { return m_DeviceID; }
            set { m_DeviceID = value; }
        }

        private ConnectionID_Device_t m_DeviceType = ConnectionID_Device_t.STATIC_ID;
        public ConnectionID_Device_t DeviceType
        {
            get { return m_DeviceType; }
            set { m_DeviceType = value; }
        }
    }

    public class TSAPIReconnectCallRequest : TSAPIRequest
    {
        private int m_ActiveCallID = -1;
        public int ActiveCallID
        {
            get { return m_ActiveCallID; }
            set { m_ActiveCallID = value; }
        }

        private int m_HeldCallID = -1;
        public int HeldCallID
        {
            get { return m_HeldCallID; }
            set { m_HeldCallID = value; }
        }

        private string m_DeviceID = string.Empty;
        public string DeviceID
        {
            get { return m_DeviceID; }
            set { m_DeviceID = value; }
        }

        private ConnectionID_Device_t m_DeviceType = ConnectionID_Device_t.STATIC_ID;
        public ConnectionID_Device_t DeviceType
        {
            get { return m_DeviceType; }
            set { m_DeviceType = value; }
        }

        private string m_UserInfo = string.Empty;
        public string UserInfo
        {
            get { return m_UserInfo; }
            set { m_UserInfo = value; }
        }
    }

    public class TSAPIPickupCallRequest : TSAPIRequest
    {
        private int m_CallID = -1;
        public int CallID
        {
            get { return m_CallID; }
            set { m_CallID = value; }
        }

        private string m_DeviceID = string.Empty;
        public string DeviceID
        {
            get { return m_DeviceID; }
            set { m_DeviceID = value; }
        }

        private ConnectionID_Device_t m_DeviceType = ConnectionID_Device_t.STATIC_ID;
        public ConnectionID_Device_t DeviceType
        {
            get { return m_DeviceType; }
            set { m_DeviceType = value; }
        }

        private string m_CalledDevice = string.Empty;
        public string CalledDevice
        {
            get { return m_CalledDevice; }
            set { m_CalledDevice = value; }
        }
    }

    public class TSAPIRetrieveCallRequest : TSAPIRequest
    {
        private int m_CallID = -1;
        public int CallID
        {
            get { return m_CallID; }
            set { m_CallID = value; }
        }

        private string m_DeviceID = string.Empty;
        public string DeviceID
        {
            get { return m_DeviceID; }
            set { m_DeviceID = value; }
        }

        private ConnectionID_Device_t m_DeviceType = ConnectionID_Device_t.STATIC_ID;
        public ConnectionID_Device_t DeviceType
        {
            get { return m_DeviceType; }
            set { m_DeviceType = value; }
        }
    }

    public class TSAPITransferCallRequest : TSAPIRequest
    {
        private int m_HeldCallID = -1;
        public int HeldCallID
        {
            get { return m_HeldCallID; }
            set { m_HeldCallID = value; }
        }

        private int m_ActiveCallID = -1;
        public int ActiveCallID
        {
            get { return m_ActiveCallID; }
            set { m_ActiveCallID = value; }
        }

        private string m_DeviceID = string.Empty;
        public string DeviceID
        {
            get { return m_DeviceID; }
            set { m_DeviceID = value; }
        }

        private ConnectionID_Device_t m_DeviceType = ConnectionID_Device_t.STATIC_ID;
        public ConnectionID_Device_t DeviceType
        {
            get { return m_DeviceType; }
            set { m_DeviceType = value; }
        }
    }

    public class TSAPIClearCallRequest : TSAPIRequest
    {
        private int m_CallID = -1;
        public int CallID
        {
            get { return m_CallID; }
            set { m_CallID = value; }
        }

        private string m_DeviceID = string.Empty;
        public string DeviceID
        {
            get { return m_DeviceID; }
            set { m_DeviceID = value; }
        }

        private ConnectionID_Device_t m_DeviceType = ConnectionID_Device_t.STATIC_ID;
        public ConnectionID_Device_t DeviceType
        {
            get { return m_DeviceType; }
            set { m_DeviceType = value; }
        }
    }

    public class TSAPIConferenceCallRequest : TSAPIRequest
    {
        private int m_HeldCallID = -1;
        public int HeldCallID
        {
            get { return m_HeldCallID; }
            set { m_HeldCallID = value; }
        }

        private int m_ActiveCallID = -1;
        public int ActiveCallID
        {
            get { return m_ActiveCallID; }
            set { m_ActiveCallID = value; }
        }

        private string m_DeviceID = string.Empty;
        public string DeviceID
        {
            get { return m_DeviceID; }
            set { m_DeviceID = value; }
        }

        private ConnectionID_Device_t m_DeviceType = ConnectionID_Device_t.STATIC_ID;
        public ConnectionID_Device_t DeviceType
        {
            get { return m_DeviceType; }
            set { m_DeviceType = value; }
        }
    }

    public class TSAPISetDoNotDisturbRequest : TSAPIRequest
    {
        private string m_DeviceID = string.Empty;
        public string DeviceID
        {
            get { return m_DeviceID; }
            set { m_DeviceID = value; }
        }

        private bool m_DoNotDisturb = true;
        public bool DoNotDisturb
        {
            get { return m_DoNotDisturb; }
            set { m_DoNotDisturb = value; }
        }
    }

    public class TSAPISetAgentStateRequest : TSAPIRequest
    {
        private string m_DeviceID = string.Empty;
        public string DeviceID
        {
            get { return m_DeviceID; }
            set { m_DeviceID = value; }
        }

        private string m_AgentID = string.Empty;
        public string AgentID
        {
            get { return m_AgentID; }
            set { m_AgentID = value; }
        }

        private string m_AgentGroup = string.Empty;
        public string AgentGroup
        {
            get { return m_AgentGroup; }
            set { m_AgentGroup = value; }
        }

        private string m_AgentPassword = string.Empty;
        public string AgentPassword
        {
            get { return m_AgentPassword; }
            set { m_AgentPassword = value; }
        }

        private AgentMode_t m_AgentMode = AgentMode_t.AM_READY;
        public AgentMode_t AgentMode
        {
            get { return m_AgentMode; }
            set { m_AgentMode = value; }
        }

        private ATTWorkMode_t m_WorkMode = ATTWorkMode_t.WM_NONE;
        public ATTWorkMode_t WorkMode
        {
            get { return m_WorkMode; }
            set { m_WorkMode = value; }
        }

        public int ReasonCode { get; set; }

        private bool m_EnablePending = true;
        public bool EnablePending
        {
            get { return m_EnablePending; }
            set { m_EnablePending = value; }
        }
    }

    public class TSAPIEscapeServiceRequest : TSAPIRequest
    {
        public int CallID { get; set; }

        private string m_DeviceID = string.Empty;
        public string DeviceID
        {
            get { return m_DeviceID; }
            set { m_DeviceID = value; }
        }

        private EscapeServiceType m_RequestType = EscapeServiceType.QUERY_ACD_SPLIT;
        public EscapeServiceType RequestType
        {
            get { return m_RequestType; }
            set { m_RequestType = value; }
        }
    }

    public class TSAPISingleStepTransferCallRequest : TSAPIEscapeServiceRequest
    {
        private string m_TransferredTo = string.Empty;
        public string TransferredTo
        {
            get { return m_TransferredTo; }
            set { m_TransferredTo = value; }
        }
    }

    public class TSAPISendDTMFToneRequest : TSAPIEscapeServiceRequest
    {
        private string m_Tones = string.Empty;
        public string Tones
        {
            get { return m_Tones; }
            set { m_Tones = value; }
        }

        public short ToneDuration { get; set; }

        public short PauseDuration { get; set; }
    }

    public enum EscapeServiceType
    {
        QUERY_ACD_SPLIT,
        QUERY_UCID,
        SINGLE_STEP_TRANSFER,
        SEND_DTMF_TONE
    }
}
