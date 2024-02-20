(function () {
    'use strict';

    angular
        .module('angle')
        .controller('welcome', welcome);

    welcome.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage',  '$route'];

    function welcome($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route){
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'welcome';

        activate();

        function activate() {
           var today = new Date();
           var months = ["January", "February", "March", "April", "May", "June",
           "July", "August", "September", "October", "November", "December"];
           var monthName = months[today.getMonth()];
           var date = today.getDate() + ' ' + monthName + ' ' + today.getFullYear();
       document.getElementById('date').innerHTML = date;
       
        var time = today.getHours() + ":" + today.getMinutes();
        document.getElementById('time').innerHTML = time;
            $scope.welcome_msg = 'Financial Intermediation & Services Pvt. Ltd.';
            var user_gid = localStorage.getItem('user_gid');
            var url = 'api/user/privilege';
            lockUI();
            SocketService.get(url + '?user_gid=' + user_gid).then(function (resp) {
              
                var its = resp.data.privileges.map(function (e) { return e.project }).indexOf("ITS");
                var ocs = resp.data.privileges.map(function (e) { return e.project }).indexOf("OCS");
                var asset = resp.data.privileges.map(function (e) { return e.project }).indexOf("AMS");
                var myapprovals = resp.data.privileges.map(function (e) { return e.project }).indexOf("APP");
                var ecms = resp.data.privileges.map(function (e) { return e.project }).indexOf("ECM");
                var osd = resp.data.privileges.map(function (e) { return e.project }).indexOf("OSD");
                var lgl = resp.data.privileges.map(function (e) { return e.project }).indexOf("LGL");
                var cms = resp.data.privileges.map(function (e) { return e.project }).indexOf("CMS");
                var rsk = resp.data.privileges.map(function (e) { return e.project }).indexOf("RSK");
                var ids = resp.data.privileges.map(function (e) { return e.project }).indexOf("IDS");
                var mst = resp.data.privileges.map(function (e) { return e.project }).indexOf("MST");
                var iassign = resp.data.privileges.map(function (e) { return e.project }).indexOf("ISN");
                //console.log(osd);

                if (its != -1)
                {
                    $scope.its = "Y";
                }
                if (ocs != -1) {
                    $scope.ocs = "Y";
                }
                if (asset != -1) {
                    $scope.asset = "Y";
                }
                if (myapprovals != -1) {
                    $scope.myapprovals = "Y";
                }
                if (ecms != -1) {
                    $scope.ecms = "Y";
                }
                if (osd != -1) {
                    $scope.osd = "Y";
                }
                if (lgl != -1) {
                    $scope.lgl = "Y";
                }
                if (cms != -1) {
                    $scope.cms = "Y";
                }
                if (rsk!=-1)
                {
                    $scope.rsk = "Y";
                }
                if(ids!=-1)
                {
                    $scope.ids = "Y";
                }
                if (mst != -1) {
                    $scope.mst = "Y";
                }
                if(iassign!=-1)
                {
                    $scope.iassign = "Y";
                }
               
            });
            var url = 'api/landingPage/landingpagedata';
            SocketService.get(url).then(function (resp) {
                $scope.count = resp.data.count_acknowledgement + resp.data.count_surrender + resp.data.count_tmpsurrender + resp.data.count_tmpholding + resp.data.count_temporaryhandover;
                $scope.count1 = resp.data.count_myapprovals;
                $scope.count2 = resp.data.count_response;
                unlockUI();
            });
            var url = 'api/UserType/Getipandlogintime';
            SocketService.get(url).then(function (resp) {
                $scope.ip = resp.data.ip;
                $scope.login_time = resp.data.login_time;
            });

            vm.myInterval = 5000;

            var slides = vm.slides = [];
            vm.addSlide = function () {
            var newWidth = 800 + slides.length;
            slides.push({
            
            });
            };



            for (var i = 0; i < 2; i++) {
            vm.addSlide();
            }

        };
        $scope.ecmssystem = function () {
            $scope.welcome_msg = 'Exceptions & Covenant Management System for SAMFIN & SAMAGRO';
           
             };
        $scope.ams = function () {
            $scope.welcome_msg = 'Asset Management System';
        };
        $scope.sd = function () {
            $scope.welcome_msg = 'Service Desk';
        };
        $scope.TMS = function () {
            $scope.welcome_msg = 'Task Management System';
        };
        $scope.CMS = function () {
            $scope.welcome_msg = 'Change Management System';
        };
        $scope.approval = function () {
            $scope.welcome_msg = 'My Approvals ( Service Desk , Change Management System )';
        };
        $scope.legal = function () {
            $scope.welcome_msg = 'Legal Management System';
        };

        $scope.RSK = function () {
            $scope.welcome_msg = 'Risk Management System';
        }
        $scope.OSD = function () {
            $scope.welcome_msg = 'Operation Service Desk';
        }
        $scope.SOP=function()
        {
            $state.go('app.MstDocumentUploadSummary')
        }    
        
    }
})();
