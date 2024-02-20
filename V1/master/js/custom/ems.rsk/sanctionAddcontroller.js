(function () {
    'use strict';

    angular
        .module('angle')
        .controller('sanctionAddcontroller', sanctionAddcontroller);

    sanctionAddcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function sanctionAddcontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'sanctionAddcontroller';

        activate();

        function activate() {
            vm.calender1 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open1 = true;
            };

            vm.calender2 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open2 = true;
            };
            vm.calender3 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open3 = true;
            };
            vm.calender4 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open4 = true;
            };
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };

            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            var url = 'api/customer/customer';
            SocketService.get(url).then(function (resp) {
                $scope.customer_list = resp.data.customer_list;
            });

            var url = 'api/newServiceTicket/employee';
            SocketService.get(url).then(function (resp) {

                $scope.employee_list = resp.data.employee_list;
            });

            var url = "api/documentation/getdocumentationdtlList";
            SocketService.get(url).then(function (resp) {
                $scope.documentationdtl = resp.data.documentationdtl;
            });
        }


        $scope.amountschange = function () {
            var input = document.getElementById('txtInput').value;
            var arr = input.split(',');
            var i;
            for (i = 0; i < arr.length; i++) {
               
                var str = input.replace(',', '');
                input = str;
            }
            var output = Number(str).toLocaleString('en-US');
            $scope.txtSanctionAmount = output;
            //console.log(output);
        }

        $scope.sanctionlimitchange = function () {
            var input = document.getElementById('txt_SanctionLimit').value;
            var arr = input.split(',');
            var i;
            for (i = 0; i < arr.length; i++) {

                var str = input.replace(',', '');
                input = str;
            }
            var output = Number(str).toLocaleString('en-US');
            $scope.txtSanctionLimit = output;

        }

        $scope.revisedlimitchange = function () {
            var input = document.getElementById('txt_RevisedLimit').value;
            var arr = input.split(',');
            var i;
            for (i = 0; i < arr.length; i++) {

                var str = input.replace(',', '');
                input = str;
            }
            var output = Number(str).toLocaleString('en-US');
            $scope.txtRevisiedLimit = output;

        }

        $scope.existinglimitchange = function () {
            var input = document.getElementById('txt_ExistingLimit').value;
            var arr = input.split(',');
            var i;
            for (i = 0; i < arr.length; i++) {

                var str = input.replace(',', '');
                input = str;
            }
            var output = Number(str).toLocaleString('en-US');
            $scope.txtexistingLimit = output;

        }

        $scope.sanctionformSubmit = function () {
            var input = $scope.txtSanctionAmount;
            var arr = input.split(',');
            var i;
            for (i = 0; i < arr.length; i++) {
                var str = input.replace(',', '');
                input = str;
            }
           
            lockUI();
            var params = {
                customer_gid: $scope.cbocustomergid.customer_gid,
                sanction_refno: $scope.txtsanctionRefNo,
                sanction_date: $scope.txtSanctionDate,
                sanction_amount: input,
                sanction_limit: $scope.txtSanctionLimit,
                sanction_validity: $scope.txtValidity,
                expiry_date: $scope.txtExpiryDate,
                review_date: $scope.txtReviewDate,
                approval_authority: $scope.cboapproval_authority,
                natureof_proposal: $scope.cbonature_proposal,
                constitution: $scope.cboconstitution,
                authorized_signatory: $scope.cboauthorizedsignatory.employee_gid,
                tenure_months: $scope.txt_tenuremonths,
                revisied_limit: $scope.txtRevisiedLimit,
                existing_limit: $scope.txtexistingLimit,
                escrow_account: $scope.cboEscrowAccount,
                specific_conditions: $scope.txtspecificcondition,
                facility_type: $scope.cbofacilitytype,
                documentationname: $scope.cbodocumentationname
            }

            var url = "api/sanction/postsanctiondetails";
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                    $state.go('app.sanctionManagement');
                    activate();
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

        $scope.cancel = function () {
            $state.go('app.sanctionManagement');
        }
    }
})();
