(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstIndividualDocumentEditController', MstIndividualDocumentEditController);

    MstIndividualDocumentEditController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstIndividualDocumentEditController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstIndividualDocumentEditController';

        $scope.individualdocument_gid = $location.search().lsindividualdocument_gid;
        var individualdocument_gid = $scope.individualdocument_gid;

        activate();
        lockUI();
        function activate() {

            $scope.program_list = [];

            var url = 'api/MstApplication360/CheckListTempClear';
            SocketService.get(url).then(function (resp) {
            });

            var url = 'api/MstApplication360/GetCompanyDropDown';
            SocketService.get(url).then(function (resp) {
                $scope.document_list = resp.data.document_list;
               
            });
            var url = 'api/MstApplication360/GetSeverityDropDown';
            SocketService.get(url).then(function (resp) {
                $scope.severity_list = resp.data.severity_list;
            });
            var url = 'api/MstApplication360/GetProgramActive';
            
            SocketService.get(url).then(function (resp) {
                $scope.program_list = resp.data.applicationprogram_list;
                
            });

            var param = {

                individualdocument_gid: individualdocument_gid
            };
            var url = 'api/MstApplication360/GetCheckEditList';

            SocketService.getparams(url, param).then(function (resp) {
                $scope.checklistedit_list = resp.data.checklist_list;
                unlockUI();
            });

            var params = {
                individualdocument_gid: individualdocument_gid
            }
            var url = 'api/MstApplication360/EditIndividualDocument';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.txteditindividualDocument_name = resp.data.individualdocument_name;
                $scope.cbodocument = resp.data.documenttypes_gid;
                //$scope.documenttype_name = resp.data.documenttype_Name,
                $scope.cboseverity = resp.data.documentseverity_gid;
                //$scope.documentseverity_name = resp.data.documentseverity_name,
                $scope.txtlms_code = resp.data.lms_code;
                $scope.txtbureau_code = resp.data.bureau_code;
                $scope.rdbCovenant_type = resp.data.covenant_type;
                $scope.documentStatus = resp.data.Status;
                $scope.txtdocumentcode = resp.data.document_code;
                $scope.txtdisplayorder = resp.data.display_order;
                $scope.txtremarks = resp.data.document_remarks;
               /* $scope.lblprogram_name_edit = resp.data.program_name_edit;*/

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

            var url = 'api/MstApplication360/TempClear';
            SocketService.get(url).then(function (resp) {
            });
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
            $state.go('app.MstIndividualDocument');

        }
        $scope.submit = function () {

            var documenttype_Name = $('#Document :selected').text();
            var documentseverity_Name = $('#Severity :selected').text();
            if (($scope.$parent.cboprogram == undefined) || ($scope.$parent.cboprogram == '') || ($scope.$parent.cboprogram == null)) {

                Notify.alert('Select Atleast One Program', 'warning');
                return;
            }
            lockUI();
           
            var params = {

                individualdocument_gid: individualdocument_gid,
                individualdocument_name: $scope.txteditindividualDocument_name,
                documenttypes_gid: $scope.cbodocument,
                documenttype_name: documenttype_Name,
                documentseverity_gid: $scope.cboseverity,
                documentseverity_name: documentseverity_Name,
                CboProgram_list: $scope.$parent.cboprogram,
                lms_code: $scope.txtlms_code,
                bureau_code: $scope.txtbureau_code,
                document_code:$scope.txtdocumentcode,
                display_order:$scope.txtdisplayorder,
                document_remarks:$scope.txtremarks,
                covenant_type: $scope.rdbCovenant_type,
                Status: $scope.documentStatus
            }
            console.log(params);
            var url = 'api/MstApplication360/UpdateIndividualDocument';
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $state.go('app.MstIndividualDocument');
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
            var params = {

                individualdocument_gid: individualdocument_gid
            };
            var url = 'api/MstApplication360/GetCheckListTempEditList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.checklistedit_list = resp.data.application_list;
                unlockUI();
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
