(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstPSLCSAGuarantorDetailsController', MstPSLCSAGuarantorDetailsController);

        MstPSLCSAGuarantorDetailsController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function MstPSLCSAGuarantorDetailsController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstPSLCSAGuarantorDetailsController';
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;
        $scope.lspath = $location.search().lspath;
        var lspath = $scope.lspath;
        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        activate();

        function activate() {

            var params = {
                application_gid: application_gid,
            }

            var url = 'api/MstApplicationView/GetApplicationBasicView';
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

                if ($scope.docchecklist_makerflag == 'N' )
                {
                    $scope.approval = false;
                    $scope.checker = false;
                    $scope.maker = true;
                }
                else if (($scope.docchecklist_makerflag == 'Y') && ($scope.docchecklist_checkerflag == 'N') )
                {
                    $scope.approval = false;
                    $scope.checker = true;
                    $scope.maker = false;
                }
                else
                {
                    $scope.approval = true;
                    $scope.checker = false;
                    $scope.maker = false;

                }
            });

            var url = 'api/MstApplicationView/GetIndividualList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.CreditIndividual_List = resp.data.individual_List;
            });

            var url = 'api/MstApplicationView/GetInstitutionList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.CreditInstitution_List = resp.data.institution_List;
            });

            var url = 'api/MstCAD/GetPSLCompleteFlag';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.pslcsacomplete_flag = resp.data.pslcsacomplete_flag;
            });

        }

        var params =
       {
           application_gid: application_gid
       }
        var url = "api/MstApplicationEdit/GetGroupSummary";
        SocketService.getparams(url, params).then(function (resp) {
            $scope.group_list = resp.data.group_list;
            angular.forEach($scope.group_list, function (value, key) {
                var params = {
                    group_gid: value.group_gid
                };

                var url = 'api/MstApplicationView/GetGrouptoMemberList';
                SocketService.getparams(url, params).then(function (resp) {
                    value.groupmember_list = resp.data.groupmember_list;
                    value.expand = false;
                });
            });
        });

        $scope.Back = function () {
            if (lspage == 'PSLCSAGuarantor') {
                $location.url('app/MstPSLCSAManagement?application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else if (lspage == 'PSLCSAComplete') {
                $location.url('app/MstPSLCSAComplete');
            }
            else {

            }
        }

        $scope.institution_add = function (institution_gid) { 
            $location.url('app/MstPSLCSADataFlaggingAdd?application_gid=' + application_gid + '&credit_gid=' + institution_gid + '&lspage=' + lspage + '&lstype=Institution&lspath=' + lspath);
        }

        $scope.individual_add = function (contact_gid) {
            $location.url('app/MstPSLCSAIndividualPSLDataFlagAdd?application_gid=' + application_gid + '&credit_gid=' + contact_gid + '&lspage=' + lspage + '&lstype=Individual&lspath=' + lspath);
        }

        $scope.group_add = function (group_gid) {
            $location.url('app/MstPSLCSAGroupPSLDataFlagAdd?application_gid=' + application_gid + '&group_gid=' + group_gid + '&lspage=' + lspage + '&lstype=Group&lspath=' + lspath);
        }
        $scope.member_add = function (contact_gid) {
            $location.url('app/MstPSLCSAGroupPSLDataFlagAdd?application_gid=' + application_gid + '&credit_gid=' + contact_gid + '&lspage=' + lspage + '&lstype=Individual&lspath=' + lspath);
        }

        $scope.pslcsacompleted = function () {

            if ( ($scope.txtpslqueries == ''|| $scope.txtpslqueries == undefined)) {
                Notify.alert(' Enter Complete Remarks','warning');
            }
            else {
                var params = {
                    application_gid: application_gid,
                    pslcompleteremarks: $scope.txtpslqueries
                }
                
                var url = "api/MstCAD/UpdatePSLCompleted";
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status = true) {
                        Notify.alert(resp.data.message, 'success');
                        $location.url('app/MstPSLCSAManagement');
                    }
                    else {
                        Notify.alert(resp.data.message, 'warning');
                        activate();
                    }
                });

            }
        }

        // $scope.proceedtoapprove = function () {

        //     var params = {
        //         application_gid: application_gid,
        //         application_no: $scope.txtapplication_no,
        //         customer_name: $scope.txtbasiccustomer_name,
        //     }

        //     var url = "api/MstCAD/PostDocChecklistCheckerSubmit";
        //     lockUI();
        //     SocketService.post(url, params).then(function (resp) {
        //         unlockUI();
        //         if (resp.data.status = true) {
        //             Notify.alert(resp.data.message, 'success');
        //             $location.url('app/MstCadDocumentChecklistSummary');
        //         }
        //         else {
        //             Notify.alert(resp.data.message, 'warning');
        //             activate();
        //         }
        //     });
        // }

        // $scope.approve = function () {

        //     var params = {
        //         application_gid: application_gid,
        //         application_no: $scope.txtapplication_no,
        //         customer_name: $scope.txtbasiccustomer_name,
        //     }

        //     var url = "api/MstCAD/PostDocChecklistApproval";
        //     lockUI();
        //     SocketService.post(url, params).then(function (resp) {
        //         unlockUI();
        //         if (resp.data.status = true) {
        //             Notify.alert(resp.data.message, 'success');
        //             $location.url('app/MstCadDocChecklistApprovalSummary');
        //         }
        //         else {
        //             Notify.alert(resp.data.message, 'warning');
        //             activate();
        //         }
        //     });
        // }
    }
})();
