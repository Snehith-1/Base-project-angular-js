(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCourierViewController', MstCourierViewController);

    MstCourierViewController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'DownloaddocumentService', 'cmnfunctionService'];

    function MstCourierViewController($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, DownloaddocumentService, cmnfunctionService) {

        $scope.title = 'MstCourierViewController';
        var vm = this;
        vm.title = 'MstCourierViewController';
        
        $scope.courier_gid = $location.search().courier_gid;
        var courier_gid = $scope.courier_gid;
        $scope.page = $location.search().page;
        var page = $scope.page;

        activate();
        lockUI();
        function activate() {
            $scope.courier_value = true;
            $scope.physical_value = false;
            $scope.courier_inward = true;
            $scope.courier_outward = false;
            $scope.physical_inward = false;
            $scope.physical_outward = false;
            
            $scope.courier_gid = courier_gid;

            var params = {
                courier_gid: courier_gid
            }
            var url = 'api/MstCourierManagement/GetEditCourierDetail';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {                   
                    $scope.customer_name = resp.data.customer_name;
                    $scope.courierref_no = resp.data.courierref_no;
                    $scope.date_of_courier = resp.data.date_of_courier;
                    $scope.document_type = resp.data.document_type;
                    $scope.address = resp.data.address;
                    $scope.sender_name = resp.data.sender_name;
                    $scope.pod_no = resp.data.pod_no;
                    $scope.courier_company_name = resp.data.couriercompany_name;
                    $scope.courierhandover_to = resp.data.courierhandover_to;
                    $scope.courier_type = resp.data.courier_type;
                    $scope.remarks = resp.data.remarks;
                    $scope.handover_name = resp.data.courierhandover_to_gid;
                    $scope.uploadDoc_list = resp.data.uploadcourierdocument;
                    $scope.sanctionref_no = resp.data.sanctionref_no;
                    $scope.ack_status = resp.data.ack_status;
                    $scope.ack_date = resp.data.ack_date;
                    $scope.ackby_name = resp.data.ackby_name;
                    $scope.created_by = resp.data.created_by;
                    $scope.created_date = resp.data.created_date;
                }

                if ($scope.courier_type == "Courier Outward" || $scope.courier_type == "Courier Inward") {
                    $scope.courier_value = true;
                    $scope.physical_value = false;
                }
                if ($scope.courier_type == "Courier Outward") {
                    $scope.courier_outward = true;
                    $scope.courier_inward = false;
                    $scope.physical_inward = false;
                    $scope.physical_outward = false;
                }

                if ($scope.courier_type == "Courier Inward") {
                    $scope.courier_outward = false;
                    $scope.courier_inward = true;
                    $scope.physical_inward = false;
                    $scope.physical_outward = false;

                }

                if ($scope.courier_type == "Physical Inward") {
                    $scope.courier_outward = false;
                    $scope.courier_inward = false;
                    $scope.physical_inward = true;
                    $scope.physical_outward = false;

                }
                if ($scope.courier_type == "Physical Outward") {
                    $scope.courier_outward = false;
                    $scope.courier_inward = false;
                    $scope.physical_inward = false;
                    $scope.physical_outward = true;

                }
                if ($scope.courier_type == "Physical Inward" || $scope.courier_type == "Physical Outward") {
                    $scope.courier_value = false;
                    $scope.physical_value = true;
                }

            });

        }

        $scope.downloadsdocument = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.courierback = function () {
            $location.url('app/MstCourierMgmtsummary?lstab=' + page);
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
    }
})();
