(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrTrnRMDeferralDtlsController', AgrTrnRMDeferralDtlsController);

        AgrTrnRMDeferralDtlsController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function AgrTrnRMDeferralDtlsController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrTrnRMDeferralDtlsController';
        var application_gid = $location.search().application_gid;
        activate();

        function activate() {
            lockUI();
            var url = 'api/AgrMstScannedDocument/tmpclearRMuploaded';
            SocketService.get(url).then(function (resp) {  
            });

            var params = {
                application_gid: application_gid,
            }

            var url = 'api/AgrMstApplicationView/GetApplicationBasicView';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtapplication_no = resp.data.application_no;
                $scope.txtbasiccustomer_name = resp.data.customer_name;
                $scope.txtcustomer_urn = resp.data.customer_urn;
                $scope.txtvertical = resp.data.vertical_name;
                $scope.txtvertical_tag = resp.data.verticaltaggs_name;
                $scope.txtconstitution = resp.data.constitution_name;
                $scope.txt_strategicbusiness_unit = resp.data.businessunit_name;
                $scope.txtprimayvalue_chain = resp.data.primaryvaluechain_name;
                $scope.txtsecondaryvalue_chain = resp.data.secondaryvaluechain_name;
                $scope.txtvernacular_language = resp.data.vernacular_language;
                $scope.txtApplfrom_SA = resp.data.sa_status;
                $scope.txtSAM_associateID = resp.data.sa_id;
                $scope.txtSAM_associatename = resp.data.sa_name;
                $scope.txtcontactperson_name = resp.data.contactperson_name;
                $scope.txtbasicdesignation = resp.data.designation_type;
                $scope.txtlandline_number = resp.data.landline_no;
                $scope.txtsocial_capital = resp.data.social_capital;
                $scope.txttrade_capital = resp.data.trade_capital;
                $scope.borrower_flag = resp.data.borrower_flag;
                $scope.borrower_type = resp.data.borrower_type;
                $scope.momapproval_flag = resp.data.momapproval_flag;
                $scope.txtcredit_group = resp.data.creditgroup_name;
                $scope.docchecklist_makerflag = resp.data.docchecklist_makerflag;
                $scope.docchecklist_checkerflag = resp.data.docchecklist_checkerflag;
                $scope.docchecklist_approvalflag = resp.data.docchecklist_approvalflag;
                if ($scope.docchecklist_approvalflag == 'N') {
                    $scope.approval = true;
                }
            });

            var url = 'api/AgrMstApplicationView/GetIndividualList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.CreditIndividual_List = resp.data.individual_List;
            });

            var url = 'api/AgrMstApplicationView/GetInstitutionList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.CreditInstitution_List = resp.data.institution_List;
            });

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
            $location.url('app/AgrTrnPostCcActivitiesRMView?application_gid=' + application_gid);
        }

        $scope.institution_view = function (institution_gid, company_name, stakeholder_type) {
            $location.url('app/AgrTrnRMDeferralDtlsView?application_gid=' + application_gid + '&credit_gid=' + institution_gid + '&lspage=RMDocChecklist&lstype=Institution' + '&lscompany_name=' + company_name + '&lscompanystakeholder=' + stakeholder_type);
        }

        $scope.individual_view = function (contact_gid, individual_name, stakeholder_type) {
            $location.url('app/AgrTrnRMDeferralDtlsView?application_gid=' + application_gid + '&credit_gid=' + contact_gid + '&lspage=RMDocChecklist&lstype=Individual' + '&lsindividual_name=' + individual_name + '&lsindividualstakeholder=' + stakeholder_type);
        }
        

        $scope.group_view = function (group_gid) {
            $location.url('app/AgrTrnRMDeferralDtlsView?application_gid=' + application_gid + '&credit_gid=' + group_gid + '&lspage=RMDocChecklist&lstype=Group');
        }

        $scope.member_view = function (contact_gid) {
            $location.url('app/AgrTrnRMDeferralDtlsView?application_gid=' + application_gid + '&credit_gid=' + contact_gid + '&lspage=RMDocChecklist&lstype=Individual');
        }
    }
})();
