(function () {
    'use strict';

    angular
        .module('angle')
        .controller('idasTrnCadDashboardController', idasTrnCadDashboardController);

        idasTrnCadDashboardController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function idasTrnCadDashboardController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'idasTrnCadDashboardController';
        lockUI(); 
        activate();
        function activate() { 

            var params = {               
                caddropdown : 'employee',
                from_date : '',
                to_date : ''
                }
            var url = 'api/IdasDashboard/GetCadDashboardSummary';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.caddashboard_list = resp.data.caddashboard_list;
                
            });
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
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };  
        }
        
        $scope.search = function () {    
            if ($scope.txtfrom_date == undefined || $scope.txtfrom_date == "") {
                var from_date = null;
                }
            else {
                var from_date1 = $scope.txtfrom_date;
                var from_date = new Date(from_date1.getTime() - (from_date1.getTimezoneOffset() * 60000))
                .toISOString()
                .split("T")[0];
                }    
            if ($scope.txtto_date == undefined || $scope.txtto_date == "") {
                var to_date = null;
                }
            else {
                var to_date1 = $scope.txtto_date;
                var to_date = new Date(to_date1.getTime() - (to_date1.getTimezoneOffset() * 60000))
                .toISOString()
                .split("T")[0];
                }     
            if (from_date <= to_date){
                var params = {               
                    caddropdown : $scope.cbocaddashboard,
                    from_date : from_date,
                    to_date : to_date
                    }
                var url = 'api/IdasDashboard/GetCadDashboardSummary';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    if($scope.cbocaddashboard == 'Customer master updation') {
                        $scope.caddashboard_list = resp.data.customerupdation_list;
                    }
                    else if ($scope.cbocaddashboard == 'Sanction Ref.No') {
                        $scope.caddashboard_list = resp.data.sanction_list;
                    }
                    else if ($scope.cbocaddashboard == 'Sanction Master updation') {
                        $scope.caddashboard_list = resp.data.sanctionupdation_list;
                    } 
                    else if ($scope.cbocaddashboard == 'LSA Maker') {
                        $scope.caddashboard_list = resp.data.lsamaker_list;
                    }
                    else if ($scope.cbocaddashboard == 'LSA Checker') {
                        $scope.caddashboard_list = resp.data.lsachecker_list;
                    }
                    else if ($scope.cbocaddashboard == 'Collateral Updation') {
                        $scope.caddashboard_list = resp.data.collateralUpdation_list;
                    } 
                    else if ($scope.cbocaddashboard == 'Deferral Stage updated') {
                        $scope.caddashboard_list = resp.data.deferralstage_list;
                    }
                    else if ($scope.cbocaddashboard == 'Deferral Checker Approval') {
                        $scope.caddashboard_list = resp.data.deferralapproval_list;
                    }
                    else if ($scope.cbocaddashboard == 'CAD Compliance Certificate generated') {
                        $scope.caddashboard_list = resp.data.cadcompliance_list;
                    } 
                    else if ($scope.cbocaddashboard == 'Document Tagged') {
                        $scope.caddashboard_list = resp.data.doctagged_list;
                    }
                    else if ($scope.cbocaddashboard == 'Deferrals Created') {
                        $scope.caddashboard_list = resp.data.deferralcreate_list;
                    }
                    else if ($scope.cbocaddashboard == 'Document Vetting Maker') {
                        $scope.caddashboard_list = resp.data.docvettingmaker_list;
                    }
                    else {
                        $scope.caddashboard_list = resp.data.docvettingchecker_list;
                    }    
                    $scope.showvalue=$scope.cbocaddashboard,
                    $scope.txtto_date="",
                    $scope.txtfrom_date=""
                    unlockUI();
                });
            }
           
            else{
                Notify.alert("Enter Date Format Correctly", {
                    status: 'warning',
                    pos: 'top-center',
                    timeout: 3000
                });   
                $scope.showvalue=$scope.cbocaddashboard,
                $scope.txtto_date="",
                $scope.txtfrom_date=""
            }
        }
    }
})();

