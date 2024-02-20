(function () {
    'use strict';

    angular
        .module('angle')
        .controller('isanconsolidatedview', isanconsolidatedview);

    isanconsolidatedview.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function isanconsolidatedview($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'isanconsolidatedview';

        var email_gid = $location.search().lsemail_gid;
        $scope.email_gid = email_gid;
        var lstab = $location.search().lstab;

        activate();

        function activate() {
            $scope.IsVisibleteam = false;
            $scope.IsVisibleemployee = false;
            $scope.pushback = false;
            $scope.forward = false;
            $scope.all = false;
            $scope.archival = false;
            $scope.typeE = "";
            $scope.IsLogShow = false;

            var url = 'api/IasnTrnWorkItem/WorkItemView';
            var params = {
                lsemail_gid: $scope.email_gid
            };
            SocketService.getparams(url, params).then(function (resp) {

                $scope.email_from = resp.data.email_from;
                $scope.email_date = resp.data.email_date;
                $scope.email_subject = resp.data.email_subject;
                $scope.email_content = resp.data.email_content;
                $scope.created_date = resp.data.created_date;
                $scope.cc_mail = resp.data.cc;
                $scope.bcc_mail = resp.data.bcc;
                $scope.to_mail = resp.data.email_to;
                $scope.workitemref_no = resp.data.workitemref_no;
                $scope.zone_gid = resp.data.zone_gid;
                $scope.zone_name = resp.data.zone_name;
                $scope.rmemployee_gid = resp.data.rmemployee_gid;
                $scope.rmemployee_name = resp.data.rmemployee_name;
                $scope.rmemployee_mailid = resp.data.rmemployee_mailid;
                $scope.checkeremployee_name = resp.data.checkeremployee_name;
                $scope.attch_list = resp.data.MdlAttachmentList;
                $scope.allottedby_on = resp.data.allottedby_on;
                $scope.aging = resp.data.aging;
                $scope.status = resp.data.status;
                $scope.archivalremarks = resp.data.archivalremarks;
                if ($scope.archivalremarks == '' || $scope.archivalremarks == null) {
                    $scope.archiverem = false;
                }
                else {
                    $scope.archiverem = true;
                }
                $scope.closedremarks = resp.data.closedremarks;
                if ($scope.closedremarks == '' || $scope.closedremarks == null) {
                    $scope.closerem = false;
                }
                else {
                    $scope.closerem = true;
                }
                $scope.updatedby_on = resp.data.updatedby_on;
                $scope.message_id = resp.data.message_id;
                $scope.reference_id = resp.data.reference_id;

                if (resp.data.employee_gid != null) {

                    $scope.assign_to = resp.data.employee_gid;

                }


            });


            var params = {
                email_gid: $scope.email_gid
            };

            var url = "api/IasnTrnWorkItem/ReferenceMail";
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.referenceMail = resp.data.MdlReferenceMail;
                    // $scope.attch_list=resp.data.MdlAttachmentList;  
                }
                else {

                }
            });




            var url = 'api/IasnTrnAuditLog/PostAuditView';

            var params = {
                email_gid: $scope.email_gid
            };
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                }
                else {

                }

            });


        }

        $scope.export = function (path, attchment_name) {


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

        $scope.logdetails = function () {

            if ($scope.IsLogShow == true) {
                $scope.IsLogShow = false;
            }
            else {
                $scope.IsLogShow = true;

                var url = 'api/IasnTrnWorkItem/TransferLog';

                var params = {
                    lsemail_gid: $scope.email_gid
                };

                SocketService.getparams(url, params).then(function (resp) {
                    if (resp.data.status == true) {

                        $scope.transferlog_list = resp.data.MdlTransferLog;
                        if ($scope.transferlog_list == null) {
                            $scope.transfershow = true;
                        }
                        else {
                            $scope.transfershow = false;
                        }
                    }
                    else {

                    }

                });

                var url = 'api/IasnTrnAuditLog/AuditLog';

                var params = {
                    email_gid: $scope.email_gid
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
        $scope.logClose = function () {
            $scope.IsLogShow = false;
        }

        $scope.trustAsHtml = function (string) { return $sce.trustAsHtml(string); };

        $scope.forwardtochange = function (val) {

            var url = "api/IasnTrnWorkItem/EmployeeEmailID";
            var params = {
                employee_gid: val
            }
            SocketService.getparams(url, params).then(function (resp) {

                $scope.tomail_pushback = resp.data.employee_emailid;

            });
        }

        $scope.back = function () {
            if (lstab == 'ConsolidatedReport') {
                $state.go("app.iasnTrnConsolidatedReport");
            }
            else {
                $state.go("app.iasnConsolidatedWorkItem");
            }
        }
    }
})();
