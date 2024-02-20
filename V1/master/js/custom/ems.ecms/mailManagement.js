(function () {
    'use strict';

    angular
        .module('angle')
        .controller('mailManagementcontroller', mailManagementcontroller);

    mailManagementcontroller.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$filter', 'ngTableParams', '$resource', '$timeout', 'ngTableDataService'];

    function mailManagementcontroller($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $filter, ngTableParams, $resource, $timeout, ngTableDataService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'mailManagementcontroller';

        activate();


        function activate() {

            $scope.totalDisplayed=100;
            var url = 'api/customerAlertGenerate/mailManagement';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.customer_data = resp.data.customermail_list;
              // new code start   
              if ($scope.customer_data == null) {
                                    $scope.total = 0;
                                    $scope.totalDisplayed = 0;
                                }
                                else {
                                    $scope.total = $scope.customer_data.length;
                                    if ($scope.customer_data.length < 100) {
                                        $scope.totalDisplayed = $scope.customer_data.length;
                                    }
                                }
                // new code end
                //$scope.total=$scope.customer_data.length;
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
        if ($scope.customer_data != null) {
       
                if (pagecount < $scope.customer_data.length) {
                    $scope.totalDisplayed += Number;
                    if($scope.customer_data.length<$scope.totalDisplayed){
                        $scope.totalDisplayed =$scope.customer_data.length;
                        Notify.alert(" Total Summary " + $scope.customer_data.length + " Records Only", "warning");
                    }
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(" Total Summary " + $scope.customer_data.length + " Records Only", "warning");
                    return;
                }
            }
            // new code end
            // $scope.totalDisplayed += Number;
            // console.log(pagecount);
            unlockUI();
        };
        $scope.send = function (val) {
            $scope.customeralert_gid = val;
            $scope.customeralert_gid = localStorage.setItem('customeralert_gid', val);
            $state.go('app.sendMailalert');
        }
        $scope.mailHistory = function (val) {
            $scope.customer_gid = val;
            $scope.customer_gid = localStorage.setItem('customer_gid', val);
            $scope.pageNavigation = localStorage.setItem('mailManagement', 'mailManagement');
            $state.go('app.customerAlertHistory');

        }


    }
})();
