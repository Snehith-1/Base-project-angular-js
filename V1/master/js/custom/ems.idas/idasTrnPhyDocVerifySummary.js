(function () {
    'use strict';

    angular
        .module('angle')
        .controller('idasTrnPhyDocVerifySummary', idasTrnPhyDocVerifySummary);

    idasTrnPhyDocVerifySummary.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','DownloaddocumentService'];

    function idasTrnPhyDocVerifySummary($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,DownloaddocumentService) {
        $scope.title = 'idasTrnPhyDocVerifySummary';
     
        activate();

        function activate() {
            $scope.tab = {};

            var url = window.location.href;
            var relPath = url.split("lstab=");
            var relpath1 = relPath[1];
            if (relpath1 != undefined)
            {
                if(relpath1=="pending")
                {
                    $scope.tabpending = true;
                }
                else if(relpath1=="created")
                {
                    $scope.tabcreated = true;
                }
               
            }
            else {
               
                $scope.tabpending = true;
            }


            $scope.totalDisplayedPending = 100;
            $scope.totalDisplayedCreated = 100;
            lockUI();
            var url = "api/IdasTrnPhyDoc/PhysicalDocumentPendingSummary";
            SocketService.get(url).then(function (resp) {
               
                $scope.batch_pending = resp.data.MdlPhyDocSummary;
              
                if($scope.batch_pending==null){
                    $scope.total_pending=0;
                }
                else{
                    $scope.total_pending = $scope.batch_pending.length;
                }
                

            });
            unlockUI();
            var url = "api/IdasTrnPhyDoc/PhysicalDocumentCreatedSummary";
            lockUI();
            SocketService.get(url).then(function (resp) {
               
                $scope.batch_created = resp.data.MdlPhyDocSummary;
                console.log(resp.data);
                if( $scope.batch_created==null){
                    $scope.total_created=0;
                }
                else{
                    $scope.total_created = $scope.batch_created.length;
                }
              

            });
            unlockUI(); 
            var url = window.location.href;
            var relPath = url.split("lstab=");
            var relpath1 = relPath[1];
            if (relpath1 != undefined) {
                if (relpath1 == "pending") {
                    $scope.tabpending = true;
                }
                else if (relpath1 == "completed") {
                    $scope.tabcreated = true;
                }
              
            }
            else {
                if ($scope.tab.activeTabId == undefined) {
                    $scope.tabpending = true;
                }
                else if ($scope.tab.activeTabId == 'pending') {
                    $scope.tabpending = true;

                }
                else if ($scope.tab.activeTabId == 'completed') {
                    $scope.tabcreated = true;
                }
               
            }

          
        }
       
        $scope.loadMorePending = function (pagecount) {
            if(pagecount==undefined){
                Notify.alert("Enter the Total Summary Count","warning");
                return;
            }
            lockUI();
            var Number = parseInt(pagecount);
            
            $scope.totalDisplayedPending += Number;
            unlockUI();
        };
        $scope.loadMoreCreated= function (pagecount) {
            if(pagecount==undefined){
                Notify.alert("Enter the Total Summary Count","warning");
                return;
            }
            lockUI();
            var Number = parseInt(pagecount);
            
            $scope.totalDisplayedCreated += Number;
            unlockUI();
        };
        $scope.GetPdf = function (val) {
            var params = {
                sanction_gid: val
            };
            var url = 'api/idasTrnMakerCheckerDtls/GetComplaintCertificatePdf';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    DownloaddocumentService.Downloaddocument(resp.data.file_path, "Compliance Certificate Report.pdf");
                    Notify.alert('Compliance Certificate Report Downloaded Successfully', 'success');
                }
                else {
                    unlockUI();
                    Notify.alert('Error Occurred While Export PDF !', 'warning');
                }
            });

        }
        $scope.docVerifyPending = function (sanction_gid) {
            localStorage.setItem('sanction_gid', sanction_gid);
            $location.url('app/idasTrnPhyDocVerification?lstab=pending');
        }
        $scope.docVerifyCreated = function (sanction_gid) {
            localStorage.setItem('sanction_gid', sanction_gid);
           $location.url('app/idasTrnPhyDocVerification?lstab=created');
        }
        $scope.export = function (sanction_gid) {
            lockUI();
            var params = {
                sanction_gid: sanction_gid,
                type_of_copy: 'Scan Copy'
            }
            var url = 'api/IdasTrnLsaManagement/ExportDocumentCoversation';
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    DownloaddocumentService.Downloaddocument(resp.data.attachment_path, resp.data.attachment_name);
                    // var phyPath = resp.data.attachment_path;
                    // var relPath = phyPath.split("EMS");
                    // var relpath1 = relPath[1].replace("\\", "/");
                    // var hosts = window.location.host;
                    // var prefix = location.protocol + "//";
                    // var str = prefix.concat(hosts, relpath1);
                    // var link = document.createElement("a");
                    // var name = resp.data.attachment_name.split('.');
                    // link.download = name[0];
                    // var uri = str;
                    // link.href = uri;
                    // link.click();

                }
                else {
                    Notify.alert(resp.data.message, 'success')
                }

            });
        }
    }
})();
