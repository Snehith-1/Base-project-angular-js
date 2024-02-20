(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AtmMstCheckpointSummaryController', AtmMstCheckpointSummaryController);

    AtmMstCheckpointSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$filter', '$route', 'ngTableParams'];

    function AtmMstCheckpointSummaryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $filter, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AtmMstCheckpointSummaryController';
        var checklistmaster_gid = $location.search().checklistmaster_gid;
        var checklistmasteradd_gid = $location.search().checklistmasteradd_gid;
        $scope.IsVisible = false;
        $scope.IsVisible1 = false;
        $scope.IsVisible2 = false;
        activate();

        function activate() {

            var params =
                {
                    checklistmaster_gid: checklistmaster_gid
                }
            var url = 'api/AtmMstChecklistMaster/GetChecklistMasterAdd';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.checklistmasteradd_list = resp.data.checklistmasteradd_list;
                unlockUI();
            });
            var url = 'api/AtmMstRiskCategory/GetRiskCategoryActive';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.riskcategory_list = resp.data.riskcategory_list;
                unlockUI();
            });

            var url = 'api/AtmMstPositiveConfirmity/GetPositiveConfirmityActive';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.positiveconfirmity_data = resp.data.positiveconfirmity_list;
                unlockUI();
            });
            var url = 'api/AtmMstChecklistMaster/GetCheckpointStatus';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.checklistmaster_list = resp.data.checklistmaster_list;
                unlockUI();
            });
        }
        $scope.addcheckpoint = function () {
            $scope.IsVisible = $scope.IsVisible ? false : true;
            $scope.IsVisible1 = $scope.IsVisible1 ? false : true;
            $scope.IsVisible2 = false;
            $scope.IsVisible3 = false;
        }
        $scope.edit = function (val) {
           
            var params = {
                checklistmasteradd_gid: val
            }
            var url = 'api/AtmMstChecklistMaster/EditChecklistMaster';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.cbocheckpointgroup_edit = resp.data.checkpointgroup_gid,
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
            $scope.IsVisible2 = $scope.IsVisible2 ? false : true;
            $scope.IsVisible = $scope.IsVisible ? false : true;
            $scope.IsVisible3 = false;
            $scope.IsVisible1 = false;
 
            $scope.id = val;
            activate();
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
        $scope.checklistmasterUpdate = function (id) {

            var riskcategoryname;
            var riskcategory_index = $scope.riskcategory_list.map(function (e) { return e.riskcategory_gid }).indexOf($scope.cboriskcategoryedit);
            if (riskcategory_index == -1) { riskcategoryname = ''; } else { riskcategoryname = $scope.riskcategory_list[riskcategory_index].riskcategory_name; };

            var positiveconfirmityname;
            var positiveconfirmity_index = $scope.positiveconfirmity_data.map(function (e) { return e.positiveconfirmity_gid }).indexOf($scope.cbopositiveconfirmityedit);
            if (positiveconfirmity_index == -1) { positiveconfirmityname = ''; } else { positiveconfirmityname = $scope.positiveconfirmity_data[positiveconfirmity_index].positiveconfirmity_name; };

            var checkpointgroupname;
            var checkpointgroup_index = $scope.checklistmaster_list.map(function (e) { return e.checkpointgroup_gid }).indexOf($scope.cbocheckpointgroup_edit);
            if (checkpointgroup_index == -1) { checkpointgroupname = ''; } else { checkpointgroupname = $scope.checklistmaster_list[checkpointgroup_index].checkpointgroup_name; };

            var params = {

                checklistmasteradd_gid: id,
                riskcategory_gid: $scope.cboriskcategoryedit,
                riskcategory_name: riskcategoryname,
                checkpointgroup_gid: $scope.cbocheckpointgroup_edit,
                checkpointgroup_name: checkpointgroupname,
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

            var url = 'api/AtmMstChecklistMaster/UpdateChecklistMaster';
            lockUI()
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI()
                    activate();
                    $state.go('app.AtmMstChecklistMasterSummary');
                    Notify.alert(resp.data.message, 'success')
                }
                else {
                    unlockUI();
                    Notify.alert(resp.data.message, 'warning')
                }

            });
            $scope.IsVisible2 = $scope.IsVisible2 ? false : true;
            $scope.IsVisible = $scope.IsVisible ? false : true;
        }
        $scope.changevalueadd = function () {
            var val1 = 0;
            var val2 = 0;
            var val3 = 0;
            var val4 = 0;
            var val5 = 0;
            if ($scope.txtna_score != undefined || $scope.txtna_score != null) {
                val1 = $scope.txtna_score;
            }

            if ($scope.txtno_score != undefined || $scope.txtno_score != null) {
                val2 = $scope.txtno_score;
            }

            if ($scope.txtyes_score != undefined || $scope.txtyes_score != null) {
                val3 = $scope.txtyes_score;
            }

            if ($scope.txtpartial_score != undefined || $scope.txtpartial_score != null) {
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
            var lscheckpointgroup_gid = '';
            var lscheckpointgroup_name = '';
            if ($scope.cbocheckpointgroup != undefined || $scope.cbocheckpointgroup != null) {
                lscheckpointgroup_gid = $scope.cbocheckpointgroup.checkpointgroup_gid;
                lscheckpointgroup_name = $scope.cbocheckpointgroup.checkpointgroup_name;
            }
            var params = {
                checkpointgroup_gid: lscheckpointgroup_gid,
                checkpointgroup_name: lscheckpointgroup_name,
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
                location.reload(true);
            });
            $scope.IsVisible2 = $scope.IsVisible2 ? false : true;
            $scope.IsVisible = $scope.IsVisible ? false : true;
           
        }
        $scope.edit_back = function (val1) {
            $scope.IsVisible2 = $scope.IsVisible2 ? false : true;
            $scope.IsVisible = false;
        }

        $scope.backcheckpoint = function (val) {
            $state.go('app.AtmMstChecklistMasterSummary');
        }

        //$scope.view = function (val2) {
        //    //var checklistmaster_gid = $scope.checklistmaster_gid;

        //    $location.url('app/AtmMstChecklistMasterAuditView?checklistmasteradd_gid=' + val2 )
        //}

        $scope.viewback = function () {

            $scope.IsVisible3 = $scope.IsVisible3 ? false : true;
            $scope.IsVisible = false;

        }

        $scope.view = function (val7) {

            var url = 'api/AtmMstChecklistMaster/EditChecklistMaster';
            var params = {
                checklistmasteradd_gid: val7
            }
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtcheckpoint1 = resp.data.checkpointgroup_name
                $scope.cboriskcategory1 = resp.data.riskcategory_gid,
                  $scope.cboriskcategory1 = resp.data.riskcategory_name,
                 $scope.cbopositiveconfirmity1 = resp.data.positiveconfirmity_gid,
                  $scope.cbopositiveconfirmity1 = resp.data.positiveconfirmity_name,
                $scope.txtcheckpoint_intent1 = resp.data.checkpoint_intent,
                $scope.txtcheckpoint_description1 = resp.data.checkpoint_description,
                $scope.txtnoteto_auditor1 = resp.data.noteto_auditor,
                $scope.txtyes_score1 = resp.data.yes_score,
                $scope.txtyes_disposition1 = resp.data.yes_disposition,
                $scope.txtno_score1 = resp.data.no_score,
                $scope.txtno_disposition1 = resp.data.no_disposition,
                $scope.txtpartial_score1 = resp.data.partial_score,
                $scope.txtpartial_disposition1 = resp.data.partial_disposition,
                $scope.txtna_score1 = resp.data.na_score,
                $scope.txtna_disposition1 = resp.data.na_disposition,
                $scope.txt_totalscore1 = resp.data.total_score,

                unlockUI();
            });

            $scope.IsVisible3 = true;
            $scope.IsVisible2 = false;
            $scope.IsVisible1 = false;
            $scope.IsVisible = $scope.IsVisible ? false : true;
            $scope.id = val7;
            activate();
        }


        $scope.addchecklist = function () {
            $state.go('app.AtmMstChecklistMasterSummary');
        }
        $scope.checkpointintent = function (checklistmasteradd_gid, checkpoint_intent) {
            var modalInstance = $modal.open({
                templateUrl: '/checkpointintent.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    checklistmasteradd_gid: checklistmasteradd_gid
                }
                lockUI();
                var url = 'api/AtmMstChecklistMaster/GetChecklistMasterIntent';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtcheckpointintent = resp.data.checkpoint_intent;
                    $scope.txtcheckpointdescription = resp.data.checkpoint_description;

                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }
        $scope.description = function (checklistmasteradd_gid, checkpoint_description) {
            var modalInstance = $modal.open({
                templateUrl: '/checkpointdescription.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                'use strict';

                angular
                    .module('angle')
                var params = {
                    checklistmasteradd_gid: checklistmasteradd_gid
                }
                lockUI();
                var url = 'api/AtmMstChecklistMaster/GetChecklistMasterDescription';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtcheckpointdescription = resp.data.checkpoint_description;

                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }
        $scope.notetoauditor = function (checklistmasteradd_gid, noteto_auditor) {
            var modalInstance = $modal.open({
                templateUrl: '/notetoauditor.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    checklistmasteradd_gid: checklistmasteradd_gid
                }
                lockUI();
                var url = 'api/AtmMstChecklistMaster/GetChecklistMasterAuditor';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtnotetoauditor = resp.data.noteto_auditor;

                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }
        $scope.auditname = function (checklistmaster_gid, audit_name) {
            var modalInstance = $modal.open({
                templateUrl: '/auditname.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    checklistmaster_gid: checklistmaster_gid
                }
                lockUI();
                var url = 'api/AtmMstChecklistMaster/GetChecklistMasterAuditorName';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtaudit_name = resp.data.audit_name;

                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

    }
    })();