(function () {
    'use strict';

    angular
        .module('angle')
        .controller('allocationHistorycontroller', allocationHistorycontroller);

    allocationHistorycontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$modal', '$http', 'SocketService', 'Notify', '$location', '$route', '$cookies', '$filter', 'ngTableParams', '$timeout','DownloaddocumentService'];

    function allocationHistorycontroller($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $modal, $http, SocketService, Notify, $location, $route, $cookies, $filter, ngTableParams, $timeout, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'allocationHistorycontroller';
        activate();

        function activate() {
            lockUI();
            $scope.MyZonalAllocationHistory = localStorage.getItem('MyZonalAllocationHistory');

            var customer_gid = {
                customer_gid: localStorage.getItem('allocation_customer_gid')
            }
            var url = "api/allocationManagement/getAllocationHistory";
            SocketService.getparams(url, customer_gid).then(function (resp) {
                if (resp.data.overallhistoryallocationdtl == null) {
                    if (localStorage.getItem('MyZonalAllocationHistory') == "Y") {
                        $state.go('app.allocationZonalRM');
                    }
                    else {
                        $state.go('app.caseAllocation');
                    }
                    alert('No History for this selected Customer');
                    return

                }
                else {
                    $scope.allocationHistoryList = resp.data.overallhistoryallocationdtl;
                    $scope.customername = resp.data.overallhistoryallocationdtl[0].customername;
                    $scope.customer_urn = resp.data.overallhistoryallocationdtl[0].customer_urn;
                }

            });
            unlockUI();
        }


        $scope.ViewCancelReason = function (allocationdtl_gid, customername, customer_urn, allocate_external) {

            var modalInstance = $modal.open({
                templateUrl: '/reportCancelModal.html',
                controller: ModalInstanceCtrl,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.customername = customername;
                $scope.customer_urn = customer_urn;
                var params = {
                    allocationdtl_gid: allocationdtl_gid
                }
                if (allocate_external == 'Y') {
                    var url = "api/VisitReportCancel/GetExternalVisitCancelLog";
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.visistreportcancel = resp.data.visistreportcancel;
                    });
                }
                else {
                    var url = "api/VisitReportCancel/GetVisitCancelLog";
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.visistreportcancel = resp.data.visistreportcancel;

                    });
                }

                //var url = "api/allocationManagement/GetViewCancelReason";
                //SocketService.getparams(url, params).then(function (resp) {

                //    $scope.cancel_remarks = resp.data.cancel_remarks;
                //    $scope.created_date = resp.data.created_date;
                //    $scope.created_by = resp.data.created_by;
                //});
                $scope.ok = function () {
                    $modalInstance.close('closed');
                    activate();
                };


            }
        }

        $scope.externalAssigned = function (allocationdtl_gid) {
            var params = {
                allocationdtl_gid: allocationdtl_gid
            }

            var modalInstance = $modal.open({
                templateUrl: '/AssignedExternal.html',
                controller: ModalInstanceCtrl,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                lockUI();
                var url = "api/allocationManagement/getExternalDetails";
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.customerdtl = resp.data.customerdtl;
                    $scope.externalname = resp.data.externalname;
                    $scope.AllocateExtRemarks = resp.data.requested_remarks;
                    $scope.assigned_by = resp.data.assigned_by;
                    $scope.assigned_date = resp.data.assigned_date;
                    $scope.target_date = resp.data.target_date;
                });

                var url = "api/allocationManagement/getExternaldocument";
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.upload_list = resp.data.upload_list;
                });
                unlockUI();

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.close = function () {
                    $modalInstance.close('closed');
                }

                $scope.downloads = function (val1, val2) {
                    DownloaddocumentService.Downloaddocument(val1, val2);
                }
            }
        }

        $scope.historyback = function () {
            if (localStorage.getItem('MyZonalAllocationHistory') == "Y") {
                $state.go('app.allocationZonalRM');
            }
            else {
                $state.go('app.caseAllocation');
            }
        }

        $scope.observationviewreport = function (allocationdtl_gid, observation_reportgid) {

            if (observation_reportgid !== "") {
                localStorage.setItem('allocationdtl_gid', allocationdtl_gid);
                localStorage.setItem('observation_reportgid', observation_reportgid);
                //$state.go('app.ZRMObservationReportView');
                $location.url('app/ZRMObservationReportView?allocationdtl_gid=' + allocationdtl_gid + '&?&observation_reportgid=' + observation_reportgid);
            }
            else {
                Notify.alert('Observation Report  Not Found..!', {
                    status: 'warning',
                    pos: 'top-center',
                    timeout: 3000
                });
                
            }
        }

        $scope.generetePDF = function (allocationdtl_gid) {

            lockUI();
            var params = {
                allocationdtl_gid: allocationdtl_gid
            };
            var url = 'api/zonalAllocation/visitReportpdfcontent';
            SocketService.getparams(url, params).then(function (resp) {


                if (resp.data.status == true) {
                    var filepath = resp.data.file_path;
                    var filename = resp.data.file_name;
                    DownloaddocumentService.Downloaddocument(filepath, filename);

                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert('Error Occurred While Export PDF !', 'warning');
                }

            });


        }

        $scope.viewallocation = function (allocationdtl_gid, customer_gid) {
            localStorage.setItem('allocationdtl_gid', allocationdtl_gid);
            localStorage.setItem('allocation_customer_gid', customer_gid);
            $state.go('app.allocationHistoryView');
        }

        $scope.genereteATRPDF = function (observation_reportgid) {

            if (observation_reportgid !== "") {
            var params = {
                observation_reportgid: observation_reportgid

            };
            var url = 'api/ObservationReport/ATRReportpdfcontent';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    var filepath = resp.data.file_path;
                    var filename = resp.data.file_name;
                    DownloaddocumentService.Downloaddocument(filepath, filename);
                }
                else {
                    unlockUI();
                    Notify.alert('Error Occurred While Export PDF !', 'warning');
                }
                unlockUI();
            });
        }
    else {
                Notify.alert('ATR PDF  Not Found..!', {
                    status: 'warning',
                    pos: 'top-center',
                    timeout: 3000
                });
                
    }

        }
    }
})();
