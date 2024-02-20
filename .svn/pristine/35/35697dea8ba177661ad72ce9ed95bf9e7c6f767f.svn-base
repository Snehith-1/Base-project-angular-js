(function () {
    'use strict';

    angular
        .module('angle')
        .controller('osdTrnMyActivity360', osdTrnMyActivity360);

    osdTrnMyActivity360.$inject = ['$rootScope', '$scope', '$sce', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', '$cookies', '$filter', 'ngTableParams', '$timeout', 'DownloaddocumentService', 'cmnfunctionService'];

    function osdTrnMyActivity360($rootScope, $scope, $sce, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, $cookies, $filter, ngTableParams, $timeout, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'osdTrnMyActivity360';
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        var servicerequest_gid = searchObject.servicerequest_gid;
        var bankalert_flag = searchObject.bankalert_flag;
        var bankalert2allocated_gid = searchObject.bankalert2allocated_gid;
        var customer_gid = searchObject.customer_gid;
        var request_refno = searchObject.request_refno;
        var customer_urn = searchObject.customer_urn;
        var lspage = searchObject.lspage;
        var RequestCompletedFlag = searchObject.RequestCompletedFlag;
        var compfilename, forwardfilename;
        var compfilepath, forwardfilepath;
        //var p1;
        //p1='Y'
        var assigned_status;
        activate();

        function activate() {


            lockUI();
            $scope.BankAlert_View = false;
            $scope.servicerequest_gid = servicerequest_gid;


            if (bankalert_flag == 'Y') {
                $scope.BankAlert_View = true;
                $scope.osd_View = false
            }
            else {
                $scope.BankAlert_View = false;
                $scope.osd_View = true;
            }
            $scope.trustAsHtml = function (string) { return $sce.trustAsHtml(string); };

            //var url = 'api/OsdTrnBankAlert/GetAllocatedDtl';
            //var param = {
            //    bankalert2allocated_gid: bankalert2allocated_gid,
            //    customer_gid: customer_gid
            //}


            var url = 'api/OsdTrnBankAlert/GetAllocatedDetail';
            var param = {
                bankalert2allocated_gid: bankalert2allocated_gid,
                customer_gid: customer_gid,
                customer_urn: customer_urn,

            }
            SocketService.getparams(url, param).then(function (resp) {
                $scope.lblkotakAPI_flag = resp.data.kotakAPI_flag;
                $scope.lbldetailsreceived_at = resp.data.detailsreceived_at;
                $scope.lblsource_name = resp.data.source;
                $scope.lblMaster_Acc_No = resp.data.Master_Acc_No;
                $scope.lblRemitt_Info = resp.data.Remitt_Info;
                $scope.lblRemit_Name = resp.data.Remit_Name;
                $scope.lblRemit_Ifsc = resp.data.Remit_Ifsc;
                $scope.lblAmount = resp.data.Amount;
                $scope.lblTxn_Ref_No = resp.data.Txn_Ref_No;
                $scope.lblUtr_No = resp.data.Utr_No;
                $scope.lblPay_Mode = resp.data.Pay_Mode;
                $scope.lblE_Coll_Acc_No = resp.data.E_Coll_Acc_No;
                $scope.lblRemit_Ac_Nmbr = resp.data.Remit_Ac_Nmbr;
                $scope.lblCreditdateandtime = resp.data.Creditdateandtime;
                $scope.lblTxn_Date = resp.data.Txn_Date;
                $scope.lblBene_Cust_Acname = resp.data.Bene_Cust_Acname;
                $scope.lblREF1 = resp.data.REF1;
                $scope.lblREF2 = resp.data.REF2;
                $scope.lblREF3 = resp.data.REF3;
                $scope.lblticketref_no = resp.data.ticketref_no;
                $scope.lblemail_from = resp.data.email_from;
                $scope.lblemail_date = resp.data.email_date;
                $scope.lblemail_subject = resp.data.email_subject;
                $scope.lblemail_content = resp.data.email_content;
                $scope.lblaging = resp.data.aging;
                $scope.lblrelationshipmanager_name = resp.data.relationshipmanager_name;
                $scope.lblallocated_status = resp.data.allocated_status;
                $scope.lblcustomer_urn = resp.data.customer_urnname;
                $scope.lblcustomer_name = resp.data.customername_application;
                $scope.lblvertical = resp.data.vertical;
                $scope.lblconstitution = resp.data.constitution;
                $scope.lblcontact_person = resp.data.contact_person;
                $scope.lblzonal_head = resp.data.zonal_head;
                $scope.lblbusiness_head = resp.data.business_head;
                $scope.lblrm_name = resp.data.rm_name;
                $scope.lblcluster_manager = resp.data.cluster_manager;
                $scope.lblcredit_manager = resp.data.credit_manager;
                $scope.lblzonal_riskmanagerName = resp.data.zonalriskmanager_name;
                $scope.lblriskmanager_name = resp.data.risk_managername;
                $scope.lblriskMonitoring_Name = resp.data.headriskmonitoring_name;
                $scope.lblmobile_no = resp.data.mobile_no;
                $scope.lbladdress_type = resp.data.address_type;
                $scope.lbladdressline1 = resp.data.addressline1;
                $scope.lbladdressline2 = resp.data.addressline2;
                $scope.lblcity = resp.data.city;
                $scope.lblstate = resp.data.state;
                $scope.lbltaluka = resp.data.taluka;
                $scope.lbldistrict = resp.data.district;
                $scope.lblpostal_code = resp.data.postal_code;
                $scope.lblcountry = resp.data.country;
                $scope.lblemail_cc = resp.data.email_cc;
                $scope.lblemail_bcc = resp.data.email_bcc;
                $scope.lbldocument_path = resp.data.document_path;
                $scope.lbldocument_name = resp.data.document_name;
                $scope.lblemail_to = resp.data.email_to;
                $scope.lblfrom_mailaddress = resp.data.from_mailaddress;
                $scope.lblcustomer_name = resp.data.customername_application;
                $scope.lblvertical = resp.data.vertical_name;
                $scope.lblconstitution = resp.data.constitution_name;
                $scope.lblcontact_person = resp.data.contactpersonfirst_name;
                $scope.lblrm_name = resp.data.drm_name;
                $scope.lblrelationshipmanager_name = resp.data.relationship_managername;
                $scope.lblcluster_manager = resp.data.clustermanager_name;
                $scope.lblcredit_manager = resp.data.creditmanager_name;
                $scope.lblzonal_riskmanagerName = resp.data.zonalriskmanager_name;
                $scope.lblriskmanager_name = resp.data.risk_managername;
                $scope.lblriskMonitoring_Name = resp.data.headriskmonitoring_name;
                $scope.lblregional_head = resp.data.regionalhead_name;
                $scope.lblcredithead_name = resp.data.credithead_name;
                $scope.lblcreditnationalmanager_name = resp.data.creditnationalmanager_name;
                $scope.lblcreditregionalmanager_name = resp.data.creditregionalmanager_name;
                $scope.lblzonal_head = resp.data.zonalhead_name;
                $scope.lblbusiness_head = resp.data.businesshead_name;
                unlockUI();
            });

            var url = 'api/OsdTrnBankAlert/GetRMStatusDtl';
            var param = {
                bankalert2allocated_gid: bankalert2allocated_gid,
            }
            SocketService.getparams(url, param).then(function (resp) {
                $scope.lblrm_remarks = resp.data.rm_remarks;
                // $('#lblrm_remarks').html(resp.data.rm_remarks);
                $scope.lblrm_status = resp.data.rm_status;
                $scope.lblupdated_date = resp.data.updated_date;
                $scope.lblupdated_by = resp.data.updated_by;
                $scope.rmdocument_list = resp.data.rmdocument_list;
                $scope.lblrmhfilename = resp.data.rmhfilename;
                $scope.lblrmhfilepath = resp.data.rmhfilepath;
                $scope.refund = $scope.lblrm_status.replace("'","");
            });

            $scope.RequestCompletedFlag = RequestCompletedFlag;

            if ($scope.RequestCompletedFlag == "Y") {
                $scope.remarkscommunication = false;
            }
            else {
                $scope.remarkscommunication = true;
            }

            var param = {
                servicerequest_gid: servicerequest_gid
            }

            var url = "api/OsdTrnMyTicket/GetAllotted360";
            SocketService.getparams(url, param).then(function (resp) {

                $scope.request_refno = resp.data.request_refno;
                $scope.raised_date = resp.data.raised_date;
                $scope.lblraised_date = resp.data.raised_date;
                $scope.request_title = resp.data.request_title;
                $scope.raised_by = resp.data.raised_by;
                $scope.lblraised_by = resp.data.raised_by;
                $scope.request_status = resp.data.request_status;
                $scope.request_description = resp.data.request_description;
                // $('#request_description').html(resp.data.request_description);
                $scope.alloteddocumentdtl = resp.data.alloteddocumentdtl;
                $scope.forwarddocumentdtl = resp.data.forwarddocumentdtl;
                $scope.forward_remarks = resp.data.forward_remarks;
                $scope.forward_date = resp.data.forward_date;
                $scope.forward_to = resp.data.forward_to;
                $scope.forward_flag = resp.data.forward_flag;
                assigned_status = resp.data.assigned_status;
                $scope.transfer_flag = resp.data.transfer_flag;
                $scope.assigned_team = resp.data.assigned_team;
                $scope.assigned_member = resp.data.assigned_member;
                $scope.reopenrequestdocumentdtl = resp.data.reopenrequestdocumentdtl;
                $scope.reopen_reason = resp.data.reopen_reason;
                $scope.reopened_date = resp.data.reopened_date;
                $scope.reopen_flag = resp.data.reopen_flag;
                $scope.completed_flag = resp.data.completed_flag;
                $scope.rejected_flag = resp.data.rejected_flag;
                $scope.rejected_by = resp.data.rejected_by;
                $scope.rejected_date = resp.data.rejected_date;
                $scope.rejected_remarks = resp.data.rejected_remarks;
                $scope.cancel_flag = resp.data.cancel_flag;
                $scope.cancel_date = resp.data.cancel_date;
                $scope.forwardreopendocumentdtl = resp.data.forwardreopendocumentdtl;
                $scope.reopendtl = resp.data.reopendtl;
                $scope.employee_mobileno = resp.data.employee_mobileno;
                $scope.level_one = resp.data.level_one;
                $scope.employee_number = resp.data.employee_number;
                $scope.baselocation_name = resp.data.baselocation_name;
                $scope.lblallotfilename = resp.data.allofilename;
                $scope.lblallotfilepath = resp.data.allofilepath;
                $scope.frrofilepath = resp.data.frrofilepath;
                $scope.frrofilename = resp.data.frrofilename;
                $scope.frfilepath = resp.data.frfilepath;
                $scope.frfilename = resp.data.frfilename;
                $scope.fwdfilename = resp.data.fwdfilename;
                $scope.fwdfilepath = resp.data.fwdfilepath;
                $scope.rofilename = resp.data.rofilename;
                $scope.rofilepath = resp.data.rofilepath;
                
                if ($scope.reopen_flag == "Y") {
                    $scope.reopendetails = true;
                    $scope.reqdtls = false;
                }
                else {
                    $scope.reopendetails = false;
                    $scope.reqdtls = true;
                }

                if ($scope.forward_flag == "Y") {
                    $scope.forward = false;
                    $scope.forwarddtls = true;
                }
                else {
                    $scope.forward = true;
                    $scope.forwarddtls = false;
                }

                if ($scope.rejected_flag == "Y" || $scope.cancel_flag == "Y") {
                    $scope.Completeddtls = false;
                }
                else {
                    $scope.Completeddtls = true;
                }

                if ($scope.rejected_flag == "Y") {
                    $scope.Rejectdtls = true;

                }
                else {
                    $scope.Rejecteddtls = false;

                }
                if ($scope.cancel_flag == "Y") {
                    $scope.canceldtls = true;
                    $scope.cancelleddtlsback = true;
                }
                else {
                    $scope.canceldtls = false;
                    $scope.cancelleddtlsback = false;
                }

            });

            var url = "api/OsdTrnMyTicket/GetApprovalDtls"

            SocketService.getparams(url, param).then(function (resp) {
                $scope.approvaldetails = resp.data.approvaldetails;
                $scope.employee_gid = resp.data.employee_gid;

            });
            var url = 'api/OsdTrnMyTicket/GetBusinessUnitStatusMyActivityList';
            var params = {
                servicerequest_gid: $scope.servicerequest_gid,

            }
            SocketService.getparams(url, params).then(function (resp) {
                $scope.businessstatusunitmyactivity_list = resp.data.businessstatusunitmyactivity_list;

            });
            var url = 'api/OsdTrnMyTicket/GetservicerequestactivityhistoryList';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.servicerequestactivityhistory_list = resp.data.servicerequestactivityhistory_list;

            });
            var url = 'api/OsdTrnMyTicket/GetBusinessUnitStatusMyActivityComplete';
            var params = {
                servicerequest_gid: $scope.servicerequest_gid,

            }
            SocketService.getparams(url, params).then(function (resp) {
                $scope.businessstatusunitmyactivitycompleted_list = resp.data.businessstatusunitmyactivity_list;

            });


            var url = "api/OsdTrnMyTicket/GetAssetDtls"

            SocketService.getparams(url, param).then(function (resp) {
                $scope.assetdetails = resp.data.assetdetails;
                $scope.employee_gid = resp.data.employee_gid;

            });
            var url = "api/OsdTrnMyTicket/GetForwardDocumentUploadtempclear"
            SocketService.getparams(url, params).then(function (resp) {

            });
            var url = "api/OsdTrnMyTicket/GetCompleteDocumentUploadtempclear"
            SocketService.getparams(url, params).then(function (resp) {

            });
            var url = 'api/OsdTrnMyTicket/GetBusinessUnitStatusMyActivityTempClear';
            SocketService.get(url).then(function (resp) {
            });

            var url = 'api/OsdTrnMyTicket/GetBusinessunitStatusMyActivity';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.businessstatuslist = resp.data.businessstatuslist;
            });
            var url = "api/OsdTrnMyTicket/GetMultipleForward"

            SocketService.getparams(url, param).then(function (resp) {
                $scope.forwarddtl = resp.data.forwarddtl;
                $scope.forwardreopendtl = resp.data.forwardreopendtl;
                unlockUI();
            });
          
            var url = "api/OsdTrnTicketManagement/GetAllocateManagerlist"
            SocketService.getparams(url, params).then(function (resp) {
                $scope.allocatelistdtl = resp.data.allocatelistdtl;

            });
            var url = "api/OsdTrnTicketManagement/GetAllocateManagerlist"
            SocketService.getparams(url, params).then(function (resp) {
                $scope.allocatelistdtlreopen = resp.data.allocatelistdtlreopen;

            });
            var url = 'api/OsdTrnBankAlert/GetunreconAllocatedDetail';
            var param = {
                bankalert2allocated_gid: bankalert2allocated_gid,
                customer_gid: customer_gid,
                customer_urn: customer_urn,

            }

            SocketService.getparams(url, param).then(function (resp) {
                $scope.ticketref_no = resp.data.ticketref_no;
                $scope.lblbank_name = resp.data.bank_name;
                $scope.lblcustomer_refno = resp.data.customer_urn;
                $scope.lblbranch_name = resp.data.branch_name;
                $scope.lblcr_dr = resp.data.cr_dr;
                $scope.lbltransc_value = resp.data.transact_val;
                $scope.lblremarks = resp.data.remarks;
                $scope.lbltransc_balance = resp.data.transc_balance;
                $scope.lblacc_no = resp.data.acc_no;
                $scope.lbltrn_date = resp.data.trn_date;
                $scope.lblvalue_date = resp.data.value_date;
                $scope.lblpayment_date = resp.data.payment_date;
                $scope.lbltransact_particulars = resp.data.transact_particulars;
                $scope.lbldebit_amt = resp.data.debit_amt;
                $scope.lblcredit_amt = resp.data.credit_amt;
                $scope.lblchq_no = resp.data.chq_no;
                $scope.lblcreated_by = resp.data.created_by;
                $scope.lblbrs_flag = resp.data.brs_flag;
                $scope.lblsource = resp.data.source;
                $scope.lblallocated_status = resp.data.allocated_status;
                $scope.lbloperation_status = resp.data.operation_status;
                $scope.servicerequest_gid = resp.data.servicerequest_gid;
                $scope.lblkotakAPI_flag = resp.data.kotakAPI_flag;



            });
            var param = {
                banktransc_gid: request_refno,
            }

            var url = 'api/OsdTrnBankAlert/GetUnReconUploadedDoc';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.uploaddocument1 = resp.data.MdlDocDetails;
                $scope.filename = resp.data.filename;
                $scope.filepath = resp.data.filepath;

            });
            var param = {
                servicerequest_gid: servicerequest_gid
            }
            var url = "api/OsdTrnMyTicket/GetRequestorlist"

            SocketService.getparams(url, param).then(function (resp) {
                $scope.requestorlist = resp.data.requestordtl;
                unlockUI();
            });

            var param = {
                servicerequest_gid: servicerequest_gid
            }

            var url = 'api/OsdTrnServiceRequest/GetServiceRequestView';
            SocketService.getparams(url, param).then(function (resp) {

                $scope.completerequestdocumentdtl = resp.data.completerequestdocumentdtl;
                $scope.completed_flag = resp.data.completed_flag;
                $scope.closed_by = resp.data.closed_by;
                $scope.closed_date = resp.data.closed_date;
                $scope.closed_flag = resp.data.closed_flag;
                $scope.rejected_flag = resp.data.rejected_flag;
                $scope.comfilename = resp.data.comfilename;
                $scope.comfilepath = resp.data.comfilepath;
                $scope.tagmemberdtl = resp.data.tagmemberdtl;

                if ($scope.completed_flag == "Y") {
                    $scope.completeddtls = true;
                    $scope.completeddtlsback = true;

                }
                else {
                    $scope.completeddtls = false;
                    $scope.completeddtlsback = false;
                }
                if ($scope.closed_flag == "Y") {

                    $scope.closeddtls = true;
                    $scope.closeddtlsback = true;
                    $scope.completeddtlsback = false;
                    $scope.rejecteddtlsback = false;
                }
                else {
                    $scope.closeddtlsback = false;
                    $scope.closeddtls = false;
                }
                if ($scope.rejected_flag == "Y") {
                    $scope.Rejectdtls = true;
                    $scope.rejecteddtlsback = true;
                    $scope.closeddtlsback = false;
                    $scope.completeddtlsback = false;
                }
                else {
                    $scope.Rejectdtls = false;
                    $scope.rejecteddtlsback = false;
                }

                unlockUI();
            });
            var url = "api/OsdTrnTicketManagement/GetTransferMemberlist"

            SocketService.getparams(url, param).then(function (resp) {
                $scope.transferlistdtl = resp.data.transferlistdtl;
                $scope.transferlistdtlreopen = resp.data.transferlistdtlreopen;
                unlockUI();
            });

            var param = {
                servicerequest_gid: servicerequest_gid
            }
            var url = 'api/OsdTrnMyTicket/GetCompletedDetails';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.completerequestdocumentdtl = resp.data.completerequestdocumentdtl;
                $scope.completed_remarks = resp.data.completed_remarks;
                $scope.completed_by = resp.data.completed_by;
                $scope.completed_date = resp.data.completed_date;
               
                $scope.compfilename = resp.data.complefilename;
                $scope.compfilepath = resp.data.complefilepath;
                unlockUI();
            });

            var param = {
                requestreopen_gid: localStorage.getItem('requestreopen_gid')
            }

            if (localStorage.getItem('requestreopen_gid') == null || localStorage.getItem('requestreopen_gid') == '') {
            }
            else {
                var url = "api/OsdTrnMyTicket/GetReopenHistory";
                SocketService.getparams(url, param).then(function (resp) {

                    $scope.forwardreopendtl = resp.data.forwardreopendtl;
                    $scope.forwarddocumentdtl = resp.data.forwarddocumentdtl;

                    $scope.transferlistdtlreopen = resp.data.transferlistdtlreopen;
                    $scope.completereopendocumentdtl = resp.data.completereopendocumentdtl;
                    $scope.completed_by = resp.data.completed_by;
                    $scope.completed_date = resp.data.completed_date;
                    $scope.completed_remarks = resp.data.completed_remarks;

                    $scope.raised_by = resp.data.raised_by;
                    $scope.raised_date = resp.data.raised_date;
                    $scope.lblfilename = resp.data.filename;
                    $scope.lblfilepath = resp.data.filepath;
                    $scope.fwdfilename = resp.data.fwdfilename;
                    $scope.fwdfilepath = resp.data.fwdfilepath;
                });
            }
            var param = {
                bankalert2allocated_gid: bankalert2allocated_gid,
            }
            var url = "api/OsdTrnRequestApproval/GetRHApprovalDtlsByToken"
            SocketService.getparams(url, param).then(function (resp) {
                $scope.rhapprovaldetails = resp.data.rhapprovaldetails;
                unlockUI();
            });
            var param = {
                bankalert2allocated_gid: bankalert2allocated_gid,
            }
            var url = "api/OsdTrnRequestApproval/GetRHRejectedDtlsByToken"
            SocketService.getparams(url, param).then(function (resp) {
                $scope.rhrejecteddetails = resp.data.rhrejecteddetails;
                unlockUI();
            });
            var url = 'api/UnreconciliationManagement/GetAssignedHistory';
            var param = {
                banktransc_gid: request_refno
            }
            SocketService.getparams(url, param).then(function (resp) {
                $scope.assignedlist = resp.data.assignedlist;
            });

            var url = 'api/UnreconciliationManagement/GetTransferredHistory';
            var param = {
                banktransc_gid: request_refno
            }
            SocketService.getparams(url, param).then(function (resp) {
                $scope.transferlist = resp.data.transferlist;
            });

        }

        //$scope.download = function (val1, val2) {
        //    var phyPath = val1;
        //    var relPath = phyPath.split("EMS");
        //    var relpath1 = relPath[1].replace("\\", "/");
        //    var hosts = window.location.host;
        //    var prefix = location.protocol + "//";
        //    var str = prefix.concat(hosts, relpath1);
        //    var link = document.createElement("a");
        //    var name = val2.split(".")
        //    link.download = val2;
        //    var uri = str;
        //    link.href = uri;
        //    link.click();
        //}
        $scope.download = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.downloadall = function (val1, val2) {

            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }

        }
        $scope.FORdownloadall = function (val1, val2) {

            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }

        }
        $scope.FRDdownloadall = function (val1, val2) {

            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }

        }
        $scope.Udownloadall = function (val1, val2) {

            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }

        }
         $scope.Rdownloadall = function (val1, val2) {

             for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }

        }
         $scope.RHdownloadall = function (val1, val2) {

             for (var i = 0; i < val2.length; i++) {
                 //  console.log(array[i]);
                 DownloaddocumentService.Downloaddocument(val1, val2[i]);
             }

         }

        $scope.fwddownloadall = function (val1, val2) {

            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }

        }
        $scope.uploadallocation = function (val, val1, name) {
            var item = {
                name: val[0].name,
                file: val[0]
            };
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "Default");

               if (IsValidExtension == false) {
                    Notify.alert("File format is not supported..!", {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    return false;
                }

            var frm = new FormData();
            frm.append('fileupload', item.file);
            frm.append('file_name', item.name);
            frm.append('document_name', $scope.documentname);
            frm.append('document_title', $scope.txtdocument_title);
            frm.append('servicerequest_gid', servicerequest_gid);
            frm.append('project_flag', "Default");
            $scope.uploadfrm = frm;
            var url = 'api/OsdTrnMyTicket/ConversationDocUpload';
            lockUI();
            SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                $("#addupload").val('');
                $scope.txtdocument_title = '';
                if (resp.data.status == true) {
                    unlockUI();
                    Notify.alert('Document Uploaded Successfully..!!', 'success')

                    var url = "api/OsdTrnMyTicket/GetRequestorlist"
                    var param = {
                        servicerequest_gid: servicerequest_gid
                    };
                    SocketService.getparams(url, param).then(function (resp) {
                        $scope.requestorlist = resp.data.requestordtl;
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        unlockUI();
                    });
                }
                else {
                    unlockUI();
                    Notify.alert('File Format Not Supported!','warning')

                }

            });

        }
        $scope.businessstatus_add = function (servicerequest_gid) {
            //var lsbusinessstatus_gid = '';
            //  var lsbusiness_status = '';
            //  if ($scope.cbobusinessstatus != undefined || $scope.cbobusinessstatus != null) {
            //      //lsbusinessstatus_gid = $scope.cbobusinessstatus.businessstatus_gid;
            //      lsbusiness_status = $scope.cbobusinessstatus.business_status;
            //}
            //if ((cbobusinessstatus == undefined) || (cbobusinessstatus == '') ) {
            //    Notify.alert('Enter Business Unit Status');
            //    status: 'danger';
            //    pos: 'top-center';
            //    timeout: 3000;
            //}

            //var servicerequest_gid = servicerequest_gid;
            var servicerequest_gid = cmnfunctionService.decryptURL($location.search().hash).servicerequest_gid;
            var business_status = $('#businessstatus :selected').text();
            var params = {
                //businessstatus_gid: $scope.cbobusinessstatus,
                servicerequest_gid: servicerequest_gid,

                business_status: business_status,



                //businessstatus_gid: lsbusinessstatus_gid,



            }
            if ((business_status == null) || (business_status == ' -----Select Business Unit Status-----')) {
                Notify.alert('Enter Business Unit Status', {
                    status: 'warning',
                    pos: 'top-center',
                    timeout: 3000
                });
            }
            else {
                //if ((business_status == undefined) || (business_status == '') ) {
                //    Notify.alert('Enter Business Unit Status');
                //    status: 'danger';
                //    pos: 'top-center';
                //    timeout: 3000;
                //}
                //else {
                var url = 'api/OsdTrnMyTicket/PostBusinessUnitStatusWorkinProgress';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'info',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    businessstatus_list();

                });
            }
            businessstatus_list();
        }

        // Delete Code Ends
        $scope.businessstatus_delete = function (businessstatusactivity_gid) {
            var params =
                {
                    businessstatusactivity_gid: businessstatusactivity_gid
                }
            var url = 'api/OsdTrnMyTicket/DeleteBusinessUnitStatusMyActivity';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'info',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }

                businessstatus_list();
            });

        }
        function businessstatus_list() {
            var url = 'api/OsdTrnMyTicket/GetBusinessUnitStatusMyActivityList';
            var params = {
                servicerequest_gid: servicerequest_gid,

            }
            SocketService.getparams(url, params).then(function (resp) {
                $scope.businessstatusunitmyactivity_list = resp.data.businessstatusunitmyactivity_list;

            });
        }
        //$scope.downloadsdocument = function (val1, val2) {
        //    var phyPath = val1;

        //    var relPath = phyPath.split("EMS");
        //    var relpath1 = relPath[1].replace("\\", "/");
        //    var hosts = window.location.host;
        //    var prefix = location.protocol + "//";
        //    var str = prefix.concat(hosts, relpath1);
        //    var link = document.createElement("a");
        //    var name = val2.split(".")
        //    link.download = val2;
        //    var uri = str;
        //    link.href = uri;

        //    link.click();
        //}
        $scope.downloadsdocument = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.sendrequestorclick = function () {
            var params = {
                servicerequest_gid: servicerequest_gid,
                remarks: $scope.txtqueries
            }
            lockUI();
            var url = "api/OsdTrnMyTicket/PostSendRequestor";
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    var url = "api/OsdTrnMyTicket/GetRequestorlist"
                    var param = {
                        servicerequest_gid: servicerequest_gid
                    };
                    SocketService.getparams(url, param).then(function (resp) {
                        $scope.requestorlist = resp.data.requestordtl;
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        unlockUI();
                    });

                    $scope.txtqueries = "";
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
            });
        }
        $scope.isDisabled = false;
        $scope.requestRejected = function () {
            var modalInstance = $modal.open({
                templateUrl: '/rejectrequestmodal.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'

            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.ok = function () {
                    modalInstance.close('closed');
                };
                $scope.rejectSubmit = function () {
                    var params = {
                        rejected_remarks: $scope.reject_remarks,
                        servicerequest_gid: servicerequest_gid


                    }
                    lockUI();
                    
                    var url = 'api/OsdTrnMyTicket/RejectRequest';
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            modalInstance.close('closed'); Notify.alert(resp.data.message,
                                {
                                    status: 'success',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                            unlockUI();
                            $state.go('app.OsdTrnWorkMyTicket');

                    }

                        else {
                            modalInstance.close('closed');
                            Notify.alert(resp.data.message,
                                {
                                    status: 'warning',
                                    pos: 'top-center',
                                    timeout: 3000
                            }); unlockUI();
                        }

                    });


                }
            }

        }
        $scope.ForwardRequestDetails = function () {

            var modalInstance = $modal.open({
                templateUrl: '/forwardrequestmodal.html',
                controller: ModalInstanceCtrl,
                size: 'lg',
                backdrop: 'static',
                keyboard: false,
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var url = 'api/OsdTrnServiceRequest/GetEmployees';
                SocketService.get(url).then(function (resp) {
                    $scope.employee_list = resp.data.employee_list;
                });

                $scope.ok = function () {
                    modalInstance.close('closed');
                    activate();
                };
                $scope.Fdownloadall = function () {

                    for (var i = 0; i < forwardfilename.length; i++) {
                        //  console.log(array[i]);
                        DownloaddocumentService.Downloaddocument(forwardfilepath, forwardfilename[i]);
                    }

                }
                $scope.forwardSubmit = function () {

                    var params = {
                        servicerequest_gid: servicerequest_gid,
                        forwardto_gid: $scope.cboteam_member.employee_gid,
                        forward_to: $scope.cboteam_member.employee_name,
                        forward_remarks: $scope.forward_remarks
                    }
                    lockUI();
                    var url = "api/OsdTrnMyTicket/ForwardTicket"
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $scope.isDisabled = true;
                            modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });

                            $state.go('app.OsdTrnWorkMyTicket');
                            unlockUI();

                        }
                        else {
                            modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                        }
                    });
                    activate();
                }


                $scope.ForwardRequestDocumentUpload = function () {
                    
                    var fi = document.getElementById('file');
                    console.log(fi);
                    if (fi.files.length > 0) {
                        var frm = new FormData();
                        for (var i = 0; i <= fi.files.length - 1; i++) {

                            frm.append(fi.files[i].name, fi.files[i]);
                           
                            $scope.uploadfrm = frm;
                            var fname = fi.files.item(i).name;
                            var fsize = fi.files.item(i).size;
                            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(fname, "Default");

                        if (IsValidExtension == false) {
                                Notify.alert("File format is not supported..!", {
                                    status: 'danger',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                return false;
                            }

                        }
                        frm.append('project_flag', "Default");
                        lockUI();
                        var url = 'api/OsdTrnMyTicket/ForwardDocumentUpload';
                        SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                            $("#file").val('');
                            $scope.upload_list = resp.data.upload_list;
                            forwardfilename = resp.data.forwardfilename;
                            forwardfilepath = resp.data.forwardfilepath;
                            console.log($scope.upload_list);
                            unlockUI();
                            if (resp.data.status == true) {
                                Notify.alert(resp.data.message, {
                                    status: 'success',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                            }
                            else {
                                Notify.alert(resp.data.message, {
                                    status: 'info',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                            }
                            unlockUI();
                        });
                    }
                    else {
                        Notify.alert('Please select a file.')
                    }
                }
                //$scope.downloads = function (val1, val2) {
                //    var phyPath = val1;
                //    var relPath = phyPath.split("StoryboardAPI");
                //    var relpath1 = relPath[1].replace("\\", "/");
                //    var hosts = window.location.host;
                //    var prefix = location.protocol + "//";
                //    var str = prefix.concat(hosts, relpath1);
                //    var link = document.createElement("a");
                //    link.download = val2;
                //    var uri = str;
                //    link.href = uri;
                //    link.click();
                //}
                $scope.downloads = function (val1, val2) {
                    DownloaddocumentService.Downloaddocument(val1, val2);
                }
                $scope.downloadall = function (val1, val2) {

                    for (var i = 0; i < val2.length; i++) {
                        //  console.log(array[i]);
                        DownloaddocumentService.Downloaddocument(val1, val2[i]);
                    }

                }
                $scope.uploaddocumentcancel = function (tmp_documentGid) {
                    lockUI();
                    var params = {
                        tmp_documentGid: tmp_documentGid
                    }
                    var url = 'api/OsdTrnMyTicket/GetForwardtmpDocumentDelete';
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.upload_list = resp.data.upload_list;
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }
                        unlockUI();
                    });
                }

                $scope.documentviewer = function (val1, val2) {
                    lockUI();
                    var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
                    if (IsValidExtension == false) {
                        Notify.alert("View is not supported for this format..!", {
                            status: 'danger',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        unlockUI();
                        return false;
                    }
                    DownloaddocumentService.DocumentViewer(val1, val2);
                }

            }
        }

        //$scope.downloadsdocs = function (val1, val2) {
        //    var phyPath = val1;
        //    var relPath = phyPath.split("StoryboardAPI");
        //    var relpath1 = relPath[1].replace("\\", "/");
        //    var hosts = window.location.host;
        //    var prefix = location.protocol + "//";
        //    var str = prefix.concat(hosts, relpath1);
        //    var link = document.createElement("a");
        //    link.download = val2;
        //    var uri = str;
        //    link.href = uri;
        //    link.click();
        //}
        $scope.downloadsdocs = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.requestCompleted = function () {

            var modalInstance = $modal.open({
                templateUrl: '/completedrequestmodal.html',
                controller: ModalInstanceCtrl,
                size: 'lg',
                backdrop: 'static',
                keyboard: false,
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.ok = function () {
                    modalInstance.close('closed');
                    activate();
                };

                $scope.completedSubmit = function () {
                    var params = {
                        completed_remarks: $scope.completed_remarks,
                        servicerequest_gid: servicerequest_gid
                    }
                    lockUI();
                    var url = 'api/OsdTrnMyTicket/GetCompletedRequest';
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                            $state.go('app.OsdTrnWorkMyTicket');
                        }
                        else {
                            modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                        }
                    });
                }

                //$scope.downloadsdoc = function (val1, val2) {
                //    var phyPath = val1;
                //    var relPath = phyPath.split("StoryboardAPI");
                //    var relpath1 = relPath[1].replace("\\", "/");
                //    var hosts = window.location.host;
                //    var prefix = location.protocol + "//";
                //    var str = prefix.concat(hosts, relpath1);
                //    var link = document.createElement("a");
                //    link.download = val2;
                //    var uri = str;
                //    link.href = uri;
                //    link.click();
                //}
                $scope.downloadsdoc = function (val1, val2) {
                    DownloaddocumentService.Downloaddocument(val1, val2);
                }

                
                $scope.Cdownloadall = function () {

                    for (var i = 0; i < compfilename.length; i++) {
                        //  console.log(array[i]);
                        DownloaddocumentService.Downloaddocument(compfilepath, compfilename[i]);
                    }

                }
                $scope.CompleteRequestDocumentUpload = function () {
                
                    var fi = document.getElementById('file2');
                    console.log(fi);
                    if (fi.files.length > 0) {
                        var frm = new FormData();
                        for (var i = 0; i <= fi.files.length - 1; i++) {

                            frm.append(fi.files[i].name, fi.files[i]);
                           
                            $scope.uploadfrm = frm;
                            var fname = fi.files.item(i).name;
                            var fsize = fi.files.item(i).size;
                            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(fname, "");

                            if (IsValidExtension == false) {
                                Notify.alert("File format is not supported..!", {
                                    status: 'danger',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                return false;
                            }

                        }
                        frm.append('project_flag', "Default");
                        lockUI();
                        var url = 'api/OsdTrnMyTicket/CompleteDocumentUpload';
                        SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                            $("#file2").val('');
                            $scope.upload_list = resp.data.upload_list;
                            compfilename = resp.data.compfilename;
                            compfilepath = resp.data.compfilepath;
                            unlockUI();
                            if (resp.data.status == true) {
                                Notify.alert(resp.data.message, {
                                    status: 'success',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                            }
                            else {
                                Notify.alert(resp.data.message, {
                                    status: 'info',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                            }
                            unlockUI();
                        });
                    }
                    else {
                        alert('Please select a file.')
                    }
                }


                $scope.uploaddocumentdelete = function (tmp_documentGid) {
                    lockUI();
                    var params = {
                        tmp_documentGid: tmp_documentGid
                    }
                    var url = 'api/OsdTrnMyTicket/GettmpCompleteDocDelete';
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.upload_list = resp.data.upload_list;
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }
                        unlockUI();
                    });
                }

                $scope.documentviewer = function (val1, val2) {
                    lockUI();
                    var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
                    if (IsValidExtension == false) {
                        Notify.alert("View is not supported for this format..!", {
                            status: 'danger',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        unlockUI();
                        return false;
                    }
                    DownloaddocumentService.DocumentViewer(val1, val2);
                }

            }
        }

        $scope.ServiceRequestViewDocumentUpload = function () {
            lockUI();
            var fi = document.getElementById('file1');
            if (fi.files.length > 0) {
                var frm = new FormData();
                for (var i = 0; i <= fi.files.length - 1; i++) {

                    frm.append(fi.files[i].name, fi.files[i])
                    $scope.uploadfrm = frm;
                    var fname = fi.files.item(i).name;
                    var fsize = fi.files.item(i).size;
                }
                frm.append('project_flag', "Default");
                frm.append('servicerequest_gid', servicerequest_gid);

                var url = 'api/OsdTrnServiceRequest/RequestViewDocUpload';
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                    $("#file1").val('');
                    $scope.alloteddocumentdtl = resp.data.servicerequestdocumentdtl;
                    $scope.lblfilename = resp.data.filename;
                    $scope.lblfilepath = resp.data.filepath;
                    unlockUI();
                    if (resp.data.status == true) {
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });

                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'info',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    unlockUI();
                });
            }
            else {
                alert('Please select a file.')
            }

        }

        //$scope.downloads = function (val1, val2) {
        //    var phyPath = val1;
        //    var relPath = phyPath.split("StoryboardAPI");
        //    var relpath1 = relPath[1].replace("\\", "/");
        //    var hosts = window.location.host;
        //    var prefix = location.protocol + "//";
        //    var str = prefix.concat(hosts, relpath1);
        //    var link = document.createElement("a");
        //    link.download = val2;
        //    var uri = str;
        //    link.href = uri;
        //    link.click();
        //}
        $scope.downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.Back = function () {
            $state.go('app.OsdTrnAllotedMyTicket');
        }
        $scope.WorkinProgressBack = function () {
            $state.go('app.OsdTrnWorkMyTicket');
        }

        $scope.CompletedBack = function () {
            $state.go('app.OsdTrnCompletedMyTicket');
        }
        $scope.ClosedBack = function () {
            $state.go('app.OsdTrnClosedMyTicket');
        }

        $scope.cancelclick = function () {
            $scope.txtqueries = "";
        }

        // Previous History Code 
        $scope.tickethistory = function (servicerequest_gid) {
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/osdTrnMyActivityHistory?hash=" + cmnfunctionService.encryptURL("servicerequest_gid=" + servicerequest_gid + "");
            window.open(URL, '_blank');
        }
        // Reopen History
        $scope.ticketreopenhistory = function (requestreopen_gid, servicerequest_gid) {

            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/osdTrnMyActivityReopenHistory?hash=" + cmnfunctionService.encryptURL("servicerequest_gid=" + servicerequest_gid + " &requestreopen_gid=" + requestreopen_gid + "");
            window.open(URL, '_blank');
        }

        $scope.CancelApprovalPerson = function (approval_token) {

            var modalInstance = $modal.open({
                templateUrl: '/cancelmembermodal.html',
                controller: ModalInstanceCtrl,
                size: 'md',
                backdrop: 'static',
                keyboard: false,
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.ok = function () {
                    modalInstance.close('closed');
                };
                lockUI();
                var param = {
                    approval_token: approval_token
                };
                var url = 'api/OsdTrnRequestApproval/GetRequestDtl';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.request_title = resp.data.request_title;
                    $scope.request_refno = resp.data.request_refno;
                    $scope.activity_name = resp.data.activity_name;
                    $scope.assigned_dtl = resp.data.assigned_dtl;
                    $scope.getapproval_remarks = resp.data.getapproval_remarks;
                    $scope.hierary_level = resp.data.hierary_level;
                    $scope.servicerequest_gid = resp.data.servicerequest_gid;
                    $scope.approval_type = resp.data.approval_type;
                    unlockUI();
                });


                $scope.CancelMemberSubmit = function () {
                    var hierarylevel = $scope.hierary_level;
                    var level = ++hierarylevel;

                    var params = {
                        approval_token: approval_token,
                        approval_remarks: $scope.txtremarks,
                        hierary_level: level,
                        servicerequest_gid: $scope.servicerequest_gid,
                        approval_type: $scope.approval_type
                    }
                    lockUI();
                    var url = "api/OsdTrnRequestApproval/PostRequestCancelled";
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();

                            activate();
                        }
                        else {
                            modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();

                            activate();

                        }
                    });
                }
            }
        }

        // Tagmember in chat Code Starts
        $scope.TagMemberChat = function () {
            var modalInstance = $modal.open({
                templateUrl: '/tagmemberinchat.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    modalInstance.close('closed');
                };

                var params = {
                    servicerequest_gid: servicerequest_gid
                }
                var url = 'api/OsdTrnServiceRequest/TagEmployee';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.employee_list = resp.data.employee_list;
                });
                var url = 'api/OsdTrnServiceRequest/GetServiceRequestView';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.tagmemberdtl = resp.data.tagmemberdtl;
                    $scope.raised_date = resp.data.raised_date;
                });

                // Submit
                $scope.TagMemberSubmit = function () {
                    var params = {
                        servicerequest_gid: servicerequest_gid,
                        tagmemberdtl: $scope.tagmember_list
                    }
                    lockUI();
                    var url = "api/OsdTrnServiceRequest/PostTagMemberInChat"
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });

                            unlockUI();
                            activate();
                        }
                        else {
                            modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                        }
                    });
                }
                // Click Cancel Button
                $scope.ok = function () {
                    modalInstance.close('closed');
                };
            }
        }

        // Get Request Remarks
        $scope.request_remarks = function (requestapproval_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/RequestRemarks.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                   {
                       requestapproval_gid: requestapproval_gid,
                   }
                var url = 'api/osdTrnMyTicket/GetRequestRemarks';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtrequestapproval_remarks = resp.data.requestapproval_remarks;

                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }

        $scope.documentviewer = function (val1, val2) {
            lockUI();
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
            if (IsValidExtension == false) {
                Notify.alert("View is not supported for this format..!", {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                unlockUI();
                return false;
            }
            DownloaddocumentService.DocumentViewer(val1, val2);
        }
    }
})();