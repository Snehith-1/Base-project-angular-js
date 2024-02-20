(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstProgramViewController', MstProgramViewController);

    MstProgramViewController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService', 'cmnfunctionService'];

    function MstProgramViewController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstProgramViewController';
        $scope.program_gid = $location.search().lsprogram_gid;
        var program_gid = $scope.program_gid;

        lockUI();
        activate();
        function activate() {
            var param = {
                program_gid: program_gid
            };
            var url = 'api/MstApplication360/GetProgram2ProductEditList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.loanproduct_list = resp.data.loanproduct_list;
                unlockUI();
            });

            var url = 'api/MstApplication360/GetProgramDocumentEditList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.upload_list = resp.data.uploadprogramdocumentlist;
                unlockUI();
            });

            var params = {
                program_gid: program_gid
            }
            var url = 'api/MstApplication360/EditProgram';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtprogram_refno = resp.data.program_refno;
                $scope.approved_date = resp.data.approved_date;

                $scope.txtdescription = resp.data.program_description;
                $scope.txtprogram_limit = resp.data.program_limit;
                $scope.txtmax_limit = resp.data.maximum_limit;
                $scope.lblentity = resp.data.entity_name;
                $scope.txtprogram_name = resp.data.program;
                $scope.txtlms_code = resp.data.lms_code;
                $scope.txtbureau_code = resp.data.bureau_code;
                $scope.program_gid = resp.data.program_gid;
                              
            });

            var params = {
                program_gid: program_gid
            }

            var url =  'api/MstApplication360/GetProgramMultiselectList';

            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.cboapproved_edit = resp.data.employee_name;
                $scope.cbovertical_edit = resp.data.vertical_name;
               
            });

            var url = 'api/MstApplication360/GetProgramLog';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.productlog_list = resp.data.application_list;
                unlockUI();
            });
        }

        $scope.downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.back = function () {
            $state.go('app.MstProgram');
        }

        $scope.downloadall = function () {
            for (var i = 0; i < $scope.upload_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.upload_list[i].document_path, $scope.upload_list[i].document_name);
            }
        }

        $scope.documentviewer = function (val1, val2) {
            lockUI();
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
            if (IsValidExtension == false) {
                Notify.alert("View is not supported for this format..!", {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                unlockUI();
                return false;
            }
            DownloaddocumentService.DocumentViewer(val1, val2);
        }
    }
})();