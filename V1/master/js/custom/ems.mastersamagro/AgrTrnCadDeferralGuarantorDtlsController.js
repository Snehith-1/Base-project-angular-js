﻿(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrTrnCadDeferralGuarantorDtlsController', AgrTrnCadDeferralGuarantorDtlsController);

    AgrTrnCadDeferralGuarantorDtlsController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'DownloaddocumentService'];

    function AgrTrnCadDeferralGuarantorDtlsController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrTrnCadDeferralGuarantorDtlsController';
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;
        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        var processtypeassign_gid = "";
        if (($location.search().lsprocesstypeassign_gid) != undefined)
            processtypeassign_gid = $location.search().lsprocesstypeassign_gid;
        else
            processtypeassign_gid = $location.search().processtypeassign_gid;
        var lstype = $location.search().lspath;
        activate();

        function activate() {
            lockUI();
            var params = {
                application_gid: application_gid,
            }

            var url = 'api/AgrMstApplicationView/GetApplicationBasicView';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtapplication_no = resp.data.application_no;
                $scope.txtbasiccustomer_name = resp.data.customer_name;
                $scope.txtcustomer_urn = resp.data.customer_urn;
            });

            var url = 'api/AgrMstScannedDocument/GetIndividualList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.CreditIndividual_List = resp.data.individual_List;
            });

            var url = 'api/AgrMstScannedDocument/GetScannedGeneralInfo';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.scannedgeneralinfo = resp.data;
            });

            var url = 'api/AgrMstScannedDocument/GetInstitutionList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.CreditInstitution_List = resp.data.institution_List;
            });
            $scope.pendingapproval = true;
            if (lstype == 'Maker') {
                $scope.btnname = 'Proceed To Checker';
            }
            else if (lstype == 'Checker') {
                $scope.btnname = 'Proceed To Approval';
            }
            else if (lstype == 'Approver') {
                $scope.btnname = 'Approve';
            }
            else {
                $scope.pendingapproval = false;;
            }
        }

        var params =
       {
           application_gid: application_gid
       }
        var url = "api/AgrMstApplicationEdit/GetGroupSummary";
        SocketService.getparams(url, params).then(function (resp) {
            $scope.group_list = resp.data.group_list;
            angular.forEach($scope.group_list, function (value, key) {
                var params = {
                    group_gid: value.group_gid
                };

                var url = 'api/AgrMstApplicationView/GetGrouptoMemberList';
                SocketService.getparams(url, params).then(function (resp) {
                    value.groupmember_list = resp.data.groupmember_list;
                    value.expand = false;
                });
            });
        });

        $scope.Back = function () {
            if (lspage == 'CadDeferralMaker') {
                $location.url('app/AgrMstCadDeferralSummary');
            }
            else if (lspage == 'CadDeferralChecker') {
                $location.url('app/AgrMstCadDeferralCheckerSummary');
            }
            else if (lspage == 'CadDeferralApproval') {
                $location.url('app/AgrMstCadDeferralApprovalSummary');
            }
            else {
                $location.url('app/AgrMstScannedCompletedSummary');
            }
        }

        //$scope.institution_add = function (institution_gid) {
        //    $location.url('app/AgrTrnCadDeferralDochecklist?application_gid=' + application_gid + '&credit_gid=' + institution_gid + '&lspage=' + lspage + '&processtypeassign_gid=' + processtypeassign_gid + '&lspath=' + lstype + '&lstype=Institution');
        //}

        //$scope.individual_add = function (contact_gid) {
        //    $location.url('app/AgrTrnCadDeferralDochecklist?application_gid=' + application_gid + '&credit_gid=' + contact_gid + '&lspage=' + lspage + '&processtypeassign_gid=' + processtypeassign_gid + '&lspath=' + lstype + '&lstype=Individual');
        //}

        //$scope.group_add = function (group_gid) {
        //    $location.url('app/AgrTrnCadDeferralDochecklist?application_gid=' + application_gid + '&credit_gid=' + group_gid + '&lspage=' + lspage + '&processtypeassign_gid=' + processtypeassign_gid + '&lspath=' + lstype + '&lstype=Group');
        //}

        //$scope.member_add = function (contact_gid) {
        //    $location.url('app/MstCadDeferralDochecklist?application_gid=' + application_gid + '&credit_gid=' + contact_gid + '&lspage=' + lspage + '&processtypeassign_gid=' + processtypeassign_gid + '&lspath=' + lstype + '&lstype=Individual');
        //}

        $scope.institution_add = function (institution_gid, company_name, stakeholder_type) {
            $location.url('app/AgrTrnPMGDeferralDochecklist?application_gid=' + application_gid + '&credit_gid=' + institution_gid + '&lspage=' + lspage + '&processtypeassign_gid=' + processtypeassign_gid + '&lspath=' + lstype + '&lstype=Institution' + '&lscompany_name=' + company_name + '&lscompanystakeholder=' + stakeholder_type);
        }

        $scope.individual_add = function (contact_gid, individual_name, stakeholder_type) {
            $location.url('app/AgrTrnPMGDeferralDochecklist?application_gid=' + application_gid + '&credit_gid=' + contact_gid + '&lspage=' + lspage + '&processtypeassign_gid=' + processtypeassign_gid + '&lspath=' + lstype + '&lstype=Individual' + '&lsindividual_name=' + individual_name + '&lsindividualstakeholder=' + stakeholder_type);
        }

        $scope.group_add = function (group_gid) {
            $location.url('app/AgrTrnPMGDeferralDochecklist?application_gid=' + application_gid + '&credit_gid=' + group_gid + '&lspage=' + lspage + '&processtypeassign_gid=' + processtypeassign_gid + '&lspath=' + lstype + '&lstype=Group');
        }

        $scope.member_add = function (contact_gid) {
            $location.url('app/AgrTrnPMGDeferralDochecklist?application_gid=' + application_gid + '&credit_gid=' + contact_gid + '&lspage=' + lspage + '&processtypeassign_gid=' + processtypeassign_gid + '&lspath=' + lstype + '&lstype=Individual');
        }

        $scope.proceedsubmit = function () {
            lockUI();
            var params = {
                lstype: lstype,
                processtypeassign_gid: processtypeassign_gid
            }

            var url = 'api/AgrMstScannedDocument/UpdateScannedApproval';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $scope.Back();
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
            });
        }
        $scope.export = function () {
            var params = {
                application_gid: application_gid,

            }
            var url = 'api/AgrMstScannedDocument/SoftcopyVettingDocExport';
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    DownloaddocumentService.Downloaddocument(resp.data.attachment_cloudpath, resp.data.attachment_name);
                    // var phyPath = resp.data.attachment_path;
                    // var relPath = phyPath.split("EMS");
                    // var relpath1 = relPath[1].replace("\\", "/");
                    // var hosts = window.location.host;
                    // var prefix = location.protocol + "//";
                    // var str = prefix.concat(hosts, relpath1);
                    // var link = document.createElement("a");
                    // var name = resp.data.attachment_name.split('.');
                    // link.download = name[0];
                    // var uri = str;
                    // link.href = uri;
                    // link.click();

                }
                else {
                    Notify.alert(resp.data.message, 'success')
                    activate();

                }

            });
        }
    }
})();
