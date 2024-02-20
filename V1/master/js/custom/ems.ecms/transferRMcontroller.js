(function () {
    'use strict';

    angular
        .module('angle')
        .controller('transferRMcontroller', transferRMcontroller);

    transferRMcontroller.$inject = ['$rootScope', '$scope','$modal', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function transferRMcontroller($rootScope, $scope,$modal, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'transferRMcontroller';
       
        activate();

        function activate() {
            $scope.totalDisplayed=100;
            lockUI();
            var url = 'api/deferral/rm';
            SocketService.get(url).then(function (resp) {
                $scope.transferRM_data = resp.data.deferralSummaryDtls;
                // new code start 
                unlockUI();  
                if ($scope.transferRM_data == null) {
                                        $scope.total = 0;
                                        $scope.totalDisplayed = 0;
                                    }
                                    else {
                                        $scope.total = $scope.transferRM_data.length;
                                        if ($scope.transferRM_data.length < 100) {
                                            $scope.totalDisplayed = $scope.transferRM_data.length;
                                        }
                                    }
                    // new code end
                // if ( $scope.transferRM_data==null){
                //     $scope.total=0;
                // }
                // else{
                //     $scope.total= $scope.transferRM_data.length;
                // }
                //console.log(resp.data.deferralSummaryDtls);
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
        if ($scope.transferRM_data != null) {
       
                if (pagecount < $scope.transferRM_data.length) {
                    $scope.totalDisplayed += Number;
                    if($scope.transferRM_data.length<$scope.totalDisplayed){
                        $scope.totalDisplayed =$scope.transferRM_data.length;
                        Notify.alert(" Total Summary " + $scope.transferRM_data.length + " Records Only", "warning");
                    }
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(" Total Summary " + $scope.transferRM_data.length + " Records Only", "warning");
                    return;
                }
            }
            // new code end
                // $scope.totalDisplayed += Number;
                // console.log(pagecount);
                unlockUI();
            };
    
           

            $scope.popuptransfer = function (val) {
                $scope.relationshipmgmt_gid = val;
                $scope.relationshipmgmt_gid = localStorage.setItem('relationshipmgmt_gid', val);
                $state.go('app.RMDetails');
            }

           


            $scope.close = function (val) {
                document.getElementById("userform").reset();
                var doc = document.getElementById(val);
                doc.style.display = 'none';
            }
        }
})();