(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AtmMstCheckpointEditController', AtmMstCheckpointEditController);

    AtmMstCheckpointEditController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route'];

    function AtmMstCheckpointEditController($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AtmMstCheckpointEditController';
        var checkpointgroupadd_gid = $location.search().checkpointgroupadd_gid;
        activate();
        function activate() {
            var url = 'api/AtmMstRiskCategory/GetRiskCategory';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.riskcategory_list = resp.data.riskcategory_list;
                unlockUI();
            });

            var url = 'api/AtmMstPositiveConfirmity/GetPositiveConfirmity';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.positiveconfirmity_data = resp.data.positiveconfirmity_list;
                unlockUI();
            });
            var params = {
                checkpointgroupadd_gid: checkpointgroupadd_gid
            }
            var url = 'api/AtmMstCheckpointGroup/EditCheckpoint';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.cboriskcategoryedit = resp.data.riskcategory_gid,
                $scope.cbopositiveconfirmityedit = resp.data.positiveconfirmity_gid,
                $scope.txteditcheckpoint_intent = resp.data.checkpoint_intent,
                $scope.txteditcheckpoint_description = resp.data.checkpoint_description,
                $scope.txteditnoteto_auditor = resp.data.noteto_auditor,
                $scope.txtedityes_score = parseFloat(resp.data.yes_score),
                $scope.txtedityes_disposition = resp.data.yes_disposition,
                $scope.txteditno_score = parseFloat(resp.data.no_score),
                $scope.txteditno_disposition = resp.data.no_disposition,
                $scope.txteditpartial_score = parseFloat(resp.data.partial_score),
                $scope.txteditpartial_disposition = resp.data.partial_disposition,
                $scope.txteditna_score = parseFloat(resp.data.na_score),
                $scope.txteditna_disposition = resp.data.na_disposition,
                $scope.txtedit_totalscore = parseFloat(resp.data.total_score),


                unlockUI();
            });
        }

        $scope.changevalue = function () {
            //var val1 = parseFloat($scope.txteditna_score);
            var val1 = parseFloat($scope.txteditna_score);
            var val2 = parseFloat($scope.txteditno_score);
            var val3 = parseFloat($scope.txtedityes_score);
            var val4 = parseFloat($scope.txteditpartial_score);
            var val5 = parseFloat($scope.txtedit_totalscore);
            if ($scope.txteditna_score != undefined || $scope.txteditna_score != null) {
                val1 = parseFloat($scope.txteditna_score);
            }

            if ($scope.txteditno_score != undefined || $scope.txteditno_score != null) {
                val2 = parseFloat($scope.txteditno_score);
            }

            if ($scope.txtedityes_score != undefined || $scope.txtedityes_score != null) {
                val3 = parseFloat($scope.txtedityes_score);
            }

            if ($scope.txteditpartial_score != undefined || $scope.txteditpartial_score != null) {
                val4 = parseFloat($scope.txteditpartial_score);
            }


            val5 = val1 + val2 + val3 + val4;


            $scope.txtedit_totalscore = val5;

        }
        $scope.Update = function () {

            var riskcategoryname;
            var riskcategory_index = $scope.riskcategory_list.map(function (e) { return e.riskcategory_gid }).indexOf($scope.cboriskcategoryedit);
            if (riskcategory_index == -1) { riskcategoryname = ''; } else { riskcategoryname = $scope.riskcategory_list[riskcategory_index].riskcategory_name; };
            var positiveconfirmityname;
            var positiveconfirmity_index = $scope.positiveconfirmity_data.map(function (e) { return e.positiveconfirmity_gid }).indexOf($scope.cbopositiveconfirmityedit);
            if (positiveconfirmity_index == -1) { positiveconfirmityname = ''; } else { positiveconfirmityname = $scope.positiveconfirmity_data[positiveconfirmity_index].positiveconfirmity_name; };

            var params = {

                checkpointgroupadd_gid: checkpointgroupadd_gid,
                riskcategory_gid: $scope.cboriskcategoryedit,
                riskcategory_name: riskcategoryname,
                positiveconfirmity_gid: $scope.cbopositiveconfirmityedit,
                positiveconfirmity_name: positiveconfirmityname,
                checkpoint_intent: $scope.txteditcheckpoint_intent,
                checkpoint_description: $scope.txteditcheckpoint_description,
                noteto_auditor: $scope.txteditnoteto_auditor,
                yes_score: $scope.txtedityes_score,
                yes_disposition: $scope.txtedityes_disposition,
                no_score: $scope.txteditno_score,
                no_disposition: $scope.txteditno_disposition,
                partial_score: $scope.txteditpartial_score,
                partial_disposition: $scope.txteditpartial_disposition,
                na_score: $scope.txteditna_score,
                na_disposition: $scope.txteditna_disposition,
                total_score: $scope.txtedit_totalscore,

            }

            var url = 'api/AtmMstCheckpointGroup/UpdateCheckpoint';
            lockUI()
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI()
                    activate();
                    $state.go('app.AtmMstCheckpointGroupAdd');
                    Notify.alert(resp.data.message, 'success')
                }
                else {
                    unlockUI();
                    Notify.alert(resp.data.message, 'warning')
                }

            });
        }
        $scope.back = function (val) {
            $state.go('app.AtmMstCheckpointGroupAdd');
        }
    }
})();