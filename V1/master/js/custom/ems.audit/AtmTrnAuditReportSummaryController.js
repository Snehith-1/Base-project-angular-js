(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AtmTrnAuditReportSummaryController', AtmTrnAuditReportSummaryController);

    AtmTrnAuditReportSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$modal', 'DownloaddocumentService', 'cmnfunctionService'];

    function AtmTrnAuditReportSummaryController($rootScope, $scope, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $modal, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AtmTrnAuditReportSummaryController';

        activate();

        function activate() {

                var url = 'api/AtmRptAuditReports/GetAuditReportSummary';
                //lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.audit_list = resp.data.auditreport_list;
                    //unlockUI();
                    console.log($scope.audit_list);
                });

            }

            $scope.view = function (val1) {
                $location.url('app/AtmRptAuditReportView?hash=' + cmnfunctionService.encryptURL('auditcreation_gid=' + val1 ))
            }

            $scope.exporauditreport = function () {


                var url = 'api/AtmRptAuditReports/GetAuditReportExcelExport';
                SocketService.get(url).then(function (resp) {
                    if (resp.data.status == true) {
                        DownloaddocumentService.Downloaddocument(resp.data.lscloudpath,resp.data.lsname);


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
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        }); activate();
                    }

                });
            }

            $scope.query_report = function (auditcreation_gid) {

                var params = {
                    auditcreation_gid: auditcreation_gid,

                };

                var url = 'api/AtmRptAuditReports/GetAuditSampleQueryReportExcelExport';
                SocketService.getparams(url, params).then(function (resp) {
                    if (resp.data.status == true) {
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
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        activate();
                    }

                });
            }


            $scope.observationreport = function (auditcreation_gid) {

                var params = {
                    auditcreation_gid: auditcreation_gid,

                };

                var url = 'api/AtmRptAuditReports/GetAuditObservationReportExcelExport';
                SocketService.getparams(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                         DownloaddocumentService.Downloaddocument(resp.data.lscloudpath,resp.data.lsname);

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
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        activate();
                    }

                });
            }


            $scope.sample_report = function (auditcreation_gid) {

                var params = {
                    auditcreation_gid: auditcreation_gid,

                };

                var url = 'api/AtmRptAuditReports/GetAuditSampleReportExcelExport';
                SocketService.getparams(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                         DownloaddocumentService.Downloaddocument(resp.data.lscloudpath,resp.data.lsname);

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
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        activate();
                    }

                });
        }

        $scope.sampleobservation_report = function (auditcreation_gid) {

            var params = {
                auditcreation_gid: auditcreation_gid,

            };

            var url = 'api/AtmRptAuditReports/GetAuditObservationSampleReportExcelExport';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
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
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    activate();
                }

            });
        }

        }
    })();