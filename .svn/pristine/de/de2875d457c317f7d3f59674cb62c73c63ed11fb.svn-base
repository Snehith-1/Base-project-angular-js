(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstCreditorMasterViewController', AgrMstCreditorMasterViewController);

    AgrMstCreditorMasterViewController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService','cmnfunctionService'];

    function AgrMstCreditorMasterViewController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstCreditorMasterViewController';
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        $scope.creditor_gid = searchObject.creditor_gid;
        $scope.lspage = searchObject.lspage;
        var lspage = $scope.lspage;
        activate();
        //lockUI();
        function activate() {
            lockUI();

            if ($scope.lspage == 'AppRej') {
                $scope.enable = true;
                $scope.showdiv = true;
            }
            else {
                $scope.enable = false;
                $scope.showdiv = false;
            }

            if ($scope.lspage == 'AppV' || $scope.lspage == 'AppRM') {
                $scope.apprej = true;
            }
            else {
                $scope.apprej = false;
            }

            var params = {
                creditor_gid: $scope.creditor_gid
            };

            var url = 'api/AgrMstCreditorMaster/EditCreditorDetails';

            lockUI();
            SocketService.getparams(url, params).then(function (resp) {

                $scope.txtpan_number = resp.data.pan_no;
                $scope.txtapplicant_name = resp.data.Applicant_name;
                $scope.txtcreditor_code = resp.data.creditorref_no;
                $scope.cboapplicanttype_name = resp.data.Applicant_type;
                //$scope.cboProductTypelist = resp.data.loanproduct_name;
                $scope.applicant_category = resp.data.Applicant_category;
                //$scope.cboProductSubTypelist = resp.data.loansubproduct_name;
                //$scope.txteditwarehouse_area = resp.data.loanproduct_name;
                $scope.cbodesignation_type = resp.data.designation_type;
                //$scope.txtedittotalcapacity_area = resp.data.designation_type;
                $scope.txtcontactperson_name = resp.data.contactperson_name;
                $scope.txtcontact_number = resp.data.contact_no;
                $scope.txtmail_id = resp.data.email_id;
                $scope.txtaadhar_no = resp.data.aadhar_no;
                $scope.txtaadhar_no = resp.data.aadhar_no;
                if ($scope.txtaadhar_no.length < 12) {

                }
                else {
                    var aadhar = $scope.txtaadhar_no;
                    var mask = aadhar.slice(-4);
                    var maskaadhar = 'XXXX-XXXX-' + mask;
                    $scope.txtaadhar_no = maskaadhar;
                }

                unlockUI();
            });


            var url = 'api/AgrMstCreditorMaster/CreditorProductView';

            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.cboProductTypelist = resp.data.loanproduct_name;
            unlockUI();
            });


            var url = 'api/AgrMstCreditorMaster/CreditorProgramView';

            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.cboProductSubTypelist = resp.data.loansubproduct_name;
                unlockUI();
            });


            var url = 'api/AgrMstCreditorMaster/GetCreditorApproveRejectSummary';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                
                $scope.approved_date = resp.data.approved_date;
                $scope.approved_by = resp.data.approved_by;
                $scope.rejected_date = resp.data.rejected_date;
                $scope.rejected_by = resp.data.rejected_by;
                $scope.creditorapproval_status = resp.data.creditorapproval_status;
                $scope.approval_status = resp.data.approval_status;
                $scope.approval_remarks = resp.data.approval_remarks;

                if (resp.data.approval_status == 'Y') {
                    $scope.approve = true 
                    $scope.reject = false 

                }
                else if (resp.data.approval_status == 'R') {
                    $scope.approve = false
                    $scope.reject = true

                }

                else {
                    $scope.approve = false
                    $scope.reject = false

                }

                unlockUI();
            });

            var url = 'api/AgrMstCreditorMaster/creditorAddressTmpList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.creditoraddress_list = resp.data.creditoraddress_list;
            });

            var url = 'api/AgrMstCreditorMaster/creditorGSTTmpList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.gst_list = resp.data.creditorgst_list;
            });

            var url = 'api/AgrMstCreditorMaster/GetcreditorAgreementDetailsTmpList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.agreementdocumentaddress_list = resp.data.Mdlcreditoragreementdtllist;

            });

            var url = 'api/AgrMstCreditorMaster/GetChequeSummaryTmplist';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.cheque_list = resp.data.creditorcheque_list;

            });

            var url = 'api/AgrMstCreditorMaster/GetCreditorRaiseQuerySummary';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.byrraisequerylist = resp.data.creditorraisequerylist;

            });

            var url = 'api/AgrMstCreditorMaster/GetOpenQueryStatus';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.openquery_flag = resp.data.openquery_flag;
                if ($scope.openquery_flag == "Y") {
                    $scope.hideapproval = true;
                }

            });

        }


        $scope.Back = function () {    

            if (lspage == 'AppV') {
                $location.url('app/AgrMstCreditorMasterApproval');
        }
            else if (lspage == 'AppRej') {
                $location.url('app/AgrMstCreditorMasterApproval');
            }

            else if (lspage == 'AppRM') {
                $location.url('app/AgrMstCreditorMasterSummary');
            }
        else {
            $location.url('app/AgrMstCreditorMasterSummary');
        }
    }
       
        $scope.agreementdoc_upload = function (creditor2agreement_gid, creditor_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/defferal_docupload.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                //$scope.txtwarehousedocument_name = document_title;
                var params = {
                    creditor2agreement_gid: creditor2agreement_gid,
                }
                var url = 'api/AgrMstCreditorMaster/Getcreditordocument';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.warehouseuploaddocument_list = resp.data.creditoragreement_upload;

                });


                $scope.creditordocumentupload = function (val) {
                    if (($scope.txtcreditordocument_name == null) || ($scope.txtcreditordocument_name == '') || ($scope.txtcreditordocument_name == undefined)) {
                        $("#momdocument").val('');
                        Notify.alert('Kindly Enter the Document Title', 'warning');
                    } 
                    else {
                        var frm = new FormData();
                        for (var i = 0; i < val.length; i++) {
                            var item = {
                                name: val[i].name,
                                file: val[i]
                            };
                            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[i].name, "documentformatonly");
    
                            if (IsValidExtension == false) {
                                Notify.alert("File format is not supported..!", {
                                    status: 'danger',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                return false;
                            }
                            frm.append('fileupload', item.file);
                            frm.append('file_name', item.name);
                            frm.append('document_title', $scope.txtcreditordocument_name);
                            frm.append('creditor_gid', creditor_gid);
                            frm.append('creditor2agreement_gid', creditor2agreement_gid);
                            frm.append('project_flag', "documentformatonly");
                        }
                       
                        $scope.uploadfrm = frm;
                        if ($scope.uploadfrm != undefined) {
                            lockUI();
                            var url = 'api/AgrMstCreditorMaster/creditorDocumentUpload';
                            SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                                $scope.warehouseuploaddocument_list = resp.data.creditoragreement_upload;
                                unlockUI();

                                $("#institutionfile").val('');
                                $scope.uploadfrm = undefined;
                                $scope.txtcreditordocument_name = '';
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
                            alert('Document is not Available..!');
                            return;
                        }
                    }
                }

                $scope.institutiondocument_delete = function (creditoragreement2docupload_gid) {
                    lockUI();
                    var params = {
                        creditoragreement2docupload_gid: creditoragreement2docupload_gid,
                        creditor2agreement_gid: creditor2agreement_gid
                    }
                    var url = 'api/AgrMstCreditorMaster/creditordoc_delete';
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.warehouseuploaddocument_list = resp.data.creditoragreement_upload;
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

                $scope.download_doc = function (val1, val2) {

                    DownloaddocumentService.Downloaddocument(val1, val2);
                }

                $scope.ok = function () {
                    $modalInstance.close('closed');

                }
                $scope.downloadall_2 = function () {
                    for (var i = 0; i < $scope.warehouseuploaddocument_list.length; i++) {
                        DownloaddocumentService.Downloaddocument($scope.warehouseuploaddocument_list[i].document_path, $scope.warehouseuploaddocument_list[i].document_name);
                    }
                }

            }
        }

        $scope.download_doc = function (val1, val2) {

            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.cheque_view = function (creditor2cheque_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/cheque_view.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    creditor2cheque_gid: creditor2cheque_gid
                }
                var url = 'api/AgrMstCreditorMaster/ChequeDetailsEdit';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txtaccountholder_name = resp.data.accountholder_name;
                    $scope.txtaccount_number = resp.data.account_number;
                    $scope.txtbank_name = resp.data.bank_name;
                    $scope.txtcheque_no = resp.data.cheque_no;
                    $scope.txtifsc_code = resp.data.ifsc_code;
                    $scope.txtmicr = resp.data.micr;
                    $scope.txtbranch_address = resp.data.branch_address;
                    $scope.txtbranch_name = resp.data.branch_name;
                    $scope.txtcity = resp.data.city;
                    $scope.txtdistrict = resp.data.district;
                    $scope.txtstate = resp.data.state;
                    $scope.cbomergedbanking_entity = resp.data.mergedbankingentity_gid;
                    $scope.cbocheque_type = resp.data.cheque_type;
                    $scope.rdbprimarystatus = resp.data.primary_status;
                //    $scope.txtdate_chequetype = resp.data.datechequetype;
                //    //$scope.txtdate_chequetype = Date.parse($scope.txtdate_chequetype);

                //    $scope.rbocts_enabled = resp.data.cts_enabled;

                //    $scope.txtdate_chequepresentation = resp.data.datechequepresentation;
                //    //$scope.txtdate_chequepresentation = Date.parse($scope.txtdate_chequepresentation);

                //    $scope.txtstatus_chequepresentation = resp.data.status_chequepresentation;

                //    $scope.txtdate_chequeclearance = resp.data.datechequeclearance;
                //    //$scope.txtdate_chequeclearance = Date.parse($scope.txtdate_chequeclearance);

                //    $scope.txtstatus_chequeclearance = resp.data.status_chequeclearance;
                //    $scope.txtspecial_condition = resp.data.special_condition;
                //    $scope.txtgeneral_remarks = resp.data.general_remarks;
                //});

                    //$scope.txtdate_chequetype = resp.data.datechequetype;

                    if (resp.data.datechequetype == "0001-01-01T00:00:00") {

                        $scope.txtdate_chequetype = '';
                    }

                    else {
                        $scope.txtdate_chequetype = resp.data.datechequetype;

                    }
                    //$scope.txtdate_chequetype = Date.parse($scope.txtdate_chequetype);

                    $scope.rbocts_enabled = resp.data.cts_enabled;

                    if (resp.data.datechequepresentation == "0001-01-01T00:00:00") {

                        $scope.txtdate_chequepresentation = '';
                    }

                    else {

                        $scope.txtdate_chequepresentation = resp.data.datechequepresentation;

                    }
                    //$scope.txtdate_chequepresentation = Date.parse($scope.txtdate_chequepresentation);

                    $scope.txtstatus_chequepresentation = resp.data.status_chequepresentation;
                    if (resp.data.datechequeclearance == "0001-01-01T00:00:00") {

                        $scope.txtdate_chequeclearance = '';
                    }
                    else {
                        $scope.txtdate_chequeclearance = resp.data.datechequeclearance;

                    }


                    //$scope.txtdate_chequeclearance = Date.parse($scope.txtdate_chequeclearance);

                    $scope.txtstatus_chequeclearance = resp.data.status_chequeclearance;
                    $scope.txtspecial_condition = resp.data.special_condition;
                    $scope.txtgeneral_remarks = resp.data.general_remarks;
                });



                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

            }

        }


        $scope.approvecredit = function () {
            var params = {
                creditor_gid: $scope.creditor_gid,
                approval_status: 'Approved',
                approval_remarks: $scope.txtapproval_remarks
            }
            lockUI();
            var url = "api/AgrMstCreditorMaster/PostCreditorApproval";
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    //$location.url('app/AgrMstCustomerApprovalSummary?selectedIndex=0');
                    $location.url('app/AgrMstCreditorMasterApproval');
                }
                else {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
            });
        }

        $scope.rejectcredit = function () {
            var params = {
                creditor_gid: $scope.creditor_gid,
                approval_status: 'Rejected',
                approval_remarks: $scope.txtapproval_remarks
            }
            lockUI();
            var url = "api/AgrMstCreditorMaster/PostCreditorApproval";
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    //$location.url('app/AgrMstCustomerApprovalSummary?selectedIndex=0');
                    $location.url('app/AgrMstCreditorMasterApproval');
                }
                else {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
            });
        }

        $scope.downloadall = function () {
            for (var i = 0; i < $scope.cheque_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.cheque_list[i].document_path, $scope.cheque_list[i].document_name);
            }
        }
        $scope.downloadall_2 = function () {
            for (var i = 0; i < $scope.warehouseuploaddocument_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.warehouseuploaddocument_list[i].document_path, $scope.warehouseuploaddocument_list[i].document_name);
            }
        }


        $scope.Raise_Query = function () {
            $scope.showraisequery = true;
            $scope.showdiv = false;
        }
        $scope.Cancel = function () {
            $scope.txtquery_title = "";
            $scope.txtquery_desc = "";
            $scope.showraisequery = false;
            $scope.showdiv = true;
        }


        $scope.submit = function () {

            var params = {
                creditor_gid: $scope.creditor_gid,
                description: $scope.txtquery_desc,
                query_title: $scope.txtquery_title,

            }

            var url = 'api/AgrMstCreditorMaster/PostCreditorRaiseQuery';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $scope.txtquery_title = "";
                    $scope.txtquery_desc = "";
                    $scope.txtapproval_remarks = "";
                    creditorquery_list();
                    $scope.showraisequery = false;
                    $scope.showdiv = true;
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });

                }
            });

            //$modalInstance.close('closed');

        }


        function creditorquery_list() {

            var param = {
                creditor_gid: $scope.creditor_gid
            };

            var url = 'api/AgrMstCreditorMaster/GetCreditorRaiseQuerySummary';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.byrraisequerylist = resp.data.creditorraisequerylist;

            });

            var url = 'api/AgrMstCreditorMaster/GetOpenQueryStatus';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.openquery_flag = resp.data.openquery_flag;
                if ($scope.openquery_flag == "Y") {
                    $scope.hideapproval = true;
                }

            });

        }


        $scope.close_query = function (creditorquery_gid, creditor_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/queryClose.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                var params =
                {
                    creditorquery_gid: creditorquery_gid
                }
                var url = 'api/AgrMstCreditorMaster/GetRaiseQuerydesc';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.lblquery_title = resp.data.query_title;

                });

                $scope.submit = function () {
                    var params = {
                        creditorquery_gid: creditorquery_gid,
                        creditor_gid: creditor_gid,
                        close_remarks: $scope.txtcloseremarks
                    }
                    var url = 'api/AgrMstCreditorMaster/PostUpdateQueryStatus';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            creditorquery_list();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'info',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }

                    });

                    $modalInstance.close('closed');
                }

            }
        }



        $scope.view_querydesc = function (creditorquery_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/queryDescriptionView.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                {
                    creditorquery_gid: creditorquery_gid
                }
                var url = 'api/AgrMstCreditorMaster/GetRaiseQuerydesc';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.lblquery_desc = resp.data.description;

                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }


        $scope.view_queryremarks = function (creditorquery_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/queryremarks.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                {
                    creditorquery_gid: creditorquery_gid
                }
                var url = 'api/AgrMstCreditorMaster/GetRaiseQuerydesc';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.lblclose_remarks = resp.data.close_remarks;

                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }


    }

})();