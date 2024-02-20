(function () {
    'use strict';

    angular
        .module('angle')
        .controller('myApprovalController', myApprovalController);

    myApprovalController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'cmnfunctionService'];

    function myApprovalController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'myApprovalController';

        activate();

        function activate() {
            //var url = 'api/taskManagement/taskapprovallist';
            //SocketService.get(url).then(function (resp) {
            //    $scope.taskapproval = resp.data.taskapproval_list;
            //});
            var url = 'api/myApprovals/myapproval';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.departmentapproval = resp.data.departmentapproval_list;
                $scope.serviceapproval = resp.data.serviceapproval_list;
                $scope.managementapproval = resp.data.managerapproval_list;
                $scope.historyapproval = resp.data.approvalhistory_list;
                $scope.dependencyapproval = resp.data.dependencyapproval_list;
                $scope.cacapproval = resp.data.cabapproval_list;
                $scope.dependencyhistory = resp.data.dependencyhistory_list;
                $scope.cachistory = resp.data.cachistory_list;
                $scope.approvalcount = resp.data.myapprovalcount;
            });

            var url = 'api/OsdTrnRequestApproval/GetApprovalSummary';
            SocketService.get(url).then(function (resp) {
                $scope.approvalsummarylist = resp.data.approvalsummarylist;
                $scope.approvalcompletedlist = resp.data.approvalcompletedlist;
            });

            var url = 'api/OsdTrnRequestApproval/GetRHApprovalSummary';
            SocketService.get(url).then(function (resp) {
                $scope.rhapprovalsummarylist = resp.data.rhapprovalsummarylist;
                $scope.rhapprovalcompletedlist = resp.data.rhapprovalcompletedlist;               
            });

            var url = 'api/MstScannedDocument/GetDeferralApprovalSummary';
            SocketService.get(url).then(function (resp) {
                $scope.deferralapprovallist = resp.data.mdldeferralapproval;
            });
            var url = 'api/MstScannedDocument/GetDeferralApprovalHistorySummary';
            SocketService.get(url).then(function (resp) {
                $scope.deferralcompletedapprovallist = resp.data.mdldeferralapproval;
            });
            var url = 'api/MstRMPostCCWaiver/GetWaiverApprovalSummary';
            SocketService.get(url).then(function (resp) {
                $scope.waiverapproval_list = resp.data.rmpostccwaiver_list;               
            });

            var url = 'api/MstRMPostCCWaiver/GetWaiverApprovalHistorySummary';
            SocketService.get(url).then(function (resp) {
                $scope.waiverapprovalhistory_list = resp.data.rmpostccwaiver_list;               
            });
            
            var url = 'api/AgrMstScannedDocument/GetDeferralApprovalSummary';
            SocketService.get(url).then(function (resp) {
                $scope.samagrodeferralapprovallist = resp.data.mdldeferralapproval;
            });
            var url = 'api/AgrMstScannedDocument/GetDeferralApprovalHistorySummary';
            SocketService.get(url).then(function (resp) {
                $scope.samagrodeferralcompletedapprovallist = resp.data.mdldeferralapproval;
            });

            var url = 'api/AgrMstSuprScannedDocument/GetDeferralApprovalSummary';
            SocketService.get(url).then(function (resp) {
                $scope.samagrosupplierdeferralapprovallist = resp.data.mdldeferralapproval;
            });
            var url = 'api/AgrMstSuprScannedDocument/GetDeferralApprovalHistorySummary';
            SocketService.get(url).then(function (resp) {
                $scope.samagrosupplierdeferralcompletedapprovallist = resp.data.mdldeferralapproval;
            });

            var url = 'api/MstSAOnboardingInstitution/GetSaApprovalPendingSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.saOnboardSummary_list = resp.data.saOnboardSummary_list;
                unlockUI();
            });
            var url = 'api/MstSAOnboardingInstitution/GetSaApprovalInitiatedSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.saOnboardSummary1_list = resp.data.saOnboardSummary_list;
                unlockUI();
            });
            var url = 'api/MstSAOnboardingIndividual/GetApprovalPendingSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.saOnboardSummary2_list = resp.data.saOnboardSummary_list;
                unlockUI();
            });
            var url = 'api/MstSAOnboardingIndividual/GetSaApprovalInitiateSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.saOnboardSummary3_list = resp.data.saOnboardSummary_list;
                unlockUI();
            });
        }

        // Task Approve & Reject ...//

        $scope.taskapprove_click = function (trntask_gid, trntask2activity_gid) {
            var params = {
                trntask_gid: trntask_gid,
                trntask2activity_gid: trntask2activity_gid
            }
            var url = apiManage.apiList["taskapprove"].api;
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status = true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                activate();
            });
        }

        $scope.showPopover = function (release_gid, approval_remarks) {
            var modalInstance = $modal.open({
                templateUrl: '/showpopupModal.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    release_gid: release_gid
                }
                var url = 'api/myApprovals/ApprovalRemarksView';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.approval_remarks = resp.data.approval_remarks;
                    $scope.application = resp.data.application;


                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        $scope.taskreject_click = function (trntask_gid, trntask2activity_gid) {
            var params = {
                trntask_gid: trntask_gid,
                trntask2activity_gid: trntask2activity_gid
            }
            var url = apiManage.apiList["taskreject"].api;
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status = true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                activate();
            });
        }

        // View Department Details Popup....//

        $scope.btn_viewdepartmentclick = function (serviceapproval_gid, lsinternalapproval) {

            $scope.serviceapproval_gid = localStorage.setItem('serviceapproval_gid', serviceapproval_gid);
            $scope.lsinternalapproval = localStorage.setItem('lsinternalapproval', lsinternalapproval);
            $state.go('app.departmentApprovalView');

        }

        // View Service Details Popup....//

        $scope.btn_viewserviceclick = function (serviceapproval_gid, lsinternalapproval) {

            $scope.serviceapproval_gid = localStorage.setItem('serviceapproval_gid', serviceapproval_gid);
            $scope.lsinternalapproval = localStorage.setItem('lsinternalapproval', lsinternalapproval);
            $state.go('app.serviceApprovalView');

        }

        // View Management Details Popup....//

        $scope.btn_viewmanagementclick = function (serviceapproval_gid) {
            $scope.serviceapproval_gid = localStorage.setItem('serviceapproval_gid', serviceapproval_gid);
            $state.go('app.managementApprovalView');
        }

        // View History Details Popup....//

        $scope.btn_viewhistoryclick = function (serviceapproval_gid) {
            $scope.serviceapproval_gid = localStorage.setItem('serviceapproval_gid', serviceapproval_gid);
            $state.go('app.historyApprovalView');
        }

        // View Dependency Details Popup..//

        $scope.btn_viewdependencyclick = function (release_gid) {
            $scope.serviceapproval_gid = localStorage.setItem('release_gid', release_gid);
            $state.go('app.dependencyApprovalView');
        }

        // View CAB Details Popup....//

        $scope.btn_viewcabclick = function (release_gid) {
            $scope.release_gid = localStorage.setItem('release_gid', release_gid);
            $state.go('app.cacApproval');
        }


        $scope.btn_approvalclick = function (val) {
            $scope.requestapproval_gid = val;
            $scope.requestapproval_gid = localStorage.setItem('requestapproval_gid', val);
            $state.go('app.osdTrnApprovalView');
        }
        
        $scope.btn_approvalhistory = function (val) {
            $scope.requestapproval_gid = val;
            $scope.requestapproval_gid = localStorage.setItem('requestapproval_gid', val);
            $state.go('app.osdTrnApprovalViewHistory');
        }

        $scope.btn_rhapprovalclick = function (val,val1,val2,val3) {
            $scope.bankalertrefundapprl_gid = val;
            $scope.bankalertrefundapprl_gid = localStorage.setItem('bankalertrefundapprl_gid', val);
            $scope.bankalert2allocated_gid = val1;
            $scope.bankalert2allocated_gid = localStorage.setItem('bankalert2allocated_gid', val1);
            $scope.customername = val2;
            $scope.customername = localStorage.setItem('customername', val2);
            $scope.customerurn = val3;
            $scope.customerurn = localStorage.setItem('customerurn', val3);
            $state.go('app.osdTrnRHApprovalView');
        }
        $scope.btn_rhapprovalhistory = function (val,val1,val2,val3) {
            $scope.bankalertrefundapprl_gid = val;
            $scope.bankalertrefundapprl_gid = localStorage.setItem('bankalertrefundapprl_gid', val);
            $scope.bankalert2allocated_gid = val1;
            $scope.bankalert2allocated_gid = localStorage.setItem('bankalert2allocated_gid', val1);
            $scope.customername = val2;
            $scope.customername = localStorage.setItem('customername', val2);
            $scope.customerurn = val3;
            $scope.customerurn = localStorage.setItem('customerurn', val3);
            $state.go('app.osdTrnRHApprovalViewHistory');
        }

        $scope.approvaldeferral_view = function (approval_initiationgid, documentcheckdtl_gid, initiateextendorwaiver_gid, application_gid,fromphysical_document) {
            $location.url('app/MstDeferralMyApproval?application_gid=' + application_gid + '&approval_initiationgid=' + approval_initiationgid + '&initiateextendorwaiver_gid=' + initiateextendorwaiver_gid + '&documentcheckdtl_gid=' + documentcheckdtl_gid+ '&fromphysical_document=' + fromphysical_document);
        }

        $scope.approvaldeferral_historyview = function (extendorwaiverapproval_gid, documentcheckdtl_gid, initiateextendorwaiver_gid, application_gid,fromphysical_document) {
            $location.url('app/MstDeferralMyApprovalHistory?application_gid=' + application_gid + '&extendorwaiverapproval_gid=' + extendorwaiverapproval_gid + '&initiateextendorwaiver_gid=' + initiateextendorwaiver_gid + '&documentcheckdtl_gid=' + documentcheckdtl_gid+ '&fromphysical_document=' + fromphysical_document);
        }

        $scope.approvalsamagrodeferral_view = function (approval_initiationgid, application_gid, initiateextendorwaiver_gid, documentcheckdtl_gid, fromphysical_document) {
            $location.url('app/AgrTrnBuyerDeferralMyApproval?application_gid=' + application_gid + '&approval_initiationgid=' + approval_initiationgid + '&initiateextendorwaiver_gid=' + initiateextendorwaiver_gid + '&documentcheckdtl_gid=' + documentcheckdtl_gid + '&fromphysical_document=' + fromphysical_document);
        }

        $scope.approvalsamagrodeferral_historyview = function (extendorwaiverapproval_gid, documentcheckdtl_gid, initiateextendorwaiver_gid, application_gid, fromphysical_document) {
            $location.url('app/AgrTrnDeferralMyApprovalHistory?application_gid=' + application_gid + '&extendorwaiverapproval_gid=' + extendorwaiverapproval_gid + '&initiateextendorwaiver_gid=' + initiateextendorwaiver_gid + '&documentcheckdtl_gid=' + documentcheckdtl_gid + '&fromphysical_document=' + fromphysical_document);
        }

        $scope.approvalsamagrosupplierdeferral_view = function (extendorwaiverapproval_gid, documentcheckdtl_gid, initiateextendorwaiver_gid, application_gid) {
            $location.url('app/AgrTrnSuprDeferralMyApproval?application_gid=' + application_gid + '&extendorwaiverapproval_gid=' + extendorwaiverapproval_gid + '&initiateextendorwaiver_gid=' + initiateextendorwaiver_gid + '&documentcheckdtl_gid=' + documentcheckdtl_gid);
        }

        $scope.approvalsamagrosupplierdeferral_historyview = function (extendorwaiverapproval_gid, documentcheckdtl_gid, initiateextendorwaiver_gid, application_gid) {
            $location.url('app/AgrTrnSuprDeferralMyApprovalHistory?application_gid=' + application_gid + '&extendorwaiverapproval_gid=' + extendorwaiverapproval_gid + '&initiateextendorwaiver_gid=' + initiateextendorwaiver_gid + '&documentcheckdtl_gid=' + documentcheckdtl_gid);
        }

        // View Task Details Popup....//

        $scope.btn_viewtaskdetailclick = function (trntask_gid) {
            var doc = document.getElementById('view_taskdetails');
            doc.style.display = 'block';
            var params = {
                trntask_gid: trntask_gid
            };
            lockUI();
            var url = 'api/taskManagement/viewtaskapproval_details';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.taskdetails = resp.data;
            });

        }


        // Click Event Hide & Show...//

        $scope.servicedesk = function () {
            $scope.service_desk = true;
            $scope.change_management = false;
            $scope.task_managment = false;
        }
        $scope.changemanagment = function () {
            $scope.service_desk = false;
            $scope.change_management = true;
            $scope.task_managment = false;

        }
        $scope.taskmanagement = function () {
            $scope.service_desk = false;
            $scope.change_management = false;
            $scope.task_managment = true;
        }

        $scope.approval_view = function (rmpostccwaiver_gid, application_gid) {
            $location.url('app/MstRMWaiverApprovalView?rmpostccwaiver_gid=' + rmpostccwaiver_gid + '&application_gid=' + application_gid);
        }

        $scope.approvalhistory_view = function (rmpostccwaiver_gid, application_gid) {
            $location.url('app/MstRMWaiverApprovalHistoryView?rmpostccwaiver_gid=' + rmpostccwaiver_gid + '&application_gid=' + application_gid);
        }
        $scope.saonboardingverification = function (sacontactinstitution_gid) {

            // $location.url('app/MstSAOnboardingInstitutionVerification');

            $location.url('app/MstSBAInstitutionFinalApproval?hash=' + cmnfunctionService.encryptURL('lssacontactinstitution_gid=' + sacontactinstitution_gid + '&lspage=InstitutePending'));
        }
        $scope.saonboardingverificationcompleted = function (sacontactinstitution_gid) {

            // $location.url('app/MstSAOnboardingInstitutionVerification');

            $location.url('app/MstSBAInstitutionFinalApproval?hash=' + cmnfunctionService.encryptURL('lssacontactinstitution_gid=' + sacontactinstitution_gid + '&lspage=InstituteInitiate'));
        }
        $scope.saonboardingverificationpending = function (sacontact_gid) {
            $location.url('app/MstSBAIndividualFinalApproval?hash=' + cmnfunctionService.encryptURL('lssacontact_gid=' + sacontact_gid + '&lspage=IndividualPending'));
        }
        $scope.verificationindividualcompleted = function (sacontact_gid) {
            $location.url('app/MstSBAIndividualFinalApprovalView?hash=' + cmnfunctionService.encryptURL('lssacontact_gid=' + sacontact_gid + '&lspage=IndividualInitiate'));
        }
    }
})();