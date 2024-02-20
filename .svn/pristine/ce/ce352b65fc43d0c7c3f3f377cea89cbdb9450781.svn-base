(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AtmMstChecklistMasterAuditController', AtmMstChecklistMasterAuditController);

    AtmMstChecklistMasterAuditController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$filter', '$route', 'ngTableParams'];

    function AtmMstChecklistMasterAuditController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $filter, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AtmMstChecklistMasterAuditController';
        var checklistmaster_gid = $location.search().checklistmaster_gid;
        var checklistmasteradd_gid = $location.search().checklistmasteradd_gid;

        activate();

        function activate() {
           
            var url = 'api/AtmMstPositiveConfirmity/GetPositiveConfirmity';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.positiveconfirmity_data = resp.data.positiveconfirmity_list;
                unlockUI();
            });

            
            var url = 'api/AtmMstRiskcategory/GetRiskcategory';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.riskcategory_list = resp.data.riskcategory_list;
                unlockUI();
            });

            var params = {
                checklistmaster_gid: checklistmaster_gid
            }
        }
          
        $scope.changevalue = function () {
            var val1 = 0;
           var val2 = 0 ;
           var val3 = 0;
           var val4 = 0;
           var val5 = 0 ;
           if ($scope.txtna_score != undefined || $scope.txtna_score != null)
           {
               val1 = $scope.txtna_score;
           }
         
           if ($scope.txtno_score != undefined || $scope.txtno_score != null)
           {
               val2 = $scope.txtno_score;
           }
          
           if ($scope.txtyes_score != undefined || $scope.txtyes_score != null)
           {
               val3 = $scope.txtyes_score;
           }
          
           if ($scope.txtpartial_score != undefined || $scope.txtpartial_score != null)
           {
               val4 = $scope.txtpartial_score;
           }
          
         
               val5 = val1 + val2 + val3 + val4;
             

               $scope.txt_totalscore = parseFloat(val5.toFixed(2));
           
        }
        $scope.submitChecklistmasterAdd = function () {
           
            var lsriskcategory_gid = '';
            var lsriskcategory_name = '';
            if ($scope.cboriskcategory != undefined || $scope.cboriskcategory != null) {
                lsriskcategory_gid = $scope.cboriskcategory.riskcategory_gid;
                lsriskcategory_name = $scope.cboriskcategory.riskcategory_name;
            }

            var lspositiveconfirmity_gid = '';
            var lspositiveconfirmity_name = '';
            if ($scope.cbopositiveconfirmity != undefined || $scope.cbopositiveconfirmity != null) {
                lspositiveconfirmity_gid = $scope.cbopositiveconfirmity.positiveconfirmity_gid;
                lspositiveconfirmity_name = $scope.cbopositiveconfirmity.positiveconfirmity_name;
            }
            var params = {
                positiveconfirmity_gid: lspositiveconfirmity_gid,
                positiveconfirmity_name: lspositiveconfirmity_name,
                riskcategory_gid: lsriskcategory_gid,
                riskcategory_name: lsriskcategory_name,
                checklistmaster_gid: checklistmaster_gid,
                checkpoint_intent: $scope.txtcheckpoint_intent,
                checkpoint_description: $scope.txtcheckpoint_description,
                noteto_auditor: $scope.txtnoteto_auditor,
                yes_score: $scope.txtyes_score,
                yes_disposition: $scope.txtyes_disposition,
                no_score: $scope.txtno_score,
                no_disposition: $scope.txtno_disposition,
                partial_score: $scope.txtpartial_score,
                partial_disposition: $scope.txtpartial_disposition,
                na_score: $scope.txtna_score,
                na_disposition: $scope.txtna_disposition,
                total_score: $scope.txt_totalscore,

            }

            var url = 'api/AtmMstChecklistMaster/PostChecklistMasterAdd';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {


                    $scope.cboriskcategory = '';
                    $scope.cbopositiveconfirmity = '';
                    $scope.txtcheckpoint_intent = '';
                    $scope.txtcheckpoint_description = '';
                    $scope.txtnoteto_auditor = '';
                    $scope.txtyes_score = '';
                    $scope.txtyes_disposition = '';
                    $scope.txtno_score = '';
                    $scope.txtno_disposition = '';
                    $scope.txtpartial_score = '';
                    $scope.txtpartial_disposition = '';
                    $scope.txtna_score = '';
                    $scope.txtna_disposition = '';
                    $scope.txt_totalscore = '';
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    //$state.go('app.AtmMstChecklistMasterAudit');
                    //activate();
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
       
        $scope.checklistback = function () {
            $location.url('app/AtmMstCheckpointSummary?checklistmaster_gid=' + checklistmaster_gid)
        }
      
    }
})();