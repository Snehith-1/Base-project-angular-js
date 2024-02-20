﻿(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrTrnSuprCreditIndividualDtlAddController', AgrTrnSuprCreditIndividualDtlAddController);

    AgrTrnSuprCreditIndividualDtlAddController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce', 'DownloaddocumentService','cmnfunctionService'];

    function AgrTrnSuprCreditIndividualDtlAddController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrTrnSuprCreditIndividualDtlAddController';
        $scope.contact_gid = $location.search().contact_gid;
        var contact_gid = $scope.contact_gid;
        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;
       /*  lockUI(); */
        activate();
        function activate() {

            var url = 'api/AgrMstSuprApplicationAdd/GetIndividualBureauTempClear';
            SocketService.get(url).then(function (resp) {
            });

            var param = {
                contact_gid: contact_gid
            }
            var url = 'api/AgrMstSuprApplicationAdd/GetContactBureauList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.contactbureau_list = resp.data.contactbureau_list;
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
                
              vm.formats = ['dd-MM-yyyy'];
              vm.format = vm.formats[0];
              vm.dateOptions = {
                  formatYear: 'yy',
                  startingDay: 1
              };

              var url = 'api/AgrMstSuprApplication360/BureauNameList';
              SocketService.get(url).then(function (resp) {
                  $scope.bureau_list = resp.data.bureauname_list;
              });


              $scope.reportgeneration_success = false;
              $scope.reportgeneration_failure = false;

        }

        

        $scope.bureauname_change = function () {

            var bureauname_name = $('#BureauName :selected').text();
            if (bureauname_name == 'High Mark') {
                $scope.reportbureauselected = true;
            } else {
                $scope.reportbureauselected = false;
            }
        }; 

        $scope.generateDetails = function () {
            $scope.html_content = '';

            var bureauname_name = $('#BureauName :selected').text();
            if (bureauname_name == 'TransUnion') {
                
                var params = {
                    contact_gid: contact_gid
                }
                var url = 'api/AgrBureauAPI/GetTransUnionConsumerCreditInfo';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {

                    if (resp.data.status == true) {
                        $scope.bureauscore_disabled = true;                       
                        $scope.txtbureau_score = resp.data.bureau_score;                       
                        
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $scope.reportbureauselected = false;

                        var param = {
                            contact2bureau_gid: $scope.contact2bureau_gid
                        }
                        var url = 'api/AgrMstSuprApplicationEdit/CICUploadIndividualDocList';
                        SocketService.getparams(url, param).then(function (resp) {
                            $scope.cicuploaddoc_list = resp.data.cicuploaddoc_list;
                        });

                        unlockUI();
                    }
                    else {
                        $scope.bureauscore_disabled = false;                       

                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $scope.reportbureauselected = false;
                        unlockUI();
                    }
                });
                


            }
            else if (bureauname_name == 'High Mark')
            {
                var params = {
                    contact_gid: contact_gid
                }
                $scope.html_content = '';
                var url = 'api/AgrBureauAPI/GetHighmarkCreditInfo';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {

                    if (resp.data.status == true) {
                        $scope.bureauscore_disabled = true;
                        $scope.bureauresponse_disabled = true;
                        $scope.txtbureau_score = resp.data.bureau_score;
                        $scope.txtbureau_response = resp.data.bureau_response;
                       
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });

                        $scope.reportbureauselected = false;
                        var param = {
                            contact2bureau_gid: $scope.contact2bureau_gid
                        };
                        var url = 'api/AgrMstSuprApplicationEdit/CICUploadIndividualDocList';
                        SocketService.getparams(url, param).then(function (resp) {
                            $scope.cicuploaddoc_list = resp.data.cicuploaddoc_list;
                        });                       
                        unlockUI();
                    }
                    else {
                        $scope.bureauscore_disabled = false;
                        $scope.bureauresponse_disabled = false;

                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $scope.reportbureauselected = false;
                        unlockUI();
                    }
                });
            }
            var paramstemp = {
                contact2bureau_gid: $scope.contact2bureau_gid
            }
            var url = 'api/AgrMstSuprApplicationEdit/CICUploadIndividualDocList';
            SocketService.getparams(url, paramstemp).then(function (resp) {
                $scope.cicuploaddoc_list = resp.data.cicuploaddoc_list;
            });

        };

        $scope.report_View = function (tmpcicdocument_gid) {
            var bureauname_name = $('#BureauName :selected').text();

            if (bureauname_name == 'TransUnion') {
                var tmpcicdocument_gid = tmpcicdocument_gid;
                localStorage.setItem('tmpcicdocument_gid', tmpcicdocument_gid);
                var URL = location.protocol + "//" + location.hostname + "/v1/#/app/AgrSuprTransUnionReport";
                window.open(URL, '_blank');
            } else if (bureauname_name == 'High Mark') {
                var tmpcicdocument_gid = tmpcicdocument_gid;
                localStorage.setItem('tmpcicdocument_gid', tmpcicdocument_gid);
                var URL = location.protocol + "//" + location.hostname + "/v1/#/app/AgrSuprHighmarkReport";
                window.open(URL, '_blank');
            }

            
        };

        $scope.add_bureauDtl = function () {
            if (($scope.cboBureauName == undefined) || ($scope.txtbureauscore_date == undefined) || ($scope.txtbureau_score == undefined) 
            || ($scope.txtobservations == undefined) || ($scope.txtbureau_response == undefined)) {
                Notify.alert('Enter All Mandatory Fields', 'warning');
            }
            else {
                var params = {                   
                    contact_gid: $scope.contact_gid,                   
                    bureauname_gid: $scope.cboBureauName.bureauname_gid,
                    bureauname_name: $scope.cboBureauName.bureauname_name,
                    bureau_score: $scope.txtbureau_score,
                    bureauscore_date: $scope.txtbureauscore_date,
                    observations: $scope.txtobservations,
                    bureau_response: $scope.txtbureau_response,                    
                }
                var url = 'api/AgrMstSuprApplicationAdd/PostCICUploadIndividual';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {                        
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
                    var param = {
                        contact_gid: $scope.contact_gid
                    };
                    var url = 'api/AgrMstSuprApplicationAdd/GetContactBureauList';
                    SocketService.getparams(url, param).then(function (resp) {
                        $scope.contactbureau_list = resp.data.contactbureau_list;
                    });

                    $scope.cboBureauName = '';
                    $scope.txtbureauscore_date = '';
                    $scope.txtobservations = '';
                    $scope.txtbureau_response = '';
                    $scope.txtbureau_score = '';
                    $scope.cicuploaddoc_list = '';
                    $scope.txtreportdoc_name = '';
                    $scope.bureauscore_disabled = false;
                    $scope.bureauresponse_disabled = false;

                });
            }
      }

    
        $scope.bureau_delete = function (contact2bureau_gid) {
            var params = {
                contact2bureau_gid: contact2bureau_gid
            }
            var url = 'api/AgrMstApplicationAdd/DeleteContactBureau';
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

        $scope.highriskalert_view = function (contact2bureau_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/viewHighRiskAlert.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                   {
                    contact2bureau_gid: contact2bureau_gid
                   }
                var url = 'api/AgrBureauAPI/GetHighRiskAlertDetails';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.noofph_rep3mon = resp.data.noofph_rep3mon;
                    $scope.noofad_rep3mon = resp.data.noofad_rep3mon;
                    $scope.noofdistph_rep3mon = resp.data.noofdistph_rep3mon;
                    $scope.noofdistad_rep3mon = resp.data.noofdistad_rep3mon;

                    $scope.noofdistid_rep3mon = resp.data.noofdistid_rep3mon;
                    $scope.noofdistpin_3mon = resp.data.noofdistpin_3mon;
                    $scope.enqdifflend_30days = resp.data.enqdifflend_30days;
                    $scope.newloanopened_30days = resp.data.newloanopened_30days;

                    $scope.distunsecenq_3mon = resp.data.distunsecenq_3mon;
                    $scope.ranksegment_hml = resp.data.ranksegment_hml;

                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }

  

      $scope.CICDocumentUpload = function (val, val1, name) {
        
        var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "documentformatonly");

            if (IsValidExtension == false) {
                Notify.alert("File format is not supported..!", {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                return false;
            }
          var item = {
              file: val[0]
          };
          var frm = new FormData();
          frm.append('file', item.file);

          frm.append('document_name', $scope.documentname);
          frm.append('project_flag', "documentformatonly");
          $scope.uploadfrm = frm;
          if ($scope.uploadfrm != undefined) {
              lockUI();
              var url = 'api/AgrMstSuprApplicationAdd/CICIndividualDocumentUpload';
              SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                  $scope.upload_list = resp.data.upload_list;
                  unlockUI();
                  if (resp.data.status == true) {
                      Notify.alert(resp.data.message, {
                          status: 'success',
                          pos: 'top-center',
                          timeout: 3000
                      });
                      $("#file").val('');
                      $scope.txtcic_document = "";
                      $scope.uploadfrm = undefined;
                  }
                  else {
                      Notify.alert(resp.data.message, {
                          status: 'info',
                          pos: 'top-center',
                          timeout: 3000
                      });
                  }
                  var params = {
                      contact2bureau_gid: $scope.contact2bureau_gid
                  };
                  var url = 'api/AgrMstSuprApplicationEdit/CICUploadIndividualDocList';
                  SocketService.getparams(url, params).then(function (resp) {
                      $scope.cicuploaddoc_list = resp.data.cicuploaddoc_list;
                  });
                  unlockUI();
              });
          }
          else {
              alert('Please select a file.')
          }
      }

      $scope.uploaddocumentcancel = function (tmpcicdocument_gid) {
          lockUI();
          var params = {
              tmpcicdocument_gid: tmpcicdocument_gid
          }
          var url = 'api/AgrMstSuprApplicationEdit/CICUploadIndividualDocDelete';
          SocketService.getparams(url, params).then(function (resp) {
              if (resp.data.status == true) {

                  Notify.alert(resp.data.message, {
                      status: 'success',
                      pos: 'top-center',
                      timeout: 3000
                  });
              }
              else {
                  Notify.alert(resp.data.message, {
                      status: 'danger',
                      pos: 'top-center',
                      timeout: 3000
                  });

              }
              var params = {
                  contact2bureau_gid: $scope.contact2bureau_gid
              };
              var url = 'api/AgrMstSuprApplicationEdit/CICUploadIndividualDocList';
              SocketService.getparams(url, params).then(function (resp) {
                  $scope.cicuploaddoc_list = resp.data.cicuploaddoc_list;
              });

              unlockUI();
          });
      }

    $scope.downloads = function (val1, val2) {
        //var phyPath = val1;
        //var relPath = phyPath.split("StoryboardAPI");
        //var relpath1 = relPath[1].replace("\\", "/");
        //var hosts = window.location.host;
        //var prefix = location.protocol + "//";
        //var str = prefix.concat(hosts, relpath1);
        //var link = document.createElement("a");
        //link.download = val2;
        //var uri = str;
        //link.href = uri;
        //link.click();

        DownloaddocumentService.Downloaddocument(val1, val2);
    }

   
        $scope.Back = function () {
            $location.url('app/AgrTrnSuprStartCreditUnderwriting?application_gid=' + application_gid);
        }

        $scope.individual_docchecklist = function () {
            $location.url('app/AgrTrnSuprIndividualDocCheckList?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage);
        }

        $scope.individual_covenantdocchecklist = function () {
            $location.url('app/AgrTrnSuprIndividualCovenantDocChecklist?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage);
        }

        $scope.company_bankstatement = function () {
            $location.url('app/AgrTrnSuprCreditIndividualBankStatementAnalysisAdd?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage);
        }

        $scope.individual_bureauadd = function () {
            $location.url('app/AgrTrnSuprCreditIndividualDtlAdd?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage);
        }

        $scope.individual_bankaccount = function () {
            $location.url('app/AgrTrnSuprCreditIndividualBankAcctAdd?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage);
        }

        $scope.individual_existingbankaccount = function () {
            $location.url('app/AgrTrnSuprCreditIndividualExistingBankAdd?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage);
        }

        $scope.individual_PSLdata = function () {
            $location.url('app/AgrTrnSuprCreditIndividualPSLDataFlagAdd?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage);
        }       

        $scope.individual_repayment = function () {
            $location.url('app/AgrTrnSuprCreditIndividualRepaymentAdd?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage);
        }

        $scope.individual_observation = function () {
            $location.url('app/AgrTrnSuprCreditIndividualObservationAdd?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage);
        }

        $scope.bureau_edit = function (contact2bureau_gid) {
            $location.url('app/AgrTrnSuprCreditIndividualBureauEdit?lscontact2bureau_gid=' + contact2bureau_gid + '&lscontact_gid=' + $scope.contact_gid + '&lsapplication_gid=' + application_gid + '&lspage=' + lspage);
        }

        $scope.bureau_view = function (contact2bureau_gid) {
            $location.url('app/AgrTrnSuprCreditIndividualBureauView?lscontact2bureau_gid=' + contact2bureau_gid + '&lscontact_gid=' + $scope.contact_gid + '&lsapplication_gid=' + application_gid + '&lspage=' + lspage);
        }

        //KYC API
        $scope.iecdetailed_vertification = function () {
            $location.url('app/AgrTrnSuprCreditIndividualAPI?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lsapi_name=IECDETAILED' + '&lspage=' + lspage);
        }
        $scope.fssai_vertification = function () {
            $location.url('app/AgrTrnSuprCreditIndividualAPI?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lsapi_name=FSSAI' + '&lspage=' + lspage);
        }
        $scope.fda_vertification = function () {
            $location.url('app/AgrTrnSuprCreditIndividualAPI?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lsapi_name=FDA' + '&lspage=' + lspage);
        }
        $scope.lpgid_vertification = function () {
            $location.url('app/AgrTrnSuprCreditIndividualAPI?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lsapi_name=LPGID' + '&lspage=' + lspage);
        }
        $scope.shop_vertification = function () {
            $location.url('app/AgrTrnSuprCreditIndividualAPI?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lsapi_name=SHOP' + '&lspage=' + lspage);
        }
        $scope.rcauthadv_vertification = function () {
            $location.url('app/AgrTrnSuprCreditIndividualAPI?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lsapi_name=RCAUTHADV' + '&lspage=' + lspage);
        }
        $scope.rcsearch_vertification = function () {
            $location.url('app/AgrTrnSuprCreditIndividualAPI?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lsapi_name=RCSEARCH' + '&lspage=' + lspage);
        }
        $scope.propertytax_vertification = function () {
            $location.url('app/AgrTrnSuprCreditIndividualAPI?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lsapi_name=PROPERTYTAX' + '&lspage=' + lspage);
        }
        $scope.crimecheck_record = function () {
            $location.url('app/AgrTrnSuprCreditCrimeCheckRecordAPI?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lsapi_name=PROPERTYTAX' + '&lspage=' + lspage);
        }
        $scope.crimecheckreport_realtime = function () {
            $location.url('app/AgrTrnSuprCreditIndividualAPI?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lsapi_name=CRIMECHECKREPORTREALTIME' + '&lspage=' + lspage);
        }

        $scope.downloadall = function () {
            for (var i = 0; i < $scope.cicuploaddoc_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.cicuploaddoc_list[i].document_path, $scope.cicuploaddoc_list[i].document_name);
            }
        }
       
    }
})();