(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rmVisitReportcontroller', rmVisitReportcontroller);

    rmVisitReportcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams','DownloaddocumentService'];

    function rmVisitReportcontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'rmVisitReportcontroller';

        activate();

        function activate() {
            lockUI();

            localStorage.setItem('RSK_RM', 'Y');
            localStorage.setItem('CC', 'N');
            localStorage.setItem('AgrCC', 'N');
            localStorage.setItem('AgrSuprCC', 'N');
            var url = "api/visitReport/GetRMTodayActivity";
            SocketService.get(url).then(function (resp) {
                $scope.todayactivity = resp.data.todayactivity;
                $scope.monthlyactivity = resp.data.monthlyactivity;
                $scope.count_current = resp.data.count_current;
                $scope.count_upcoming = resp.data.count_upcoming;
                $scope.count_completed = resp.data.count_completed;
                $scope.count_exclusion = resp.data.count_external;
                $scope.overallRMdistrictdtl = resp.data.mydistrictdtl;
                //angular.forEach($scope.overallRMdistrictdtl, function (value, key) {
                //    var params = {
                //        state_gid: value.state_gid,
                //        district_gid: value.district_gid
                //    };
                //    var url = 'api/visitReport/GetRMCustomerDetails';
                //    SocketService.getparams(url, params).then(function (resp) {
                //        value.mycustomerdtl = resp.data.mycustomerdtl;
                //        value.expand = false;
                //    });
                //});
                unlockUI();
            });
        }

        $scope.myschedule = function () {
            lockUI();
            $scope.MySchedule = true;
            localStorage.setItem('RSK_RM', 'Y');
            var url = "api/visitReport/GetRMTodayActivity";
            SocketService.get(url).then(function (resp) {
                $scope.todayactivity = resp.data.todayactivity;
                $scope.monthlyactivity = resp.data.monthlyactivity;
                $scope.count_current = resp.data.count_current;
                $scope.count_upcoming = resp.data.count_upcoming;
                $scope.count_completed = resp.data.count_completed;
                unlockUI();
            });
        }

        $scope.current = function () {
            $scope.Current = true;
            lockUI();
            $scope.totalCurrentDisplayed = 100;
            var url = "api/allocationManagement/GetRMcurrentallocateddtl";
            SocketService.get(url).then(function (resp) {
                $scope.count_current = resp.data.count_current;
                $scope.allocatedList = resp.data.rmallocation;
                if ($scope.allocatedList == null) {
                    $scope.totalCurrent = 0;
                    $scope.totalCurrentDisplayed = 0;
                }
                else {
                    $scope.totalCurrent = $scope.allocatedList.length;
                    if ($scope.allocatedList.length < 100) {
                        $scope.totalCurrentDisplayed = $scope.allocatedList.length;
                    }
                }
                unlockUI();
            });
        }

        $scope.upcoming = function () {
            $scope.Upcoming = true;
            lockUI();
            $scope.totalUpcomingDisplayed = 100;
            var url = "api/allocationManagement/GetRMupcomingallocateddtl";
            SocketService.get(url).then(function (resp) {
                $scope.count_upcoming = resp.data.count_upcoming;
                $scope.upcomingallocationList = resp.data.rmallocation;
                if ($scope.upcomingallocationList == null) {
                    $scope.totalUpcoming = 0;
                    $scope.totalUpcomingDisplayed = 0;
                }
                else {
                    $scope.totalUpcoming = $scope.upcomingallocationList.length;
                    if ($scope.upcomingallocationList.length < 100) {
                        $scope.totalUpcomingDisplayed = $scope.upcomingallocationList.length;
                    }
                }
                unlockUI();
            });
        }

        $scope.exclusion = function () {
            $scope.Upcoming = true;
            lockUI();
            $scope.totalExclusionDisplayed = 100;
            var url = "api/allocationManagement/GetRMExclusiondetails";
            SocketService.get(url).then(function (resp) {
                $scope.count_exclusion = resp.data.count_exclusion;
                $scope.exclusionAllocationlist = resp.data.exclusionAllocation;
                if ($scope.exclusionAllocationlist == null) {
                    $scope.totalExclusion = 0;
                    $scope.totalExclusionDisplayed = 0;
                }
                else {
                    $scope.totalExclusion = $scope.exclusionAllocationlist.length;
                    if ($scope.exclusionAllocationlist.length < 100) {
                        $scope.totalExclusionDisplayed = $scope.exclusionAllocationlist.length;
                    }
                }
                unlockUI();
            });
        }

        $scope.completed = function () {
            $scope.Completed = true;
            lockUI();
            var url = "api/allocationManagement/getRMCompleteddetails";
            SocketService.get(url).then(function (resp) {
                $scope.count_completed = resp.data.count_completed;
                $scope.completedallocationList = resp.data.rmallocation;
                if ($scope.completedallocationList == null) {
                    $scope.totalCompleted = 0;
                    $scope.totalCompletedDisplayed = 0;
                }
                else {
                    $scope.totalCompleted = $scope.completedallocationList.length;
                    if ($scope.completedallocationList.length < 100) {
                        $scope.totalCompletedDisplayed = $scope.completedallocationList.length;
                    }
                }
                unlockUI();
            });
        }

        $scope.loadCurrentMore = function (pageCurrentcount) {
            if (pageCurrentcount == undefined) {
                Notify.alert("Enter the Total Summary Count", "warning");
                return;
            }
            lockUI();
            var Number = parseInt(pageCurrentcount);
            if ($scope.totalCurrentDisplayed < $scope.allocatedList.length) {
                $scope.totalCurrentDisplayed += Number;
                unlockUI();
            }
            else {
                unlockUI();
                Notify.alert(" Total Summary " + $scope.allocatedList.length + " Records Only", "warning");
                return;
            }
        };

        $scope.loadUpcomingMore = function (pageUpcomingcount) {
            if (pageUpcomingcount == undefined) {
                Notify.alert("Enter the Total Summary Count", "warning");
                return;
            }
            lockUI();
            var Number = parseInt(pageUpcomingcount);
            if ($scope.totalUpcomingDisplayed < $scope.upcomingallocationList.length) {
                $scope.totalUpcomingDisplayed += Number;
                unlockUI();
            }
            else {
                unlockUI();
                Notify.alert(" Total Summary " + $scope.upcomingallocationList.length + " Records Only", "warning");
                return;
            }
        };

        $scope.loadExclusionMore = function (pageExclusioncount) {
            if (pageExclusioncount == undefined) {
                Notify.alert("Enter the Total Summary Count", "warning");
                return;
            }
            lockUI();
            var Number = parseInt(pageExclusioncount);
            if ($scope.totalUpcomingDisplayed < $scope.exclusionAllocationlist.length) {
                $scope.totalUpcomingDisplayed += Number;
                unlockUI();
            }
            else {
                unlockUI();
                Notify.alert(" Total Summary " + $scope.exclusionAllocationlist.length + " Records Only", "warning");
                return;
            }
        };

        $scope.loadCompletedMore = function (pageCompletedcount) {
            if (pageCompletedcount == undefined) {
                Notify.alert("Enter the Total Summary Count", "warning");
                return;
            }
            lockUI();
            var Number = parseInt(pageCompletedcount);
            if ($scope.totalUpcomingDisplayed < $scope.completedallocationList.length) {
                $scope.totalUpcomingDisplayed += Number;
                unlockUI();
            }
            else {
                unlockUI();
                Notify.alert(" Total Summary " + $scope.completedallocationList.length + " Records Only", "warning");
                return;
            }
        };

        $scope.schedulecurrentlog = function (allocationdtl_gid, customer_gid, customername) {
            // localStorage.setItem('allocationdtl_gid', allocationdtl_gid);
            // $state.go('app.rmScheduleLogDetails');
            $location.url('app/rmScheduleLogDetails?allocationdtl_gid='+allocationdtl_gid);
        }
    
        $scope.scheduleupcominglog = function (allocationdtl_gid) {
            // localStorage.setItem('allocationdtl_gid', allocationdtl_gid);
            // $state.go('app.rmScheduleLogDetails');
            $location.url('app/rmScheduleLogDetails?allocationdtl_gid=' + allocationdtl_gid);
        }

        $scope.schedulecompletelog = function (allocationdtl_gid) {
            // localStorage.setItem('allocationdtl_gid', allocationdtl_gid);
            // $state.go('app.rmScheduleLogView');
            $location.url('app/rmScheduleLogView?allocationdtl_gid='+allocationdtl_gid);
        }

        $scope.observationreport = function (allocationdtl_gid, observation_reportgid) {
            // localStorage.setItem('allocationdtl_gid', allocationdtl_gid);
            // $state.go('app.rmObservationReport');
            //$location.url('app/rmObservationReport?allocationdtl_gid='+allocationdtl_gid);
            $location.url('app/rmObservationReport?allocationdtl_gid=' + allocationdtl_gid + '&?&observation_reportgid=' + observation_reportgid);
        }

        $scope.exclusioncustomer = function (allocationdtl_gid, customer_urn, customername, allocation_status) {

            var modalInstance = $modal.open({
                templateUrl: '/exclusionpopup.html',
                controller: ModalInstanceCtrl,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.customer_urn = customer_urn;
                $scope.customername = customername;
                $scope.customer_status = allocation_status;

                var params = {
                    customer_urn: customer_urn
                }
                var url = "api/zonalAllocation/GetExclusionCustomerHistory";
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.exclusioncustomerHistory = resp.data.exclusionhistory;
                    if (resp.data.exclusionhistory == null) {
                        $scope.Nohistoryexclusion = true;
                    }
                    else {
                        $scope.historyexclusion = true;
                    }
                });
                $scope.confirmExclusioncustomer = function () {
                    var params = {
                        customer_urn: customer_urn,
                        allocationdtl_gid: allocationdtl_gid,
                        exclusion_reason: $scope.txtexclusion_reason
                    }
                    var url = "api/ExclusionList/GetExclusionAllocation";
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                            $scope.current = $state.current.name;
                            ScopeValueService.store("dataldCtrl", $scope);
                            $state.go('app.pageredirect');
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'info',
                                pos: 'top-center',
                                timeout: 3000
                            })
                            $modalInstance.close('closed');
                        }
                    });
                }
            }
        }

        $scope.observationviewreport = function (allocationdtl_gid, observation_reportgid) {
            // localStorage.setItem('allocationdtl_gid', allocationdtl_gid);
            // localStorage.setItem('observation_reportgid', observation_reportgid);
            // $state.go('app.rmObservationReportView');

            $location.url('app/rmObservationReportView?allocationdtl_gid='+allocationdtl_gid+'&?&observation_reportgid='+observation_reportgid);
        }

        $scope.reportCanceldtl = function (allocationdtl_gid, customer_gid, customername) {

            var modalInstance = $modal.open({
                templateUrl: '/reportCancelModal.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.customername = customername;
                $scope.proceedReportCancel = function () {
                    lockUI();

                    var params = {
                        cancel_reason: $scope.txtcancel_reason,
                        allocationdtl_gid: allocationdtl_gid,
                        customer_gid: customer_gid
                    }
                    var url = "api/VisitReportCancel/PostCancelReport";
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                            $modalInstance.close('closed');
                            // localStorage.setItem('allocationdtl_gid', allocationdtl_gid);
                            // localStorage.setItem('allocation_customer_gid', customer_gid);

                           // $state.go('app.visitReportCancel');
                            $location.url('app/visitReportCancel?allocationdtl_gid='+allocationdtl_gid+'&allocation_customer_gid='+customer_gid);
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

        $scope.visitStatus = function (allocationdtl_gid, customername, customer_urn, visit_status) {

            var modalInstance = $modal.open({
                templateUrl: '/visitStatusModal.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.customername = customername;
                $scope.customer_urn = customer_urn;
                $scope.txtVisitStatus = visit_status;
                $scope.VisitStatusUpdate = function () {
                    lockUI();

                    var params = {
                        visit_status: $scope.txtVisitStatus,
                        allocationdtl_gid: allocationdtl_gid,
                    }

                    var url = "api/VisitReportCancel/PostVisitStatus";
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                            $modalInstance.close('closed');
                            $scope.current = $state.current.name;
                            ScopeValueService.store("dataldCtrl", $scope);
                            $state.go('app.pageredirect');

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

        $scope.viewallocationDtl = function (allocationdtl_gid, customer_gid, allocation_status) {

            if (allocation_status == 'Allocated') {
               var lscompleted_flag= 'N';
            }
            else {
                var lscompleted_flag= 'Y';
               
            }
            var url = "api/allocationManagement/tmpAllocatedocumentclear";
            SocketService.get(url).then(function (resp) {
            });
            // localStorage.setItem('allocationdtl_gid', allocationdtl_gid);
            // localStorage.setItem('allocation_customer_gid', customer_gid);
            // localStorage.setItem('MyAllocation', 'Y');
            var url='app/allocationView?allocationdtl_gid='+allocationdtl_gid+'&?&allocation_customer_gid='+customer_gid+'&?&MyAllocation=Y&?&completed_flag='+lscompleted_flag;
            $location.url(url);
            console.log('url',url);
            //$state.go('app.allocationView');
        }

        $scope.genereteallocation = function (allocationdtl_gid, customer_gid) {
            lockUI();
            var params = {
                allocationdtl_gid: allocationdtl_gid
            }
            var url = "api/visitReport/GetScheduleInfo";
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.info_flag == "Y") {
                    // localStorage.setItem('allocationdtl_gid', allocationdtl_gid);
                    // localStorage.setItem('allocation_customer_gid', customer_gid);
                    // $state.go('app.visitReportGenerate');
                    $location.url('app/visitReportGenerate?allocationdtl_gid='+allocationdtl_gid+'&allocation_customer_gid='+customer_gid);
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                unlockUI();
            });

          
        }

        $scope.Viewgenereteddtl = function (allocationdtl_gid) {
            // localStorage.setItem('allocationdtl_gid', allocationdtl_gid);
            // localStorage.setItem('allocation_customer_gid', customer_gid);
            // $state.go('app.visitReportdetailView');
            $location.url('app/visitReportdetailView?allocationdtl_gid='+allocationdtl_gid);
        }

        $scope.generetePDF = function (allocationdtl_gid) {

            lockUI();
            var params = {
                allocationdtl_gid: allocationdtl_gid
            };
            var url = 'api/zonalAllocation/visitReportpdfcontent';
            SocketService.getparams(url, params).then(function (resp) {       
                if (resp.data.status == true) {
                    var filepath = resp.data.file_path;
                    var filename = resp.data.file_name;
                    DownloaddocumentService.Downloaddocument(filepath, filename);
            }
            else {
                unlockUI();
                Notify.alert('Error Occurred While Export PDF !', 'warning');
                }
                unlockUI();
            });
         
            
        }
        $scope.genereteATRPDF = function (observation_reportgid) {


            var params = {
                observation_reportgid: observation_reportgid

            };
            var url = 'api/ObservationReport/ATRReportpdfcontent';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    var filename = resp.data.file_name;
                    var filepath = resp.data.file_path;
                    DownloaddocumentService.Downloaddocument(filepath, filename);
                }
                else {
                    unlockUI();
                    Notify.alert('Error Occurred While Export PDF !', 'warning');
                }
          
            });

        }
      
    }
})();

               