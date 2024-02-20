(function () {
    'use strict';

    angular
        .module('angle')
        .controller('osdBamAssign2operationController', osdBamAssign2operationController);
    osdBamAssign2operationController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce', 'DownloaddocumentService', 'cmnfunctionService'];

    function osdBamAssign2operationController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce, DownloaddocumentService, cmnfunctionService) {
        var vm = this;
        vm.title = 'osdBamAssign2operationController';
        // var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        var searchObject = $location.search();
        var bankalert2allocated_gid = searchObject.lsbankalert2allocated_gid;
        var customer_gid = searchObject.lscustomer_gid;
        var customer_urn = searchObject.lscustomer_urn;
        var ticketref_no = searchObject.lsticketref_no;
        var lstab = searchObject.lstab;
        var lspage = searchObject.lspage;
        var compfilename, forwardfilename;
        var session_empgid;
        var compfilepath;
        var lblservicerequest_gid;
        activate();
        function activate() {

            $scope.trustAsHtml = function (string) { return $sce.trustAsHtml(string); }; 

            var url = 'api/OsdTrnBankAlert/GetEmployeeName';

            SocketService.get(url).then(function (resp) {
                $scope.cboemployee = resp.data.employee_gid;
                $scope.employee_name = resp.data.user_name;
                // session_empgid = resp.data.employee_gid;
                // assigned_remarks: $scope.txtremarks,
            });

            var url = 'api/OsdTrnBankAlert/GetOperationTempDelete';

            SocketService.get(url).then(function (resp) {
            });
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
                        // $('#lblassigned_remarks').html(resp.data.assigned_remarks);
                        $scope.lblassigned_date = resp.data.assigned_date;
                        $scope.lblassigned_toname = resp.data.assigned_toname;
                        $scope.lblassigned_by = resp.data.assigned_by;
                        $scope.document_list = resp.data.rmdocument_list;
                        $scope.lblrmfilename = resp.data.rmfilename;
                        $scope.lblrmfilepath = resp.data.rmfilepath;
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
                        $scope.lblcompfilename = resp.data.compfilename;
                        $scope.lblcompfilepath = resp.data.compfilepath;
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
                    banktransc_gid: ticketref_no,
                }

                var url = 'api/OsdTrnBankAlert/GetUnReconUploadedDoc';
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.uploaddocument = resp.data.MdlDocDetails;
                    $scope.lblfilename = resp.data.filename;
                    $scope.lblfilepath = resp.data.filepath;

                });
                if (resp.data.operation_status == 'Completed' || resp.data.operation_status == 'Assigned') {
                    var url = 'api/OsdTrnBankAlert/GetOperationStatusDtl';
                    var param = {
                        bankalert2allocated_gid: bankalert2allocated_gid,
                    }
                    SocketService.getparams(url, param).then(function (resp) {
                        $scope.lblassigned_remarks = resp.data.assigned_remarks;
                        // $('#lblassigned_remarks').html(resp.data.assigned_remarks);
                        $scope.lblassigned_date = resp.data.assigned_date;
                        $scope.lblassigned_toname = resp.data.assigned_toname;
                        $scope.lblassigned_by = resp.data.assigned_by;
                        $scope.document_list = resp.data.rmdocument_list;
                        $scope.lblrmfilename = resp.data.rmfilename;
                        $scope.lblrmfilepath = resp.data.rmfilepath;
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
                        $scope.lblcompfilename = resp.data.compfilename;
                        $scope.lblcompfilepath = resp.data.compfilepath;
                        unlockUI();
                    });

                } 
                 

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
            //var url = 'api/employee/Employee';
            //SocketService.get(url).then(function (resp) {
            //    $scope.employee_list = resp.data.employee_list;
            //});
            var url = 'api/OsdTrnBankAlert/Employee';
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


            // var param = {
            //     bankalertrefundapprl_gid: localStorage.getItem('bankalertrefundapprl_gid')
            // }
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
                banktransc_gid: ticketref_no
            }
            SocketService.getparams(url, param).then(function (resp) {
                $scope.assignedlist = resp.data.assignedlist;
            });

            var url = 'api/UnreconciliationManagement/GetTransferredHistory';
            var param = {
                banktransc_gid: ticketref_no
            }
            SocketService.getparams(url, param).then(function (resp) {
                $scope.transferlist = resp.data.transferlist;
            });
        }

        $scope.employee = function (cboemployee) {
            for (var i = 0; i < $scope.cboemployee.length; i++) {
                if (cboemployee == $scope.cboemployee[i].employee_gid)
                    $scope.cboemployee = $scope.cboemployee[i].employee_name.replace(" ", "").toLowerCase();

            }
        }
        $scope.AckComplete = function () {
           
           
            var modalInstance = $modal.open({
                templateUrl: '/Ackcompleterequestmodal.html',
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
                var url = 'api/OsdTrnBankAlert/GetEmployeeName';

                SocketService.get(url).then(function (resp) {

                    $scope.user_name = resp.data.user_name;
                    session_empgid = resp.data.employee_gid;
                    // assigned_remarks: $scope.txtremarks,
                });
              
                $scope.ackcompletedSubmit = function () {
                    var params =
                    {
                        bankalert2allocated_gid: bankalert2allocated_gid,
                        customer_gid: customer_gid,
                        assigned_to: session_empgid,
                        assigned_toname: $scope.user_name,
                         assigned_remarks:  $scope.completed_remarks,
                        //    assigned_remarks: $scope.viewFroala,
                    }
                    var url = 'api/OsdTrnBankAlert/Post2OperationTeam';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {

                        if (resp.data.status == true) {
                            var params =
                            {
                                bankalert2allocated_gid: bankalert2allocated_gid,
                            }
                            var url = 'api/OsdTrnBankAlert/GetServiceReqDtl';

                            SocketService.getparams(url,params).then(function (resp) {
                                lblservicerequest_gid = resp.data.servicerequest_gid;
                                if (lblservicerequest_gid != null || lblservicerequest_gid != "" || lblservicerequest_gid != undefined)
                                {
                                    var params = {
                                        completed_remarks: $scope.completed_remarks,
                                        servicerequest_gid: lblservicerequest_gid,

                                    }
                                    lockUI();
                                    var url = 'api/OsdTrnMyTicket/PostAckCompleteRequest';
                                    SocketService.post(url, params).then(function (resp) {
                                        if (resp.data.status == true) {
                                            modalInstance.close('closed');
                                            Notify.alert(resp.data.message, {
                                                status: 'success',
                                                pos: 'top-center',
                                                timeout: 3000
                                            });
                                            unlockUI();
                                            $state.go('app.OsdTrnBankAlertManagementSummary');
                                            // $location.url('app/OsdTrnBankAlertManagementSummary?hash=' + cmnfunctionService.encryptURL('lstab=' + lstab));
                                        }
                                        else {
                                            modalInstance.close('closed');
                                            Notify.alert(resp.data.message, {
                                                status: 'warning',
                                                pos: 'top-center',
                                                timeout: 3000
                                            });
                                            unlockUI();
                                            $state.go('app.OsdTrnBankAlertManagementSummary');
                                            // $location.url('app/OsdTrnBankAlertManagementSummary?hash=' + cmnfunctionService.encryptURL('lstab=' + lstab));

                                        }
                                    });
                                    unlockUI();
                                    Notify.alert(resp.data.message, {
                                        status: 'success',
                                        pos: 'top-center',
                                        timeout: 3000
                                    });
                                }
                            });
                            

                        }
                        else {
                            unlockUI();
                            Notify.alert(resp.data.message, {
                                status: 'info',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                       
                    });
                   
                }


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
          $scope.Back = function () {
            if (lspage == 'BamCompletedList') {
                $state.go('app.osdBamTicketCompletedSummary');
            } else {
              
                $location.url("app/OsdTrnBankAlertManagementSummary?lstab=" + lstab);

            }
             
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
        //$scope.download = function (val1, val2) {
        //var relpath2 = "https://devsamunnati.blob.core.windows.net/" + val1;
        //var link = document.createElement("a");
        //link.href = relpath2;
        //link.setAttribute('download', val2);
        //link.click();
        //}
        $scope.download = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
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

        $scope.udownloadall = function (val1, val2) {

            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }

        }
        $scope.COdownloadall = function (val1, val2) {

            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }

        }
        $scope.logdetails = function () {

            if ($scope.IsLogShow == true) {
                $scope.IsLogShow = false;
            }
            else {
                $scope.IsLogShow = true;
            }

        }
     
        $scope.UploadDocCancel = function (id) {
            var params = {
                fileupload_gid: id
            }
            var url = 'api/OsdTrnBankAlert/DeleteOperationUploadedDoc';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                    $scope.upload_list = resp.data.upload_list;
                    $scope.lblfilename = resp.data.doufilename;
                    $scope.lblfilepath = resp.data.doufilepath;
            });
        }
        $scope.ServiceRequestDocumentUpload = function () {
            
            var fi = document.getElementById('file');
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
                var url = 'api/OsdTrnServiceRequest/RequestDocumentUpload';
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                    $("#file").val('');
                    $scope.upload_list = resp.data.upload_list;
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
                Notify.alert('Please select a file.', 'info')
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
        //$scope.downloads = function (val1, val2) {
        //var relpath2 = "https://devsamunnati.blob.core.windows.net/" + val1;
        //var link = document.createElement("a");
        //link.href = relpath2;
        //link.setAttribute('download', val2);
        //link.click();
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
        $scope.rmdownloadall = function (val1, val2) {

            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }

        }
        $scope.Submit = function () {
            // var editor = new FroalaEditor('div#froala-editor', {}, function () { })
            // $scope.viewFroala = editor.html.get();
            // var remarks = $($scope.viewFroala).text();

            // if ((editor.core.isEmpty())) {
            //     Notify.alert('Enter Remarks', {
            //         status: 'warning',
            //         pos: 'top-center',
            //         timeout: 3000
            //     });
            // }
            // else {

            var assigned_toname = $('#employeeList :selected').text();
                var params =
                   {
                       bankalert2allocated_gid: bankalert2allocated_gid,
                       customer_gid: customer_gid,
                       assigned_to: $scope.cboemployee,
                    assigned_toname: assigned_toname,
                       assigned_remarks: $scope.txtremarks,
                    //    assigned_remarks: $scope.viewFroala,
                   }
                var url = 'api/OsdTrnBankAlert/Post2OperationTeam';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        unlockUI();
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });

                    }
                    else {
                        unlockUI();
                        Notify.alert(resp.data.message, {
                            status: 'info',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    $location.url('app/OsdTrnBankAlertManagementSummary?lstab=' + lstab);
                });
            // }

               
            
        }
        ////$scope.downloadsdocument = function (val1, val2) {
        ////    var phyPath = val1;
        ////    var relPath = phyPath.split("EMS");
        ////    var relpath1 = relPath[1].replace("\\", "/");
        ////    var hosts = window.location.host;
        ////    var prefix = location.protocol + "//";
        ////    var str = prefix.concat(hosts, relpath1);
        ////    var link = document.createElement("a");
        ////    var name = val2.split(".")
        ////    link.download = val2;
        ////    var uri = str;
        ////    link.href = uri;
        ////    link.click();
        ////}
      //$scope.downloadsdocument = function (val1, val2) {
      //  var relpath2 = "https://devsamunnati.blob.core.windows.net/" + val1;
      //  var link = document.createElement("a");
      //  link.href = relpath2;
      //  link.setAttribute('download', val2);
      //  link.click();
      //}
        $scope.downloadsdocument = function (val1, val2) {
          DownloaddocumentService.Downloaddocument(val1, val2);
      }
    }
})();
