(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstVisitorReportController', MstVisitorReportController);

    MstVisitorReportController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'DownloaddocumentService'];

    function MstVisitorReportController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstVisitorReportController';
        activate();

        function activate() {
            var params ={
                branch_gid:null
            }
            var url = 'api/MstVisitor/GetVisitorManage';
            lockUI();
            SocketService.getparams(url,params).then(function (resp) {
                $scope.visitor_list = resp.data.visitor_list;
                unlockUI();
            });
    
            var url = 'api/MstVisitor/Branch';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.branch_list = resp.data.branchname_list;
                unlockUI();
            });
        }

        $scope.all = function () {
            $scope.cbobranch_name = '';
            activate();
        }

        $scope.edit = function (visitor_gid) {
            var url = 'api/MstVisitor/GetVisitorTempClear';
            SocketService.get(url).then(function (resp) { });
            $location.url('app/MstVisitorEdit?visitor_gid=' + visitor_gid);
        }
        $scope.branchsearch = function (branch_name) {
            var params = {
                branch_gid : branch_name
            }
            var url = 'api/MstVisitor/GetVisitorManage';
            SocketService.getparams(url,params).then(function (resp) {
                $scope.visitor_list = resp.data.visitor_list;
                unlockUI();
            });
    
        }


        $scope.exportvisitorreport = function () {
            lockUI();
            var url = 'api/MstVisitor/VisitorExportExcel';
            SocketService.get(url).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    DownloaddocumentService.Downloaddocument(resp.data.lscloudpath, resp.data.lsname);
                    //var phyPath = resp.data.lspath;
                    //var relPath = phyPath.split("EMS");
                    //var relpath1 = relPath[1].replace("\\", "/");
                    //var hosts = window.location.host;
                    //var prefix = location.protocol + "//";
                    //var str = prefix.concat(hosts, relpath1);
                    //var link = document.createElement("a");
                    //var name = resp.data.lsname.split('.');
                    //link.download = name[0];
                    //var uri = str;
                    //link.href = uri;
                    //link.click();
                }
                else {
                    unlockUI();
                    Notify.alert('Error Occurred While Export !')

                }

            });
        }

       
       
        //$scope.downloadpdf = function (visitor_gid) {

        //    var params = {
        //        visitor_gid: visitor_gid
        //    }

        //    var url = 'api/MstVisitor/GetVistorTagpdf';
        //    lockUI();
        //    SocketService.getparams(url, params).then(function (resp) {
        //        var phyPath = resp.data;
        //        //var relPath = phyPath.split("EMS");
        //        //var relpath1 = relPath[1].replace("\\", "/");
        //        //var hosts = window.location.host;
        //        //var prefix = location.protocol + "//";
        //        //var str = prefix.concat(hosts, relpath1);
        //        //var link = document.createElement("a");
        //        //link.download = "Visitor Form";
        //        //var uri = str;
        //        //link.href = uri;
        //        //link.click();

        //        var phyPath = phyPath.replace("\\", "/");;
        //        var relPath = phyPath.split("EMS/");
        //        var relpath1 = relPath[1].replace("\\", "/");
        //        var url1 = relpath1;
        //        var filename = url1.substring(url1.lastIndexOf('/') + 1);



        //        var url = 'api/azurestorage/FileUploadDocument';
        //        var params = {
        //            file_path: relpath1
        //        }
        //        SocketService.post(url, params).then(function (resp) {
        //            if (resp.data.status == true) {
        //                DownloaddocumentService.Downloaddocument(relpath1, filename);
        //                Notify.alert('Visitor Form Downloaded Successfully', 'success')
        //                unlockUI();
        //            }
        //            else {
        //                unlockUI();
        //                Notify.alert('Error Occurred While Export PDF !', 'warning');
        //            }
        //        });
                
        //    });

        //}
        $scope.downloadpdf = function (visitor_gid) {
            lockUI();
            var params = {
                visitor_gid: visitor_gid
            }
            var url = 'api/MstVisitor/GetVistorTagpdf';
            SocketService.getparams(url, params).then(function (resp) {

                if (resp.data.status == true) {
                    var filepath = resp.data.file_path;
                    var filename = resp.data.file_name;
                    DownloaddocumentService.Downloaddocument(filepath, filename);
                    Notify.alert('Visitor Form Downloaded Successfully', 'success')
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert('Error Occurred While Export PDF !', 'warning');
                }

            });

        }

        $scope.visitor_view = function (visitor_gid) {
            $location.url('app/MstVisitorView?visitor_gid=' + visitor_gid + '&lstab=VisitorReport');
        }

    }
})();
