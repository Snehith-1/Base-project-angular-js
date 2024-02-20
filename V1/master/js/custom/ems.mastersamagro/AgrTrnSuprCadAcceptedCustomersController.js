(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrTrnSuprCadAcceptedCustomersController', AgrTrnSuprCadAcceptedCustomersController);

    AgrTrnSuprCadAcceptedCustomersController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function AgrTrnSuprCadAcceptedCustomersController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrTrnSuprCadAcceptedCustomersController';

        lockUI();
        activate();
        function activate() {
            var url = 'api/AgrTrnSuprCAD/CADApplicationCount';
            SocketService.get(url).then(function (resp) {
                $scope.cadreview_count = resp.data.cadreview_count;
                $scope.sentbackcc_count = resp.data.sentbackcc_count;
                $scope.accept_count = resp.data.accept_count;
                $scope.backtounderwriting_count = resp.data.backtounderwriting_count;
                $scope.ccrejected_count = resp.data.ccrejected_count;
                $scope.lstotalcount = resp.data.lstotalcount;
            });

            var url = 'api/AgrTrnSuprCAD/GetCADAcceptedCustomerSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.Cadacceptedcustomer_list = resp.data.cadapplicationlist;
            });
        }

        $scope.view = function (val) {
            $location.url('app/AgrMstSuprCadApplicationView?application_gid=' + val + '&lspage=CADAcceptanceCustomers');
        }

        $scope.edit = function (val) {
            $location.url('app/AgrTrnSuprCADApplicationEdit?application_gid=' + val + '&lspage=CADAcceptanceCustomers');
        }

        $scope.pendincad_review = function () {
            $location.url('app/AgrTrnSuprPendingCADReview');
        }

        $scope.cadaccepted_customers = function () {
            $location.url('app/AgrTrnSuprCadAcceptedCustomers');
        }

        $scope.sendback_underwriting = function () {
            $location.url('app/AgrTrnSuprSentBackToUnderwriting');
        }

        $scope.sendback_cc = function () {
            $location.url('app/AgrTrnSuprSentBackToCC');
        }

        $scope.cc_rejected = function () {
            $location.url('app/AgrTrnSuprCCRejectedApplications');
        }

        $scope.assignment_view = function (application_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/assignmentdtl_view.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                  {
                      application_gid: application_gid
                  }
                var url = 'api/AgrTrnSuprCAD/GetAssignmentView';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.assignment_list = resp.data.assignment_list;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.download_doc = function (val1, val2) {
                    var phyPath = val1;
                    var relPath = phyPath.split("EMS");
                    var relpath1 = relPath[1].replace("\\", "/");
                    var hosts = window.location.host;
                    var prefix = location.protocol + "//";
                    var str = prefix.concat(hosts, relpath1);
                    var link = document.createElement("a");
                    link.download = val2;
                    var uri = str;
                    link.href = uri;
                    link.click();
                }


            }

        }

        $scope.reassign_application = function (val) {
            $location.url('app/AgrTrnSuprCADReassignApplication?application_gid=' + val);
        }

        $scope.assign_application = function (val) {
            $location.url('app/AgrTrnSuprCADGroupProcessAssign?application_gid=' + val + '&lspage=CADAcceptanceCustomers');
        }
    }
})();
