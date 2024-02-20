(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCompanyDocumentEditController', MstCompanyDocumentEditController);

    MstCompanyDocumentEditController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstCompanyDocumentEditController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCompanyDocumentEditController';
        $scope.companydocument_gid = $location.search().lscompanydocument_gid;
        var companydocument_gid = $scope.companydocument_gid;

        activate();
        lockUI();
        function activate() {
            
            $scope.program_list = [];

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

            var param = {

                companydocument_gid: companydocument_gid
            };
            var url = 'api/MstApplication360/GetCompanyCheckListEditList';

            SocketService.getparams(url, param).then(function (resp) {
                $scope.checklistedit_list = resp.data.checklist_list;
                unlockUI();
            });
            
             $scope.cboprogram = [];

            var params = {
                companydocument_gid: companydocument_gid
            }
            var url = 'api/MstApplication360/EditCompanyDocument';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.txteditcompanydocument_name = resp.data.companydocument_name;
                $scope.cbodocument = resp.data.documenttypes_gid;
                $scope.cbodocumentseverity = resp.data.documentseverity_gid;
                $scope.txtlms_code = resp.data.lms_code;
                $scope.txtbureau_code = resp.data.bureau_code;
                $scope.txtdocumentcode = resp.data.document_code;
                $scope.txtdisplayorder = resp.data.display_order;
                $scope.txtremarks = resp.data.document_remarks;
                $scope.rdbCovenant_type = resp.data.covenant_type;
                $scope.Status = resp.data.Status;
         /*       $scope.lblprogram_name_edit = resp.data.program_name_edit;*/
                 //$scope.cboprogram__list = resp.data.CboProgram_list;

                 //if (resp.data.CboProgram_list != null) {
                 //    var count = resp.data.CboProgram_list.length;
                 //    for (var i = 0; i < count; i++) {
                 //        var indexs = $scope.program_list.map(function (x) { return x.program_gid; })
                 //        .indexOf(resp.data.CboProgram_list[i].program_gid);
                 //        $scope.cboprogram_list.push($scope.program_list[indexs]);
                 //        $scope.$parent.cboprogram = $scope.cboprogram_list;
                 //        // $scope.cboprogram.push($scope.program_list[indexs]);
                 //        // $scope.cboprogram = $scope.cboprogram;
                 //    }
                 //}
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

            //var url = 'api/MstApplication360/TempClear';
            //SocketService.get(url).then(function (resp) {
            //});
            //var url = 'api/MstApplication360/GetDropDown';
            //SocketService.get(url).then(function (resp) {
            //    $scope.sector_list = resp.data.sector_list;
            //    $scope.category_list = resp.data.category_list;
            //});
        }
       

        $scope.ok = function () {
            $modalInstance.close('closed');
        };
        //        var url = 'api/MstApplication360/GetCompanyDropDown';
        //        SocketService.get(url).then(function (resp) {
        //            $scope.document_list = resp.data.document_list;

        //});
        $scope.Back = function () {
            $state.go('app.MstCompanyDocument');

        }
        $scope.update = function () {

            var documenttype_Name = $('#Document :selected').text();
            var documentseverity_Name = $('#Severity :selected').text();
            if (($scope.$parent.cboprogram == undefined) || ($scope.$parent.cboprogram == '') || ($scope.$parent.cboprogram == null)) {

                Notify.alert('Select Atleast One Program', 'warning');
                return;
            }

            lockUI();
            var url = 'api/MstApplication360/UpdateCompanyDocument';
            var params = {

                companydocument_gid: companydocument_gid,
                companydocument_name: $scope.txteditcompanydocument_name,
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
                Status: $scope.Status,
                // CboProgram_list: $scope.cboprogram

            }
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

        $scope.checklist_edit = function () {
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
            var params = {

                companydocument_gid: companydocument_gid
            };
            var url = 'api/MstApplication360/GetCompanyCheckListTempEditList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.checklistedit_list = resp.data.application_list;
                unlockUI();
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
    //    }
    //}
})();
