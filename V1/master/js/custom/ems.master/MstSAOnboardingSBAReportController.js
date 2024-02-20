(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstSAOnboardingSBAReportController', MstSAOnboardingSBAReportController);

    MstSAOnboardingSBAReportController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService'];

    function MstSAOnboardingSBAReportController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstSAOnboardingSBAReportController';

        activate();

        function activate() {

            var params = {

                satype_gid: $scope.cbosatype
            }
            var url = 'api/MstSAOnboardingInstitution/GetDropDown';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();                
                $scope.applicationadd_salist = resp.data.satype_list;
            });


            var params = {

                approvalstatus: $scope.cboapproval
            }
            var url = 'api/MstSAOnboardingBussDevtVerification/GetBothReport';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();                
                $scope.reportList = resp.data.reportList;
            });

            
            

        }
//
$scope.Onsearch=function(cboapproval)
{
    if (($scope.cbosatype == undefined)) {
        Notify.alert('Select SA Type', 'warning')
    }
  /*  else if ($scope.cboapproval == undefined) {
        Notify.alert('Select approval status', 'warning')
    }*/
    else 
    
     {
        var satypename = $('#satype_name :selected').text();
        var params = {

            approvalstatus: $scope.cboapproval
        }
        if ((satypename == 'Company')&&(($scope.cboapproval == undefined)||($scope.cboapproval == '')))  {
            var url = 'api/MstSAOnboardingBussDevtVerification/GetInstitutionReportOnly';
        }
        else if ((satypename == 'Company')&&(($scope.cboapproval != undefined)||($scope.cboapproval != ''))) {
            var url = 'api/MstSAOnboardingBussDevtVerification/GetInstitutionReport';
        }
        else if ((satypename == 'Individual')&&(($scope.cboapproval == undefined)||($scope.cboapproval == ''))) {
            var url = 'api/MstSAOnboardingBussDevtVerification/GetIndividualReportOnly';
        }
        else if  ((satypename == 'Individual') && (($scope.cboapproval != undefined) || ($scope.cboapproval != ''))){
            var url = 'api/MstSAOnboardingBussDevtVerification/GetIndividualReport';
        }
        else  {
            var url = 'api/MstSAOnboardingBussDevtVerification/GetBothReport';
        }
        SocketService.post(url, params).then(function (resp) {
            $scope.reportList = resp.data.reportList;
            unlockUI();
        });
    }
}




//
        $scope.search=function(cbosatype)
        {
            if (($scope.cbosatype == undefined)) {
              //  Notify.alert('Select SBA Type', 'warning')
            }
          /*  else if ($scope.cboapproval == undefined) {
                Notify.alert('Select approval status', 'warning')
            }*/
            else 
            
             {
                var satypename = $('#satype_name :selected').text();
                var params = {

                    approvalstatus: $scope.cboapproval
                }
                if ((satypename == 'Company')&&(($scope.cboapproval == undefined)||($scope.cboapproval == '')))  {
                    var url = 'api/MstSAOnboardingBussDevtVerification/GetInstitutionReportOnly';
                }
                else if ((satypename == 'Company')&&(($scope.cboapproval != undefined)||($scope.cboapproval != ''))) {
                    var url = 'api/MstSAOnboardingBussDevtVerification/GetInstitutionReport';
                }
                else if ((satypename == 'Individual')&&(($scope.cboapproval == undefined)||($scope.cboapproval == ''))) {
                    var url = 'api/MstSAOnboardingBussDevtVerification/GetIndividualReportOnly';
                }
                else if  ((satypename == 'Individual') && (($scope.cboapproval != undefined) || ($scope.cboapproval != ''))){
                    var url = 'api/MstSAOnboardingBussDevtVerification/GetIndividualReport';
                }
                else  {
                    var url = 'api/MstSAOnboardingBussDevtVerification/GetBothReport';
                }
                SocketService.post(url, params).then(function (resp) {
                    $scope.reportList = resp.data.reportList;
                    unlockUI();
                });
            }
        }
        $scope.all = function () {
              activate();
            $scope.cboapproval = "";
            $scope.reportList = "";
        }
        $scope.ExportExcel = function () {
            var satypename = $('#satype_name :selected').text();
                var params = {

                    satype_name: satypename,
                    approvalstatus: $scope.cboapproval

                }
                var url = 'api/MstSAOnboardingBussDevtVerification/Report'
               
                SocketService.post(url, params).then(function (resp) {

                    if (resp.data.status == true) {
                         DownloaddocumentService.Downloaddocument(resp.data.lscloudpath, resp.data.lsname);


                        //DownloaddocumentService.Downloaddocument(val1, val2);
                       /* var phyPath = resp.data.lspath;
                        var relPath = phyPath.split("EMS");
                        var relpath1 = relPath[1].replace("\\", "/");
                        var hosts = window.location.host;
                        var prefix = location.protocol + "//";
                        var str = prefix.concat(hosts, relpath1);
                        var link = document.createElement("a");
                        var name = resp.data.lsname.split('.');
                        link.download = name[0];
                        var uri = str;
                        link.href = uri;
                        link.click();*/
                    }
                    else {
                       
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });

                       // Notify.alert('Error Occurred While Exporting !', 'warning')
                    }
                });
            
        }

        $scope.submit = function () {
            var satypename = $('#satype_name :selected').text(); 
            var params = {

                sbatypename: satypename
            }
            var url = 'api/MstSAOnboardingBussDevtVerification/Report';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
            unlockUI();                
            $scope.applicationadd_salist = resp.data.satype_list;
        });
    
        }
    }
})();
