namespace TSAPIClient.ATT
{
    public enum ATTDeliveredType_t
    {
        DELIVERED_TO_ACD = 1,
        DELIVERED_TO_STATION = 2,
        DELIVERED_OTHER = 3 /* not in use */
    }

    public enum ATTInterflow_t
    {
        LAI_NO_INTERFLOW = -1, /* indicates info not present */
        LAI_ALL_INTERFLOW = 0,
        LAI_THRESHOLD_INTERFLOW = 1,
        LAI_VECTORING_INTERFLOW = 2
    }

    public enum ATTConsultMode_t
    {
        ATT_CM_NONE = 0,
        ATT_CM_CONSULTATION = 1,
        ATT_CM_TRANSFER = 2,
        ATT_CM_CONFERENCE = 3,
        ATT_CM_NOT_PROVIDED = 4
    }

    public enum ATTLocalCallState_t
    {
        ATT_CS_INITIATED = 1,
        ATT_CS_ALERTING = 2,
        ATT_CS_CONNECTED = 3,
        ATT_CS_HELD = 4,
        ATT_CS_BRIDGED = 5,
        ATT_CS_OTHER = 6
    }

    public enum ATTDropResource_t
    {
        DR_NONE = -1, /* indicates not specified */
        DR_CALL_CLASSIFIER = 0, /* call classifier to be dropped */
        DR_TONE_GENERATOR = 1 /* tone generator to be dropped */
    }

    public enum ATTProgressDescription_t
    {
        PD_NONE = -1, /* not provided */
        PD_CALL_OFF_ISDN = 1, /* call is not end-to-end ISDN, call progress in-band */
        PD_DEST_NOT_ISDN = 2, /* destination address is non-ISDN */
        PD_ORIG_NOT_ISDN = 3, /* origination address is non-ISDN */
        PD_CALL_ON_ISDN = 4, /* call has returned to ISDN */
        PD_INBAND = 8 /* in-band information now available */
    }

    public enum ATTLinkState_t
    {
        LS_LINK_UNAVAIL = 0, /* the link is disabled */
        LS_LINK_UP = 1, /* the link is up */
        LS_LINK_DOWN = 2 /* the link is down */
    }

    public enum ATTProgressLocation_t
    {
        PL_NONE = -1, /* not provided */
        PL_USER = 0, /* user */
        PL_PUB_LOCAL = 1, /* public network serving local user */
        PL_PUB_REMOTE = 4, /* public network serving remote user */
        PL_PRIV_REMOTE = 5 /* private network serving remote user */
    }

    public enum ATTChargeType_t
    {
        CT_INTERMEDIATE_CHARGE = 1,
        CT_FINAL_CHARGE = 2,
        CT_SPLIT_CHARGE = 3
    }

    public enum ATTChargeError_t
    {
        CE_NONE = 0,
        CE_NO_FINAL_CHARGE = 1,
        CE_LESS_FINAL_CHARGE = 2,
        CE_CHARGE_TOO_LARGE = 3,
        CE_NETWORK_BUSY = 4
    }

    public enum ATTWorkMode_t
    {
        WM_NONE = -1,
        WM_AUX_WORK = 1,
        WM_AFTCAL_WK = 2,
        WM_AUTO_IN = 3,
        WM_MANUAL_IN = 4
    }

    public enum ATTDeviceType_t
    {
        ATT_DT_UNKNOWN = 0,
        ATT_DT_ACD_SPLIT = 1,
        ATT_DT_ANNOUNCEMENT = 2,
        ATT_DT_DATA = 3,
        ATT_DT_LOGICAL_AGENT = 4,
        ATT_DT_STATION = 5,
        ATT_DT_TRUNK_ACCESS_CODE = 6,
        ATT_DT_VDN = 7
    }

    public enum ATTExtensionClass_t
    {
        EC_VDN = 0,
        EC_ACD_SPLIT = 1,
        EC_ANNOUNCEMENT = 2,
        EC_DATA = 4,
        EC_ANALOG = 5,
        EC_PROPRIETARY = 6,
        EC_BRI = 7,
        EC_CTI = 8,
        EC_LOGICAL_AGENT = 9,
        EC_OTHER = 10
    }

    public enum ATTTalkState_t
    {
        TS_ON_CALL = 0,
        TS_IDLE = 1
    }

    public enum ATTPriority_t
    {
        LAI_NOT_IN_QUEUE = 0,
        LAI_LOW = 1,
        LAI_MEDIUM = 2,
        LAI_HIGH = 3,
        LAI_TOP = 4
    }

    public enum ATTUserEnteredCodeType_t
    {
        UE_NONE = -1, /* indicates not provided */
        UE_ANY = 0,
        UE_LOGIN_DIGITS = 2,
        UE_CALL_PROMPTER = 5,
        UE_DATA_BASE_PROVIDED = 17,
        UE_TONE_DETECTOR = 32
    }

    public enum ATTUserEnteredCodeIndicator_t
    {
        UE_COLLECT = 0,
        UE_ENTERED = 1
    }

    public enum ATTReasonCode_t
    {
        AR_NONE = 0,
        /* no reason code provided */
        AR_ANSWER_NORMAL = 1,
        /* answer supervision from the
        * network or internal answer */
        AR_ANSWER_TIMED = 2,
        /* answer assumed based on
        * internal timer */
        AR_ANSWER_VOICE_ENERGY = 3,
        /* voice energy detection by call
        * classifier */
        AR_ANSWER_MACHINE_DETECTED = 4,/* answering machine detected */
        AR_SIT_REORDER = 5,
        /* switch equipment congestion */
        AR_SIT_NO_CIRCUIT = 6,
        /* no circuit or channel available
        */
        AR_SIT_INTERCEPT = 7,
        /* number changed */
        AR_SIT_VACANT_CODE = 8,
        /* unassigned number */
        AR_SIT_INEFFECTIVE_OTHER = 9, /* invalid number */
        AR_SIT_UNKNOWN = 10,
        /* normal unspecified */
        AR_IN_QUEUE = 11,
        /* call still in queue - for
        * Delivered Event only */
        AR_SERVICE_OBSERVER = 12
        /* service observer connected */
    }

    public enum ATTUUIProtocolType_t
    {
        UUI_NONE = -1, /* indicates not specified */
        UUI_USER_SPECIFIC = 0, /* user-specific */
        UUI_IA5_ASCII = 4 /* null terminated ASCII * character string */
    }

    public enum ATTReasonForCallInfo_t
    {
        OR_NONE = 0, /* indicates not present */
        OR_CONSULTATION = 1,
        OR_CONFERENCED = 2,
        OR_TRANSFERRED = 3,
        OR_NEW_CALL = 4
    }
}
