(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstUserProfileController', MstUserProfileController);

    MstUserProfileController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'DownloaddocumentService'];

    function MstUserProfileController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstUserProfileController';

        activate();
        lockUI();
        function activate() {
            var url = 'api/ManageEmployee/EmployeeProfileView';
            SocketService.get(url).then(function (resp) {
                $scope.employee_details = resp.data;
                unlockUI();
            });

            var url = 'api/ManageEmployee/GetHRDocProfilelist';

            SocketService.get(url).then(function (resp) {
                $scope.document_list = resp.data.hrdoc;
                unlockUI();
            });
        }

        $scope.Back = function () {
            $state.go('app.welcome');
        }

        $scope.hrdocument_downloads = function (val1, val2, val3) {
            if (val3 == 'N') {
                DownloaddocumentService.Downloaddocument(val1, val2);
            }
            else {
                DownloaddocumentService.OtherDownloaddocument(val1, val2, val3);
            }  
        }

        $scope.downloadall = function () {
            for (var i = 0; i < $scope.document_list.length; i++) {
                if ($scope.document_list[i].migration_flag == 'N') {
                    DownloaddocumentService.Downloaddocument($scope.document_list[i].hrdoc_path, $scope.document_list[i].hrdoc_name);
                }
                else {
                    DownloaddocumentService.OtherDownloaddocument($scope.document_list[i].hrdoc_path, $scope.document_list[i].hrdoc_name, "HRMigration");
                } 
            }
        }

        $scope.downloaddocfromdigio = function (val1, val2, val3) {
            var params = {
                hrdoc_id: val1,
                hrdoc_path: val2,
                hrdoc_name: val3
            }
            var url = 'api/SysMstHRDocument/DownloadDocfromDigio';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true)
                    $rootScope.$emit('downloadEvent', resp);
                else {
                    return resp;
                }
            });
        }

        $scope.esignstatusenquiry = function (val1, val2, val3) {
            var params = {
                hrdoc_id: val1,
                file_name: val2,
                file_path: val3
            }
            var url = 'api/SysMstHRDocument/GetDocumentDetails';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                activate();
            });
        }


    }
})();
