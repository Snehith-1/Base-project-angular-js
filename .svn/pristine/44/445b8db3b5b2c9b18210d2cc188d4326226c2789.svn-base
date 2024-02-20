(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstVisitorViewController', MstVisitorViewController);

    MstVisitorViewController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'DownloaddocumentService', 'cmnfunctionService'];

    function MstVisitorViewController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstVisitorViewController';
        $scope.visitor_gid = $location.search().visitor_gid;
        var visitor_gid = $scope.visitor_gid;
        $scope.lstab = $location.search().lstab;
        var lstab = $scope.lstab;
        lockUI();
        activate();
        function activate() {
            var params = {
                visitor_gid: $scope.visitor_gid
            }
            var url = 'api/MstVisitor/GetVisitorView';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.lblvisitor_id = resp.data.visit_id;
                $scope.lblbranch_name = resp.data.branch_name;
                $scope.lblvisit_date = resp.data.visit_date;
                $scope.lblvisit_type = resp.data.visiting_type;
                $scope.lblvisitingofficial_name = resp.data.visitofficer_name;
                $scope.lblin_time = resp.data.in_time;
                $scope.lbltentative_outtime = resp.data.tentative_out_time;
                $scope.lblactualout_time = resp.data.actual_out_time;
                $scope.lblpurposeof_visit = resp.data.purpose_of_visit;
                unlockUI();
            });

            var params = {
                visitor_gid: $scope.visitor_gid
            }
            var url = 'api/MstVisitor/GetVisitorNameViewList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.visitornameview_list = resp.data.visitornameview_list;
                unlockUI();
            });

        }

        $scope.upload_doc = function (visitorname_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/UploadDocuments.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                  {
                      visitorname_gid: visitorname_gid
                  }
                var url = 'api/MstVisitor/GetVisitorUploadDoc';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.UploadDocument_List = resp.data.UploadDocument_List;

                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
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
                
                $scope.download_doc = function (val1, val2) {
                    DownloaddocumentService.Downloaddocument(val1, val2);
                }
                $scope.downloadall = function () {
                    for (var i = 0; i < $scope.UploadDocument_List.length; i++) {
                        DownloaddocumentService.Downloaddocument($scope.UploadDocument_List[i].visitphoto_path, $scope.UploadDocument_List[i].file_name);

                    }
                }


            }

        }
      

        $scope.Back = function () {
            if (lstab == 'TodayVisitor') {
                $location.url('app/MstVisitorSummary');
            }
            else if (lstab == 'TaggedVisitor') {
                $state.go('app.MstTaggedVisitorSummary');
            }
            else if (lstab == 'HistoryVisitor') {
                $state.go('app.MstHistoryVisitorSummary');
            }
            else if (lstab == 'VisitorReport') {
                $state.go('app.MstVisitorManagementReport');
            }
            else {

            }

        }

    }
})();
