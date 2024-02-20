(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCreditGroupBureauEditController', MstCreditGroupBureauEditController);

    MstCreditGroupBureauEditController.$inject = ['$rootScope', '$sce', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','cmnfunctionService'];

    function MstCreditGroupBureauEditController($rootScope, $sce, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCreditGroupBureauEditController';
        $scope.opsgroup_gid = $location.search().opsgroup_gid;
        var opsgroup_gid = $scope.opsgroup_gid;
        $scope.opsapplication_gid = $location.search().opsapplication_gid;
        var opsapplication_gid = $scope.opsapplication_gid;
       
        activate();
        function activate() { 

           /*  $scope.opsapplication_gid = $location.search().opsapplication_gid;
           
            var param = {
                 opsapplication_gid: $scope.opsapplication_gid
             }
             
             var url = 'api/MstAppCreditUnderWriting/SocialAndTradeEdit';
           
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                $scope.cboBureauName = resp.data.bureau_gid;
                $scope.txtBureau_Score = resp.data.Bureau_Score;
                $scope.txtscoreas_on = resp.data.scoreas_on;
                $scope.txtObservation = resp.data.Observation;
                $scope.txtBureau_Response = resp.data.Bureau_Respons;
                $scope.txtreportdoc_name = resp.data.reportdoc_name;
                unlockUI();
            });  */

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
              
              vm.formats = ['dd-MM-yyyy'];
              vm.format = vm.formats[0];
              vm.dateOptions = {
                  formatYear: 'yy',
                  startingDay: 1
              };
        }
      
        $scope.bureaudtl_Back = function () {
            $location.url('app/MstCreditGroupDtlAdd?opsapplication_gid=' + opsapplication_gid + '&opsgroup_gid=' + opsgroup_gid);
        }

        $scope.update_bureaudtl = function () {
        if (($scope.cboBureauName == undefined) || ($scope.txtBureau_Score == undefined) || ($scope.reportdoc_name == undefined)) {
            Notify.alert('Enter All Mandatory Fields','warning');
        }
        else {
           var params = {
               opsapplication_gid: opsapplication_gid,
               opsgroup_gid: opsgroup_gid,
               applicant_type:'Group',
               bureau_name: $scope.cboBureauName.bureau_name,
               bureau_gid: $scope.cboBureauName.bureau_gid,
               Bureau_Score: $scope.txtBureau_Score,
               scoreas_on: $scope.txtscoreas_on,
               Observation : $scope.txtObservation,
               Bureau_Response : $scope.txtBureau_Response,
               reportdoc_name : $scope.txtreportdoc_name
           }
            var url = 'api/MstAppCreditUnderWriting/PSLDataFlaggingSubmit';
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
                        status: 'info',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                $location.url('app/MstCreditGroupDtlAdd?opsapplication_gid=' + opsapplication_gid + '&opsgroup_gid=' + opsgroup_gid);
            });  
        }  
    } 

$scope.documentUpload = function (val) {
        
    var frm = new FormData();
        for(var i = 0;i < val.length;i++ ){
            var item = {
                name: val[i].name,
                file: val[i]
            };   
            frm.append('fileupload', item.file);
            frm.append('file_name', item.name);
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
            var url = 'api/MstApplicationAdd/InstitutionDocumentUpload';
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

        $scope.documentviewer = function (val1, val2) {
            lockUI();
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
            if (IsValidExtension == false) {
                Notify.alert("View is not supported for this format..!", {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                unlockUI();
                return false;
            }
            DownloaddocumentService.DocumentViewer(val1, val2);
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
    var url = 'api/MstApplicationAdd/InstitutionDocumentDelete';
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
       
    }
})();

