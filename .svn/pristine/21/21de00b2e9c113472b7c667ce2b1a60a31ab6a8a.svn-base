(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstApplCreationVisitReportViewController', AgrMstApplCreationVisitReportViewController);

    AgrMstApplCreationVisitReportViewController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$filter', 'ngTableParams', '$resource', '$timeout', 'ngTableDataService', '$anchorScroll', '$sce', 'DownloaddocumentService', 'cmnfunctionService'];

    function AgrMstApplCreationVisitReportViewController($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $filter, ngTableParams, $resource, $timeout, ngTableDataService, $anchorScroll, $sce, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstApplCreationVisitReportViewController';
        var visitreport_gid = localStorage.getItem('visitreport_gid');

        lockUI();
        activate();

        function activate() {

            var params = {
                visitreport_gid: visitreport_gid
            }
            var url = 'api/AgrMstApplicationView/GetVisitReportDtls';

            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.visitreport_id = resp.data.visitreport_id;
                $scope.txtdate_ofvisit = resp.data.applicationvisit_date;
                $scope.txtname_inspecting = resp.data.inspectingofficials_name;
                $scope.txtwhere_visitdone = resp.data.visitdone_name;
                $scope.Visitpersondtl_list = resp.data.mstVisitpersondtl_list;
                $scope.Visitpersonaddress_list = resp.data.mstVisitpersonaddress_list;
                $scope.txtkmp_activities = resp.data.clientkmp_activities;
                $scope.txtpromoter_background = resp.data.promoter_background;
                $scope.txtoverall_onservation = resp.data.overall_observations;
                $scope.txtinspecting_officials = resp.data.inspectingofficial_recommenation;
                $scope.txttrading_relationship = resp.data.trading_relationship;
                $scope.txtsummary = resp.data.summary;
                $scope.UploadDocumentList = resp.data.UploadDocumentList;
                $scope.UploadphotoList = resp.data.UploadphotoList;
                unlockUI();
            });

        }

        $scope.visitreportmobileno_view = function (applicationvisit2person_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/mobileno_summary.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


                var params = {
                    applicationvisit2person_gid: applicationvisit2person_gid

                }
                lockUI();
                var url = 'api/AgrMstApplicationView/GetVisitContactList';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        $scope.Visitpersoncontact_list = resp.data.mstVisitpersoncontact_list;
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        addresslist();
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'info',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                });

                $modalInstance.close('closed');


            }
        }

        $scope.visitdoc_download = function (val1, val2) {
            //var phyPath = val1;
            //var relPath = phyPath.split("EMS");
            //var relpath1 = relPath[1].replace("\\", "/");
            //var hosts = window.location.host;
            //var prefix = location.protocol + "//";
            //var str = prefix.concat(hosts, relpath1);
            //var link = document.createElement("a");
            //link.download = val2;
            //var uri = str;
            //link.href = uri;
            //link.click();
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.visitphoto_download = function (val1, val2) {
            //var phyPath = val1;
            //var relPath = phyPath.split("EMS");
            //var relpath1 = relPath[1].replace("\\", "/");
            //var hosts = window.location.host;
            //var prefix = location.protocol + "//";
            //var str = prefix.concat(hosts, relpath1);
            //var link = document.createElement("a");
            //link.download = val2;
            //var uri = str;
            //link.href = uri;
            //link.click();
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.close = function () {
            window.close();
        }
        $scope.downloadall_3 = function () {
            for (var i = 0; i < $scope.UploadDocumentList.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.UploadDocumentList[i].document_path, $scope.UploadDocumentList[i].filename);
            }
        }
        $scope.downloadall_4 = function () {
            for (var i = 0; i < $scope.UploadphotoList.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.UploadphotoList[i].document_path, $scope.UploadphotoList[i].filename);
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
