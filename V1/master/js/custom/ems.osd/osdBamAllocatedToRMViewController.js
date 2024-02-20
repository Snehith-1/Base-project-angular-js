(function () {
    'use strict';

    angular
        .module('angle')
        .controller('osdBamAllocatedToRMViewController', osdBamAllocatedToRMViewController);
    osdBamAllocatedToRMViewController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce', 'DownloaddocumentService', 'cmnfunctionService'];

    function osdBamAllocatedToRMViewController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,$sce, DownloaddocumentService, cmnfunctionService) {
      {
        var vm = this;
        vm.title = 'osdBamAllocatedToRMViewController';
        //var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        //var bankalert2allocated_gid = searchObject.lsbankalert2allocated_gid;
        //var customer_gid = searchObject.lscustomer_gid;
        //var customer_urn = searchObject.lscustomer_urn;
        //    var lstab = searchObject.lstab;
        //$scope.lspage = searchObject.lspage;
        //var lspage = $scope.lspage;
            var searchObject = $location.search();
            var bankalert2allocated_gid = searchObject.lsbankalert2allocated_gid;
            var customer_gid = searchObject.lscustomer_gid;
            var customer_urn = searchObject.lscustomer_urn;
            var lstab = searchObject.lstab;
            $scope.lspage = searchObject.lspage;
            var lspage = $scope.lspage;
        activate();
        lockUI();
        function activate() {
            $scope.trustAsHtml = function (string) { return $sce.trustAsHtml(string); };
            var url = 'api/OsdTrnBankAlert/GetRMTempDelete';
           
            SocketService.get(url).then(function (resp) {
            });
            var url = 'api/OsdTrnBankAlert/GetAllocatedDetail';
            var param = {
                bankalert2allocated_gid: bankalert2allocated_gid,
                customer_gid: customer_gid,
                customer_urn: customer_urn,
              
            }
        
            SocketService.getparams(url,param).then(function (resp) {
                unlockUI();
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
                $scope.lblcustomer_urn = customer_urn;
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
                $scope.transfer_flag = resp.data.transfer_flag;
                $scope.employee_gid = resp.data.employee_gid;
                $scope.relationshipmanager_gid = resp.data.relationshipmanager_gid;
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
                unlockUI();
                if ($scope.lblallocated_status == 'Completed') {
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
                var param = {
                    servicerequest_gid: $scope.servicerequest_gid,
                }
                var url = "api/OsdTrnTicketManagement/GetTransferMemberlist"

                SocketService.getparams(url, param).then(function (resp) {
                    $scope.transferlistdtl = resp.data.transferlistdtl;
                    $scope.transferlistdtlreopen = resp.data.transferlistdtlreopen;
                    unlockUI();
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

            });
            var param = {
                bankalert2allocated_gid: bankalert2allocated_gid
            }
            var url = "api/OsdTrnRequestApproval/GetRHRejectedDtlsByToken"
            SocketService.getparams(url, param).then(function (resp) {
                $scope.rhrejecteddetails = resp.data.rhrejecteddetails;
                unlockUI();
            }); 
        }
        $scope.logdetails = function () {

            if ($scope.IsLogShow == true) {
                $scope.IsLogShow = false;
            }
            else {
                $scope.IsLogShow = true;
            }

        }

        $scope.Back = function () {
            if (lspage == 'Allocatedsummary') { 
                $location.url("app/osdBamAllocatedToRM?lstab=" + lstab);
            }
            else if (lspage == 'Completedsummary') {
                $state.go('app.OsdBamRMCompletedSummary');
            }           
           
        }

        //$scope.download = function (val1, val2) {
        //    var phyPath = val1;
            //var relPath = phyPath.split("EMS");
            //var relpath1 = relPath[1].replace("\\", "/");
            //var hosts = window.location.host;
            //var prefix = location.protocol + "//";
            //var str = prefix.concat(hosts, relpath1);
            //var link = document.createElement("a");
            //var name = val2.split(".")
            //link.download = val2;
            //var uri = str;
            //link.href = uri;
            //link.click();

        //$scope.download = function (val1, val2) {
        //        var relpath2 = "https://devsamunnati.blob.core.windows.net/" + val1;
        //        var link = document.createElement("a");
        //        link.href = relpath2;
        //        link.setAttribute('download', val2);
        //        link.click();
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
        $scope.RHdownloadall = function (val1, val2) {

            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }

        }
        $scope.Adownloadall = function (val1, val2) {

            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }

        }
        $scope.Cdownloadall = function (val1, val2) {

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
        //$scope.Submit = function () {
        //    $state.go('app.osdBamAllocatedToRM');
        //}
        $scope.uploadattachment = function (val, val1, name) {
            var fi = document.getElementById('addupload');
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
            }
            frm.append('project_flag', "Default");
            var url = 'api/OsdTrnBankAlert/RMDocumentUpload';
            lockUI();
            SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                $("#addupload").val('');
                if (resp.data.status == true) {
                   
                    var url = 'api/OsdTrnBankAlert/GetRMUploadedDoc';

                    SocketService.get(url).then(function (resp) {
                        $scope.uploaddocument = resp.data.MdlDocDetails;
                        $scope.lblfilename = resp.data.filename;
                        $scope.lblfilepath = resp.data.filepath;
                    });
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
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

        $scope.UploadDocCancel = function (id) {
            var params = {
                fileupload_gid: id
            }
             var url = 'api/OsdTrnBankAlert/DeleteRMUploadedDoc';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                 if (resp.data.status == true) {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    var url = 'api/OsdTrnBankAlert/GetRMUploadedDoc';
                    SocketService.get(url).then(function (resp) {
                        $scope.uploaddocument = resp.data.MdlDocDetails;
                        $scope.lblfilename = resp.data.filename;
                        $scope.lblfilepath = resp.data.filepath;
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
        $scope.uploadallocation = function (val, val1, name) {
            var item = {
                name: val[0].name,
                file: val[0]
            };
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "");

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
            frm.append('servicerequest_gid', $scope.servicerequest_gid);
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
                        servicerequest_gid: $scope.servicerequest_gid
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
        $scope.Submit=function()
        {
            var refund = $scope.cborm_status.replace("'","");
            if($scope.cborm_status == 'Adjust against loan outstanding'){
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
                var params =
                  {
                    bankalert2allocated_gid: bankalert2allocated_gid,
                    customer_gid: customer_gid,
                    rm_remarks: $scope.txtremarks,                   
                    //   rm_remarks: $scope.viewFroala,
                    rm_status: $scope.cborm_status
                  }
                var url = 'api/OsdTrnBankAlert/PostRMStatus';
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
                    $state.go('app.osdBamAllocatedToRM');
                });
            // }
            }
            else if(refund == 'Refund to clients bank account'){
                var params =
                  {
                    bankalert2allocated_gid: bankalert2allocated_gid,
                    customer_gid: customer_gid,
                    rm_remarks: $scope.txtremarks,                 
                    rm_status: $scope.cborm_status                    
                  }
                var url = 'api/OsdTrnBankAlert/PostRHapproval';
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
                    $state.go('app.osdBamAllocatedToRM');
                });
            }
            
        }
        $scope.sendrequestorclick = function () {
            var params = {
                servicerequest_gid: $scope.servicerequest_gid,
                remarks: $scope.txtqueries
            }
            lockUI();
            var url = "api/OsdTrnMyTicket/PostSendRequestor";
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    var url = "api/OsdTrnMyTicket/GetRequestorlist"
                    var param = {
                        servicerequest_gid: $scope.servicerequest_gid
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
})();
