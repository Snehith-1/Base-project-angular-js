(function () {
    'use strict';

    angular
        .module('angle')
        .controller('caseAllocationcontroller', caseAllocationcontroller);

    caseAllocationcontroller.$inject = ['$rootScope', '$scope', '$state', '$cookies', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route','$modal'];

    function caseAllocationcontroller($rootScope, $scope, $state, $cookies, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, $route,$modal) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'caseAllocationcontroller';

        activate();

        function activate() {

            var url = 'api/caseAllocation/getExternalallocateddtl';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.allocationdtl = resp.data.allocationdtl;
               unlockUI();
            });
        }

        $scope.View = function (val, customer_gid) {
            localStorage.setItem('allocationdtl_gid', val);
            localStorage.setItem('customer_gid', customer_gid)
            $state.go('app.caseAllocationView');
        }

        $scope.reportCanceldtl = function (allocationdtl_gid, customer_gid, customername) {
            
            var modalInstance = $modal.open({
                templateUrl: '/reportCancelModal.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.customername = customername;
                $scope.proceedReportCancel = function () {
                    lockUI();
                     
                    var params = {
                        cancel_reason: $scope.txtcancel_reason,
                        allocationdtl_gid: allocationdtl_gid,
                        customer_gid: customer_gid
                    }
                    var url = "api/VisitReportCancel/PostCancelReport";
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                            $modalInstance.close('closed');
                            localStorage.setItem('allocationdtl_gid', allocationdtl_gid);
                            localStorage.setItem('allocation_customer_gid', customer_gid);

                            $state.go('app.externalvisitReportCancel');
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
            }
        }

        

        $scope.genereteallocation = function (allocationdtl_gid, customer_gid)
        {
            localStorage.setItem('allocationdtl_gid', allocationdtl_gid);
            localStorage.setItem('allocation_customer_gid', customer_gid);
            $state.go('app.externalvisitReportGenerate');
        }

        $scope.Viewgenereteddtl = function (allocationdtl_gid, customer_gid) {
            localStorage.setItem('allocationdtl_gid', allocationdtl_gid);
            localStorage.setItem('allocation_customer_gid', customer_gid);
            $state.go('app.externalvisitReportdetailView');
        }

        $scope.generetePDF = function (allocationdtl_gid) {

            lockUI();
            var params = {
                allocationdtl_gid: allocationdtl_gid
            };
            var url = 'api/zonalAllocation/visitReportpdfcontent';
            SocketService.getparams(url, params).then(function (resp) {
                var phyPath = resp.data;
                var relPath = phyPath.split("EMS");
                var relpath1 = relPath[1].replace("\\", "/");
                var hosts = window.location.host;
                var prefix = "http://"
                var str = prefix.concat(hosts, relpath1);
                var link = document.createElement("a");
                // var name = val2.split('.');
                link.download = "VisitReport";
                var uri = str;
                link.href = uri;
                link.click();
                Notify.alert('Visist Report Downloaded Successfully', 'success')
                unlockUI();
            });
           
        }
    }
})();
