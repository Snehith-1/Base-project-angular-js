(function () {
    'use strict';

    angular
        .module('angle')
        .controller('osdBamAllocatedViewController', osdBamAllocatedViewController);
    osdBamAllocatedViewController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce', 'DownloaddocumentService','cmnfunctionService'];

    function osdBamAllocatedViewController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce, DownloaddocumentService,cmnfunctionService) {
        var vm = this;
        vm.title = 'osdBamAllocatedViewController';
        // var searchObject = cmnfunctionService.decryptURL($location.search() .hash);
        var searchObject = $location.search();
        var bankalert2allocated_gid = searchObject.lsbankalert2allocated_gid;
        var customer_gid = searchObject.lscustomer_gid;
        var customer_urn = searchObject.lscustomer_urn;
        var lstab = searchObject.lstab;
        activate();
        function activate() {
            $scope.trustAsHtml = function (string) { return $sce.trustAsHtml(string); };
            $scope.lspagename = lstab;
           //var url = 'api/OsdTrnBankAlert/GetAllocatedDtl';
           // var param = {
           //     bankalert2allocated_gid: bankalert2allocated_gid,
           //     customer_gid: customer_gid
            // }
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
                $scope.lbltransfer_flag = resp.data.transfer_flag;
                $scope.lbltransfer_type = resp.data.transfer_type;
                $scope.bankalert2allocated_gid = bankalert2allocated_gid;
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
                $scope.transferto_name = resp.data.transferto_name;
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
                }
            });
            var url = 'api/OsdTrnBankAlert/GetticketTransferLog'
            var param = {
                bankalert2allocated_gid: bankalert2allocated_gid,
            }
            SocketService.getparams(url, param).then(function (resp) {
                $scope.relationshipmanager_name = resp.data.relationshipmanager_name;
                $scope.transferedinitiated_by = resp.data.transferedinitiated_by;
                $scope.transferinitiated_date = resp.data.transferinitiated_date;
                $scope.transfer_type = resp.data.transfer_type;
                $scope.transfer_remarks = resp.data.transfer_remarks;
            });
            var url = 'api/OsdTrnBankAlert/GetCustomer2RM'
            SocketService.getparams(url, param).then(function (resp) {
                $scope.lblrmupdated_flag = resp.data.rmupdated_flag;
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
        $scope.initiate_transfer = function (bankalert2allocated_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/initiatetransfer.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                var url = "api/employee/Employee";
                SocketService.get(url).then(function (resp) {
                    $scope.employee_list = resp.data.employee_list;
                });
                $scope.transfer_submit = function () {

                    var params = {
                        bankalert2allocated_gid: bankalert2allocated_gid,
                        assigned_to: $scope.cboassign_to,
                        assigned_toname: $('#cboassign_to :selected').text(),
                        }
                     var url = 'api/OsdTrnBankAlert/Posttranasfe2Assign';

                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                            $location.url('app/OsdTrnBankAlertManagementSummary?hash=' + cmnfunctionService.encryptURL('lstab=' + lstab));
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                    });
                }
            }
        }
        $scope.rm_update = function (bankalert2allocated_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/rmtransfer.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                var url = 'api/OsdTrnBankAlert/GetCustomer2RM';
                var params = {
                    bankalert2allocated_gid: bankalert2allocated_gid
                }
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.lscustomerRM_name = resp.data.relationshipmanager_name;
                    $scope.lsbankRM_name = resp.data.lsrelationshipmanager_name;
                });
                $scope.RM_submit = function () {

                    var params = {
                        bankalert2allocated_gid: bankalert2allocated_gid,
                       }
                    var url = 'api/OsdTrnBankAlert/PostRM';

                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                            $location.url('app/OsdTrnBankAlertManagementSummary?hash=' + cmnfunctionService.encryptURL('lstab=' + lstab));
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                    });
                }
            }
        }

        $scope.Back = function () {
            $location.url("app/OsdTrnBankAlertManagementSummary?lstab=" + lstab);
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
    }
})();
