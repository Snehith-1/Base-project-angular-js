(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCADCreditObservationAddController', MstCADCreditObservationAddController);

        MstCADCreditObservationAddController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','$sce'];

    function MstCADCreditObservationAddController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCADCreditObservationAddController';
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
            var url = 'api/MstAppCreditUnderWriting/GetCrepolicy';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.creditpolicy_list = resp.data.CreditObservation_list;
            });
            var url = 'api/MstCADCreditAction/GetCreditObservationList';
              SocketService.getparams(url,params).then(function (resp) {
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
                credit_gid: institution_gid,
                applicant_type: 'Institution'
                }

            var url = 'api/MstCADCreditAction/GetCreditOperationsView';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtinstitution_name = resp.data.company_name;
                $scope.txtstakeholder_type = resp.data.stakeholder_type;
            }); 

        }

        $scope.add_creditobservation = function () {
            if (($scope.cboCreditObservations == undefined) || ($scope.cboCreditObservations == '') || ($scope.rdbCompliednonComplied == undefined) || ($scope.rdbCompliednonComplied == '') || ($scope.txtObservations == undefined)  || ($scope.txtObservations == '' ) )
           {
              Notify.alert('Enter All Mandatory Fields','warning');
          }
          else {
             var params = {
                 application_gid: application_gid,
                 credit_gid: institution_gid,
                 applicant_type:'Institution',
                 creditpolicy_gid: $scope.cboCreditObservations.creditpolicy_gid,
                 credit_policy: $scope.cboCreditObservations.credit_policy,
                 complied_status : $scope.rdbCompliednonComplied,
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

            var url = 'api/MstAppCreditUnderWriting/GetCrepolicy';
           lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.creditpolicy_list = resp.data.CreditObservation_list;
            });
            
             var params = {
                 creditobservation_gid: creditobservation_gid
            }
             var url = 'api/MstCADCreditAction/EditGetCreditObservation';
             lockUI();
             SocketService.getparams(url, params).then(function (resp) {
                 unlockUI();
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
                       applicant_type:'Institution',
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
            credit_gid: institution_gid
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

        $scope.company_addguarantee = function () {
            $location.url('app/MstCADGuaranteeDetailAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_deferraldocchecklist = function () {
            $location.url('app/MstCADDocumentCheckList?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_covenantdocchecklist = function () {
            $location.url('app/MstCADCreditAddCovenantCheckList?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_economicadd = function () {
            $location.url('app/MstCADCreditEconomicCapitalAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_geneticadd = function () {
            $location.url('app/MstCADCreditCompanyDtlAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_PSLdata = function () {
            $location.url('app/MstCADCreditPSLDataFlaggingAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_supplier = function () {
            $location.url('app/MstCADCreditSuppliersDtlAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_buyer = function () {
            $location.url('app/MstCADCreditBuyerDtlAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_bankaccount = function () {
            $location.url('app/MstCADCreditBankAccountDtlAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_existingbankaccount = function () {
            $location.url('app/MstCADCreditExistingBankDtlAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_repayment = function () {
            $location.url('app/MstCADCreditRepaymentDtlAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_observation = function () {
            $location.url('app/MstCADCreditObservationAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }
        $scope.tan_verification = function () {
            $location.url('app/MstCADCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=TAN' + '&lspage=' + lspage);
        }
        $scope.companyllpno_vertification = function () {
            $location.url('app/MstCADCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=LLPNO' + '&lspage=' + lspage);
        }
        $scope.mcasign_vertification = function () {
            $location.url('app/MstCADCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=MCASIGNATURE' + '&lspage=' + lspage);
        }
        $scope.iecdetailed_vertification = function () {
            $location.url('app/MstCADCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=IECDETAILED' + '&lspage=' + lspage);
        }
        $scope.fssai_vertification = function () {
            $location.url('app/MstCADCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=FSSAI' + '&lspage=' + lspage);
        }
        $scope.fda_vertification = function () {
            $location.url('app/MstCADCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=FDA' + '&lspage=' + lspage);
        }
        $scope.gst_verification = function () {
            $location.url('app/MstCADCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=GST' + '&lspage=' + lspage);
        }
        $scope.lpgid_verification = function () {
            $location.url('app/MstCADCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=LPGID' + '&lspage=' + lspage);
        }
        $scope.shop_vertification = function () {
            $location.url('app/MstCADCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=SHOP' + '&lspage=' + lspage);
        }
        $scope.rcauthadv_vertification = function () {
            $location.url('app/MstCADCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=RCAUTHADV' + '&lspage=' + lspage);
        }
        $scope.rcsearch_vertification = function () {
            $location.url('app/MstCADCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=RCSEARCH' + '&lspage=' + lspage);
        }
        $scope.propertytax_vertification = function () {
            $location.url('app/MstCADCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=PROPERTYTAX' + '&lspage=' + lspage);
        }
        $scope.company_bankstatement = function () {
            $location.url('app/MstCADCreditBankStatementAnalysisAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }
        $scope.company_fsa = function () {
            $location.url('app/MstCADCreditFsaDetailAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }
        $scope.institution_bureauadd = function () {
            $location.url('app/MstCADCreditInstitutionDtlAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }
        $scope.crimecheck_record = function () {
            $location.url('app/MstCADCompanyCrimeCheckRecordAPI?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }
        $scope.crimecheckreport_realtime = function () {
            $location.url('app/MstCADCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=CRIMECHECKREPORTREALTIME' + '&lspage=' + lspage);    
        }
    }
})();
