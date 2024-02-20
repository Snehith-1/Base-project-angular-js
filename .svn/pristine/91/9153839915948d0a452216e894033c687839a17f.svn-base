(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCompanyDocumentAddController', MstCompanyDocumentAddController);

    MstCompanyDocumentAddController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstCompanyDocumentAddController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCompanyDocumentAddController';

        activate();

        function activate() {
            $scope.covenant_type = 'N';
            var url = 'api/MstApplication360/CompanyCheckListTempClear';
            SocketService.get(url).then(function (resp) {
            });

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
      
        $scope.Back = function () {
            $state.go('app.MstCompanyDocument');

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
            // var lsprogram_gid = '';
            // var lsprogram_name = '';
            // if ($scope.cboprogram != undefined || $scope.cboprogram != null) {
            //     lsprogram_gid = $scope.cbodocumentseverity.program_gid;
            //     lsprogram_name = $scope.cbodocumentseverity.program;
            // }
            var params = {
                companydocument_name: $scope.txtCompanyDocument_name,
                documenttypes_gid: lsdocumenttypes_gid,
                documenttype_name: lsdocumenttype_name,
                documentseverity_gid: lsdocumentseverity_gid,
                documentseverity_name: lsdocumentseverity_name,
                // program_gid: lsprogram_gid,
                // program_name: lsprogram_name,
                CboProgram_list: $scope.cboprogram,
                lms_code: $scope.txtlms_code,
                bureau_code: $scope.txtbureau_code,
                covenant_type: $scope.covenant_type,
                document_code: $scope.txtdocumentcode,
                display_order: $scope.txtdisplayorder,
                document_remarks: $scope.txtremarks 
            }
            lockUI();
            var url = 'api/MstApplication360/CreateCompanyDocument';
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $state.go('app.MstCompanyDocument');
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
                var url = 'api/MstApplication360/CreateCompanyCheckList';
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
            var url = 'api/MstApplication360/GetCompanyCheckList';
            SocketService.get(url).then(function (resp) {
                $scope.checklist_list = resp.data.application_list;
            });
        }

        $scope.checklist_delete = function (companychecklist_gid) {
            var params =
                {
                    companychecklist_gid: companychecklist_gid
                }
            lockUI();
            var url = 'api/MstApplication360/DeleteCompanyCheckList';
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
