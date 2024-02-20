(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstHRLoanHRDocumentViewController', MstHRLoanHRDocumentViewController);

    MstHRLoanHRDocumentViewController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','cmnfunctionService'];

    function MstHRLoanHRDocumentViewController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstHRLoanHRDocumentViewController';
        $scope.hrdocument_gid = cmnfunctionService.decryptURL($location.search().hash).lshrdocument_gid;
        var hrdocument_gid = $scope.hrdocument_gid;

        activate();

        function activate() {

            var param = {

                hrdocument_gid: hrdocument_gid
            };
            var url = 'api/MstHRLoanHRDocument/GetHRDocumentCheckListEditList';

            SocketService.getparams(url, param).then(function (resp) {
                $scope.checklistview_list = resp.data.checklist_list;
                unlockUI();
            });
            var params = {
                hrdocument_gid: hrdocument_gid
            }
            var url = 'api/MstHRLoanHRDocument/EditHRDocument';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.lblhrdocument_name = resp.data.hrdocument_name;
                $scope.lblhrdocument = resp.data.hrloantypeoffinancialassistance_name;
                $scope.lblhrdocumentseverity = resp.data.hrloanseverity_name;
                $scope.lbllms_code = resp.data.lms_code;
                $scope.lblbureau_code = resp.data.bureau_code;
                // $scope.lblCovenanttype = (resp.data.covenant_type=='N') ? 'No' :'Yes';
                unlockUI();
            });
        }
       
        $scope.Back = function () {
            $state.go('app.MstHRLoanHRDocument');

        }
        }
})();
