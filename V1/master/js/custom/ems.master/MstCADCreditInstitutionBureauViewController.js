(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCADCreditInstitutionBureauViewController', MstCADCreditInstitutionBureauViewController);

    MstCADCreditInstitutionBureauViewController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService', 'cmnfunctionService'];

    function MstCADCreditInstitutionBureauViewController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCADCreditInstitutionBureauViewController';
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


            // var url = 'api/MstApplication360/BureauNameList';
            // SocketService.get(url).then(function (resp) {
            //     $scope.bureau_list = resp.data.bureauname_list;
            // });

            var param = {
                institution2bureau_gid: $scope.institution2bureau_gid
            };

            var url = 'api/MstCADCreditAction/CICInstitutionEdit';

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
            var url = 'api/MstCADCreditAction/CICUploadInstitutionDocList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.cicuploaddoc_list = resp.data.cicuploaddoc_list;
            });
        }

        $scope.report_View = function (tmpcicdocument_gid) {
            var tmpcicdocument_gid = tmpcicdocument_gid;
            localStorage.setItem('tmpcicdocument_gid', tmpcicdocument_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/CADHighmarkInstitutionReport";
            window.open(URL, '_blank');
        };

        // $scope.CICUploads_Back = function () {
        //     $location.url('app/MstCADCreditInstitutionDtlAdd?application_gid=' + $scope.application_gid + '&institution_gid=' + $scope.institution_gid + '&lspage=' + $scope.lspage);
        // }
        $scope.CICUploads_Back = function () {
            if(lspagename=='MstCADCreditInstitutionDtlAdd')
                $location.url('app/MstCADCreditInstitutionDtlAdd?application_gid=' + $scope.application_gid + '&institution_gid=' + $scope.institution_gid + '&lspage=' + $scope.lspage);
            
            else if(lspagename=='MstCADCreditCompanyDtlView')
                $location.url('app/MstCADCreditCompanyDtlView?application_gid=' + $scope.application_gid + '&institution_gid=' + $scope.institution_gid + '&lspage=' + $scope.lspage +'&lspagetype='+ lspagetype);
        
            else if(lspagename=='MstCadInstitutionView')
                $location.url('app/MstCadInstitutionView?application_gid=' + $scope.application_gid + '&institution_gid=' + $scope.institution_gid + '&lspage=' + $scope.lspage +'&lspagetype='+ lspagetype);
        
            }

        $scope.downloads = function (val1, val2, val3) {
            if (val3 == 'N') {
                DownloaddocumentService.Downloaddocument(val1, val2);
            }
            else {
                DownloaddocumentService.OtherDownloaddocument(val1, val2, val3);
            }
        }

        $scope.downloadallcic = function () {
            for (var i = 0; i < $scope.cicuploaddoc_list.length; i++) {
                if ($scope.cicuploaddoc_list[i].migration_flag == 'N') {
                    DownloaddocumentService.Downloaddocument($scope.cicuploaddoc_list[i].document_path, $scope.cicuploaddoc_list[i].document_name);
                }
                else {
                    DownloaddocumentService.OtherDownloaddocument($scope.cicuploaddoc_list[i].document_path, $scope.cicuploaddoc_list[i].document_name, $scope.cicuploaddoc_list[i].migration_flag);
                }
            }
        }

        $scope.documentviewer = function (val1, val2, val3) {
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

            if (val3 == 'N') {
                DownloaddocumentService.DocumentViewer(val1, val2);
            }
            else {
                DownloaddocumentService.OtherDocumentViewer(val1, val2, val3);
            }

        }

    }
})();

