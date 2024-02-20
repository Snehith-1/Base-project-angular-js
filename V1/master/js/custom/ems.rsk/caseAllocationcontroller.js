(function () {
    'use strict';

    angular
        .module('angle')
        .controller('caseAllocation', caseAllocation);

    caseAllocation.$inject = ['$rootScope', '$scope', '$sce', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', '$cookies', '$filter', 'ngTableParams', '$timeout','DownloaddocumentService','cmnfunctionService'];

    function caseAllocation($rootScope, $scope, $sce, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, $cookies, $filter, ngTableParams, $timeout, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'caseAllocation';

        activate();

        function activate() {
            lockUI();
            var url = "api/allocationManagement/GetOverallZonalPendingCount";
            SocketService.get(url).then(function (resp) {
                $scope.count_qualified = resp.data.count_qualified;
                $scope.count_currentallo = resp.data.count_current;
                $scope.count_upcoming = resp.data.count_upcoming;
                $scope.count_completedallo = resp.data.count_completed;
                $scope.count_external = resp.data.count_external;
                $scope.count_breached = resp.data.count_breached;
                $scope.ADM_updatedby = resp.data.ADM_updatedby;
                $scope.ADM_updateddate = resp.data.ADM_updateddate;
                $scope.count_reportcancel = resp.data.count_reportchanges;
                $scope.count_unmatchedqualified = "500";

                var num1 = parseInt(resp.data.count_qualified);
                var num2 = parseInt($scope.count_unmatchedqualified);
                $scope.count_overall = num1 + num2;
                $scope.overallzonalcountdtl = resp.data.overallzonalcount;
                angular.forEach($scope.overallzonalcountdtl, function (value, key) {
                    var params = {
                        zonalmapping_gid: value.zonalmapping_gid
                    };

                    var url = 'api/allocationManagement/GetAllocationPendingCount';
                    SocketService.getparams(url, params).then(function (resp) {
                        value.zonalwisecountdtl = resp.data.zonalwisecount;

                        value.expand = false;
                    });
                });
                unlockUI();
            });
        }

        $scope.showPopover = function (assigned_RM) {

            var params = {
                assigned_RM: assigned_RM
            }

            var url = 'api/RskDashboard/GetRMAllocateCountdtl';
            SocketService.getparams(url, params).then(function (resp) {

                if (resp.data.status == true) {
                    $scope.customerstatusdtl = resp.data.customerstatusdtl;
                }
                else {
                    $scope.customerstatusdtl = 'No Record';
                    console.log('NO', $scope.customerstatusdtl);
                    $("[data-toggle=popover]").popover({
                        html: false,
                        content: function () {
                            return $('#popover-content').html();
                        }
                    });
                }
            });

        };

        $scope.Fresh = function () {
            lockUI();
            $scope.totalQualifiedDisplayed = 100;
            $scope.cboQualifiedStatus = "Fresh";
            var url = "api/allocationManagement/GetQualifiedFreshAllocation";
            SocketService.get(url).then(function (resp) {
                $scope.qualifiedallocationList = resp.data.qualifiedallocation;
                if ($scope.qualifiedallocationList == null) {
                    $scope.totalQualified = 0;
                    $scope.totalQualifiedDisplayed = 0;
                }
                else {
                    $scope.totalQualified = $scope.qualifiedallocationList.length;
                    if ($scope.qualifiedallocationList.length < 100) {
                        $scope.totalQualifiedDisplayed = $scope.qualifiedallocationList.length;
                    }
                }
                unlockUI();
            });
        }

        $scope.Revisit = function () {
            lockUI();
            $scope.totalQualifiedDisplayed = 100;
            var url = "api/allocationManagement/GetQualifiedReVisitAllocation";
            SocketService.get(url).then(function (resp) {
                $scope.qualifiedallocationList = resp.data.qualifiedallocation;
                if ($scope.qualifiedallocationList == null) {
                    $scope.totalQualified = 0;
                    $scope.totalQualifiedDisplayed = 0;
                }
                else {
                    $scope.totalQualified = $scope.qualifiedallocationList.length;
                    if ($scope.qualifiedallocationList.length < 100) {
                        $scope.totalQualifiedDisplayed = $scope.qualifiedallocationList.length;
                    }
                }
                unlockUI();
            });
        }

        $scope.all = function () {
            lockUI();
            $scope.totalQualifiedDisplayed = 100;
            var url = "api/allocationManagement/GetQualifiedAllocationSummary";
            SocketService.get(url).then(function (resp) {
                $scope.qualifiedallocationList = resp.data.qualifiedallocation;
                if ($scope.qualifiedallocationList == null) {
                    $scope.totalQualified = 0;
                    $scope.totalQualifiedDisplayed = 0;
                }
                else {
                    $scope.totalQualified = $scope.qualifiedallocationList.length;
                    if ($scope.qualifiedallocationList.length < 100) {
                        $scope.totalQualifiedDisplayed = $scope.qualifiedallocationList.length;
                    }
                }
                unlockUI();
            });
        }

        $scope.loadQualifiedMore = function (pageQualifiedcount) {

            if (pageQualifiedcount == undefined) {
                Notify.alert("Enter the Total Summary Count", "warning");
                return;
            }
            lockUI();
            var Number = parseInt(pageQualifiedcount);
            if ($scope.qualifiedallocationList != null) {
                if ($scope.totalQualifiedDisplayed < $scope.qualifiedallocationList.length) {
                    $scope.totalQualifiedDisplayed += Number;
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(" Total Summary " + $scope.qualifiedallocationList.length + " Records Only", "warning");
                    return;
                }
            }
        };

        $scope.allunmatch = function () {
            lockUI();
            $scope.cbounmatchStatus = undefined;
            unlockUI();
        }
        $scope.unmatchedqualified = function () {
            lockUI();
            $scope.totalUnmatchedDisplayed = 100;
            var url = "api/allocationManagement/GetQualifiedUnmatched";
            SocketService.get(url).then(function (resp) {
                $scope.count_unmatchedqualified = resp.data.count_unmatchedqualified;
                $scope.unmatchedallocationList = resp.data.qualifiedallocation;
                if ($scope.unmatchedallocationList == null) {
                    $scope.totalUnmatched = 0;
                    $scope.totalUnmatchedDisplayed = 0;
                }
                else {
                    $scope.totalUnmatched = $scope.unmatchedallocationList.length;
                    if ($scope.unmatchedallocationList.length < 100) {
                        $scope.totalUnmatchedDisplayed = $scope.unmatchedallocationList.length;
                    }
                }
                unlockUI();
            });
        }


        $scope.movetocurrentallocation = function (allocationdtl_gid, customer_urn, customername) {

            var modalInstance = $modal.open({
                templateUrl: '/movetocurrentallocationpopup.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.customer_urn = customer_urn;
                $scope.customername = customername;
                $scope.confirmmoveAllocation = function () {
                    var params = {
                        allocationdtl_gid: allocationdtl_gid,
                        allocationmove_reason: $scope.txtmove_reason,
                    }
                    var url = "api/allocationManagement/GetMovetoCurrentAllocation";
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


        $scope.loadUnmatchedMore = function (pageUnmatchedcount) {

            if (pageUnmatchedcount == undefined) {
                Notify.alert("Enter the Total Summary Count", "warning");
                return;
            }
            lockUI();
            var Number = parseInt(pageUnmatchedcount);
            if ($scope.totalUnmatchedDisplayed < $scope.unmatchedallocationList.length) {
                $scope.totalUnmatchedDisplayed += Number;
                unlockUI();
            }
            else {
                unlockUI();
                Notify.alert(" Total Summary " + $scope.unmatchedallocationList.length + " Records Only", "warning");
                return;
            }
        };

        $scope.qualified = function () {

            lockUI();
            $scope.totalQualifiedDisplayed = 100;
            var url = "api/allocationManagement/GetQualifiedFreshAllocation";
            SocketService.get(url).then(function (resp) {
                $scope.qualifiedallocationList = resp.data.qualifiedallocation;
                if ($scope.qualifiedallocationList == null) {
                    $scope.totalQualified = 0;
                    $scope.totalQualifiedDisplayed = 0;
                }
                else {
                    $scope.totalQualified = $scope.qualifiedallocationList.length;
                    if ($scope.qualifiedallocationList.length < 100) {
                        $scope.totalQualifiedDisplayed = $scope.qualifiedallocationList.length;
                    }
                }
                unlockUI();
            });
        }

        $scope.current = function () {
            lockUI();
            $scope.totalCurrentDisplayed = 100;
            var url = "api/allocationManagement/GetCurrentAllocateSummary";
            SocketService.get(url).then(function (resp) {
                $scope.count_currentallo = resp.data.count_currentallo;
                $scope.currentallocationList = resp.data.allocationdtl;
                if ($scope.currentallocationList == null) {
                    $scope.totalCurrent = 0;
                    $scope.totalCurrentDisplayed = 0;
                }
                else {
                    $scope.totalCurrent = $scope.currentallocationList.length;
                    if ($scope.currentallocationList.length < 100) {
                        $scope.totalCurrentDisplayed = $scope.currentallocationList.length;
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
            if ($scope.totalCurrentDisplayed < $scope.currentallocationList.length) {
                $scope.totalCurrentDisplayed += Number;
                unlockUI();
            }
            else {
                unlockUI();
                Notify.alert(" Total Summary " + $scope.currentallocationList.length + " Records Only", "warning");
                return;
            }
        };

        $scope.upcoming = function () {
            lockUI();
            $scope.totalUpcomingDisplayed = 100;
            var url = "api/allocationManagement/GetUpcomingAllocation";
            SocketService.get(url).then(function (resp) {
                $scope.count_upcoming = resp.data.count_upcoming;
                $scope.upcomingallocationList = resp.data.allocationdtl;
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

        $scope.breached = function () {
            lockUI();
            $scope.totalBreachedDisplayed = 100;
            var url = "api/allocationManagement/GetBreachedAllocationSummary";
            SocketService.get(url).then(function (resp) {
                $scope.count_breached = resp.data.count_breached;
                $scope.breachedallocationList = resp.data.breacheddtl;
                if ($scope.breachedallocationList == null) {
                    $scope.totalBreached = 0;
                    $scope.totalBreachedDisplayed = 0;
                }
                else {
                    $scope.totalBreached = $scope.breachedallocationList.length;
                    if ($scope.breachedallocationList.length < 100) {
                        $scope.totalBreachedDisplayed = $scope.breachedallocationList.length;
                    }
                }
                unlockUI();
            });

        }

        $scope.loadBreachedMore = function (pageBreachedcount) {

            if (pageBreachedcount == undefined) {
                Notify.alert("Enter the Total Summary Count", "warning");
                return;
            }
            lockUI();
            var Number = parseInt(pageBreachedcount);
            if ($scope.breachedallocationList != null) {
                if ($scope.totalBreachedDisplayed < $scope.breachedallocationList.length) {
                    $scope.totalBreachedDisplayed += Number;
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(" Total Summary " + $scope.breachedallocationList.length + " Records Only", "warning");
                    return;
                }
            }
            else {

            }
            unlockUI();
        };

        $scope.completed = function () {
            lockUI();
            $scope.totalCompletedDisplayed = 100;
            var url = "api/allocationManagement/getcompletedAllocationSummary";
            SocketService.get(url).then(function (resp) {
                $scope.count_completedallo = resp.data.count_completedallo;
                $scope.completedallocationList = resp.data.allocationdtl;
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

        $scope.loadCompletedMore = function (pageCompletedcount) {

            if (pageCompletedcount == undefined) {
                Notify.alert("Enter the Total Summary Count", "warning");
                return;
            }
            lockUI();
            var Number = parseInt(pageCompletedcount);
            if ($scope.totalCompletedDisplayed < $scope.completedallocationList.length) {
                $scope.totalCompletedDisplayed += Number;
                unlockUI();
            }
            else {
                unlockUI();
                Notify.alert(" Total Summary " + $scope.completedallocationList.length + " Records Only", "warning");
                return;
            }
        };

        $scope.external = function () {
            lockUI();
            $scope.totalExternalDisplayed = 100;
            var url = "api/allocationManagement/getExternalAllocationSummary";
            SocketService.get(url).then(function (resp) {
                $scope.count_external = resp.data.count_external;
                $scope.externalallocationList = resp.data.allocationdtl;
                if ($scope.externalallocationList == null) {
                    $scope.totalExternal = 0;
                    $scope.totalExternalDisplayed = 0;
                }
                else {
                    $scope.totalExternal = $scope.externalallocationList.length;
                    if ($scope.externalallocationList.length < 100) {
                        $scope.totalExternalDisplayed = $scope.externalallocationList.length;
                    }
                }
                unlockUI();
            });
        }

        $scope.loadExternalMore = function (pageExternalcount) {

            if (pageExternalcount == undefined) {
                Notify.alert("Enter the Total Summary Count", "warning");
                return;
            }
            lockUI();
            var Number = parseInt(pageExternalcount);
            if ($scope.totalExternalDisplayed < $scope.externalallocationList.length) {
                $scope.totalExternalDisplayed += Number;
                unlockUI();
            }
            else {
                unlockUI();
                Notify.alert(" Total Summary " + $scope.externalallocationList.length + " Records Only", "warning");
                return;
            }
        };

        $scope.reportcancel = function () {
            lockUI();
            $scope.totalReportDisplayed = 100;
            var url = "api/allocationManagement/GetCaseAllocaCancelChanges";
            SocketService.get(url).then(function (resp) {
                $scope.count_reportcancel = resp.data.count_reportcancel;
                $scope.reportcancelList = resp.data.allocationdtl;
                if ($scope.reportcancelList == null) {
                    $scope.totalReport = 0;
                    $scope.totalReportDisplayed = 0;
                }
                else {
                    $scope.totalReport = $scope.reportcancelList.length;
                    if ($scope.reportcancelList.length < 100) {
                        $scope.totalReportDisplayed = $scope.reportcancelList.length;
                    }
                }
                unlockUI();
            });
        }

        $scope.loadReportMore = function (pageReportcount) {

            if (pageReportcount == undefined) {
                Notify.alert("Enter the Total Summary Count", "warning");
                return;
            }
            lockUI();
            var Number = parseInt(pageReportcount);
            if ($scope.totalReportDisplayed < $scope.reportcancelList.length) {
                $scope.totalReportDisplayed += Number;
                unlockUI();
            }
            else {
                unlockUI();
                Notify.alert(" Total Summary " + $scope.reportcancelList.length + " Records Only", "warning");
                return;
            }
        };

        $scope.exclusioncustomer = function (customer_urn, customername, qualified_status) {

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
                $scope.customer_status = qualified_status;

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
                        exclusion_reason: $scope.txtexclusion_reason
                    }
                    var url = "api/zonalAllocation/GetExclusionCustomer";
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

        $scope.lasvisitdate = function (customer_gid, customername, customer_urn) {
            var modalInstance = $modal.open({
                templateUrl: '/lastVisitDate.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.customername = customername;
                $scope.customer_urn = customer_urn;

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.close = function () {
                    $modalInstance.close('closed');
                }

                $scope.submitlastvisit = function () {
                    lockUI();

                    var visitdate = new Date();
                    visitdate.setFullYear($scope.visitdate.getFullYear());
                    visitdate.setMonth($scope.visitdate.getMonth());
                    visitdate.setDate($scope.visitdate.getDate());

                    var params = {
                        customer_gid: customer_gid,
                        lastvisit_date: visitdate
                    }

                    var url = "api/allocationManagement/postlastVisitDate";
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $scope.visitdate = "";
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                            $modalInstance.close('closed');
                            activate();
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

        $scope.transferRM = function (allocationdtl_gid) {
            var params = {
                allocationdtl_gid: allocationdtl_gid
            }

            var modalInstance = $modal.open({
                templateUrl: '/RMTransfer.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                lockUI();
                var url = 'api/newServiceTicket/employee';
                SocketService.get(url).then(function (resp) {
                    $scope.employee_list = resp.data.employee_list;
                });

                var url = "api/allocationManagement/gettransferDetails";
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.transferFrom = resp.data.transferred_from;
                    $scope.customerdtl = resp.data.customerdtl;
                });

                unlockUI();

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.submittransfer = function () {
                    lockUI();
                    var transferTo = {
                        allocation_Gid: allocationdtl_gid,
                        transferred_to: $scope.employee_id
                    }
                    var url = "api/allocationManagement/postAllocationTransfer";
                    SocketService.post(url, transferTo).then(function (resp) {
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                            $modalInstance.close('closed');
                            activate();

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


        $scope.exteralAllocate = function (allocationdtl_gid) {
            var params = {
                allocationdtl_gid: allocationdtl_gid
            }

            var modalInstance = $modal.open({
                templateUrl: '/AllocateExternal.html',
                controller: ModalInstanceCtrl,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                lockUI();
                var url = "api/allocationManagement/tmpExternaldocumentclear"
                SocketService.get(url).then(function (resp) {

                });
                var url = 'api/allocationManagement/getExternalNamelist';
                SocketService.get(url).then(function (resp) {
                    $scope.externaldtl = resp.data.externaldtl;
                });

                var url = "api/allocationManagement/gettransferDetails";
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.customerdtl = resp.data.customerdtl;
                });

                unlockUI();

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.close = function () {
                    $modalInstance.close('closed');
                }

                $scope.uploadallocation = function (val, val1, name) {
                    var frm = new FormData();

                    for (var i = 0; i < val.length; i++) {
                        var item = {
                            name: val[i].name,
                            file: val[i]
                        };
                        frm.append('fileupload', item.file);
                        frm.append('file_name', item.name);
                        var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[i].name, "documentformatonly");
        
                                if (IsValidExtension == false) {
                                    Notify.alert("File format is not supported..!", {
                                        status: 'danger',
                                        pos: 'top-center',
                                        timeout: 3000
                                    });
                                    return false;
                                }
                    }
                    // var item = {
                    //     name: val[0].name,
                    //     file: val[0]
                    // };
                    // var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "documentformatonly");

                    // if (IsValidExtension == false) {
                    //     Notify.alert("File format is not supported..!", {
                    //         status: 'danger',
                    //         pos: 'top-center',
                    //         timeout: 3000
                    //     });
                    //     return false;
                    // }
                    // frm.append('fileupload', item.file);
                    // frm.append('file_name', item.name);
                    frm.append('document_name', $scope.documentname);
                    frm.append('project_flag', "documentformatonly");
                    $scope.uploadfrm = frm;

                    var url = 'api/allocationManagement/ExternalUpload';

                    SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                        console.log(resp);
                        $scope.uploadallocation_list = resp.data.upload_list;
                        $("#addExternalupload").val('');

                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }
                        else {
                            Notify.alert('File Format Not Supported!', {
                                status: 'info',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }
                    });

                }

                $scope.uploadcancel = function (tmp_documentGid) {
                    var allocationupload = {
                        tmp_documentGid: tmp_documentGid
                    }
                    var url = 'api/allocationManagement/ExternalUploadcancel';
                    SocketService.getparams(url, allocationupload).then(function (resp) {
                        $scope.uploadallocation_list = resp.data.upload_list;
                    });
                }

                $scope.submitAllocateExternal = function () {
                    lockUI();

                    var targetdate = new Date();
                    targetdate.setFullYear($scope.targetdate.getFullYear());
                    targetdate.setMonth($scope.targetdate.getMonth());
                    targetdate.setDate($scope.targetdate.getDate());

                    var transferTo = {
                        allocationdtl_gid: allocationdtl_gid,
                        external_usergid: $scope.external_usergid,
                        external_allocateRemarks: $scope.AllocationExtRemarks,
                        target_date: targetdate,
                    }

                    var url = "api/allocationManagement/postAllocationExternal";
                    SocketService.post(url, transferTo).then(function (resp) {
                        if (resp.data.status == true) {
                            $scope.AllocationExtRemarks = "";
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

        $scope.externalAssigned = function (allocationdtl_gid) {
            var params = {
                allocationdtl_gid: allocationdtl_gid
            }

            var modalInstance = $modal.open({
                templateUrl: '/AssignedExternal.html',
                controller: ModalInstanceCtrl,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                lockUI();
                var url = "api/allocationManagement/getExternalDetails";
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.customerdtl = resp.data.customerdtl;
                    $scope.externalname = resp.data.externalname;
                    $scope.AllocateExtRemarks = resp.data.requested_remarks;
                    $scope.assigned_by = resp.data.assigned_by;
                    $scope.assigned_date = resp.data.assigned_date;
                    $scope.target_date = resp.data.target_date;
                });

                var url = "api/allocationManagement/getExternaldocument";
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.upload_list = resp.data.upload_list;
                });
                unlockUI();

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.close = function () {
                    $modalInstance.close('closed');
                }

                $scope.downloads = function (val1, val2) {
                    DownloaddocumentService.Downloaddocument(val1, val2);
                }
            }
        }

        $scope.createAllocation = function (customer_urn, qualified_status) {
            console.log(qualified_status)
            var params = {
                customer_urn: customer_urn
            }
            lockUI();
            var url = "api/allocationManagement/GetCustomerGid";
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    //var url = "api/allocationManagement/tmpAllocatedocumentclear";
                    //SocketService.get(url).then(function (resp) {
                    //});
                    localStorage.setItem('allocation_customer_gid', resp.data.customer_gid);
                    localStorage.setItem('qualified_status', qualified_status)
                    $state.go('app.allocationCreate');
                    unlockUI();
                }
                else {
                    unlockUI();
                    var modalInstance = $modal.open({
                        templateUrl: '/warningpopup.html',
                        controller: ModalInstanceCtrl,
                        size: 'md'
                    });
                    ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
                    function ModalInstanceCtrl($scope, $modalInstance) {
                        $scope.ok = function () {
                            $modalInstance.close('closed');
                        };
                    }
                }
            });


        }


        $scope.holdAllocation = function (allocationdtl_gid, customer_urn, customername) {

            var modalInstance = $modal.open({
                templateUrl: '/holdAllocationpopup.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.customer_urn = customer_urn;
                $scope.customername = customername;

                $scope.confirmHoldAllocation = function () {
                    var params = {
                        allocationdtl_gid: allocationdtl_gid,
                        allocationhold_reason: $scope.txthold_reason,
                    }
                    var url = "api/allocationManagement/GetHoldAllocation";
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



        $scope.createDirectAllocation = function () {
            var url = "api/allocationManagement/tmpAllocatedocumentclear";
            SocketService.get(url).then(function (resp) {
            });
            $state.go('app.allocationCreateDirect');
        }

        $scope.viewallocation = function (allocationdtl_gid, customer_gid) {
            localStorage.setItem('allocationdtl_gid', allocationdtl_gid);
            localStorage.setItem('allocation_customer_gid', customer_gid);
            localStorage.setItem('MyAllocation', 'N');
            localStorage.setItem('ZonalAllocationBack', 'N');
            localStorage.setItem('Allocated', 'N');
            $state.go('app.allocation360');
        }

        $scope.viewallocateddetails = function (allocationdtl_gid, customer_gid) {
            localStorage.setItem('allocationdtl_gid', allocationdtl_gid);
            localStorage.setItem('allocation_customer_gid', customer_gid);
            localStorage.setItem('MyAllocation', 'N');
            localStorage.setItem('Allocated', 'Y');
            localStorage.setItem('ZonalAllocationBack', 'N');
            $state.go('app.allocation360');
        }

        $scope.historyallocation = function (customer_gid) {
            localStorage.setItem('MyZonalAllocationHistory', 'N');
            localStorage.setItem('allocation_customer_gid', customer_gid);
            $state.go('app.allocationHistorydetails');
        }
    }
})();
