(function () {
    'use strict';

    angular
        .module('angle')
        .controller('osdTrnRHApprovalViewController', osdTrnRHApprovalViewController);

    osdTrnRHApprovalViewController.$inject = ['$rootScope', '$scope', '$sce', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', '$cookies', '$filter', 'ngTableParams', '$timeout', 'DownloaddocumentService', 'cmnfunctionService'];

    function osdTrnRHApprovalViewController($rootScope, $scope, $sce, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, $cookies, $filter, ngTableParams, $timeout, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'osdTrnRHApprovalViewController';
        var url = window.location.href;
        var relPath = url.split("?id=");
        var relpath1 = relPath[1];
        activate();
        
        function activate() {
            var param = {
                bankalertrefundapprl_gid: localStorage.getItem('bankalertrefundapprl_gid')
            }            
            var url = 'api/OsdTrnRequestApproval/GetRHApprovaldetails';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                $scope.ref_no = resp.data.ref_no;
                $scope.customerurn = resp.data.customerurn;
                $scope.customername = resp.data.customername;
                $scope.assignedrmname = resp.data.assignedrmname;
                $scope.bankalertrefundapprl_gid = resp.data.bankalertrefundapprl_gid;
                unlockUI();
            });
            
            var param = {
                bankalert2allocated_gid: localStorage.getItem('bankalert2allocated_gid'),
                customer_gid: localStorage.getItem('customername'),
                customer_urn: localStorage.getItem('customerurn')
                             
            }
            var url = 'api/OsdTrnBankAlert/GetAllocatedDetail';        
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
                $scope.lblcustomer_urn = resp.data.customer_urnname;               
                $scope.lblzonal_head = resp.data.zonalhead_name;
                $scope.lblbusiness_head = resp.data.businesshead_name;               
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
            });
            var param = {
                bankalert2allocated_gid: localStorage.getItem('bankalert2allocated_gid'),
            }
            var url = "api/OsdTrnRequestApproval/GetRHApprovalDtlsByToken"
            SocketService.getparams(url, param).then(function (resp) {
                $scope.rhapprovaldetails = resp.data.rhapprovaldetails;
                unlockUI();
            });
            var param = {
                bankalert2allocated_gid: localStorage.getItem('bankalert2allocated_gid'),
            }
            var url = "api/OsdTrnRequestApproval/GetRHRejectedDtlsByToken"
            SocketService.getparams(url, param).then(function (resp) {
                $scope.rhrejecteddetails = resp.data.rhrejecteddetails;
                unlockUI();
            });
        }

        $scope.rh_approve = function () {
       
            var params = {

                rh_remarks: $scope.rh_txtremarks,
                bankalertrefundapprl_gid: $scope.bankalertrefundapprl_gid,                 
            }
            var url = 'api/OsdTrnRequestApproval/PostRHApprovalUpdate';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $state.go('app.myApproval');
                    activate();
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
     $scope.rh_reject = function () {
       
        var params = {

            rh_remarks: $scope.rh_txtremarks,
            bankalertrefundapprl_gid: $scope.bankalertrefundapprl_gid,                 
        }
        var url = 'api/OsdTrnRequestApproval/PostRHApprovalReject';
        lockUI();
        SocketService.post(url, params).then(function (resp) {
            unlockUI();
            if (resp.data.status == true) {

                Notify.alert(resp.data.message, {
                    status: 'success',
                    pos: 'top-center',
                    timeout: 3000
                });
                $state.go('app.myApproval');
                activate();
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

        $scope.back = function () {
            $state.go('app.myApproval');
        }

    }
})();
