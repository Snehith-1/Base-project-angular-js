(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rmManagementcontroller', rmManagementcontroller);

    rmManagementcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function rmManagementcontroller($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'rmManagementcontroller';

        activate();
        function activate() {
            $scope.totalDisplayed=100;
            var url = 'api/deferral/getrmSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.rmDeferral_data = resp.data.rmdeferralSummaryDtls;
                // new code start   
                unlockUI();
                if ($scope.rmDeferral_data == null) {
                                        $scope.total = 0;
                                        $scope.totalDisplayed = 0;
                                    }
                                    else {
                                        $scope.total = $scope.rmDeferral_data.length;
                                        if ($scope.rmDeferral_data.length < 100) {
                                            $scope.totalDisplayed = $scope.rmDeferral_data.length;
                                        }
                                    }
                    // new code end
                // if( $scope.rmDeferral_data==null){
                //     $scope.total=0;
                // }
                // else{
                //     $scope.total=$scope.rmDeferral_data.length;
                // }
             
           });
         
        }

        // document.getElementById('pagecount').onkeyup = function () {
           
        //     if($scope.pagecount==null){
        //      var el = document.getElementById('loadmore');
        //      el.style.backgroundColor = '#DCDCDC';  
        //     }
        //     else{
        //      var el = document.getElementById('loadmore');
        //      el.style.backgroundColor = '#ffa';
        //     }
        // };

  $scope.loadMore= function (pagecount) {
            if(pagecount==undefined){
                Notify.alert("Enter the Total Summary Count","warning");
                return;
            }
            lockUI();

            var Number = parseInt(pagecount);
            // new code start
        if ($scope.rmDeferral_data != null) {
       
                if (pagecount < $scope.rmDeferral_data.length) {
                    $scope.totalDisplayed += Number;
                    if($scope.rmDeferral_data.length<$scope.totalDisplayed){
                        $scope.totalDisplayed =$scope.rmDeferral_data.length;
                        Notify.alert(" Total Summary " + $scope.rmDeferral_data.length + " Records Only", "warning");
                    }
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(" Total Summary " + $scope.rmDeferral_data.length + " Records Only", "warning");
                    return;
                }
            }
            // new code end
            // $scope.totalDisplayed += Number;
            // console.log(pagecount);
            unlockUI();
        };



        $scope.popupUpload = function (val_deferral, val_loan) {
            $scope.deferral_gid = val_deferral;
            $scope.deferral_gid = localStorage.setItem('deferral_gid', val_deferral);
          
            $scope.loan_gid = val_loan;
            $scope.loan_gid = localStorage.setItem('loan_gid', val_loan);

            $state.go('app.rmManagementRequest');

        }

       }
})();
