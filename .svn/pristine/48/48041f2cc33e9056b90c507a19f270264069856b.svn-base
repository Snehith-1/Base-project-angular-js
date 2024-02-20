(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstHRLoanHRDocumentAddController', MstHRLoanHRDocumentAddController);

    MstHRLoanHRDocumentAddController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstHRLoanHRDocumentAddController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstHRLoanHRDocumentAddController';

        activate();

        function activate() {
            
            var url = 'api/MstHRLoanHRDocument/HRDocumentCheckListTempClear';
            SocketService.get(url).then(function (resp) {
            });
            
            var url = 'api/MstHRLoanHRDocument/GetHRDocumentDropDown';
            SocketService.get(url).then(function (resp) {
                $scope.document_list = resp.data.document_list;
                $scope.hrdocumentseverity_list = resp.data.hrdocumentseverity_list;

            });

        }
       
        $scope.ok = function () {
            $modalInstance.close('closed');
        };
      
        $scope.Back = function () {
            $state.go('app.MstHRLoanHRDocument');

        }
        $scope.submit = function () {
            var lshrloantypeoffinancialassistance_gid = '';
            var lshrloantypeoffinancialassistance_name = '';
            if ($scope.cbohrdocument != undefined || $scope.cbohrdocument != null) {
                lshrloantypeoffinancialassistance_gid = $scope.cbohrdocument.hrloantypeoffinancialassistance_gid;
                lshrloantypeoffinancialassistance_name = $scope.cbohrdocument.hrloantypeoffinancialassistance_name;
            }

            var lshrloanseverity_gid = '';
            var lshrloanseverity_name = '';
            if ($scope.cbohrdocumentseverity != undefined || $scope.cbohrdocumentseverity != null) {
                lshrloanseverity_gid = $scope.cbohrdocumentseverity.hrloanseverity_gid;
                lshrloanseverity_name = $scope.cbohrdocumentseverity.hrloanseverity_name;
            }
            var params = {
                hrdocument_name: $scope.txthrdocument_name,
                hrloantypeoffinancialassistance_gid: lshrloantypeoffinancialassistance_gid,
                hrloantypeoffinancialassistance_name: lshrloantypeoffinancialassistance_name,
                hrloanseverity_gid: lshrloanseverity_gid,
                hrloanseverity_name: lshrloanseverity_name,
                lms_code: $scope.txtlms_code,
                bureau_code: $scope.txtbureau_code,
                // covenant_type: $scope.covenant_type 
            }
            lockUI();
            var url = 'api/MstHRLoanHRDocument/CreateHRDocument';
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


        $scope.checklist_add = function () {
            if (($scope.txtchecklist == undefined) || ($scope.txtchecklist == '')) {
                Notify.alert('Enter Check List', {
                    status: 'warning',
                    pos: 'top-center',
                    timeout: 3000
                });
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
            var url = 'api/MstHRLoanHRDocument/GetHRDocumentCheckList';            
            SocketService.get(url).then(function (resp) {
                $scope.checklist_list = resp.data.hrloanhrdocument_list;

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
})();
