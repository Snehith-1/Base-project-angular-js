(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstSAonboardingIndBureauViewController', MstSAonboardingIndBureauViewController);

    MstSAonboardingIndBureauViewController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstSAonboardingIndBureauViewController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstSAonboardingIndBureauViewController';
        $scope.saindividual2bureau_gid = $location.search().lssaindividual2bureau_gid;
        $scope.sacontact_gid = $location.search().lssacontact_gid;

        var lstab = $location.search().lstab;

        activate();
        function activate() {


            // Calender Popup... //
            vm.calender1 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open1 = true;
            };

            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };

            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];


            var url = 'api/MstApplication360/BureauNameList';
            SocketService.get(url).then(function (resp) {
                $scope.bureau_list = resp.data.bureauname_list;
            });

            var param = {
                saindividual2bureau_gid: $scope.saindividual2bureau_gid
            };

            var url = 'api/MstSAOnboardingIndividual/SABureauView';

            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                $scope.bureauname_name = resp.data.bureauname_name;
                $scope.bureau_gid = resp.data.bureauname_gid;
                $scope.txtbureau_score = resp.data.bureau_score;
                $scope.txtbureauscore_date = resp.data.bureauscore_date;
                $scope.txtobservations = resp.data.observations;
                $scope.txtbureau_response = resp.data.bureau_response;
                $scope.contact2bureau_gid = resp.data.contact2bureau_gid;

                unlockUI();
            });
          
            var url = 'api/MstSAOnboardingIndividual/SAUploadIndDocList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.cicuploaddoc_list = resp.data.sauploaddoc_list;
            });
        }

        //$scope.report_View = function (tmpcicdocument_gid) {
        //    var tmpcicdocument_gid = tmpcicdocument_gid;
        //    localStorage.setItem('tmpcicdocument_gid', tmpcicdocument_gid);
        //    var URL = location.protocol + "//" + location.hostname + "/v1/#/app/HighmarkReport";
        //    window.open(URL, '_blank');
        //};

        $scope.CICUploads_Back = function () {
            $location.url('app/MstSAOnboardingIndividualVerification?lssacontact_gid=' + $scope.sacontact_gid);
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


    }
})();

