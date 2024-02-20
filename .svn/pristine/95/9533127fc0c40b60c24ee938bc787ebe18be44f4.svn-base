(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCadLSADtlSummaryController', MstCadLSADtlSummaryController);

    MstCadLSADtlSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'DownloaddocumentService'];

    function MstCadLSADtlSummaryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCadLSADtlSummaryController';
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;
        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        $scope.employee_gid = $location.search().employee_gid;
        var employee_gid = $scope.employee_gid;
        var application2sanction_gid = $location.search().application2sanction_gid;
        var lsfollowup = $location.search().lsfollowup;

        activate();

        function activate() {
            lockUI();
            var params = {
                application_gid: application_gid
            }
            var url = 'api/MstLSA/GetGenerateLSAMakerSummary';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.GenerateLSAMakerSummary = resp.data.MdlGenerateLSAMakerSummary;
            });

            var lspage = $location.search().lspage;
            if (lspage == 'CadLsaCompleted') {
                $scope.lsacompletedview = true;
                $scope.hideincompletedcase = true;
            }
        }

        $scope.generate_lsa = function () {
            $location.url('app/MstCadGenerateLSA?application_gid=' + application_gid + '&application2sanction_gid=' + application2sanction_gid + '&lsfilledlimitproduct=N&lspage=' + lspage + '&lsfollowup=' + lsfollowup);
        }

        $scope.viewlsa = function (val, generatelsa_gid, limitproduct_filled) {
            if (lspage != "CadLsaApprover" && lspage != "CadLsaCompleted" && lsfollowup != "Y") {
                if (limitproduct_filled == "Y") {
                    $location.url('app/MstCadGenerateLSA?application_gid=' + application_gid + '&generatelsa_gid=' + generatelsa_gid + '&application2sanction_gid=' + application2sanction_gid + '&lsfilledlimitproduct=Y&lspage=' + lspage + '&lsfollowup=' + lsfollowup);

                }
                else {
                    $location.url('app/MstCadGenerateLSA?application_gid=' + application_gid + '&generatelsa_gid=' + generatelsa_gid + '&application2sanction_gid=' + application2sanction_gid + '&lsfilledlimitproduct=N&lspage=' + lspage + '&lsfollowup=' + lsfollowup);
                }
            } 
            else {
                $location.url('app/MstCADlsa360View?application_gid=' + application_gid + '&generatelsa_gid=' + generatelsa_gid + '&application2sanction_gid=' + application2sanction_gid + '&lsfilledlimitproduct=Y&lspage=' + lspage + '&lsfollowup=' + lsfollowup);
            }
        }

        $scope.Back = function () {
            if (lspage == "CadLsaMaker")
                $location.url('app/MstCadLSASummary');
            else if (lspage == "CadLsaChecker")
                $location.url('app/MstCadLSACheckerSummary');
            else if (lspage == "CadLsaApprover")
                $location.url('app/MstCadLSAApprovalSummary');
            else if (lspage == "CadLsaCompleted")
                $location.url('app/MstLSAApprovalCompleted');
            else if (lspage == "CadLsaReport")
                $location.url('app/MstLSAReport');
        }

        $scope.LSApdf = function (lsgeneratelsa_gid) {
            lockUI();
            var params = {
                generatelsa_gid: lsgeneratelsa_gid
            }
            var url = 'api/MstLSA/GetLSApdf';
            SocketService.getparams(url, params).then(function (resp) {

                if (resp.data.status == true) {
                    var filepath = resp.data.file_path;
                    var filename = resp.data.file_name;
                    DownloaddocumentService.Downloaddocument(filepath, filename);
                    Notify.alert('LSA Report Downloaded Successfully', 'success')
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert('Error Occurred While Export PDF !', 'warning');
                }

            });

        }
    }
})();
