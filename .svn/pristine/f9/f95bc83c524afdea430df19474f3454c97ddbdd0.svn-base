(function () {
    'use strict';

    angular
        .module('angle')
        .controller('holidayCalendercontroller', holidayCalendercontroller);

    holidayCalendercontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function holidayCalendercontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'holidayCalendercontroller';

        activate();

        function activate() {
            var url = "api/holidayCalender/holidaycalender";
            SocketService.get(url).then(function (resp) {
                $scope.holidaycalender_list = resp.data.holidaycalender_list;
            });

            var url = "api/holidayCalender/event";
            SocketService.get(url).then(function (resp) {
                $scope.createeventdata = resp.data.createevent;
            });

            var url = "api/holidayCalender/todayactivity"
            SocketService.get(url).then(function (resp) {
                $scope.todayschdule_details = resp.data.createevent;
            });
        }

        $scope.createevent = function () {
            var modalInstance = $modal.open({
                templateUrl: '/myModalContent.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.eventsubmit = function () {
                    var sdate = new Date();
                    sdate.setFullYear($scope.event_date.getFullYear());
                    sdate.setMonth($scope.event_date.getMonth());
                    sdate.setDate($scope.event_date.getDate());
  
                    var eventtime;
                    if ($scope.eventtime == undefined) {
                        var today = new Date();
                        eventtime = today.getHours() + ":" + today.getMinutes();
                    }
                    else {
                       var time = $scope.eventtime;
                        eventtime = time.getHours() + ":" + time.getMinutes();
                    }

                    var params = {
                        event_date: sdate,
                        event_title: $scope.event_title,
                        event_time: eventtime
                    }
                    console.log(params);
                    //var event_date = $scope.event_date
                    // console.log(event_date);
                    // var date = new event_date();
                    // var d = date.getDate(),
                    //     m = date.getMonth(),
                    //     y = date.getFullYear();
                    // console.log(d);
                    // console.log(date);
                    // console.log(m);
                    // console.log(y);

                    var url = 'api/holidayCalender/createevent';

                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, 
                                {
                                    status: 'success',
                                    pos: 'top-center',
                                    timeout: 3000
                                });//activate();
                            $state.reload();

                        }
                        else {
                            Notify.alert('Error Occurred!', 'warning')
                             
                        }
                    });

                }

            }
        }

    }
})();
