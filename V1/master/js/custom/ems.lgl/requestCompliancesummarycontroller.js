(function () {
    'use strict';

    angular
        .module('angle')
        .controller('requestCompliancesummarycontroller', requestCompliancesummarycontroller);

    requestCompliancesummarycontroller.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$filter', 'ngTableParams', '$resource', '$timeout', 'ngTableDataService'];

    function requestCompliancesummarycontroller($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $filter, ngTableParams, $resource, $timeout, ngTableDataService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'requestCompliancesummarycontroller';

        activate();


        function activate() {
            $scope.totalDisplayedcompliance = 100;
            $scope.totalDisplayedlegalsr = 100;
            var url = 'api/requestCompliance/requestCompliancesummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();

                $scope.requestcompliance_data = resp.data.requestcompliance_list;
                $scope.complianceCount = $scope.requestcompliance_data.length;
            });
            var url = 'api/raiseLegalSR/GetSR';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.mdlMisdataimport = resp.data.RaiselegalSR_list;
                $scope.legalsrCount = $scope.mdlMisdataimport.length;
            });
            $scope.tab = {};
            var url = window.location.href;
            var relPath = url.split("lstab=");
            var relpath1 = relPath[1];
            if (relpath1 != undefined) {
                if (relpath1 == "compliance") {
                    $scope.tabcompliance = true;
                }
                else if (relpath1 == "legalsr") {
                    $scope.tablegalsr = true;
                }
            }
            else {
                if ($scope.tab.activeTabId == undefined) {
                    $scope.tabcompliance = true;
                }
                else if ($scope.tab.activeTabId == 'compliance') {
                    $scope.tabcompliance = true;

                }
                else if ($scope.tab.activeTabId == 'legalsr') {
                    $scope.tablegalsr = true;
                }            

            }
        }
       
        $scope.loadMorecompliance = function (pagecount) {
            lockUI();
            var Number = parseInt(pagecount);

            $scope.totalDisplayedcompliance += Number;
            unlockUI();
        };
        $scope.loadMorelegalsr = function (pagecount) {
            lockUI();
            var Number = parseInt(pagecount);

            $scope.totalDisplayedlegals += Number;
            unlockUI();
        };
        //---Add Request Compliance-----//
        $scope.popuprequest = function () {

            $state.go('app.requestcompliance');

        }
        //---Add Raise Legal SR-----//
        $scope.raiselegalsr = function () {
            $location.url('app/raiselegalSR?lstab=legalsr');      
        }
        $scope.edit = function (val) {
            $scope.requestcompliance_gid = val;
            //$location.url('app/raiseSR2authentication?requestcompliance_gid=' + val + '&lstab=legalsr');
       
            $scope.requestcompliance_gid = localStorage.setItem('requestcompliance_gid', val);
            $state.go('app.editRequestcompliance');
            
        }
        $scope.view360 = function (val) {
            $scope.requestcompliance_gid = val;
            console.log($scope.requestcompliance_gid);
            //$location.url('app/raiseSR2authentication?requestcompliance_gid=' + val + '&lstab=legalsr');
            $scope.requestcompliance_gid = localStorage.setItem('requestcompliance_gid', val);
            $state.go('app.requestComplianceview');
        }

        $scope.delete = function (requestcompliance_gid) {
            var params = {
                requestcompliance_gid: requestcompliance_gid
            }
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do You Want To Delete the Record ?',

                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Yes, delete it!',
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    var url = 'api/requestCompliance/requestcompliancedelete';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                        }
                        else {
                            Notify.alert('Error Occurred !', {
                                status: 'warning',
                                pos: 'top-right',
                                timeout: 3000
                            });
                            activate();
                        }
                    });
                    SweetAlert.swal('Deleted Successfully!');
                }
            });
        };

        //--------------Action -legal SR--------------//
        $scope.raisesr = function (templegalsr_gid, customer_gid)
        {
           
            $location.url('app/raiseSR2authentication?lstemplegalsr_gid=' + templegalsr_gid + '&lscustomer_gid=' + customer_gid );

            
            //$scope.templegalsr_gid = localStorage.setItem('templegalsr_gid', templegalsr_gid);
            //$scope.customer_gid = localStorage.setItem('customer_gid', customer_gid);
            //$location.url('app/raiseSR2authentication?lstab=legalsr');
           
        }
    }
})();
