(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AtmTrnAssignedQueryController', AtmTrnAssignedQueryController);

    AtmTrnAssignedQueryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams', '$sce'];

    function AtmTrnAssignedQueryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams, $sce) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AtmTrnAssignedQueryController';
        $scope.auditcreation_gid = $location.search().auditcreation_gid;
        var auditcreation_gid = $scope.auditcreation_gid;
        activate();
        function activate() {

            var params = {
                auditcreation_gid: auditcreation_gid,

            };

            var url = 'api/AtmTrnMyAuditTask/GetAssignedQuerySummary';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.AssignedQueryList = resp.data.AssignedQueryList;
                unlockUI();
            });


            var url = 'api/AtmTrnSampling/GetSample';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI()
                auditcreation_gid = resp.data.auditcreation_gid
                $scope.sample_list = resp.data.sample_list

            });


            //var url = "api//";
            //SocketService.get(url).then(function (resp) {
            //    $scope.assigned_count = resp.data.assigned_count;
            //    $scope.reply_count = resp.data.reply_count;
            //    $scope.close_count = resp.data.close_count;
            //    unlockUI();
            //});


        }
        

        //$scope.Reply = function (val1) {
        //    var auditcreation_gid = $scope.auditcreation_gid;
        //     $location.url('app.AtmTrnReplyToQuery?auditcreation_gid=' + auditcreation_gid + '&raisequery_gid=' + val1);
        //}

        $scope.Reply = function () {
            var auditcreation_gid = $scope.auditcreation_gid;
            $location.url('app/AtmTrnReplyToQuery?auditcreation_gid=' + auditcreation_gid);
        }

        $scope.replytoquery = function (raisequery_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/replytoquery.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    raisequery_gid: raisequery_gid
                }
                var url = 'api/AtmTrnMyAuditTask/EditAssignedQuery';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.raisequery_gid = resp.data.raisequery_gid

                });


                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                

                $scope.submit = function () {

                    var params = {
                        raisequery_gid: $scope.raisequery_gid,
                        reply_query: $scope.txtdescription,

                    }

                    var url = 'api/AtmTrnMyAuditTask/PostReplyToQuery';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $location.url('app/AtmTrnAssignedQuery?auditcreation_gid=' + val)
                            $modalInstance.close('closed');
                            activate();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }
                    });

                    $modalInstance.close('closed');

                }

            }
        }


        //$scope.showPopover = function (raisequery_gid) {
        //    var modalInstance = $modal.open({
        //        templateUrl: '/showemployee.html',
        //        controller: ModalInstanceCtrl,
        //        backdrop: 'static',
        //        keyboard: false,
        //        size: 'md'
        //    });
        //    ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
        //    function ModalInstanceCtrl($scope, $modalInstance) {
        //        var params = {
        //            raisequery_gid: raisequery_gid
        //        }
        //        lockUI();
        //        var url = 'api/AtmTrnMyAuditTask/GetEmployeeName';
        //        SocketService.getparams(url, params).then(function (resp) {
        //            unlockUI();
        //            $scope.employee_name = resp.data.employee_name;
        //        });
        //        $scope.ok = function () {
        //            $modalInstance.close('closed');
        //        };
        //    }
        //}


        $scope.back = function (val) {
            $state.go('app.AtmTrnMyAuditTaskSummary');
        }

    }
})();
