(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstVisitorSummaryController', MstVisitorSummaryController);

        MstVisitorSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function MstVisitorSummaryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstVisitorSummaryController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;
        
        lockUI();
        activate();
        function activate() {
            var url = 'api/MstVisitor/GetVisitor';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.visitor_list = resp.data.visitor_list;
            });

            var url = 'api/MstVisitor/VisitorCount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.todayvisitor_count = resp.data.todayvisitor_count;
                $scope.taggedvisitor_count = resp.data.taggedvisitor_count;
                $scope.visitorhistory_count = resp.data.visitorhistory_count;
                $scope.lstotalcount = resp.data.totalvisitor_count;
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
            $state.go('app.MstTaggedVisitorSummary');
        }

        $scope.history_visitor = function () {
            $state.go('app.MstHistoryVisitorSummary');
        }

        $scope.visitor_view = function (visitor_gid) {
            $location.url('app/MstVisitorView?visitor_gid=' + visitor_gid + '&lstab=TodayVisitor');
        }

        $scope.downloadpdf = function (visitor_gid) {

            var params = {
                visitor_gid: visitor_gid
            }

            var url = 'api/MstVisitor/GetVistorTagpdf';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                var phyPath = resp.data;
                var relPath = phyPath.split("EMS");
                var relpath1 = relPath[1].replace("\\", "/");
                var hosts = window.location.host;
                var prefix = location.protocol + "//";
                var str = prefix.concat(hosts, relpath1);
                var link = document.createElement("a");
                link.download = "Visitor Form";
                var uri = str;
                link.href = uri;
                link.click();
                Notify.alert('Visitor Form Downloaded Successfully', 'success')
                unlockUI();
            });

        }
        
    }
})();