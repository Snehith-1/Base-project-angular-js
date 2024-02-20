(function () {
    'use strict';

    angular
        .module('angle')
        .controller('observationReportApproval', observationReportApproval);

    observationReportApproval.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];

    function observationReportApproval($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'observationReportApproval';

        activate();

        function activate() {
            var params = {
                observation_reportgid: localStorage.getItem('observation_reportgid')
            }
            var url = "api/ObservationReport/GetViewObservationdtl";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.customer_name = resp.data.customer_name;
                $scope.customer_urn = resp.data.customer_urn;
                $scope.dateof_RMDvisit = resp.data.dateof_RMDvisit;
                $scope.report_pertainingto = resp.data.report_pertainingto;
                $scope.vertical = resp.data.vertical;
                $scope.disbursement_amount = resp.data.disbursement_amount;
                $scope.approving_authority = resp.data.approving_authority;
                $scope.loansanction_date = resp.data.loansanction_date;
                $scope.relationship_manager_name = resp.data.relationship_manager_name;
                $scope.PPA_name = resp.data.PPA_name;
                $scope.RMDvisit_officialname = resp.data.RMDvisit_officialname;
                $scope.loandisbursement_date = resp.data.loandisbursement_date;
                $scope.people_accompaniedRMD = resp.data.people_accompaniedRMD;
                $scope.sanction_amount = resp.data.sanction_amount;
                $scope.outstanding_amount = resp.data.outstanding_amount;
                $scope.current_DPD = resp.data.current_DPD;
                $scope.contact_details1 = resp.data.contact_details1;
                $scope.contact_details2 = resp.data.contact_details2;
                $scope.observation_flag = resp.data.observation_flag;
                $scope.cboriskcode = resp.data.risk_code;
                //$scope.riskcode_classification = resp.data.riskcode_classification;
            });

            var url = "api/ObservationReport/GetViewObservationCriticalDtl";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.criticalobservation = resp.data.criticalobservation;
            });
        }

        $scope.ObservationRemarks = function (critical_observationgid, criteria, RMD_observations, actionable_recommended, relationship_manager_remarks) {

            var modalInstance = $modal.open({
                templateUrl: '/criticalobservationModal.html',
                controller: ModalInstanceCtrl,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.txtcriteria = criteria;
                $scope.txtRMD_observations = RMD_observations;
                $scope.txtactionable_recommend = actionable_recommended;
                $scope.txtrelationshipmangerremarks = relationship_manager_remarks;
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.observationremarkssubmit = function () {
                    var params = {
                        critical_observationgid: critical_observationgid,
                        relationshipmanager_remarks: $scope.txtrelationshipmangerremarks,
                    }
                    var url = "api/ObservationReport/PostObservationCriticalRemarks"
                    SocketService.post(url, params).then(function (resp) {

                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                            $modalInstance.close('closed');
                            activate();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                        }
                    });
                }
            }
        }

        $scope.ObservationReportSubmit = function () {
            var params = {
                observation_reportgid: localStorage.getItem('observation_reportgid')
            }
            lockUI();
            var url = "api/ObservationReport/PostObservationRemarksSubmit";
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                    $state.go('app.observationReportSummary');
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                }
            });

        }
    }
})();
