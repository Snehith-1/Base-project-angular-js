(function () {
    'use strict';

    angular
        .module('angle')
        .controller('sopdocumentsummary', sopdocumentsummary);

    sopdocumentsummary.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$filter', 'ngTableParams', '$resource', '$timeout', 'ngTableDataService', 'DownloaddocumentService', 'cmnfunctionService'];

    function sopdocumentsummary($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $filter, ngTableParams, $resource, $timeout, ngTableDataService, DownloaddocumentService, cmnfunctionService) {
        $scope.title = 'sopdocumentsummary';

        activate();

        function activate() {

            $scope.sopdepartment_gid = $location.search().sopdepartment_gid;
            var sopdepartment_gid = $scope.sopdepartment_gid;

            $scope.totalDisplayed = 100;
            lockUI();
            var params = {
                sopdepartment_gid: $scope.sopdepartment_gid
            }
            var url = "api/MstSOPDocumentUpload/GetSOPdocument_list";
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.SOPDocument_upload = resp.data.SOPDocument_upload;
                $scope.sopdepartment_name = resp.data.sopdepartment_name;
                $scope.total = $scope.SOPDocument_upload.length;
            });
            var url = "api/MstSOPDocumentUpload/GetITflag";
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.department_flag = resp.data.department_flag;

            });
        }
        //document.getElementById('pagecount').onkeyup = function () {
        //    if ($scope.pagecount == null) {
        //        var el = document.getElementById('loadmore');
        //        el.style.backgroundColor = '#DCDCDC';
        //    }
        //    else {
        //        var el = document.getElementById('loadmore');
        //        el.style.backgroundColor = '#ffa';
        //    }
        //};
        $scope.loadMore = function (pagecount) {
            lockUI();
            var Number = parseInt(pagecount);
            $scope.totalDisplayed += Number;
            unlockUI();
        };
        $scope.downloads = function (val1, val2) {
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

        $scope.viewdocument = function () {
            $state.go('app.MstDocumentDownload')
        }
        $scope.back = function () {
            $state.go('app.MstDocumentUploadSummary')
        }
        $scope.delete = function (sopdocumentupload_gid) {
            var params = {
                sopdocumentupload_gid: sopdocumentupload_gid
            }
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do You Want To Delete the Record ?',
                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Yes, delete it!',
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    var url = "api/MstSOPDocumentUpload/DeleteSOPDocument";
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                        }
                        else {
                            Notify.alert('Error Occurred While Deleting SOP Document!', {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                    });
                    SweetAlert.swal('Deleted Successfully!');
                }

            });
        };
    }
})();
