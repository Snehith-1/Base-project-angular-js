(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstTaggedVisitorSummaryController', MstTaggedVisitorSummaryController);

    MstTaggedVisitorSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'DownloaddocumentService'];

    function MstTaggedVisitorSummaryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstTaggedVisitorSummaryController';

        activate();
        lockUI();
        function activate() {
            var url = 'api/MstVisitor/GetTaggedVisitor';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.visitor_list = resp.data.visitor_list;
            });
            var today = new Date();

            var date = today.getDate() + '-' + (today.getMonth() + 1) + '-' + today.getFullYear();

            $scope.txtvisit_date = date;
        }

        $scope.addvisitor = function () {
            var url = 'api/MstVisitor/GetVisitorTempClear';
            SocketService.get(url).then(function (resp) { });

            $location.url("app/MstVisitorAdd");
        }
        $scope.edit = function (visitor_gid) {
            var url = 'api/MstVisitor/GetVisitorTempClear';
            SocketService.get(url).then(function (resp) { });

            $location.url('app/MstVisitorEdit?visitor_gid=' + visitor_gid);
        }

        $scope.delete = function (visitor_gid) {
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do You Want To Delete the Visitor Form ?',
                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Yes, delete it!',
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    var params = {
                        visitor_gid: visitor_gid
                    }
                    var url = 'api/MstVisitor/DeleteVisitor';
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
                        activate();
                    });
                    SweetAlert.swal('Deleted Successfully!');
                }
            });
        }

        $scope.tagged_visitor = function () {
            $location.url('app/MstTaggedVisitorSummary');
        }

        $scope.history_visitor = function () {
            $location.url('app/MstHistoryVisitorSummary');
        }

        $scope.downloadpdf = function (visitor_gid) {

            var params = {
                visitor_gid: visitor_gid
            }

            var url = 'api/MstVisitor/GetVistorTagpdf';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                var phyPath = resp.data;
                //var relPath = phyPath.split("EMS");
                //var relpath1 = relPath[1].replace("\\", "/");
                //var hosts = window.location.host;
                //var prefix = location.protocol + "//";
                //var str = prefix.concat(hosts, relpath1);
                //var link = document.createElement("a");
                //link.download = "Visitor Form";
                //var uri = str;
                //link.href = uri;
                //link.click();
                //Notify.alert('Visitor Form Downloaded Successfully', 'success')
                //unlockUI();

                var phyPath = phyPath.replace("\\", "/");;
                var relPath = phyPath.split("EMS/");
                var relpath1 = relPath[1].replace("\\", "/");
                var url1 = filename1;
                var filename = url1.substring(url1.lastIndexOf('/') + 1);



                var url = 'api/azurestorage/FileUploadDocument';
                var params = {
                    file_path: relpath1
                }
                SocketService.post(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        DownloaddocumentService.Downloaddocument(relpath1, filename);
                        Notify.alert('Visitor Form Downloaded Successfully', 'success')
                        unlockUI();
                    }
                    else {
                        unlockUI();
                        Notify.alert('Error Occurred While Export PDF !', 'warning');
                    }
                });
            });

        }

        $scope.visitor_view = function (visitor_gid) {
            $location.url('app/MstVisitorView?visitor_gid=' + visitor_gid + '&lstab=TaggedVisitor');
        }

    }
})();
