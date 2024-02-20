(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrTrnCreditGroupDtlAddController', AgrTrnCreditGroupDtlAddController);

        AgrTrnCreditGroupDtlAddController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','$sce','cmnfunctionService'];

    function AgrTrnCreditGroupDtlAddController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrTrnCreditGroupDtlAddController';
        $scope.opsgroup_gid = $location.search().opsgroup_gid;
        var opsgroup_gid = $scope.opsgroup_gid;
        $scope.opsapplication_gid = $location.search().opsapplication_gid;
        var opsapplication_gid = $scope.opsapplication_gid;
        /* lockUI(); */
        activate();
        function activate() {

           /*  var params = {
                opsgroup_gid: opsgroup_gid
             }
             var url = 'api/AgrMstApplicationAdd/GetGeneticCodeList';
             SocketService.get(url).then(function (resp) {
                 unlockUI();
                $scope.genetic_list = resp.data.genetic_list;
              });        
              
              var url = 'api/AgrTrnAppCreditUnderWriting/GetGeneticCodeList';
              SocketService.getparams(url,params).then(function (resp) {
                  unlockUI();
                 $scope.mstgeneticcode_list = resp.data.mstcuwgeneticcode_list;
               });    */     
    
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
        }

        $scope.add_bureauDtl = function () {
            if (($scope.cboBureauName == undefined) || ($scope.txtBureau_Score == undefined) || ($scope.reportdoc_name == undefined)) {
                Notify.alert('Enter All Mandatory Fields','warning');
            }
            else {
               var params = {
                   opsapplication_gid: opsapplication_gid,
                   opscontact_gid: opscontact_gid,
                   bureau_name: $scope.cboBureauName.bureau_name,
                   bureau_gid: $scope.cboBureauName.bureau_gid,
                   Bureau_Score: $scope.txtBureau_Score,
                   scoreas_on: $scope.txtscoreas_on,
                   Observation : $scope.txtObservation,
                   Bureau_Response : $scope.txtBureau_Response,
                   reportdoc_name : $scope.txtreportdoc_name
               }
                  var url = 'api/AgrTrnAppCreditUnderWriting/PostGeneticCode';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        $scope.mstgeneticcode_list = resp.data.mstcuwgeneticcode_list;
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
                    $scope.cboBureauName = '';
                    $scope.txtBureau_Score = '';
                    $scope.txtscoreas_on = '';
                    $scope.txtObservation = '';
                    $scope.txtBureau_Response ='';
                   
                }); 
            }
        }

        $scope.bureau_delete = function () {
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
    
        $scope.documentUpload = function (val) {

            
            var frm = new FormData();
            for (var i = 0; i < val.length; i++) {
                var item = {
                        name: val[i].name,
                        file: val[i]
                    };   
                    frm.append('fileupload', item.file);
                frm.append('file_name', item.name);
                frm.append('project_flag', "documentformatonly");
                    var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[i].name, "documentformatonly");
        
                                if (IsValidExtension == false) {
                                    Notify.alert("File format is not supported..!", {
                                        status: 'danger',
                                        pos: 'top-center',
                                        timeout: 3000
                                    });
                                    return false;
                                }
                }
    
                $scope.uploadfrm = frm;
                if ($scope.uploadfrm != undefined) {
                    lockUI();
                    var url = 'api/AgrMstApplicationAdd/InstitutionDocumentUpload';
                    SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
    
                        $scope.institutionupload_list = resp.data.institutionupload_list;
                        unlockUI();
    
                        $("#institutionfile").val('');
                        $scope.uploadfrm = undefined;
    
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
                        unlockUI();
                    });
                }
                else {
                    alert('Document is not Available..!');
                    return;
                }
            
        }
    
        $scope.downloads = function (val1, val2) {
            var phyPath = val1;
            var relPath = phyPath.split("StoryboardAPI");
            var relpath1 = relPath[1].replace("\\", "/");
            var hosts = window.location.host;
            var prefix = location.protocol + "//";
            var str = prefix.concat(hosts, relpath1);
            var link = document.createElement("a");
            link.download = val2;
            var uri = str;
            link.href = uri;
            link.click();
        }
    
        $scope.uploaddocumentcancel = function () {
           /*  lockUI();
            var params = {
                
            }
            var url = 'api/AgrMstApplicationAdd/InstitutionDocumentDelete';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.institutionupload_list = resp.data.institutionupload_list;
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
                unlockUI();
            }); */
        }
        $scope.Back = function () {
            $location.url('app/AgrTrnStartCreditUnderwriting?opsapplication_gid=' + opsapplication_gid);
        }

        $scope.group_bureauadd = function () {
            $location.url('app/AgrTrnCreditGroupDtlAdd?opsapplication_gid=' + opsapplication_gid + '&opsgroup_gid=' + opsgroup_gid);
        }

        $scope.group_bankaccount = function () {
            $location.url('app/AgrTrnCreditGroupBankAcctAdd?opsapplication_gid=' + opsapplication_gid + '&opsgroup_gid=' + opsgroup_gid);
        }

        $scope.group_existingbankaccount = function () {
            $location.url('app/AgrTrnCreditGroupExistingBankAdd?opsapplication_gid=' + opsapplication_gid + '&opsgroup_gid=' + opsgroup_gid);
        }

        $scope.group_PSLdata = function () {
            $location.url('app/AgrTrnCreditGroupPSLDataFlagAdd?opsapplication_gid=' + opsapplication_gid + '&opsgroup_gid=' + opsgroup_gid);
        }       

        $scope.group_repayment = function () {
            $location.url('app/AgrTrnCreditGroupRepaymentAdd?opsapplication_gid=' + opsapplication_gid + '&opsgroup_gid=' + opsgroup_gid);
        }

        $scope.group_observation = function () {
            $location.url('app/AgrTrnCreditGroupObservationAdd?opsapplication_gid=' + opsapplication_gid + '&opsgroup_gid=' + opsgroup_gid);
        }

        $scope.bureau_edit = function () {
            $location.url('app/AgrTrnCreditGroupBureauEdit?opsapplication_gid=' + opsapplication_gid + '&opsgroup_gid=' + opsgroup_gid);
        }

        $scope.downloadall = function () {
            for (var i = 0; i < $scope.institutionbankacct_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.institutionbankacct_list[i].document_path, $scope.institutionbankacct_list[i].document_name);
            }
        }
       
    }
})();
