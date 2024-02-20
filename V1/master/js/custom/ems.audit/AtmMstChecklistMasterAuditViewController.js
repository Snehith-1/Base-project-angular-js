(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AtmMstChecklistMasterAuditViewController', AtmMstChecklistMasterAuditViewController);

    AtmMstChecklistMasterAuditViewController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route'];

    function AtmMstChecklistMasterAuditViewController($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AtmMstChecklistMasterAuditViewController';
        var checklistmasteradd_gid = $location.search().checklistmasteradd_gid;
        var checklistmasteradd_gid = $location.search().checklistmasteradd_gid;
        var checklistmaster_gid = $location.search().checklistmaster_gid;


        activate();
        function activate() {
            var url = 'api/AtmMstChecklistMaster/EditChecklistMaster';
            var params = {
                checklistmasteradd_gid: checklistmasteradd_gid
            }       
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.cboriskcategory = resp.data.riskcategory_gid,
                  $scope.cboriskcategory = resp.data.riskcategory_name,
                 $scope.cbocheckpointgroup = resp.data.checkpointgroup_gid,
                  $scope.cbocheckpointgroup = resp.data.checkpointgroup_name,
                 $scope.cbopositiveconfirmity = resp.data.positiveconfirmity_gid,
                  $scope.cbopositiveconfirmity = resp.data.positiveconfirmity_name,
                $scope.txtcheckpoint_intent = resp.data.checkpoint_intent,
                $scope.txtcheckpoint_description = resp.data.checkpoint_description,
                $scope.txtnoteto_auditor = resp.data.noteto_auditor,
                $scope.txtyes_score = resp.data.yes_score,
                $scope.txtyes_disposition = resp.data.yes_disposition,
                $scope.txtno_score = resp.data.no_score,
                $scope.txtno_disposition = resp.data.no_disposition,
                $scope.txtpartial_score = resp.data.partial_score,
                $scope.txtpartial_disposition = resp.data.partial_disposition,
                $scope.txtna_score = resp.data.na_score,
                $scope.txtna_disposition = resp.data.na_disposition,
                $scope.txt_totalscore = resp.data.total_score,

               
                unlockUI();
            });
        }
        $scope.checklistback = function () {
            $state.go('app.AtmMstChecklistMasterSummary');
            //var checklistmaster_gid = $scope.checklistmaster_gid;
            //$location.url('app/AtmMstCheckpointSummary?checklistmaster_gid=' + checklistmaster_gid)
        }
    }

})();