(function () {
    'use strict';

    angular
        .module('angle')
        .controller('osdRptAllTicketsController', osdRptAllTicketsController);

    osdRptAllTicketsController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$filter', '$route', 'ngTableParams', 'DownloaddocumentService','cmnfunctionService'];

    function osdRptAllTicketsController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $filter, $route, ngTableParams, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'osdRptAllTicketsController';

        activate();

        function activate() {
            $scope.limit = 6000;
            $scope.totalDisplayed = 100;

            vm.open = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.opened = true;
            };
            vm.close = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.closed = true;
            };
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];

            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };

                var url = 'api/OsdMstDepartmentManagement/GetActivatedept';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.deptmasterlist = resp.data.deptlist;

                    unlockUI();
                    if ($scope.deptmasterlist.length == 1) {
                        $scope.single = true
                        $scope.lbldepartmentname = resp.data.department_name;
                        $scope.lbldepartmentgid = resp.data.department_gid

                        var params = {
                            department_gid: resp.data.department_gid
                        }
                        var url = 'api/OsdMstActivity/GetDeptTeam';
                        SocketService.getparams(url, params).then(function (resp) {
                            $scope.supportdtl = resp.data.supportdtl;
                        });
                        var url = 'api/OsdTrnServiceRequest/GetDeptActivity';
                        SocketService.getparams(url, params).then(function (resp) {
                            $scope.activity_list = resp.data.activitydtl;
                        });
                        var url = 'api/OsdMstSupportTeam/GetAllTeamMember';
                        SocketService.get(url).then(function (resp) {
                            $scope.employee_list = resp.data.teammembers;
                        });
                    }
                    else {
                        $scope.supportteam_gid = "";
                        $scope.activitymaster_gid = "";
                        $scope.assignedmember_gid = "";
                        $scope.multiple = true
                        $scope.single = false
                    }
                });


            
           
        
            //var url = 'api/OsdMstSupportTeam/GetSupportTeamSummary';
            //SocketService.get(url).then(function (resp) {
            //    $scope.supportdtl = resp.data.supportdtl;
            //});

            //var url = 'api/employee/Employee';
            //SocketService.get(url).then(function (resp) {
            //    $scope.employee_list = resp.data.employee_list;
            //});

            //var url = 'api/OsdMstSupportTeam/GetAllTeamMember';
            //SocketService.get(url).then(function (resp) {
            //    $scope.employee_list = resp.data.teammembers;
            //});

           
            var url = 'api/OsdRptAllTickets/GetAllTicketsSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.servicerequestsummary = resp.data.servicerequestdtl;
                unlockUI();
                // new code start   
                if ($scope.servicerequestsummary == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.servicerequestsummary.length;
                    if ($scope.servicerequestsummary.length < 100) {
                        $scope.totalDisplayed = $scope.servicerequestsummary.length;
                    }
                }
            });
        }

        $scope.onselectdept = function (department_gid) {
            lockUI();
            var params = {
                department_gid: department_gid.department_gid
            }
            var url = 'api/OsdMstActivity/GetDeptTeam';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.supportdtl = resp.data.supportdtl;
            });
            var url = 'api/OsdTrnServiceRequest/GetDeptActivity';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.activity_list = resp.data.activitydtl;
            });
            var url = 'api/OsdMstSupportTeam/GetAllTeamMember';
            SocketService.get(url).then(function (resp) {
                $scope.employee_list = resp.data.teammembers;
            });
            unlockUI();
        }

        $scope.viewNew = function (val, val2, val3, val4) {
          
            $location.url('app/osdRptAllTicketsView?hash=' + cmnfunctionService.encryptURL('servicerequest_gid=' + val + '&bankalert_flag=' + val2 + '&bankalert2allocated_gid=' + val3 + '&customer_gid=' + val4 ));
        }
        var limitStep = 6000;
        $scope.incrementLimit = function () {
            $scope.limit += limitStep;
        };
        $scope.decrementLimit = function () {
            $scope.limit -= limitStep;
        };

        $scope.loadMore = function (pagecount) {
            if (pagecount == undefined) {
                Notify.alert("Enter the Total Summary Count", "warning");
                return;
            }
            lockUI();

            var Number = parseInt(pagecount);
            // new code start
            if ($scope.servicerequestsummary != null) {

                if (pagecount < $scope.servicerequestsummary.length) {
                    $scope.totalDisplayed += Number;
                    if ($scope.servicerequestsummary.length < $scope.totalDisplayed) {
                        $scope.totalDisplayed = $scope.servicerequestsummary.length;
                        Notify.alert(" Total Summary " + $scope.servicerequestsummary.length + " Records Only", "warning");
                    }
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(" Total Summary " + $scope.servicerequestsummary.length + " Records Only", "warning");
                    return;
                }
            }
            unlockUI();
        };

        $scope.all = function () {
            $scope.supportteam_gid = "";
            $scope.activitymaster_gid = "";
            $scope.request_title = "";
            $scope.raisedmember_gid = "";
            $scope.assignedmember_gid = "";
            $scope.ref_no = "";
            $scope.raised_date = "";
            $scope.ticket_status = "";
            $scope.cbodepartment = "";
            $scope.ticket_source = "";
            var url = 'api/OsdRptAllTickets/GetAllTicketsSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.servicerequestsummary = resp.data.servicerequestdtl;
                unlockUI();
            });
        }
        

        $scope.search = function () {
            //var raised_date = $filter('date')($scope.raised_date, 'yyyy-MM-dd');
            //var raised_date = new Date();

            //raised_date.setDate($scope.raised_date.getDate() + 1);

            if ($scope.raised_date == undefined || $scope.raised_date == "") {
                var raised_date = 'null';
            }
            else {
                var raised_date1 = $scope.raised_date;

                var raised_date = new Date(raised_date1.getTime() - (raised_date1.getTimezoneOffset() * 60000))
                                    .toISOString()
                                    .split("T")[0];
            }
            if ($scope.lbldepartmentgid == "" || $scope.lbldepartmentgid== undefined ) {
                var departmentgid = $scope.cbodepartment.department_gid
            }
            else {
                var departmentgid =$scope.lbldepartmentgid
            }
            var params = {
                activitymaster_gid: $scope.activitymaster_gid,
                supportteam_gid: $scope.supportteam_gid,
                assignedmember_gid: $scope.assignedmember_gid,
                raisedmember_gid: $scope.raisedmember_gid,
                ticket_status: $scope.ticket_status,
                request_title: $scope.request_title,            
                request_refno: $scope.ref_no,
                raised_date: raised_date,
                department_gid: departmentgid,
                source: $scope.ticket_source 
            }

            var url = 'api/OsdRptAllTickets/PostAllTicketsSummarySearch';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                $scope.servicerequestsummary = resp.data.servicerequestdtl;
                
            });
        }

      
        $scope.viewPDF = function (val) {
            lockUI();
            var params = {
                servicerequest_gid: val
            }

            var url = 'api/OsdTrnTicketManagement/txtfile';
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    activate();
                    unlockUI();
                }
                else {
                    //$modalInstance.close('closed');
                    alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    activate();
                    unlockUI();
                }
            });
        }

        $scope.export = function (val1, val2) {
            lockUI();
            if (($scope.single == false) &&  ($scope.cbodepartment == "" || $scope.cbodepartment == undefined)) {
                Notify.alert("Kindly Select Business Unit Name", "warning");
                unlockUI();
            }
            else {
                if ($scope.raised_date == undefined || $scope.raised_date == "") {
                    var raised_date = 'null';
                }
                else {
                    var raised_date1 = $scope.raised_date;

                    var raised_date = new Date(raised_date1.getTime() - (raised_date1.getTimezoneOffset() * 60000))
                                        .toISOString()
                                        .split("T")[0];
                }
                if ($scope.lbldepartmentgid == "" || $scope.lbldepartmentgid == undefined) {
                    var departmentgid = $scope.cbodepartment.department_gid
                }
                else {
                    var departmentgid = $scope.lbldepartmentgid
                }
                var params = {
                    activitymaster_gid: $scope.activitymaster_gid,
                    supportteam_gid: $scope.supportteam_gid,
                    assignedmember_gid: $scope.assignedmember_gid,
                    ticket_status: $scope.ticket_status,
                    raised_date: raised_date,
                    department_gid: departmentgid,
                    source: $scope.ticket_source 
                }
                var url = 'api/OsdRptAllTickets/TicketExport';
                SocketService.post(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        unlockUI();
                        DownloaddocumentService.Downloaddocument(resp.data.lscloudpath, resp.data.lsname);
                        // var phyPath = resp.data.lspath;
                        // var relPath = phyPath.split("EMS");
                        // var relpath1 = relPath[1].replace("\\", "/");
                        // var hosts = window.location.host;
                        // var prefix = location.protocol + "//";
                        // var str = prefix.concat(hosts, relpath1);
                        // var link = document.createElement("a");
                        // var name = resp.data.lsname.split('.');
                        // link.download = name[0];
                        // var uri = str;
                        // link.href = uri;
                        // link.click();
                        //DownloaddocumentService.Downloaddocument(val1, val2);
                    }
                    else {
                        unlockUI();
                        Notify.alert('Error Occurred While Export !', 'success')
                        activate();

                    }

                });
            }
        }
    }
})();