(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstPendingCADReviewController', MstPendingCADReviewController);

    MstPendingCADReviewController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstPendingCADReviewController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstPendingCADReviewController';

        activate();
        //lockUI();
        function activate() {
            lockUI();
            var url = 'api/MstCAD/GetPendingCADReviewSummary';
            SocketService.get(url).then(function (resp) {
                if (resp.data.cadapplicationlist != null && resp.data.cadapplicationlist.length > 0) {
                    $scope.Pendingcadreview_list = resp.data.cadapplicationlist;
                    unlockUI();
                }
                else if (resp.data.status == false)
                    unlockUI();
            });
            var url = 'api/MstCAD/CADApplicationCount';
            //lockUI();
            SocketService.get(url).then(function (resp) {
                //unlockUI();
                $scope.cadreview_count = resp.data.cadreview_count;
                $scope.sentbackcc_count = resp.data.sentbackcc_count;
                $scope.accept_count = resp.data.accept_count;
                $scope.backtounderwriting_count = resp.data.backtounderwriting_count;
                $scope.ccrejected_count = resp.data.ccrejected_count;
                $scope.lstotalcount = resp.data.lstotalcount;
                $scope.urngrouping_count = resp.data.urngrouping_count;
            });
        }

        $scope.view = function (val, val1) {
            $location.url('app/MstCadPendingApplicationView?application_gid=' + val + '&employee_gid=' + val1 + '&lspage=PendingCADReview');
        }

        $scope.edit = function (val, val1, val2) {
            $location.url('app/MstCADPendingApplicationEdit?application_gid=' + val + '&lspage=PendingCADReview' + '&product_gid=' + val1 + '&variety_gid=' + val2);
        }

        //$scope.process = function (val) {
        //    $location.url('app/MstApplicationCreationView?application_gid=' + val + '&lstab=MyApplications');
        //}

        $scope.pendincad_review = function () {
            $location.url('app/MstPendingCADReview');
        }

        $scope.urn_grouping = function () {
            $location.url('app/MstCadUrnAcceptedCustomers');
        }

        $scope.cadaccepted_customers = function () {
            $location.url('app/MstCadAcceptedCustomers');
        }

        $scope.sendback_underwriting = function () {
            $location.url('app/MstSentBackToUnderwriting');
        }

        $scope.sendback_cc = function () {
            $location.url('app/MstSentBackToCC');
        }

        $scope.cc_rejected = function () {
            $location.url('app/MstCCRejectedApplications');
        }

        $scope.process = function (val) {
            $location.url('app/MstCADGroupProcessAssign?application_gid=' + val + '&lspage=PendingCADReview');
        }

        $scope.history = function (application_gid) {
            $location.url('app/MstSentbackCadtoCcHistory?application_gid=' + application_gid + '&lspage=CADPendingSummary');
        }

        $scope.cad_verification = function (application_gid) {
            $location.url('app/MstColendingCCApprovedVerification?application_gid=' + application_gid + '&lspage=PendingCADReview');
        }

        //$scope.process = function (val) {
        //    var modalInstance = $modal.open({
        //        templateUrl: '/processdtl.html',
        //        controller: ModalInstanceCtrl,
        //        backdrop: 'static',
        //        keyboard: false,
        //        size: 'md'
        //    });

        //    ModalInstanceCtrl.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];
        //    function ModalInstanceCtrl($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {
        //        var application_gid = val;
        //        $scope.change_process = function (cboprocesstype) {
        //            if ($scope.cboprocesstype == 'Sendback to CC') {
        //                $scope.processshow = true;
        //            }
        //            else if ($scope.cboprocesstype == 'Sendback to Credit Underwriting') {
        //                $scope.processshow = true;
        //            }
        //            else {
        //                $scope.processshow = false;
        //            }
        //        }

        //        $scope.submit = function () {
        //            if (( $scope.cboprocesstype == 'Sendback to CC' || $scope.cboprocesstype == 'Sendback to Credit Underwriting') && ( $scope.remarks == '' || $scope.remarks == null )) {
        //                modalInstance.close('closed');
        //                Notify.alert('Kindly Enter the Remarks Details', 'warning');
        //                return;
        //            }

        //            var params = {
        //                application_gid: application_gid,
        //                process_type: $scope.cboprocesstype,
        //                processtype_remarks: $scope.remarks
        //            }

        //            var url = "api/MstApplicationAdd/UpdateProcessType";
        //            lockUI();
        //            SocketService.post(url, params).then(function (resp) {
        //                unlockUI();
        //                if (resp.data.status = true) {
        //                    modalInstance.close('closed');
        //                    Notify.alert(resp.data.message, 'success');
        //                    activate();
        //                }
        //                else {
        //                    modalInstance.close('closed');
        //                    Notify.alert(resp.data.message, 'warning');
        //                    activate();
        //                }
        //            });

        //        }

        //        $scope.close = function () {
        //            modalInstance.close('closed');
        //        };
        //    }
        //}

    }
})();
