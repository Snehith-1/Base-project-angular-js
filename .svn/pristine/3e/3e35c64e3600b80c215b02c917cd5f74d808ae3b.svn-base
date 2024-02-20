(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstGroupDocumentAddController', MstGroupDocumentAddController);

    MstGroupDocumentAddController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstGroupDocumentAddController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstGroupDocumentAddController';

        activate();

        function activate() {
            $scope.rdbcovenant_type = 'N';
            var url = 'api/MstApplication360/GetCompanyDropDown';
            SocketService.get(url).then(function (resp) {
                $scope.document_list = resp.data.document_list;
                $scope.documentseverity_list = resp.data.documentseverity_list;

            });
            var url = 'api/MstApplication360/GetProgramActive';
            
            SocketService.get(url).then(function (resp) {
                $scope.program_list = resp.data.applicationprogram_list;
                
            });

        }

        $scope.ok = function () {
            $modalInstance.close('closed');
        };
        //        var url = 'api/MstApplication360/GetCompanyDropDown';
        //        SocketService.get(url).then(function (resp) {
        //            $scope.document_list = resp.data.document_list;

        //});
        $scope.Back = function () {
            $state.go('app.MstGroupDocument');

        }
        $scope.submit = function () {
            if (($scope.cboprogram == undefined) || ($scope.cboprogram == '') || ($scope.cboprogram == null)) {

                Notify.alert('Select Atleast One Program', 'warning');
                return;
            }
            var lsdocumenttypes_gid = '';
            var lsdocumenttype_name = '';
            if ($scope.cbodocument != undefined || $scope.cbodocument != null) {
                lsdocumenttypes_gid = $scope.cbodocument.documenttypes_gid;
                lsdocumenttype_name = $scope.cbodocument.documenttype_name;
            }

            var lsdocumentseverity_gid = '';
            var lsdocumentseverity_name = '';
            if ($scope.cbodocumentseverity != undefined || $scope.cbodocumentseverity != null) {
                lsdocumentseverity_gid = $scope.cbodocumentseverity.documentseverity_gid;
                lsdocumentseverity_name = $scope.cbodocumentseverity.documentseverity_name;
            }

            var params = {
                groupdocument_name: $scope.txtgroupdocument_name,
                documenttypes_gid: lsdocumenttypes_gid,
                documenttype_name: lsdocumenttype_name,
                documentseverity_gid: lsdocumentseverity_gid,
                documentseverity_name: lsdocumentseverity_name,
                CboProgram_list: $scope.cboprogram,
                lms_code: $scope.txtlms_code,
                bureau_code: $scope.txtbureau_code,
                covenant_type: $scope.rdbcovenant_type,
                document_code: $scope.txtdocumentcode,
                display_order: $scope.txtdisplayorder,
                document_remarks: $scope.txtremarks  
            }
            var url = 'api/MstApplication360/CreateGroupDocument';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    activate();
                    $state.go('app.MstGroupDocument');
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
                Notify.alert('Enter Check List', 'warning');
            }
            else {

                var params = {
                    checklist_name: $scope.txtchecklist,

                }
                lockUI();
                var url = 'api/MstApplication360/CreateGroupCheckList';
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
            var url = 'api/MstApplication360/GetGroupCheckList';
            SocketService.get(url).then(function (resp) {
                $scope.checklist_list = resp.data.application_list;
            });
        }

        $scope.checklist_delete = function (groupchecklist_gid) {
            var params =
                {
                    groupchecklist_gid: groupchecklist_gid
                }
            lockUI();
            var url = 'api/MstApplication360/DeleteGroupCheckList';
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
