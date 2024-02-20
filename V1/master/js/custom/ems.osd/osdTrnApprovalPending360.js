(function () {
    'use strict';


    angular
        .module('angle')
        .controller('osdTrnApprovalPending360', osdTrnApprovalPending360);

    osdTrnApprovalPending360.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$filter', 'ngTableParams', '$resource', '$timeout', 'ngTableDataService', '$sce', 'DownloaddocumentService', 'cmnfunctionService'];

    function osdTrnApprovalPending360($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $filter, ngTableParams, $resource, $timeout, ngTableDataService, $sce, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'osdTrnApprovalPending360';
        var assigned_status;
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        var servicerequest_gid = searchObject.servicerequest_gid;
        var bankalert_flag = searchObject.bankalert_flag;
        var bankalert2allocated_gid = searchObject.bankalert2allocated_gid;
        var customer_gid = searchObject.customer_gid;
        var customer_urn = searchObject.customer_urn;
        var request_refno = searchObject.request_refno;
        var lspage = searchObject.lspage;
        var RequestCompletedFlag = searchObject.RequestCompletedFlag;

        activate();

        function activate() {
            lockUI();
            $scope.RequestCompletedFlag = RequestCompletedFlag;
            //var url = window.location.href;
            //var relPath = url.split("lstab=");
            //$scope.relpath1 = relPath[1];
            $scope.BankAlert_View = false;
            $scope.tdshow = false;


            if (bankalert_flag == 'Y') {
                $scope.BankAlert_View = true;
                $scope.osd_View = false
            }
            else {
                $scope.BankAlert_View = false;
                $scope.osd_View = true;
            }


            if ($scope.RequestCompletedFlag == "N") {
                $scope.remarkscommunication = true;
            }
            else {
                $scope.remarkscommunication = false;
            }


            $scope.servicerequest_gid = servicerequest_gid;

            var param = {
                servicerequest_gid: servicerequest_gid,

            }
            var url = "api/OsdTrnMyTicket/GetAllotted360";
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.request_refno = resp.data.request_refno;
                $scope.raised_by = resp.data.raised_by;
                $scope.raised_date = resp.data.raised_date;
                $scope.raised_department = resp.data.raised_department;
                $scope.activity_name = resp.data.activity_name;
                $scope.request_title = resp.data.request_title;
                $scope.request_status = resp.data.request_status;
                $scope.request_description = resp.data.request_description;
                // $('#request_description').html(resp.data.request_description);
                $scope.alloteddocumentdtl = resp.data.alloteddocumentdtl;
                assigned_status = resp.data.assigned_status;
                $scope.transfer_flag = resp.data.transfer_flag;
                $scope.assigned_team = resp.data.assigned_team;
                $scope.assigned_member = resp.data.assigned_member;
                $scope.employee_mobileno = resp.data.employee_mobileno;
                $scope.level_one = resp.data.level_one;
                $scope.employee_number = resp.data.employee_number;
                $scope.baselocation_name = resp.data.baselocation_name;
                $scope.lblfilename = resp.data.filename;
                $scope.lblfilepath = resp.data.filepath;
                $scope.lblallotfilename = resp.data.allofilename;
                $scope.lblallotfilepath = resp.data.allofilepath;
            });

            var url = "api/OsdTrnMyTicket/GetRequestorlist"

            SocketService.getparams(url, param).then(function (resp) {
                $scope.requestorlist = resp.data.requestordtl;
                unlockUI();
            });

            var url = "api/OsdTrnTicketManagement/GetTransferMemberlist"

            SocketService.getparams(url, param).then(function (resp) {
                $scope.transferlistdtl = resp.data.transferlistdtl;
                unlockUI();
            });

              var url = "api/OsdTrnMyTicket/GetAssetDtls"

              SocketService.getparams(url, param).then(function (resp) {
                $scope.assetdetails = resp.data.assetdetails;
                $scope.employee_gid = resp.data.employee_gid;

            });

            var url = "api/OsdTrnMyTicket/GetApprovalDtls"
            SocketService.getparams(url, param).then(function (resp) {

                $scope.approvaldetails = resp.data.approvaldetails;
                $scope.employee_gid = resp.data.employee_gid;
                //if ($scope.approvaldetails != null) {
                //    var getapproval = $scope.approvaldetails.find(function (v) { return v.approval_status === "Pending" });
                //    //var getpendingapplicant = $scope.individual_list.find(function (v) { return v.contact_status === "Pending" });
                //    //if ((getapplicant && getapplicant.contact_gid) && (getpendingapplicant == undefined || getpendingapplicant == "")) {
                //    if (getapproval && $scope.employee_gid != null) {
                //        $scope.tdshow = true;
                //    }
                //}

               
            });

            var params = {
                servicerequest_gid: servicerequest_gid
            }
            var url = 'api/OsdTrnServiceRequest/GetServiceRequestView';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.tagmemberdtl = resp.data.tagmemberdtl;
            });
            var url = 'api/OsdTrnMyTicket/GetBusinessUnitStatusMyActivityList';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.businessstatusunitmyactivity_list = resp.data.businessstatusunitmyactivity_list;

            });
            var url = 'api/OsdTrnMyTicket/GetservicerequestactivityhistoryList';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.servicerequestactivityhistory_list = resp.data.servicerequestactivityhistory_list;

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
                $scope.lblzonal_head = resp.data.zonal_head;
                $scope.lblbusiness_head = resp.data.business_head;
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

            var url = "api/OsdTrnRequestApproval/GetRHApprovalDtlsByToken"
            SocketService.getparams(url, param).then(function (resp) {
                $scope.rhapprovaldetails = resp.data.rhapprovaldetails;
                unlockUI();
            });
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
        $scope.ServiceRequestViewDocumentUpload = function () {
            lockUI();
            var fi = document.getElementById('file');
            if (fi.files.length > 0) {

                var frm = new FormData();
                for (var i = 0; i <= fi.files.length - 1; i++) {

                    frm.append(fi.files[i].name, fi.files[i])
                    $scope.uploadfrm = frm;
                    var fname = fi.files.item(i).name;
                    var fsize = fi.files.item(i).size;

                }

                frm.append('servicerequest_gid', servicerequest_gid);
                frm.append('project_flag', "Default");
                var url = 'api/OsdTrnServiceRequest/RequestViewDocUpload';
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                    $("#file").val('');
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
                Notify.alert('Please select a file.','info')
            }

        }
        $scope.Udownloadall = function (val1, val2) {

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
        $scope.ackActivity = function (relpath1) {
            var url = "api/OsdTrnMyTicket/PostUpdateAck";
            lockUI();
            var params = {
                servicerequest_gid: servicerequest_gid
            }
            SocketService.post(url, params).then(function (resp) {

                if (resp.data.status == true) {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });

                    $state.go('app.OsdTrnAllotedMyTicket');

                    activate();
                }
                else {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });

                    $state.go('app.OsdTrnAllotedMyTicket');

                    activate();
                }
            });
        }


        $scope.CancelApprovalPerson = function (approval_token) {

            var modalInstance = $modal.open({
                templateUrl: '/cancelmembermodal.html',
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
                            //$state.go('app.osdTrnMyActivityAllotted360');
                            var val5 = "N";
                            $location.url('app/osdTrnApprovalPending360?hash=' + cmnfunctionService.encryptURL('servicerequest_gid=' + servicerequest_gid + '&bankalert_flag=' + bankalert_flag + '&bankalert2allocated_gid=' + bankalert2allocated_gid + '&customer_gid=' + customer_gid + '&RequestCompletedFlag=' + val5 + '&lspage=Alloted' + '&request_refno=' + request_refno));
                            activate();
                        }
                        else {
                            modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                            //$state.go('app.osdTrnMyActivityAllotted360');
                            $location.url('app/osdTrnApprovalPending360?hash=' + cmnfunctionService.encryptURL('servicerequest_gid=' + servicerequest_gid + '&bankalert_flag=' + bankalert_flag + '&bankalert2allocated_gid=' + bankalert2allocated_gid + '&customer_gid=' + customer_gid + '&RequestCompletedFlag=' + val5 + '&lspage=Alloted' + '&request_refno=' + request_refno));
                            activate();

                        }
                    });
                }
            }
        }


        //lockUI();
        //var url = "api/OsdTrnRequestApproval/PostRequestRejected";
        //SocketService.post(url, params).then(function (resp) {
        //    unlockUI();
        //    if (resp.data.status == true) {

        //        Notify.alert(resp.data.message, {
        //            status: 'success',
        //            pos: 'top-center',
        //            timeout: 3000
        //        });
        //        unlockUI();
        //        $scope.showapproval = false;
        //        $scope.hideapproval = false;
        //    }
        //    else {

        //        Notify.alert(resp.data.message, {
        //            status: 'danger',
        //            pos: 'top-center',
        //            timeout: 3000
        //        });
        //        unlockUI();

        //    }
        //});


        $scope.back = function () {

            $state.go('app.OsdTrnApprovalMyTicket');


        }

        $scope.cancelclick = function () {
            $scope.txtqueries = "";
        }

        $scope.RejectActivity = function () {

            var modalInstance = $modal.open({
                templateUrl: '/rejectrequestmodal.html',
                controller: ModalInstanceCtrl,
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
                            modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();

                            $state.go('app.OsdTrnAllotedMyTicket');


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
            }
        }

        // Tagmember in chat Code Starts
        $scope.TagMemberChat = function (relpath1) {
            var modalInstance = $modal.open({
                templateUrl: '/tagmemberinchat.html',
                controller: ModalInstanceCtrl
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