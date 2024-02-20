(function () {
    'use strict';

    angular
        .module('angle')
        .controller('osdCqmQueryTicketAssignmentController', osdCqmQueryTicketAssignmentController);

    osdCqmQueryTicketAssignmentController.$inject = ['DownloaddocumentService','$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams','$sce','cmnfunctionService'];

    function osdCqmQueryTicketAssignmentController(DownloaddocumentService,$rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams,$sce,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'osdCqmQueryTicketAssignmentController';
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        var email_gid = searchObject.email_gid;
        activate();
        function activate() {
            
            $scope.trustAsHtml = function (string) { return $sce.trustAsHtml(string); };

            var url = 'api/employee/Employee';
            SocketService.get(url).then(function (resp) {
                $scope.employee_list = resp.data.employee_list;
                console.log(resp.data.employee_list);
                unlockUI();
            });

            var url = 'api/OsdTrnCustomerQueryMgmt/PostAuditView';

            var params = {
                email_gid: email_gid
            };
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                }
                else {

                }

            });


            var  params={
                email_gid: email_gid
            }
            var url = 'api/OsdTrnCustomerQueryMgmt/CustomerQueryView';
            SocketService.getparams(url,params).then(function (resp) {
                $scope.email_gid = resp.data.email_gid;
                $scope.ticketref_no = resp.data.ticketref_no;
                $scope.email_from = resp.data.email_from;
                $scope.from_mailaddress = resp.data.from_mailaddress;
                $scope.email_to = resp.data.email_to;

                $scope.email_date = resp.data.email_date;
                $scope.email_subject = resp.data.email_subject;
                $scope.email_content = resp.data.email_content;
                $scope.aging = resp.data.aging;
                $scope.cc=resp.data.cc;
                $scope.bcc=resp.data.bcc;
                unlockUI();
            });  

         
            var url = 'api/OsdTrnCustomerQueryMgmt/CustomerQueryAttachments';
            SocketService.getparams(url,params).then(function (resp) {
                $scope.QueryAttachmentsList = resp.data.QueryAttachmentsList;
                unlockUI();
            });

            var url = 'api/OsdTrnCustomerQueryMgmt/ReferenceMail';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.frommail_id = resp.data.frommail_id;
                $scope.tomail_id = resp.data.tomail_id;
                $scope.ccmail_id = resp.data.ccmail_id;
                $scope.bccmail_id = resp.data.bccmail_id;
                $scope.mail_date = resp.data.mail_date;
                $scope.mail_subject = resp.data.mail_subject;
                $scope.mailcontent = resp.data.mailcontent;
                unlockUI();
            });
        }
        
            $scope.Back = function () {
                $state.go('app.osdCqmQueryAssignment');
            };
           $scope.AssignTicket = function () {
                
            };

            $scope.Download = function (val1, val2) {
                // var phyPath = val1;
                // var relPath = phyPath.split("EMS");
                // var relpath1 = relPath[1].replace("\\", "/");
                // var hosts = window.location.host;
                // var prefix = location.protocol + "//";
                // var str = prefix.concat(hosts, relpath1);
                // var link = document.createElement("a");
                // var name = val2.split(".")
                // link.download = val2;
                // var uri = str;
                // link.href = uri;
                // link.click();
                DownloaddocumentService.Downloaddocument(val1, val2);
            }

            $scope.assign_ticket = function() {
                var params={
                    email_gid: email_gid,
                    employee_gid: $scope.cboemployee_list.employee_gid,
                    employee_name: $scope.cboemployee_list.employee_name,
                    assigned_remarks: $scope.assigned_remarks
                }
                lockUI();
                    var url = "api/OsdTrnCustomerQueryMgmt/PostTicketAssign";
                    SocketService.post(url, params).then(function (resp) {
                   
                    if (resp.data.status == true) {
                        $state.go('app.osdCqmQueryAssignment');
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        unlockUI();
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'danger',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        unlockUI();
                    }
                });
                
            }
            
            $scope.logdetails=function() {

                if($scope.IsLogShow == true) {
                    $scope.IsLogShow = false; 
                }
                else {
                    
                    $scope.IsLogShow=true;
                                
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
        
    };

})();