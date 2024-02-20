(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstIndividualDocumentAddController', MstIndividualDocumentAddController);

    MstIndividualDocumentAddController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstIndividualDocumentAddController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstIndividualDocumentAddController';

        activate();

        function activate() {
            $scope.rdbcovenant_type = 'N';
            var url = 'api/MstApplication360/GetIndividualDropDown';
            SocketService.get(url).then(function (resp) {
                $scope.document_list = resp.data.document_list;
            });
            var url = 'api/MstApplication360/GetSeverityDropDown';
            SocketService.get(url).then(function (resp) {
                $scope.severity_list = resp.data.severity_list;
            });
            var url = 'api/MstApplication360/CheckListTempClear';
            SocketService.get(url).then(function (resp) {
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
            $state.go('app.MstIndividualDocument');

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
            if ($scope.cboseverity != undefined || $scope.cboseverity != null) {
                lsdocumentseverity_gid = $scope.cboseverity.documentseverity_gid;
                lsdocumentseverity_name = $scope.cboseverity.documentseverity_name;
            }
            var params = {
                individualdocument_name: $scope.txtIndividualDocument_name,
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
            var url = 'api/MstApplication360/CreateIndividualDocument';
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
                    $state.go('app.MstIndividualDocument');
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'info',
                        pos: 'top-center',
                        timeout: 3000
                    });
              
                    activate();
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
                var url = 'api/MstApplication360/CreateCheckList';
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
            var url = 'api/MstApplication360/GetCheckList';
            SocketService.get(url).then(function (resp) {
                $scope.checklist_list = resp.data.application_list;
            });
        }

        $scope.checklist_delete = function (individualchecklist_gid) {
            var params =
            {
                individualchecklist_gid: individualchecklist_gid
            }
            lockUI();
            var url = 'api/MstApplication360/DeleteCheckList';
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
