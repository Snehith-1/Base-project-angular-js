(function () {
    'use strict';

    angular
        .module('angle')
        .controller('idasMstsecurityEdit', idasMstsecurityEdit);

    idasMstsecurityEdit.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function idasMstsecurityEdit($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        var vm = this;
        vm.title = 'idasMstsecurityAdd';
        var sanction_gid;
        activate();

        function activate() {
            $scope.collateral_gid = localStorage.getItem('collateral_gid');

            
            vm.calenderEdit = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.openEdit = true;
            };
            vm.dateOptionsEdit = {
                formatYear: 'yy',
                startingDay: 1
            };

            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];

            var url = 'api/security/getSecuritytype';
            SocketService.get(url).then(function (resp) {
                $scope.security_data = resp.data.securitytype_list;
                console.log(resp.data.securitytype_list);
            });
            var params = {
                collateral_gid:$scope.collateral_gid
            }
            var url = 'api/IdasTrnLsaManagement/GetColletarl';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.security_type = resp.data.security_type;
                $scope.cbosecurity_type = resp.data.securitytype_gid;
                $scope.txtsecurity_description = resp.data.security_description;
                $scope.cboaccount_status = resp.data.account_status;
                $scope.collateralref_no = resp.data.collateralref_no;
                $scope.security_code = resp.data.security_code;
                $scope.collateral_gid = resp.data.collateral_gid;
                $scope.txtborrowercheque_no = resp.data.borrowercheque_no;
                $scope.txtborroweraccount_no = resp.data.borroweraccount_no;
                $scope.txtborrowertbank_name = resp.data.borrowertbank_name;
                $scope.txtborrowerdeviation = resp.data.borrowerdeviation;
                $scope.txtborrowerother_remarks = resp.data.borrowerother_remarks;
                $scope.txtguarantor_cheque = resp.data.guarantor_cheque;
                $scope.txtguarantor_acno = resp.data.guarantor_acno;
                $scope.txtguarantor_bankname = resp.data.guarantor_bankname;
                $scope.txtguarantor_deviation = resp.data.guarantor_deviation;
                $scope.txtpersonalguarantor_name = resp.data.personalguarantor_name;
                $scope.txtguarantor_panno = resp.data.guarantor_panno;
                $scope.txtcorporate_guarantee = resp.data.corporate_guarantee;
                $scope.txtpersonal_guarantee = resp.data.personal_guarantee;
                $scope.txtfd_bank_name = resp.data.fd_bank_name;
                $scope.txtfd_no = resp.data.fd_no;
                $scope.txtfd_expiry_date = new Date(resp.data.fdexpiry_date);
                $scope.txtauto_renewal = resp.data.auto_renewal;
                $scope.txtbankguarantee_bankname = resp.data.bankguarantee_bankname;
                $scope.txtbankguarantee_expirydate = new Date(resp.data.bankguarantee_expiry_date); 
                $scope.txtinsurancecompany_name = resp.data.insurancecompany_name;
                $scope.txtpolicy_no = resp.data.policy_no;
                $scope.txtpolicy_expiry_date = new Date(resp.data.policyexpiry_date);
                console.log(resp.data.securitytype_gid);
                console.log(resp.data.security_type);
            if (resp.data.security_type == 'UDC From Borrower') {
                $scope.borrower = false;
                $scope.guarantor = true;
                $scope.insurance = true;
                $scope.bank_guarantee = true;
                $scope.fixed_deposits = true;
                $scope.personal_guarantee = true;
                $scope.corporate_guarantee = true;
            }
           else if (resp.data.security_type == 'UDC From Guarantor') {
                $scope.guarantor = false;
                $scope.borrower = true;
                $scope.insurance = true;
                $scope.bank_guarantee = true;
                $scope.fixed_deposits = true;
                $scope.personal_guarantee = true;
                $scope.corporate_guarantee = true;
            }
           else if (resp.data.security_type == 'Personal Guarantee') {
                $scope.personal_guarantee = false;
                $scope.borrower = true;
                $scope.guarantor = true;
                $scope.insurance = true;
                $scope.bank_guarantee = true;
                $scope.fixed_deposits = true;
                $scope.corporate_guarantee = true;
            }
           else if (resp.data.security_type == 'Corporate Guarantee') {
                $scope.corporate_guarantee = false;
                $scope.borrower = true;
                $scope.guarantor = true;
                $scope.insurance = true;
                $scope.bank_guarantee = true;
                $scope.fixed_deposits = true;
                $scope.personal_guarantee = true;
            }
           else if (resp.data.security_type == 'Bank Guarantee') {
                $scope.bank_guarantee = false;
                $scope.borrower = true;
                $scope.guarantor = true;
                $scope.insurance = true;
                $scope.fixed_deposits = true;
                $scope.personal_guarantee = true;
                $scope.corporate_guarantee = true;
            }
           else if (resp.data.security_type == 'Fixed Deposits') {
                $scope.fixed_deposits = false;
                $scope.borrower = true;
                $scope.guarantor = true;
                $scope.insurance = true;
                $scope.bank_guarantee = true;
                $scope.personal_guarantee = true;
                $scope.corporate_guarantee = true;
            }
            else if (resp.data.security_type == 'Assignment of Insurance Policy') {
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
            });
        }

        $scope.securityback = function () {
            $state.go('app.lsaManagementadd');
        }

        $scope.show_additional = function () {
            var str = $('#security_type :selected').text();

            if (str.match(/UDC From Borrower/g) == 'UDC From Borrower') {
                $scope.borrower = false;
                $scope.guarantor = true;
                $scope.insurance = true;
                $scope.bank_guarantee = true;
                $scope.fixed_deposits = true;
                $scope.personal_guarantee = true;
                $scope.corporate_guarantee = true;
            }
            else if (str.match(/UDC From Guarantor/g) == 'UDC From Guarantor') {
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
        $scope.security_submit = function (lsacreate_gid) {
            var security_type = $('#security_type :selected').text();
           
            if (security_type.match(/Fixed Deposits/g) == 'Fixed Deposits') {
                //Fixed Deposits
               var fd_expiry_date = $scope.txtfd_expiry_date;

            }
            else {
                 var fd_expiry_date = null;
            }
            if (security_type.match(/Bank Guarantee/g) == 'Bank Guarantee') {
                ////Bank Guarantee
                var bankguarantee_expirydate= $scope.txtbankguarantee_expirydate;
            }
            else
            {
                var bankguarantee_expirydate = null;
            }
            if (security_type.match(/Assignment of Insurance Policy/g) == 'Assignment of Insurance Policy') {

                ////Insurance Policy
                var policy_expiry_date = $scope.txtpolicy_expiry_date;
            }
            else {
                var policy_expiry_date = null;
            }
           
            var params = {
                security_type: security_type,
                securitytype_gid: $scope.cbosecurity_type,
                account_status: $scope.cboaccount_status,
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
                fd_no: $scope.txtguarantor_bankname,
                fd_expiry_date: fd_expiry_date,
                auto_renewal: $scope.txtguarantor_deviation,
                bankguarantee_bankname: $scope.txtbankguarantee_bankname,
                bankguarantee_expirydate: bankguarantee_expirydate,
                insurancecompany_name: $scope.txtinsurancecompany_name,
                policy_no: $scope.txtpolicy_no,
                policy_expiry_date: policy_expiry_date,
                security_description: $scope.txtsecurity_description,
                collateral_gid:$scope.collateral_gid
           
            }
            console.log(params);
            var url = 'api/IdasTrnLsaManagement/updatesecurityinfo';
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
