(function () {
    'use strict';

    angular
        .module('angle')
        .controller('iasnTrnForwardSummary', iasnTrnForwardSummary);

    iasnTrnForwardSummary.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function iasnTrnForwardSummary($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'iasnTrnForwardSummary';

        activate();
        lockUI();
        function activate() {
            $scope.assignto_team = false;
            $scope.assignto_employee = false;
            $scope.total = 0;
            $scope.totalDisplayed = 100;
            if ($scope.page == undefined) {
                localStorage.setItem('page', 'workitemsummary')
            }
            $scope.page = localStorage.getItem('page');
            var url = 'api/IasnTrnWorkItem/WorkItemCounts';
            SocketService.get(url).then(function (resp) {
                $scope.count_workitempending = resp.data.count_workitempending;
                $scope.count_workitemassigned = resp.data.count_workitemassigned;
                $scope.count_pushback = resp.data.count_pushback;
                $scope.count_forward = resp.data.count_forward;
                $scope.count_close = resp.data.count_close;
                $scope.count_archival = resp.data.count_archival;
                $scope.count_composemail = resp.data.count_composemail;

            });
            var url = 'api/IasnTrnWorkItem/WorkItemPendingSummary';
            SocketService.get(url).then(function (resp) {
                $scope.WorkItemPending_List = resp.data.MdlWorkItem;
                if ($scope.WorkItemPending_List == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.WorkItemPending_List.length;
                    if ($scope.WorkItemPending_List.length < 100) {
                        $scope.totalDisplayed = $scope.WorkItemPending_List.length;
                    }
                }
            });

            var url = 'api/IasnTrnWorkItem/WorkItemSummary';
            SocketService.get(url).then(function (resp) {
                $scope.WorkItemAllotted_List = resp.data.MdlWorkItem;

                if ($scope.WorkItemAllotted_List == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.WorkItemAllotted_List.length;
                    if ($scope.WorkItemAllotted_List.length < 100) {
                        $scope.totalDisplayed = $scope.WorkItemAllotted_List.length;
                    }
                }
            });
            var url = 'api/IasnTrnWorkItem/WorkItemForwardSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.WorkItemForward_List = resp.data.MdlWorkItem;
                if ($scope.WorkItemForward_List == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.WorkItemForward_List.length;
                    if ($scope.WorkItemForward_List.length < 100) {
                        $scope.totalDisplayed = $scope.WorkItemForward_List.length;
                    }
                }

            });
        }

        $scope.EmployeeProfile = function (emp_gid) {
            var url = 'api/IasnTrnWorkItem/EmployeeProfile';
            var params = {
                employee_gid: emp_gid
            }
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.user_code = resp.data.user_code;
                    $scope.user_name = resp.data.user_name;
                    $scope.user_photo = resp.data.user_photo;
                    $scope.user_designation = resp.data.user_designation;
                    $scope.user_department = resp.data.user_department;
                    $scope.user_mobileno = resp.data.user_mobileno;
                }
                else {
                    $scope.user_code = "-";
                    $scope.user_name = "-";
                    $scope.user_photo = "N";
                    $scope.user_designation = "-";
                    $scope.user_department = "-";
                }
            });

        }
        $scope.loadMore = function (pagecount) {
            if (pagecount == undefined) {
                Notify.alert("Enter the Total Summary Count", "warning");
                return;
            }
            lockUI();

            var Number = parseInt(pagecount);
            // new code start
            if ($scope.total != 0) {

                if (pagecount < $scope.total) {
                    $scope.totalDisplayed += Number;
                    if ($scope.total < $scope.totalDisplayed) {
                        $scope.totalDisplayed = $scope.total;
                        Notify.alert(" Total Summary " + $scope.total + " Records Only", "warning");
                    }
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(" Total Summary " + $scope.total + " Records Only", "warning");
                    return;
                }
            }
            // new code end
            unlockUI();
        };
        // Action Work Item 360
        $scope.WorkItem360 = function (val) {
            localStorage.setItem('email_gid', val)
            var params = {
                email_gid: val
            }
            var url = 'api/IasnTrnWorkItem/MailSeen';
            SocketService.getparams(url, params).then(function (resp) {
            });
            if ($scope.page == undefined) {
                localStorage.setItem('page', 'workitemsummarypage')
            }
            else {
                localStorage.setItem('page', $scope.page)
            }
            localStorage.setItem('page', 'Forward')
            $state.go("app.iasnTrnWorkItem360");
        }

        // Action Work Item Allotted 360
        $scope.WorkItemAllotted360 = function (val) {
            localStorage.setItem('email_gid', val)
            var params = {
                email_gid: val
            }
            var url = 'api/IasnTrnWorkItem/MailSeen';
            SocketService.getparams(url, params).then(function (resp) {
            });
            if ($scope.page == undefined) {
                localStorage.setItem('page', 'workitemsummarypage')
            }
            else {
                localStorage.setItem('page', $scope.page)
            }

            $state.go("app.iasnTrnWorkItemAllotted360");
        }



        $scope.WorkItem = function () {
            var url = 'api/IasnTrnWorkItem/WorkItemPendingSummary';
            SocketService.get(url).then(function (resp) {
                $scope.WorkItemPending_List = resp.data.MdlWorkItem;
                if ($scope.WorkItemPending_List == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.WorkItemPending_List.length;
                    if ($scope.WorkItemPending_List.length < 100) {
                        $scope.totalDisplayed = $scope.WorkItemPending_List.length;
                    }
                }
            });
        }

        $scope.Pushback = function () {
            var url = 'api/IasnTrnWorkItem/WorkItemPushbackSummary';
            SocketService.get(url).then(function (resp) {
                $scope.WorkItemPushback_List = resp.data.MdlWorkItem;
                if ($scope.WorkItemPushback_List == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.WorkItemPushback_List.length;
                    if ($scope.WorkItemPushback_List.length < 100) {
                        $scope.totalDisplayed = $scope.WorkItemPushback_List.length;
                    }
                }

            });
        }

       
        $scope.CloseTab = function () {
            var url = 'api/IasnTrnWorkItem/WorkItemCloseSummary';
            SocketService.get(url).then(function (resp) {
                $scope.WorkItemClose_List = resp.data.MdlWorkItem;
                if ($scope.WorkItemClose_List == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.WorkItemClose_List.length;
                    if ($scope.WorkItemClose_List.length < 100) {
                        $scope.totalDisplayed = $scope.WorkItemClose_List.length;
                    }
                }

            });
        }


        $scope.AssignZone = function (email_gid, workitemref_no, email_from, email_subject, zone_gid, zone_name) {

            var modalInstance = $modal.open({
                templateUrl: '/assignZoneContent.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });


            ModalInstanceCtrl.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];
            function ModalInstanceCtrl($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {
                $scope.workitemref_no = workitemref_no;
                $scope.subject = email_subject;
                $scope.from = email_from;

                $scope.rdb_acks = "N";
                var url = "api/IasnMstZone/ZoneSummary";
                SocketService.get(url).then(function (resp) {

                    $scope.zone_list = resp.data.MdlZoneSummary;

                });
                //if(zone_gid==undefined || zone_gid==""){
                //    $scope.zone_flag="N"

                //}
                //else{
                //    $scope.zone_name=zone_gid;
                //    $scope.lblzonename=zone_name;
                //    $scope.zone_flag="Y"
                //}

                var url = 'api/IasnTrnWorkItem/IsnEmployee';
                SocketService.get(url).then(function (resp) {
                    $scope.employee_list = resp.data.MdlIsnEmployee;

                });
                $scope.close = function () {
                    modalInstance.close('closed');
                };

                $scope.AssignToUpdate = function () {


                    var params = {
                        email_gid: email_gid,
                        zone_gid: $scope.zone_name,
                        zone_name: $('#zone_name :selected').text(),
                        zone_flag: $scope.zone_flag,
                        acknowledgement_flag: $scope.rdb_acks,
                    }

                    var url = "api/IasnTrnWorkItem/AssignZone";
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status = true) {
                            Notify.alert(resp.data.message, 'success');
                        }
                        else {
                            Notify.alert(resp.data.message, 'warning');
                        }
                        modalInstance.close('closed');
                        activate();
                    });

                }
            }
        }

        $scope.TransferWorkItem = function (email_gid, workitemref_no, email_from, email_subject, zone_name, assign_to) {

            var modalInstance = $modal.open({
                templateUrl: '/transferWIContent.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });


            ModalInstanceCtrl.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];
            function ModalInstanceCtrl($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {
                $scope.workitemref_no = workitemref_no;
                $scope.subject = email_subject;
                $scope.from = email_from;
                $scope.zone_name = zone_name;
                $scope.checkeremployee_name = assign_to;

                var url = 'api/IasnTrnWorkItem/IsnEmployee';
                SocketService.get(url).then(function (resp) {
                    $scope.employee_list = resp.data.MdlIsnEmployee;

                });

                var params = {
                    lsemail_gid: email_gid
                }
                var url = 'api/IasnTrnWorkItem/TransferLog';
                SocketService.getparams(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        $scope.transferlog_list = resp.data.MdlTransferLog;
                        $scope.showtransfer = true;
                    }
                    else {
                        $scope.showtransfer = false;
                    }


                });
                $scope.close = function () {
                    modalInstance.close('closed');
                };

                $scope.transferWIUpdate = function () {

                    if ($scope.transfer_to == undefined) {
                        modalInstance.close('closed');
                        Notify.alert('Kindly Select the Assign to Person', 'warning');
                        return;
                    }

                    var params = {
                        email_gid: email_gid,
                        employee_gid: $scope.transfer_to,
                        employee_name: $('#transfer_to :selected').text(),
                        zone_gid: '',
                        zone_name: '',
                        zone_flag: 'Y'
                    }

                    var url = "api/IasnTrnWorkItem/AssignTo";
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status = true) {
                            Notify.alert(resp.data.message, 'success');
                        }
                        else {
                            Notify.alert(resp.data.message, 'warning');
                        }
                        modalInstance.close('closed');
                        activate();
                    });

                }
            }
        }

        $scope.CloseWorkItem = function (email_gid, workitemref_no, email_from, email_subject, zone_name) {
            var modalInstance = $modal.open({
                templateUrl: '/closeWIContent.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];
            function ModalInstanceCtrl($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {
                $scope.workitemref_no = workitemref_no;
                $scope.subject = email_subject;
                $scope.from = email_from;

                $scope.CloseWIUpdate = function () {

                    var params = {
                        email_gid: email_gid,
                        decision: 'Close',
                        employee_gid: '',
                        employee_name: '',
                        remarks: $scope.close_remarks,
                        close_acknowledge: $scope.Acknowledge_mail_trigger,
                        mailcontent: 'Close',
                        customer_gid: '',
                        customer_name: '',
                        subject: '',
                        tomail_id: '',
                        ccmail_id: '',
                        bccmail_id: ''
                    }


                    var url = 'api/IasnTrnWorkItem/PostDecision';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, 'success')
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
        // Add Work Item Code Ends

        // Merge Work Item Code Starts
        $scope.mergeworkitem = function (email_gid, subject, ref_no) {
            localStorage.setItem('email_gid', email_gid);
            localStorage.setItem('email_subject', subject);
            localStorage.setItem('workitemref_no', ref_no);

            if ($scope.page == undefined) {
                localStorage.setItem('page', 'workitemsummarypage')
            }
            else {
                localStorage.setItem('page', 'workitemsummarypage')
            }

            $state.go('app.iasnWomMergeWorkItem');
        }
        // Merge Work Item Code Ends
        $scope.Alloted = function () {
            $state.go('app.iasnTrnAllotedSummary');
        }
        $scope.Pushback = function () {
            $state.go('app.iasnTrnPushbackSummary');
        }
        $scope.Forward = function () {
            $state.go('app.iasnTrnForwardSummary');
        }
        $scope.Close = function () {
            $state.go('app.iasnTrnCloseSummary');
        }
        $scope.WorkItem = function () {
            $state.go('app.iasnTrnWorkItemSummary');
        }
        $scope.ComposeMail = function () {
            $state.go('app.iasnWomWorkOrderSummary');
        }
    }
})();
