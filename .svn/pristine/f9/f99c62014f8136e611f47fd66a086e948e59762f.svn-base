(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstApplicationCreationAddController', MstApplicationCreationAddController);

        MstApplicationCreationAddController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

        function MstApplicationCreationAddController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
            /* jshint validthis:true */
            var vm = this;
            vm.title = 'MstApplicationCreationAddController';
            var lstab = $location.search().lstab;
            console.log(lstab)
            activate();
            function activate() {
$scope.application_status=false;
                if (lstab == 'add') {
                    $scope.hide_generalsummary = false;
                    $scope.show_generalform = false;
                     }
              
                else {
                    $scope.hide_generalsummary = true;
                    $scope.show_generalform = true;
                }
              
               

            }
           
            
            $scope.loading = function () {
                lockUI();
                var url = 'api/MstApplicationAdd/GetIndividualSummary';
                SocketService.get(url).then(function (resp) {
                    unlockUI();
                    $scope.individual_list = resp.data.cicindividual_list;
                    
                });
                var url = 'api/MstApplicationAdd/GetProductList';
                SocketService.get(url).then(function (resp) {
                    $scope.lblsocial_capital = resp.data.social_capital;
                    $scope.lbltrade_capital = resp.data.trade_capital;
                });
                var url = 'api/MstApplicationAdd/GetInstitutionSummary';
                SocketService.get(url).then(function (resp) {
                    $scope.institution_list = resp.data.cicinstitution_list;
                });
                var url = 'api/MstApplicationAdd/GetAppSocialTradeSummary';
                SocketService.get(url).then(function (resp) {
                    $scope.application_no = resp.data.application_no;
                    $scope.social_capital = resp.data.social_capital;
                    $scope.trade_capital = resp.data.trade_capital;
                    $scope.application_gid = resp.data.application_gid;
                    $scope.created_date = resp.data.created_date;
                    $scope.created_by = resp.data.created_by;
                    $scope.updated_date = resp.data.updated_date;
                    $scope.updated_by = resp.data.updated_by;
                });
                var url = 'api/MstApplicationAdd/GetAppProductcharges';
                SocketService.get(url).then(function (resp) {
                    $scope.lbloveralllimit_amount = resp.data.overalllimit_amount;
                    $scope.lblprocessing_fee = resp.data.processing_fee;
                    $scope.lbldoc_charges = resp.data.doc_charges;
                    $scope.application_gid = resp.data.application_gid;
                    $scope.applicant_type = resp.data.applicant_type;

                    $scope.productcharge_flag = resp.data.productcharge_flag;
                     $scope.lblproductcharges_status = resp.data.productcharges_status;
                   
                    $scope.economical_flag = resp.data.economical_flag;

                    if ($scope.applicant_type == "" || $scope.applicant_type == null) {
                        $scope.applicant_typenull = true;
                        $scope.applicant_typenotnull = false;
                    }
                    else {
                        $scope.applicant_typenotnull = true;
                        $scope.applicant_typenull = false;
                    }

                    if ($scope.productcharge_flag == 'Y') {
                        $scope.product_chargetab = false;
                    }
                    else {
                        $scope.product_chargetab = true;
                    }

                    if ($scope.economical_flag == 'Y') {
                        $scope.social_tradetab = false;
                        $scope.social_trade = true;
                    }
                    else {
                        $scope.social_tradetab = true;
                        $scope.social_trade = false;
                    }
                });
            }
           

            $scope.company_add = function () {
                $state.go('app.MstApplicationInstitutionAdd');
            }

            $scope.tabclick = function () {
                lockUI();
                var url = 'api/MstApplicationAdd/GetGeneralInfo';
                SocketService.get(url).then(function (resp) {
                    unlockUI();
                    $scope.application_status = resp.data.application_status;
                });

                var url = 'api/MstApplicationAdd/GetIndividualSummary';
                SocketService.get(url).then(function (resp) {
                    $scope.cicindividual_list = resp.data.cicindividual_list;

                });
             
                var url = 'api/MstApplicationAdd/GetInstitutionSummary';
                SocketService.get(url).then(function (resp) {
                    $scope.cicinstitution_list = resp.data.cicinstitution_list;
                });
            }
           
        }
})();

