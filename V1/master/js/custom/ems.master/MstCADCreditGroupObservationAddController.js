(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCADCreditGroupObservationAddController', MstCADCreditGroupObservationAddController);

        MstCADCreditGroupObservationAddController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','$sce'];

    function MstCADCreditGroupObservationAddController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCADCreditGroupObservationAddController';
        $scope.group_gid = $location.search().group_gid;
        var group_gid = $scope.group_gid;
        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;
        lockUI();
        activate();
        
        function activate() {
             var params = {
                credit_gid: group_gid
             }
             var url = 'api/MstAppCreditUnderWriting/GetCrepolicy';
             SocketService.get(url).then(function (resp) {
                 unlockUI();
                 $scope.creditpolicy_list = resp.data.CreditObservation_list;
             });

             var url = 'api/MstCADCreditAction/GetCreditObservationList';
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
                credit_gid: group_gid,
                applicant_type: 'Group'
            }

            var url = 'api/MstCADCreditAction/GetCreditOperationsView';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtgroup_name = resp.data.group_name;
                $scope.txtgroup_type = resp.data.group_type;
            }); 

        }

        $scope.add_creditobservation = function () {
            if (($scope.cboCreditObservations == undefined) || ($scope.cboCreditObservations == '') || ($scope.rdbCompliednonComplied == undefined) || ($scope.rdbCompliednonComplied == '' ) || ($scope.txtObservations == undefined) || ($scope.txtObservations == '' )) {
              Notify.alert('Enter All Mandatory Fields','warning');
          }
          else {
             var params = {
                 application_gid: application_gid,
                 credit_gid: group_gid,
                 applicant_type:'Group',
                 creditpolicy_gid: $scope.cboCreditObservations.creditpolicy_gid,
                 credit_policy: $scope.cboCreditObservations.credit_policy,
                 complied_status: $scope.rdbCompliednonComplied,
                 observation : $scope.txtObservations,
                
             }
             var url = 'api/MstCADCreditAction/PostCreditObservation';
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
                lockUI();
                var url = 'api/MstAppCreditUnderWriting/GetCrepolicy';
                SocketService.get(url).then(function (resp) {
                    unlockUI();
                    $scope.creditpolicy_list = resp.data.CreditObservation_list;
                });

                var params = {
                    creditobservation_gid: creditobservation_gid
                }
                lockUI();
                var url = 'api/MstCADCreditAction/EditGetCreditObservation';
                   unlockUI();
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
                    var url = 'api/MstCADCreditAction/UpdateCreditObservation';
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
            credit_gid: group_gid
        }
        var url = 'api/MstCADCreditAction/DeleteCreditObservation';
        lockUI();
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
            if (lspage == "StartCreditUnderwriting") {
                $location.url('app/MstStartCreditUnderwriting?application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else if (lspage == "PendingCADReview") {
                $location.url('app/MstCADApplicationEdit?application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else if (lspage == "CADApplicationEdit") {
                $location.url('app/MstCADApplicationEdit?application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else if (lspage == "CADAcceptanceCustomers") {
                $location.url('app/MstCADApplicationEdit?application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else {

            }
        }

        $scope.group_addguarantee = function () {
            $location.url('app/MstCADGroupGuaranteeDtlAdd?application_gid=' + application_gid + '&group_gid=' + group_gid + '&lspage=' + lspage);
        }

        $scope.group_docchecklist = function () {
            $location.url('app/MstCADGroupDocCheckList?application_gid=' + application_gid + '&group_gid=' + group_gid + '&lspage=' + lspage);
        }

        $scope.group_covenantdocchecklist = function () {
            $location.url('app/MstCADGroupCovenantDocChecklist?application_gid=' + application_gid + '&group_gid=' + group_gid + '&lspage=' + lspage);
        }

        $scope.group_bureauadd = function () {
            $location.url('app/MstCADCreditGroupDtlAdd?application_gid=' + application_gid + '&group_gid=' + group_gid + '&lspage=' + lspage);
        }

        $scope.group_bankaccount = function () {
            $location.url('app/MstCADCreditGroupBankAcctAdd?application_gid=' + application_gid + '&group_gid=' + group_gid + '&lspage=' + lspage);
        }

        $scope.group_existingbankaccount = function () {
            $location.url('app/MstCADCreditGroupExistingBankAdd?application_gid=' + application_gid + '&group_gid=' + group_gid + '&lspage=' + lspage);
        }

        $scope.group_PSLdata = function () {
            $location.url('app/MstCADCreditGroupPSLDataFlagAdd?application_gid=' + application_gid + '&group_gid=' + group_gid + '&lspage=' + lspage);
        }       

        $scope.group_repayment = function () {
            $location.url('app/MstCADCreditGroupRepaymentAdd?application_gid=' + application_gid + '&group_gid=' + group_gid + '&lspage=' + lspage);
        }

        $scope.group_observation = function () {
            $location.url('app/MstCADCreditGroupObservationAdd?application_gid=' + application_gid + '&group_gid=' + group_gid + '&lspage=' + lspage);
        }
        
        $scope.company_bankstatement = function () {
            $location.url('app/MstCADCreditGroupBankStatementAnalysisAdd?application_gid=' + application_gid + '&group_gid=' + group_gid + '&lspage=' + lspage);
        }
        
    }
})();