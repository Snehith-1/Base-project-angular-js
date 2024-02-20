(function () {
    'use strict';

    angular
        .module('angle')
        .controller('osdCqmQueryAssign360Controller', osdCqmQueryAssign360Controller);

    osdCqmQueryAssign360Controller.$inject = ['DownloaddocumentService','$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams','$sce','cmnfunctionService'];
    function osdCqmQueryAssign360Controller( DownloaddocumentService ,$rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams, $sce,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        var email_gid = searchObject.email_gid;
        var status = searchObject.status;
        vm.title = 'osdCqmQueryAssign360Controller';

        activate();
        function activate() {
        
            $scope.transfer_status = status;
            $scope.trustAsHtml = function (string) { return $sce.trustAsHtml(string); };
            $scope.IsLogShow = false;
            $scope.Transferlist = true;

            if( $scope.transfer_status == 'Transfer') {
                $scope.Transferlist = false;
            }
            else {
                $scope.Transferlist = true;
            }

            var  params={
                email_gid: email_gid
            }
            var url = 'api/OsdTrnCustomerQueryMgmt/CustomerAssignedQuery360';
            SocketService.getparams(url,params).then(function (resp) {
                $scope.email_gid = resp.data.email_gid;
                $scope.ticketref_no = resp.data.ticketref_no;
                $scope.assigned_by = resp.data.assigned_by;
                $scope.assigned_date = resp.data.assigned_date;
                $scope.assigned_to = resp.data.assigned_to;
                $scope.email_from = resp.data.email_from;
                $scope.email_date = resp.data.email_date;
                $scope.status = resp.data.status;
                $scope.aging = resp.data.aging;
                $scope.email_subject = resp.data.email_subject;
                $scope.email_content = resp.data.email_content;
                $scope.email_to = resp.data.email_to;
                $scope.cc = resp.data.cc;
                $scope.bcc = resp.data.bcc;
                $scope.Query360list= resp.data.Query360list;
                $scope.assigned_remarks= resp.data.assigned_remarks;
                $scope.from_mailaddress = resp.data.from_mailaddress;
                $scope.attch_list = resp.data.MdlAttachmentList;
                unlockUI();
            }); 

            var url = 'api/OsdTrnCustomerQueryMgmt/CustomerQueryAttachments';
            SocketService.getparams(url,params).then(function (resp) {
                $scope.QueryAttachmentsList = resp.data.QueryAttachmentsList;
                unlockUI();
            });

            var url = 'api/OsdTrnCustomerQueryMgmt/PostAuditView';

            var params={
                email_gid: email_gid
            };
            SocketService.getparams(url,params).then(function (resp) {
                if(resp.data.status==true){  
                }
                else{

                }
                
            });

            var  params={
                email_gid: email_gid
            }
            var url = 'api/OsdTrnCustomerQueryMgmt/ComposeMail360';
            SocketService.getparams(url,params).then(function (resp) {
                $scope.MdlComposeMaillist = resp.data.MdlComposeMaillist;
            //    $scope.MdlAttachmentList = resp.data.MdlAttachmentList;
                unlockUI();
            });

            var url = 'api/OsdTrnCustomerQueryMgmt/TransferLog';
        
                var params={
                    lsemail_gid: email_gid
                };
            
                SocketService.getparams(url,params).then(function (resp) {
                    if(resp.data.status==true){
                       
                        $scope.MdlTransferLog = resp.data.MdlTransferLog;
                        if( $scope.MdlTransferLog  == null)
                        {
                            $scope.transfershow = true;
                        }
                        else{
                            $scope.transfershow = false;
                        }
                    }
                    else{
                       
                    }
                    
                }); 

        }
        $scope.Back= function(){
            $state.go('app.osdCqmAssignToQuery');
        }

        $scope.Download = function (path, attchment_name) {
            DownloaddocumentService.Downloaddocument(path, attchment_name);
            // var phyPath = path;
            // var relPath = phyPath.split("EMS");
            // var relpath1 = relPath[1].replace("\\", "/");
            // var hosts = window.location.host;
            // var prefix = location.protocol + "//";
            // var str = prefix.concat(hosts, relpath1);
            // var link = document.createElement("a");
            // var name = attchment_name.split('.');
            // link.download = name[0];
            // var uri = str;
            // link.href = uri;
            // link.click();
        }

        $scope.logdetails=function() {

            if($scope.IsLogShow == true) {
                $scope.IsLogShow = false; 
            }
            else {
                
                $scope.IsLogShow=true;
                
               var url = 'api/OsdTrnCustomerQueryMgmt/TransferLog';
        
                var params={
                    lsemail_gid: email_gid
                };
            
                SocketService.getparams(url,params).then(function (resp) {
                    if(resp.data.status==true){
                       
                        $scope.MdlTransferLog = resp.data.MdlTransferLog;
                        if( $scope.MdlTransferLog  == null)
                        {
                            $scope.transfershow = true;
                        }
                        else{
                            $scope.transfershow = false;
                        }
                    }
                    else{
                       
                    }
                    
                }); 
        
                var url = 'api/OsdTrnCustomerQueryMgmt/AuditLog';
        
                var params={
                    email_gid: email_gid
                };            
                SocketService.getparams(url,params).then(function (resp) {
                    if(resp.data.status==true){
                        $scope.auditlog_list = resp.data.MdlAuditLog;
                    }
                    else{
                       
                    }
                    
                });
            } 
        }
               
            
    }
})();