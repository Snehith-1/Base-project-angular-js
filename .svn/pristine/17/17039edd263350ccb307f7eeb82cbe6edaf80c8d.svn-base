(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rmObservationReport', rmObservationReport);

    rmObservationReport.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];

    function rmObservationReport($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'rmObservationReport';
        var allocationdtl_gid = $location.search().allocationdtl_gid;
        var observation_reportgid = $location.search().observation_reportgid;
        //console.log(observation_reportgid);
        activate();

        function activate() {

            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];

            // Calender Popup... //

            vm.calender1 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open1 = true;
            };

            if (observation_reportgid != '' && observation_reportgid != "undefined") {
                $scope.subbutton = "N";
            }
            else {
                $scope.subbutton = "Y";
            }

            lockUI();
            var params = {
                allocationdtl_gid: allocationdtl_gid
            }
            var url = "api/ObservationReport/GetViewObservationReportDtl";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.customer_name = resp.data.customer_name;
                $scope.customer_urn = resp.data.customer_urn;
                $scope.vertical_code = resp.data.vertical_code;
                $scope.RMD_visitname = resp.data.RMD_visitname;
                $scope.RMD_visitGid = resp.data.RMD_visitGid;
                $scope.relationship_manager_gid = resp.data.relationship_manager_gid;
                $scope.relationship_manager_name = resp.data.relationship_manager_name;
                $scope.visit_date = resp.data.visit_date;
                $scope.sanction_amount = resp.data.sanction_amount;
                $scope.disbursement_amount = resp.data.disbursement_amount;
                $scope.txtoutstanding_amount = resp.data.totalloan_outstanding;
                $scope.cboriskcode = resp.data.risk_code;
                //$scope.riskcode_classification = resp.data.riskcode_classification;
                if (resp.data.PPA_name != "")
                {
                    $scope.txtPPA_name = resp.data.PPA_name;
                }
                $scope.disbursement_date = resp.data.disbursement_date;
                $scope.txtcontact_details1 = resp.data.contact_details1;
                $scope.txtcontact_details2 = resp.data.contact_details2;
            });

            if (observation_reportgid != '' && observation_reportgid != "undefined") {
                var params = {
                    observation_reportgid: observation_reportgid
                }
                var url = "api/ObservationReport/GetViewObservationdtl";
                SocketService.getparams(url, params).then(function (resp) {
                   
                    $scope.dateof_RMDvisit = resp.data.dateof_RMDvisit;
                    $scope.txtreportpertaining = resp.data.report_pertainingto;
                    $scope.vertical = resp.data.vertical;
                    $scope.cboapproval_authority = resp.data.approving_authority;
                    $scope.txtsanction_date = resp.data.loansanction_date;
                    $scope.txtPPA_name = resp.data.PPA_name;
                    $scope.RMD_visitname = resp.data.RMDvisit_officialname;
                    $scope.loandisbursement_date = resp.data.loandisbursement_date;
                    $scope.txtpeopleaccompanied_RMD = resp.data.people_accompaniedRMD;
                    $scope.outstanding_amount = resp.data.outstanding_amount;
                    $scope.txtcurrent_DPD = resp.data.current_DPD;
                    $scope.txtcontact_details1 = resp.data.contact_details1;
                    $scope.txtcontact_details2 = resp.data.contact_details2;
                    $scope.observation_flag = resp.data.observation_flag;
                });

                var url = "api/ObservationReport/GetViewObservationCriticalDtl";
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.criticalobservation = resp.data.criticalobservation;
                    $scope.trn_status = "Y";
                });
                unlockUI();
            }
            else {
                var params = {
                    allocationdtl_gid: allocationdtl_gid
                }
                var url = "api/ObservationReport/GetTmpCriticaldtl";
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.criticalobservation = resp.data.criticalobservation;
                    $scope.trn_status = "N";
                    unlockUI();
                });
            }
    
        }


        $scope.observations = function (customer_name, customer_urn) {

            var modalInstance = $modal.open({
                templateUrl: '/criticalobservationModal.html',
                controller: ModalInstanceCtrl,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.customer_name = customer_name;
                $scope.customer_urn = customer_urn;
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.criticalobservationsubmit = function () {
                    var params = {
                        allocationdtl_gid: allocationdtl_gid,
                        observation_reportgid:observation_reportgid,
                        criteria: $scope.txtcriteria,
                        RMD_observations: $scope.txtRMD_observations,
                        actionable_recommended: $scope.txtactionable_recommend
                    }
                    lockUI();
                    var url = "api/ObservationReport/PostObservationCritical"
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

        $scope.trndeleteCriticalObservation = function (critical_observationgid)
        {
            var params = {
                critical_observationgid: critical_observationgid
            }
            var url = "api/ObservationReport/GetDeleteTrnCriticalObser"
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
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


        $scope.deleteCriticalObservation = function (tmpcritical_observationgid) {
            var params = {
                tmpcritical_observationgid: tmpcritical_observationgid
            }
            var url = "api/ObservationReport/GetDeleteCriticalObser"
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
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

        $scope.outstandingamountchange = function () {
            var input = document.getElementById('outstanding_amount').value;
            var arr = input.split(',');
            var i;
            for (i = 0; i < arr.length; i++) {

                var str = input.replace(',', '');
                input = str;
            }
            var output = Number(str).toLocaleString('en-US');
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtoutstanding_amount = "";
            }
            else {
                $scope.txtoutstanding_amount = output;
            }
        }

        $scope.observationreportSubmit = function () {

            var visitdate = $scope.visit_date;
            var visit_date = visitdate.split("-").reverse().join("-");

            var disbursementdate = $scope.disbursement_date;
            var disbursement_date = disbursementdate.split("-").reverse().join("-");

            var params = {
                observation_reportgid: observation_reportgid,
                customer_name: $scope.customer_name,
                customer_urn: $scope.customer_urn,
                risk_code: $scope.cboriskcode,
                //riskcode_classification: $scope.riskcode_classification,
                dateof_RMDvisit: visit_date,
                allocationdtl_gid: allocationdtl_gid,
                report_pertainingto: $scope.txtreportpertaining,
                vertical: $scope.vertical_code,
                disbursement_amount: $scope.disbursement_amount,
                approving_authority: $scope.cboapproval_authority,
                loansanction_date: $scope.txtsanction_date,
                relationship_manager_gid: $scope.relationship_manager_gid,
                relationship_manager_name: $scope.relationship_manager_name,
                PPA_name: $scope.txtPPA_name,
                RMDvisit_officialname: $scope.RMD_visitname,
                loandisbursement_date: disbursement_date,
                people_accompaniedRMD: $scope.txtpeopleaccompanied_RMD,
                sanction_amount: $scope.sanction_amount,
                outstanding_amount: $scope.txtoutstanding_amount,
                current_DPD: $scope.txtcurrent_DPD,
                contact_details1: $scope.txtcontact_details1,
                contact_details2: $scope.txtcontact_details2
            }
            lockUI();
            var url = "api/ObservationReport/PostObservationReport"
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                    $state.go('app.rmVisitReport');
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
