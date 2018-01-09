namespace TSAPIClient.CSTA
{
    public enum SelectValue_t
    {
        SV_NORMAL = 0,
        SV_LEAST_COST = 1,
        SV_EMERGENCY = 2,
        SV_ACD = 3,
        SV_USER_DEFINED = 4,
    }

    public enum StreamType_t
    {
        /// <summary>
        /// identifies a request as a CSTA call control stream. This
        /// stream can be used for TSAPI service requests and responses which
        /// begin with the prefix csta or CSTA.
        /// </summary>
        ST_CSTA = 1,

        /// <summary>
        /// requests an OAM stream. (The AE Services TSAPI Service
        /// does not support this value).
        /// </summary>
        ST_OAM = 2,
        ST_DIRECTORY = 3,
        ST_NMSRV = 4
    }

    public enum InvokeIDType_t
    {
        /// <summary>
        /// When APP_GEN_ID is selected, the application will provide an
        /// invokeID with every TSAPI function call that requires an invokeID.
        /// TSAPI will return the supplied invokeID value to the application in
        /// the confirmation event for the service request. Application-generated
        /// invokeID values can be any 32-bit value.
        /// </summary>
        APP_GEN_ID = 0,

        /// <summary>
        /// When LIB_GEN_ID is selected, the ACS Library will automatically
        /// generate an invokeID and will return its value upon successful
        /// completion of the function call. The value will be the return from the
        /// function call (RetCode_t). Library-generated invokeIDs are always in
        /// the range 1 to 32767.
        /// </summary>
        LIB_GEN_ID = 1
    }

    public enum Level_t
    {
        ACS_LEVEL1 = 1,
        ACS_LEVEL2 = 2,
        ACS_LEVEL3 = 3,
        ACS_LEVEL4 = 4
    }

    public enum ACSAuthType_t
    {
        REQUIRES_EXTERNAL_AUTH = -1,
        AUTH_LOGIN_ID_ONLY = 0,
        AUTH_LOGIN_ID_IS_DEFAULT = 1,
        NEED_LOGIN_ID_AND_PASSWD = 2,
        ANY_LOGIN_ID = 3
    }

    public enum Feature_t
    {
        FT_CAMP_ON = 0,
        FT_CALL_BACK = 1,
        FT_INTRUDE = 2
    }

    public enum ACSEncodeType_t
    {
        CAN_USE_BINDERY_ENCRYPTION = 1,
        NDS_AUTH_CONNID = 2,
        WIN_NT_LOCAL = 3,
        WIN_NT_NAMED_PIPE = 4,
        WIN_NT_WRITE_DATA = 5,
    }

    public enum ACSUniversalFailure_t
    {
        TSERVER_STREAM_FAILED = 0,
        TSERVER_NO_THREAD = 1,
        TSERVER_BAD_DRIVER_ID = 2,
        TSERVER_DEAD_DRIVER = 3,
        TSERVER_MESSAGE_HIGH_WATER_MARK = 4,
        TSERVER_FREE_BUFFER_FAILED = 5,
        TSERVER_SEND_TO_DRIVER = 6,
        TSERVER_RECEIVE_FROM_DRIVER = 7,
        TSERVER_REGISTRATION_FAILED = 8,
        TSERVER_SPX_FAILED = 9,
        TSERVER_TRACE = 10,
        TSERVER_NO_MEMORY = 11,
        TSERVER_ENCODE_FAILED = 12,
        TSERVER_DECODE_FAILED = 13,
        TSERVER_BAD_CONNECTION = 14,
        TSERVER_BAD_PDU = 15,
        TSERVER_NO_VERSION = 16,
        TSERVER_ECB_MAX_EXCEEDED = 17,
        TSERVER_NO_ECBS = 18,
        TSERVER_NO_SDB = 19,
        TSERVER_NO_SDB_CHECK_NEEDED = 20,
        TSERVER_SDB_CHECK_NEEDED = 21,
        TSERVER_BAD_SDB_LEVEL = 22,
        TSERVER_BAD_SERVERID = 23,
        TSERVER_BAD_STREAM_TYPE = 24,
        TSERVER_BAD_PASSWORD_OR_LOGIN = 25,
        TSERVER_NO_USER_RECORD = 26,
        TSERVER_NO_DEVICE_RECORD = 27,
        TSERVER_DEVICE_NOT_ON_LIST = 28,
        TSERVER_USERS_RESTRICTED_HOME = 30,
        TSERVER_NO_AWAYPERMISSION = 31,
        TSERVER_NO_HOMEPERMISSION = 32,
        TSERVER_NO_AWAY_WORKTOP = 33,
        TSERVER_BAD_DEVICE_RECORD = 34,
        TSERVER_DEVICE_NOT_SUPPORTED = 35,
        TSERVER_INSUFFICIENT_PERMISSION = 36,
        TSERVER_NO_RESOURCE_TAG = 37,
        TSERVER_INVALID_MESSAGE = 38,
        TSERVER_EXCEPTION_LIST = 39,
        TSERVER_NOT_ON_OAM_LIST = 40,
        TSERVER_PBX_ID_NOT_IN_SDB = 41,
        TSERVER_USER_LICENSES_EXCEEDED = 42,
        TSERVER_OAM_DROP_CONNECTION = 43,
        TSERVER_NO_VERSION_RECORD = 44,
        TSERVER_OLD_VERSION_RECORD = 45,
        TSERVER_BAD_PACKET = 46,
        TSERVER_OPEN_FAILED = 47,
        TSERVER_OAM_IN_USE = 48,
        TSERVER_DEVICE_NOT_ON_HOME_LIST = 49,
        TSERVER_DEVICE_NOT_ON_CALL_CONTROL_LIST = 50,
        TSERVER_DEVICE_NOT_ON_AWAY_LIST = 51,
        TSERVER_DEVICE_NOT_ON_ROUTE_LIST = 52,
        TSERVER_DEVICE_NOT_ON_MONITOR_DEVICE_LIST = 53,
        TSERVER_DEVICE_NOT_ON_MONITOR_CALL_DEVICE_LIST = 54,
        TSERVER_NO_CALL_CALL_MONITOR_PERMISSION = 55,
        TSERVER_HOME_DEVICE_LIST_EMPTY = 56,
        TSERVER_CALL_CONTROL_LIST_EMPTY = 57,
        TSERVER_AWAY_LIST_EMPTY = 58,
        TSERVER_ROUTE_LIST_EMPTY = 59,
        TSERVER_MONITOR_DEVICE_LIST_EMPTY = 60,
        TSERVER_MONITOR_CALL_DEVICE_LIST_EMPTY = 61,
        TSERVER_USER_AT_HOME_WORKTOP = 62,
        TSERVER_DEVICE_LIST_EMPTY = 63,
        TSERVER_BAD_GET_DEVICE_LEVEL = 64,
        TSERVER_DRIVER_UNREGISTERED = 65,
        TSERVER_NO_ACS_STREAM = 66,
        TSERVER_DROP_OAM = 67,
        TSERVER_ECB_TIMEOUT = 68,
        TSERVER_BAD_ECB = 69,
        TSERVER_ADVERTISE_FAILED = 70,
        TSERVER_NETWARE_FAILURE = 71,
        TSERVER_TDI_QUEUE_FAULT = 72,
        TSERVER_DRIVER_CONGESTION = 73,
        TSERVER_NO_TDI_BUFFERS = 74,
        TSERVER_OLD_INVOKEID = 75,
        TSERVER_HWMARK_TO_LARGE = 76,
        TSERVER_SET_ECB_TO_LOW = 77,
        TSERVER_NO_RECORD_IN_FILE = 78,
        TSERVER_ECB_OVERDUE = 79,
        TSERVER_BAD_PW_ENCRYPTION = 80,
        TSERVER_BAD_TSERV_PROTOCOL = 81,
        TSERVER_BAD_DRIVER_PROTOCOL = 82,
        TSERVER_BAD_TRANSPORT_TYPE = 83,
        TSERVER_PDU_VERSION_MISMATCH = 84,
        TSERVER_VERSION_MISMATCH = 85,
        TSERVER_LICENSE_MISMATCH = 86,
        TSERVER_BAD_ATTRIBUTE_LIST = 87,
        TSERVER_BAD_TLIST_TYPE = 88,
        TSERVER_BAD_PROTOCOL_FORMAT = 89,
        TSERVER_OLD_TSLIB = 90,
        TSERVER_BAD_LICENSE_FILE = 91,
        TSERVER_NO_PATCHES = 92,
        TSERVER_SYSTEM_ERROR = 93,
        TSERVER_OAM_LIST_EMPTY = 94,
        TSERVER_TCP_FAILED = 95,
        TSERVER_SPX_DISABLED = 96,
        TSERVER_TCP_DISABLED = 97,
        TSERVER_REQUIRED_MODULES_NOT_LOADED = 98,
        TSERVER_TRANSPORT_IN_USE_BY_OAM = 99,
        TSERVER_NO_NDS_OAM_PERMISSION = 100,
        TSERVER_OPEN_SDB_LOG_FAILED = 101,
        TSERVER_INVALID_LOG_SIZE = 102,
        TSERVER_WRITE_SDB_LOG_FAILED = 103,
        TSERVER_NT_FAILURE = 104,
        TSERVER_LOAD_LIB_FAILED = 105,
        TSERVER_INVALID_DRIVER = 106,
        TSERVER_REGISTRY_ERROR = 107,
        TSERVER_DUPLICATE_ENTRY = 108,
        TSERVER_DRIVER_LOADED = 109,
        TSERVER_DRIVER_NOT_LOADED = 110,
        TSERVER_NO_LOGON_PERMISSION = 111,
        TSERVER_ACCOUNT_DISABLED = 112,
        TSERVER_NO_NETLOGON = 113,
        TSERVER_ACCT_RESTRICTED = 114,
        TSERVER_INVALID_LOGON_TIME = 115,
        TSERVER_INVALID_WORKSTATION = 116,
        TSERVER_ACCT_LOCKED_OUT = 117,
        TSERVER_PASSWORD_EXPIRED = 118,
        DRIVER_DUPLICATE_ACSHANDLE = 1000,
        DRIVER_INVALID_ACS_REQUEST = 1001,
        DRIVER_ACS_HANDLE_REJECTION = 1002,
        DRIVER_INVALID_CLASS_REJECTION = 1003,
        DRIVER_GENERIC_REJECTION = 1004,
        DRIVER_RESOURCE_LIMITATION = 1005,
        DRIVER_ACSHANDLE_TERMINATION = 1006,
        DRIVER_LINK_UNAVAILABLE = 1007,
        DRIVER_OAM_IN_USE = 1008
    }

    public enum CSTAUniversalFailure_t
    {
        GENERIC_UNSPECIFIED = 0,
        GENERIC_OPERATION = 1,
        REQUEST_INCOMPATIBLE_WITH_OBJECT = 2,
        VALUE_OUT_OF_RANGE = 3,
        OBJECT_NOT_KNOWN = 4,
        INVALID_CALLING_DEVICE = 5,
        INVALID_CALLED_DEVICE = 6,
        INVALID_FORWARDING_DESTINATION = 7,
        PRIVILEGE_VIOLATION_ON_SPECIFIED_DEVICE = 8,
        PRIVILEGE_VIOLATION_ON_CALLED_DEVICE = 9,
        PRIVILEGE_VIOLATION_ON_CALLING_DEVICE = 10,
        INVALID_CSTA_CALL_IDENTIFIER = 11,
        INVALID_CSTA_DEVICE_IDENTIFIER = 12,
        INVALID_CSTA_CONNECTION_IDENTIFIER = 13,
        INVALID_DESTINATION = 14,
        INVALID_FEATURE = 15,
        INVALID_ALLOCATION_STATE = 16,
        INVALID_CROSS_REF_ID = 17,
        INVALID_OBJECT_TYPE = 18,
        SECURITY_VIOLATION = 19,
        GENERIC_STATE_INCOMPATIBILITY = 21,
        INVALID_OBJECT_STATE = 22,
        INVALID_CONNECTION_ID_FOR_ACTIVE_CALL = 23,
        NO_ACTIVE_CALL = 24,
        NO_HELD_CALL = 25,
        NO_CALL_TO_CLEAR = 26,
        NO_CONNECTION_TO_CLEAR = 27,
        NO_CALL_TO_ANSWER = 28,
        NO_CALL_TO_COMPLETE = 29,
        GENERIC_SYSTEM_RESOURCE_AVAILABILITY = 31,
        SERVICE_BUSY = 32,
        RESOURCE_BUSY = 33,
        RESOURCE_OUT_OF_SERVICE = 34,
        NETWORK_BUSY = 35,
        NETWORK_OUT_OF_SERVICE = 36,
        OVERALL_MONITOR_LIMIT_EXCEEDED = 37,
        CONFERENCE_MEMBER_LIMIT_EXCEEDED = 38,
        GENERIC_SUBSCRIBED_RESOURCE_AVAILABILITY = 41,
        OBJECT_MONITOR_LIMIT_EXCEEDED = 42,
        EXTERNAL_TRUNK_LIMIT_EXCEEDED = 43,
        OUTSTANDING_REQUEST_LIMIT_EXCEEDED = 44,
        GENERIC_PERFORMANCE_MANAGEMENT = 51,
        PERFORMANCE_LIMIT_EXCEEDED = 52,
        UNSPECIFIED_SECURITY_ERROR = 60,
        SEQUENCE_NUMBER_VIOLATED = 61,
        TIME_STAMP_VIOLATED = 62,
        PAC_VIOLATED = 63,
        SEAL_VIOLATED = 64,
        GENERIC_UNSPECIFIED_REJECTION = 70,
        GENERIC_OPERATION_REJECTION = 71,
        DUPLICATE_INVOCATION_REJECTION = 72,
        UNRECOGNIZED_OPERATION_REJECTION = 73,
        MISTYPED_ARGUMENT_REJECTION = 74,
        RESOURCE_LIMITATION_REJECTION = 75,
        ACS_HANDLE_TERMINATION_REJECTION = 76,
        SERVICE_TERMINATION_REJECTION = 77,
        REQUEST_TIMEOUT_REJECTION = 78,
        REQUESTS_ON_DEVICE_EXCEEDED_REJECTION = 79,
        UNRECOGNIZED_APDU_REJECTION = 80,
        MISTYPED_APDU_REJECTION = 81,
        BADLY_STRUCTURED_APDU_REJECTION = 82,
        INITIATOR_RELEASING_REJECTION = 83,
        UNRECOGNIZED_LINKEDID_REJECTION = 84,
        LINKED_RESPONSE_UNEXPECTED_REJECTION = 85,
        UNEXPECTED_CHILD_OPERATION_REJECTION = 86,
        MISTYPED_RESULT_REJECTION = 87,
        UNRECOGNIZED_ERROR_REJECTION = 88,
        UNEXPECTED_ERROR_REJECTION = 89,
        MISTYPED_PARAMETER_REJECTION = 90,
        NON_STANDARD = 100,
    }

    public enum CSTAEventCause_t
    {
        EC_NONE = -1,
        EC_ACTIVE_MONITOR = 1,
        EC_ALTERNATE = 2,
        EC_BUSY = 3,
        EC_CALL_BACK = 4,
        EC_CALL_CANCELLED = 5,
        EC_CALL_FORWARD_ALWAYS = 6,
        EC_CALL_FORWARD_BUSY = 7,
        EC_CALL_FORWARD_NO_ANSWER = 8,
        EC_CALL_FORWARD = 9,
        EC_CALL_NOT_ANSWERED = 10,
        EC_CALL_PICKUP = 11,
        EC_CAMP_ON = 12,
        EC_DEST_NOT_OBTAINABLE = 13,
        EC_DO_NOT_DISTURB = 14,
        EC_INCOMPATIBLE_DESTINATION = 15,
        EC_INVALID_ACCOUNT_CODE = 16,
        EC_KEY_CONFERENCE = 17,
        EC_LOCKOUT = 18,
        EC_MAINTENANCE = 19,
        EC_NETWORK_CONGESTION = 20,
        EC_NETWORK_NOT_OBTAINABLE = 21,
        EC_NEW_CALL = 22,
        EC_NO_AVAILABLE_AGENTS = 23,
        EC_OVERRIDE = 24,
        EC_PARK = 25,
        EC_OVERFLOW = 26,
        EC_RECALL = 27,
        EC_REDIRECTED = 28,
        EC_REORDER_TONE = 29,
        EC_RESOURCES_NOT_AVAILABLE = 30,
        EC_SILENT_MONITOR = 31,
        EC_TRANSFER = 32,
        EC_TRUNKS_BUSY = 33,
        EC_VOICE_UNIT_INITIATOR = 34,
    }

    public enum DeviceIDType_t
    {
        DEVICE_IDENTIFIER = 0,
        IMPLICIT_PUBLIC = 20,
        EXPLICIT_PUBLIC_UNKNOWN = 30,
        EXPLICIT_PUBLICintERNATIONAL = 31,
        EXPLICIT_PUBLIC_NATIONAL = 32,
        EXPLICIT_PUBLIC_NETWORK_SPECIFIC = 33,
        EXPLICIT_PUBLIC_SUBSCRIBER = 34,
        EXPLICIT_PUBLIC_ABBREVIATED = 35,
        IMPLICIT_PRIVATE = 40,
        EXPLICIT_PRIVATE_UNKNOWN = 50,
        EXPLICIT_PRIVATE_LEVEL3_REGIONAL_NUMBER = 51,
        EXPLICIT_PRIVATE_LEVEL2_REGIONAL_NUMBER = 52,
        EXPLICIT_PRIVATE_LEVEL1_REGIONAL_NUMBER = 53,
        EXPLICIT_PRIVATE_PTN_SPECIFIC_NUMBER = 54,
        EXPLICIT_PRIVATE_LOCAL_NUMBER = 55,
        EXPLICIT_PRIVATE_ABBREVIATED = 56,
        OTHER_PLAN = 60,
        TRUNK_IDENTIFIER = 70,
        TRUNK_GROUP_IDENTIFIER = 71,
    }

    public enum DeviceIDStatus_t
    {
        ID_PROVIDED = 0,
        ID_NOT_KNOWN = 1,
        ID_NOT_REQUIRED = 2,
    }

    public enum ConnectionID_Device_t
    {
        STATIC_ID = 0,
        DYNAMIC_ID = 1,
    }

    public enum LocalConnectionState_t
    {
        CS_NONE = -1,
        CS_NULL = 0,
        CS_INITIATE = 1,
        CS_ALERTING = 2,
        CS_CONNECT = 3,
        CS_HOLD = 4,
        CS_QUEUED = 5,
        CS_FAIL = 6,
    }

    public enum ForwardingType_t
    {
        FWD_IMMEDIATE = 0,
        FWD_BUSY = 1,
        FWD_NO_ANS = 2,
        FWD_BUSYint = 3,
        FWD_BUSY_EXT = 4,
        FWD_NO_ANSint = 5,
        FWD_NO_ANS_EXT = 6,
    }

    public enum AllocationState_t
    {
        AS_CALL_DELIVERED = 0,
        AS_CALL_ESTABLISHED = 1,
    }

    public enum AgentState_t
    {
        AG_NOT_READY = 0,
        AG_NULL = 1,
        AG_READY = 2,
        AG_WORK_NOT_READY = 3,
        AG_WORK_READY = 4,
    }

    public enum DeviceType_t
    {
        DT_STATION = 0,
        DT_LINE = 1,
        DT_BUTTON = 2,
        DT_ACD = 3,
        DT_TRUNK = 4,
        DT_OPERATOR = 5,
        DT_STATION_GROUP = 16,
        DT_LINE_GROUP = 17,
        DT_BUTTON_GROUP = 18,
        DT_ACD_GROUP = 19,
        DT_TRUNK_GROUP = 20,
        DT_OPERATOR_GROUP = 21,
        DT_OTHER = 255,
    }

    public enum AgentMode_t
    {
        AM_LOG_IN = 0,
        AM_LOG_OUT = 1,
        AM_NOT_READY = 2,
        AM_READY = 3,
        AM_WORK_NOT_READY = 4,
        AM_WORK_READY = 5,
    }

    public enum SystemStatus_t
    {
        SS_INITIALIZING = 0,
        SS_ENABLED = 1,
        SS_NORMAL = 2,
        SS_MESSAGES_LOST = 3,
        SS_DISABLED = 4,
        SS_OVERLOAD_IMMINENT = 5,
        SS_OVERLOAD_REACHED = 6,
        SS_OVERLOAD_RELIEVED = 7,
    }

    public enum CSTALevel_t
    {
        CSTA_HOME_WORK_TOP = 1,
        CSTA_AWAY_WORK_TOP = 2,
        CSTA_DEVICE_DEVICE_MONITOR = 3,
        CSTA_CALL_DEVICE_MONITOR = 4,
        CSTA_CALL_CONTROL = 5,
        CSTA_ROUTING = 6,
        CSTA_CALL_CALL_MONITOR = 7,
    }

    public enum SDBLevel_t
    {
        NO_SDB_CHECKING = -1,
        ACS_ONLY = 1,
        ACS_AND_CSTA_CHECKING = 0,
    }
}
