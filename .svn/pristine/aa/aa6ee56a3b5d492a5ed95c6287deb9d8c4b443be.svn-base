(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrTrnCreditCompanyDtlAddController', AgrTrnCreditCompanyDtlAddController);

        AgrTrnCreditCompanyDtlAddController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function AgrTrnCreditCompanyDtlAddController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrTrnCreditCompanyDtlAddController';
        $scope.institution_gid = $location.search().institution_gid;
        var institution_gid = $scope.institution_gid;
        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;
        lockUI();
        activate();
        function activate() {

            var params = {
                credit_gid: institution_gid
            }
            var url = 'api/AgrMstApplicationAdd/GetGeneticCodeList';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.genetic_list = resp.data.genetic_list;
            });

            var url = 'api/AgrTrnAppCreditUnderWriting/GetGeneticCodeList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.mstgeneticcode_list = resp.data.mstcuwgeneticcode_list;
            });

            vm.submitted = false;
            vm.validateInput = function (name, type) {
                var input = vm.formValidate[name];
                return (input.$dirty || vm.submitted) && input.$error[type];
            };

            // Submit form
            vm.submitForm = function () {
                vm.submitted = true;
                if (vm.formValidate.$valid) {
                } else {
                    return false;
                }
            };

            var params = {
                credit_gid: institution_gid,
                applicant_type: 'Institution'
            }

            var url = 'api/AgrTrnAppCreditUnderWriting/GetCreditOperationsView';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtinstitution_name = resp.data.company_name;
                $scope.txtstakeholder_type = resp.data.stakeholder_type;
            });

        }

        $scope.add_creditgeneticcode = function () {
            if (($scope.cboGeneticCode == undefined) || ($scope.rdbStatus == undefined) || ($scope.txtgenetic_remarks == undefined) || ($scope.cboGeneticCode == null) || ($scope.rdbStatus == null) || ($scope.txtgenetic_remarks == null) || ($scope.cboGeneticCode == '') || ($scope.rdbStatus == '') || ($scope.txtgenetic_remarks == '')) {
                Notify.alert('Enter All Mandatory Fields', 'warning');
            }
            else {
                var params = {
                    application_gid: application_gid,
                    credit_gid: institution_gid,
                    geneticcode_name: $scope.cboGeneticCode.geneticcode_name,
                    geneticcode_gid: $scope.cboGeneticCode.geneticcode_gid,
                    geneticcode_status: $scope.rdbStatus,
                    geneticcode_remarks: $scope.txtgenetic_remarks
                }
                var url = 'api/AgrTrnAppCreditUnderWriting/PostGeneticCode';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        activate();
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        activate();
                    }
                    $scope.cboGeneticCode = '';
                    $scope.rdbStatus = '';
                    $scope.txtgenetic_remarks = '';

                });
            }
        }

        $scope.geneticcode_edit = function (creditgeneticcode_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editgeneticcode.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var url = 'api/AgrMstApplication360/GetGeneticCode';
                SocketService.get(url).then(function (resp) {
                    $scope.geneticcode_list = resp.data.application_list;
                });

                var params = {
                    creditgeneticcode_gid: creditgeneticcode_gid
                }
                var url = 'api/AgrTrnAppCreditUnderWriting/EditGeneticCode';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.lblgeneticcode_name = resp.data.geneticcode_name;
                    $scope.rdbeditgenetic_status = resp.data.geneticcode_status;
                    $scope.txteditgenetic_remarks = resp.data.geneticcode_remarks;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update_geneticcode = function () {
                    var geneticcode_Name = $('#GeneticCode :selected').text();

                    var params = {
                        geneticcode_gid: $scope.cboeditGeneticCode,
                        geneticcode_name: geneticcode_Name,
                        geneticcode_status: $scope.rdbeditgenetic_status,
                        geneticcode_remarks: $scope.txteditgenetic_remarks,
                        creditgeneticcode_gid: creditgeneticcode_gid
                    }
                    var url = 'api/AgrTrnAppCreditUnderWriting/UpdateGeneticCode';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }

                    });

                    $modalInstance.close('closed');

                }
            }
        }

        $scope.geneticcode_delete = function (creditgeneticcode_gid) {
            var params = {
                creditgeneticcode_gid: creditgeneticcode_gid
            }
            var url = 'api/AgrTrnAppCreditUnderWriting/DeleteGeneticCode';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    activate();
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    activate();
                }


            });
        }

        $scope.Back = function () {
            if (lspage == "myapp") {
                $location.url('app/AgrTrnStartCreditUnderwriting?application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else if (lspage == "CreditApproval") {
                $location.url('app/AgrTrnStartCreditUnderwriting?application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else if (lspage == "PendingCADReview") {
                $location.url('app/AgrTrnCADApplicationEdit?application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else if (lspage == "CADApplicationEdit") {
                $location.url('app/AgrTrnCADApplicationEdit?application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else if (lspage == "CADAcceptanceCustomers") {
                $location.url('app/AgrTrnCADApplicationEdit?application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else {

            }

        }

        $scope.company_deferraldocchecklist = function () {
            $location.url('app/AgrTrnDocumentCheckList?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_covenantdocchecklist = function () {
            $location.url('app/AgrTrnCreditAddCovenantCheckList?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_economicadd = function () {
            $location.url('app/AgrTrnCreditEconomicCapitalAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_geneticadd = function () {
            $location.url('app/AgrTrnCreditCompanyDtlAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_PSLdata = function () {
            $location.url('app/AgrTrnCreditPSLDataFlaggingAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_supplier = function () {
            $location.url('app/AgrTrnCreditSuppliersDtlAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_buyer = function () {
            $location.url('app/AgrTrnCreditBuyerDtlAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_bankaccount = function () {
            $location.url('app/AgrTrnCreditBankAccountDtlAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_existingbankaccount = function () {
            $location.url('app/AgrTrnCreditExistingBankDtlAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_repayment = function () {
            $location.url('app/AgrTrnCreditRepaymentDtlAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_observation = function () {
            $location.url('app/AgrTrnCreditObservationAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }
        $scope.tan_verification = function () {
            $location.url('app/AgrTrnCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=TAN' + '&lspage=' + lspage);
        }
        $scope.companyllpno_vertification = function () {
            $location.url('app/AgrTrnCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=LLPNO' + '&lspage=' + lspage);
        }
        $scope.mcasign_vertification = function () {
            $location.url('app/AgrTrnCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=MCASIGNATURE' + '&lspage=' + lspage);
        }
        $scope.iecdetailed_vertification = function () {
            $location.url('app/AgrTrnCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=IECDETAILED' + '&lspage=' + lspage);
        }
        $scope.fssai_vertification = function () {
            $location.url('app/AgrTrnCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=FSSAI' + '&lspage=' + lspage);
        }
        $scope.fda_vertification = function () {
            $location.url('app/AgrTrnCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=FDA' + '&lspage=' + lspage);
        }
        $scope.gst_verification = function () {
            $location.url('app/AgrTrnCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=GST' + '&lspage=' + lspage);
        }
        $scope.lpgid_verification = function () {
            $location.url('app/AgrTrnCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=LPGID' + '&lspage=' + lspage);
        }
        $scope.shop_vertification = function () {
            $location.url('app/AgrTrnCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=SHOP' + '&lspage=' + lspage);
        }
        $scope.rcauthadv_vertification = function () {
            $location.url('app/AgrTrnCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=RCAUTHADV' + '&lspage=' + lspage);
        }
        $scope.rcsearch_vertification = function () {
            $location.url('app/AgrTrnCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=RCSEARCH' + '&lspage=' + lspage);
        }
        $scope.propertytax_vertification = function () {
            $location.url('app/AgrTrnCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=PROPERTYTAX' + '&lspage=' + lspage);
        }
        $scope.probe42_api = function () {
            $location.url('app/AgrTrnCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=PROBEAPI' + '&lspage=' + lspage);
        }
        $scope.company_bankstatement = function () {
            $location.url('app/AgrTrnCreditBankStatementAnalysisAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }
        $scope.company_fsa = function () {
            $location.url('app/AgrTrnCreditFsaDetailAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }
        $scope.institution_bureauadd = function () {
            $location.url('app/AgrTrnCreditInstitutionDtlAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }
        $scope.crimecheck_record = function () {
            $location.url('app/AgrTrnCompanyCrimeCheckRecordAPI?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }
        $scope.crimecheckreport_realtime = function () {
            $location.url('app/AgrTrnCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=CRIMECHECKREPORTREALTIME' + '&lspage=' + lspage);    
        }

    }
})();
