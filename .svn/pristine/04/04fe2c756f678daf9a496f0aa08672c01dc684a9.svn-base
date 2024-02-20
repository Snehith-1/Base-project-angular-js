(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCourierMgmtAckListController', MstCourierMgmtAckListController);

    MstCourierMgmtAckListController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$timeout', 'ngTableParams', 'DownloaddocumentService', 'cmnfunctionService'];

    function MstCourierMgmtAckListController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $timeout, ngTableParams, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCourierMgmtAckListController';

        activate();

        function activate() {
            var url = 'api/MstCourierManagement/CourierAckList';
            SocketService.get(url).then(function (resp) {
                $scope.courierackpending_list = resp.data.CourierAckPending;
                $scope.courierack_list = resp.data.CourierMgmt;
            });
        }

        $scope.back = function () {
            $state.go('app.MstCourierMgmtsummary');
        }

        // Submit Acknowledgement Code Starts
        $scope.ack_submit = function (courierMgmt_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/sendack.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    modalInstance.close('closed');
                };

                var params = {
                    courierMgmt_gid: courierMgmt_gid
                }

                var url = 'api/MstCourierManagement/CourierAckView';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.courierref_no = resp.data.courierref_no;
                    $scope.date_of_courier = resp.data.date_of_courier;
                    $scope.sanctionref_no = resp.data.sanctionref_no;
                    $scope.customer_name = resp.data.customer_name;
                    $scope.document_type = resp.data.document_type;
                    $scope.sender_name = resp.data.sender_name;
                    $scope.pod_no = resp.data.pod_no;
                    $scope.couriercompany_name = resp.data.couriercompany_name;
                    $scope.courierhandover_to = resp.data.courierhandover_to;
                    $scope.courier_type = resp.data.courier_type;
                    $scope.ack_status = resp.data.ack_status;
                    $scope.remarks = resp.data.remarks;
                    $scope.ack_date = resp.data.ack_date;
                    $scope.ackby_name = resp.data.ackby_name;
                });               

                $scope.downloadsdocument = function (val1, val2) {
                    DownloaddocumentService.Downloaddocument(val1, val2);
                }

                $scope.downloadall = function () {
                    for (var i = 0; i < $scope.uploadDoc_list.length; i++) {
                        DownloaddocumentService.Downloaddocument($scope.uploadDoc_list[i].document_path, $scope.uploadDoc_list[i].document_name);
                    }
                }

                $scope.documentviewer = function (val1, val2) {
                    lockUI();
                    var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
                    if (IsValidExtension == false) {
                        Notify.alert("View is not supported for this format..!", {
                            status: 'danger',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        unlockUI();
                        return false;
                    }
                    DownloaddocumentService.DocumentViewer(val1, val2);
                }
                              
                var params = {
                    courier_gid: courierMgmt_gid
                }
                var url = 'api/MstCourierManagement/GetEditCourierDetail';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        $scope.uploadDoc_list = resp.data.uploadcourierdocument;
                    }
                });

                // Submit
                $scope.sendcourierack = function () {
                    var params = {
                        courierMgmt_gid: courierMgmt_gid,
                    }
                    lockUI();
                    var url = "api/MstCourierManagement/AckStatus"
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                            unlockUI();
                        }
                        else {
                            modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                        }
                    });
                }
                // Click Cancel Button
                $scope.ok = function () {
                    modalInstance.close('closed');
                };
            }
        }
    }
})();
