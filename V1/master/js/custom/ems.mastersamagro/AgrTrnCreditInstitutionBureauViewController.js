(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrTrnCreditInstitutionBureauViewController', AgrTrnCreditInstitutionBureauViewController);

    AgrTrnCreditInstitutionBureauViewController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService','cmnfunctionService'];

    function AgrTrnCreditInstitutionBureauViewController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrTrnCreditInstitutionBureauViewController';
        $scope.institution2bureau_gid = $location.search().lsinstitution2bureau_gid;
        $scope.institution_gid = $location.search().lsinstitution_gid;
        $scope.application_gid = $location.search().lsapplication_gid;
        $scope.lspage = $location.search().lspage;
        var lspagename = $location.search().lspagename;
        var lspagetype = $location.search().lspagetype;

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


            var url = 'api/AgrMstApplication360/BureauNameList';
            SocketService.get(url).then(function (resp) {
                $scope.bureau_list = resp.data.bureauname_list;
            });

            var param = {
                institution2bureau_gid: $scope.institution2bureau_gid
            };

            var url = 'api/AgrMstApplicationEdit/CICInstitutionEdit';

            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                $scope.bureauname_name = resp.data.bureauname_name;
                $scope.bureau_gid = resp.data.bureauname_gid;
                $scope.txtbureau_score = resp.data.bureau_score;
                $scope.txtbureauscore_date = resp.data.bureauscore_date;
                $scope.txtobservations = resp.data.observations;
                $scope.txtbureau_response = resp.data.bureau_response;
                $scope.institution2bureau_gid = resp.data.institution2bureau_gid;

                unlockUI();
            });
            var url = 'api/AgrMstApplicationEdit/CICUploadInstitutionDocList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.cicuploaddoc_list = resp.data.cicuploaddoc_list;
            });
        }

        $scope.report_View = function (tmpcicdocument_gid) {
            if ($scope.bureauname_name == 'TransUnion') {
                var tmpcicdocument_gid = tmpcicdocument_gid;
                localStorage.setItem('tmpcicdocument_gid', tmpcicdocument_gid);
                var URL = location.protocol + "//" + location.hostname + "/v1/#/app/AgrTransUnionInstitutionReport";
                window.open(URL, '_blank');
            } else if ($scope.bureauname_name == 'High Mark') {
                var tmpcicdocument_gid = tmpcicdocument_gid;
                localStorage.setItem('tmpcicdocument_gid', tmpcicdocument_gid);
                var URL = location.protocol + "//" + location.hostname + "/v1/#/app/AgrHighmarkInstitutionReport";
                window.open(URL, '_blank');
            }
            // var tmpcicdocument_gid = tmpcicdocument_gid;
            // localStorage.setItem('tmpcicdocument_gid', tmpcicdocument_gid);
            // var URL = location.protocol + "//" + location.hostname + "/v1/#/app/AgrHighmarkInstitutionReport";
            // window.open(URL, '_blank');
        };

        $scope.CICUploads_Back = function () {
            if(lspagename=='AgrTrnCreditInstitutionDtlAdd')
                $location.url('app/AgrTrnCreditInstitutionDtlAdd?application_gid=' + $scope.application_gid + '&institution_gid=' + $scope.institution_gid + '&lspage=' + $scope.lspage);
            
            else if(lspagename=='AgrTrnCreditCompanyDtlView' || lspagename=='AgrTrnCcCommitteeInstitutionView' || lspagename=='AgrMstCcCommitteeInstitutionView')
                $location.url('app/'+ lspagename +'?application_gid=' + $scope.application_gid + '&institution_gid=' + $scope.institution_gid + '&lspage=' + $scope.lspage +'&lspagetype='+ lspagetype);
            
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

        $scope.downloadall = function () {
            for (var i = 0; i < $scope.cicuploaddoc_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.cicuploaddoc_list[i].document_path, $scope.cicuploaddoc_list[i].document_name);
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


    }
})();

