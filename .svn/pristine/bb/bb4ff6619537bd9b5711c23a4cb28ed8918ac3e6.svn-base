(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstHRLoanTenureEditController', MstHRLoanTenureEditController);

    MstHRLoanTenureEditController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','cmnfunctionService'];

    function MstHRLoanTenureEditController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstHRLoanTenureEditController';
        $scope.hrloantenure_gid = cmnfunctionService.decryptURL($location.search().hash).lshrloantenure_gid;
        var hrloantenure_gid = $scope.hrloantenure_gid;

        activate();
        lockUI();
        function activate() {
            

            var url = 'api/MstHRLoanTenure/GetHRLoanTenureDropDown';
            SocketService.get(url).then(function (resp) {
                $scope.typeofdocument_list = resp.data.typeofdocument_list;
                // $scope.hrdocumentseverity_list = resp.data.hrdocumentseverity_list;

            });

            var params = {
                hrloantenure_gid: hrloantenure_gid
            }
            var url = 'api/MstHRLoanTenure/EditHRLoanTenure';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtedittenure_name = resp.data.hrloantenure_name;
                $scope.cbohrdocument = resp.data.hrloantypeoffinancialassistance_gid;
                $scope.txttenurestart_date = resp.data.hrloantenurestart_date;
                // $scope.txttenureend_date = resp.data.hrloantenureend_date;
                $scope.txtlms_code = resp.data.lms_code;
                $scope.txtbureau_code = resp.data.bureau_code;                
                $scope.Status = resp.data.Status;
                unlockUI();
            });

           
        }
       

        $scope.ok = function () {
            $modalInstance.close('closed');
        };
      
        $scope.Back = function () {
            $state.go('app.MstHRLoanTenure');

        }
        $scope.update = function () {

            var hrloantypeoffinancialassistance_name = $('#Document :selected').text();
            // var hrloanseverity_name = $('#Severity :selected').text();

            lockUI();
            var url = 'api/MstHRLoanTenure/UpdateHRLoanTenure';
            var params = {

                hrloantenure_gid: hrloantenure_gid,
                hrloantenure_name: $scope.txtedittenure_name,
                hrloantypeoffinancialassistance_gid: $scope.cbohrdocument,
                hrloantypeoffinancialassistance_name: hrloantypeoffinancialassistance_name,
                hrloantenurestart_date: $scope.txttenurestart_date,
                // hrloantenureend_date: $scope.txttenureend_date,
                lms_code: $scope.txtlms_code,
                bureau_code: $scope.txtbureau_code,               
                Status: $scope.Status
            }
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $state.go('app.MstHRLoanTenure');
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
       
    }
  
})();
