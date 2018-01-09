using System;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using TSAPIClient;
using TSAPIClient.ATT;
using TSAPIClient.CSTA;
using Constants = TSAPIClient.CSTA.Constants;

// ReSharper disable PossibleInvalidOperationException
namespace TSAPIClientDemo
{
    public partial class FrmTSAPIClient : Form
    {
        private Client m_Client;
        private int m_MonitorXRef;
        private int m_CallID;

        public FrmTSAPIClient()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            string serverID = cboServerNames.Text;
            string loginID = txtLoginID.Text;
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(serverID))
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(loginID))
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                return;
            }

            m_Client = new Client();

            BackgroundWorker worker = new BackgroundWorker();

            var connectCmd = new ConnectCommand(m_Client, serverID, loginID, password);

            worker.DoWork += (o, args) =>
            {
                connectCmd.Connect();

                worker.ReportProgress(100);
            };

            worker.RunWorkerCompleted += (o, args) =>
            {
                if (connectCmd.Connected)
                {
                    rtbOutput.AppendText("Connected!\n");
                    rtbOutput.AppendText(string.Format("ApiVersion={0}\n", connectCmd.ApiVersion));
                    rtbOutput.AppendText(string.Format("DriverVersion={0}\n", connectCmd.DriverVersion));
                    rtbOutput.AppendText(string.Format("LibraryVersion={0}\n", connectCmd.LibraryVersion));
                    rtbOutput.AppendText(string.Format("ServerVersion={0}\n", connectCmd.ServerVersion));
                    rtbOutput.AppendText(string.Format("PrivateDataVersion={0}\n", connectCmd.PrivateDataVersion));

                    m_Client.TSAPIEvent += onTSAPIEvent;
                }
                else
                {
                    rtbOutput.AppendText("Connection Failed!\n");
                    rtbOutput.AppendText(string.Format("ErrorMessage={0}\n", connectCmd.ErrorMessage));
                }
            };

            rtbOutput.AppendText("connect...\n");
            worker.RunWorkerAsync();
        }

        private void onTSAPIEvent(object sender, TSAPIEventArgs e)
        {
            try
            {
                TSAPIEventArgs tsapiEvent = e;
                CSTAEvent_t cstaEvent = e.cstaEvent;
                ATTEvent_t attEvent = e.attEvent;

                switch (cstaEvent.eventHeader.eventClass)
                {
                    case Constants.CSTACONFIRMATION:

                        #region CSTACONFIRMATION

                        switch (cstaEvent.eventHeader.eventType)
                        {
                            case Constants.CSTA_ALTERNATE_CALL_CONF:

                                #region CSTA_ALTERNATE_CALL_CONF

                                {
                                    if (cstaEvent.Event.cstaConfirmation.u.alternateCall != null)
                                    {
                                        CSTAAlternateCallConfEvent_t alternateCall = (CSTAAlternateCallConfEvent_t)cstaEvent.Event.cstaConfirmation.u.alternateCall;

                                        Console.WriteLine("onTSAPIEvent.CSTA_ALTERNATE_CALL_CONF: alternateCall={0};", alternateCall);
                                    }
                                }

                                break;

                                #endregion CSTA_ALTERNATE_CALL_CONF

                            case Constants.CSTA_ANSWER_CALL_CONF:

                                #region CSTA_ANSWER_CALL_CONF

                                {
                                    if (cstaEvent.Event.cstaConfirmation.u.answerCall != null)
                                    {
                                        CSTAAnswerCallConfEvent_t answerCall = (CSTAAnswerCallConfEvent_t)cstaEvent.Event.cstaConfirmation.u.answerCall;

                                        Console.WriteLine("onTSAPIEvent.CSTA_ANSWER_CALL_CONF: answerCall={0};", answerCall);
                                    }
                                }

                                break;

                                #endregion CSTA_ANSWER_CALL_CONF

                            case Constants.CSTA_CALL_COMPLETION_CONF:

                                #region CSTA_CALL_COMPLETION_CONF

                                {
                                    if (cstaEvent.Event.cstaConfirmation.u.callCompletion != null)
                                    {
                                        CSTACallCompletionConfEvent_t callCompletion = (CSTACallCompletionConfEvent_t)cstaEvent.Event.cstaConfirmation.u.callCompletion;

                                        Console.WriteLine("onTSAPIEvent.CSTA_CALL_COMPLETION_CONF: callCompletion={0};", callCompletion);
                                    }
                                }

                                break;

                                #endregion CSTA_CALL_COMPLETION_CONF

                            case Constants.CSTA_CLEAR_CALL_CONF:

                                #region CSTA_CLEAR_CALL_CONF

                                {
                                    if (cstaEvent.Event.cstaConfirmation.u.clearCall != null)
                                    {
                                        CSTAClearCallConfEvent_t clearCall = (CSTAClearCallConfEvent_t)cstaEvent.Event.cstaConfirmation.u.clearCall;

                                        Console.WriteLine("onTSAPIEvent.CSTA_CLEAR_CALL_CONF: clearCall={0};", clearCall);
                                    }
                                }

                                break;

                                #endregion CSTA_CLEAR_CALL_CONF

                            case Constants.CSTA_CONFERENCE_CALL_CONF:

                                #region CSTA_CONFERENCE_CALL_CONF

                                Console.WriteLine("onTSAPIEvent eventType=CSTA_CONFERENCE_CALL_CONF");
                                {
                                    if (cstaEvent.Event.cstaConfirmation.u.conferenceCall != null)
                                    {
                                        CSTAConferenceCallConfEvent_t conferenceCall = (CSTAConferenceCallConfEvent_t)cstaEvent.Event.cstaConfirmation.u.conferenceCall;

                                        int callID = conferenceCall.newCall.callID;
                                        string device = conferenceCall.newCall.deviceID.device;

                                        Console.WriteLine("onTSAPIEvent.CSTA_CONFERENCE_CALL_CONF: deviceID={0};callID={1};", device, callID);}
                                }

                                break;

                                #endregion CSTA_CONFERENCE_CALL_CONF

                            case Constants.CSTA_CONSULTATION_CALL_CONF:

                                #region CSTA_CONSULTATION_CALL_CONF

                                Console.WriteLine("onTSAPIEvent eventType=CSTA_CONSULTATION_CALL_CONF");
                                {
                                    if (cstaEvent.Event.cstaConfirmation.u.consultationCall != null)
                                    {
                                        CSTAConsultationCallConfEvent_t consultationCall = (CSTAConsultationCallConfEvent_t)cstaEvent.Event.cstaConfirmation.u.consultationCall;

                                        string device = consultationCall.newCall.deviceID.device;
                                        int callID = consultationCall.newCall.callID;

                                        Console.WriteLine("onTSAPIEvent.CSTA_CONSULTATION_CALL_CONF: deviceID={0};callID={1};", device, callID);
                                    }
                                }

                                break;

                                #endregion CSTA_CONSULTATION_CALL_CONF

                            case Constants.CSTA_DEFLECT_CALL_CONF:

                                #region CSTA_DEFLECT_CALL_CONF

                                {
                                    Console.WriteLine("onTSAPIEvent eventType=CSTA_DEFLECT_CALL_CONF");

                                    if (cstaEvent.Event.cstaConfirmation.u.deflectCall != null)
                                    {
                                        CSTADeflectCallConfEvent_t deflectCall = (CSTADeflectCallConfEvent_t)cstaEvent.Event.cstaConfirmation.u.deflectCall;

                                        Console.WriteLine("onTSAPIEvent.CSTA_DEFLECT_CALL_CONF: deflectCall={0};", deflectCall);
                                    }
                                }

                                break;

                                #endregion CSTA_DEFLECT_CALL_CONF

                            case Constants.CSTA_PICKUP_CALL_CONF:

                                #region CSTA_PICKUP_CALL_CONF

                                {
                                    if (cstaEvent.Event.cstaConfirmation.u.pickupCall != null)
                                    {
                                        CSTAPickupCallConfEvent_t pickupCall = (CSTAPickupCallConfEvent_t)cstaEvent.Event.cstaConfirmation.u.pickupCall;

                                        Console.WriteLine("onTSAPIEvent.CSTA_PICKUP_CALL_CONF: pickupCall={0};", pickupCall);
                                    }
                                }

                                break;

                                #endregion CSTA_PICKUP_CALL_CONF

                            case Constants.CSTA_GROUP_PICKUP_CALL_CONF:

                                #region CSTA_GROUP_PICKUP_CALL_CONF

                                {
                                    if (cstaEvent.Event.cstaConfirmation.u.groupPickupCall != null)
                                    {
                                        CSTAGroupPickupCallConfEvent_t groupPickupCall = (CSTAGroupPickupCallConfEvent_t)cstaEvent.Event.cstaConfirmation.u.groupPickupCall;

                                        Console.WriteLine("onTSAPIEvent.CSTA_GROUP_PICKUP_CALL_CONF: groupPickupCall={0};", groupPickupCall);
                                    }
                                }

                                break;

                                #endregion CSTA_GROUP_PICKUP_CALL_CONF

                            case Constants.CSTA_HOLD_CALL_CONF:

                                #region CSTA_HOLD_CALL_CONF

                                {
                                    if (cstaEvent.Event.cstaConfirmation.u.holdCall != null)
                                    {
                                        CSTAHoldCallConfEvent_t holdCall = (CSTAHoldCallConfEvent_t)cstaEvent.Event.cstaConfirmation.u.holdCall;

                                        Console.WriteLine("onTSAPIEvent.CSTA_HOLD_CALL_CONF: holdCall={0};", holdCall);
                                    }
                                }

                                break;

                                #endregion CSTA_HOLD_CALL_CONF

                            case Constants.CSTA_MAKE_CALL_CONF:

                                #region CSTA_MAKE_CALL_CONF

                                {
                                    Console.WriteLine("onTSAPIEvent eventType=CSTA_MAKE_CALL_CONF");

                                    if (cstaEvent.Event.cstaConfirmation.u.makeCall != null)
                                    {
                                        CSTAMakeCallConfEvent_t makeCall = (CSTAMakeCallConfEvent_t)cstaEvent.Event.cstaConfirmation.u.makeCall;

                                        string ucid = string.Empty;

                                        if (attEvent.u.makeCall != null)
                                        {
                                            ATTMakeCallConfEvent_t attMakeCall = (ATTMakeCallConfEvent_t)attEvent.u.makeCall;

                                            ucid = attMakeCall.ucid.value;
                                        }

                                        Console.WriteLine("onTSAPIEvent.CSTA_MAKE_CALL_CONF: deviceID={0};callID={1};ucid={2}", makeCall.newCall.deviceID.device, makeCall.newCall.callID, ucid);
                                    }
                                }

                                break;

                                #endregion CSTA_MAKE_CALL_CONF

                            case Constants.CSTA_MAKE_PREDICTIVE_CALL_CONF:

                                #region CSTA_MAKE_PREDICTIVE_CALL_CONF

                                {
                                    if (cstaEvent.Event.cstaConfirmation.u.makePredictiveCall != null)
                                    {
                                        CSTAMakePredictiveCallConfEvent_t makePredictiveCall = (CSTAMakePredictiveCallConfEvent_t)cstaEvent.Event.cstaConfirmation.u.makePredictiveCall;

                                        Console.WriteLine("onTSAPIEvent.CSTA_MAKE_PREDICTIVE_CALL_CONF: newCall.callID={0};newCall.deviceID={1};newCall.devIDType={2};", makePredictiveCall.newCall.callID, makePredictiveCall.newCall.deviceID.device, makePredictiveCall.newCall.devIDType);
                                    }
                                }

                                break;

                                #endregion CSTA_MAKE_PREDICTIVE_CALL_CONF

                            case Constants.CSTA_QUERY_MWI_CONF:

                                #region CSTA_QUERY_MWI_CONF

                                {
                                    if (cstaEvent.Event.cstaConfirmation.u.queryMwi != null)
                                    {
                                        CSTAQueryMwiConfEvent_t queryMwi = (CSTAQueryMwiConfEvent_t)cstaEvent.Event.cstaConfirmation.u.queryMwi;

                                        Console.WriteLine("onTSAPIEvent.CSTA_QUERY_MWI_CONF: messages={0};", queryMwi.messages);
                                    }
                                }

                                break;

                                #endregion CSTA_QUERY_MWI_CONF

                            case Constants.CSTA_QUERY_DND_CONF:

                                #region CSTA_QUERY_DND_CONF

                                Console.WriteLine("onTSAPIEvent eventType=CSTA_QUERY_DND_CONF");
                                {
                                    if (cstaEvent.Event.cstaConfirmation.u.queryDnd != null)
                                    {
                                        CSTAQueryDndConfEvent_t queryDnd = (CSTAQueryDndConfEvent_t)cstaEvent.Event.cstaConfirmation.u.queryDnd;

                                        Console.WriteLine("onTSAPIEvent.CSTA_QUERY_FWD_CONF: doNotDisturb={0};", queryDnd.doNotDisturb);
                                    }
                                }

                                break;

                                #endregion CSTA_QUERY_DND_CONF

                            case Constants.CSTA_QUERY_FWD_CONF:

                                #region CSTA_QUERY_FWD_CONF

                                {
                                    if (cstaEvent.Event.cstaConfirmation.u.queryFwd != null)
                                    {
                                        CSTAQueryFwdConfEvent_t queryFwd = (CSTAQueryFwdConfEvent_t)cstaEvent.Event.cstaConfirmation.u.queryFwd;

                                        Console.WriteLine("onTSAPIEvent.CSTA_QUERY_FWD_CONF: count={0};param={1};", queryFwd.forward.count, queryFwd.forward.param);
                                    }
                                }

                                break;

                                #endregion CSTA_QUERY_FWD_CONF

                            case Constants.CSTA_QUERY_AGENT_STATE_CONF:

                                #region CSTA_QUERY_AGENT_STATE_CONF

                                {
                                    if (cstaEvent.Event.cstaConfirmation.u.queryAgentState != null)
                                    {
                                        CSTAQueryAgentStateConfEvent_t queryAgentState = (CSTAQueryAgentStateConfEvent_t)cstaEvent.Event.cstaConfirmation.u.queryAgentState;

                                        Console.WriteLine("onTSAPIEvent.CSTA_QUERY_AGENT_STATE_CONF: agentState={0};", queryAgentState.agentState);
                                    }
                                }

                                break;

                                #endregion CSTA_QUERY_AGENT_STATE_CONF

                            case Constants.CSTA_QUERY_LAST_NUMBER_CONF:

                                #region CSTA_QUERY_LAST_NUMBER_CONF

                                {
                                    if (cstaEvent.Event.cstaConfirmation.u.queryLastNumber != null)
                                    {
                                        CSTAQueryLastNumberConfEvent_t queryLastNumber = (CSTAQueryLastNumberConfEvent_t)cstaEvent.Event.cstaConfirmation.u.queryLastNumber;

                                        Console.WriteLine("onTSAPIEvent.CSTA_QUERY_LAST_NUMBER_CONF: lastNumber={0};", queryLastNumber.lastNumber);
                                    }
                                }

                                break;

                                #endregion CSTA_QUERY_LAST_NUMBER_CONF

                            case Constants.CSTA_QUERY_DEVICE_INFO_CONF:

                                #region CSTA_QUERY_DEVICE_INFO_CONF

                                {
                                    if (cstaEvent.Event.cstaConfirmation.u.queryDeviceInfo != null)
                                    {
                                        CSTAQueryDeviceInfoConfEvent_t queryDeviceInfo = (CSTAQueryDeviceInfoConfEvent_t)cstaEvent.Event.cstaConfirmation.u.queryDeviceInfo;

                                        Console.WriteLine("onTSAPIEvent.CSTA_QUERY_DEVICE_INFO_CONF: device={0};", queryDeviceInfo.device);
                                    }
                                }

                                break;

                                #endregion CSTA_QUERY_DEVICE_INFO_CONF

                            case Constants.CSTA_RECONNECT_CALL_CONF:

                                #region CSTA_RECONNECT_CALL_CONF

                                {
                                    if (cstaEvent.Event.cstaConfirmation.u.reconnectCall != null)
                                    {
                                        CSTAReconnectCallConfEvent_t reconnectCall = (CSTAReconnectCallConfEvent_t)cstaEvent.Event.cstaConfirmation.u.reconnectCall;

                                        Console.WriteLine("onTSAPIEvent.CSTA_RECONNECT_CALL_CONF: reconnectCall={0};", reconnectCall);
                                    }
                                }

                                break;

                                #endregion CSTA_RECONNECT_CALL_CONF

                            case Constants.CSTA_RETRIEVE_CALL_CONF:

                                #region CSTA_RETRIEVE_CALL_CONF

                                {
                                    if (cstaEvent.Event.cstaConfirmation.u.retrieveCall != null)
                                    {
                                        CSTARetrieveCallConfEvent_t retrieveCall = (CSTARetrieveCallConfEvent_t)cstaEvent.Event.cstaConfirmation.u.retrieveCall;

                                        Console.WriteLine("onTSAPIEvent.CSTA_RECONNECT_CALL_CONF: retrieveCall={0};", retrieveCall);
                                    }
                                }

                                break;

                                #endregion CSTA_RETRIEVE_CALL_CONF

                            case Constants.CSTA_SET_MWI_CONF:

                                #region CSTA_SET_MWI_CONF

                                {
                                    if (cstaEvent.Event.cstaConfirmation.u.setMwi != null)
                                    {
                                        CSTASetMwiConfEvent_t setMwi = (CSTASetMwiConfEvent_t)cstaEvent.Event.cstaConfirmation.u.setMwi;

                                        Console.WriteLine("onTSAPIEvent.CSTA_SET_MWI_CONF: setMwi={0};", setMwi);
                                    }
                                }

                                break;

                                #endregion CSTA_SET_MWI_CONF

                            case Constants.CSTA_SET_DND_CONF:

                                #region CSTA_SET_DND_CONF

                                {
                                    if (cstaEvent.Event.cstaConfirmation.u.setDnd != null)
                                    {
                                        CSTASetDndConfEvent_t setDnd = (CSTASetDndConfEvent_t)cstaEvent.Event.cstaConfirmation.u.setDnd;

                                        Console.WriteLine("onTSAPIEvent.CSTA_SET_DND_CONF: setDnd={0};", setDnd);
                                    }
                                }

                                break;

                                #endregion CSTA_SET_DND_CONF

                            case Constants.CSTA_SET_FWD_CONF:

                                #region CSTA_SET_FWD_CONF

                                {
                                    if (cstaEvent.Event.cstaConfirmation.u.setFwd != null)
                                    {
                                        CSTASetFwdConfEvent_t setFwd = (CSTASetFwdConfEvent_t)cstaEvent.Event.cstaConfirmation.u.setFwd;

                                        Console.WriteLine("onTSAPIEvent.CSTA_SET_FWD_CONF: setFwd={0};", setFwd);
                                    }
                                }

                                break;

                                #endregion CSTA_SET_FWD_CONF

                            case Constants.CSTA_SET_AGENT_STATE_CONF:

                                #region CSTA_SET_AGENT_STATE_CONF

                                {
                                    if (cstaEvent.Event.cstaConfirmation.u.setAgentState != null)
                                    {
                                        CSTASetAgentStateConfEvent_t setAgentState = (CSTASetAgentStateConfEvent_t)cstaEvent.Event.cstaConfirmation.u.setAgentState;

                                        Console.WriteLine("onTSAPIEvent.CSTA_SET_AGENT_STATE_CONF: setAgentState={0};", setAgentState);
                                    }
                                }

                                break;

                                #endregion CSTA_SET_AGENT_STATE_CONF

                            case Constants.CSTA_TRANSFER_CALL_CONF:

                                #region CSTA_TRANSFER_CALL_CONF

                                Console.WriteLine("onTSAPIEvent eventType=CSTA_TRANSFER_CALL_CONF");
                                {
                                    if (cstaEvent.Event.cstaConfirmation.u.transferCall != null)
                                    {
                                        CSTATransferCallConfEvent_t transferCall = (CSTATransferCallConfEvent_t)cstaEvent.Event.cstaConfirmation.u.transferCall;

                                        string device = transferCall.newCall.deviceID.device;
                                        int callID = transferCall.newCall.callID;

                                        Console.WriteLine("onTSAPIEvent.CSTA_TRANSFER_CALL_CONF: deviceID={0};callID={1};", device, callID);
                                    }
                                }

                                break;

                                #endregion CSTA_TRANSFER_CALL_CONF

                            case Constants.CSTA_UNIVERSAL_FAILURE_CONF:

                                #region CSTA_UNIVERSAL_FAILURE_CONF

                                {
                                    if (cstaEvent.Event.cstaConfirmation.u.universalFailure != null)
                                    {
                                        CSTAUniversalFailureConfEvent_t universalFailure = (CSTAUniversalFailureConfEvent_t)cstaEvent.Event.cstaConfirmation.u.universalFailure;

                                        Console.WriteLine("onTSAPIEvent.CSTA_UNIVERSAL_FAILURE_CONF: error={0};", universalFailure.error);
                                    }
                                }

                                break;

                                #endregion CSTA_UNIVERSAL_FAILURE_CONF

                            case Constants.CSTA_MONITOR_CONF:

                                #region CSTA_MONITOR_CONF

                                {
                                    if (cstaEvent.Event.cstaConfirmation.u.monitorStart != null)
                                    {
                                        CSTAMonitorConfEvent_t monitorStart = (CSTAMonitorConfEvent_t)cstaEvent.Event.cstaConfirmation.u.monitorStart;

                                        Console.WriteLine("onTSAPIEvent.CSTA_MONITOR_CONF: monitorCrossRefID={0};", monitorStart.monitorCrossRefID);
                                    }
                                }

                                break;

                                #endregion CSTA_MONITOR_CONF

                            case Constants.CSTA_CHANGE_MONITOR_FILTER_CONF:

                                #region CSTA_CHANGE_MONITOR_FILTER_CONF

                                {
                                    if (cstaEvent.Event.cstaConfirmation.u.changeMonitorFilter != null)
                                    {
                                        CSTAChangeMonitorFilterConfEvent_t changeMonitorFilter = (CSTAChangeMonitorFilterConfEvent_t)cstaEvent.Event.cstaConfirmation.u.changeMonitorFilter;

                                        Console.WriteLine("onTSAPIEvent.CSTA_CHANGE_MONITOR_FILTER_CONF: monitorFilter={0};", changeMonitorFilter.monitorFilter);
                                    }
                                }

                                break;

                                #endregion CSTA_CHANGE_MONITOR_FILTER_CONF

                            case Constants.CSTA_MONITOR_STOP_CONF:

                                #region CSTA_MONITOR_STOP_CONF

                                {
                                    if (cstaEvent.Event.cstaConfirmation.u.monitorStop != null)
                                    {
                                        CSTAMonitorStopConfEvent_t monitorStop = (CSTAMonitorStopConfEvent_t)cstaEvent.Event.cstaConfirmation.u.monitorStop;

                                        Console.WriteLine("onTSAPIEvent.CSTA_MONITOR_STOP_CONF: monitorStop={0};", monitorStop);
                                    }
                                }

                                break;

                                #endregion CSTA_MONITOR_STOP_CONF

                            case Constants.CSTA_SNAPSHOT_DEVICE_CONF:

                                #region CSTA_SNAPSHOT_DEVICE_CONF

                                {
                                    if (cstaEvent.Event.cstaConfirmation.u.snapshotDevice != null)
                                    {
                                        CSTASnapshotDeviceConfEvent_t snapshotDevice = (CSTASnapshotDeviceConfEvent_t)cstaEvent.Event.cstaConfirmation.u.snapshotDevice;

                                        Console.WriteLine("onTSAPIEvent.CSTA_SNAPSHOT_DEVICE_CONF: snapshotData={0};", snapshotDevice.snapshotData);
                                    }

                                }

                                break;

                                #endregion CSTA_SNAPSHOT_DEVICE_CONF

                            case Constants.CSTA_SNAPSHOT_CALL_CONF:

                                #region CSTA_SNAPSHOT_CALL_CONF

                                {
                                    if (cstaEvent.Event.cstaConfirmation.u.snapshotCall != null)
                                    {
                                        CSTASnapshotCallConfEvent_t snapshotCall = (CSTASnapshotCallConfEvent_t)cstaEvent.Event.cstaConfirmation.u.snapshotCall;

                                        Console.WriteLine("onTSAPIEvent.CSTA_SNAPSHOT_CALL_CONF: snapshotCall={0};", snapshotCall.snapshotData);
                                    }
                                }

                                break;

                                #endregion CSTA_SNAPSHOT_CALL_CONF

                            case Constants.CSTA_ROUTE_REGISTER_REQ_CONF:

                                #region CSTA_ROUTE_REGISTER_REQ_CONF

                                {
                                    if (cstaEvent.Event.cstaConfirmation.u.routeRegister != null)
                                    {
                                        CSTARouteRegisterReqConfEvent_t routeRegister = (CSTARouteRegisterReqConfEvent_t)cstaEvent.Event.cstaConfirmation.u.routeRegister;

                                        Console.WriteLine("onTSAPIEvent.CSTA_ROUTE_REGISTER_REQ_CONF: registerReqID={0};", routeRegister.registerReqID);
                                    }
                                }

                                break;

                                #endregion CSTA_ROUTE_REGISTER_REQ_CONF

                            case Constants.CSTA_ROUTE_REGISTER_CANCEL_CONF:

                                #region CSTA_ROUTE_REGISTER_CANCEL_CONF

                                {
                                    if (cstaEvent.Event.cstaConfirmation.u.routeCancel != null)
                                    {
                                        CSTARouteRegisterCancelConfEvent_t routeCancel = (CSTARouteRegisterCancelConfEvent_t)cstaEvent.Event.cstaConfirmation.u.routeCancel;

                                        Console.WriteLine("onTSAPIEvent.CSTA_ROUTE_REGISTER_CANCEL_CONF: routeRegisterReqID={0};", routeCancel.routeRegisterReqID);
                                    }
                                }

                                break;

                                #endregion CSTA_ROUTE_REGISTER_CANCEL_CONF

                            case Constants.CSTA_ESCAPE_SVC_CONF:

                                #region CSTA_ESCAPE_SVC_CONF

                                {
                                    Console.WriteLine("onTSAPIEvent eventType=CSTA_ESCAPE_SVC_CONF");

                                    if (cstaEvent.Event.cstaConfirmation.u.escapeService != null)
                                    {
                                        if (tsapiEvent.attEvent != null)
                                        {
                                            if (tsapiEvent.attEvent.eventType == TSAPIClient.ATT.Constants.ATT_QUERY_ACD_SPLIT_CONF)
                                            {
                                                Console.WriteLine("onTSAPIEvent attEventType=ATT_QUERY_ACD_SPLIT_CONF");

                                                if (tsapiEvent.attEvent.u.queryAcdSplit != null)
                                                {
                                                    ATTQueryAcdSplitConfEvent_t queryAcdSplit = (ATTQueryAcdSplitConfEvent_t)tsapiEvent.attEvent.u.queryAcdSplit;

                                                    Console.WriteLine("onTSAPIEvent.ATT_QUERY_ACD_SPLIT_CONF: agentsLoggedIn={0};", queryAcdSplit.agentsLoggedIn);
                                                }
                                            }
                                            else if (tsapiEvent.attEvent.eventType == TSAPIClient.ATT.Constants.ATT_QUERY_UCID_CONF)
                                            {
                                                Console.WriteLine("onTSAPIEvent attEventType=ATT_QUERY_UCID_CONF");

                                                if (tsapiEvent.attEvent.u.queryUCID != null)
                                                {
                                                    ATTQueryUcidConfEvent_t queryUCID = (ATTQueryUcidConfEvent_t)tsapiEvent.attEvent.u.queryUCID;

                                                    Console.WriteLine("onTSAPIEvent.ATT_QUERY_UCID_CONF: ucid={0};", queryUCID.ucid.value);
                                                }
                                            }
                                            else if (tsapiEvent.attEvent.eventType == TSAPIClient.ATT.Constants.ATT_SINGLE_STEP_TRANSFER_CALL_CONF)
                                            {
                                                Console.WriteLine("onTSAPIEvent attEventType=ATT_SINGLE_STEP_TRANSFER_CALL_CONF");

                                                if (tsapiEvent.attEvent.u.ssTransferCallConf != null)
                                                {
                                                    ATTSingleStepTransferCallConfEvent_t ssTransferCallConf = (ATTSingleStepTransferCallConfEvent_t)tsapiEvent.attEvent.u.ssTransferCallConf;

                                                    Console.WriteLine("onTSAPIEvent.ATT_SINGLE_STEP_TRANSFER_CALL_CONF: ucid={0};transferredCall.callID={1};transferredCall.deviceID={2};", ssTransferCallConf.ucid.value, ssTransferCallConf.transferredCall.callID, ssTransferCallConf.transferredCall.deviceID.device);
                                                }
                                            }
                                        }
                                    }
                                }

                                break;

                                #endregion CSTA_ESCAPE_SVC_CONF

                            case Constants.CSTA_SYS_STAT_REQ_CONF:

                                #region CSTA_SYS_STAT_REQ_CONF

                                {
                                    if (cstaEvent.Event.cstaConfirmation.u.sysStatReq != null)
                                    {
                                        CSTASysStatReqConfEvent_t sysStatReq = (CSTASysStatReqConfEvent_t)cstaEvent.Event.cstaConfirmation.u.sysStatReq;

                                        Console.WriteLine("onTSAPIEvent.CSTA_SYS_STAT_REQ_CONF: systemStatus={0};", sysStatReq.systemStatus);
                                    }
                                }

                                break;

                                #endregion CSTA_SYS_STAT_REQ_CONF

                            case Constants.CSTA_SYS_STAT_START_CONF:

                                #region CSTA_SYS_STAT_START_CONF

                                {
                                    if (cstaEvent.Event.cstaConfirmation.u.sysStatStart != null)
                                    {
                                        CSTASysStatStartConfEvent_t sysStatStart = (CSTASysStatStartConfEvent_t)cstaEvent.Event.cstaConfirmation.u.sysStatStart;

                                        Console.WriteLine("onTSAPIEvent.CSTA_SYS_STAT_START_CONF: statusFilter={0};", sysStatStart.statusFilter);
                                    }
                                }

                                break;

                                #endregion CSTA_SYS_STAT_START_CONF

                            case Constants.CSTA_SYS_STAT_STOP_CONF:

                                #region CSTA_SYS_STAT_STOP_CONF

                                {
                                    if (cstaEvent.Event.cstaConfirmation.u.sysStatStop != null)
                                    {
                                        CSTASysStatStopConfEvent_t sysStatStop = (CSTASysStatStopConfEvent_t)cstaEvent.Event.cstaConfirmation.u.sysStatStop;

                                        Console.WriteLine("onTSAPIEvent.CSTA_SYS_STAT_STOP_CONF: sysStatStop={0};", sysStatStop);
                                    }
                                }

                                break;

                                #endregion CSTA_SYS_STAT_STOP_CONF

                            case Constants.CSTA_CHANGE_SYS_STAT_FILTER_CONF:

                                #region CSTA_CHANGE_SYS_STAT_FILTER_CONF

                                {
                                    if (cstaEvent.Event.cstaConfirmation.u.changeSysStatFilter != null)
                                    {
                                        CSTAChangeSysStatFilterConfEvent_t changeSysStatFilter = (CSTAChangeSysStatFilterConfEvent_t)cstaEvent.Event.cstaConfirmation.u.changeSysStatFilter;

                                        Console.WriteLine("onTSAPIEvent.CSTA_CHANGE_SYS_STAT_FILTER_CONF: statusFilterActive={0};", changeSysStatFilter.statusFilterActive);
                                    }
                                }

                                break;

                                #endregion CSTA_CHANGE_SYS_STAT_FILTER_CONF

                            case Constants.CSTA_GETAPI_CAPS_CONF:

                                #region CSTA_GETAPI_CAPS_CONF

                                {
                                    if (cstaEvent.Event.cstaConfirmation.u.getAPICaps != null)
                                    {
                                        CSTAGetAPICapsConfEvent_t getAPICaps = (CSTAGetAPICapsConfEvent_t)cstaEvent.Event.cstaConfirmation.u.getAPICaps;

                                        Console.WriteLine("onTSAPIEvent.CSTA_GETAPI_CAPS_CONF: queryDeviceInfo={0};", getAPICaps.queryDeviceInfo);
                                    }
                                }

                                break;

                                #endregion CSTA_GETAPI_CAPS_CONF

                            case Constants.CSTA_GET_DEVICE_LIST_CONF:

                                #region CSTA_GET_DEVICE_LIST_CONF

                                {
                                    if (cstaEvent.Event.cstaConfirmation.u.getDeviceList != null)
                                    {
                                        CSTAGetDeviceListConfEvent_t getDeviceList = (CSTAGetDeviceListConfEvent_t)cstaEvent.Event.cstaConfirmation.u.getDeviceList;

                                        Console.WriteLine("onTSAPIEvent NumberOfDevices=" + getDeviceList.devList.count.ToString() + ";Index=" + getDeviceList.index.ToString());

                                    }
                                }

                                break;

                                #endregion CSTA_GET_DEVICE_LIST_CONF

                            case Constants.CSTA_QUERY_CALL_MONITOR_CONF:

                                #region CSTA_QUERY_CALL_MONITOR_CONF

                                {
                                    if (cstaEvent.Event.cstaConfirmation.u.queryCallMonitor != null)
                                    {
                                        CSTAQueryCallMonitorConfEvent_t queryCallMonitor = (CSTAQueryCallMonitorConfEvent_t)cstaEvent.Event.cstaConfirmation.u.queryCallMonitor;
                                    }
                                }

                                break;

                                #endregion CSTA_QUERY_CALL_MONITOR_CONF

                            default:

                                break;
                        }

                        #endregion CSTACONFIRMATION

                        break;

                    case Constants.CSTAUNSOLICITED:

                        #region CSTAUNSOLICITED

                        Console.WriteLine("onTSAPIEvent eventClass=CSTAUNSOLICITED");

                        int xref = cstaEvent.Event.cstaUnsolicited.monitorCrossRefId;

                        Console.WriteLine("onTSAPIEvent monitorCrossRefId={0}", xref);

                        switch (cstaEvent.eventHeader.eventType)
                        {
                            case Constants.CSTA_CALL_CLEARED:

                                #region CSTA_CALL_CLEARED

                                {
                                    Console.WriteLine("onTSAPIEvent.CSTA_CALL_CLEARED: eventType=CSTA_CALL_CLEARED");

                                    if (cstaEvent.Event.cstaUnsolicited.u.callCleared != null)
                                    {
                                        CSTACallClearedEvent_t callCleared = (CSTACallClearedEvent_t)cstaEvent.Event.cstaUnsolicited.u.callCleared;

                                        string reason = string.Empty;

                                        if (attEvent != null)
                                        {
                                            if (attEvent.eventType == TSAPIClient.ATT.Constants.ATT_CALL_CLEARED)
                                            {
                                                if (attEvent.u.callClearedEvent != null)
                                                {
                                                    ATTCallClearedEvent_t callClearedEvent = (ATTCallClearedEvent_t)attEvent.u.callClearedEvent;

                                                    reason = callClearedEvent.reason.ToString();
                                                }
                                            }
                                        }

                                        StringBuilder bldr = new StringBuilder();

                                        bldr.Append("onTSAPIEvent.CSTA_CALL_CLEARED: ");
                                        bldr.AppendFormat("clearedCall.callID={0};", callCleared.clearedCall.callID);
                                        bldr.AppendFormat("clearedCall.deviceID={0};", callCleared.clearedCall.deviceID.device);
                                        bldr.AppendFormat("clearedCall.devIDType={0};", callCleared.clearedCall.devIDType);
                                        bldr.AppendFormat("localConnectionInfo={0};", callCleared.localConnectionInfo);
                                        bldr.AppendFormat("cause={0};", callCleared.cause);
                                        bldr.AppendFormat("reason={0};", reason);

                                        Console.WriteLine(bldr.ToString());
                                    }
                                }

                                break;

                                #endregion CSTA_CALL_CLEARED

                            case Constants.CSTA_CONFERENCED:

                                #region CSTA_CONFERENCED

                                {
                                    Console.WriteLine("onTSAPIEvent eventType=CSTA_CONFERENCED");

                                    if (cstaEvent.Event.cstaUnsolicited.u.conferenced != null)
                                    {
                                        CSTAConferencedEvent_t conferenced = (CSTAConferencedEvent_t)cstaEvent.Event.cstaUnsolicited.u.conferenced;

                                        /*
										secondaryOldCall: this is a callID of the call that was conferenced.
                                        This is usually the ACTIVE call before the conference. This call was
                                        retained by the switch after the conference
										*/
                                        int secondaryCallID = conferenced.secondaryOldCall.callID;

                                        /*
										primaryOldCall: this is a callID of the call that was conferenced.
                                        This is usually the HELD call before the conference. This call
                                        is by the switch after the conference
										*/
                                        int primaryCallID = conferenced.primaryOldCall.callID;
                                        string device = conferenced.confController.deviceID.device;
                                        
                                    }
                                }

                                break;

                                #endregion CSTA_CONFERENCED

                            case Constants.CSTA_CONNECTION_CLEARED:

                                #region CSTA_CONNECTION_CLEARED

                                {
                                    Console.WriteLine("onTSAPIEvent eventType=CSTA_CONNECTION_CLEARED");

                                    if (cstaEvent.Event.cstaUnsolicited.u.connectionCleared != null)
                                    {
                                        CSTAConnectionClearedEvent_t connectionCleared = (CSTAConnectionClearedEvent_t)cstaEvent.Event.cstaUnsolicited.u.connectionCleared;

                                        int callID = connectionCleared.droppedConnection.callID;
                                        string clearedExt = connectionCleared.droppedConnection.deviceID.device;
                                        string termExt = connectionCleared.releasingDevice.deviceID.device;
                                    }
                                }

                                break;

                                #endregion CSTA_CONNECTION_CLEARED

                            case Constants.CSTA_DELIVERED:

                                #region CSTA_DELIVERED

                                {
                                    Console.WriteLine("onTSAPIEvent eventType=CSTA_DELIVERED");

                                    if (cstaEvent.Event.cstaUnsolicited.u.delivered != null)
                                    {
                                        CSTADeliveredEvent_t delivered = (CSTADeliveredEvent_t)cstaEvent.Event.cstaUnsolicited.u.delivered;

                                        Console.WriteLine("onTSAPIEvent.CSTA_DELIVERED: alertingDevice={0};alertingDeviceType={1};calledDevice={2};calledDeviceType={3};callingDevice={4};callingDeviceType={5};cause={6}", delivered.alertingDevice.deviceID.device, delivered.alertingDevice.deviceIDType, delivered.calledDevice.deviceID.device, delivered.calledDevice.deviceIDType, delivered.callingDevice.deviceID.device, delivered.callingDevice.deviceIDType, delivered.cause);

                                        if (cstaEvent.Event.cstaUnsolicited.monitorCrossRefId == m_MonitorXRef)
                                        {
                                            m_CallID = delivered.connection.callID;
                                        }
                                    }
                                }

                                break;

                                #endregion CSTA_DELIVERED

                            case Constants.CSTA_DIVERTED:

                                #region CSTA_DIVERTED

                                {
                                    Console.WriteLine("onTSAPIEvent eventType=CSTA_DIVERTED");

                                    if (cstaEvent.Event.cstaUnsolicited.u.diverted != null)
                                    {
                                        CSTADivertedEvent_t diverted = (CSTADivertedEvent_t)cstaEvent.Event.cstaUnsolicited.u.diverted;
                                        int callID = diverted.connection.callID;
                                        string device = diverted.connection.deviceID.device;

                                        Console.WriteLine("callID={0};device={1};", callID, device);
                                    }
                                }

                                break;

                                #endregion CSTA_DIVERTED

                            case Constants.CSTA_ESTABLISHED:

                                #region CSTA_ESTABLISHED

                                {
                                    Console.WriteLine("onTSAPIEvent eventType=CSTA_ESTABLISHED");

                                    if (cstaEvent.Event.cstaUnsolicited.u.established != null)
                                    {
                                        CSTAEstablishedEvent_t established = (CSTAEstablishedEvent_t)cstaEvent.Event.cstaUnsolicited.u.established;

                                        Console.WriteLine("onTSAPIEvent.CSTA_ESTABLISHED: answeringDevice={0};answeringDeviceType={1};calledDevice={2};calledDeviceType={3};callingDevice={4};callingDeviceType={5};cause={6}", established.answeringDevice.deviceID.device, established.answeringDevice.deviceIDType, established.calledDevice.deviceID.device, established.calledDevice.deviceIDType, established.callingDevice.deviceID.device, established.callingDevice.deviceIDType, established.cause);
                                    }
                                }

                                break;

                                #endregion CSTA_ESTABLISHED

                            case Constants.CSTA_FAILED:

                                #region CSTA_FAILED

                                {
                                    Console.WriteLine("onTSAPIEvent eventType=CSTA_FAILED");

                                    if (cstaEvent.Event.cstaUnsolicited.u.failed != null)
                                    {
                                        CSTAFailedEvent_t failed = (CSTAFailedEvent_t)cstaEvent.Event.cstaUnsolicited.u.failed;

                                        int callID = failed.failedConnection.callID;
                                        string device = failed.failedConnection.deviceID.device;

                                        Console.WriteLine("onTSAPIEvent.CSTA_FAILED: callID={0};deviceID={1};", callID, device);
                                    }
                                }

                                break;

                                #endregion CSTA_FAILED

                            case Constants.CSTA_HELD:

                                #region CSTA_HELD

                                {
                                    Console.WriteLine("onTSAPIEvent eventType=CSTA_HELD");

                                    if (cstaEvent.Event.cstaUnsolicited.u.held != null)
                                    {
                                        CSTAHeldEvent_t held = (CSTAHeldEvent_t)cstaEvent.Event.cstaUnsolicited.u.held;

                                        int callID = held.heldConnection.callID;
                                        string device = held.heldConnection.deviceID.device;

                                        Console.WriteLine("onTSAPIEvent.CSTA_HELD: callID={0};device={1}", callID, device);
                                    }
                                }

                                break;

                                #endregion CSTA_HELD

                            case Constants.CSTA_NETWORK_REACHED:

                                #region CSTA_NETWORK_REACHED

                                {
                                Console.WriteLine("onTSAPIEvent eventType=CSTA_NETWORK_REACHED");

                                    if (cstaEvent.Event.cstaUnsolicited.u.networkReached != null)
                                    {
                                        CSTANetworkReachedEvent_t networkReached = (CSTANetworkReachedEvent_t)cstaEvent.Event.cstaUnsolicited.u.networkReached;

                                        int callID = networkReached.connection.callID;
                                        string device = networkReached.connection.deviceID.device;

                                        Console.WriteLine("onTSAPIEvent callID={0};deviceID={1};", callID, device);
                                    }
                                
                                }

                                break;

                                #endregion CSTA_NETWORK_REACHED

                            case Constants.CSTA_ORIGINATED:

                                #region CSTA_ORIGINATED

                                {
                                    Console.WriteLine("onTSAPIEvent eventType=CSTA_ORIGINATED");

                                    if (cstaEvent.Event.cstaUnsolicited.u.originated != null)
                                    {
                                        CSTAOriginatedEvent_t originated = (CSTAOriginatedEvent_t)cstaEvent.Event.cstaUnsolicited.u.originated;

                                        int callID = originated.originatedConnection.callID;
                                        string device = originated.originatedConnection.deviceID.device;

                                        Console.WriteLine("onTSAPIEvent.CSTA_ORIGINATED: callID={0};deviceID={1};", callID, device);

                                        if (attEvent != null)
                                        {
                                            if (attEvent.eventType == TSAPIClient.ATT.Constants.ATT_ORIGINATED)
                                            {
                                                if (attEvent.u.originatedEvent != null)
                                                {
                                                    ATTOriginatedEvent_t originatedEvent = (ATTOriginatedEvent_t)attEvent.u.originatedEvent;

                                                    Console.WriteLine("onTSAPIEvent.ATT_ORIGINATED: consultMode={0};", originatedEvent.consultMode);
                                                }
                                            }
                                        }
                                    }
                                }

                                break;

                                #endregion CSTA_ORIGINATED

                            case Constants.CSTA_QUEUED:

                                #region CSTA_QUEUED

                                {
                                    Console.WriteLine("onTSAPIEvent eventType=CSTA_QUEUED");

                                    if (cstaEvent.Event.cstaUnsolicited.u.queued != null)
                                    {
                                        CSTAQueuedEvent_t queued = (CSTAQueuedEvent_t)cstaEvent.Event.cstaUnsolicited.u.queued;

                                        int callID = queued.queuedConnection.callID;
                                        string device = queued.queuedConnection.deviceID.device;

                                        Console.WriteLine("onTSAPIEvent.CSTA_QUEUED: callID={0};deviceID={1};", callID, device);
                                    }
                                }

                                break;

                                #endregion CSTA_QUEUED

                            case Constants.CSTA_RETRIEVED:

                                #region CSTA_RETRIEVED

                                {
                                    Console.WriteLine("onTSAPIEvent eventType=CSTA_RETRIEVED");

                                    if (cstaEvent.Event.cstaUnsolicited.u.retrieved != null)
                                    {
                                        CSTARetrievedEvent_t retrieved = (CSTARetrievedEvent_t)cstaEvent.Event.cstaUnsolicited.u.retrieved;

                                        int callID = retrieved.retrievedConnection.callID;
                                        string device = retrieved.retrievingDevice.deviceID.device; //this is also the Retreiving Extension(RETRVEXT) and Original Called Device(ORIGCALLEDDEV)                                        

                                        Console.WriteLine("onTSAPIEvent callID={0};deviceID={1};", callID, device);
                                    }
                                }

                                break;

                                #endregion CSTA_RETRIEVED

                            case Constants.CSTA_SERVICE_INITIATED:

                                #region CSTA_SERVICE_INITIATED

                                {
                                    Console.WriteLine("onTSAPIEvent eventType=CSTA_SERVICE_INITIATED");

                                    if (cstaEvent.Event.cstaUnsolicited.u.serviceInitiated != null)
                                    {
                                        CSTAServiceInitiatedEvent_t serviceInitiated = (CSTAServiceInitiatedEvent_t)cstaEvent.Event.cstaUnsolicited.u.serviceInitiated;

                                        int callID = serviceInitiated.initiatedConnection.callID;
                                        string device = serviceInitiated.initiatedConnection.deviceID.device;
                                        string ucid = string.Empty;

                                        if (attEvent != null)
                                        {
                                            if (attEvent.eventType == TSAPIClient.ATT.Constants.ATT_SERVICE_INITIATED)
                                            {
                                                if (attEvent.u.serviceInitiated != null)
                                                {
                                                    ATTServiceInitiatedEvent_t attServiceInitiated = (ATTServiceInitiatedEvent_t)attEvent.u.serviceInitiated;

                                                    ucid = attServiceInitiated.ucid.value ?? string.Empty;
                                                }
                                            }
                                        }

                                        Console.WriteLine("onTSAPIEvent.CSTA_SERVICE_INITIATED: callID={0};deviceID={1};ucid={2};", callID, device, ucid);
                                    }
                                }

                                break;

                                #endregion CSTA_SERVICE_INITIATED

                            case Constants.CSTA_TRANSFERRED:

                                #region CSTA_TRANSFERRED

                                {
                                Console.WriteLine("onTSAPIEvent eventType=CSTA_TRANSFERRED");

                                    if (cstaEvent.Event.cstaUnsolicited.u.transferred != null)
                                    {
                                        CSTATransferredEvent_t transferred = (CSTATransferredEvent_t)cstaEvent.Event.cstaUnsolicited.u.transferred;

                                        Console.WriteLine("onTSAPIEvent.CSTA_TRANSFERRED: callID={0};", transferred.primaryOldCall.callID);

                                        if (attEvent != null)
                                        {
                                            if (attEvent.eventType == TSAPIClient.ATT.Constants.ATT_TRANSFERRED)
                                            {
                                                if (attEvent.u.transferredEvent != null)
                                                {
                                                    ATTTransferredEvent_t transferredEvent = (ATTTransferredEvent_t)attEvent.u.transferredEvent;

                                                    Console.WriteLine("onTSAPIEvent.ATT_TRANSFERRED: transferredEvent.ucid={0};transferredEvent.originalCallInfo.calledDevice={1};transferredEvent.originalCallInfo.callingDevice={2};transferredEvent.originalCallInfo.ucid={3};transferredEvent.distributingDevice={4};transferredEvent.distributingVDN.value={5};", transferredEvent.ucid.value, transferredEvent.originalCallInfo.calledDevice.value, transferredEvent.originalCallInfo.callingDevice.value, transferredEvent.originalCallInfo.ucid, transferredEvent.distributingDevice.value, transferredEvent.distributingVDN.value);
                                                }
                                            }
                                        }
                                    }
                                }

                                break;

                                #endregion CSTA_TRANSFERRED

                            case Constants.CSTA_CALL_INFORMATION:

                                #region CSTA_CALL_INFORMATION

                                {
                                    Console.WriteLine("onTSAPIEvent eventType=CSTA_CALL_INFORMATION");

                                    if (cstaEvent.Event.cstaUnsolicited.u.callInformation != null)
                                    {
                                        CSTACallInformationEvent_t callInformation = (CSTACallInformationEvent_t)cstaEvent.Event.cstaUnsolicited.u.callInformation;

                                        int callID = callInformation.connection.callID;
                                        string device = callInformation.connection.deviceID.device;

                                        Console.WriteLine("onTSAPIEvent.CSTA_CALL_INFORMATION: callID={0};deviceID={1};", callID, device);
                                    }
                                }

                                break;

                                #endregion CSTA_CALL_INFORMATION

                            case Constants.CSTA_DO_NOT_DISTURB:

                                #region CSTA_DO_NOT_DISTURB

                                {
                                Console.WriteLine("onTSAPIEvent eventType=CSTA_DO_NOT_DISTURB");

                                    if (cstaEvent.Event.cstaUnsolicited.u.doNotDisturb != null)
                                    {
                                        CSTADoNotDisturbEvent_t doNotDisturb = (CSTADoNotDisturbEvent_t)cstaEvent.Event.cstaUnsolicited.u.doNotDisturb;
                                        string device = doNotDisturb.device.deviceID.device;
                                        bool doNotDisturbOn = doNotDisturb.doNotDisturbOn;

                                        Console.WriteLine("onTSAPIEvent.CSTA_DO_NOT_DISTURB: deviceID={0};doNotDisturbOn={1};", device, doNotDisturbOn);
                                    }
                                }

                                break;

                                #endregion CSTA_DO_NOT_DISTURB

                            case Constants.CSTA_FORWARDING:

                                #region CSTA_FORWARDING

                                {
                                    Console.WriteLine("onTSAPIEvent eventType=CSTA_FORWARDING");

                                    if (cstaEvent.Event.cstaUnsolicited.u.forwarding != null)
                                    {
                                        CSTAForwardingEvent_t forwarding = (CSTAForwardingEvent_t)cstaEvent.Event.cstaUnsolicited.u.forwarding;

                                        Console.WriteLine("onTSAPIEvent.CSTA_FORWARDING: deviceID={0};forwardDN={1};", forwarding.device.deviceID, forwarding.forwardingInformation.forwardDN);
                                    }
                                }

                                break;

                                #endregion CSTA_FORWARDING

                            case Constants.CSTA_MESSAGE_WAITING:

                                #region CSTA_MESSAGE_WAITING

                                {
                                    if (cstaEvent.Event.cstaUnsolicited.u.messageWaiting != null)
                                    {
                                        CSTAMessageWaitingEvent_t messageWaiting = (CSTAMessageWaitingEvent_t)cstaEvent.Event.cstaUnsolicited.u.messageWaiting;

                                        Console.WriteLine("onTSAPIEvent.CSTA_MESSAGE_WAITING: deviceID={0};messageWaitingOn={1};", messageWaiting.deviceForMessage.deviceID, messageWaiting.messageWaitingOn);
                                    }
                                }

                                break;

                                #endregion CSTA_MESSAGE_WAITING

                            case Constants.CSTA_LOGGED_ON:

                                #region CSTA_LOGGED_ON

                                {
                                    Console.WriteLine("onTSAPIEvent eventType=CSTA_LOGGED_ON");

                                    if (cstaEvent.Event.cstaUnsolicited.u.loggedOn != null)
                                    {
                                        CSTALoggedOnEvent_t loggedOn = (CSTALoggedOnEvent_t)cstaEvent.Event.cstaUnsolicited.u.loggedOn;

                                        string device = loggedOn.agentDevice.deviceID.device;
                                        string agentID = loggedOn.agentID.agent;

                                        Console.WriteLine("onTSAPIEvent.CSTA_LOGGED_ON: agentID={0};deviceID={1};", agentID, device);
                                    }
                                }

                                break;

                                #endregion CSTA_LOGGED_ON

                            case Constants.CSTA_LOGGED_OFF:

                                #region CSTA_LOGGED_OFF

                                {
                                    Console.WriteLine("onTSAPIEvent eventType=CSTA_LOGGED_OFF");

                                    if (cstaEvent.Event.cstaUnsolicited.u.loggedOff != null)
                                    {
                                        CSTALoggedOffEvent_t loggedOff = (CSTALoggedOffEvent_t)cstaEvent.Event.cstaUnsolicited.u.loggedOff;

                                        string device = loggedOff.agentDevice.deviceID.device;
                                        string agentID = loggedOff.agentID.agent;

                                        Console.WriteLine("onTSAPIEvent.CSTA_LOGGED_OFF: agentID={0};deviceID={1};", agentID, device);
                                    }
                                }

                                break;

                                #endregion CSTA_LOGGED_OFF

                            case Constants.CSTA_NOT_READY:

                                #region CSTA_NOT_READY

                                {
                                    Console.WriteLine("onTSAPIEvent eventType=CSTA_NOT_READY");

                                    if (cstaEvent.Event.cstaUnsolicited.u.notReady != null)
                                    {
                                        CSTANotReadyEvent_t notReady = (CSTANotReadyEvent_t)cstaEvent.Event.cstaUnsolicited.u.notReady;

                                        string device = notReady.agentDevice.deviceID.device;
                                        string agentID = notReady.agentID.agent;

                                        Console.WriteLine("onTSAPIEvent.CSTA_NOT_READY: agentID={0};deviceID={1};", agentID, device);
                                    }
                                }

                                break;

                                #endregion CSTA_NOT_READY

                            case Constants.CSTA_READY:

                                #region CSTA_READY

                                Console.WriteLine("onTSAPIEvent.CSTA_NOT_READY: eventType=CSTA_READY");
                                {
                                    if (cstaEvent.Event.cstaUnsolicited.u.ready != null)
                                    {
                                        CSTAReadyEvent_t ready = (CSTAReadyEvent_t)cstaEvent.Event.cstaUnsolicited.u.ready;

                                        string device = ready.agentDevice.deviceID.device;
                                        string agentID = ready.agentID.agent;

                                        Console.WriteLine("onTSAPIEvent.CSTA_NOT_READY: agentID={0};deviceID={1};", agentID, device);
                                    }
                                }

                                break;

                                #endregion CSTA_READY

                            case Constants.CSTA_WORK_NOT_READY:

                                #region CSTA_WORK_NOT_READY

                                Console.WriteLine("onTSAPIEvent.CSTA_WORK_NOT_READY: eventType=CSTA_WORK_NOT_READY");
                                {
                                    if (cstaEvent.Event.cstaUnsolicited.u.workNotReady != null)
                                    {
                                        CSTAWorkNotReadyEvent_t workNotReady = (CSTAWorkNotReadyEvent_t)cstaEvent.Event.cstaUnsolicited.u.workNotReady;

                                        string agentID = workNotReady.agentID.agent;
                                        string device = workNotReady.agentDevice.deviceID.device;

                                        Console.WriteLine("onTSAPIEvent.CSTA_WORK_NOT_READY: agentID={0};deviceID={1};", agentID, device);
                                    }
                                }

                                break;

                                #endregion CSTA_WORK_NOT_READY

                            case Constants.CSTA_WORK_READY:

                                #region CSTA_WORK_READY

                                Console.WriteLine("onTSAPIEvent eventType=CSTA_WORK_READY");
                                {
                                    if (cstaEvent.Event.cstaUnsolicited.u.workReady != null)
                                    {
                                        CSTAWorkReadyEvent_t workReady = (CSTAWorkReadyEvent_t)cstaEvent.Event.cstaUnsolicited.u.workReady;

                                        string agentID = workReady.agentID.agent;
                                        string device = workReady.agentDevice.deviceID.device;

                                        Console.WriteLine("onTSAPIEvent.CSTA_WORK_READY: agentID={0};deviceID={1};", agentID, device);
                                    }
                                }

                                break;

                                #endregion CSTA_WORK_READY

                            case Constants.CSTA_BACK_IN_SERVICE:

                                #region CSTA_BACK_IN_SERVICE

                                {
                                    if (cstaEvent.Event.cstaUnsolicited.u.backInService != null)
                                    {
                                        CSTABackInServiceEvent_t backInService = (CSTABackInServiceEvent_t)cstaEvent.Event.cstaUnsolicited.u.backInService;
                                    }
                                }

                                break;

                                #endregion CSTA_BACK_IN_SERVICE

                            case Constants.CSTA_OUT_OF_SERVICE:

                                #region CSTA_OUT_OF_SERVICE

                                {
                                    if (cstaEvent.Event.cstaUnsolicited.u.outOfService != null)
                                    {
                                        CSTAOutOfServiceEvent_t outOfService = (CSTAOutOfServiceEvent_t)cstaEvent.Event.cstaUnsolicited.u.outOfService;
                                    }
                                }

                                break;

                                #endregion CSTA_OUT_OF_SERVICE

                            case Constants.CSTA_PRIVATE_STATUS:

                                #region CSTA_PRIVATE_STATUS

                                {
                                    if (cstaEvent.Event.cstaUnsolicited.u.privateStatus != null)
                                    {
                                        CSTAPrivateStatusEvent_t privateStatus = (CSTAPrivateStatusEvent_t)cstaEvent.Event.cstaUnsolicited.u.privateStatus;
                                    }
                                }

                                break;

                                #endregion CSTA_PRIVATE_STATUS

                            case Constants.CSTA_MONITOR_ENDED:

                                #region CSTA_MONITOR_ENDED

                                {
                                    Console.WriteLine("onTSAPIEvent eventType=CSTA_MONITOR_ENDED");

                                    if (cstaEvent.Event.cstaUnsolicited.u.monitorEnded != null)
                                    {
                                        CSTAMonitorEndedEvent_t monitorEnded = (CSTAMonitorEndedEvent_t)cstaEvent.Event.cstaUnsolicited.u.monitorEnded;

                                        Console.WriteLine("onTSAPIEvent.CSTA_MONITOR_ENDED: xref={0};cause={1};", xref, monitorEnded.cause);
                                    }
                                        
                                }

                                break;

                                #endregion CSTA_MONITOR_ENDED
                        }

                        #endregion CSTAUNSOLICITED

                        break;

                    case Constants.ACSCONFIRMATION:

                        #region ACSCONFIRMATION

                        Console.WriteLine("onTSAPIEvent eventClass=ACSCONFIRMATION");

                        switch (cstaEvent.eventHeader.eventType)
                        {
                            case Constants.ACS_OPEN_STREAM_CONF:

                                #region ACS_OPEN_STREAM_CONF

                                {
                                    ACSOpenStreamConfEvent_t acsopen = (ACSOpenStreamConfEvent_t)cstaEvent.Event.acsConfirmation.u.acsopen;
                                }

                                break;

                                #endregion ACS_OPEN_STREAM_CONF

                            case Constants.ACS_CLOSE_STREAM_CONF:

                                #region ACS_CLOSE_STREAM_CONF

                                {
                                    ACSCloseStreamConfEvent_t acsclose = (ACSCloseStreamConfEvent_t)cstaEvent.Event.acsConfirmation.u.acsclose;
                                }

                                break;

                                #endregion ACS_CLOSE_STREAM_CONF

                            case Constants.ACS_SET_HEARTBEAT_INTERVAL_CONF:

                                #region ACS_SET_HEARTBEAT_INTERVAL_CONF

                                {
                                    ACSSetHeartbeatIntervalConfEvent_t acssetheartbeatinterval = (ACSSetHeartbeatIntervalConfEvent_t)cstaEvent.Event.acsConfirmation.u.acssetheartbeatinterval;
                                }

                                break;

                                #endregion ACS_SET_HEARTBEAT_INTERVAL_CONF

                            case Constants.ACS_UNIVERSAL_FAILURE_CONF:

                                #region ACS_UNIVERSAL_FAILURE_CONF

                                {
                                    ACSUniversalFailureConfEvent_t failureEvent = (ACSUniversalFailureConfEvent_t)cstaEvent.Event.acsConfirmation.u.failureEvent;
                                }

                                break;

                                #endregion ACS_UNIVERSAL_FAILURE_CONF

                            default:

                                break;
                        }

                        #endregion ACSCONFIRMATION

                        break;

                    case Constants.ACSUNSOLICITED:

                        #region ACSUNSOLICITED

                        switch (cstaEvent.eventHeader.eventType)
                        {
                            case Constants.ACS_UNIVERSAL_FAILURE:

                                #region ACS_UNIVERSAL_FAILURE

                                {

                                    ACSUniversalFailureEvent_t failureEvent = (ACSUniversalFailureEvent_t)cstaEvent.Event.acsUnsolicited.failureEvent;
                                }

                                break;

                                #endregion ACS_UNIVERSAL_FAILURE
                        }

                        #endregion ACSUNSOLICITED

                        break;
                }
            }
            catch (Exception err)
            {
                Console.WriteLine("Error in TSAPIFacade.m_Client_CSTAEvent: " + err.ToString());
            }
        }

        private void btnGetServerNames_Click(object sender, EventArgs e)
        {
            var serverNames = Client.acsEnumServerNames();

            rtbOutput.AppendText(string.Format("Server Names: [{0}]\n", string.Join(",", serverNames)));

            foreach (string serverName in serverNames)
            {
                if (!cboServerNames.Items.Contains(serverName))
                {
                    cboServerNames.Items.Add(serverName);    
                }
            }
        }

        private void btnGetDevices_Click(object sender, EventArgs e)
        {
            string[] devices = new string[] { };

            BackgroundWorker worker = new BackgroundWorker();


            worker.DoWork += (o, args) =>
            {
                devices = new GetDeviceListCommand(m_Client).GetDeviceList();

                worker.ReportProgress(100);
            };

            worker.RunWorkerCompleted += (o, args) =>
            {
                rtbOutput.AppendText("done getting devices.\n");

                foreach (string device in devices)
                {
                    rtbOutput.AppendText(string.Format("Device: {0}\n", device));
                }
            };

            rtbOutput.AppendText("get devices...\n");
            worker.RunWorkerAsync();
        }

        private void btnMonitorDevice_Click(object sender, EventArgs e)
        {
            BackgroundWorker worker = new BackgroundWorker();

            var monitorCmd = new MonitorDeviceCommand(m_Client, txtDeviceID.Text);

            worker.DoWork += (o, args) =>
            {
                monitorCmd.MonitorDevice();

                worker.ReportProgress(100);
            };

            worker.RunWorkerCompleted += (o, args) =>
            {
                rtbOutput.AppendText("done monitoring device.\n");

                if (monitorCmd.MonitorCrossRef > 0)
                {
                    rtbOutput.AppendText(string.Format("MonitorCrossRef={0}\n", monitorCmd.MonitorCrossRef));
                    m_MonitorXRef = monitorCmd.MonitorCrossRef;
                }
            };

            rtbOutput.AppendText("monitor device...\n");
            worker.RunWorkerAsync();
        }

        private void btnQueryDevice_Click(object sender, EventArgs e)
        {
            BackgroundWorker worker = new BackgroundWorker();

            var queryCmd = new QueryDeviceCommand(m_Client, txtDeviceID.Text);

            worker.DoWork += (o, args) =>
            {
                queryCmd.QueryDevice();

                worker.ReportProgress(100);
            };

            worker.RunWorkerCompleted += (o, args) =>
            {
                rtbOutput.AppendText("done getting device type.\n");

                if (!string.IsNullOrWhiteSpace(queryCmd.DeviceType))
                {
                    rtbOutput.AppendText(string.Format("DeviceType={0}\n", queryCmd.DeviceType));
                }
            };

            rtbOutput.AppendText("get device type...\n");
            worker.RunWorkerAsync();
        }

        private void btnDial_Click(object sender, EventArgs e)
        {
            BackgroundWorker worker = new BackgroundWorker();

            var dialCmd = new DialCommand(m_Client, txtDeviceID.Text, txtNumberToDial.Text);

            worker.DoWork += (o, args) =>
            {
                dialCmd.Dial();

                worker.ReportProgress(100);
            };

            worker.RunWorkerCompleted += (o, args) =>
            {
                rtbOutput.AppendText("done originating call.\n");

                if (dialCmd.CallID > 0)
                {
                    m_CallID = dialCmd.CallID;
                    rtbOutput.AppendText(string.Format("CallID={0}\n", m_CallID));
                }

                if (!string.IsNullOrWhiteSpace(dialCmd.UCID))
                {
                    rtbOutput.AppendText(string.Format("UCID={0}\n", dialCmd.UCID));
                }
            };

            rtbOutput.AppendText(string.Format("dial {0}...\n", txtNumberToDial.Text));
            worker.RunWorkerAsync();
        }

        private void btnHangUp_Click(object sender, EventArgs e)
        {
            if (m_CallID <= 0)
            {
                return;
            }

            BackgroundWorker worker = new BackgroundWorker();

            var hangUpCmd = new HangUpCommand(m_Client, txtDeviceID.Text, m_CallID);

            worker.DoWork += (o, args) =>
            {
                hangUpCmd.HangUp();

                worker.ReportProgress(100);
            };

            worker.RunWorkerCompleted += (o, args) =>
            {
                m_CallID = -1;

                rtbOutput.AppendText("done hanging up call.\n");
            };

            rtbOutput.AppendText("hang up...\n");
            worker.RunWorkerAsync();
        }
    }

    public class ConnectCommand
    {
        private readonly Client m_Client;
        private readonly string m_ServerID;
        private readonly string m_LoginID;
        private readonly string m_Password;
        private readonly ManualResetEvent m_ResponseReceived = new ManualResetEvent(false);
        private readonly int m_InvokeID = InvokeIDGenerator.Generate();

        public bool Connected { get; private set; }
        public string ApiVersion { get; private set; }
        public string DriverVersion { get; private set; }
        public string LibraryVersion { get; private set; }
        public string ServerVersion { get; private set; }
        public int PrivateDataVersion { get; private set; }
        public string ErrorMessage { get; private set; }

        public ConnectCommand(Client client, string serverID, string loginID, string password)
        {
            m_Client = client;
            m_ServerID = serverID;
            m_LoginID = loginID;
            m_Password = password;
        }

        public bool Connect()
        {
            try
            {
                m_Client.TSAPIEvent += onTSAPIEvent;

                int result = m_Client.acsOpenStream(new TSAPIOpenStreamRequest() { InvokeID = m_InvokeID, ServerID = m_ServerID, LoginID = m_LoginID, Passwd = m_Password });

                if (result != 0)
                {
                    return false;
                }

                m_ResponseReceived.WaitOne(TimeSpan.FromMinutes(1));

                return Connected;
            }
            finally
            {
                m_Client.TSAPIEvent -= onTSAPIEvent;
            }
        }

        private void onTSAPIEvent(object sender, TSAPIEventArgs e)
        {
            if (e == null || e.cstaEvent == null || e.cstaEvent.eventHeader.eventClass != Constants.ACSCONFIRMATION)
            {
                return;
            }

            ACSConfirmationEvent acsConfirmation = e.cstaEvent.Event.acsConfirmation;

            if (e.cstaEvent.eventHeader.eventType == Constants.ACS_OPEN_STREAM_CONF)
            {
                Connected = true;

                if (acsConfirmation.u.acsopen != null)
                {
                    ACSOpenStreamConfEvent_t acsopen = (ACSOpenStreamConfEvent_t)acsConfirmation.u.acsopen;

                    //Console.WriteLine(string.Format("TSAPIFacade.Connect: apiVer={0};drvrVer={1};libVer={2};tsrvVer={3};", acsopen.apiVer, acsopen.drvrVer, acsopen.libVer, acsopen.tsrvVer));

                    ApiVersion = acsopen.apiVer.version;
                    DriverVersion = acsopen.drvrVer.version;
                    LibraryVersion = acsopen.libVer.version;
                    ServerVersion = acsopen.tsrvVer.version;

                    if (e.privateData != null)
                    {
                        PrivateData_t privateData = (PrivateData_t)e.privateData;

                        if (privateData.length > 0)
                        {
                            //Console.WriteLine(string.Format("TSAPIFacade.Connect: vendor={0}", privateData.vendor));

                            if (privateData.data[0] == Constants.PRIVATE_DATA_ENCODING)
                            {
                                char c = privateData.data[1];
                                PrivateDataVersion = int.Parse(c.ToString());
                            }
                        }
                    }
                }

                m_ResponseReceived.Set();
            }
            else if (e.cstaEvent.eventHeader.eventType == Constants.ACS_UNIVERSAL_FAILURE_CONF)
            {
                if (acsConfirmation.u.failureEvent != null)
                {
                    ACSUniversalFailureConfEvent_t failureEvent = (ACSUniversalFailureConfEvent_t)acsConfirmation.u.failureEvent;

                    ErrorMessage = failureEvent.error.ToString();

                    m_ResponseReceived.Set();
                }
            }
        }
    }

    public class GetDeviceListCommand
    {
        private readonly Client m_Client;
        private readonly ManualResetEvent m_ResponseReceived = new ManualResetEvent(false);
        private readonly int m_InvokeID = InvokeIDGenerator.Generate();
        private string[] m_Devices = new string[] { };

        public GetDeviceListCommand(Client client)
        {
            m_Client = client;
        }

        public string[] GetDeviceList()
        {
            try
            {
                m_Client.TSAPIEvent += onTSAPIEvent;

                int result = m_Client.cstaGetDeviceList(new TSAPIGetDeviceListRequest() { InvokeID = m_InvokeID, Index = -1, Level = CSTALevel_t.CSTA_DEVICE_DEVICE_MONITOR });

                if (result != 0)
                {
                    return new string[] { };
                }

                m_ResponseReceived.WaitOne(TimeSpan.FromSeconds(5));

                return m_Devices;
            }
            finally
            {
                m_Client.TSAPIEvent -= onTSAPIEvent;
            }
        }

        private void onTSAPIEvent(object sender, TSAPIEventArgs e)
        {
            if (e == null || e.cstaEvent == null || e.cstaEvent.eventHeader.eventClass != Constants.CSTACONFIRMATION || e.cstaEvent.Event.cstaConfirmation == null || e.cstaEvent.Event.cstaConfirmation.invokeID != m_InvokeID || e.cstaEvent.eventHeader.eventType != Constants.CSTA_GET_DEVICE_LIST_CONF || e.cstaEvent.Event.cstaConfirmation.u.getDeviceList == null)
            {
                return;
            }

            CSTAGetDeviceListConfEvent_t getDeviceList = (CSTAGetDeviceListConfEvent_t)e.cstaEvent.Event.cstaConfirmation.u.getDeviceList;

            m_Devices = getDeviceList.devList.device.Select(d => d.device).ToArray();
        }
    }

    public class QueryDeviceCommand
    {
        private readonly Client m_Client;
        private readonly ManualResetEvent m_ResponseReceived = new ManualResetEvent(false);
        private readonly string m_DeviceID;
        private readonly int m_InvokeID = InvokeIDGenerator.Generate();
        private string m_DeviceType = string.Empty;

        public string DeviceType { get { return m_DeviceType; } }

        public QueryDeviceCommand(Client client, string deviceID)
        {
            m_Client = client;
            m_DeviceID = deviceID;
        }

        public void QueryDevice()
        {
            try
            {
                m_Client.TSAPIEvent += onTSAPIEvent;

                int ret = m_Client.cstaQueryDeviceInfo(new TSAPIQueryDeviceInfoRequest() { InvokeID = m_InvokeID, DeviceID = m_DeviceID });

                if (ret != 0)
                {
                    return;
                }

                m_ResponseReceived.WaitOne(TimeSpan.FromSeconds(5));
            }
            finally
            {
                m_Client.TSAPIEvent -= onTSAPIEvent;
            }
        }

        private void onTSAPIEvent(object sender, TSAPIEventArgs e)
        {
            if (e == null || e.cstaEvent == null || e.cstaEvent.eventHeader.eventClass != Constants.CSTACONFIRMATION || e.cstaEvent.Event.cstaConfirmation == null || e.cstaEvent.Event.cstaConfirmation.invokeID != m_InvokeID || e.cstaEvent.eventHeader.eventType != Constants.CSTA_QUERY_DEVICE_INFO_CONF || e.cstaEvent.Event.cstaConfirmation.u.queryDeviceInfo == null)
            {
                return;
            }

            CSTAQueryDeviceInfoConfEvent_t queryDeviceInfo = (CSTAQueryDeviceInfoConfEvent_t)e.cstaEvent.Event.cstaConfirmation.u.queryDeviceInfo;

            m_DeviceType = queryDeviceInfo.deviceType.ToString();

            m_ResponseReceived.Set();
        }
    }

    public class MonitorDeviceCommand
    {
        private readonly Client m_Client;
        private readonly ManualResetEvent m_ResponseReceived = new ManualResetEvent(false);
        private readonly string m_DeviceID;
        private readonly int m_InvokeID = InvokeIDGenerator.Generate();

        public int MonitorCrossRef { get; private set; }

        public MonitorDeviceCommand(Client client, string deviceID)
        {
            m_Client = client;
            m_DeviceID = deviceID;
        }

        public void MonitorDevice()
        {
            try
            {
                m_Client.TSAPIEvent += onTSAPIEvent;

                int ret = m_Client.cstaMonitorDevice(new TSAPIMonitorDeviceRequest() { InvokeID = m_InvokeID, DeviceID = m_DeviceID, AgentFilter = 0, FeatureFilter = 0, MaintenanceFilter = 0, PrivateFilter = 0, FilterPrivateData = false, CallFilter = 0 });

                if (ret != 0)
                {
                    return;
                }

                m_ResponseReceived.WaitOne(TimeSpan.FromSeconds(5));
            }
            finally
            {
                m_Client.TSAPIEvent -= onTSAPIEvent;
            }
        }

        private void onTSAPIEvent(object sender, TSAPIEventArgs e)
        {
            if (e == null || e.cstaEvent == null || e.cstaEvent.eventHeader.eventClass != Constants.CSTACONFIRMATION || e.cstaEvent.Event.cstaConfirmation == null || e.cstaEvent.Event.cstaConfirmation.invokeID != m_InvokeID || e.cstaEvent.eventHeader.eventType != Constants.CSTA_MONITOR_CONF || e.cstaEvent.Event.cstaConfirmation.u.monitorStart == null)
            {
                return;
            }

            CSTAMonitorConfEvent_t monitorStart = (CSTAMonitorConfEvent_t)e.cstaEvent.Event.cstaConfirmation.u.monitorStart;

            MonitorCrossRef = monitorStart.monitorCrossRefID;

            m_ResponseReceived.Set();
        }
    }

    public class DialCommand
    {
        private readonly Client m_Client;
        private readonly string m_CallingDeviceDN;
        private readonly string m_CalledDeviceDN;
        private readonly ManualResetEvent m_ResponseReceived = new ManualResetEvent(false);
        private readonly int m_InvokeID = InvokeIDGenerator.Generate();

        public int CallID { get; private set; }
        public string UCID { get; private set; }
        public string ErrorMessage { get; private set; }

        public DialCommand(Client client, string callingDeviceDN, string calledDevice)
        {
            m_Client = client;
            m_CallingDeviceDN = callingDeviceDN;
            m_CalledDeviceDN = calledDevice;
        }

        public bool Dial()
        {
            try
            {
                m_Client.TSAPIEvent += onTSAPIEvent;

                int result = m_Client.cstaMakeCall(new TSAPIMakeCallRequest() { InvokeID = m_InvokeID, CallingDevice = m_CallingDeviceDN, CalledDevice = m_CalledDeviceDN, UserInfo = string.Empty });

                if (result != 0)
                {
                    return false;
                }

                m_ResponseReceived.WaitOne(TimeSpan.FromMinutes(1));

                return CallID > 0;
            }
            finally
            {
                m_Client.TSAPIEvent -= onTSAPIEvent;
            }
        }

        private void onTSAPIEvent(object sender, TSAPIEventArgs e)
        {
            if (e == null || e.cstaEvent == null || e.cstaEvent.eventHeader.eventClass != Constants.CSTACONFIRMATION)
            {
                return;
            }
            
            CSTAEvent_t cstaEvent = e.cstaEvent;
            ATTEvent_t attEvent = e.attEvent;
            CSTAConfirmationEvent cstaConfirmation = e.cstaEvent.Event.cstaConfirmation;

            if (e.cstaEvent.eventHeader.eventType == Constants.CSTA_MAKE_CALL_CONF)
            {
                if (cstaConfirmation.u.makeCall != null)
                {
                    CSTAMakeCallConfEvent_t makeCall = (CSTAMakeCallConfEvent_t)cstaEvent.Event.cstaConfirmation.u.makeCall;

                    CallID = makeCall.newCall.callID;

                    if (attEvent.u.makeCall != null)
                    {
                        ATTMakeCallConfEvent_t attMakeCall = (ATTMakeCallConfEvent_t)attEvent.u.makeCall;

                        UCID = attMakeCall.ucid.value;
                    }
                }

                m_ResponseReceived.Set();
            }
            else if (e.cstaEvent.eventHeader.eventType == Constants.ACS_UNIVERSAL_FAILURE_CONF)
            {
                ACSConfirmationEvent acsConfirmation = e.cstaEvent.Event.acsConfirmation;

                if (acsConfirmation.u.failureEvent != null)
                {
                    ACSUniversalFailureConfEvent_t failureEvent = (ACSUniversalFailureConfEvent_t)acsConfirmation.u.failureEvent;

                    ErrorMessage = failureEvent.error.ToString();

                    m_ResponseReceived.Set();
                }
            }
        }
    }

    public class HangUpCommand
    {
        private readonly Client m_Client;
        private readonly string m_DeviceDN;
        private readonly int m_CallID;
        private readonly ManualResetEvent m_ResponseReceived = new ManualResetEvent(false);
        private readonly int m_InvokeID = InvokeIDGenerator.Generate();

        public string ErrorMessage { get; private set; }

        public HangUpCommand(Client client, string deviceDN, int callID)
        {
            m_Client = client;
            m_DeviceDN = deviceDN;
            m_CallID = callID;
        }

        public bool HangUp()
        {
            try
            {
                m_Client.TSAPIEvent += onTSAPIEvent;

                int result = m_Client.cstaClearConnection(new TSAPIClearConnectionRequest() { InvokeID = m_InvokeID, DeviceID = m_DeviceDN, CallID = m_CallID });

                if (result != 0)
                {
                    return false;
                }

                return m_ResponseReceived.WaitOne(TimeSpan.FromSeconds(5));
            }
            finally
            {
                m_Client.TSAPIEvent -= onTSAPIEvent;
            }
        }

        private void onTSAPIEvent(object sender, TSAPIEventArgs e)
        {
            if (e == null || e.cstaEvent == null || e.cstaEvent.eventHeader.eventClass != Constants.CSTACONFIRMATION)
            {
                return;
            }

            if (e.cstaEvent.eventHeader.eventType == Constants.CSTA_CLEAR_CONNECTION_CONF)
            {
                m_ResponseReceived.Set();
            }
            else if (e.cstaEvent.eventHeader.eventType == Constants.ACS_UNIVERSAL_FAILURE_CONF)
            {
                ACSConfirmationEvent acsConfirmation = e.cstaEvent.Event.acsConfirmation;

                if (acsConfirmation.u.failureEvent != null)
                {
                    ACSUniversalFailureConfEvent_t failureEvent = (ACSUniversalFailureConfEvent_t)acsConfirmation.u.failureEvent;

                    ErrorMessage = failureEvent.error.ToString();

                    m_ResponseReceived.Set();
                }
            }
        }
    }

    public static class InvokeIDGenerator
    {
        private static int m_LastInvokeID;
        private static readonly object locker = new object();
        public static int Generate()
        {
            int intNextInvokeID;

            lock (locker)
            {
                if (m_LastInvokeID == int.MaxValue)
                {
                    m_LastInvokeID = 0;
                }

                m_LastInvokeID++;

                intNextInvokeID = m_LastInvokeID;
            }

            return intNextInvokeID;
        }
    }
}
