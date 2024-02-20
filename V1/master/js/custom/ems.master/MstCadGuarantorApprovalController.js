(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCadGuarantorApprovalController', MstCadGuarantorApprovalController);

    MstCadGuarantorApprovalController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function MstCadGuarantorApprovalController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCadGuarantorApprovalController';
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

            var url = 'api/MstCadFlow/GetApplicationBasicView';
            lockUI();
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
                if($scope.docchecklist_approvalflag == 'N')
                {
                    $scope.approval = true;
                }
            });

            var url = 'api/MstCadFlow/GetIndividualList';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.CreditIndividual_List = resp.data.individual_List;
            });

            var url = 'api/MstCadFlow/GetInstitutionList';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.CreditInstitution_List = resp.data.institution_List;
            });

            var url = 'api/MstCAD/GetDocChecklistApprovalDtls';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.maker_name = resp.data.maker_name;
                $scope.checker_name = resp.data.checker_name;
                $scope.approver_name = resp.data.approver_name;
                $scope.maker_approveddate = resp.data.maker_approveddate;
                $scope.checker_approveddate = resp.data.checker_approveddate;
                $scope.approver_approveddate = resp.data.approver_approveddate;
            });

        }

        var params =
       {
           application_gid: application_gid
       }
        var url = "api/MstCadFlow/GetGroupSummary";
        lockUI();
        SocketService.getparams(url, params).then(function (resp) {
            unlockUI();
            $scope.group_list = resp.data.group_list;
            angular.forEach($scope.group_list, function (value, key) {
                var params = {
                    group_gid: value.group_gid
                };

                var url = 'api/MstCadFlow/GetGrouptoMemberList';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    value.groupmember_list = resp.data.groupmember_list;
                    value.expand = false;
                });
            });
        });

        $scope.Back = function () {
            $location.url('app/MstDocChecklistApprovalCompleted?application_gid=' + application_gid + '&lspage=CadDocumentChecklist');           
        }

        $scope.view = function (credit_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/docview.html',
                controller: ModalInstanceCtrl,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var url = 'api/MstCADFlow/GetproductDropDown';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    unlockUI();
                    $scope.buyerlist = resp.data.buyerlist;
                });
               
                var params = {
                    credit_gid: credit_gid
                }
                var url = "api/MstCADCreditAction/GetCADTrnTaggedDocList";
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.taggeddoc_list = resp.data.TaggedDocument;
                });
                var url = "api/MstCADCreditAction/GetCADTrnCovenantTaggedDocList";
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.taggedcovenantdoc_list = resp.data.TaggedDocument;
                    unlockUI();
                });
                
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

            }
        }


        $scope.approve = function () {

            var params = {
                application_gid: application_gid,
                application_no: $scope.txtapplication_no,
                customer_name: $scope.txtbasiccustomer_name,
            }

            var url = "api/MstCADFlow/PostDocChecklistApproval";
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status = true) {
                    Notify.alert(resp.data.message, 'success');
                    $location.url('app/MstCadDocChecklistApprovalSummary');
                }
                else {
                    Notify.alert(resp.data.message, 'warning');
                    activate();
                }
            });
        }

        $scope.institution_add = function (institution_gid) {
            $location.url('app/MstCadDocumentChecklistAdd?application_gid=' + application_gid + '&credit_gid=' + institution_gid + '&lspage=CADAfterApproval&lstype=Institution&lspath=' + lspath + '&lsfollowup=N');
        }
        $scope.individual_add = function (contact_gid) {
            $location.url('app/MstCadDocumentChecklistAdd?application_gid=' + application_gid + '&credit_gid=' + contact_gid + '&lspage=CADAfterApproval&lstype=Individual&lspath=' + lspath + '&lsfollowup=N');
        }

        $scope.group_add = function (group_gid) {
            $location.url('app/MstCadDocumentChecklistAdd?application_gid=' + application_gid + '&credit_gid=' + group_gid + '&lspage=CADAfterApproval&lstype=Group&lspath=' + lspath + '&lsfollowup=N');
        }
        $scope.member_add = function (contact_gid) {
            $location.url('app/MstCadDocumentChecklistAdd?application_gid=' + application_gid + '&credit_gid=' + contact_gid + '&lspage=CADAfterApproval&lstype=Individual&lspath=' + lspath + '&lsfollowup=N');
        }
    }
})();
