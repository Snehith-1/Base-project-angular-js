(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstHRLoanHRDocumentEditController', MstHRLoanHRDocumentEditController);

    MstHRLoanHRDocumentEditController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','cmnfunctionService'];

    function MstHRLoanHRDocumentEditController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstHRLoanHRDocumentEditController';
        $scope.hrdocument_gid = cmnfunctionService.decryptURL($location.search().hash).lshrdocument_gid;
        var hrdocument_gid = $scope.hrdocument_gid;

        activate();
        lockUI();
        function activate() {

            var url = 'api/MstHRLoanHRDocument/HRDocumentCheckListTempClear';
            SocketService.get(url).then(function (resp) {
            });

            var url = 'api/MstHRLoanHRDocument/GetHRDocumentDropDown';
            SocketService.get(url).then(function (resp) {
                $scope.document_list = resp.data.document_list;
                $scope.hrdocumentseverity_list = resp.data.hrdocumentseverity_list;

            });

            var param = {

                hrdocument_gid: hrdocument_gid
            };
            var url = 'api/MstHRLoanHRDocument/GetHRDocumentCheckListEditList';

            SocketService.getparams(url, param).then(function (resp) {
                $scope.checklistedit_list = resp.data.checklist_list;
                unlockUI();
            });
            var params = {
                hrdocument_gid: hrdocument_gid
            }
            var url = 'api/MstHRLoanHRDocument/EditHRDocument';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtedithrdocument_name = resp.data.hrdocument_name;
                $scope.cbohrdocument = resp.data.hrloantypeoffinancialassistance_gid;
                $scope.cbohrdocumentseverity = resp.data.hrloanseverity_gid;
                $scope.txtlms_code = resp.data.lms_code;
                $scope.txtbureau_code = resp.data.bureau_code;
                // $scope.rdbCovenant_type = resp.data.covenant_type;
                $scope.Status = resp.data.Status;
                unlockUI();
            });

           
        }
       

        $scope.ok = function () {
            $modalInstance.close('closed');
        };
      
        $scope.Back = function () {
            $state.go('app.MstHRLoanHRDocument');

        }
        $scope.update = function () {

            var hrloantypeoffinancialassistance_name = $('#Document :selected').text();
            var hrloanseverity_name = $('#Severity :selected').text();

            lockUI();
            var url = 'api/MstHRLoanHRDocument/UpdateHRDocument';
            var params = {

                hrdocument_gid: hrdocument_gid,
                hrdocument_name: $scope.txtedithrdocument_name,
                hrloantypeoffinancialassistance_gid: $scope.cbohrdocument,
                hrloantypeoffinancialassistance_name: hrloantypeoffinancialassistance_name,
                hrloanseverity_gid: $scope.cbohrdocumentseverity,
                hrloanseverity_name: hrloanseverity_name,
                lms_code: $scope.txtlms_code,
                bureau_code: $scope.txtbureau_code,
                // covenant_type: $scope.rdbCovenant_type,
                Status: $scope.Status
            }
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $state.go('app.MstHRLoanHRDocument');
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
            });
        }

        $scope.checklist_edit = function () {
            if (($scope.txtchecklist == undefined) || ($scope.txtchecklist == '')) {
                Notify.alert('Enter Check List', 'warning');
            }
            else {

                var params = {
                    hrdocumentchecklist_name: $scope.txtchecklist,

                }
                lockUI();
                var url = 'api/MstHRLoanHRDocument/CreateHRDocumentCheckList';
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
                    checklist();
                    $scope.txtchecklist = '';

                });
            }
        }

        function checklist() {
            var params = {

                hrdocument_gid: hrdocument_gid
            };
            var url = 'api/MstHRLoanHRDocument/GetHRDocumentCheckListTempEditList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.checklistedit_list = resp.data.hrloanhrdocument_list;
                unlockUI();
            });
        }

        $scope.checklist_delete = function (hrdocumentchecklist_gid) {
            var params =
                {
                    hrdocumentchecklist_gid: hrdocumentchecklist_gid
                }
            lockUI();
            var url = 'api/MstHRLoanHRDocument/DeleteHRDocumentCheckList';
            SocketService.getparams(url, params).then(function (resp) {
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
                checklist();
            });
        }

    }
    //    }
    //}
})();
