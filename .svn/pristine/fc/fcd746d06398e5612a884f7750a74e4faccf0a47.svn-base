(function () {
    'use strict';

    angular
        .module('angle')
        .controller('osdTrnTicketManagement', osdTrnTicketManagement);

    osdTrnTicketManagement.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams','cmnfunctionService'];

    function osdTrnTicketManagement($rootScope, $scope, $modal, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'osdTrnTicketManagement';
        var servicerequest_gid;
        activate();

        function activate() {
            lockUI();

            var url = 'api/OsdTrnTicketManagement/GetCountSummary';
            SocketService.get(url).then(function (resp) {
                $scope.count_newticket = resp.data.alloted_count;
                $scope.count_transferticket = resp.data.workinprogress_count;
                $scope.count_completedticket = resp.data.completed_count;
                $scope.count_closedticket = resp.data.closed_count;
                $scope.forward_count = resp.data.forward_count;
                unlockUI();
            });

            lockUI();

            var url = 'api/OsdTrnBankAlert/GetBankalertNotification';
            SocketService.get(url).then(function (resp) {
                if (resp.data.allocated_new == "true" || resp.data.notallocated_new == "true") {
                    if (resp.data.privilege == "true") {
                        $scope.new = true;
                        $scope.old = false;
                    }
                    else {
                        $scope.new = false;
                        $scope.old = false;
                    }

                }
                else {
                    if (resp.data.privilege == "true") {
                        $scope.new = false;
                        $scope.old = true;
                    }
                    else {
                        $scope.new = false;
                        $scope.old = false;
                    }
                }
                unlockUI();
            });

            $scope.tab = {};
            var url = window.location.href;
            var relPath = url.split("lstab=");
            var relpath1 = relPath[1];
            if (relpath1 != undefined) {
                if (relpath1 == "New") {
                    $scope.tabnew = true;
                }
                else if (relpath1 == "Workin-Progress") {
                    $scope.tabworkinprogressticket = true;
                }
                else if (relpath1 == "Forward-Ticket") {
                    $scope.tabforwardticket = true;
                }
                else if (relpath1 == "Completed") {
                    $scope.tabcompleted = true;
                }
                else if (relpath1 == "Closed") {
                    $scope.tabclosed = true;
                }
            }
            else {
                if ($scope.tab.activeTabId == undefined) {
                    $scope.tabnew = true;
                }
                else if ($scope.tab.activeTabId == 'New') {
                    $scope.tabnew = true;
                }
                else if ($scope.tab.activeTabId == 'Workin-Progress') {
                    $scope.tabworkinprogressticket = true;
                }
                else if ($scope.tab.activeTabId == 'Forward-Ticket') {
                    $scope.tabforwardticket = true;
                }
                else if ($scope.tab.activeTabId == 'Completed') {
                    $scope.tabcompleted = true;
                }
                else if ($scope.tab.activeTabId == 'Closed') {
                    $scope.tabclosed = true;
                }
            }

        
            // Allotted

                $scope.totalDisplayednew = 100;
                var url = 'api/OsdTrnTicketManagement/GetServiceRequestSummary';
                SocketService.get(url).then(function (resp) {
                    $scope.servicerequestdtl = resp.data.servicerequestdtl;
                    if ($scope.servicerequestdtl == null) {
                        $scope.totalDisplayednew = 0;
                        $scope.totalnew = 0;
                    }
                    else {
                        $scope.totalDisplayednew = $scope.servicerequestdtl.length;
                        if ($scope.servicerequestdtl.length < 100) {
                            $scope.totalnew = $scope.servicerequestdtl.length;
                        }
                    }
                    unlockUI();
                });

            // Work-in-progress

                lockUI();
                $scope.totalDisplayedworkinprogress = 100;
                var url = 'api/OsdTrnTicketManagement/GetMyWorkInProgressSummary';
                SocketService.get(url).then(function (resp) {
                    $scope.workinprogressdtl = resp.data.workinprogressdtl;
                    if ($scope.workinprogressdtl == null) {
                        $scope.totalworkinprogress = 0;
                        $scope.totalDisplayedworkinprogress = 0;
                    }
                    else {
                        $scope.totalworkinprogress = $scope.workinprogressdtl.length;
                        if ($scope.workinprogressdtl.length < 100) {
                            $scope.totalDisplayedworkinprogress = $scope.workinprogressdtl.length;
                        }
                    }
                    unlockUI();
                });

            // Forward 

                lockUI();
                $scope.totalDisplayedforwardacivity = 100;
                var url = "api/OsdTrnTicketManagement/GetAllForwardSummary";
                SocketService.get(url).then(function (resp) {
                    $scope.forwardactivitysummary = resp.data.forwarddtl;
                    if ($scope.forwardactivitysummary == null) {
                        $scope.totalforwardactivity = 0;
                        $scope.totalDisplayedforwardacivity = 0;
                    }
                    else {
                        $scope.totalforwardactivity = $scope.forwardactivitysummary.length;
                        if ($scope.forwardactivitysummary.length < 100) {
                            $scope.totalDisplayedforwardacivity = $scope.forwardactivitysummary.length;
                        }
                    }
                    unlockUI();
                });

            // Completed
                lockUI();
                $scope.totalDisplayedcompleted = 100;
                var url = 'api/OsdTrnTicketManagement/GetMyCompletedSummary';
                SocketService.get(url).then(function (resp) {
                    $scope.completeddtl = resp.data.completeddtl;
                    if ($scope.completeddtl == null) {
                        $scope.totalcompleted = 0;
                        $scope.totalDisplayedcompleted = 0;
                    }
                    else {
                        $scope.totalcompleted = $scope.completeddtl.length;
                        if ($scope.completeddtl.length < 100) {
                            $scope.totalDisplayedcompleted = $scope.completeddtl.length;
                        }
                    }
                    unlockUI();
                });

            // Closed

                lockUI();
                $scope.totalDisplayedclosed = 100;
                var url = 'api/OsdTrnTicketManagement/GetMyClosedSummary';
                SocketService.get(url).then(function (resp) {
                    $scope.closeddtl = resp.data.closeddtl;
                    if ($scope.closeddtl == null) {
                        $scope.totalclosed = 0;
                        $scope.totalDisplayedclosed = 0;
                    }
                    else {
                        $scope.totalclosed = $scope.closeddtl.length;
                        if ($scope.closeddtl.length < 100) {
                            $scope.totalDisplayedclosed = $scope.closeddtl.length;
                        }
                    }
                    unlockUI();
                });
            }

        $scope.viewNew = function (val, val2, val3, val4) {
            $scope.servicerequest_gid = val;
            $scope.bankalert_flag = val2;
            $scope.bankalert2allocated_gid = val3;
            $scope.customer_gid = val4;

            var servicerequest_gid = val;
            var bankalert_flag = val2;
            var bankalert2allocated_gid = val3;
            var customer_gid = val4;

            $scope.servicerequest_gid = localStorage.setItem('servicerequest_gid', val);
            $scope.bankalert_flag = localStorage.setItem('bankalert_flag', val2);
            $scope.bankalert2allocated_gid = localStorage.setItem('bankalert2allocated_gid', val3);
            $scope.customer_gid = localStorage.setItem('customer_gid', val4);

            $location.url('app/osdTrnActivityManagement360?hash=' + cmnfunctionService.encryptURL('lstab=New'));
            localStorage.setItem('servicerequest_gid', servicerequest_gid)
        }
        $scope.viewWorking = function (val, val2, val3, val4) {
            $scope.servicerequest_gid = val;
            $scope.bankalert_flag = val2;
            $scope.bankalert2allocated_gid = val3;
            $scope.customer_gid = val4;

            var servicerequest_gid = val;
            var bankalert_flag = val2;
            var bankalert2allocated_gid = val3;
            var customer_gid = val4;

            $scope.servicerequest_gid = localStorage.setItem('servicerequest_gid', val);
            $scope.bankalert_flag = localStorage.setItem('bankalert_flag', val2);
            $scope.bankalert2allocated_gid = localStorage.setItem('bankalert2allocated_gid', val3);
            $scope.customer_gid = localStorage.setItem('customer_gid', val4);

            $location.url('app/osdTrnActivityManagement360?hash=' + cmnfunctionService.encryptURL('lstab=Workin-Progress'));
            localStorage.setItem('servicerequest_gid', servicerequest_gid)
        }
        $scope.viewforward = function (val, val2, val3, val4) {

            $scope.servicerequest_gid = val;
            $scope.bankalert_flag = val2;
            $scope.bankalert2allocated_gid = val3;
            $scope.customer_gid = val4;

            var servicerequest_gid = val;
            var bankalert_flag = val2;
            var bankalert2allocated_gid = val3;
            var customer_gid = val4;

            $scope.servicerequest_gid = localStorage.setItem('servicerequest_gid', val);
            $scope.bankalert_flag = localStorage.setItem('bankalert_flag', val2);
            $scope.bankalert2allocated_gid = localStorage.setItem('bankalert2allocated_gid', val3);
            $scope.customer_gid = localStorage.setItem('customer_gid', val4);

            $location.url('app/osdTrnActivityManagement360?hash=' + cmnfunctionService.encryptURL('lstab=Forward-Ticket'));
            localStorage.setItem('servicerequest_gid', servicerequest_gid)
        }
        $scope.viewCompleted = function (val, val2, val3, val4) {
            $scope.servicerequest_gid = val;
            $scope.bankalert_flag = val2;
            $scope.bankalert2allocated_gid = val3;
            $scope.customer_gid = val4;

            var servicerequest_gid = val;
            var bankalert_flag = val2;
            var bankalert2allocated_gid = val3;
            var customer_gid = val4;

            $scope.servicerequest_gid = localStorage.setItem('servicerequest_gid', val);
            $scope.bankalert_flag = localStorage.setItem('bankalert_flag', val2);
            $scope.bankalert2allocated_gid = localStorage.setItem('bankalert2allocated_gid', val3);
            $scope.customer_gid = localStorage.setItem('customer_gid', val4);
            $location.url('app/osdTrnActivityManagement360?hash=' + cmnfunctionService.encryptURL('lstab=Completed'));
            localStorage.setItem('servicerequest_gid', servicerequest_gid)
        }
        $scope.viewClosed = function (val, val2, val3, val4) {
            $scope.servicerequest_gid = val;
            $scope.bankalert_flag = val2;
            $scope.bankalert2allocated_gid = val3;
            $scope.customer_gid = val4;

            var servicerequest_gid = val;
            var bankalert_flag = val2;
            var bankalert2allocated_gid = val3;
            var customer_gid = val4;

            $scope.servicerequest_gid = localStorage.setItem('servicerequest_gid', val);
            $scope.bankalert_flag = localStorage.setItem('bankalert_flag', val2);
            $scope.bankalert2allocated_gid = localStorage.setItem('bankalert2allocated_gid', val3);
            $scope.customer_gid = localStorage.setItem('customer_gid', val4);
            $location.url('app/osdTrnActivityManagement360?hash=' + cmnfunctionService.encryptURL('lstab=Closed'));
            localStorage.setItem('servicerequest_gid', servicerequest_gid)
        }
       
        $scope.loadMorenew = function (pagecountnew) {
            if (pagecountnew == undefined) {
                Notify.alert("Enter the Total Summary Count", "warning");
                return;
            }
            lockUI();
            var Number = parseInt(pagecountnew);
            if ($scope.servicerequestdtl != null) {

                if (pagecountnew < $scope.servicerequestdtl.length) {
                    $scope.totalDisplayednew += Number;
                    if ($scope.servicerequestdtl.length < $scope.totalDisplayednew) {
                        $scope.totalDisplayednew = $scope.servicerequestdtl.length;
                        Notify.alert(" Total Summary " + $scope.servicerequestdtl.length + " Records Only", "warning");
                    }
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(" Total Summary " + $scope.servicerequestdtl.length + " Records Only", "warning");
                    return;
                }
            }
            unlockUI();
        };

        // Work in Progress

        $scope.workinprogress_ticket = function () {
            lockUI();
            $scope.totalDisplayedworkinprogress = 100;
            var url = 'api/OsdTrnTicketManagement/GetMyWorkInProgressSummary';
            SocketService.get(url).then(function (resp) {
                $scope.workinprogressdtl = resp.data.workinprogressdtl;
                if ($scope.workinprogressdtl == null) {
                    $scope.totalworkinprogress = 0;
                    $scope.totalDisplayedworkinprogress = 0;
                }
                else {
                    $scope.totalworkinprogress = $scope.workinprogressdtl.length;
                    if ($scope.workinprogressdtl.length < 100) {
                        $scope.totalDisplayedworkinprogress = $scope.workinprogressdtl.length;
                    }
                }
                unlockUI();
            });
        }
        $scope.loadmoreworkinprogress = function (pagecountworkinprogress) {
            if (pagecountworkinprogress == undefined) {
                Notify.alert("Enter the Total Summary Count", "warning");
                return;
            }
            lockUI();
            var Number = parseInt(pagecountworkinprogress);
            if ($scope.workinprogressdtl != null) {

                if (pagecountworkinprogress < $scope.workinprogressdtl.length) {
                    $scope.totalDisplayedworkinprogress += Number;
                    if ($scope.workinprogressdtl.length < $scope.totalDisplayedworkinprogress) {
                        $scope.totalDisplayedworkinprogress = $scope.workinprogressdtl.length;
                        Notify.alert(" Total Summary " + $scope.workinprogressdtl.length + " Records Only", "warning");
                    }
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(" Total Summary " + $scope.workinprogressdtl.length + " Records Only", "warning");
                    return;
                }
            }
            unlockUI();
        };

        // Transfer Activity

        $scope.transfer_ticket = function () {
            lockUI();
            $scope.totalDisplayedtransferticket = 100;
            var url = 'api/OsdTrnTicketManagement/GetMyWorkInProgressSummary';
            SocketService.get(url).then(function (resp) {
                $scope.transferticket_list = resp.data.workinprogressdtl;
                if ($scope.transferticket_list == null) {
                    $scope.totaltransferticket = 0;
                    $scope.totalDisplayedtransferticket = 0;
                }
                else {
                    $scope.totaltransferticket = $scope.transferticket_list.length;
                    if ($scope.transferticket_list.length < 100) {
                        $scope.totalDisplayedtransferticket = $scope.transferticket_list.length;
                    }
                }
                unlockUI();
            });
        }
        $scope.loadMoretransferticket = function (pagecounttransferticket) {
            if (pagecounttransferticket == undefined) {
                Notify.alert("Enter the Total Summary Count", "warning");
                return;
            }
            lockUI();
            var Number = parseInt(pagecounttransferticket);
            if ($scope.transferticket_list != null) {

                if (pagecounttransferticket < $scope.transferticket_list.length) {
                    $scope.totalDisplayedtransferticket += Number;
                    if ($scope.transferticket_list.length < $scope.totalDisplayedtransferticket) {
                        $scope.totalDisplayedtransferticket = $scope.transferticket_list.length;
                        Notify.alert(" Total Summary " + $scope.transferticket_list.length + " Records Only", "warning");
                    }
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(" Total Summary " + $scope.transferticket_list.length + " Records Only", "warning");
                    return;
                }
            }
            unlockUI();
        };

        // Forward Tickets
        $scope.forward_ticket = function () {
            lockUI();
            $scope.totalDisplayedforwardacivity = 100;
            var url = "api/OsdTrnTicketManagement/GetAllForwardSummary";
            SocketService.get(url).then(function (resp) {
                $scope.forwardactivitysummary = resp.data.forwarddtl;
                if ($scope.forwardactivitysummary == null) {
                    $scope.totalforwardactivity = 0;
                    $scope.totalDisplayedforwardacivity = 0;
                }
                else {
                    $scope.totalforwardactivity = $scope.forwardactivitysummary.length;
                    if ($scope.forwardactivitysummary.length < 100) {
                        $scope.totalDisplayedforwardacivity = $scope.forwardactivitysummary.length;
                    }
                }
                unlockUI();
            });
        }
        $scope.loadMoreforwardactivity = function (pagecountforward) {
            if (pagecountforward == undefined) {
                Notify.alert("Enter the Total Summary Count", "warning");
                return;
            }
            lockUI();
            var Number = parseInt(pagecountforward);
            if ($scope.forwardactivitysummary != null) {

                if (pagecountforward < $scope.forwardactivitysummary.length) {
                    $scope.totalDisplayedforwardacivity += Number;
                    if ($scope.forwardactivitysummary.length < $scope.totalDisplayedforwardacivity) {
                        $scope.totalDisplayedforwardacivity = $scope.forwardactivitysummary.length;
                        Notify.alert(" Total Summary " + $scope.forwardactivitysummary.length + " Records Only", "warning");
                    }
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(" Total Summary " + $scope.forwardactivitysummary.length + " Records Only", "warning");
                    return;
                }
            }
            unlockUI();
        };

        // Completed
        $scope.completed_ticket = function () {
            lockUI();
            $scope.totalDisplayedcompleted = 100;
            var url = 'api/OsdTrnTicketManagement/GetMyCompletedSummary';
            SocketService.get(url).then(function (resp) {
                $scope.completeddtl = resp.data.completeddtl;
                if ($scope.completeddtl == null) {
                    $scope.totalcompleted = 0;
                    $scope.totalDisplayedcompleted = 0;
                }
                else {
                    $scope.totalcompleted = $scope.completeddtl.length;
                    if ($scope.completeddtl.length < 100) {
                        $scope.totalDisplayedcompleted = $scope.completeddtl.length;
                    }
                }
                unlockUI();
            });
        }
        $scope.loadMorecompleted = function (pagecountcompleted) {
            if (pagecountcompleted == undefined) {
                Notify.alert("Enter the Total Summary Count", "warning");
                return;
            }
            lockUI();
            var Number = parseInt(pagecountcompleted);
            if ($scope.completeddtl != null) {

                if (pagecountcompleted < $scope.completeddtl.length) {
                    $scope.totalDisplayedcompleted += Number;
                    if ($scope.completeddtl.length < $scope.totalDisplayedcompleted) {
                        $scope.totalDisplayedcompleted = $scope.completeddtl.length;
                        Notify.alert(" Total Summary " + $scope.completeddtl.length + " Records Only", "warning");
                    }
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(" Total Summary " + $scope.completeddtl.length + " Records Only", "warning");
                    return;
                }
            }
            unlockUI();
        };

        $scope.closed_ticket = function (servicerequest_gid) {
            lockUI();
            var url = 'api/OsdTrnTicketManagement/GetMyClosedSummary';
            SocketService.get(url).then(function (resp) {
                $scope.closeddtl = resp.data.closeddtl;
                unlockUI();
            });
        }

        // Closed Activity
        $scope.closed_ticket = function () {
            lockUI();
            $scope.totalDisplayedclosed = 100;
            var url = 'api/OsdTrnTicketManagement/GetMyClosedSummary';
            SocketService.get(url).then(function (resp) {
                $scope.closeddtl = resp.data.closeddtl;
                if ($scope.closeddtl == null) {
                    $scope.totalclosed = 0;
                    $scope.totalDisplayedclosed = 0;
                }
                else {
                    $scope.totalclosed = $scope.closeddtl.length;
                    if ($scope.closeddtl.length < 100) {
                        $scope.totalDisplayedclosed = $scope.closeddtl.length;
                    }
                }
                unlockUI();
            });
        }
        $scope.loadMoreclosed = function (pagecountclosed) {
            if (pagecountclosed == undefined) {
                Notify.alert("Enter the Total Summary Count", "warning");
                return;
            }
            lockUI();
            var Number = parseInt(pagecountclosed);
            if ($scope.closeddtl != null) {

                if (pagecountclosed < $scope.closeddtl.length) {
                    $scope.totalDisplayedclosed += Number;
                    if ($scope.closeddtl.length < $scope.totalDisplayedclosed) {
                        $scope.totalDisplayedclosed = $scope.closeddtl.length;
                        Notify.alert(" Total Summary " + $scope.closeddtl.length + " Records Only", "warning");
                    }
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(" Total Summary " + $scope.closeddtl.length + " Records Only", "warning");
                    return;
                }
            }
            unlockUI();
        };

        $scope.TransferAllocation = function (servicerequest_gid, assigned_team, assigned_to, assigned_membergid, assigned_supportteamgid) {
            localStorage.setItem('servicerequest_gid', servicerequest_gid)
            var modalInstance = $modal.open({
                templateUrl: '/transferallocationmodal.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var url = 'api/OsdMstActivity/GetTeamSummary';
                SocketService.get(url).then(function (resp) {
                    $scope.supportdtllist = resp.data.supportdtl;
                });

                $scope.onselectedchangeteam = function (team_name) {
                    $scope.supportteam_gid = localStorage.setItem('onchangesupportteam_gid', team_name);
                    var params = {
                        supportteam_gid: $scope.cbosuppport_team.supportteam_gid,
                        servicerequest_gid: localStorage.getItem('servicerequest_gid'),
                    }
                    var url = 'api/OsdMstSupportTeam/PostTeamMemberExceptAssigned';
                    SocketService.post(url, params).then(function (resp) {
                        $scope.teammembers_list = resp.data.teammembers;
                    });
                }
               
                var params = {
                    servicerequest_gid: localStorage.getItem('servicerequest_gid')
                }

                var url = "api/OsdTrnTicketManagement/GetTransferMemberlist"

                SocketService.getparams(url, params).then(function (resp) {
                    $scope.transferlistdtllist = resp.data.transferlistdtl;
                    var transfer_flag = resp.data.transferlistdtl[0].transfer_flag;
                    if (transfer_flag == 'Y') {
                        $scope.transferlist = true;
                    }
                    else {
                        $scope.transferlist = false;
                    }
                    unlockUI();
                });

                // TransferAllocation Submit Event
                $scope.teamSubmit = function () {
                    lockUI();
                    var params = {
                        servicerequest_gid: servicerequest_gid,
                        assigned_supportteam: assigned_team,
                        assigned_member: assigned_to,
                        assigned_membergid: assigned_membergid,
                        assigned_supportteamgid: assigned_supportteamgid,
                        transferteam_gid: $scope.cbosuppport_team.supportteam_gid,
                        transferteam_name: $scope.cbosuppport_team.team_name,
                        transferemployee_gid: $scope.cboemployee_name.employee_gid,
                        transferemployee_name: $scope.cboemployee_name.employee_name
                    }
                    var url = "api/OsdTrnTicketManagement/PostTransferAllocation";
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                            unlockUI();
                        }
                        else {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                            unlockUI();
                        }
                    });
                }
               
                $scope.ok = function () {
                    modalInstance.close('closed');
                };

            }
        }

        //$scope.Zip = function (servicerequest_gid) {

        //    var param = {
        //        documentIdentifier: 1,
        //        documentPaths: "F:\Web\EMS\erp_documents\SAMUNNATI\ECMS-src",
        //        zipDestinationPath: "F:\Web\EMS\erp_documents\SAMUNNATI\ECMS-test"

        //    };

        //    var url = 'api/deferral/ZipDocument';
        //    SocketService.post(url, param).then(function (resp) {
        //        var phyPath = "F:/Web/EMS/erp_documents/SAMUNNATI/ECMS-test1.zip";
        //        var relPath = phyPath.split("EMS");
        //        var relpath1 = relPath[1].replace("\\", "/");
        //        var hosts = window.location.host;
        //        var prefix = location.protocol + "//";
        //        var str = prefix.concat(hosts, relpath1);
        //        var link = document.createElement("a");
        //        var name = "name";
        //        link.download = name[0];
        //        var uri = str;
        //        link.href = uri;
        //        link.click();

        //    });
        //}

        $scope.TransferAllocationwithRemarks = function (servicerequest_gid, assigned_team, assigned_to, assigned_membergid, assigned_supportteamgid) {
            localStorage.setItem('servicerequest_gid', servicerequest_gid)
            var modalInstance = $modal.open({
                templateUrl: '/transferallocationwithremarksmodal.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var url = 'api/OsdMstActivity/GetTeamSummary';
                SocketService.get(url).then(function (resp) {
                    $scope.supportdtllist = resp.data.supportdtl;
                });

                $scope.onselectedchangeteam = function (team_name) {
                    $scope.supportteam_gid = localStorage.setItem('onchangesupportteam_gid', team_name);
                    var params = {
                        supportteam_gid: $scope.cbosuppport_team.supportteam_gid,
                        servicerequest_gid: localStorage.getItem('servicerequest_gid'),
                    }
                    var url = 'api/OsdMstSupportTeam/PostTeamMemberExceptAssigned';
                    SocketService.post(url, params).then(function (resp) {
                        $scope.teammembers_list = resp.data.teammembers;
                    });
                }

                var params = {
                    servicerequest_gid: localStorage.getItem('servicerequest_gid'),                   
            }
                var url = "api/OsdTrnTicketManagement/GetTransferMemberlist"

                SocketService.getparams(url, params).then(function (resp) {
                    $scope.transferlistdtllist = resp.data.transferlistdtl;
                    var transfer_flag = resp.data.transferlistdtl[0].transfer_flag;
                    if (transfer_flag == 'Y') {
                        $scope.transferlist = true;
                    }
                    else { 
                        $scope.transferlist = false;
                    }
                    unlockUI();
                });
                // TransferAllocationremarks Submit Event
                $scope.teamSubmitremarks = function () {
                    lockUI();
                    var params = {
                        servicerequest_gid: servicerequest_gid,
                        assigned_supportteam: assigned_team,
                        assigned_member: assigned_to,
                        assigned_membergid: assigned_membergid,
                        assigned_supportteamgid: assigned_supportteamgid,
                        transferteam_gid: $scope.cbosuppport_team.supportteam_gid,
                        transferteam_name: $scope.cbosuppport_team.team_name,
                        transferemployee_gid: $scope.cboemployee_name.employee_gid,
                        transferemployee_name: $scope.cboemployee_name.employee_name,
                        remarks: $scope.team_description
                    }
                    var url = "api/OsdTrnTicketManagement/PostTransferAllocation";
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                            $scope.current = $state.current.name;
                            ScopeValueService.store("dataldCtrl", $scope);
                            $state.go('app.pageredirect');
                            unlockUI();
                        }
                        else {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                            unlockUI();
                        }
                    });
                }
                $scope.ok = function () {
                    modalInstance.close('closed');
                };

            }
        }
    }
})();