(function () {
    'use strict';

    angular
        .module('angle')
        .controller('sanctionEditcontroller', sanctionEditcontroller);

    sanctionEditcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function sanctionEditcontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'sanctionEditcontroller';

        activate();

        function activate() {
            $scope.documentationEdit = [];

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
            lockUI();
            var url = 'api/customer/customer';
            SocketService.get(url).then(function (resp) {
                $scope.customer_list = resp.data.customer_list;
            });

            var url = "api/documentation/getdocumentationdtlList";
            SocketService.get(url).then(function (resp) {
                $scope.documentationdtl = resp.data.documentationdtl;
            });

            var url = 'api/newServiceTicket/employee';
            SocketService.get(url).then(function (resp) {
                $scope.employee_list = resp.data.employee_list;
            });
            var params = {
                customer2sanction_gid: localStorage.getItem('customer2sanction_gid')
            }
        
            var url = 'api/sanction/getsanctiondetails';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.customer_gid = resp.data.customer_gid;
                $scope.txteditsanctionRefNo = resp.data.sanction_refno;
                $scope.txteditSanctionDate = resp.data.sanctionDate;
                $scope.txteditSanctionAmount = resp.data.sanction_amount;
                $scope.txteditSanctionLimit = resp.data.sanction_limit;
                $scope.txtEditValidity = resp.data.sanction_validity;
                $scope.txtEditExpiryDate = resp.data.expiry_date;
                $scope.txtEditReviewDate = resp.data.review_date;
                $scope.cboeditapproval_authority = resp.data.approval_authority;
                $scope.cboEditnature_proposal = resp.data.natureof_proposal;
                $scope.cboEditconstitution = resp.data.constitution;
                $scope.cboEditauthorizedsignatory = resp.data.authorized_signatory;
                $scope.txtEditRevisiedLimit = resp.data.revisied_limit;
                $scope.txtEditexistingLimit = resp.data.existing_limit;
                $scope.cboEditEscrowAccount = resp.data.escrow_account;
                $scope.txtEditspecificcondition = resp.data.specific_conditions;
                $scope.cboEditfacilitytype = resp.data.facility_type;
                $scope.documentationEdit = resp.data.documentationname;
                $scope.txtEdittenure_months = resp.data.tenure_months;
                console.log(resp);
                
                $scope.documentationEditData = [];
                if (resp.data.documentationname!=null)
                {
                    var count = resp.data.documentationname.length;
                    for (var i = 0; i < count; i++) {

                        var indexs = $scope.documentationdtl.map(function (x) { return x.customer2document_gid; }).indexOf(resp.data.documentationname[i].customer2document_gid); 
                        $scope.documentationEditData.push($scope.documentationdtl[indexs]);
                    }
                }
               
            });


            unlockUI();
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
            $scope.txteditSanctionAmount = output;
           
        }

        $scope.sanctionlimitchange = function () {
            var input = document.getElementById('txtSanctionLimit').value;
            var arr = input.split(',');
            var i;
            for (i = 0; i < arr.length; i++) {

                var str = input.replace(',', '');
                input = str;
            }
            var output = Number(str).toLocaleString('en-US');
            $scope.txteditSanctionLimit = output;

        }

        $scope.revisedlimitchange = function () {
            var input = document.getElementById('txtRevisedLimit').value;
            var arr = input.split(',');
            var i;
            for (i = 0; i < arr.length; i++) {

                var str = input.replace(',', '');
                input = str;
            }
            var output = Number(str).toLocaleString('en-US');
            $scope.txtEditRevisiedLimit = output;

        }

        $scope.existinglimitchange = function () {
            var input = document.getElementById('txtExistingLimit').value;
            var arr = input.split(',');
            var i;
            for (i = 0; i < arr.length; i++) {

                var str = input.replace(',', '');
                input = str;
            }
            var output = Number(str).toLocaleString('en-US');
            $scope.txtEditexistingLimit = output;

        }

        $scope.sanctionformUpdate = function () {
            var input = $scope.txteditSanctionAmount;
            var arr = input.split(',');
            var i;
            for (i = 0; i < arr.length; i++) {
                var str = input.replace(',', '');
                input = str;
            }
            lockUI();
            var params = {
                customer2sanction_gid: localStorage.getItem('customer2sanction_gid'),
                customer_gid: $scope.customer_gid,
                sanction_refno: $scope.txteditsanctionRefNo,
                sanction_date: $scope.txteditSanctionDate,
                sanction_amount: input,
                sanction_limit: $scope.txteditSanctionLimit,
                sanction_validity: $scope.txtEditValidity,
                expiry_date: $scope.txtEditExpiryDate,
                review_date: $scope.txtEditReviewDate,
                approval_authority: $scope.cboeditapproval_authority,
                natureof_proposal: $scope.cboEditnature_proposal,
                constitution: $scope.cboEditconstitution,
                authorized_signatory: $scope.cboEditauthorizedsignatory,
                revisied_limit: $scope.txtEditRevisiedLimit,
                tenure_months: $scope.txtEdittenure_months,
                existing_limit: $scope.txtEditexistingLimit,
                escrow_account: $scope.cboEditEscrowAccount,
                specific_conditions: $scope.txtEditspecificcondition,
                facility_type: $scope.cboEditfacilitytype,
                documentationname: $scope.documentationEditData
            }

            var url = "api/sanction/postsanctionupdate";
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
