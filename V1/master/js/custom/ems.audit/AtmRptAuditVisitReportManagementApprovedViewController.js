(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AtmRptAuditVisitReportManagementApprovedViewController', AtmRptAuditVisitReportManagementApprovedViewController);

    AtmRptAuditVisitReportManagementApprovedViewController.$inject = ['$rootScope', '$scope', '$state', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'DownloaddocumentService', 'cmnfunctionService'];


    function AtmRptAuditVisitReportManagementApprovedViewController($rootScope, $scope, $state, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, DownloaddocumentService, cmnfunctionService) {

        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AtmRptAuditVisitReportManagementApprovedViewController';
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);

        var auditvisit_gid = searchObject.auditvisit_gid;
        activate();

        function activate() {

            var params = {
                auditvisit_gid: auditvisit_gid
            }
            var url = 'api/AtmRptAuditReports/GetAuditVisitReportDtls';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtdate_ofvisit = resp.data.auditvisit_date;
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
                $scope.txtentity_name = resp.data.entity_name;
                $scope.reportingmanager_name = resp.data.reportingmanager_name;
                $scope.txtsamfincustomer_name = resp.data.samfincustomer_name;
                $scope.txtsamagrocustomer_name = resp.data.samagrocustomer_name;
                //$scope.lsfilename = resp.data.filesname;
                //$scope.lsfilepath = resp.data.filepath;
                //$scope.UploadDocumentList = resp.data.UploadDocumentList;
                //$scope.UploadphotoList = resp.data.UploadphotoList;
                unlockUI();
            });
            var url = 'api/AtmRptAuditReports/GetAuditVisitPhotosList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.lsfilename = resp.data.filesname;
                $scope.lsfilepath = resp.data.filepath;
                $scope.UploadphotoList = resp.data.UploadphotoList;

            });

            var url = 'api/AtmRptAuditReports/GetAuditVisitDocumentList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.lsfilename = resp.data.filesname;
                $scope.lsfilepath = resp.data.filepath;
                $scope.UploadDocumentList = resp.data.UploadDocumentList;

            });
        }

        $scope.visitreportmobileno_view = function (auditvisit2person_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/mobileno_summary.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    auditvisit2person_gid: auditvisit2person_gid

                }
                var url = 'api/AtmRptAuditReports/GetAuditVisitContactList';
                lockUI();
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
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

            }
        }

        $scope.Back = function () {
            $location.url('app/AtmRptAuditVisitReportManagementApprovedSummary');
        }

        $scope.Document_Downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.photo_downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
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
        $scope.downloadall = function (val1, val2) {

            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }

        }
        $scope.documentphotoviewer = function (val1, val2) {
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
        $scope.downloadall1 = function (val1, val2) {

            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }

        }
        $scope.StaticMapAndPhotos_View = function (latitude, longitude, address_line1, address_line2, postal_code) {
            var modalInstance = $modal.open({
                templateUrl: '/StaticMapAndPhotosView.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    latitude: latitude,
                    longitude: longitude
                }
                var url = 'api/GoogleMapsAPI/GetStaticMapUrl';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.staticmapImgUrl = resp.data;
                });
                if (address_line2 == '') {
                    var addressString = ''.concat(address_line1.toString(), ",", postal_code.toString());
                } else {
                    var addressString = ''.concat(address_line1.toString(), ",", address_line2.toString(), ",", postal_code.toString());
                }
                var params = {
                    address: addressString
                }
                var url = 'api/GoogleMapsAPI/GetPlaceImage';
                SocketService.getparams(url, params).then(function (resp) {
                    var photoUrlArray = [];
                    for (var i = 0; i < resp.data.length; i++) {
                        if (resp.data[i] != null) {
                            photoUrlArray[i] = resp.data[i];
                        }
                    }
                    if (photoUrlArray.length == 0) {
                        $scope.photoNotFound = true;
                    } else {
                        $scope.photoUrlList = photoUrlArray;
                        $scope.photoFound = true;
                    }
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }
        $scope.downloadall = function () {
            for (var i = 0; i < $scope.UploadDocumentList.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.UploadDocumentList[i].document_path, $scope.UploadDocumentList[i].filename);
            }
        }
        $scope.downloadall_photo = function () {
            for (var i = 0; i < $scope.UploadphotoList.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.UploadphotoList[i].document_path, $scope.UploadphotoList[i].filename);
            }
        }
    }
})();
