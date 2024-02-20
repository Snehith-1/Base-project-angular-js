(function () {
    'use strict';

    angular
        .module('angle')
        .controller('penalityAlertcontroller', penalityAlertcontroller);

    penalityAlertcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'Notify', 'SocketService', '$location', '$route', '$filter', 'ngTableParams', '$modal', '$resource'];

    function penalityAlertcontroller($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, Notify, SocketService, $location, $route, $filter, ngTableParams, $modal, $resource) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'penalityAlertcontroller';

        activate();

        function activate() {

            // open dialog window
            $scope.totalDisplayed=100;
            var url = 'api/penalityAlert/penalityManagement';
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
        $scope.viewpenalityalert = function (customeralert_gid) {
            localStorage.setItem('penalityalert_gid', customeralert_gid);
            $state.go('app.penalityAlertView')
        }

        $scope.startpenalityalert = function (customeralert_gid) {
            var params = {
                customeralert_gid: customeralert_gid
            };
            console.log(params);
            var modalInstance = $modal.open({
                templateUrl: '/penalityAlertMailContent.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var url = "api/penalityAlert/Getcustomerpenalitydetails";
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.customerdetails = resp.data;
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.sendStartPenalityAlert = function () {
                    var url = "api/penalityAlert/startpenalityalert";
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                            activate();
                            unlockUI();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'Warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                        }

                    });
                }
            }
        }

        $scope.endpenalityalert = function (customeralert_gid) {
            var params = {
                customeralert_gid: customeralert_gid
            };

            var url = "api/penalityAlert/Getpenalityrecorddetails"
            SocketService.getparams(url, params).then(function (resp) {
                console.log(resp);
                if (resp.data.status == false) {
                     
                    alert(resp.data.message);
                    return resp.data.message;
                }
                else {

                    var modalInstance = $modal.open({
                        templateUrl: '/endpenalityAlertMailContent.html',
                        controller: ModalInstanceCtrl,
                        size: 'md'
                    });
                    ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
                    function ModalInstanceCtrl($scope, $modalInstance) {

                        $scope.customerdetails = resp.data;

                        $scope.ok = function () {
                            $modalInstance.close('closed');
                        };

                        $scope.sendendPenalityAlert = function () {
                            lockUI();
                            var url = "api/penalityAlert/endpenalityalert";
                            SocketService.post(url, params).then(function (resp) {
                                if (resp.data.status == true) {

                                    Notify.alert(resp.data.message, {
                                        status: 'success',
                                        pos: 'top-center',
                                        timeout: 3000
                                    });
                                    $modalInstance.close('closed');
                                    activate();
                                    unlockUI();
                                }
                                else {
                                    Notify.alert(resp.data.message, {
                                        status: 'Warning',
                                        pos: 'top-center',
                                        timeout: 3000
                                    });
                                    unlockUI();
                                }
                            });
                        }
                    }
                }
            });

        }
    }
})();
