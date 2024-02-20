(function () {
    'use strict';

    angular
        .module('angle')
        .controller('osdCqmForwardViewController', osdCqmForwardViewController);

        osdCqmForwardViewController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams', '$sce','cmnfunctionService'];

    function osdCqmForwardViewController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams, $sce,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'osdCqmForwardViewController';

        activate();
        function activate() {
            var email_gid = localStorage.getItem('email_gid');
            $scope.trustAsHtml = function (string) { return $sce.trustAsHtml(string); };
            $scope.IsLogShow = false;

            var url = 'api/OsdTrnCustomerQueryMgmt/PostAuditView';

            var params = {
                email_gid: localStorage.getItem("email_gid")
            };
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                }
                else {

                }

            });

            var params = {
                email_gid: email_gid
            }
            var url = 'api/OsdTrnCustomerQueryMgmt/CustomerAssignQueryForward360';
            SocketService.getparams(url, params).then(function (resp) {
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
                $scope.from_mailaddress = resp.data.from_mailaddress;
                $scope.QueryAssignForward360list = resp.data.QueryAssignForward360list;
                $scope.forward_by = resp.data.forward_by;
                $scope.forward_date = resp.data.forward_date;
                unlockUI();
            });

            var url = 'api/OsdTrnCustomerQueryMgmt/CustomerQueryAttachments';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.QueryAttachmentsList = resp.data.QueryAttachmentsList;
                unlockUI();
            });

            var  params={
                email_gid: email_gid
            }
            var url = 'api/OsdTrnCustomerQueryMgmt/ComposeMail360';
            SocketService.getparams(url,params).then(function (resp) {
                $scope.MdlComposeMaillist = resp.data.MdlComposeMaillist;
                $scope.MdlAttachmentList = resp.data.MdlAttachmentList;
                unlockUI();
            });
        }
        
        $scope.Back = function () {
            $state.go('app.osdCqmForwardSummary');
        }

        $scope.logdetails = function () {

            if ($scope.IsLogShow == true) {
                $scope.IsLogShow = false;
            }
            else {

                $scope.IsLogShow = true;

                var url = 'api/OsdTrnCustomerQueryMgmt/TransferLog';

                var params = {
                    lsemail_gid: localStorage.getItem("email_gid")
                };

                SocketService.getparams(url, params).then(function (resp) {
                    if (resp.data.status == true) {

                        $scope.MdlTransferLog = resp.data.MdlTransferLog;
                        if ($scope.MdlTransferLog == null) {
                            $scope.transfershow = true;
                        }
                        else {
                            $scope.transfershow = false;
                        }
                    }
                    else {

                    }

                });

                var url = 'api/OsdTrnCustomerQueryMgmt/AuditLog';

                var params = {
                    email_gid: localStorage.getItem("email_gid")
                };
                SocketService.getparams(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        $scope.auditlog_list = resp.data.MdlAuditLog;
                    }
                    else {

                    }

                });
            }
        }

        $scope.Download = function (path, attchment_name) {
            var phyPath = path;
            var relPath = phyPath.split("EMS");
            var relpath1 = relPath[1].replace("\\", "/");
            var hosts = window.location.host;
            var prefix = location.protocol + "//";
            var str = prefix.concat(hosts, relpath1);
            var link = document.createElement("a");
            var name = attchment_name.split('.');
            link.download = name[0];
            var uri = str;
            link.href = uri;
            link.click();
        }

        $scope.transfer = function (email_gid, ticketref_no, from_mailaddress, email_subject, assigned_to) {
           
            var modalInstance = $modal.open({
                templateUrl: '/transferContent.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            
            ModalInstanceCtrl.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];
            function ModalInstanceCtrl($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {
                var url = 'api/OsdTrnCustomerQueryMgmt/TransferEmployee';
                SocketService.get(url).then(function (resp) {
                    $scope.TransferEmployeeList = resp.data.TransferEmployeeList;
                    unlockUI();
                });

                    $scope.ticketref_no = ticketref_no;
                    $scope.subject = email_subject;
                    $scope.from = from_mailaddress;
                    $scope.checkeremployee_name = assigned_to;

                    $scope.transferWIUpdate = function () {

                        if ($scope.transfer_to == undefined) {
                            modalInstance.close('closed');
                            Notify.alert('Kindly Select the Assign to Person', 'warning');
                            return;
                        }

                        var params = {
                            email_gid: localStorage.getItem("email_gid"),
                            employee_gid: $scope.transfer_to,
                            employee_name: $('#transfer_to :selected').text(),
                            transfer_remarks:$scope.transfer_remarks
                        }

                        var url = "api/OsdTrnCustomerQueryMgmt/TicketTransfer";
                        lockUI();
                        SocketService.post(url, params).then(function (resp) {
                            unlockUI();
                            if (resp.data.status = true) {
                                modalInstance.close('closed');
                                Notify.alert(resp.data.message, 'success');
                                activate();
                                $state.go('app.osdCqmTransferSummary');
                            }
                            else {
                                modalInstance.close('closed');
                                Notify.alert(resp.data.message, 'warning');
                                activate();
                            }
                        });

                    }
                    var url = 'api/OsdTrnCustomerQueryMgmt/TransferLog';

                    var params = {
                        lsemail_gid:email_gid
                    }
    
                    SocketService.getparams(url, params).then(function (resp) {
                            $scope.MdlTransferLog = resp.data.MdlTransferLog;
                    });


                $scope.close = function () {
                    modalInstance.close('closed');
                };
            }
        }

        $scope.signature = function () {
            var modalInstance = $modal.open({
                templateUrl: '/signatureContent.html',
                controller: ModalInstanceCtrl,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var url = 'api/OsdTrnCustomerQueryMgmt/GetEmailSignature';
                SocketService.get(url).then(function (resp) {
                    
                    $scope.EmailSignature = resp.data.emailsignature;
                   
                });
                $scope.ok = function () {
                    modalInstance.close('closed');
                };
                $scope.close = function () {
                    modalInstance.close('closed');
                };
                $scope.submit = function () {
                    lockUI();
                    var params={
                        emailsignature:$scope.EmailSignature
                    }
            
                    var url = "api/OsdTrnCustomerQueryMgmt/PostEmailSignature";
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                         
                            $modalInstance.close('closed');
                            
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
                   
            }
        }

        $scope.close = function (email_gid, ticketref_no, from_mailaddress, email_subject) {
           
            var modalInstance = $modal.open({
                templateUrl: '/closeContent.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];
            function ModalInstanceCtrl($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {
                $scope.ticketref_no = ticketref_no;
                $scope.subject = email_subject;

                $scope.from = from_mailaddress;
                $scope.CloseWIUpdate = function () {

                    var params = {
                        email_gid: email_gid,
                        decision: 'Close',
                        employee_gid: '',
                        employee_name: '',
                        remarks: $scope.close_remarks,
                        mailcontent: 'Close',
                        subject: '',
                        tomail_id: '',
                        ccmail_id: '',
                        bccmail_id: ''
                    }


                    var url = 'api/OsdTrnCustomerQueryMgmt/PostDecision';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, 'success')
                            $state.go('app.osdCqmAssignCloseSummary');
                        }
                        else {
                            Notify.alert(resp.data.message, 'warning')
                        }
                        modalInstance.close('closed');
                        activate();
                    });
                }

                $scope.close = function () {
                    modalInstance.close('closed');
                };
            }
        }

        $scope.forward=function(){
            
            var decision = "Forward";
            var lspage = "forwardview";
            var llspage = "forward";
            $location.url('app/osdComposeMail?hash=' + cmnfunctionService.encryptURL('email_gid=' + email_gid + '&toMail=' + $scope.from_mailaddress + '&ccMail=' + $scope.cc + '&bccMail=' + $scope.bcc + '&email_subject=' + $scope.email_subject + '&message_id=' + $scope.message_id + '&reference_id= ' + $scope.reference_id + '&rmemployee_gid=' + $scope.rmemployee_gid + '&rmemployee_name=' + $scope.rmemployee_name + '&decision=' + decision + '&lspage=' + lspage + '&llspage=' + llspage));
        }

        $scope.Reply=function () {
           
            var decision = "Reply";
            var lspage = "forwardview";
            var llspage = "reply";
            $location.url('app/osdComposeMail?hash=' + cmnfunctionService.encryptURL('email_gid=' + email_gid + '&toMail=' + $scope.from_mailaddress + '&ccMail=' + $scope.cc + '&bccMail=' + $scope.bcc + '&email_subject=' + $scope.email_subject + '&message_id=' + $scope.message_id + '&reference_id= ' + $scope.reference_id + '&rmemployee_gid=' + $scope.rmemployee_gid + '&rmemployee_name=' + $scope.rmemployee_name + '&decision=' + decision + '&lspage=' + lspage + '&llspage=' + llspage));
        }
    }
})();
