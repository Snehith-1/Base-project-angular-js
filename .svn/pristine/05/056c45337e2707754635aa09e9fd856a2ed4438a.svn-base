(function () {
    'use strict';

    angular
        .module('angle')
        .controller('viewLawfirmcontroller', viewLawfirmcontroller);

    viewLawfirmcontroller.$inject = ['$rootScope', '$scope', '$state', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'DownloaddocumentService'];

    function viewLawfirmcontroller($rootScope, $scope, $state, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'viewLawfirmcontroller';

        activate();
        function activate() {
            $scope.lawfirm_gid = localStorage.getItem('lawfirm_gid');
            var params = {
                lawfirm_gid: $scope.lawfirm_gid,
                lawyerref_no: $scope.lawyerref_no,
                lawyer_name: $scope.lawyer_name,
                mobile_no: $scope.mobile_no,
                date_enrolment: $scope.date_enrolment
            }
        
            var url = 'api/lawFirm/lawfirmView'
            SocketService.getparams(url, params).then(function (resp) {
                $scope.Lawfirmupload = resp.data;
                $scope.firm_refno = resp.data.firm_refno;
                $scope.firm_name = resp.data.firm_name;
                $scope.contact_no = resp.data.contact_no;
                $scope.mail_address = resp.data.mail_address;
                $scope.firm_years = resp.data.firm_years;
                $scope.firm_address = resp.data.firm_address;               
                $scope.remarks = resp.data.remarks;
                $scope.filename_list = resp.data.UploadDocument;
                 $scope.lawyer_list = resp.data.lawfirm_list;
               

            });

            //var url = 'api/Lawfirm/lawfirmDetails';
            //lockUI();
            //SocketService.getparams(url, params).then(function (resp) {
            //    unlockUI();
            //    $scope.lawyer_list = resp.data.lawfirm_list;
               
            //});
            var url = 'api/Lawfirm/viewmember';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.member_list = resp.data.member_list;

            });
            $scope.close = function (val) {
                document.getElementById("userform").reset();
                var doc = document.getElementById(val);
                doc.style.display = 'none';
            }
        }
        //$scope.downloads = function (val1, val2) {
        //    //var phyPath = val1;
        //    //console.log(val1);
        //    //var relPath = phyPath.split("StoryboardAPI");
        //    //var relpath1 = relPath[1].replace("\\", "/");
        //    //var hosts = window.location.host;
        //    //var prefix = location.protocol + "//";
        //    //var str = prefix.concat(hosts, relpath1);
        //    //console.log(str);
        //    //var link = document.createElement("a");
        //    //var name = val2.split('.');
        //    //link.download = name[0];
        //    //var uri = str;
        //    //link.href = uri;
        //    //link.click();
        //    DownloaddocumentService.Downloaddocument(val1, val2);
        //}
        $scope.downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.Back = function () {
            $state.go('app.lawfirmSummary');
        }

    }
})();
