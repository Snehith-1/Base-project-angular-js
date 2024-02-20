(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCreditMappingEditController', MstCreditMappingEditController);

    MstCreditMappingEditController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function MstCreditMappingEditController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCreditMappingEditController';
        $scope.creditmapping_gid = $location.search().lscreditmapping_gid;
        var creditmapping_gid = $scope.creditmapping_gid;

        activate();

        function activate() {
            var params = {
                creditmapping_gid: creditmapping_gid
            }
            var url = 'api/MstCreditMapping/GetCreditGroupEdit';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtcreditgroup_edit = resp.data.creditgroup_id;
                $scope.txteditcreditgroup_name = resp.data.creditgroup_name;
                $scope.cbocredithead_editlist = resp.data.Creditheadem_list;
                $scope.Credithead = resp.data.Credithead;
                $scope.cbocredithead_edit = [];
                if (resp.data.Credithead != null) {
                    var count = resp.data.Credithead.length;
                    for (var i = 0; i < count; i++) {
                        var indexs = $scope.cbocredithead_editlist.map(function (x) { return x.employee_gid; }).indexOf(resp.data.Credithead[i].employee_gid);
                        $scope.cbocredithead_edit.push($scope.cbocredithead_editlist[indexs]);
                        $scope.$parent.cboSecondaryValueChain = $scope.cboSecondaryValueChain;
                    }
                }
                $scope.cbonational_editlist = resp.data.Creditnationalmanagerem_list;
                $scope.Creditnationalmanager = resp.data.Creditnationalmanager;
                $scope.cbonational_edit = [];
                if (resp.data.Creditnationalmanager != null) {
                    var count = resp.data.Creditnationalmanager.length;
                    for (var i = 0; i < count; i++) {
                        var indexs = $scope.cbonational_editlist.map(function (x) { return x.employee_gid; }).indexOf(resp.data.Creditnationalmanager[i].employee_gid);
                        $scope.cbonational_edit.push($scope.cbonational_editlist[indexs]);
                        $scope.$parent.cboSecondaryValueChain = $scope.cboSecondaryValueChain;
                    }
                }
                $scope.cboregional_editlist = resp.data.Creditregionalmanagerem_list;
                $scope.Creditregionalmanager = resp.data.Creditregionalmanager;
                $scope.cboregional_edit = [];
                if (resp.data.Creditregionalmanager != null) {
                    var count = resp.data.Creditregionalmanager.length;
                    for (var i = 0; i < count; i++) {
                        var indexs = $scope.cboregional_editlist.map(function (x) { return x.employee_gid; }).indexOf(resp.data.Creditregionalmanager[i].employee_gid);
                        $scope.cboregional_edit.push($scope.cboregional_editlist[indexs]);
                        $scope.$parent.cboSecondaryValueChain = $scope.cboSecondaryValueChain;
                    }
                }
                $scope.cbocreditmanager_editlist = resp.data.CreditManagerem_list;
                $scope.CreditManager = resp.data.CreditManager;
                $scope.cbocreditmanager_edit = [];
                if (resp.data.CreditManager != null) {
                    var count = resp.data.CreditManager.length;
                    for (var i = 0; i < count; i++) {
                        var indexs = $scope.cbocreditmanager_editlist.map(function (x) { return x.employee_gid; }).indexOf(resp.data.CreditManager[i].employee_gid);
                        $scope.cbocreditmanager_edit.push($scope.cbocreditmanager_editlist[indexs]);
                        $scope.$parent.cboSecondaryValueChain = $scope.cboSecondaryValueChain;
                    }
                }
            });
            unlockUI();
        }
        $scope.update = function () {
            lockUI();
            var url = 'api/MstCreditMapping/PostCreditGroupUpdate';
            var params = {
                creditmapping_gid: creditmapping_gid,
                Credithead: $scope.cbocredithead_edit,
                Creditnationalmanager: $scope.cbonational_edit,
                Creditregionalmanager: $scope.cboregional_edit,
                CreditManager: $scope.cbocreditmanager_edit,
                creditgroup_name: $scope.txteditcreditgroup_name
            }
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
         
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                 
                    $state.go('app.MstCreditMappingSummary');
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

        $scope.back = function () {
            $state.go('app.MstCreditMappingSummary');
        };

     

    }
})();