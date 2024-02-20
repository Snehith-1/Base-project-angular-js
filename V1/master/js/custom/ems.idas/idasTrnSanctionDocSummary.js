(function () {
    'use strict';

    angular
        .module('angle')
        .controller('idasTrnSanctionDocSummary', idasTrnSanctionDocSummary);

    idasTrnSanctionDocSummary.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function idasTrnSanctionDocSummary($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        $scope.title = 'idasTrnSanctionDocSummary';

        activate();

        function activate() {
            $scope.totalDisplayed = 100;
            lockUI();
            var url = "api/IdasTrnSanctionDoc/SanctionSummary";
            SocketService.get(url).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    $scope.sanctionlist = resp.data.MdlSanctionDocSummary;
                    $scope.total = $scope.sanctionlist.length;
                }
                else {
                    console.log(resp.data.message);
                }


            });
        }
      
        document.getElementById('pagecount').onkeyup = function () {
            // console.log(document.getElementById('pagecount').value);
            if($scope.pagecount==null){
             var el = document.getElementById('loadmore');
             el.style.backgroundColor = '#DCDCDC';  
            }
            else{
             var el = document.getElementById('loadmore');
             el.style.backgroundColor = '#ffa';
            }
        };
        $scope.loadMore = function (pagecount) {
            if(pagecount==undefined){
                Notify.alert("Enter the Total Summary Count","warning");
                return;
            }
            lockUI();
            var Number = parseInt(pagecount);

            $scope.totalDisplayed += Number;
            unlockUI();
        };

        $scope.DocSanction = function (sanction_gid) {

            $location.url('app/idasTrnSanctionDoc?sanction_gid=' + sanction_gid);

            //localStorage.setItem('sanction_gid', sanction_gid);

            //$state.go('app.idasTrnSanctionDoc');
        }
        $scope.generate = function (sanction_gid) {

            $location.url('app/idasTrnPreFilGeneration?sanction_gid=' + sanction_gid + '&lspage=sanctiondocprefil');
        }
    }
})();
