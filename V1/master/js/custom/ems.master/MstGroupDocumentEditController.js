(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstGroupDocumentEditController', MstGroupDocumentEditController);

    MstGroupDocumentEditController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstGroupDocumentEditController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstGroupDocumentEditController';

        $scope.groupdocument_gid = $location.search().lsgroupdocument_gid;
        var groupdocument_gid = $scope.groupdocument_gid;

        activate();

        function activate() {

            $scope.program_list = [];

            var url = 'api/MstApplication360/GroupCheckListTempClear';
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

            var param = {

                groupdocument_gid: groupdocument_gid
            };
            var url = 'api/MstApplication360/GetGroupCheckListEditList';

            SocketService.getparams(url, param).then(function (resp) {
                $scope.checklistedit_list = resp.data.checklist_list;
                unlockUI();
            });

            var params = {
                groupdocument_gid: groupdocument_gid
            }
            var url = 'api/MstApplication360/EditGroupDocument';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.txteditgroupdocument_name = resp.data.groupdocument_name;
                $scope.cbodocument = resp.data.documenttypes_gid;
                $scope.cbodocumentseverity = resp.data.documentseverity_gid;
                $scope.txtlms_code = resp.data.lms_code;
                $scope.txtbureau_code = resp.data.bureau_code;
                $scope.rdbCovenant_type = resp.data.covenant_type;
                $scope.groupstatus = resp.data.Status;
                $scope.txtdocumentcode = resp.data.document_code;
                $scope.txtdisplayorder = resp.data.display_order;
                $scope.txtremarks = resp.data.document_remarks;
              /*  $scope.lblprogram_name_edit = resp.data.program_name_edit;*/
                $scope.appvernacularlanguage_list = resp.data.CboProgram_list;
                $scope.cboprogram_list = [];

                if (resp.data.CboProgram_list != null) {
                    var count = resp.data.CboProgram_list.length;
                    for (var i = 0; i < count; i++) {
                        var indexs = $scope.program_list.map(function (x) { return x.program_gid; }).indexOf(resp.data.CboProgram_list[i].program_gid);
                        $scope.cboprogram_list.push($scope.program_list[indexs]);
                        $scope.$parent.cboprogram = $scope.cboprogram_list;
                    }
                }

                unlockUI();
            });

        }
        //$scope.addCompanyDocument = function () {
        //    var modalInstance = $modal.open({
        //        templateUrl: '/addCompanyDocument.html',
        //        controller: ModalInstanceCtrl,
        //        backdrop: 'static',
        //        keyboard: false,
        //        size: 'md'
        //    });
        //    ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
        //    function ModalInstanceCtrl($scope, $modalInstance) {

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
        $scope.update = function () {

            var documenttype_Name = $('#Document :selected').text();
            var documentseverity_Name = $('#Severity :selected').text();
            if (($scope.$parent.cboprogram == undefined) || ($scope.$parent.cboprogram == '') || ($scope.$parent.cboprogram == null)) {

                Notify.alert('Select Atleast One Program', 'warning');
                return;
            }
            lockUI();
            var url = 'api/MstApplication360/UpdateGroupDocument';
            var params = {

                groupdocument_gid: groupdocument_gid,
                groupdocument_name: $scope.txteditgroupdocument_name,
                documenttypes_gid: $scope.cbodocument,
                documenttype_name: documenttype_Name,
                documentseverity_gid: $scope.cbodocumentseverity,
                documentseverity_name: documentseverity_Name,
                CboProgram_list: $scope.$parent.cboprogram,
                lms_code: $scope.txtlms_code,
                bureau_code: $scope.txtbureau_code,
                document_code:$scope.txtdocumentcode,
                display_order:$scope.txtdisplayorder,
                document_remarks:$scope.txtremarks,
                covenant_type: $scope.rdbCovenant_type,
                Status: $scope.groupstatus
            }
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
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

        $scope.checklist_edit = function () {
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

            var params = {

                groupdocument_gid: groupdocument_gid
            };
            var url = 'api/MstApplication360/GetGroupCheckListTempEditList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.checklistedit_list = resp.data.application_list;
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
