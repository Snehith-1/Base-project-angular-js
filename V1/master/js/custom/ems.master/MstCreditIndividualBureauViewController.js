(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCreditIndividualBureauViewController', MstCreditIndividualBureauViewController);

    MstCreditIndividualBureauViewController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService', 'cmnfunctionService'];

    function MstCreditIndividualBureauViewController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCreditIndividualBureauViewController';
        $scope.contact2bureau_gid = $location.search().lscontact2bureau_gid;
        $scope.contact_gid = $location.search().lscontact_gid;
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


            var url = 'api/MstApplication360/BureauNameList';
            SocketService.get(url).then(function (resp) {
                $scope.bureau_list = resp.data.bureauname_list;
            });

            var param = {
                contact2bureau_gid: $scope.contact2bureau_gid
            };

            var url = 'api/MstApplicationEdit/CICIndividualEdit';

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
            var url = 'api/MstApplicationEdit/CICUploadIndividualDocList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.cicuploaddoc_list = resp.data.cicuploaddoc_list;
            });
        }

        $scope.report_View = function (tmpcicdocument_gid) {          

            if ($scope.bureauname_name == 'TransUnion') {
                var tmpcicdocument_gid = tmpcicdocument_gid;
                localStorage.setItem('tmpcicdocument_gid', tmpcicdocument_gid);
                var URL = location.protocol + "//" + location.hostname + "/v1/#/app/TransUnionReport";
                window.open(URL, '_blank');
            } else if ($scope.bureauname_name == 'High Mark') {
                var tmpcicdocument_gid = tmpcicdocument_gid;
                localStorage.setItem('tmpcicdocument_gid', tmpcicdocument_gid);
                var URL = location.protocol + "//" + location.hostname + "/v1/#/app/HighmarkReport";
                window.open(URL, '_blank');
            }

            
        };

        $scope.CICUploads_Back = function () {
            if(lspagename=='MstCreditIndividualDtlAdd')
                $location.url('app/MstCreditIndividualDtlAdd?application_gid=' + $scope.application_gid + '&contact_gid=' + $scope.contact_gid + '&lspage=' + $scope.lspage );
            
            else if(lspagename=='MstCreditIndividualDtlView')
                $location.url('app/MstCreditIndividualDtlView?application_gid=' + $scope.application_gid + '&contact_gid='+ $scope.contact_gid + '&lspage='+ $scope.lspage + '&lspagename=' + lspagename + '&lspagetype=' + lspagetype );
        
            else if(lspagename=='MstCcCommitteeIndividualView')
                $location.url('app/MstCcCommitteeIndividualView?application_gid=' + $scope.application_gid + '&contact_gid='+ $scope.contact_gid + '&lspage='+ $scope.lspage + '&lspagename=' + lspagename + '&lspagetype=' + lspagetype);
        
        } 

        $scope.downloads = function (val1, val2, val3) {
            if (val3 == 'N') {
                DownloaddocumentService.Downloaddocument(val1, val2);
            }
            else {
                DownloaddocumentService.OtherDownloaddocument(val1, val2, val3);
            }
        }
       
       
        $scope.downloadall = function () {
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

