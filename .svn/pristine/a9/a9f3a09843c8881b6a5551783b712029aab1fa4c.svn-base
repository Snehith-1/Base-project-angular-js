(function () {
    'use strict';

    angular
        .module('angle')
        .controller('osdTrnMyTicketcontroller', osdTrnMyTicketcontroller);

    osdTrnMyTicketcontroller.$inject = ['$rootScope', '$scope', '$sce', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', '$cookies', '$filter', 'ngTableParams', '$timeout','cmnfunctionService'];

    function osdTrnMyTicketcontroller($rootScope, $scope, $sce, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, $cookies, $filter, ngTableParams, $timeout,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;

        vm.title = 'osdTrnMyTicketcontroller';
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        var lstab = searchObject.lstab;
        activate();

        function activate() {
            lockUI();

    // Get My Activity Count Code 
            var url = "api/OsdTrnMyTicket/GetActivityCount";
            SocketService.get(url).then(function (resp) {
                $scope.alloted_count = resp.data.alloted_count;
                $scope.workinprogress_count = resp.data.workinprogress_count;
                $scope.completed_count = resp.data.completed_count;
                $scope.closed_count = resp.data.closed_count;
                $scope.transfer_count = resp.data.transfer_count;
                $scope.forward_count = resp.data.forward_count;
                unlockUI();
            });


            $scope.tab = {};
            var url = window.location.href;
            var relPath = url.split("lstab=");
            var relpath1 = relPath[1];
            var i;
            if (lstab != undefined) {
                if (lstab == "Alloted") {
                    $scope.taballoted = true;
                }
                else if (lstab == "Work-InProgress") {
                    $scope.tabworkinprogress = true;
                }
                else if (lstab == "Forward") {
                    $scope.tabforward = true;
                }
                else if (lstab == "Tagged-Ticket") {
                    $scope.tabtaggedticket = true;
                }
                else if (lstab == "Transfer-Ticket") {
                    $scope.tabtransferticket = true;
                }
                else if (lstab == "Completed") {
                    $scope.tabcompleted = true;
                }
                else if (lstab == "Closed") {
                    $scope.tabclosed = true;
                }
            }
            else {
                if ($scope.tab.activeTabId == undefined) {
                    $scope.taballoted = true;
                }
                else if ($scope.tab.activeTabId == 'Alloted') {
                    $scope.taballoted = true;
                }
                else if ($scope.tab.activeTabId == 'Work-InProgress') {
                    $scope.tabworkinprogress = true;
                }
                else if ($scope.tab.activeTabId == 'Forward') {
                    $scope.tabforward = true;
                }
                else if ($scope.tab.activeTabId == 'Tagged-Ticket') {
                    $scope.tabtaggedticket = true;
                }
                else if ($scope.tab.activeTabId == 'Transfer-Ticket') {
                    $scope.tabtransferticket = true;
                }
                else if ($scope.tab.activeTabId == 'Completed') {
                    $scope.tabcompleted = true;
                }
                else if ($scope.tab.activeTabId == 'Closed') {
                    $scope.tabclosed = true;
                }
            }



            //  Alloted Activity
            $scope.totalDisplayednew = 100;
            var url = "api/OsdTrnMyTicket/GetAllottedSummary";
            
            SocketService.get(url).then(function (resp) {
                $scope.allotedactivity_list = resp.data.allotteddtl;
                if ($scope.allotedactivity_list == null) {
                    $scope.totalnew = 0;
                    $scope.totalDisplayednew = 0;
                }
                else {
                    $scope.totalnew = $scope.allotedactivity_list.length;
                    if ($scope.allotedactivity_list.length < 100) {
                        $scope.totalDisplayednew = $scope.allotedactivity_list.length;
                    }
                }
                unlockUI();
            });

            //transfer tickets
            
                lockUI();
                $scope.totaldisplayedtransferticket = 100;
                var url = "api/OsdTrnMyTicket/GetTransferSummary";
                
                SocketService.get(url).then(function (resp) {
                    $scope.transferticket_list = resp.data.transferlistdtl;
                    if ($scope.transferticket_list == null) {
                        $scope.totaltransferticket = 0;
                        $scope.totaldisplayedtransferticket = 0;
                    }
                    else {
                        $scope.totaltransferticket = $scope.transferticket_list.length;
                        if ($scope.transferticket_list.length < 100) {
                            $scope.totaldisplayedtransferticket = $scope.transferticket_list.length;
                        }
                    }
                    unlockUI();
                });

            // Work In Progress
                    lockUI();
                    $scope.totalDisplayedworkinprogress = 100;
                    var url = "api/OsdTrnMyTicket/GetWorkInProgressSummary";
                    
                    SocketService.get(url).then(function (resp) {
                        $scope.workinprogress_list = resp.data.workinprogressdtl;
                        if ($scope.workinprogress_list == null) {
                            $scope.workinprogressCount = 0;
                            $scope.totalDisplayedworkinprogress = 0;
                        }
                        else {
                            $scope.workinprogressCount = $scope.workinprogress_list.length;
                            if ($scope.workinprogress_list.length < 100) {
                                $scope.totalDisplayedworkinprogress = $scope.workinprogress_list.length;
                            }
                        }
                        unlockUI();
                    });

            // Forward
                    lockUI();
                    $scope.totalDisplayedforward = 100;
                    var url = "api/OsdTrnMyTicket/GetForwardSummary";
                    SocketService.get(url).then(function (resp) {
                        $scope.forwarddtl = resp.data.forwarddtl;
                        if ($scope.forwarddtl == null) {
                            $scope.forwardCount = 0;
                            $scope.totalDisplayedforward = 0;
                        }
                        else {
                            $scope.forwardCount = $scope.forwarddtl.length;
                            if ($scope.forwarddtl.length < 100) {
                                $scope.totalDisplayedforward = $scope.forwarddtl.length;
                            }
                        }
                        unlockUI();
                    });

            // Completed
                    lockUI();
                    $scope.totalDisplayedcompleted = 100;
                    var url = "api/OsdTrnMyTicket/GetCompletedSummary";
                    SocketService.get(url).then(function (resp) {

                        $scope.completedticket_list = resp.data.completeddtl;

                        if ($scope.completedticket_list == null) {
                            $scope.totalcompleted = 0;
                            $scope.totalDisplayedcompleted = 0;
                        }
                        else {
                            $scope.totalcompleted = $scope.completedticket_list.length;
                            if ($scope.completedticket_list.length < 100) {
                                $scope.totalDisplayedcompleted = $scope.completedticket_list.length;
                            }
                        }
                        unlockUI();
                    });

            // Closed
                    lockUI();
                    $scope.totalDisplayedclosed = 100;
                    var url = "api/OsdTrnMyTicket/GetClosedSummary";
                    SocketService.get(url).then(function (resp) {
                        $scope.closedtickets_list = resp.data.closeddtl;
                        if ($scope.closedtickets_list == null) {
                            $scope.totalclosed = 0;
                            $scope.totalDisplayedclosed = 0;
                        }
                        else {
                            $scope.totalclosed = $scope.closedtickets_list.length;
                            if ($scope.closedtickets_list.length < 100) {
                                $scope.totalDisplayedclosed = $scope.closedtickets_list.length;
                            }
                        }
                        unlockUI();
                    });
        }
        $scope.loadMorenew = function (pagecountalloted) {
            if (pagecountalloted == undefined) {
                Notify.alert("Enter the Total Summary Count", "warning");
                return;
            }
            lockUI();
            var Number = parseInt(pagecountalloted);
            if ($scope.allotedactivity_list != null) {

                if (pagecountalloted < $scope.allotedactivity_list.length) {
                    $scope.totalDisplayednew += Number;
                    if ($scope.allotedactivity_list.length < $scope.totalDisplayednew) {
                        $scope.totalDisplayednew = $scope.allotedactivity_list.length;
                        Notify.alert(" Total Summary " + $scope.allotedactivity_list.length + " Records Only", "warning");
                    }
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(" Total Summary " + $scope.allotedactivity_list.length + " Records Only", "warning");
                    return;
                }
            }
            unlockUI();
        };
     // Work In Progress
        $scope.work_inprogress = function () {
            lockUI();
            $scope.totalDisplayedworkinprogress = 100;
            var url = "api/OsdTrnMyTicket/GetWorkInProgressSummary";
            SocketService.get(url).then(function (resp) {
                $scope.workinprogress_list = resp.data.workinprogressdtl;
                if ($scope.workinprogress_list == null) {
                    $scope.workinprogressCount = 0;
                    $scope.totalDisplayedworkinprogress = 0;
                }
                else {
                    $scope.workinprogressCount = $scope.workinprogress_list.length;
                    if ($scope.workinprogress_list.length < 100) {
                        $scope.totalDisplayedworkinprogress = $scope.workinprogress_list.length;
                    }
                }
                unlockUI();
            });
        }
        $scope.loadMoreworkinprogress = function (pagecountworkinprogress) {
            if (pagecountworkinprogress == undefined) {
                Notify.alert("Enter the Total Summary Count", "warning");
                return;
            }
            lockUI();
            var Number = parseInt(pagecountworkinprogress);
            if ($scope.workinprogress_list != null) {

                if (pagecountworkinprogress < $scope.workinprogress_list.length) {
                    $scope.totalDisplayedworkinprogress += Number;
                    if ($scope.workinprogress_list.length < $scope.totalDisplayedworkinprogress) {
                        $scope.totalDisplayedworkinprogress = $scope.workinprogress_list.length;
                        Notify.alert(" Total Summary " + $scope.workinprogress_list.length + " Records Only", "warning");
                    }
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(" Total Summary " + $scope.workinprogress_list.length + " Records Only", "warning");
                    return;
                }
            }
            unlockUI();
        };
        //Forward Activity
        $scope.forward_request = function () {
            lockUI();
            $scope.totalDisplayedforward = 100;
            var url = "api/OsdTrnMyTicket/GetForwardSummary";
            SocketService.get(url).then(function (resp) {
                $scope.forwarddtl = resp.data.forwarddtl;
                if ($scope.forwarddtl == null) {
                    $scope.forwardCount = 0;
                    $scope.totalDisplayedforward = 0;
                }
                else {
                    $scope.forwardCount = $scope.forwarddtl.length;
                    if ($scope.forwarddtl.length < 100) {
                        $scope.totalDisplayedforward = $scope.forwarddtl.length;
                    }
                }
                unlockUI();
            });
        }
        $scope.loadMoreforward = function (pagecountforward) {
            if (pagecountforward == undefined) {
                Notify.alert("Enter the Total Summary Count", "warning");
                return;
            }
            lockUI();
            var Number = parseInt(pagecountforward);
            if ($scope.forwarddtl != null) {

                if (pagecountforward < $scope.forwarddtl.length) {
                    $scope.totalDisplayedforward += Number;
                    if ($scope.forwarddtl.length < $scope.totalDisplayedforward) {
                        $scope.totalDisplayedforward = $scope.forwarddtl.length;
                        Notify.alert(" Total Summary " + $scope.forwarddtl.length + " Records Only", "warning");
                    }
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(" Total Summary " + $scope.forwarddtl.length + " Records Only", "warning");
                    return;
                }
            }
            unlockUI();
        };
    //transfer tickets
        $scope.transfer_ticket = function () {
            lockUI();
            $scope.totaldisplayedtransferticket = 100;
            var url = "api/OsdTrnMyTicket/GetTransferSummary";
            SocketService.get(url).then(function (resp) {
                $scope.transferticket_list = resp.data.transferlistdtl;
                if ($scope.transferticket_list == null) {
                    $scope.totaltransferticket = 0;
                    $scope.totaldisplayedtransferticket = 0;
                }
                else {
                    $scope.totaltransferticket = $scope.transferticket_list.length;
                    if ($scope.transferticket_list.length < 100) {
                        $scope.totaldisplayedtransferticket = $scope.transferticket_list.length;
                    }
                }
                unlockUI();
            });
        }
        $scope.loadmoretransferticket = function (pagecounttransferticket) {
            if (pagecounttransferticket == undefined) {
                Notify.alert("enter the total summary count", "warning");
                return;
            }
            lockUI();
            var number = parseInt(pagecounttransferticket);
            if ($scope.transferticket_list != null) {

                if (pagecounttransferticket < $scope.transferticket_list.length) {
                    $scope.totaldisplayedtransferticket += number;
                    if ($scope.transferticket_list.length < $scope.totaldisplayedtransferticket) {
                        $scope.totaldisplayedtransferticket = $scope.transferticket_list.length;
                        Notify.alert(" total summary " + $scope.transferticket_list.length + " records only", "warning");
                    }
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(" total summary " + $scope.transferticket_list.length + " records only", "warning");
                    return;
                }
            }
            unlockUI();
        };
   // Completed Activity
        $scope.completed_ticket = function () {
            lockUI();
            $scope.totalDisplayedcompleted = 100;
            var url = "api/OsdTrnMyTicket/GetCompletedSummary";
            SocketService.get(url).then(function (resp) {
               
                $scope.completedticket_list = resp.data.completeddtl;
                
                if ($scope.completedticket_list == null) {
                    $scope.totalcompleted = 0;
                    $scope.totalDisplayedcompleted = 0;
                }
                else {
                    $scope.totalcompleted = $scope.completedticket_list.length;
                    if ($scope.completedticket_list.length < 100) {
                        $scope.totalDisplayedcompleted = $scope.completedticket_list.length;
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
            if ($scope.completedticket_list != null) {

                if (pagecountcompleted < $scope.completedticket_list.length) {
                    $scope.totalDisplayedcompleted += Number;
                    if ($scope.completedticket_list.length < $scope.totalDisplayedcompleted) {
                        $scope.totalDisplayedcompleted = $scope.completedticket_list.length;
                        Notify.alert(" Total Summary " + $scope.completedticket_list.length + " Records Only", "warning");
                    }
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(" Total Summary " + $scope.completedticket_list.length + " Records Only", "warning");
                    return;
                }
            }
            unlockUI();
        };
    // Closed Activity
        $scope.closed_ticket = function () {
            lockUI();
            $scope.totalDisplayedclosed = 100;
            var url = "api/OsdTrnMyTicket/GetClosedSummary";
            SocketService.get(url).then(function (resp) {
                $scope.closedtickets_list = resp.data.closeddtl;
                if ($scope.closedtickets_list == null) {
                    $scope.totalclosed = 0;
                    $scope.totalDisplayedclosed = 0;
                }
                else {
                    $scope.totalclosed = $scope.closedtickets_list.length;
                    if ($scope.closedtickets_list.length < 100) {
                        $scope.totalDisplayedclosed = $scope.closedtickets_list.length;
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
            if ($scope.closedtickets_list != null) {

                if (pagecountclosed < $scope.closedtickets_list.length) {
                    $scope.totalDisplayedclosed += Number;
                    if ($scope.closedtickets_list.length < $scope.totalDisplayedclosed) {
                        $scope.totalDisplayedclosed = $scope.closedtickets_list.length;
                        Notify.alert(" Total Summary " + $scope.closedtickets_list.length + " Records Only", "warning");
                    }
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(" Total Summary " + $scope.closedtickets_list.length + " Records Only", "warning");
                    return;
                }
            }
            unlockUI();
        };

        $scope.view360 = function (val, val2, val3, val4) {
            $scope.servicerequest_gid = val;
            $scope.bankalert_flag = val2;
            $scope.bankalert2allocated_gid = val3;
            $scope.customer_gid = val4;

            var servicerequest_gid = val;
            var bankalert_flag = val2;
            var bankalert2allocated_gid = val3;
            var customer_gid = val4;

         
            var param = {
                servicerequest_gid: servicerequest_gid,
                bankalert_flag : bankalert_flag,
                bankalert2allocated_gid : bankalert2allocated_gid,
                customer_gid : customer_gid
            }
            var url = 'api/OsdTrnMyTicket/GetServiceRequestForwardView360Update';
            lockUI()
            SocketService.getparams(url, param).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI()
                }
                else {
                    unlockUI();
                }
            });
            
            var val5 = "N";
            $location.url('app/osdTrnMyActivityAllotted360?hash=' + cmnfunctionService.encryptURL('servicerequest_gid=' + val + '&bankalert_flag=' + val2 + '&bankalert2allocated_gid=' + val3 + '&customer_gid=' + val4 + '&RequestCompletedFlag=' + val5 + '&lstab=Alloted'));
        }
        $scope.workinprogressview360 = function (val, val2, val3, val4) {
            $scope.servicerequest_gid = val;
            $scope.bankalert_flag = val2;
            $scope.bankalert2allocated_gid = val3;
            $scope.customer_gid = val4;

            var servicerequest_gid = val;
            var bankalert_flag = val2;
            var bankalert2allocated_gid = val3;
            var customer_gid = val4;

           
            var param = {
                servicerequest_gid: servicerequest_gid,
                bankalert_flag : bankalert_flag,
                bankalert2allocated_gid : bankalert2allocated_gid,
                customer_gid : customer_gid
            }
            var url = 'api/OsdTrnMyTicket/GetServiceRequestForwardView360Update';
            lockUI()
            SocketService.getparams(url, param).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI()
                }
                else {
                    unlockUI();
                }
            });
            var val5 = "N";
            $location.url('app/osdTrnMyActivity360?hash=' + cmnfunctionService.encryptURL('servicerequest_gid=' + val + '&bankalert_flag=' + val2 + '&bankalert2allocated_gid=' + val3 + '&customer_gid=' + val4 + '&RequestCompletedFlag=' + val5 + '&lstab=Work-InProgress'));
        }
        $scope.forwardview360 = function (val, val2, val3, val4) {
            $scope.servicerequest_gid = val;
            $scope.bankalert_flag = val2;
            $scope.bankalert2allocated_gid = val3;
            $scope.customer_gid = val4;

            var servicerequest_gid = val;
            var bankalert_flag = val2;
            var bankalert2allocated_gid = val3;
            var customer_gid = val4;

          
            var param = {
                servicerequest_gid: servicerequest_gid,
                bankalert_flag : bankalert_flag,
                bankalert2allocated_gid : bankalert2allocated_gid,
                customer_gid : customer_gid
            }
        var url = 'api/OsdTrnMyTicket/GetServiceRequestForwardView360Update';
            lockUI()
            SocketService.getparams(url, param).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI()
                }
                else {
                    unlockUI();
                }
            });
            var val5 = "N";
            $location.url('app/osdTrnMyActivityForward360?hash=' + cmnfunctionService.encryptURL('servicerequest_gid=' + val + '&bankalert_flag=' + val2 + '&bankalert2allocated_gid=' + val3 + '&customer_gid=' + val4 + '&RequestCompletedFlag=' + val5 + '&lstab=Forward'));
        }
        $scope.taggedticketview360 = function (val) {
            $scope.servicerequest_gid = val;            
            var val2 = "N";
            $location.url('app/osdTrnMyTicketTag?hash=' + cmnfunctionService.encryptURL('servicerequest_gid=' + val + '&RequestCompletedFlag=' + val2 + '&lstab=Tagged-Ticket'));
        }
        //$scope.transferview360 = function (val) {
        //    $scope.servicerequest_gid = val;
        //    $scope.servicerequest_gid = localStorage.setItem('servicerequest_gid', val);
        //    localStorage.setItem('RequestCompletedFlag', 'N');
        //    $location.url('app/osdTrnMyActivityTransfer?lstab=Transfer-Ticket');
        //}
        $scope.transferview360 = function (val, val2, val3, val4) {
            $scope.servicerequest_gid = val;
            $scope.bankalert_flag = val2;
            $scope.bankalert2allocated_gid = val3;
            $scope.customer_gid = val4;

            var servicerequest_gid = val;
            var bankalert_flag = val2;
            var bankalert2allocated_gid = val3;
            var customer_gid = val4;

        
            var param = {
                servicerequest_gid: servicerequest_gid,
                bankalert_flag : bankalert_flag,
                bankalert2allocated_gid : bankalert2allocated_gid,
                customer_gid : customer_gid
            }
            var url = 'api/OsdTrnMyTicket/GetServiceRequestTransferView360Update';
            lockUI()
            SocketService.getparams(url, param).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI()
                }
                else {
                    unlockUI();
                }
            });
            var val5 = "N";
            $location.url('app/osdTrnMyActivityTransfer?hash=' + cmnfunctionService.encryptURL('servicerequest_gid=' + val + '&bankalert_flag=' + val2 + '&bankalert2allocated_gid=' + val3 + '&customer_gid=' + val4 + '&RequestCompletedFlag=' + val5 + '&lstab=Transfer-Ticket'));
        }
        $scope.completedview360 = function (val, val2, val3, val4) {
            $scope.servicerequest_gid = val;
            $scope.bankalert_flag = val2;
            $scope.bankalert2allocated_gid = val3;
            $scope.customer_gid = val4;

            var servicerequest_gid = val;
            var bankalert_flag = val2;
            var bankalert2allocated_gid = val3;
            var customer_gid = val4;
                              
           
            var val5 = "Y";
            $location.url('app/osdTrnMyActivityComplete?hash=' + cmnfunctionService.encryptURL('servicerequest_gid=' + val + '&bankalert_flag=' + val2 + '&bankalert2allocated_gid=' + val3 + '&customer_gid=' + val4 + '&RequestCompletedFlag=' + val5 + '&lstab=Completed'));
        }
        $scope.closedview360 = function (val, val2, val3, val4) {
            $scope.servicerequest_gid = val;
            $scope.bankalert_flag = val2;
            $scope.bankalert2allocated_gid = val3;
            $scope.customer_gid = val4;

            var servicerequest_gid = val;
            var bankalert_flag = val2;
            var bankalert2allocated_gid = val3;
            var customer_gid = val4;

         
            
            var val5 = "Y";
            $location.url('app/osdTrnMyActivityComplete?hash=' + cmnfunctionService.encryptURL('servicerequest_gid=' + val + '&bankalert_flag=' + val2 + '&bankalert2allocated_gid=' + val3 + '&customer_gid=' + val4 + '&RequestCompletedFlag=' + val5 + '&lstab=Closed'));
        }

        $scope.getApprovalRequest = function (servicerequest_gid,val) {
            var modalInstance = $modal.open({
                templateUrl: '/getApprovalmodal.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            var val = val;
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    servicerequest_gid: servicerequest_gid
                }
                var url = 'api/OsdTrnMyTicket/EmployeeNotIn';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.employee_list = resp.data.employeelist;
                });
                var url = 'api/OsdMstActivity/GetTeamSummary';
                SocketService.get(url).then(function (resp) {
                    $scope.supportdtllist = resp.data.supportdtl;
                });
                var url = 'api/OsdTrnMyTicket/TmpAllMembersDelete';
                SocketService.get(url).then(function (resp) {
                    
                });

                $scope.ok = function () {
                    modalInstance.close('closed');
                };

                $scope.ApprovalMembercancel = function (tmpapprovalmember_gid)
                {
                    var params = {
                        tmpapprovalmember_gid: tmpapprovalmember_gid,
                        servicerequest_gid: servicerequest_gid,
                    }
                    var url = 'api/OsdTrnMyTicket/TmpApprovalMembersDelete';
                    SocketService.post(url, params).then(function (resp) {
                        $scope.approvalmember = resp.data.approvalmember;

                        var param = {
                            servicerequest_gid: servicerequest_gid
                        }
                        var url = 'api/OsdTrnMyTicket/EmployeeNotIn';
                        SocketService.getparams(url, param).then(function (resp) {
                            $scope.employee_list = resp.data.employeelist;
                        });


                    });



                }

                $scope.coreChanged = function(cboapproval_member)
                {
                        var params = {
                            approvalgid: $scope.cboapproval_member.employee_gid,
                            approvalname: $scope.cboapproval_member.employee_name,
                            servicerequest_gid: servicerequest_gid,
                        }

                            lockUI();
                            var url = "api/OsdTrnMyTicket/TempApprovalMember";
                            SocketService.post(url, params).then(function (resp) {
                               
                                if (resp.data.status == true) {
                                    Notify.alert(resp.data.message, {
                                        status: 'success',
                                        pos: 'top-center',
                                        timeout: 3000
                                      
                                    });
                                    unlockUI();
                                    $scope.current = $state.current.name;
                                    ScopeValueService.store("dataldCtrl", $scope);
                                    //$state.go('app.pageredirect');
                                    $scope.cboapproval_member = "";
                                    var params = {
                                        servicerequest_gid: servicerequest_gid
                                    }
                                    var url = 'api/OsdTrnMyTicket/TmpApprovalMembersView';
                                    SocketService.getparams(url, params).then(function (resp) {
                                        $scope.approvalmember = resp.data.approvalmember;
                                    });

                                    var url = 'api/OsdTrnMyTicket/EmployeeNotIn';
                                    SocketService.getparams(url, params).then(function (resp) {
                                        $scope.employee_list = resp.data.employeelist;
                                    });
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

                $scope.getapprovalclick = function () {
                    var params = {
                        approval_remarks: $scope.approval_remarks,
                        approval_type: $scope.approval_type,
                        approval_basedon: 'HA',
                        servicerequest_gid: servicerequest_gid
                    }
                    
                    lockUI();
                    var url = "api/OsdTrnMyTicket/PostApprovalGet";
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                            if (val == "alloted")
                            {
                                activate();                                
                                $location.url('app/osdTrnMyTicket?hash=' + cmnfunctionService.encryptURL('lstab=Alloted'));
                            }
                            else if (val == "work")
                            {
                                activate();
                                work_inprogress();
                                $location.url('app/osdTrnMyTicket?hash=' + cmnfunctionService.encryptURL('lstab=Work-InProgress'));
                            }
                            else if (val == "forward") {
                                activate();
                                forward_request();
                                $location.url('app/osdTrnMyTicket?hash=' + cmnfunctionService.encryptURL('lstab=Forward'));
                            }
                           
                        }
                        else {
                            //modalInstance.close('closed');
                            alert(resp.data.message, {
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


        $scope.getReApprovalRequest = function (servicerequest_gid,val) {
            var modalInstance = $modal.open({
                templateUrl: '/getReApprovalmodal.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            var val = val;
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var url = 'api/employee/employee';
                SocketService.get(url).then(function (resp) {
                    $scope.employee_list = resp.data.employee_list;
                });
                var url = 'api/OsdMstActivity/GetTeamSummary';
                SocketService.get(url).then(function (resp) {
                    $scope.supportdtllist = resp.data.supportdtl;
                });
                $scope.ok = function () {
                    modalInstance.close('closed');
                };

                $scope.getreapprovalclick = function () {
                    var params = {
                        approvalname: $scope.cboapproval_member.employee_name,
                        approvalgid: $scope.cboapproval_member.employee_gid,
                        approval_remarks: $scope.approval_remarks,
                        approval_type: 'Approval',
                        approval_basedon: 'RA',
                        servicerequest_gid: servicerequest_gid
                    }
                    lockUI();
                    var url = "api/OsdTrnMyTicket/PostApprovalGet";
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                            if (val == "alloted") {
                                activate();
                                $location.url('app/osdTrnMyTicket?hash=' + cmnfunctionService.encryptURL('lstab=Alloted'));
                            }
                            else if (val == "work") {
                                activate();
                                work_inprogress();
                                $location.url('app/osdTrnMyTicket?hash=' + cmnfunctionService.encryptURL('lstab=Work-InProgress'));
                            }
                            else if (val == "forward")
                            {
                                activate();
                                forward_request();
                                $location.url('app/osdTrnMyTicket?hash=' + cmnfunctionService.encryptURL('lstab=Forward'));
                            }
                        }
                        else {
                            modalInstance.close('closed');
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

    // Transfer Allocation in Allotted Tab
        $scope.TransferAllocation = function (servicerequest_gid, assigned_team, assigned_member, assigned_membergid, assigned_supportteamgid) {
       
            var modalInstance = $modal.open({
                templateUrl: '/transferallocationmodal.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            var servicerequest_gid = servicerequest_gid;
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var url = 'api/OsdMstActivity/GetTeamSummary';
                SocketService.get(url).then(function (resp) {
                    $scope.supportdtllist = resp.data.supportdtl;
                });

                $scope.onselectedchangeteam = function (team_name) {
                  
                    var params = {
                        supportteam_gid: $scope.cbosuppport_team.supportteam_gid
                    }
                    var url = 'api/OsdMstSupportTeam/GetTeamMemberExcept';
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.teammembers_list = resp.data.teammembers;
                    });
                }

                var params = {
                    servicerequest_gid: servicerequest_gid
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
                        assigned_member: assigned_member,
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

   // Transfer Allocation in Work In Progress Tab
        $scope.TransferAllocationwithRemarks = function (servicerequest_gid, assigned_team, assigned_to, assigned_membergid, assigned_supportteamgid) {
          
            var modalInstance = $modal.open({
                templateUrl: '/transferallocationwithremarksmodal.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            var servicerequest_gid = servicerequest_gid;
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var url = 'api/OsdMstActivity/GetTeamSummary';
                SocketService.get(url).then(function (resp) {
                    $scope.supportdtllist = resp.data.supportdtl;
                });

                $scope.onselectedchangeteam = function (team_name) {
                   
                    var params = {
                        supportteam_gid: $scope.cbosuppport_team.supportteam_gid
                    }
                    var url = 'api/OsdMstSupportTeam/GetTeamMemberExcept';
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.teammembers_list = resp.data.teammembers;
                    });
                }

                var params = {
                    servicerequest_gid: servicerequest_gid
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
                          
                            activate();
                            $location.url('app/osdTrnMyTicket?hash=' + cmnfunctionService.encryptURL('lstab=Work-InProgress'));
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