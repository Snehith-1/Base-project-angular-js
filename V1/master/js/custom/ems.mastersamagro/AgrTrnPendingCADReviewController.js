(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrTrnPendingCADReviewController', AgrTrnPendingCADReviewController);

        AgrTrnPendingCADReviewController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function AgrTrnPendingCADReviewController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrTrnPendingCADReviewController';

        activate();
        //lockUI();
        function activate() {
            lockUI();
            var url = 'api/AgrTrnCAD/GetPendingCADReviewSummary';
            SocketService.get(url).then(function (resp) {

                if (resp.data.cadapplicationlist != null && resp.data.cadapplicationlist.length > 0) {
                    $scope.Pendingcadreview_list = resp.data.cadapplicationlist;
                    unlockUI();
                }
                else if (resp.data.status == false)
                    unlockUI();
            });
            var url = 'api/AgrTrnCAD/CADApplicationCount';
            SocketService.get(url).then(function (resp) {
                //unlockUI();
                $scope.cadreview_count = resp.data.cadreview_count;
                $scope.sentbackcc_count = resp.data.sentbackcc_count;
                $scope.accept_count = resp.data.accept_count;
                $scope.backtounderwriting_count = resp.data.backtounderwriting_count;
                $scope.ccrejected_count = resp.data.ccrejected_count;
                $scope.advancerejected_count = resp.data.advancerejected_count;
                $scope.lstotalcount = resp.data.lstotalcount;
            });
        }

        $scope.view = function (val) {
            $location.url('app/AgrMstCadApplicationView?application_gid=' + val + '&lspage=pendingcadreview');
        }

        $scope.edit = function (val, val1, val2) {
            $location.url('app/AgrTrnCADApplicationEdit?application_gid=' + val + '&lspage=PendingCADReview' + '&product_gid=' + val1 + '&variety_gid=' + val2);
        }

        //$scope.process = function (val) {
        //    $location.url('app/MstApplicationCreationView?application_gid=' + val + '&lstab=MyApplications');
        //}

        $scope.pendincad_review = function () {
            $location.url('app/AgrTrnPendingCADReview');
        }

        $scope.cadaccepted_customers = function () {
            $location.url('app/AgrTrnCadAcceptedCustomers');
        }

        $scope.sendback_underwriting = function () {
            $location.url('app/AgrTrnSentBackToUnderwriting');
        }

        $scope.sendback_cc = function () {
            $location.url('app/AgrTrnSentBackToCC');
        }

        $scope.cc_rejected = function () {
            $location.url('app/AgrTrnCCRejectedApplications');
        }

        $scope.process = function (val) {
            $location.url('app/AgrTrnCADGroupProcessAssign?application_gid=' + val + '&lspage=PendingCADReview');
        } 

        $scope.Advance_rejected = function () {
            $location.url('app/AgrTrnPmgAdvanceRejectedSummary');
        }
        $scope.history = function (application_gid) {
            $location.url('app/AgrTrnSentbackCadtoCcHistory?application_gid=' + application_gid + '&lspage=CADPendingSummary');
        }

        $scope.shortclosing_creditapproval = function (application_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/BuyerShortClosingPopup.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var param = {
                    application_gid: application_gid
                };

                var url = 'api/AgrTrnApplicationApproval/Getapplicationdetails';
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.shortclosing_reason = resp.data.shortclosing_reason;
                    $scope.expired_flag = resp.data.expired_flag;
                });
                $scope.submitcredit_shortclosing = function () {
                    lockUI();
                    var params = {
                        application_gid: application_gid
                      
                    }
                    var url = 'api/AgrTrnCreditApproval/ShortClosingUpdate';
                    SocketService.getparams(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                            activate();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'info',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                    });
                }

                $scope.ok = function () {
                    $modalInstance.close('closed');
                    activate();
                };
            }

        }

    }
})();
