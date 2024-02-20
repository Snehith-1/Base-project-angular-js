(function () {
    'use strict';

    angular
        .module('angle')
        .controller('cibilsummaryController', cibilsummaryController);

    cibilsummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','cmnfunctionService'];

    function cibilsummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, cmnfunctionService) {
        $scope.title = 'cibilsummaryController';

        activate();

        function activate() {
            $scope.totalDisplayed = 100;
        
            lockUI();
            var url = "api/MstCibilData/GetCibilUploadSummary";
            SocketService.get(url).then(function (resp) {
                unlockUI();
               $scope.list = resp.data.uploadedcibil_data;
                $scope.total = $scope.list.length;
            });
        }
        $scope.uploaddocument = function () {
            var modalInstance = $modal.open({
                templateUrl: '/uploaddocument.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.txtbuyer_exposure = 0;

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.upload = function (val, val1, name) {
                    var item = {
                        name: val[0].name,
                        file: val[0]
                    };
                    var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "documentformatonly");
                    if (IsValidExtension == false) {
                        Notify.alert("File format is not supported..!", {
                            status: 'danger',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        return false;
                    }
                    var frm = new FormData();
                    frm.append('fileupload', item.file);
                    frm.append('file_name', item.name);
                    frm.append('document_name', $scope.documentname);
                    $scope.uploadfrm = frm;
                }
                $scope.uploadfile = function () {
                    var url = 'api/MstCibilData/UploadCibil';
                    lockUI();
                    SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                        $scope.options = false;
                        $("#addupload").val('');
                        if (resp.data.status == true) {
                            unlockUI();
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                        }
                        else {
                            unlockUI();
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                        }
                        activate();
                    });
                }
            }
        }
        $scope.openpanel = function () {
            $scope.show = false;
            $scope.options = true;
        }
        $scope.cancel = function () {
            $scope.show = true;
            $scope.options = false;
            $("#addupload").val('');
        }
     
    
        $scope.viewcibil_dtl = function (cibildata_gid)
        {
            $scope.cibildata_gid = localStorage.setItem('cibildata_gid', cibildata_gid);
            $state.go('app.MstCibilDataSummary');
        }
        $scope.logdtl = function (cibildata_gid)
        {
            $scope.cibildata_gid = localStorage.setItem('cibildata_gid', cibildata_gid);
            $state.go('app.MstCibilDataLogDetails');
        }
    }
})();
