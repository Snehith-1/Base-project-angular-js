(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrTrnCreditIndividualObservationAddController', AgrTrnCreditIndividualObservationAddController);

        AgrTrnCreditIndividualObservationAddController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','$sce'];

    function AgrTrnCreditIndividualObservationAddController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrTrnCreditIndividualObservationAddController';
        $scope.contact_gid = $location.search().contact_gid;
        var contact_gid = $scope.contact_gid;
        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;
       
        activate();
        lockUI();
        function activate() {

             var params = {
                credit_gid: contact_gid
             }
             var url = 'api/AgrTrnAppCreditUnderWriting/GetCrepolicy';
             SocketService.get(url).then(function (resp) {
                 $scope.creditpolicy_list = resp.data.CreditObservation_list;
             });
              
             var url = 'api/AgrTrnAppCreditUnderWriting/GetCreditObservationList';
             SocketService.getparams(url, params).then(function (resp) {
                 unlockUI();
                 $scope.CreditObservation_list = resp.data.CreditObservation_list;
             });
              vm.submitted = false;
              vm.validateInput = function(name, type) {
                var input = vm.formValidate[name];
                return (input.$dirty || vm.submitted) && input.$error[type];
              };
    
              // Submit form
              vm.submitForm = function() {
                vm.submitted = true;
                if (vm.formValidate.$valid) {
                } else {
                  return false;
                }
              };

               // Calender Popup... //

            vm.calender1 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.open1 = true;
              };  
  
              vm.calender2 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.open2 = true;
              }; 

              vm.calender3 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.open3 = true;
              }; 
              
              vm.formats = ['dd-MM-yyyy'];
              vm.format = vm.formats[0];
              vm.dateOptions = {
                  formatYear: 'yy',
                  startingDay: 1
              };

              var params = {
                credit_gid: contact_gid,
                applicant_type: 'Individual'
            }

            var url = 'api/AgrTrnAppCreditUnderWriting/GetCreditOperationsView';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtindividual_name = resp.data.individual_name;
                $scope.txtstakeholder_type = resp.data.stakeholder_type;
            }); 

            var params = {
                credit_gid: contact_gid,
                applicant_type: 'Individual'
            }

            var url = 'api/AgrTrnAppCreditUnderWriting/GetCreditOperationsView';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtindividual_name = resp.data.individual_name;
                $scope.txtstakeholder_type = resp.data.stakeholder_type;
            }); 

        }

        $scope.add_creditobservation = function () {
            if (($scope.cboCreditObservations == undefined) || ($scope.cboCreditObservations == '') || ($scope.rdbCompliednonComplied == undefined) || ($scope.rdbCompliednonComplied == '' ) || ($scope.txtObservations == undefined) || ($scope.txtObservations == '' ) )
           {
              Notify.alert('Enter All Mandatory Fields','warning');
          }
          else {
             var params = {
                 application_gid: application_gid,
                 credit_gid: contact_gid,
                 applicant_type:'Individual',
                 creditpolicy_gid: $scope.cboCreditObservations.creditpolicy_gid,
                 credit_policy: $scope.cboCreditObservations.credit_policy,
                 complied_status: $scope.rdbCompliednonComplied,
                 observation: $scope.txtObservations,
                
             }
             var url = 'api/AgrTrnAppCreditUnderWriting/PostCreditObservation';
             lockUI();
             SocketService.post(url, params).then(function (resp) {
                 unlockUI();
                 if (resp.data.status == true) {
                     $scope.CreditObservation_list = resp.data.CreditObservation_list;
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
                 }
                 $scope.cboCreditObservations = '';
                 $scope.rdbCompliednonComplied = '';
                 $scope.txtObservations = '';
             });
          }
      }

        $scope.creditobservation_edit = function (creditobservation_gid) {
            var creditobservation_gid = creditobservation_gid;
            var modalInstance = $modal.open({
                templateUrl: '/editcreditobservation.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var url = 'api/AgrTrnAppCreditUnderWriting/GetCrepolicy';
                SocketService.get(url).then(function (resp) {
                    $scope.creditpolicy_list = resp.data.CreditObservation_list;
                });

                var params = {
                    creditobservation_gid: creditobservation_gid
                }
                var url = 'api/AgrTrnAppCreditUnderWriting/EditGetCreditObservation';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.cboeditCreditObservations = resp.data.creditpolicy_gid;
                    $scope.rdbeditCompliednonComplied = resp.data.complied_status;
                    $scope.txtedit_Observations = resp.data.observation;
                });
                console.log(params)
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update_creditobservation = function () {
                    var creditobservation_name = $('#credit_policy :selected').text();

                    var params = {
                        creditobservation_gid: creditobservation_gid,
                        applicant_type: 'Institution',
                        creditpolicy_gid: $scope.cboeditCreditObservations,
                        credit_policy: creditobservation_name,
                        complied_status: $scope.rdbeditCompliednonComplied,
                        observation: $scope.txtedit_Observations

                    }
                    var url = 'api/AgrTrnAppCreditUnderWriting/UpdateCreditObservation';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            activate();
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }

                    });

                    $modalInstance.close('closed');

                }
            }
        }

        $scope.creditobservation_delete = function (creditobservation_gid) {
        var params = {
            creditobservation_gid: creditobservation_gid,
            credit_gid: contact_gid
        }
        var url = 'api/AgrTrnAppCreditUnderWriting/DeleteCreditObservation';
        SocketService.getparams(url, params).then(function (resp) {
            unlockUI();
            if (resp.data.status == true) {
                $scope.CreditObservation_list = resp.data.CreditObservation_list;
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

        $scope.individual_docchecklist = function () {
            $location.url('app/AgrTrnIndividualDocCheckList?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage);
        }

        $scope.individual_covenantdocchecklist = function () {
            $location.url('app/AgrTrnIndividualCovenantDocChecklist?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage);
        }

        $scope.individual_bureauadd = function () {
            $location.url('app/AgrTrnCreditIndividualDtlAdd?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage);
        }

        $scope.individual_bankaccount = function () {
            $location.url('app/AgrTrnCreditIndividualBankAcctAdd?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage);
        }

        $scope.individual_existingbankaccount = function () {
            $location.url('app/AgrTrnCreditIndividualExistingBankAdd?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage);
        }

        $scope.individual_PSLdata = function () {
            $location.url('app/AgrTrnCreditIndividualPSLDataFlagAdd?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage);
        }       

        $scope.individual_repayment = function () {
            $location.url('app/AgrTrnCreditIndividualRepaymentAdd?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage);
        }

        $scope.individual_observation = function () {
            $location.url('app/AgrTrnCreditIndividualObservationAdd?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage);
        }
        
        //KYC API
        $scope.iecdetailed_vertification = function () {
            $location.url('app/AgrTrnCreditIndividualAPI?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lsapi_name=IECDETAILED' + '&lspage=' + lspage);
        }
        $scope.fssai_vertification = function () {
            $location.url('app/AgrTrnCreditIndividualAPI?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lsapi_name=FSSAI' + '&lspage=' + lspage);
        }
        $scope.fda_vertification = function () {
            $location.url('app/AgrTrnCreditIndividualAPI?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lsapi_name=FDA' + '&lspage=' + lspage);
        }
        $scope.lpgid_vertification = function () {
            $location.url('app/AgrTrnCreditIndividualAPI?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lsapi_name=LPGID' + '&lspage=' + lspage);
        }
        $scope.shop_vertification = function () {
            $location.url('app/AgrTrnCreditIndividualAPI?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lsapi_name=SHOP' + '&lspage=' + lspage);
        }
        $scope.rcauthadv_vertification = function () {
            $location.url('app/AgrTrnCreditIndividualAPI?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lsapi_name=RCAUTHADV' + '&lspage=' + lspage);
        }
        $scope.rcsearch_vertification = function () {
            $location.url('app/AgrTrnCreditIndividualAPI?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lsapi_name=RCSEARCH' + '&lspage=' + lspage);
        }
        $scope.propertytax_vertification = function () {
            $location.url('app/AgrTrnCreditIndividualAPI?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lsapi_name=PROPERTYTAX' + '&lspage=' + lspage);
        }
        $scope.company_bankstatement = function () {
            $location.url('app/AgrTrnCreditIndividualBankStatementAnalysisAdd?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage);
        }
        $scope.crimecheck_record = function () {
            $location.url('app/AgrTrnCreditCrimeCheckRecordAPI?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lsapi_name=PROPERTYTAX' + '&lspage=' + lspage);
        }
        $scope.crimecheckreport_realtime = function () {
            $location.url('app/AgrTrnCreditIndividualAPI?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lsapi_name=CRIMECHECKREPORTREALTIME' + '&lspage=' + lspage);
        }
    }
})();
