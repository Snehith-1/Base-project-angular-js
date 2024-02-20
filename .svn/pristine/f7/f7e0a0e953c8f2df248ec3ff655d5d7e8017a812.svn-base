(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstRMDocChecklistDtlsController', MstRMDocChecklistDtlsController);

    MstRMDocChecklistDtlsController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function MstRMDocChecklistDtlsController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstRMDocChecklistDtlsController';
        var application_gid = $location.search().application_gid;
        activate();

        function activate() {

            var params = {
                application_gid: application_gid
            }

            var url = 'api/MstCadFlow/GetApplicationBasicView';
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

            var url = 'api/MstCadFlow/GetIndividualList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.CreditIndividual_List = resp.data.individual_List;
            });

            var url = 'api/MstCadFlow/GetInstitutionList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.CreditInstitution_List = resp.data.institution_List;
            });

        }

        var params =
       {
           application_gid: application_gid
       }
        var url = "api/MstCadFlow/GetGroupSummary";
        SocketService.getparams(url, params).then(function (resp) {
            $scope.group_list = resp.data.group_list;
            angular.forEach($scope.group_list, function (value, key) {
                var params = {
                    group_gid: value.group_gid
                };

                var url = 'api/MstCadFlow/GetGrouptoMemberList';
                SocketService.getparams(url, params).then(function (resp) {
                    value.groupmember_list = resp.data.groupmember_list;
                    value.expand = false;
                });
            });
        });

        $scope.Back = function () {
            $location.url('app/MstPostCcActivitiesRMView?application_gid=' + application_gid);
        }

        $scope.view = function (credit_gid) {
             var modalInstance = $modal.open({
                templateUrl: '/docview.html',
                controller: ModalInstanceCtrl,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var url = 'api/MstApplicationAdd/GetproductDropDown';
                SocketService.get(url).then(function (resp) {
                    $scope.buyerlist = resp.data.buyerlist;
                });
                 
                lockUI();
                var params = {
                    credit_gid: credit_gid
                }
                var url = "api/MstCAD/GetCADTrnTaggedDocList";
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.taggeddoc_list = resp.data.TaggedDocument;
                });
                var url = "api/MstCAD/GetCADTrnCovenantTaggedDocList";
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.taggedcovenantdoc_list = resp.data.TaggedDocument;
                    unlockUI();
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

            }
        }


        $scope.institution_view = function (institution_gid) {
            $location.url('app/MstCadDocumentChecklistView?application_gid=' + application_gid + '&credit_gid=' + institution_gid + '&lspage=RMDocChecklist&lstype=Institution');
        }

        $scope.individual_view = function (contact_gid) {
            $location.url('app/MstCadDocumentChecklistView?application_gid=' + application_gid + '&credit_gid=' + contact_gid + '&lspage=RMDocChecklist&lstype=Individual');
        }

        $scope.group_view = function (group_gid) {
            $location.url('app/MstCadDocumentChecklistView?application_gid=' + application_gid + '&credit_gid=' + group_gid + '&lspage=RMDocChecklist&lstype=Group');
        }

        $scope.member_view = function (contact_gid) {
            $location.url('app/MstCadDocumentChecklistView?application_gid=' + application_gid + '&credit_gid=' + contact_gid + '&lspage=RMDocChecklist&lstype=Individual');
        }

    }
})();
