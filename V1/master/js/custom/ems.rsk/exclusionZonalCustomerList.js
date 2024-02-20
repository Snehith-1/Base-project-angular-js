(function () {
    'use strict';

    angular
        .module('angle')
        .controller('exclusionZonalCustomerList', exclusionZonalCustomerList);

    exclusionZonalCustomerList.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams','DownloaddocumentService'];

    function exclusionZonalCustomerList($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams,DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'exclusionZonalCustomerList';

        activate();

        function activate() {
            lockUI();
            $scope.totalDisplayed = 100;
            var url = "api/zonalAllocation/GetExclusionZonalSummary";
            SocketService.get(url).then(function (resp) {
                $scope.exclusioncustomerList = resp.data.exclusioncustomer;
                if ($scope.exclusioncustomerList == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.exclusioncustomerList.length;
                    if ($scope.exclusioncustomerList.length < 100) {
                        $scope.totalDisplayed = $scope.exclusioncustomerList.length;
                    }
                }
                unlockUI();
            });
        }

        document.getElementById('pagecount').onkeyup = function () {

            if ($scope.pagecount == null) {
                var el = document.getElementById('loadmore');
                el.style.backgroundColor = '#DCDCDC';
            }
            else {
                var el = document.getElementById('loadmore');
                el.style.backgroundColor = '#ffa';
            }
        };

        $scope.loadMore = function (pagecount) {
            if (pagecount == undefined) {
                Notify.alert("Enter the Total Summary Count", "warning");
                return;
            }
            lockUI();
            var Number = parseInt(pagecount);
            $scope.totalDisplayed += Number;
            unlockUI();
        };
        $scope.export = function () {
            
            lockUI();
            var url = 'api/ExclusionList/GetExclusionZonalExport';

            SocketService.get(url).then(function (resp) {
               
                if (resp.data.status == true) {
                    unlockUI();
                    DownloaddocumentService.Downloaddocument(resp.data.excel_path, resp.data.excel_name);
                    // var phyPath = resp.data.excel_path;
                    // var relPath = phyPath.split("EMS");
                    // var relpath1 = relPath[1].replace("\\", "/");
                    // var hosts = window.location.host;
                    // var prefix = location.protocol + "//";
                    // var str = prefix.concat(hosts, relpath1);
                    // var link = document.createElement("a");
                    // var name = resp.data.excel_name.split('.');
                    // link.download = name[0];
                    // var uri = str;
                    // link.href = uri;
                    // link.click();

                }
                else {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }

            });
        }

        $scope.exclusionHistory = function (customer_urn, customername, qualified_status) {

            var modalInstance = $modal.open({
                templateUrl: '/exclusionHistoryPopup.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.customer_urn = customer_urn;
                $scope.customername = customername;
                $scope.customer_status = qualified_status;
                var params = {
                    customer_urn: customer_urn
                }
                var url = "api/zonalAllocation/GetExclusionCustomerHistory";
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.exclusioncustomerHistory = resp.data.exclusionhistory;

                });
            }
        }

        $scope.activateexclusion = function (customer_urn, customername, qualified_status) {

            var modalInstance = $modal.open({
                templateUrl: '/activateconfirmation.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.customer_urn = customer_urn;
                $scope.customername = customername;
                $scope.customer_status = qualified_status;
                $scope.confirmActivation = function () {
                    lockUI();
                    var params = {
                        customer_urn: customer_urn,
                        exclusion_reason: $scope.txtactivated_reason
                    }
                    var url = "api/zonalAllocation/GetActivationCustomer";
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                            $modalInstance.close('closed');
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

            }
        }
    }
})();
