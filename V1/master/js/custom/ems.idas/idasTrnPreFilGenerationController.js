(function () {
    'use strict';

    angular
        .module('angle')
        .controller('idasTrnPreFilGenerationController', idasTrnPreFilGenerationController);

        idasTrnPreFilGenerationController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams','DownloaddocumentService'];

    function idasTrnPreFilGenerationController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams,DownloaddocumentService) {
        $scope.title = 'idasTrnPreFilGenerationController';
        
        $scope.sanction_gid = $location.search().sanction_gid;
        var lspage = $location.search().lspage;
     
        activate();

        function activate() {
            lockUI();
            var params = {
                sanction_gid: $scope.sanction_gid
            }
            var url = 'api/IdasMstDocList/GetDocument2SanctionList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.document2sanction_list = resp.data.IDASDocument;
            });

            var url = 'api/IdasTrnSanctionDoc/SanctionDtlsView';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.sanctionrefno = resp.data.sanctionrefno;
                $scope.customerName = resp.data.customerName;
                $scope.Customerurn = resp.data.Customerurn;
            });
        }

       $scope.generate = function(val, val1, val2, val3){
           $location.url('app/idasTrnDocumentGeneration?documentlist_gid=' + val + '&sanction_gid=' + val1 + '&document_code=' + val2 + '&doctemplate_flag=' + val3 + '&lspage=' + lspage);
       }

       $scope.WordGenerate = function (documentlist_gid, sanction_gid) {
           var params = {
               documentlist_gid: documentlist_gid,
               sanction_gid: sanction_gid
           };
           var url = 'api/IdasMstDocList/GetDocWordGenerate';
           lockUI();
           SocketService.getparams(url, params).then(function (resp) {
               if (resp.data.status == true) {
                var phyPath = resp.data.lspath;
                var filename1 = resp.data.lsname;
                var phyPath = phyPath.replace("\\", "/");
                var phyPath = phyPath.replace("//", "/");
                var relPath = phyPath.split("EMS/");
                var relpath1 = relPath[1].replace("\\", "/");
                var url1 = filename1;
                var filename = url1.substring(url1.lastIndexOf('/')+1);                                                                      
               var url = 'api/azurestorage/FileUploadDocument';
                var params = {
                    file_path : relpath1
                }
                SocketService.post(url,params).then(function (resp) {
                if (resp.data.status == true) {
                    DownloaddocumentService.Downloaddocument(relpath1, filename1);
                    
                }
                else {
                    unlockUI();
                    Notify.alert('Error Occurred While Export PDF !', 'warning');
                }
                });
                //    var phyPath = resp.data.lspath;
                //    var relPath = phyPath.split("EMS");
                //    var relpath1 = relPath[1].replace("\\", "/");
                //    var hosts = window.location.host;
                //    var prefix = location.protocol + "//";
                //    var str = prefix.concat(hosts, relpath1);
                //    var link = document.createElement("a");
                //    var name = resp.data.lsname.split('.');
                //    link.download = name[0];
                //    var uri = str;
                //    link.href = uri;
                //    link.click();
                   unlockUI();
               }
               else {
                   unlockUI();
                   Notify.alert('Error Occurred While Downloading !', 'warning')
                   activate();

               }
           });
       }

       $scope.gotoback = function () {
           if (lspage == 'generatedprefil') {
               $state.go('app.idasTrnPreFilManagement');
           }
           else {
               $state.go('app.idasTrnSanctionMgmt');
           }
       }
    }
})();