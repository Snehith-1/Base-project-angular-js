(function () {
    'use strict';

    angular
        .module('angle')
        .controller('IdasTrnBatchConversationView', IdasTrnBatchConversationView);

    IdasTrnBatchConversationView.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$filter', 'ngTableParams', '$resource', '$timeout', 'ngTableDataService', 'DownloaddocumentService'];

    function IdasTrnBatchConversationView($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $filter, ngTableParams, $resource, $timeout, ngTableDataService, DownloaddocumentService) {
        $scope.title = 'IdasTrnBatchConversationView';
        var sanctiondocument_gid;

        activate();

        function activate() {
            sanctiondocument_gid = localStorage.getItem('sanctiondocument_gid');

            var url = 'api/IdasTrnPhyDoc/PhyDocConversationExternal';
            var params = {
                sanctiondocument_gid: sanctiondocument_gid
            };
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    $scope.docconversation = resp.data.MdlDocConversation;

                } else {


                }


            });


            $scope.typeofcopy = 'Scan Copy';
            var url = 'api/IdasTrnSanctionDoc/GetDocDetailsView';
            var params = {
                sanctiondocument_gid: sanctiondocument_gid
            };
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();

                $scope.sanction_gid = resp.data.sanction_gid;
                $scope.document_gid = resp.data.document_gid;
                $scope.document_code = resp.data.document_code;
                $scope.document_name = resp.data.document_name;
                $scope.phydocument_date = resp.data.phydocument_date;
                $scope.documentrecord_id = resp.data.documentrecord_id;
                $scope.phyfinal_remarks = resp.data.phyfinal_remarks;
                $scope.scanfinal_remarks = resp.data.scanfinal_remarks;
                $scope.maker_status = resp.data.maker_status;
                $scope.checker_status = resp.data.checker_status;
                $scope.phydoc_status = resp.data.phydoc_status;
                $scope.types_of_copy = resp.data.phydocument_type;

            });

            var url = 'api/IdasTrnSanctionDoc/GetDocComments';
            SocketService.getparams(url, params).then(function (resp) {

                $scope.doc_comments = resp.data.doc_comments;

            });

            var url = 'api/IdasTrnSanctionDoc/ScanDocConversationInternal';
            var params = {
                sanctiondocument_gid: sanctiondocument_gid
            };
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    $scope.docconversationlistInternal = resp.data.MdlDocConversation;

                    $scope.valueInternal = true;
                } else {
                    $scope.valueInternal = false;

                }


            });

            var url = 'api/IdasTrnSanctionDoc/ScanDocConversationExternal';
            var params = {
                sanctiondocument_gid: sanctiondocument_gid
            };
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    $scope.docconversationlistExternal = resp.data.MdlDocConversation;
                    $scope.valueExternal = true;
                } else {
                    $scope.valueExternal = false;

                }


            });


        }


        $scope.PopupDownload = function (docconversation_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/mailconversation.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.downloads = function (val1, val2) {

                    //var phyPath = val1;

                    //var relPath = phyPath.split("EMS");
                    //var relpath1 = relPath[1].replace("\\", "/");
                    //var hosts = window.location.host;
                    //var prefix = location.protocol + "//";
                    //var str = prefix.concat(hosts, relpath1);
                    //var link = document.createElement("a");
                    //var name = val2.split(".")
                    //link.download = val2;
                    //var uri = str;
                    //link.href = uri;
                    //link.click();
                    DownloaddocumentService.Downloaddocument(val1, val2);
                }

                var url = "api/IdasTrnDocConversation/GetUploadDoc";
                var params = {
                    docconversation_gid: docconversation_gid
                };
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.viewDocumentList = resp.data.uploaddocument;

                });
            }
        }

        $scope.docconback=function()
        {
            $state.go('app.IdasTrnBatchView');
        }

    }
})();
