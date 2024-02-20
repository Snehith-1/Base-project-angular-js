(function () {
    'use strict';

    angular
        .module('angle')
        .controller('osdTrnServiceRequestTaggedView', osdTrnServiceRequestTaggedView);

    osdTrnServiceRequestTaggedView.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'DownloaddocumentService', 'cmnfunctionService'];

    function osdTrnServiceRequestTaggedView($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'osdTrnServiceRequestTaggedView';
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        var servicerequest_gid = searchObject.servicerequest_gid;       
        var lstab = searchObject.lstab;
        var CompletedFlag = searchObject.CompletedFlag;
        var bankalert_flag = searchObject.bankalert_flag;
        var bankalert2allocated_gid = searchObject.bankalert2allocated_gid;
        var customer_gid = searchObject.customer_gid;
        var customer_urn = searchObject.customer_urn;
        activate();

        function activate() {
            $scope.completedflag = CompletedFlag;
            $scope.servicerequest_gid = servicerequest_gid;
            lockUI();
            var params = {
                servicerequest_gid: servicerequest_gid
            }
            var url = 'api/OsdTrnServiceRequest/GetServiceRequestView';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.request_refno = resp.data.request_refno;
                $scope.raised_date = resp.data.raised_date;
                $scope.raised_by = resp.data.raised_by;
                $scope.raised_department = resp.data.raised_department;
                $scope.activity_name = resp.data.activity_name;
                $scope.request_title = resp.data.request_title;
                $scope.request_description = resp.data.request_description;
                // $('#request_description').html(resp.data.request_description);
                $scope.request_status = resp.data.request_status;
                $scope.servicerequestdocumentdtl = resp.data.servicerequestdocumentdtl;
                $scope.reopenrequestdocumentdtl = resp.data.reopenrequestdocumentdtl;
                $scope.tagmemberdtl = resp.data.tagmemberdtl;
                $scope.transfer_flag = resp.data.transfer_flag;
                $scope.assigned_team = resp.data.assigned_team;
                $scope.assigned_to = resp.data.assigned_to;
                $scope.employee_mobileno = resp.data.employee_mobileno;
                $scope.employee_number = resp.data.employee_number;
                $scope.level_one = resp.data.level_one;
                $scope.lblsrfilename = resp.data.srfilename;
                $scope.lblsrfilepath = resp.data.srfilepath;
                $scope.reopenfilename = resp.data.reopenfilename;
                $scope.reopenfilepath = resp.data.reopenfilepath;
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
                banktransc_gid: $scope.ticketref_no,
            }

            var url = 'api/OsdTrnBankAlert/GetUnReconUploadedDoc';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.uploaddocument1 = resp.data.MdlDocDetails;
                $scope.lblfilename = resp.data.filename;
                $scope.lblfilepath = resp.data.filepath;

            });
           
            var url = 'api/OsdTrnServiceRequest/GetServiceRequestView';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.rejected_flag = resp.data.rejected_flag;
                $scope.reopenrequestdocumentdtl = resp.data.reopenrequestdocumentdtl;
                $scope.cancel_flag = resp.data.cancel_flag;
                $scope.closed_flag = resp.data.closed_flag;
                $scope.cancel_date = resp.data.cancel_date;
                $scope.rejected_date = resp.data.rejected_date;
                $scope.rejected_by = resp.data.rejected_by;
                $scope.rejected_remarks = resp.data.rejected_remarks;
                $scope.reopen_flag = resp.data.reopen_flag;

                if ($scope.reopen_flag == "Y") {
                    $scope.reopendetails = true;
                    $scope.reqdtls = false;
                }
                else {
                    $scope.reopendetails = false;
                    $scope.reqdtls = true;
                }

                $scope.completed_flag = resp.data.completed_flag;
                if ($scope.completed_flag == "Y") {
                    
                    $scope.completeddtls = false;
                }
                else {
                    
                    $scope.completeddtls = true;
                }

                if ($scope.cancel_flag == "Y") {
                    $scope.canceldtls = true;
                }
                else {
                    $scope.canceldtls = false;
                }
                if ($scope.rejected_flag == "Y") {
                    $scope.Rejectdtls = true;
                }
                else {
                    $scope.Rejecteddtls = false;
                }
                if ($scope.rejected_flag == "Y" || $scope.cancel_flag == "Y" || $scope.completedflag == "Y" || $scope.closed_flag == "Y") {
                    $scope.fileuploaddtls = false;
                }
                else {
                    $scope.fileuploaddtls = true;
                }
                if ($scope.rejected_flag == "Y" || $scope.cancel_flag == "Y") {
                    $scope.communicationdtls = false;
                }
                else {
                    $scope.communicationdtls = true;
                }

            });
            var url = 'api/OsdTrnMyTicket/GetBusinessUnitStatusMyActivityList';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.businessstatusunitmyactivity_list = resp.data.businessstatusunitmyactivity_list;

            });
            var url = 'api/OsdTrnMyTicket/GetservicerequestactivityhistoryList';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.servicerequestactivityhistory_list = resp.data.servicerequestactivityhistory_list;

            });
            var url = "api/OsdTrnMyTicket/GetAllotted360";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.reopenrequestdocumentdtl = resp.data.reopenrequestdocumentdtl;
                $scope.forwarddocumentdtl = resp.data.forwarddocumentdtl;
                $scope.forward_remarks = resp.data.forward_remarks;
                $scope.forward_date = resp.data.forward_date;
                $scope.forward_to = resp.data.forward_to;
                $scope.forward_flag = resp.data.forward_flag;
                $scope.reopendtl = resp.data.reopendtl;
            });

            var url = "api/OsdTrnMyTicket/GetApprovalDtls"

            SocketService.getparams(url, params).then(function (resp) {
                $scope.approvaldetails = resp.data.approvaldetails;

            });

            var url = "api/OsdTrnTicketManagement/GetTransferMemberlist"

            SocketService.getparams(url, params).then(function (resp) {
                $scope.transferlistdtl = resp.data.transferlistdtl;
                unlockUI();
            });

            var url = "api/OsdTrnMyTicket/GetRequestorlist"

            SocketService.getparams(url, params).then(function (resp) {
                $scope.requestorlist = resp.data.requestordtl;
               
                unlockUI();
            });
            var  params={
                servicerequest_gid: servicerequest_gid
            }
            var url = 'api/OsdTrnServiceRequest/GetServiceRequestView';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.tagmemberdtl = resp.data.tagmemberdtl;
                $scope.completed_flag = resp.data.completed_flag;
                $scope.closed_by = resp.data.closed_by;
                $scope.closed_date = resp.data.closed_date;
                $scope.closed_flag = resp.data.closed_flag;
                if ($scope.closed_flag == "Y") {

                    $scope.closeddtls = true;
                }
                else {

                    $scope.closeddtls = false;
                }
                if ($scope.completed_flag == "Y") {

                    $scope.completeddtls = true;
                }
                else {

                    $scope.completeddtls = false;
                }
            });
            var url = 'api/OsdTrnBankAlert/GetAllocatedDetail';
var param = {
bankalert2allocated_gid: bankalert2allocated_gid,
customer_gid: customer_gid,
customer_urn: customer_urn,



}



SocketService.getparams(url, param).then(function (resp) {
    $scope.lblkotakAPI_flag = resp.data.kotakAPI_flag;
    $scope.lbldetailsreceived_at = resp.data.detailsreceived_at;
    $scope.lblsource = resp.data.source;
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
$scope.lblcustomer_name = resp.data.customer_name;
$scope.lblvertical = resp.data.vertical;
$scope.lblconstitution = resp.data.constitution;
$scope.lblcontact_person = resp.data.contact_person;
$scope.lblzonal_head = resp.data.zonalhead_name;
$scope.lblbusiness_head = resp.data.businesshead_name;
$scope.lblrm_name = resp.data.rm_name;
$scope.lblcluster_manager = resp.data.cluster_manager;
$scope.lblcredit_manager = resp.data.credit_manager;
$scope.lblzonal_riskmanagerName = resp.data.zonal_riskmanagerName;
$scope.lblriskmanager_name = resp.data.riskmanager_name;
$scope.lblriskMonitoring_Name = resp.data.riskMonitoring_Name;
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
$scope.lbloperation_status = resp.data.operation_status;
$scope.servicerequest_gid = resp.data.servicerequest_gid;
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
$scope.transfer_flag = resp.data.transfer_flag;
if (resp.data.operation_status == 'Completed' || resp.data.operation_status == 'Assigned')
{
var url = 'api/OsdTrnBankAlert/GetOperationStatusDtl';
var param = {
bankalert2allocated_gid: bankalert2allocated_gid,
}
SocketService.getparams(url, param).then(function (resp) {
$scope.lblassigned_remarks = resp.data.assigned_remarks;
$scope.lblassigned_date = resp.data.assigned_date;
$scope.lblassigned_toname = resp.data.assigned_toname;
$scope.lblassigned_by = resp.data.assigned_by;
$scope.document_list = resp.data.rmdocument_list;
});
var url = "api/OsdTrnMyTicket/GetRequestorlist"
var param = {
servicerequest_gid: $scope.servicerequest_gid,
}
SocketService.getparams(url, param).then(function (resp) {
$scope.requestorlist = resp.data.requestordtl;
unlockUI();
});
var param = {
servicerequest_gid: $scope.servicerequest_gid,
}
var url = 'api/OsdTrnMyTicket/GetCompletedDetails';
SocketService.getparams(url, param).then(function (resp) {
$scope.completerequestdocumentdtl = resp.data.completerequestdocumentdtl;
$scope.completed_remarks = resp.data.completed_remarks;
$scope.completed_by = resp.data.completed_by;
$scope.completed_date = resp.data.completed_date;
$scope.complefilename = resp.data.complefilename;
$scope.complefilepath = resp.data.complefilepath;
unlockUI();
});



}
var param = {
servicerequest_gid: $scope.servicerequest_gid,
}
var url = "api/OsdTrnTicketManagement/GetTransferMemberlist"



SocketService.getparams(url, param).then(function (resp) {
$scope.transferlistdtl = resp.data.transferlistdtl;
$scope.transferlistdtlreopen = resp.data.transferlistdtlreopen;
unlockUI();
});



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
});
var url = 'api/employee/Employee';
SocketService.get(url).then(function (resp) {
$scope.employee_list = resp.data.employee_list;
});
var url = 'api/OsdTrnBankAlert/GetticketTransferLog'
var param = {
bankalert2allocated_gid: bankalert2allocated_gid,
}
SocketService.getparams(url, param).then(function (resp) {
$scope.transferto_name = resp.data.relationshipmanager_name;
$scope.transferedinitiated_by = resp.data.transferedinitiated_by;
$scope.transferinitiated_date = resp.data.transferinitiated_date;
$scope.transfer_type = resp.data.transfer_type;
$scope.transfer_remarks = resp.data.transfer_remarks;
});
            var  params={
                servicerequest_gid: servicerequest_gid
            }
            var url = 'api/OsdTrnMyTicket/GetCompletedDetails';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.completerequestdocumentdtl = resp.data.completerequestdocumentdtl;
                $scope.completed_remarks = resp.data.completed_remarks;
                $scope.completed_by = resp.data.completed_by;
                $scope.completed_date = resp.data.completed_date;
                $scope.complefilename = resp.data.complefilename;
                $scope.complefilepath = resp.data.complefilepath;
                unlockUI();
            });
            var  params={
                servicerequest_gid: servicerequest_gid
            }
            var url = 'api/OsdTrnServiceRequest/GetServiceRequestView';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.tagmemberdtl = resp.data.tagmemberdtl;
                $scope.completed_flag = resp.data.completed_flag;
                $scope.closed_by = resp.data.closed_by;
                $scope.closed_date = resp.data.closed_date;
                $scope.closed_flag = resp.data.closed_flag;
                $scope.bankalert_flag = resp.data.bankalert_flag;
                //if ($scope.bankalert_flag == "Y") {

                //    $scope.bankalert_flag = true;
                //}
                //if ($scope.bankalert_flag == "N") {

                //    $scope.bankalert_list = true;
                //}
                //else {

                //    $scope.bankalert_flag = false;
                //}
                if ($scope.closed_flag == "Y") {

                    $scope.closeddtls = true;
                }
                else {

                    $scope.closeddtls = false;
                }
                if ($scope.completed_flag == "Y") {

                    $scope.completeddtls = true;
                }
                else {

                    $scope.completeddtls = false;
                }
            });

            
            
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
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        unlockUI();
                    });
                }
                else {
                    unlockUI();
                    Notify.alert('File Format Not Supported!', 'danger')

                }

            });

        }

        //$scope.downloadsdocument = function (val1, val2) {
        //    //console.log(val1, val2);
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
                                status: 'danger',
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
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
            });
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
        $scope.COMdownloadall = function (val1, val2) {

            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }

        }
        $scope.requestClosed = function () {
            var params = {
                servicerequest_gid: servicerequest_gid
            }
            lockUI();
            var url = 'api/OsdTrnServiceRequest/GetClosedRequest';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                    $location.url('app/osdTrnServiceRequestSummary');
                }
                else {

                    Notify.alert(resp.data.message, {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();

                }
            });
        }
       
        $scope.TaggedBack = function () {
            $location.url('app/osdTrnTaggedRequestSummary');
        }

        $scope.cancelclick = function () {
            $scope.txtqueries = "";
        }

        // Tagmember in chat Code Starts
        $scope.TagMemberChat = function () {
            var modalInstance = $modal.open({
                templateUrl: '/tagmemberinchat.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false
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
                            activate();
                            unlockUI();
                        }
                        else {
                            modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'danger',
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

        // Previous History Code 
        $scope.tickethistory = function (servicerequest_gid) {
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/osdTrnMyActivityHistory?hash=" + cmnfunctionService.encryptURL("servicerequest_gid=" + servicerequest_gid + "");
            window.open(URL, '_blank');
        }
        // Reopen History
        $scope.ticketreopenhistory = function (requestreopen_gid, servicerequest_gid) {

            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/osdTrnMyActivityReopenHistory?hash=" + cmnfunctionService.encryptURL("requestreopen_gid=" + requestreopen_gid + "&servicerequest_gid=" + servicerequest_gid + "");
            window.open(URL, '_blank');
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
