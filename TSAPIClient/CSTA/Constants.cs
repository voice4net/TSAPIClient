namespace TSAPIClient.CSTA
{
    public static class Constants
    {
        /// <summary>
        /// This return value indicates that a stream could not be opened because initialization of the OpenSSL library failed.
        /// </summary>
        public const int ACSERR_SSL_INIT_FAILED = -14;

        /// <summary>
        /// This return value indicates that a stream could not be opened because the SSL connection failed.
        /// </summary>
        public const int ACSERR_SSL_CONNECT_FAILED = -15;

        /// <summary>
        /// This return value indicates that a stream could not be opened because during the SSL handshake, the fully qualified domain name (FQDN) in the server certificate did not match the expected FQDN
        /// </summary>
        public const int ACSERR_SSL_FQDN_MISMATCH = -16;

        public const int ACS_SET_HEARTBEAT_INTERVAL = 14;
        public const int ACS_SET_HEARTBEAT_INTERVAL_CONF = 15;
        public const int ACS_OPEN_STREAM = 1;
        public const int ACS_OPEN_STREAM_CONF = 2;
        public const int ACS_CLOSE_STREAM = 3;
        public const int ACS_CLOSE_STREAM_CONF = 4;
        public const int ACS_ABORT_STREAM = 5;
        public const int ACS_UNIVERSAL_FAILURE_CONF = 6;
        public const int ACS_UNIVERSAL_FAILURE = 7;
        public const int ACS_KEY_REQUEST = 8;
        public const int ACS_KEY_REPLY = 9;
        public const int ACS_NAME_SRV_REQUEST = 10;
        public const int ACS_NAME_SRV_REPLY = 11;
        public const int ACS_AUTH_REPLY = 12;
        public const int ACS_AUTH_REPLY_TWO = 13;
        public const int CSTA_ALTERNATE_CALL = 1;
        public const int CSTA_ALTERNATE_CALL_CONF = 2;
        public const int CSTA_ANSWER_CALL = 3;
        public const int CSTA_ANSWER_CALL_CONF = 4;
        public const int CSTA_CALL_COMPLETION = 5;
        public const int CSTA_CALL_COMPLETION_CONF = 6;
        public const int CSTA_CLEAR_CALL = 7;
        public const int CSTA_CLEAR_CALL_CONF = 8;
        public const int CSTA_CLEAR_CONNECTION = 9;
        public const int CSTA_CLEAR_CONNECTION_CONF = 10;
        public const int CSTA_CONFERENCE_CALL = 11;
        public const int CSTA_CONFERENCE_CALL_CONF = 12;
        public const int CSTA_CONSULTATION_CALL = 13;
        public const int CSTA_CONSULTATION_CALL_CONF = 14;
        public const int CSTA_DEFLECT_CALL = 15;
        public const int CSTA_DEFLECT_CALL_CONF = 16;
        public const int CSTA_PICKUP_CALL = 17;
        public const int CSTA_PICKUP_CALL_CONF = 18;
        public const int CSTA_GROUP_PICKUP_CALL = 19;
        public const int CSTA_GROUP_PICKUP_CALL_CONF = 20;
        public const int CSTA_HOLD_CALL = 21;
        public const int CSTA_HOLD_CALL_CONF = 22;
        public const int CSTA_MAKE_CALL = 23;
        public const int CSTA_MAKE_CALL_CONF = 24;
        public const int CSTA_MAKE_PREDICTIVE_CALL = 25;
        public const int CSTA_MAKE_PREDICTIVE_CALL_CONF = 26;
        public const int CSTA_QUERY_MWI = 27;
        public const int CSTA_QUERY_MWI_CONF = 28;
        public const int CSTA_QUERY_DND = 29;
        public const int CSTA_QUERY_DND_CONF = 30;
        public const int CSTA_QUERY_FWD = 31;
        public const int CSTA_QUERY_FWD_CONF = 32;
        public const int CSTA_QUERY_AGENT_STATE = 33;
        public const int CSTA_QUERY_AGENT_STATE_CONF = 34;
        public const int CSTA_QUERY_LAST_NUMBER = 35;
        public const int CSTA_QUERY_LAST_NUMBER_CONF = 36;
        public const int CSTA_QUERY_DEVICE_INFO = 37;
        public const int CSTA_QUERY_DEVICE_INFO_CONF = 38;
        public const int CSTA_RECONNECT_CALL = 39;
        public const int CSTA_RECONNECT_CALL_CONF = 40;
        public const int CSTA_RETRIEVE_CALL = 41;
        public const int CSTA_RETRIEVE_CALL_CONF = 42;
        public const int CSTA_SET_MWI = 43;
        public const int CSTA_SET_MWI_CONF = 44;
        public const int CSTA_SET_DND = 45;
        public const int CSTA_SET_DND_CONF = 46;
        public const int CSTA_SET_FWD = 47;
        public const int CSTA_SET_FWD_CONF = 48;
        public const int CSTA_SET_AGENT_STATE = 49;
        public const int CSTA_SET_AGENT_STATE_CONF = 50;
        public const int CSTA_TRANSFER_CALL = 51;
        public const int CSTA_TRANSFER_CALL_CONF = 52;
        public const int CSTA_UNIVERSAL_FAILURE_CONF = 53;
        public const int CSTA_CALL_CLEARED = 54;
        public const int CSTA_CONFERENCED = 55;
        public const int CSTA_CONNECTION_CLEARED = 56;
        public const int CSTA_DELIVERED = 57;
        public const int CSTA_DIVERTED = 58;
        public const int CSTA_ESTABLISHED = 59;
        public const int CSTA_FAILED = 60;
        public const int CSTA_HELD = 61;
        public const int CSTA_NETWORK_REACHED = 62;
        public const int CSTA_ORIGINATED = 63;
        public const int CSTA_QUEUED = 64;
        public const int CSTA_RETRIEVED = 65;
        public const int CSTA_SERVICE_INITIATED = 66;
        public const int CSTA_TRANSFERRED = 67;
        public const int CSTA_CALL_INFORMATION = 68;
        public const int CSTA_DO_NOT_DISTURB = 69;
        public const int CSTA_FORWARDING = 70;
        public const int CSTA_MESSAGE_WAITING = 71;
        public const int CSTA_LOGGED_ON = 72;
        public const int CSTA_LOGGED_OFF = 73;
        public const int CSTA_NOT_READY = 74;
        public const int CSTA_READY = 75;
        public const int CSTA_WORK_NOT_READY = 76;
        public const int CSTA_WORK_READY = 77;
        public const int CSTA_ROUTE_REGISTER_REQ = 78;
        public const int CSTA_ROUTE_REGISTER_REQ_CONF = 79;
        public const int CSTA_ROUTE_REGISTER_CANCEL = 80;
        public const int CSTA_ROUTE_REGISTER_CANCEL_CONF = 81;
        public const int CSTA_ROUTE_REGISTER_ABORT = 82;
        public const int CSTA_ROUTE_REQUEST = 83;
        public const int CSTA_ROUTE_SELECT_REQUEST = 84;
        public const int CSTA_RE_ROUTE_REQUEST = 85;
        public const int CSTA_ROUTE_USED = 86;
        public const int CSTA_ROUTE_END = 87;
        public const int CSTA_ROUTE_END_REQUEST = 88;
        public const int CSTA_ESCAPE_SVC = 89;
        public const int CSTA_ESCAPE_SVC_CONF = 90;
        public const int CSTA_ESCAPE_SVC_REQ = 91;
        public const int CSTA_ESCAPE_SVC_REQ_CONF = 92;
        public const int CSTA_PRIVATE = 93;
        public const int CSTA_PRIVATE_STATUS = 94;
        public const int CSTA_SEND_PRIVATE = 95;
        public const int CSTA_BACK_IN_SERVICE = 96;
        public const int CSTA_OUT_OF_SERVICE = 97;
        public const int CSTA_REQ_SYS_STAT = 98;
        public const int CSTA_SYS_STAT_REQ_CONF = 99;
        public const int CSTA_SYS_STAT_START = 100;
        public const int CSTA_SYS_STAT_START_CONF = 101;
        public const int CSTA_SYS_STAT_STOP = 102;
        public const int CSTA_SYS_STAT_STOP_CONF = 103;
        public const int CSTA_CHANGE_SYS_STAT_FILTER = 104;
        public const int CSTA_CHANGE_SYS_STAT_FILTER_CONF = 105;
        public const int CSTA_SYS_STAT = 106;
        public const int CSTA_SYS_STAT_ENDED = 107;
        public const int CSTA_SYS_STAT_REQ = 108;
        public const int CSTA_REQ_SYS_STAT_CONF = 109;
        public const int CSTA_SYS_STAT_EVENT_SEND = 110;
        public const int CSTA_MONITOR_DEVICE = 111;
        public const int CSTA_MONITOR_CALL = 112;
        public const int CSTA_MONITOR_CALLS_VIA_DEVICE = 113;
        public const int CSTA_MONITOR_CONF = 114;
        public const int CSTA_CHANGE_MONITOR_FILTER = 115;
        public const int CSTA_CHANGE_MONITOR_FILTER_CONF = 116;
        public const int CSTA_MONITOR_STOP = 117;
        public const int CSTA_MONITOR_STOP_CONF = 118;
        public const int CSTA_MONITOR_ENDED = 119;
        public const int CSTA_SNAPSHOT_CALL = 120;
        public const int CSTA_SNAPSHOT_CALL_CONF = 121;
        public const int CSTA_SNAPSHOT_DEVICE = 122;
        public const int CSTA_SNAPSHOT_DEVICE_CONF = 123;
        public const int CSTA_GETAPI_CAPS = 124;
        public const int CSTA_GETAPI_CAPS_CONF = 125;
        public const int CSTA_GET_DEVICE_LIST = 126;
        public const int CSTA_GET_DEVICE_LIST_CONF = 127;
        public const int CSTA_QUERY_CALL_MONITOR = 128;
        public const int CSTA_QUERY_CALL_MONITOR_CONF = 129;
        public const int CSTA_ROUTE_REQUEST_EXT = 130;
        public const int CSTA_ROUTE_USED_EXT = 131;
        public const int CSTA_ROUTE_SELECT_INV_REQUEST = 132;
        public const int CSTA_ROUTE_END_INV_REQUEST = 133;
        public const int CF_CALL_CLEARED = 0x8000;
        public const int CF_CONFERENCED = 0x4000;
        public const int CF_CONNECTION_CLEARED = 0x2000;
        public const int CF_DELIVERED = 0x1000;
        public const int CF_DIVERTED = 0x0800;
        public const int CF_ESTABLISHED = 0x0400;
        public const int CF_FAILED = 0x0200;
        public const int CF_HELD = 0x0100;
        public const int CF_NETWORK_REACHED = 0x0080;
        public const int CF_ORIGINATED = 0x0040;
        public const int CF_QUEUED = 0x0020;
        public const int CF_RETRIEVED = 0x0010;
        public const int CF_SERVICE_INITIATED = 0x0008;
        public const int CF_TRANSFERRED = 0x0004;
        public const int FF_CALL_INFORMATION = 0x80;
        public const int FF_DO_NOT_DISTURB = 0x40;
        public const int FF_FORWARDING = 0x20;
        public const int FF_MESSAGE_WAITING = 0x10;
        public const int AF_LOGGED_ON = 0x80;
        public const int AF_LOGGED_OFF = 0x40;
        public const int AF_NOT_READY = 0x20;
        public const int AF_READY = 0x10;
        public const int AF_WORK_NOT_READY = 0x08;
        public const int AF_WORK_READY = 0x04;
        public const int MF_BACK_IN_SERVICE = 0x80;
        public const int MF_OUT_OF_SERVICE = 0x40;
        public const int noListAvailable = -1;
        public const int noCountAvailable = -2;
        public const int DC_VOICE = 0x80;
        public const int DC_DATA = 0x40;
        public const int DC_IMAGE = 0x20;
        public const int DC_OTHER = 0x10;
        public const int SF_INITIALIZING = 0x80;
        public const int SF_ENABLED = 0x40;
        public const int SF_NORMAL = 0x20;
        public const int SF_MESSAGES_LOST = 0x10;
        public const int SF_DISABLED = 0x08;
        public const int SF_OVERLOAD_IMMINENT = 0x04;
        public const int SF_OVERLOAD_REACHED = 0x02;
        public const int SF_OVERLOAD_RELIEVED = 0x01;
        public const int TSERV_SAP_CSTA = 0x0559;
        public const int CLIENT_SAP_CSTA = 0x5905;
        public const int TSERV_SAP_NMSRV = 0x055B;
        public const int CLIENT_SAP_NMSRV = 0x5B05;

        /// <summary>
        /// Successful function return
        /// </summary>
        public const int ACSPOSITIVE_ACK = 0;

        /// <summary>
        /// The API Version requested is invalid and not supported by the API Client Library.
        /// </summary>
        public const int ACSERR_APIVERDENIED = -1;

        /// <summary>
        /// One or more of the parameters is invalid.
        /// </summary>
        public const int ACSERR_BADPARAMETER = -2;

        /// <summary>
        /// This return indicates that an ACS stream is already established with the requested server.
        /// </summary>
        public const int ACSERR_DUPSTREAM = -3;

        /// <summary>
        /// This error return value indicates that no API Client Library Driver was found or installed on the system.
        /// </summary>
        public const int ACSERR_NODRIVER = -4;

        /// <summary>
        /// The requested Server is not present in the network.
        /// </summary>
        public const int ACSERR_NOSERVER = -5;

        /// <summary>
        /// There are insufficient resources to open an ACS stream.
        /// </summary>
        public const int ACSERR_NORESOURCE = -6;

        /// <summary>
        /// The user buffer size was smaller than the size of the next available event.
        /// </summary>
        public const int ACSERR_UBUFSMALL = -7;

        /// <summary>
        /// There were no messages available to return to the application.
        /// </summary>
        public const int ACSERR_NOMESSAGE = -8;

        /// <summary>
        /// The ACS stream has encountered an unspecified error.
        /// </summary>
        public const int ACSERR_UNKNOWN = -9;

        /// <summary>
        /// The ACS Handle is invalid.
        /// </summary>
        public const int ACSERR_BADHDL = -10;

        /// <summary>
        /// The ACS stream has failed due to network problems. No further operations are possible on this stream.
        /// </summary>
        public const int ACSERR_STREAM_FAILED = -11;

        /// <summary>
        /// There were not enough buffers available to place an outgoing message on the send queue. No message has been sent.
        /// </summary>
        public const int ACSERR_NOBUFFERS = -12;

        /// <summary>
        /// The send queue is full. No message has been sent.
        /// </summary>
        public const int ACSERR_QUEUE_FULL = -13;
        public const int ACSREQUEST = 0;
        public const int ACSUNSOLICITED = 1;
        public const int ACSCONFIRMATION = 2;
        public const int ACS_MAX_HEAP = 1024;
        public const int PRIVATE_DATA_ENCODING = 0;
        public const int CSTAREQUEST = 3;
        public const int CSTAUNSOLICITED = 4;
        public const int CSTACONFIRMATION = 5;
        public const int CSTAEVENTREPORT = 6;
        public const int CSTA_MAX_GET_DEVICE = 20;
        public const int CSTA_MAX_HEAP = 1024;        
        public const int CSTA_EVENT_MAX_SIZE = 2332;        
    }
}
