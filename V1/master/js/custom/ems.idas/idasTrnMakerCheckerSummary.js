(function () {
    'use strict';

    angular
        .module('angle')
        .controller('idasTrnMakerCheckerSummary', idasTrnMakerCheckerSummary);

    idasTrnMakerCheckerSummary.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function idasTrnMakerCheckerSummary($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        $scope.title = 'idasTrnMakerCheckerSummary';

        activate();

        function activate() {
            lockUI();
            $scope.totalDisplayed = 100;
            var url = "api/IdasTrnSanctionDoc/MakerCheckerSummary";
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.sanctionlist = resp.data.MdlMakercheckerSummary;
                $scope.total = $scope.sanctionlist.length;
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
        $scope.docVerifyMaker = function (sanction_gid) {
           
            localStorage.setItem('sanction_gid', sanction_gid);

            $state.go('app.idasTrnDocVerifyMkr');
        }
        $scope.docVerifyChecker = function (sanction_gid) {
          
            localStorage.setItem('sanction_gid', sanction_gid);

            $state.go('app.idasTrnDocVerifyChkr');
        }
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
      
    }

   
})();
