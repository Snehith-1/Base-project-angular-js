(function () {
    'use strict';

    angular
        .module('angle')
        .controller('idasMstsecurityAdd', idasMstsecurityAdd);

    idasMstsecurityAdd.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function idasMstsecurityAdd($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        var vm = this;
        vm.title = 'idasMstsecurityAdd';
        var sanction_gid;
        activate();

        function activate() {
            $scope.lsacreate_gid = localStorage.getItem('lsacreate_gid');
        
         
            vm.mytime = new Date();
            vm.hstep = 1;
            vm.mstep = 15;
            vm.ismeridian = false;
            vm.calender1 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open1 = true;

            }
           

            var url = 'api/security/getSecuritytype';
            SocketService.get(url).then(function (resp) {
                $scope.security_data = resp.data.securitytype_list;
                console.log(resp.data.securitytype_list);
            });
            $scope.borrower = true;
            $scope.guarantor = true;
            $scope.insurance = true;
            $scope.bank_guarantee = true;
            $scope.fixed_deposits = true;
            $scope.personal_guarantee = true;
            $scope.corporate_guarantee = true;
        }

        $scope.sanctionback = function () {
            $state.go('app.lsaManagementadd');
        }

        $scope.show_additional = function () {
            var str = $('#security_type :selected').text();
           
            if ( str.match(/UDC From Borrower/g) == 'UDC From Borrower') {
                $scope.borrower = false;
                $scope.guarantor = true;
                $scope.insurance = true;
                $scope.bank_guarantee = true;
                $scope.fixed_deposits = true;
                $scope.personal_guarantee = true;
                $scope.corporate_guarantee = true;
            }
            
          else  if (str.match(/UDC From Guarantor/g) == 'UDC From Guarantor') {             
                $scope.guarantor = false;
                $scope.borrower = true;
                $scope.insurance = true;
                $scope.bank_guarantee = true;
                $scope.fixed_deposits = true;
                $scope.personal_guarantee = true;
                $scope.corporate_guarantee = true;
            }
           
           else if (str.match(/Personal Guarantee/g) == 'Personal Guarantee') {
                $scope.personal_guarantee = false;
                $scope.borrower = true;
                $scope.guarantor = true;
                $scope.insurance = true;
                $scope.bank_guarantee = true;
                $scope.fixed_deposits = true;
                $scope.corporate_guarantee = true;
            }
            
            else if (str.match(/Corporate Guarantee/g) == 'Corporate Guarantee') {
                $scope.corporate_guarantee = false;
                $scope.borrower = true;
                $scope.guarantor = true;
                $scope.insurance = true;
                $scope.bank_guarantee = true;
                $scope.fixed_deposits = true;
                $scope.personal_guarantee = true;
            }
            else if (str.match(/Bank Guarantee/g) == 'Bank Guarantee') {
                $scope.bank_guarantee = false;
                $scope.borrower = true;
                $scope.guarantor = true;
                $scope.insurance = true;
                $scope.fixed_deposits = true;
                $scope.personal_guarantee = true;
                $scope.corporate_guarantee = true;
            }
           
            else if (str.match(/Fixed Deposits/g) == 'Fixed Deposits') {
                $scope.fixed_deposits = false;
                $scope.borrower = true;
                $scope.guarantor = true;
                $scope.insurance = true;
                $scope.bank_guarantee = true;
                $scope.personal_guarantee = true;
                $scope.corporate_guarantee = true;
            }
           else if (str.match(/Assignment of Insurance Policy/g) == 'Assignment of Insurance Policy') {
                $scope.insurance = false;
                $scope.borrower = true;
                $scope.guarantor = true;
                $scope.bank_guarantee = true;
                $scope.fixed_deposits = true;
                $scope.personal_guarantee = true;
                $scope.corporate_guarantee = true;
            }
            else {
                $scope.borrower = true;
                $scope.guarantor = true;
                $scope.insurance = true;
                $scope.bank_guarantee = true;
                $scope.fixed_deposits = true;
                $scope.personal_guarantee = true;
                $scope.corporate_guarantee = true;
            }
        }
        $scope.security_submit=function(lsacreate_gid)
        {
            var security_type = $('#security_type :selected').text();
         
            if (security_type.match(/Fixed Deposits/g) == 'Fixed Deposits')
            {
                //Fixed Deposits
                var fd_expiry_date = new Date();
                fd_expiry_date.setFullYear($scope.txtfd_expiry_date.getFullYear());
                fd_expiry_date.setMonth($scope.txtfd_expiry_date.getMonth());
                fd_expiry_date.setDate($scope.txtfd_expiry_date.getDate());
            }
            else {
                fd_expiry_date = null;
            }
            if (security_type.match(/Bank Guarantee/g) == 'Bank Guarantee') {
                ////Bank Guarantee
             
                var bankguarantee_expirydate = new Date();
                bankguarantee_expirydate.setFullYear($scope.txtbankguarantee_expirydate.getFullYear());
                bankguarantee_expirydate.setMonth($scope.txtbankguarantee_expirydate.getMonth());
                bankguarantee_expirydate.setDate($scope.txtbankguarantee_expirydate.getDate());
                
            }
            else
            {
                bankguarantee_expirydate = null;
            }
            if (security_type.match(/Assignment of Insurance Policy/g) == 'Assignment of Insurance Policy') {

                ////Insurance Policy
                var policy_expiry_date = new Date();
                policy_expiry_date.setFullYear($scope.txtpolicy_expiry_date.getFullYear());
                policy_expiry_date.setMonth($scope.txtpolicy_expiry_date.getMonth());
                policy_expiry_date.setDate($scope.txtpolicy_expiry_date.getDate());
            }
            else {
                policy_expiry_date = null;
            }
            var params = {
                security_type:security_type,
                securitytype_gid: $scope.cbosecurity_type,
                account_status: $scope.account_status,
                borrowercheque_no: $scope.txtborrowercheque_no,
                borrowertbank_name: $scope.txtborrowertbank_name,
                borroweraccount_no: $scope.txtborroweraccount_no,
                borrowerdeviation: $scope.txtborrowerdeviation,
                borrowerother_remarks: $scope.txtborrowerother_remarks,
                guarantor_cheque: $scope.txtguarantor_cheque,
                guarantor_bankname: $scope.txtguarantor_bankname,
                guarantor_acno: $scope.txtguarantor_acno,
                guarantor_deviation: $scope.txtguarantor_deviation,
                personalguarantor_name: $scope.txtpersonalguarantor_name,
                guarantor_panno: $scope.txtguarantor_panno,
                corporate_guarantee: $scope.txtcorporate_guarantee,
                personal_guarantee: $scope.txtpersonal_guarantee,
                fd_bank_name: $scope.txtfd_bank_name,
                fd_no: $scope.txtfd_no,
                fd_expiry_date: fd_expiry_date,
                auto_renewal: $scope.rdbauto_renewal,
                bankguarantee_bankname: $scope.txtbankguarantee_bankname,
                bankguarantee_expirydate: bankguarantee_expirydate,
                insurancecompany_name: $scope.txtinsurancecompany_name,
                policy_no: $scope.txtpolicy_no,
               policy_expiry_date: policy_expiry_date,
                security_description: $scope.txtsecurity_description,
                lsacreate_gid: $scope.lsacreate_gid,
                lsacreate_gid1: lsacreate_gid

            }
            console.log(params);
            var url = 'api/IdasTrnLsaManagement/postsecurityinfo';
            SocketService.post(url, params).then(function (resp) {

                if (resp.data.status == true) {
                    $state.go('app.lsaManagementadd');
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });

                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'Warning',
                        pos: 'top-center',
                        timeout: 3000
                    });

                }

            });
        }
    }
})();
