(function () {
    'use strict';

    angular
        .module('angle')
        .controller('manageDeferral', manageDeferral);

    manageDeferral.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function manageDeferral($rootScope, $scope, $modal, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'manageDeferral';
       
        activate();
        function activate() {
            $scope.totalDisplayed=100;
            var url = 'api/deferral/manageDeferralSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.deferral_data = resp.data.managedeferralSummaryDtls;
                // new code start  
                unlockUI(); 
                if ($scope.deferral_data == null) {
                                        $scope.total = 0;
                                        $scope.totalDisplayed = 0;
                                    }
                                    else {
                                        $scope.total = $scope.deferral_data.length;
                                        if ($scope.deferral_data.length < 100) {
                                            $scope.totalDisplayed = $scope.deferral_data.length;
                                        }
                                    }
                    // new code end
                // $scope.total=$scope.deferral_data.length;
            });
         
         }
        //  document.getElementById('pagecount').onkeyup = function () {
           
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
        if ($scope.deferral_data != null) {
       
                if (pagecount < $scope.deferral_data.length) {
                    $scope.totalDisplayed += Number;
                    if($scope.deferral_data.length<$scope.totalDisplayed){
                        $scope.totalDisplayed =$scope.deferral_data.length;
                        Notify.alert(" Total Summary " + $scope.deferral_data.length + " Records Only", "warning");
                    }
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(" Total Summary " + $scope.deferral_data.length + " Records Only", "warning");
                    return;
                }
            }
            // new code end
            // $scope.totalDisplayed += Number;
            // console.log(pagecount);
            unlockUI();
        };

        $scope.edit = function (val) {
            $scope.deferral_gid = val;
            $scope.deferral_gid = localStorage.setItem('deferral_gid', val);
            $state.go('app.manageDeferraledit');
        }

        $scope.popupdeferral = function () {
            $state.go('app.manageDeferraladd');
        }

       
        $scope.popupView = function (val) {
            $scope.deferral_gid = val;
            $scope.deferral_gid = localStorage.setItem('deferral_gid', val);
            $state.go('app.manageDeferralview');

        }

         }
        })();