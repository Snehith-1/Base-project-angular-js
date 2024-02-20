(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstVisitReportViewController', AgrMstVisitReportViewController);

    AgrMstVisitReportViewController.$inject = ['$rootScope', '$scope', '$state', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'DownloaddocumentService', 'cmnfunctionService'];


    function AgrMstVisitReportViewController($rootScope, $scope, $state, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, DownloaddocumentService, cmnfunctionService) {

        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstVisitReportViewController';
        var application_gid = $location.search().application_gid;
        var applicationvisit_gid = $location.search().visitreport_gid;
        activate();

        function activate() {

            var params = {
                visitreport_gid: applicationvisit_gid
            }
            var url = 'api/AgrMstApplicationVisitReport/GetVisitReportDtls';

            SocketService.getparams(url, params).then(function (resp) {
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
                var params = {
                    applicationvisit2person_gid: applicationvisit2person_gid

                }
                var url = 'api/AgrMstApplicationVisitReport/GetVisitContactList';
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
            $location.url('app/AgrMstVisitReportAdd?visitreport_gid=' + applicationvisit_gid + '&?&application_gid=' + application_gid);
        }

        $scope.Document_Downloads = function (val1, val2) {
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

        $scope.photo_downloads = function (val1, val2) {
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
                var url = 'api/AgrGoogleMapsAPI/GetStaticMapUrl';
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
