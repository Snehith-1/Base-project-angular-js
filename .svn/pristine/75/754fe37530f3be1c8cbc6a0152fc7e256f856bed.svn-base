(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCheckerapprovalViewController', MstCheckerapprovalViewController);

    MstCheckerapprovalViewController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstCheckerapprovalViewController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCheckerapprovalViewController';
        var sanction_gid = $location.search().sanction_gid;
        var lspage = $location.search().lspage;

        activate();
        
        function activate() {
            var url = 'api/IdasMstSanction/SanctionDtlsEdit';
            var params = {
                sanction_gid: sanction_gid
            };
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.sanctionrefnoEdit = resp.data.sanction_refno;
                $scope.SanctionDateEdit = resp.data.sanction_date;
                $scope.SanctionAmountEdit = resp.data.sanction_amount;
                $scope.customerNameEdit = resp.data.customername;
                $scope.CustomerurnEdit = resp.data.customer_urn;
                $scope.verticalCodeEdit = resp.data.vertical;
                $scope.checkerapproval_flag = resp.data.checkerapproval_flag;
            });
            var url = 'api/IdasMstSanction/SanctioncheckerEdit';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.checkerfile_name = resp.data.checkerfile_name;
                $scope.checkerfile_path = resp.data.checkerfile_path;
                $scope.uploaded_by = resp.data.uploaded_by;
                $scope.uploaded_date = resp.data.updated_date;
            });
        }

        $scope.downloadsWordFile = function () {
            var params = {
                sanction_gid: sanction_gid
            };
            var url = 'api/IdasMstSanction/GetWordGenerate';
            var phyPath = val1;
            var relPath = phyPath.split("EMS");
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

        $scope.CheckerApprovalBack = function () {
            if (lspage == 'Pending') {
                $state.go('app.MstCheckerApprovalSummary')
            }
            else {
                $state.go('app.MstCheckerApprovalCompletedSummary')
            }
        }

        $scope.checkerapprove = function () {
            var params = {
                sanction_gid: sanction_gid
            }
            var url = 'api/IdasMstSanction/UpdateCheckerApproval';
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $state.go('app.MstCheckerApprovalSummary')
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
            });
        }
    }
})();
