(function () {
    'use strict';

    angular
        .module('angle')
        .controller('viewLawyercontroller', viewLawyercontroller);

    viewLawyercontroller.$inject = ['$rootScope', '$scope', '$state', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route','DownloaddocumentService'];

    function viewLawyercontroller($rootScope, $scope, $state, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'viewLawyercontroller';

        activate();
        function activate() {
            $scope.lawyerregister_gid = localStorage.getItem('lawyerregister_gid');
            var params = {
                lawyerregister_gid: $scope.lawyerregister_gid
            }
            var url = 'api/registerLawyer/lawyerView';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.UploadDocumentname = resp.data;
                $scope.lawyerref_no = resp.data.lawyerref_no;
                $scope.lawyer_name = resp.data.lawyer_name;
                $scope.dob = resp.data.dob;
                $scope.gender = resp.data.gender;
                $scope.mobile_no = resp.data.mobile_no;
                $scope.telephone_no = resp.data.telephone_no;
                $scope.email_address = resp.data.email_address;
                $scope.educational_qualification = resp.data.educational_qualification;
                $scope.date_enrolment = resp.data.date_enrolment;
                $scope.pan_no = resp.data.pan_no;
                $scope.experience = resp.data.experience;
                $scope.place_practice = resp.data.place_practice;
                $scope.address_line1 = resp.data.address_line1;
                $scope.address_line2 = resp.data.address_line2;
                $scope.state = resp.data.state;
                $scope.country = resp.data.country;
                $scope.postal_code = resp.data.postal_code;
                $scope.document_name = resp.data.document_name;
                $scope.lawyerphoto_name = resp.data.lawyerphoto_name;
                $scope.document_path = resp.data.document_path;
                $scope.filename_list = resp.data.UploadDocumentList;
                $scope.aadhar_no = resp.data.aadhar_no;
                $scope.bank_name = resp.data.bank_name;
                $scope.account_no = resp.data.account_no;
                $scope.ifsc_code = resp.data.ifsc_code;
                var pathArray = location.href.split('/');
                var protocol = pathArray[0];
                var host = pathArray[2];
                var url = location.protocol + '//' + host;
                var str = resp.data.lawyerphoto_path;
                str = str.substring(str.indexOf("StoryboardAPI") + 13);
                $scope.lawyerphoto_path = url.concat(str);
               
            });
          
            $scope.close = function (val) {
                document.getElementById("userform").reset();
                var doc = document.getElementById(val);
                doc.style.display = 'none';
            }
      }
        //$scope.downloads = function (val1, val2) {
        //    //var phyPath = val1;           
        //    //var relPath = phyPath.split("StoryboardAPI");
        //    //var relpath1 = relPath[1].replace("\\", "/");      
        //    //var hosts = window.location.host;
        //    //var prefix = location.protocol + "//";
        //    //var str = prefix.concat(hosts, relpath1);
        //    //var link = document.createElement("a");
        //    var name = val2.split('.');
        //    //link.download = name[0];
        //    //var uri = str;
        //    //link.href = uri;
        //    //link.click();
        //    DownloaddocumentService.Downloaddocument(val1, name[0]);
        //}

        $scope.downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.viewBack = function () {
            $state.go('app.lawyerManagement');
        }
    }
})();
