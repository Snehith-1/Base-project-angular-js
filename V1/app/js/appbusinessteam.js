// JavaScript source code
// JavaScript source code
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('EnquiryRequireController', EnquiryRequireController);

    EnquiryRequireController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function EnquiryRequireController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'EnquiryRequireController';
        activate();


        function activate() {

            var url = 'api/MstEnquiryRequire/GetEnquiryRequire';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.enquiryrequire_list = resp.data.enquiryrequire_list;
                unlockUI();
            });
        }

        $scope.popupenquiryrequire = function () {
            var modalInstance = $modal.open({
                templateUrl: '/myModalContent.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.enquiryrequireSubmit = function () {
                    var params = {
                        enquiryrequire_name: $scope.txtenquiryrequire,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code
                    }

                    var url = 'api/MstEnquiryRequire/CreateEnquiryRequire';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        else {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }

                        activate();

                    });
                    $modalInstance.close('closed');

                }

            }
        }

        $scope.editenquiryrequire = function (enquiryrequire_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editenquiryrequire.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    enquiryrequire_gid: enquiryrequire_gid
                }
                var url = 'api/MstEnquiryRequire/EditEnquiryRequire';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.txtenquiryrequire = resp.data.enquiryrequire_name;
                    $scope.enquiryrequire_gid = resp.data.enquiryrequire_gid;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.enquiryrequireUpdate = function () {

                    var url = 'api/MstEnquiryRequire/UpdateEnquiryRequire';
                    var params = {
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        enquiryrequire_name: $scope.txtenquiryrequire,
                        enquiryrequire_gid: $scope.enquiryrequire_gid
                    }
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });


                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }

                        activate();

                    });
                    $modalInstance.close('closed');
                }

            }
        }

        $scope.Status_update = function (enquiryrequire_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusaudittype.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    enquiryrequire_gid: enquiryrequire_gid
                }
                var url = 'api/MstEnquiryRequire/EditEnquiryRequire';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.enquiryrequire_gid = resp.data.enquiryrequire_gid
                    $scope.txtenquiryrequire = resp.data.enquiryrequire_name;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        enquiryrequire_name: $scope.txtenquiryrequire,
                        enquiryrequire_gid: $scope.enquiryrequire_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/MstEnquiryRequire/InactiveEnquiryRequire';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'info',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        activate();
                    });

                    $modalInstance.close('closed');

                }
                var param = {
                    enquiryrequire_gid: enquiryrequire_gid
                }

                var url = 'api/MstEnquiryRequire/EnquiryRequireInactiveLogview';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.enquiryrequireinactivelog_data = resp.data.enquiryrequire_list;
                    unlockUI();
                });
            }
        }


        $scope.deleteenquiryrequire = function (enquiryrequire_gid) {
            var params = {
                enquiryrequire_gid: enquiryrequire_gid
            }
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do You Want To Delete the Record ?',
                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Yes, delete it!',
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    var url = 'api/MstEnquiryRequire/DeleteEnquiryRequire';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            SweetAlert.swal('Deleted Successfully!');

                            activate();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                    });
                }

            });
        };
    }

})();
// JavaScript source code
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('LeadRequireController', LeadRequireController);

    LeadRequireController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function LeadRequireController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'LeadRequireController';
        activate();


        function activate() {

            var url = 'api/MarMstLeadRequire/GetLeadRequire';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.leadrequire_list = resp.data.leadrequire_list;
                unlockUI();
            });
        }

        $scope.popupleadrequire = function () {
            var modalInstance = $modal.open({
                templateUrl: '/myModalContent.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.leadrequireSubmit = function () {
                    var params = {
                        leadrequire_name: $scope.txtleadrequire,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code
                    }

                    var url = 'api/MarMstLeadRequire/CreateLeadRequire';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        else {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }

                        activate();

                    });
                    $modalInstance.close('closed');

                }

            }
        }

        $scope.editleadrequire = function (leadrequire_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editleadrequire.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    leadrequire_gid: leadrequire_gid
                }
                var url = 'api/MarMstLeadRequire/EditLeadRequire';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.txtleadrequire = resp.data.leadrequire_name;
                    $scope.leadrequire_gid = resp.data.leadrequire_gid;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.leadrequireUpdate = function () {

                    var url = 'api/MarMstLeadRequire/UpdateLeadRequire';
                    var params = {
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        leadrequire_name: $scope.txtleadrequire,
                        leadrequire_gid: $scope.leadrequire_gid
                    }
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });


                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }

                        activate();

                    });
                    $modalInstance.close('closed');
                }

            }
        }

        $scope.Status_update = function (leadrequire_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusaudittype.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    leadrequire_gid: leadrequire_gid
                }
                var url = 'api/MarMstLeadRequire/EditLeadRequire';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.leadrequire_gid = resp.data.leadrequire_gid
                    $scope.txtleadrequire = resp.data.leadrequire_name;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        leadrequire_name: $scope.txtleadrequire,
                        leadrequire_gid: $scope.leadrequire_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/MarMstLeadRequire/InactiveLeadRequire';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'info',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        activate();
                    });

                    $modalInstance.close('closed');

                }
                var param = {
                    leadrequire_gid: leadrequire_gid
                }

                var url = 'api/MarMstLeadRequire/LeadRequireInactiveLogview';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.leadrequireinactivelog_data = resp.data.leadrequire_list;
                    unlockUI();
                });
            }
        }

     
        $scope.deleteleadrequire = function (leadrequire_gid) {
            var params = {
                leadrequire_gid: leadrequire_gid
            }
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do You Want To Delete the Record ?',
                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Yes, delete it!',
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    var url = 'api/MarMstLeadRequire/DeleteLeadRequire';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            SweetAlert.swal('Deleted Successfully!');

                            activate();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                    });
                }

            });
        };
    }

})();
// JavaScript source code
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MilletRequireController', MilletRequireController);

        MilletRequireController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function MilletRequireController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MilletRequireController';
        activate();


        function activate() {

            var url = 'api/MarMstMilletRequire/GetMilletRequire';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.milletrequire_list = resp.data.milletrequire_list;
                unlockUI();
            });
        }

        $scope.popupmilletrequire = function () {
            var modalInstance = $modal.open({
                templateUrl: '/myModalContent.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.milletrequireSubmit = function () {
                    var params = {
                        milletrequire_name: $scope.txtmilletrequire,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code
                    }

                    var url = 'api/MarMstMilletRequire/CreateMilletRequire';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        else {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }

                        activate();

                    });
                    $modalInstance.close('closed');

                }

            }
        }

        $scope.editmilletrequire = function (milletrequire_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editmilletrequire.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    milletrequire_gid: milletrequire_gid
                }
                var url = 'api/MarMstMilletRequire/EditMilletRequire';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.txtmilletrequire = resp.data.milletrequire_name;
                    $scope.milletrequire_gid = resp.data.milletrequire_gid;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.milletrequireUpdate = function () {

                    var url = 'api/MarMstMilletRequire/UpdateMilletRequire';
                    var params = {
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        milletrequire_name: $scope.txtmilletrequire,
                        milletrequire_gid: $scope.milletrequire_gid
                    }
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });


                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }

                        activate();

                    });
                    $modalInstance.close('closed');
                }

            }
        }

        $scope.Status_update = function (milletrequire_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusaudittype.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    milletrequire_gid: milletrequire_gid
                }
                var url = 'api/MarMstMilletRequire/EditMilletRequire';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.milletrequire_gid = resp.data.milletrequire_gid
                    $scope.txtmilletrequire = resp.data.milletrequire_name;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        milletrequire_name: $scope.txtmilletrequire,
                        milletrequire_gid: $scope.milletrequire_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/MarMstMilletRequire/InactiveMilletRequire';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'info',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        activate();
                    });

                    $modalInstance.close('closed');

                }
                var param = {
                    milletrequire_gid: milletrequire_gid
                }

                var url = 'api/MarMstMilletRequire/MilletRequireInactiveLogview';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.milletrequireinactivelog_data = resp.data.milletrequire_list;
                    unlockUI();
                });
            }
        }

     
        $scope.deletemilletrequire = function (milletrequire_gid) {
            var params = {
                milletrequire_gid: milletrequire_gid
            }
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do You Want To Delete the Record ?',
                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Yes, delete it!',
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    var url = 'api/MarMstMilletRequire/DeleteMilletRequire';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            SweetAlert.swal('Deleted Successfully!');

                            activate();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                    });
                }

            });
        };
    }

})();
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstAssignedMarketingSummaryController', MstAssignedMarketingSummaryController);

    MstAssignedMarketingSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'cmnfunctionService'];

    function MstAssignedMarketingSummaryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstAssignedMarketingSummaryController';

        activate();

        function activate() {
            var url = "api/Marketing/MarketingAssignedCallCount";
            SocketService.get(url).then(function (resp) {
                $scope.assignedcall_count = resp.data.assignedcall_count;
                $scope.completedcall_count = resp.data.completedcall_count;
                $scope.followupcall_count = resp.data.followupcall_count;
                $scope.closedcall_count = resp.data.closedcall_count;
                $scope.rejectedcall_count = resp.data.rejectedcall_count;
                unlockUI();
            });

            var url = 'api/Marketing/GetAssignedCallSummary';
            SocketService.get(url).then(function (resp) {
                $scope.marketingassignedcall_list = resp.data.MarketingCall_list;
                unlockUI();
            });
        }

       
        $scope.closed_call = function () {
            $location.url("app/MstClosedMarketingSummary");
        }
        $scope.followup_call = function () {
            $location.url("app/MstFollowUpMarketingSummary");
        }
        $scope.assigned_call = function () {
            $location.url("app/MstAssignedMarketingSummary");
        }
        $scope.completed_call = function () {
            $location.url("app/MstCompletedMarketingSummary");
        }

        $scope.view = function (marketingcall_gid) {
            $location.url('app/MstMarketingAssignView?hash=' + cmnfunctionService.encryptURL('lsmarketingcall_gid=' + marketingcall_gid + '&lspage=AssignedMarketing'));
        }
        $scope.rejected_call = function () {
            $location.url("app/MstRejectedMarketingSummary");
        }
       

        $scope.transfer = function (marketingcall_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/transferContent.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];
            function ModalInstanceCtrl($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {


                var params = {
                    marketingcall_gid: marketingcall_gid
                }
                var url = 'api/Marketing/MarketingCallDetailsForTransfer';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.marketingcall_gid = resp.data.marketingcall_gid;
                    $scope.ticket_refid = resp.data.ticket_refid;
                    $scope.assignemployee_gid = resp.data.assignemployee_gid;
                    $scope.assignemployee_name = resp.data.assignemployee_name;
                    $scope.ibcalltransfer_list = resp.data.MarketingCalltransfer_list;
                });

                var url = 'api/OsdTrnCustomerQueryMgmt/TransferEmployee';
                SocketService.get(url).then(function (resp) {
                    $scope.TransferEmployeeList = resp.data.TransferEmployeeList;
                    unlockUI();
                });

                /*      $scope.marketingcall_gid = marketingcall_gid;
                      $scope.ticketref_no = ticketref_no;
                      $scope.assigned_to = assigned_to; */

                $scope.transfer_call = function () {

                    if ($scope.cboTransferTo == undefined) {
                        modalInstance.close('closed');
                        Notify.alert('Kindly Select the Transfer to Person', 'warning');
                        return;
                    }

                    var params = {
                        marketingcall_gid: $scope.marketingcall_gid,
                        ticket_refid: $scope.ticket_refid,
                        transferfrom_gid: $scope.assignemployee_gid,
                        transferfrom_name: $scope.assignemployee_name,
                        transferto_gid: $scope.cboTransferTo,
                        transferto_name: $('#transfer_to :selected').text(),
                        transfer_remarks: $scope.transfer_remarks
                    }

                    var url = "api/Marketing/MarketingCallTransferEmployee";
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status = true) {
                            modalInstance.close('closed');
                            Notify.alert(resp.data.message, 'success');
                            activate();
                        }
                        else {
                            modalInstance.close('closed');
                            Notify.alert(resp.data.message, 'warning');
                            activate();
                        }
                    });


                }
                /*     var url = 'api/MstTelecalling/TransferLog';
                     var params = {
                         marketingcall_gid:marketingcall_gid
                     }
                     SocketService.getparams(url, params).then(function (resp) {
                             $scope.TransferLog = resp.data.TransferLog;
                     });  */
                $scope.close = function () {
                    modalInstance.close('closed');
                };
            }
        }

    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstAssignedMarketingViewController', MstAssignedMarketingViewController);

    MstAssignedMarketingViewController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'DownloaddocumentService','cmnfunctionService'];

    function MstAssignedMarketingViewController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstAssignedMarketingViewController';
       
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        var marketingcall_gid = searchObject.marketingcall_gid;
        var lspage = searchObject.lspage;

        $scope.transfershow = false;
        $scope.followupshow = false;
        $scope.closedshow = false;
        $scope.RejectedShow = false;

        lockUI();
        activate();
        function activate() {
            vm.calender1 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open1 = true;
            };
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };

            if (lspage == 'ClosedMarketing') {
                $scope.followupshow = false;
                $scope.completedshow = false;
                $scope.RejectedShow = true;
            }
            else if (lspage == 'FollowUpMarketing') {
                $scope.followupshow = true;
                $scope.completedshow = false;
                $scope.closedshow = false;
                $scope.RejectedShow = false;
            }
            else if (lspage == 'CompletedMarketing') {
                $scope.completedshow = true;
                $scope.closedshow = false;
                $scope.followupshow = false;
                $scope.RejectedShow = false;
            }
            else if (lspage == 'RejectedMarketing') {
                $scope.RejectedShow = true;
                $scope.closedshow = false;
                $scope.followupshow = false;
                $scope.completedshow = false;
            }
            else {

            }

            var params = {
                marketingcall_gid: marketingcall_gid
            }
            var url = 'api/Marketing/GetMarketingCallAssignedView';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtticket_refid = resp.data.ticket_refid,
                $scope.rdbsam_fin = resp.data.entity_name,
                $scope.txtsourceofcontact_name = resp.data.sourceofcontact_name,
                $scope.txtcallreceivednumber_name = resp.data.callreceivednumber_name,
                $scope.txtcustomer_type = resp.data.leadrequesttype_name,
                $scope.txtcallreceived_date = resp.data.callreceived_date,
                $scope.txtcaller_name = resp.data.caller_name,
                $scope.txtinternalreference_name = resp.data.internalreference_name,
                $scope.txtcallerassociate_company = resp.data.callerassociate_company,
                $scope.txtoffice_landlineno = resp.data.office_landlineno,
                $scope.txtcalltype_name = resp.data.calltype_name,
                $scope.txtfunction_name = resp.data.function_name,
                 $scope.txtfunction_remarks = resp.data.function_remarks,
                 $scope.txttat_hours = resp.data.tat_hours,
                $scope.txtrequirement = resp.data.requirement,
                $scope.txtenquiry_description = resp.data.enquiry_description,
                $scope.txtcallclosure_status = resp.data.callclosure_status,
                $scope.txtassignemployee_name = resp.data.assignemployee_name,
                $scope.txtassign_date = resp.data.assign_date,
                $scope.txttagemployee_name = resp.data.tagemployee_name,
                $scope.txtassignclosure_remarks = resp.data.assignclosure_remarks,
                    $scope.ibcalladdress_list = resp.data.MarketingCalladdress_list;
                $scope.ibcallfollowup_list = resp.data.MarketingCallfollowup_list,
                $scope.ibcallextendfollowup_list = resp.data.marketingcallextendfollowup_list,
                    $scope.ibcalltaggedmember_list = resp.data.MarketingCalltaggedmember_list;
                $scope.txtcompleted_by = resp.data.completed_by,
                $scope.txtcompleted_date = resp.data.completed_date,
                $scope.txtcompleted_remarks = resp.data.completed_remarks,
                $scope.txtclosed_date = resp.data.closed_date,
                $scope.txtclosed_by = resp.data.closed_by,
                $scope.txtclosed_remarks = resp.data.closed_remarks,
                $scope.txtrejected_date = resp.data.rejected_date,
                $scope.txtrejected_by = resp.data.rejected_by,
                $scope.txtrejected_remarks = resp.data.rejected_remarks,
                 $scope.txtfollowup_date = resp.data.followup_date,
                $scope.txtfollowup_time = resp.data.followup_time,
                $scope.txtfollowup_remarks = resp.data.followup_remarks,
                  $scope.txtloanproduct_name = resp.data.loanproduct_name,
                $scope.txtloansubproduct_name = resp.data.loansubproduct_name,
                $scope.txtloan_amount = resp.data.loan_amount,
              $scope.txtclosed = resp.data.closed,
              $scope.txtbase_location = resp.data.baselocation_name,
                  $scope.txtprimary_mobileno = resp.data.primary_mobileno,
                    $scope.ibcallmobileno_list = resp.data.MarketingCallmobileno_list;
                $scope.txtprimary_email = resp.data.primary_email,
                    $scope.ibcallemail_list = resp.data.MarketingCallemail_list;
                $scope.origination = resp.data.origination;
                $scope.txtleadrequire_name = resp.data.leadrequire_name,
                    $scope.txtmilletrequire_name = resp.data.milletrequire_name,
                    $scope.txtenquiryrequire_name = resp.data.enquiryrequire_name,
                    $scope.txtstartuprequire_name = resp.data.startuprequire_name,
                    $scope.txtbusiness_name = resp.data.business_name,
                    $scope.txtindustry_name = resp.data.industry_name,

                unlockUI();
            });

            var url = 'api/Marketing/MarketingCallProofDocumentList';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.lufilename = resp.data.filename;
                $scope.lufilepath = resp.data.filepath;
                $scope.Uploadcallproofdocument_list = resp.data.callproofupload_list;
                unlockUI();
            });
            var url = 'api/Marketing/MarketingCallRecordingDocumentList';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.Uploadcallrecordingdocument_list = resp.data.callproofupload_list;
                unlockUI();
            });
            var url = 'api/Marketing/GetMarketingCallLeadstatusList';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.MarketingCallLeadstatus_list = resp.data.MarketingCallLeadstatus_list;
                unlockUI();
            });
            var url = 'api/Marketing/GetDocumentList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.lsfilename = resp.data.filename;
                $scope.lsfilepath = resp.data.filepath;
                $scope.document_list = resp.data.document_list;
            });
            var url = 'api/Marketing/GetMilletDocumentList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.lmfilename = resp.data.filename;
                $scope.lmfilepath = resp.data.filepath;
                $scope.milletdocument_list = resp.data.milletdocument_list;
            });
            var url = 'api/Marketing/GetEnquiryDocumentList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.lmfilename = resp.data.filename;
                $scope.lmfilepath = resp.data.filepath;
                $scope.enquirydocument_list = resp.data.enquirydocument_list;
            });
        }
        $scope.document_downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.download_all = function (val1, val2) {

            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }

        }
        $scope.download_allupload = function (val1, val2) {

            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }

        }
        $scope.download_allmillet = function (val1, val2) {
            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }
        }
        $scope.milletdocument_downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.download_allenquiry = function (val1, val2) {
            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }
        }
        $scope.enquirydocument_downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.documentviewerenquiry = function (val1, val2) {
            lockUI();
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
            if (IsValidExtension == false) {
                Notify.alert("View is not supported for this format..!", {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                unlockUI();
                return false;
            }
            DownloaddocumentService.DocumentViewer(val1, val2);
        }
        $scope.rec_downloads = function (val1, val2) {
            //var phyPath = val1;
            //var relPath = phyPath.split("StoryboardAPI");
            //var relpath1 = relPath[1].replace("\\", "/");
            //var hosts = window.location.host;
            //var prefix = location.protocol + "//";
            //var str = prefix.concat(hosts, relpath1);
            //var link = document.createElement("a");
            //link.download = val2;
            //var uri = str;
            //link.href = uri;
            //link.click();
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.recproof_downloads = function (val1, val2) {
            //var phyPath = val1;
            //var relPath = phyPath.split("StoryboardAPI");
            //var relpath1 = relPath[1].replace("\\", "/");
            //var hosts = window.location.host;
            //var prefix = location.protocol + "//";
            //var str = prefix.concat(hosts, relpath1);
            //var link = document.createElement("a");
            //link.download = val2;
            //var uri = str;
            //link.href = uri;
            //link.click();
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.documentviewer = function (val1, val2) {
            lockUI();
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
            if (IsValidExtension == false) {
            Notify.alert("View is not supported for this format..!", {
             status: 'danger',
             pos: 'top-center',
             timeout: 3000
             });
             unlockUI();
           return false;
             }
             DownloaddocumentService.DocumentViewer(val1, val2);
             }
             $scope.documentviewermillet = function (val1, val2) {
                lockUI();
                var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
                if (IsValidExtension == false) {
                Notify.alert("View is not supported for this format..!", {
                 status: 'danger',
                 pos: 'top-center',
                 timeout: 3000
                 });
                 unlockUI();
               return false;
                 }
                 DownloaddocumentService.DocumentViewer(val1, val2);
                 }
                 $scope.documentviewerupload = function (val1, val2) {
                    lockUI();
                    var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
                    if (IsValidExtension == false) {
                    Notify.alert("View is not supported for this format..!", {
                     status: 'danger',
                     pos: 'top-center',
                     timeout: 3000
                     });
                     unlockUI();
                   return false;
                     }
                     DownloaddocumentService.DocumentViewer(val1, val2);
                     }
        $scope.Back = function () {
            if (lspage == 'ClosedMarketing') {
                $location.url('app/MstClosedMarketingSummary');
            }
            else if (lspage == 'FollowUpMarketing') {
                $state.go('app.MstFollowUpMarketingSummary');
            }
            else if (lspage == 'CompletedMarketing') {
                $state.go('app.MstCompletedMarketingSummary');
            }
            else if (lspage == 'RejectedMarketing') {
                $state.go('app.MstRejectedMarketingSummary');
            }
            else {

            }
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstBDLeadRequestTypeController', MstBDLeadRequestTypeController);

    MstBDLeadRequestTypeController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstBDLeadRequestTypeController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstBDLeadRequestTypeController';

        activate();

        function activate() {
            var url = 'api/MstLeadRequestType/GetLeadRequestType';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.leadrequesttype_list = resp.data.leadrequesttype_list;
                unlockUI();
            });
        }
        $scope.addleadrequesttype = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addleadrequesttype.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.back = function () {
                    $modalInstance.close('closed');
                };
                $scope.submit = function () {
                    var params = {
                        leadrequesttype_name: $scope.txtleadrequesttype_name,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code
                    }
                    var url = 'api/MstLeadRequestType/CreateLeadRequestType';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                            activate();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                            activate();
                        }
                    });
                }

            }
        }
        $scope.editleadrequesttype = function (leadrequesttype_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editleadrequesttype.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.back = function () {
                    $modalInstance.close('closed');
                };
                var params = {
                    leadrequesttype_gid: leadrequesttype_gid
                }
                var url = 'api/MstLeadRequestType/EditLeadRequestType';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txteditleadrequesttype_name = resp.data.leadrequesttype_name;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.leadrequesttype_gid = resp.data.leadrequesttype_gid;
                });
                $scope.back = function () {
                    $modalInstance.close('closed');
                };
                $scope.update = function () {
                    var url = 'api/MstLeadRequestType/UpdateLeadRequestType';
                    var params = {
                        leadrequesttype_name: $scope.txteditleadrequesttype_name,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        leadrequesttype_gid: $scope.leadrequesttype_gid
                    }
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                        else {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                    });
                }
            }
        }
        $scope.Status_update = function (leadrequesttype_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusleadrequesttype.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    leadrequesttype_gid: leadrequesttype_gid
                }
                var url = 'api/MstLeadRequestType/EditLeadRequestType';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.leadrequesttype_gid = resp.data.leadrequesttype_gid
                    $scope.txtleadrequesttype_name = resp.data.leadrequesttype_name;
                    $scope.rbo_status = resp.data.Status;
                });
                $scope.back = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {
                    var params = {
                        leadrequesttype_gid: leadrequesttype_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status
                    }
                    var url = 'api/MstLeadRequestType/InactiveLeadRequestType';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        } activate();
                    });
                    $modalInstance.close('closed');
                }
                var params = {
                    leadrequesttype_gid: leadrequesttype_gid
                }
                var url = 'api/MstLeadRequestType/InactiveLeadRequestTypeHistory';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.leadrequesttypeinactivehistory_list = resp.data.leadrequesttypeinactivehistory_list;
                    unlockUI();
                });
            }
        }
        $scope.delete = function (leadrequesttype_gid) {
            var params = {
                leadrequesttype_gid: leadrequesttype_gid
            }
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do You Want To Delete the Record ?',
                showSpinner: true,
               // showCancelButton: true,
                CancelButtonColor: '#dedad9',
                showCancelButton: true,
                confirmButtonColor: '#d64b3c',
                confirmButtonText: 'Yes, delete it!',
              //  showConfirmButton: true,
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    var url = 'api/MstLeadRequestType/DeleteLeadRequestType';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            SweetAlert.swal('Deleted Successfully!');
                            activate();
                        }
                        
  else if (resp.data.status == false) {
    SweetAlert.swal({
        title: 'Warning!',
        text: "Can't able to delete Lead Request Type because it is mapped to add Business Development call",
        timer: 5000,       
        showCancelButton: false,
        showConfirmButton: false,        
        backgroundcolor: '#d64b3c'
});
    activate();
    }

                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                    });
                }
            });
        }
    }
})();
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstClosedMarketingSummaryController', MstClosedMarketingSummaryController);

    MstClosedMarketingSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'cmnfunctionService'];

    function MstClosedMarketingSummaryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstClosedMarketingSummaryController';

        activate();

        function activate() {
            var url = "api/Marketing/MarketingAssignedCallCount";
            SocketService.get(url).then(function (resp) {
                $scope.assignedcall_count = resp.data.assignedcall_count;
                $scope.completedcall_count = resp.data.completedcall_count;
                $scope.followupcall_count = resp.data.followupcall_count;
                $scope.closedcall_count = resp.data.closedcall_count;
                $scope.rejectedcall_count = resp.data.rejectedcall_count;
                unlockUI();
            });
            var url = 'api/Marketing/GetClosedCallSummary';
            SocketService.get(url).then(function (resp) {
                $scope.marketingclosedcall_list = resp.data.MarketingCall_list;
                unlockUI();
            });
        }
       
        $scope.closed_call = function () {
            $location.url("app/MstClosedMarketingSummary");
        }
        $scope.followup_call = function () {
            $location.url("app/MstFollowUpMarketingSummary");
        }
        $scope.assigned_call = function () {
            $location.url("app/MstAssignedMarketingSummary");
        }
        $scope.completed_call = function () {
            $location.url("app/MstCompletedMarketingSummary");
        }
       
        $scope.view = function (marketingcall_gid) {
            $location.url('app/MstAssignedMarketingView?hash=' + cmnfunctionService.encryptURL('marketingcall_gid=' + marketingcall_gid + '&lspage=ClosedMarketing'));
        }

        $scope.rejected_call = function () {
            $location.url("app/MstRejectedMarketingSummary");
        }

    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCompletedMarketingSummaryController', MstCompletedMarketingSummaryController);

    MstCompletedMarketingSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'cmnfunctionService'];

    function MstCompletedMarketingSummaryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCompletedMarketingSummaryController';

        activate();

        function activate() {
            var url = "api/Marketing/MarketingAssignedCallCount";
            SocketService.get(url).then(function (resp) {
                $scope.assignedcall_count = resp.data.assignedcall_count;
                $scope.completedcall_count = resp.data.completedcall_count;
                $scope.followupcall_count = resp.data.followupcall_count;
                $scope.closedcall_count = resp.data.closedcall_count;
                $scope.rejectedcall_count = resp.data.rejectedcall_count;
                unlockUI();
            });

            var url = 'api/Marketing/GetCompletedCallSummary';
            SocketService.get(url).then(function (resp) {
                $scope.marketingcompletedcall_list = resp.data.MarketingCall_list;
                unlockUI();
            });
        }


        $scope.closed_call = function () {
            $location.url("app/MstClosedMarketingSummary");
        }
        $scope.followup_call = function () {
            $location.url("app/MstFollowUpMarketingSummary");
        }
        $scope.assigned_call = function () {
            $location.url("app/MstAssignedMarketingSummary");
        }
        $scope.completed_call = function () {
            $location.url("app/MstCompletedMarketingSummary");
        }

        $scope.view = function (marketingcall_gid) {
            $location.url('app/MstAssignedMarketingView?hash=' + cmnfunctionService.encryptURL('marketingcall_gid=' + marketingcall_gid + '&lspage=CompletedMarketing'));
        }
        $scope.rejected_call = function () {
            $location.url("app/MstRejectedMarketingSummary");
        }

    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstFollowUpMarketingSummaryController', MstFollowUpMarketingSummaryController);

    MstFollowUpMarketingSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'cmnfunctionService'];

    function MstFollowUpMarketingSummaryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstFollowUpMarketingSummaryController';

        activate();

        function activate() {
            var url = "api/Marketing/MarketingAssignedCallCount";
            SocketService.get(url).then(function (resp) {
                $scope.assignedcall_count = resp.data.assignedcall_count;
                $scope.completedcall_count = resp.data.completedcall_count;
                $scope.followupcall_count = resp.data.followupcall_count;
                $scope.closedcall_count = resp.data.closedcall_count;
                $scope.rejectedcall_count = resp.data.rejectedcall_count;
                unlockUI();
            });

            var url = 'api/Marketing/GetFollowUpCallSummary';
            SocketService.get(url).then(function (resp) {
                $scope.ibfollowupcall_list = resp.data.ibcall_list;
                unlockUI();
            });
        }


        $scope.closed_call = function () {
            $location.url("app/MstClosedMarketingSummary");
        }
        $scope.followup_call = function () {
            $location.url("app/MstFollowUpMarketingSummary");
        }
        $scope.assigned_call = function () {
            $location.url("app/MstAssignedMarketingSummary");
        }
        $scope.completed_call = function () {
            $location.url("app/MstCompletedMarketingSummary");
        }

        $scope.view = function (marketingcall_gid) {
            $location.url('app/MstAssignedMarketingView?hash=' + cmnfunctionService.encryptURL('marketingcall_gid=' + marketingcall_gid + '&lspage=FollowUpMarketing'));
        }

        $scope.rejected_call = function () {
            $location.url("app/MstRejectedRejectedCallSummary");
        }


        $scope.transfer = function (marketingcall_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/transferContent.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];
            function ModalInstanceCtrl($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {


                var params = {
                    marketingcall_gid: marketingcall_gid
                }
                var url = 'api/Marketing/MarketingCallDetailsForTransfer';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.marketingcall_gid = resp.data.marketingcall_gid;
                    $scope.ticket_refid = resp.data.ticket_refid;
                    $scope.assignemployee_gid = resp.data.assignemployee_gid;
                    $scope.assignemployee_name = resp.data.assignemployee_name;
                    $scope.ibcalltransfer_list = resp.data.ibcalltransfer_list;
                });

                var url = 'api/OsdTrnCustomerQueryMgmt/TransferEmployee';
                SocketService.get(url).then(function (resp) {
                    $scope.TransferEmployeeList = resp.data.TransferEmployeeList;
                    unlockUI();
                });

                /*      $scope.inboundcall_gid = inboundcall_gid;
                      $scope.ticketref_no = ticketref_no;
                      $scope.assigned_to = assigned_to; */

                $scope.transfer_call = function () {

                    if ($scope.cboTransferTo == undefined) {
                        modalInstance.close('closed');
                        Notify.alert('Kindly Select the Transfer to Person', 'warning');
                        return;
                    }

                    var params = {
                        marketingcall_gid: $scope.marketingcall_gid,
                        ticket_refid: $scope.ticket_refid,
                        transferfrom_gid: $scope.assignemployee_gid,
                        transferfrom_name: $scope.assignemployee_name,
                        transferto_gid: $scope.cboTransferTo,
                        transferto_name: $('#transfer_to :selected').text(),
                        transfer_remarks: $scope.transfer_remarks
                    }

                    var url = "api/Marketing/MarketingCallTransferEmployee";
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status = true) {
                            modalInstance.close('closed');
                            Notify.alert(resp.data.message, 'success');
                            activate();
                        }
                        else {
                            modalInstance.close('closed');
                            Notify.alert(resp.data.message, 'warning');
                            activate();
                        }
                    });


                }
                /*     var url = 'api/MstTelecalling/TransferLog';
                     var params = {
                         inboundcall_gid:inboundcall_gid
                     }
                     SocketService.getparams(url, params).then(function (resp) {
                             $scope.TransferLog = resp.data.TransferLog;
                     });  */
                $scope.close = function () {
                    modalInstance.close('closed');
                };
            }
        }

    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstFollowUpMarketingSummaryController', MstFollowUpMarketingSummaryController);

    MstFollowUpMarketingSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'cmnfunctionService'];

    function MstFollowUpMarketingSummaryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstFollowUpMarketingSummaryController';

        activate();
        lockUI();
        function activate() {
            var url = 'api/Marketing/GetFollowUpCallSummary';
           SocketService.get(url).then(function (resp) {
               $scope.ibcallfollowup_list = resp.data.MarketingCall_list;
               unlockUI();
           }); 
           var url = "api/Marketing/MarketingAssignedCallCount";
           SocketService.get(url).then(function (resp) {
               $scope.assignedcall_count = resp.data.assignedcall_count;
               $scope.completedcall_count = resp.data.completedcall_count;
               $scope.followupcall_count = resp.data.followupcall_count;
               $scope.closedcall_count = resp.data.closedcall_count;
               $scope.rejectedcall_count = resp.data.rejectedcall_count;
               unlockUI();
           });
        }
        $scope.closed_call = function () {
            $location.url("app/MstClosedMarketingSummary");
        }
        $scope.followup_call = function () {
            $location.url("app/MstFollowUpMarketingSummary");
        }
        $scope.assigned_call = function () {
            $location.url("app/MstAssignedMarketingSummary");
        }
        $scope.completed_call = function () {
            $location.url("app/MstCompletedMarketingSummary");
        }

        $scope.call_response = function (marketingcall_gid) {
            $location.url('app/MstCallResponse?hash=' + cmnfunctionService.encryptURL('marketingcall_gid=' + marketingcall_gid));
        }
        $scope.view = function (marketingcall_gid) {
            $location.url('app/MstAssignedMarketingView?hash=' + cmnfunctionService.encryptURL('marketingcall_gid=' + marketingcall_gid + '&lspage=FollowUpMarketing'));
        }
        $scope.edit = function (marketingcall_gid) {
            $location.url('app/MstMarketingEdit?hash=' + cmnfunctionService.encryptURL('marketingcall_gid=' + marketingcall_gid));
        }
        $scope.work_inprogress = function () {
            $location.url("app/MstWorkInprogressCallSummary");
        }
       

        $scope.rejected_call = function () {
            $location.url("app/MstRejectedMarketingSummary");
        }
        $scope.transfer = function (marketingcall_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/transferContent.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];
            function ModalInstanceCtrl($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {


                var params = {
                    marketingcall_gid: marketingcall_gid
                }
                var url = 'api/Marketing/MarketingCallDetailsForTransfer';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.marketingcall_gid = resp.data.marketingcall_gid;
                    $scope.ticket_refid = resp.data.ticket_refid;
                    $scope.assignemployee_gid = resp.data.assignemployee_gid;
                    $scope.assignemployee_name = resp.data.assignemployee_name;
                    $scope.ibcalltransfer_list = resp.data.MarketingCalltransfer_list;
                });

                var url = 'api/OsdTrnCustomerQueryMgmt/TransferEmployee';
                SocketService.get(url).then(function (resp) {
                    $scope.TransferEmployeeList = resp.data.TransferEmployeeList;
                    unlockUI();
                });

                /*      $scope.marketingcall_gid = marketingcall_gid;
                      $scope.ticketref_no = ticketref_no;
                      $scope.assigned_to = assigned_to; */

                $scope.transfer_call = function () {

                    if ($scope.cboTransferTo == undefined) {
                        modalInstance.close('closed');
                        Notify.alert('Kindly Select the Transfer to Person', 'warning');
                        return;
                    }

                    var params = {
                        marketingcall_gid: $scope.marketingcall_gid,
                        ticket_refid: $scope.ticket_refid,
                        transferfrom_gid: $scope.assignemployee_gid,
                        transferfrom_name: $scope.assignemployee_name,
                        transferto_gid: $scope.cboTransferTo,
                        transferto_name: $('#transfer_to :selected').text(),
                        transfer_remarks: $scope.transfer_remarks
                    }

                    var url = "api/Marketing/MarketingCallTransferEmployee";
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status = true) {
                            modalInstance.close('closed');
                            Notify.alert(resp.data.message, 'success');
                            activate();
                        }
                        else {
                            modalInstance.close('closed');
                            Notify.alert(resp.data.message, 'warning');
                            activate();
                        }
                    });


                }
                /*     var url = 'api/MstTelecalling/TransferLog';
                     var params = {
                         marketingcall_gid:marketingcall_gid
                     }
                     SocketService.getparams(url, params).then(function (resp) {
                             $scope.TransferLog = resp.data.TransferLog;
                     });  */
                $scope.close = function () {
                    modalInstance.close('closed');
                };
            }
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstMarketingAddController', MstMarketingAddController);

    MstMarketingAddController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams','cmnfunctionService'];

    function MstMarketingAddController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstMarketingAddController';
        activate();

        function activate() {
            vm.calender1 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open1 = true;
            };
            vm.calender2 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open2 = true;
            };
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };
            
            var url = 'api/Marketing/MarketingCallTempClear';
            SocketService.get(url).then(function (resp) {
            });

            $scope.cbointernal_reference="NA";
            var d = new Date();
            var time = d.toLocaleString([], { hour: '2-digit', minute: '2-digit' });


            var today = new Date();
            var date = 0 + today.getDate() + '-' +(today.getMonth() + 1) + '-' + today.getFullYear();
            var todaytime = date + ' ' + '/' + ' ' + time;
            $scope.txtcallreceived_date = todaytime;

            $scope.minDate = new Date();
            var d = new Date();
            var time = d.toLocaleString([], { hour: '2-digit', minute: '2-digit' });
           
            $scope.txtcallreceived_time = time;

            var url = 'api/Marketing/GetEntity';
            SocketService.get(url).then(function (resp) {
                $scope.entity_list = resp.data.inboundentity_list;
            });
            var url = 'api/employee/Employee';
            SocketService.get(url).then(function (resp) {
                $scope.employee_list = resp.data.employee_list;
            });
            var url = 'api/Marketing/GetMarketingSourceofContact';
            SocketService.get(url).then(function (resp) {
                $scope.sourceofcontact_list = resp.data.MarketingSourceofContact_list;
            });
            var url = 'api/Marketing/GetMarketingCallReceivedNumber';
            SocketService.get(url).then(function (resp) {
                $scope.callreceivednumber_list = resp.data.MarketingCallReceivedNumber_list;
            });
            var url = 'api/Marketing/GetMarketingCallType';
            SocketService.get(url).then(function (resp) {
                $scope.calltype_list = resp.data.MarketingCallType_list;
            });
            var url = 'api/Marketing/GetMarketingTelecallingFunction';
            SocketService.get(url).then(function (resp) {
                $scope.telecallingfunction_list = resp.data.MarketingTelecallingFunction_list;
            });
            var url = 'api/Marketing/GetLeadRequestType';
            SocketService.get(url).then(function (resp) {
                $scope.leadrequesttype_list = resp.data.leadrequest_list;
            });
        }

        $scope.changefunctionstatus = function (marketingtelecallingfunction_name) {
            if ($('#function :selected').text() == 'Others') {
                $scope.function_show = true;
            }
            else {
                $scope.function_show = false;
            }
        }

        $scope.back = function(){
            $location.url('app/MstMarketingSummary');
        }

        $scope.auditname_change = function (cboassignemployee) {
            var params = {
                employee_gid: $scope.cboassignemployee
            }
            var url = 'api/Marketing/GetBaselocation';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.employee_gid = resp.data.employee_gid;
                $scope.txtbase_location = resp.data.baselocation_name;
               
            });
           
        }


        $scope.save = function(){
            var lsentity_name = '';
            var lsentity_gid = '';
            var lssourceofcontact_name = '';
            var lssourceofcontact_gid = '';
            var lscallreceivednumber_name = '';
            var lscallreceivednumber_gid = '';
            var lsinternalreference_name = '';
            var lsinternalreference_gid = '';
            var lscalltype_name = '';
            var lscalltype_gid = '';
            var lsfunction_name = '';
            var lsfunction_gid = '';
            var lsassignemployee_name = '';
            var lsassignemployee_gid = '';
            var lstagemployee_name = '';
            var lstagemployee_gid = '';
            if ($scope.cboentity != undefined || $scope.cboentity != null) {
                lsentity_name = $('#entity :selected').text();
                lsentity_gid = $scope.cboentity;
           }
           if ($scope.cbosourceofcontact != undefined || $scope.cbosourceofcontact != null) {
                lssourceofcontact_name = $('#sourceofcontact :selected').text();
                lssourceofcontact_gid = $scope.cbosourceofcontact;
           }
           if ($scope.cbocallreceivednumber != undefined || $scope.cbocallreceivednumber != null) {
                lscallreceivednumber_name = $('#callreceivednumber :selected').text();
                lscallreceivednumber_gid = $scope.cbocallreceivednumber;
           }
           if ($scope.cboleadrequesttype != undefined || $scope.cboleadrequesttype != null) {
               lsleadrequesttype_name = $('#leadrequesttype :selected').text();
               lsleadrequesttype_gid = $scope.cboleadrequesttype;
           }
           if ($scope.cbointernalreference != undefined || $scope.cbointernalreference != null) {
                lsinternalreference_name = $('#internalreference :selected').text();
                lsinternalreference_gid = $scope.cbointernalreference;
           }
           if ($scope.cbocalltype != undefined || $scope.cbocalltype != null) {
                lscalltype_name = $('#call_type :selected').text();
                lscalltype_gid = $scope.cbocalltype;
           }
           if ($scope.cbofunction != undefined || $scope.cbofunction != null) {
                lsfunction_name = $('#function :selected').text();
                lsfunction_gid = $scope.cbofunction;
           }
           if ($scope.cboassignemployee != undefined || $scope.cboassignemployee != null) {
                lsassignemployee_name = $('#assignemployee :selected').text();
                lsassignemployee_gid = $scope.cboassignemployee;
           }
     
            var params={    
                entity_name: $scope.lsentity_name,
                entity_gid:lsentity_gid,
                marketingsourceofcontact_name:lssourceofcontact_name,
                marketingsourceofcontact_gid: lssourceofcontact_gid,
                leadrequesttype_name: lsleadrequesttype_name,
                leadrequesttype_gid: lsleadrequesttype_gid,
                marketingcallreceivednumber_name: lscallreceivednumber_name,
                marketingcallreceivednumber_gid: lscallreceivednumber_gid,
                customer_type:$scope.cbocustomertype,
                callreceived_date: $scope.txtcallreceived_date,
                callreceived_time: $scope.txtcallreceived_time,
                caller_name: $scope.txtcaller_name,
                internalreference_name: lsinternalreference_name,
                internalreference_gid: lsinternalreference_gid,
                callerassociate_company: $scope.txtcallerassociate_company,
                office_landlineno : $scope.txtoffice_landlineno,
                marketingcalltype_name: lscalltype_name,
                marketingcalltype_gid: lscalltype_gid,
                marketingfunction_name: lsfunction_name,
                marketingfunction_gid: lsfunction_gid,
                function_remarks: $scope.txtfunction_remarks,
                requirement: $scope.txtrequirement,
                enquiry_description:$scope.txtenquiry_description,
                callclosure_status:$scope.cbocallclosure_status,
                assignemployee_name:lsassignemployee_name,
                assignemployee_gid: lsassignemployee_gid,
                baselocation_name: $scope.txtbase_location,
                tat_hours: $scope.txttat_hours,
                tagemployee_list: $scope.cbotagemployee,
                assignclosure_remarks: $scope.txtassignclosure_remarks
            }
            var url = 'api/Marketing/MarketingCallSave';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $location.url("app/MstMarketingSummary");
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
             
        $scope.submit = function(){
            var lsentity_name = '';
            var lsentity_gid = '';
            var lssourceofcontact_name = '';
            var lssourceofcontact_gid = '';
            var lsleadrequesttype_name = '';
            var lsleadrequesttype_gid = '';
            var lscallreceivednumber_name = '';
            var lscallreceivednumber_gid = '';
            var lsinternalreference_name = '';
            var lsinternalreference_gid = '';
            var lscalltype_name = '';
            var lscalltype_gid = '';
            var lsfunction_name = '';
            var lsfunction_gid = '';
            var lsassignemployee_name = '';
            var lsassignemployee_gid = '';
            var lstagemployee_name = '';
            var lstagemployee_gid = '';
            if ($scope.cboentity != undefined || $scope.cboentity != null) {
                lsentity_name = $('#entity :selected').text();
                lsentity_gid = $scope.cboentity;
            }
            if ($scope.cbosourceofcontact != undefined || $scope.cbosourceofcontact != null) {
                lssourceofcontact_name = $('#sourceofcontact :selected').text();
                lssourceofcontact_gid = $scope.cbosourceofcontact;
            }
            if ($scope.cbocallreceivednumber != undefined || $scope.cbocallreceivednumber != null) {
                lscallreceivednumber_name = $('#callreceivednumber :selected').text();
                lscallreceivednumber_gid = $scope.cbocallreceivednumber;
            }
            if ($scope.cboleadrequesttype != undefined || $scope.cboleadrequesttype != null) {
                lsleadrequesttype_name = $('#leadrequesttype :selected').text();
                lsleadrequesttype_gid = $scope.cboleadrequesttype;
            }
            if ($scope.cbointernalreference != undefined || $scope.cbointernalreference != null) {
                lsinternalreference_name = $('#internalreference :selected').text();
                lsinternalreference_gid = $scope.cbointernalreference;
            }
            if ($scope.cbointernalreference == undefined || $scope.cbointernalreference == null) {
                lsinternalreference_name = 'NA';
                lsinternalreference_gid = 'gid';
            }
            if ($scope.cbocalltype != undefined || $scope.cbocalltype != null) {
                lscalltype_name = $('#call_type :selected').text();
                lscalltype_gid = $scope.cbocalltype;
            }
            if ($scope.cbofunction != undefined || $scope.cbofunction != null) {
                lsfunction_name = $('#function :selected').text();
                lsfunction_gid = $scope.cbofunction;
            }
            if ($scope.cboassignemployee != undefined || $scope.cboassignemployee != null) {
                lsassignemployee_name = $('#assignemployee :selected').text();
                lsassignemployee_gid = $scope.cboassignemployee;
            }

            if (($scope.cbocallclosure_status == 'Assign') && ($scope.cboassignemployee == null || $scope.cboassignemployee == '' )) {
                Notify.alert('Kindly Select Assign Employee Name', 'warning')
            }
            //else if (($scope.cbocallclosure_status == 'Assign') && ($scope.txttat_hours == null || $scope.txttat_hours == '')) {
            //    Notify.alert('Kindly Select Assign TAT Hours', 'warning')
            //}
            else if ((($scope.txtclosure_remarks == '') || ($scope.txtclosure_remarks == null ) )&& (($scope.txtassignclosure_remarks == '')||($scope.txtassignclosure_remarks == null))) {
                Notify.alert('Kindly Enter Remark', 'warning')
            }
           
            else {

                var params = {
                    entity_name: lsentity_name,
                    entity_gid: lsentity_gid,
                    marketingsourceofcontact_name: lssourceofcontact_name,
                    marketingsourceofcontact_gid: lssourceofcontact_gid,
                    marketingcallreceivednumber_name: lscallreceivednumber_name,
                    marketingcallreceivednumber_gid: lscallreceivednumber_gid,
                    leadrequesttype_name: lsleadrequesttype_name,
                    leadrequesttype_gid: lsleadrequesttype_gid,
                    customer_type: $scope.cbocustomertype,
                    callreceived_date: $scope.txtcallreceived_date,
                    callreceived_time: $scope.txtcallreceived_time,
                    caller_name: $scope.txtcaller_name,
                    internalreference_name: lsinternalreference_name,
                    internalreference_gid: lsinternalreference_gid,
                    callerassociate_company: $scope.txtcallerassociate_company,
                    office_landlineno: $scope.txtoffice_landlineno,
                    marketingcalltype_name: lscalltype_name,
                    marketingcalltype_gid: lscalltype_gid,
                    marketingfunction_name: lsfunction_name,
                    marketingfunction_gid: lsfunction_gid,
                    function_remarks: $scope.txtfunction_remarks,
                    requirement: $scope.txtrequirement,
                    enquiry_description: $scope.txtenquiry_description,
                    callclosure_status: $scope.cbocallclosure_status,
                    assignemployee_name: lsassignemployee_name,
                    assignemployee_gid: lsassignemployee_gid,
                    baselocation_name: $scope.txtbase_location,
                    tat_hours: $scope.txttat_hours,
                    tagemployee_list: $scope.cbotagemployee,
                    assignclosure_remarks: $scope.txtassignclosure_remarks,
                    closed_remarks: $scope.txtclosure_remarks
                }
                var url = 'api/Marketing/MarketingCallSubmit';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $location.url("app/MstMarketingSummary");
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

        //Address Multiple Add

        $scope.address_add = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addaddress.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.geocodingFailed = false;

                var url = 'api/AddressType/GetAddressTypeASC';
                SocketService.get(url).then(function (resp) {
                    $scope.addresstype_list = resp.data.addresstype_list;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.onchangepostal_code = function () {
                    var params = {
                        postal_code: $scope.txtpostal_code
                    }
                    var url = 'api/Mstbuyer/GetPostalCodeDetails';

                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.txtcity = resp.data.city;
                        $scope.txttaluka = resp.data.taluka;
                        $scope.txtdistrict = resp.data.district;
                        $scope.txtstate = resp.data.state_name;
                    });
                    
                   
                }

                $scope.getGeoCoding = function () {
                    if($scope.txtpostal_code == undefined || $scope.txtpostal_code.length < 6){

                        $scope.txtlatitude = '';

                        $scope.txtlongitude = '';

                    }

                    else {
                        if ($scope.txtaddressline2 == undefined) {
                            var addressString = ''.concat($scope.txtaddressline1.toString(), ",", $scope.txtpostal_code.toString());
                        } else {
                            var addressString = ''.concat($scope.txtaddressline1.toString(), ",", $scope.txtaddressline2.toString(), ",", $scope.txtpostal_code.toString());
                        }
                        var params = {
                            address: addressString
                        }
                        var url = 'api/GoogleMapsAPI/GetGeoCoding';
                        SocketService.getparams(url, params).then(function (resp) {
                            if (resp.data.status == "OK") {
                                $scope.txtlatitude = resp.data.results[0].geometry.location.lat.toString();
                                $scope.txtlongitude = resp.data.results[0].geometry.location.lng.toString();
                                $scope.geocodingFailed = false;
                            }
                            else if (resp.data.status == "ZERO_RESULTS") {
                                $scope.geocodingFailed = true;
                            }
                        });
                    }
                }

                $scope.txtcountry = "India";
                $scope.addressSubmit = function () {

                    var params = {
                        addresstype_gid: $scope.cboaddresstype.address_gid,
                        addresstype_name: $scope.cboaddresstype.address_type,
                        primary_status: $scope.rdbprimaryaddress,
                        addressline1: $scope.txtaddressline1,
                        addressline2: $scope.txtaddressline2,
                        postal_code: $scope.txtpostal_code,
                        landmark: $scope.txtLand_Mark,
                        taluka: $scope.txttaluka,
                        city: $scope.txtcity,
                        state: $scope.txtstate,
                        district: $scope.txtdistrict,
                        country: $scope.txtcountry,
                        latitude: $scope.txtlatitude,
                        longitude: $scope.txtlongitude
                    }
                    var url = 'api/Marketing/PostMarketingCallAddress';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            address_list();
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
        $scope.address_delete = function (Marketingcall2address_gid) {
            var params =
                {
                    Marketingcall2address_gid: Marketingcall2address_gid
                }
            var url = 'api/Marketing/MarketingCallAddressDelete';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    address_list();
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
        function address_list() {
            var url = 'api/Marketing/GetMarketingCallAddressList';
            SocketService.get(url).then(function (resp) {
                $scope.ibcalladdress_list = resp.data.MarketingCalladdress_list;
            });
        }
        $scope.StaticMapAndPhotos_View = function (latitude, longitude, addressline1, addressline2, postal_code) {
            var modalInstance = $modal.open({
                templateUrl: '/StaticMapAndPhotosView.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    latitude: latitude,
                    longitude: longitude
                }
                var url = 'api/GoogleMapsAPI/GetStaticMapUrl';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.staticmapImgUrl = resp.data;
                });
                if (addressline2 == '') {
                    var addressString = ''.concat(addressline1.toString(), ",", postal_code.toString());
                } else {
                    var addressString = ''.concat(addressline1.toString(), ",", addressline2.toString(), ",", postal_code.toString());
                }
                var params = {
                    address: addressString
                }
                var url = 'api/GoogleMapsAPI/GetPlaceImage';
                SocketService.getparams(url, params).then(function (resp) {
                    var photoUrlArray = [];
                    for (var i = 0; i < resp.data.length; i++) {
                        if (resp.data[i] != null) {
                            photoUrlArray[i] = resp.data[i];
                        }
                    }
                    if (photoUrlArray.length == 0) {
                        $scope.photoNotFound = true;
                    } else {
                        $scope.photoUrlList = photoUrlArray;
                        $scope.photoFound = true;
                    }
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        //Mobile Number Multiple Add
        $scope.add_mobileno = function () {
            //if (($scope.txtmobile_no == undefined) || ($scope.txtmobile_no == '') || ($scope.rdbprimary_status == undefined) || ($scope.rdbwhatsapp_status == undefined) || ($scope.rdbsms_to == undefined)) {
            //    Notify.alert('Enter Mobile Number/Select Status', 'warning');
            //}
            if (($scope.txtmobile_no == undefined) || ($scope.txtmobile_no == '') || ($scope.rdbprimary_status == undefined) || ($scope.rdbwhatsapp_status == undefined) || ($scope.rdbsms_to == undefined) || ($scope.rdbprimary_status == '') || ($scope.rdbwhatsapp_status == '') || ($scope.rdbsms_to == '')) {
                Notify.alert('Enter Mobile Number / Select Primary Status', 'warning');
            }
            else if ($scope.txtmobile_no.length < 10) {
                Notify.alert('Enter 10 Digit Mobile Number', 'warning');
            }
            else {
                var params = {
                    mobile_no: $scope.txtmobile_no,
                    primary_status: $scope.rdbprimary_status,
                    whatsapp_status: $scope.rdbwhatsapp_status,
                    sms_to: $scope.rdbsms_to
                }
                var url = 'api/Marketing/PostMarketingCallMobileNo';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        
                        $scope.txtmobile_no = '';
                        $scope.rdbprimary_status = '';
                        $scope.rdbprimary_status == false;
                        $scope.rdbsms_to = '';
                        $scope.rdbsms_to == false;
                        $scope.rdbwhatsapp_status = '';
                        $scope.rdbwhatsapp_status == false;
                        mobileno_list();
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
        $scope.delete_mobileno = function (marketingcall2mobileno_gid) {
            var params = {
                marketingcall2mobileno_gid: marketingcall2mobileno_gid
            }
            var url = 'api/Marketing/MarketingCallMobileNoDelete';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                mobileno_list();
            });
        }
        function mobileno_list() {
            var url = 'api/Marketing/GetMarketingCallMobileNoList';
            SocketService.get(url).then(function (resp) {
                $scope.ibcallmobileno_list = resp.data.MarketingCallmobileno_list;
            });
        }
        //Email Address Multiple Add
        $scope.add_emailaddress = function () {
            if (($scope.txtemail_address == undefined) || ($scope.txtemail_address == '') || ($scope.rdbprimary_email == undefined) || ($scope.rdbprimary_email == '')) {
                Notify.alert('Enter Email Address/Select Status', 'warning');
            }
            else {
                var params = {
                    email_address: $scope.txtemail_address,
                    primary_status: $scope.rdbprimary_email,
                }
                var url = 'api/Marketing/PostMarketingCallEmail';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        emailaddress_list();
                        $scope.txtemail_address = '';
                        $scope.rdbprimary_email = '';
                        $scope.rdbprimary_email == false;
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
        $scope.delete_emailaddress = function (marketingcall2email_gid) {
            var params = {
                marketingcall2email_gid: marketingcall2email_gid
            }
            var url = 'api/Marketing/MarketingCallEmailDelete';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                emailaddress_list();
            });
        }
        function emailaddress_list() {
            var url = 'api/Marketing/GetMarketingCallEmailList';
            SocketService.get(url).then(function (resp) {
                $scope.ibcallemail_list = resp.data.MarketingCallemail_list;
            });
        }
//Follow Up Multiple Add
        $scope.add_followup = function () {
            if (($scope.txtfollowup_date == undefined) || ($scope.txtfollowup_date == '') || ($scope.txtfollowup_time == undefined) || ($scope.txtfollowup_time == '')) {
                Notify.alert('Enter Follow Up Date/Follow Up Time', 'warning');
            }
            else {
                var params = {
                    followup_date: $scope.txtfollowup_date,
                    followup_time: $scope.txtfollowup_time,
                }
                var url = 'api/Marketing/PostMarketingCallFollowUp';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    $scope.txtfollowup_date = '';
                    followup_list();

                    $scope.txtfollowup_time = '';
                });
            }
        }
        $scope.delete_followup = function (marketingcall2followup_gid) {
            var params ={
                marketingcall2followup_gid: marketingcall2followup_gid
                }
            var url = 'api/Marketing/MarketingCallFollowUpDelete';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning', 
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                followup_list();
            });
        }
        function followup_list() {
            var url = 'api/Marketing/GetMarketingCallFollowUpList';
            SocketService.get(url).then(function (resp) {
                $scope.ibcallfollowup_list = resp.data.MarketingCallfollowup_list;
            }); 
        }
    }
})();
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstMarketingAssignedCallsController', MstMarketingAssignedCallsController);

    MstMarketingAssignedCallsController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'DownloaddocumentService','cmnfunctionService'];

    function MstMarketingAssignedCallsController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstMarketingAssignedCallsController';
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        var marketingcall_gid = searchObject.marketingcall_gid;
        activate();
        lockUI();
        function activate() {
            vm.calender1 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open1 = true;
            };
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };
            var params = {
                marketingcall_gid: marketingcall_gid
            }
            var url = 'api/Marketing/GetMarketingCallAssignedView';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtticket_refid = resp.data.ticket_refid,
                $scope.txtentity_name = resp.data.entity_name,
                $scope.txtsourceofcontact_name = resp.data.sourceofcontact_name,
                $scope.txtcallreceivednumber_name = resp.data.callreceivednumber_name,
                $scope.txtcustomer_type = resp.data.leadrequesttype_name,
                $scope.txtcallreceived_date = resp.data.callreceived_date,
                $scope.txtcaller_name = resp.data.caller_name,
                $scope.txtinternalreference_name = resp.data.internalreference_name,
                $scope.txtcallerassociate_company = resp.data.callerassociate_company,
                $scope.txtoffice_landlineno = resp.data.office_landlineno,
                $scope.txtcalltype_name = resp.data.calltype_name,
                $scope.txtfunction_name = resp.data.function_name,
                  $scope.txtfunction_remarks = resp.data.function_remarks,
                  $scope.txttat_hours = resp.data.tat_hours,
                $scope.txtrequirement = resp.data.requirement,
                $scope.txtenquiry_description = resp.data.enquiry_description,
                $scope.txtcallclosure_status = resp.data.callclosure_status,
                $scope.txtassignemployee_name = resp.data.assignemployee_name,
                $scope.txtassign_date = resp.data.assign_date,
                $scope.txttagemployee_name = resp.data.tagemployee_name,
                $scope.txtbase_location = resp.data.baselocation_name,
                $scope.origination = resp.data.origination,
                $scope.txtassignclosure_remarks = resp.data.assignclosure_remarks,
                $scope.ibcalladdress_list = resp.data.MarketingCalladdress_list,
                $scope.ibcallfollowup_list = resp.data.MarketingCallfollowup_list,
                $scope.ibcalltaggedmember_list = resp.data.MarketingCalltaggedmember_list,
                $scope.ibcallmobileno_list = resp.data.MarketingCallmobileno_list,
                $scope.txtprimary_email = resp.data.primary_email,
                $scope.ibcallemail_list = resp.data.MarketingCallemail_list,
                    $scope.txtacknowledge_date = resp.data.acknowledge_date,
                    $scope.txtleadrequire_name = resp.data.leadrequire_name,
                    $scope.txtmilletrequire_name = resp.data.milletrequire_name,
                    $scope.txtenquiryrequire_name = resp.data.enquiryrequire_name,
                    $scope.txtstartuprequire_name = resp.data.startuprequire_name,
                    $scope.txtbusiness_name = resp.data.business_name,
                    $scope.txtindustry_name = resp.data.industry_name,

                unlockUI();
            });
            followup_list();
            $scope.minDate = new Date();
            var url = 'api/Marketing/MarketingCallRecordingDocumentList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.Uploadcallrecordingdocument_list = resp.data.callproofupload_list;
            });
            var url = 'api/Marketing/MarketingCallProofDocumentList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.Uploadcallproofdocument_list = resp.data.callproofupload_list;
            });
            var url = 'api/Marketing/GetDocumentList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.lefilename = resp.data.filename;
                $scope.lefilepath = resp.data.filepath;
                $scope.document_list = resp.data.document_list;
            });
           
            var url = 'api/Marketing/MarketingCallDocTempClear';
            SocketService.get(url).then(function () {
            });
            
            var url = 'api/Marketing/GetEntity';
            SocketService.get(url).then(function (resp) {
                $scope.entity_list = resp.data.inboundentity_list;
            });
            var url = 'api/Marketing/GetLoanProduct';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.samfinloanproduct_data = resp.data.samapplication_list;
                unlockUI();
            });
            var url = 'api/Marketing/GetLoanSubProduct';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.samfinloansubproduct_list = resp.data.samapplication_list;
                unlockUI();
            });
            var url = 'api/Marketing/GetAgrLoanProduct';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.loanproduct_data = resp.data.samapplication_list;
                unlockUI();
            });
            var url = 'api/Marketing/GetAgrLoanSubProduct';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.loansubproduct_list = resp.data.samapplication_list;
                unlockUI();
            });
            var url = 'api/Marketing/GetMilletDocumentList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.lmfilename = resp.data.filename;
                $scope.lmfilepath = resp.data.filepath;
                $scope.milletdocument_list = resp.data.milletdocument_list;
            });
            var url = 'api/Marketing/GetEnquiryDocumentList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.lmfilename = resp.data.filename;
                $scope.lmfilepath = resp.data.filepath;
                $scope.enquirydocument_list = resp.data.enquirydocument_list;
            });
           
            leadstatus_list();
            followup_list();
           
        }
        $scope.download_all = function (val1,val2) {

            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }

        }
        $scope.download_allenquiry = function (val1, val2) {
            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }
        }
        $scope.download_allmillet = function (val1,val2) {
            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            } }        
        $scope.milletdocument_downloads = function (val1,val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.enquirydocument_downloads = function (val1,val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.documentviewer = function (val1, val2) {
            lockUI();
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
            if (IsValidExtension == false) {
            Notify.alert("View is not supported for this format..!", {
             status: 'danger',
             pos: 'top-center',
             timeout: 3000
             });
             unlockUI();
           return false;
             }
             DownloaddocumentService.DocumentViewer(val1, val2);
             }

             $scope.documentviewerupload = function (val1, val2) {
                lockUI();
                var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
                if (IsValidExtension == false) {
                Notify.alert("View is not supported for this format..!", {
                 status: 'danger',
                 pos: 'top-center',
                 timeout: 3000
                 });
                 unlockUI();
               return false;
                 }
                 DownloaddocumentService.DocumentViewer(val1, val2);
                 }

        $scope.documentviewermillet = function (val1, val2) {
                    lockUI();
                    var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
                    if (IsValidExtension == false) {
                    Notify.alert("View is not supported for this format..!", {
                     status: 'danger',
                     pos: 'top-center',
                     timeout: 3000
                     });
                     unlockUI();
                   return false;
                     }
                     DownloaddocumentService.DocumentViewer(val1, val2);
        }
        $scope.documentviewerenquiry = function (val1, val2) {
            lockUI();
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
            if (IsValidExtension == false) {
                Notify.alert("View is not supported for this format..!", {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                unlockUI();
                return false;
            }
            DownloaddocumentService.DocumentViewer(val1, val2);
        }
        $scope.changeentitiy = function (cboentity) {
            for (var i = 0; i < $scope.entity_list.length; i++)
            {
                if (cboentity == $scope.entity_list[i].entity_gid)
                $scope.entityselect = $scope.entity_list[i].entity_name
            }
        }
        $scope.Back = function () {
            $location.url("app/MstMarketingWorkInprogressCallSummary");
        }
        $scope.document_downloads = function (val1,val2) {
            DownloaddocumentService.Downloaddocument(val1,val2);
        }
        $scope.downloads_all = function (val1, val2) {

            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }

        }
        //
        //lead status Multiple Add
        $scope.add_leadstatus = function ()
        {
            var lsentity_name = '';
            var lsentity_gid = '';
            lsentity_name = $('#entity :selected').text();
            lsentity_gid = $scope.cboentity;
            var status = lsentity_name;

            if (status == 'SAMFIN') {
                var lsloanproduct_name = '';
                var lsloanproduct_gid = '';
                var lsloansubproduct_name = '';
                var lsloansubproduct_gid = '';

                lsloanproduct_name = $('#saloanproductname :selected').text();
                lsloanproduct_gid = $scope.cboloanproduct;
                lsloansubproduct_name = $('#saloansubproductname :selected').text();
                lsloansubproduct_gid = $scope.cboloansubproduct;

               
              if ((status == 'SAMFIN' || status == 'SAMAGRO') && (($scope.cboloansubproduct == '' || $scope.cboloansubproduct == null) || ($scope.cboloanproduct == '' || $scope.cboloanproduct == null) || ($scope.txt_amount == '' || $scope.txt_amount == null))) {
                    Notify.alert('Kindly Fill Converted Details', 'warning')
                }
                else {
                    var params = {
                        marketingcall_gid: marketingcall_gid,
                        lead_type: lsentity_name,
                        closure_status: $scope.cboclosurestatus,
                        ticket_refid: $scope.txtticket_refid,
                        loanproduct_name: lsloanproduct_name,
                        loanproduct_gid: lsloanproduct_gid,
                        loansubproduct_name: lsloansubproduct_name,
                        loansubproduct_gid: lsloansubproduct_gid,
                        loan_amount: $scope.txt_amount,
                    }
                    var url = 'api/Marketing/PostMarketingCallLeadstatus';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            leadstatus_list();

                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        $scope.txt_amount = '';
                        $scope.cboloansubproduct = '';
                        $scope.cboloanproduct = '';
                    });
                }
            }
            else {
                             
                    var lsloanproduct_name = '';
                    var lsloanproduct_gid = '';
                    var lsloansubproduct_name = '';
                    var lsloansubproduct_gid = '';

                   
                    lsloanproduct_name = $('#loanproductname :selected').text();
                    lsloanproduct_gid = $scope.cboloanproduct;
                    lsloansubproduct_name = $('#loansubproductname :selected').text();
                    lsloansubproduct_gid = $scope.cboloansubproduct;

                    if ((status == 'SAMFIN' || status == 'SAMAGRO') && (($scope.cboloansubproduct == '' || $scope.cboloansubproduct == null) || ($scope.cboloanproduct == '' || $scope.cboloanproduct == null) || ($scope.txt_amount == '' || $scope.txt_amount == null))) {
                        Notify.alert('Kindly Fill Converted Details', 'warning')
                    }
                    else {
                        var params = {
                            marketingcall_gid: marketingcall_gid,
                            lead_type: lsentity_name,
                            closure_status: $scope.cboclosurestatus,
                            ticket_refid: $scope.txtticket_refid,
                            loanproduct_name: lsloanproduct_name,
                            loanproduct_gid: lsloanproduct_gid,
                            loansubproduct_name: lsloansubproduct_name,
                            loansubproduct_gid: lsloansubproduct_gid,
                            loan_amount: $scope.txt_amount,
                        }
                        var url = 'api/Marketing/PostMarketingCallLeadstatus';
                        lockUI();
                        SocketService.post(url, params).then(function (resp) {
                            unlockUI();
                            if (resp.data.status == true) {

                                Notify.alert(resp.data.message, {
                                    status: 'success',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                leadstatus_list();

                            }
                            else {
                                Notify.alert(resp.data.message, {
                                    status: 'warning',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                            }
                            $scope.txt_amount = '';
                            $scope.cboloansubproduct = '';
                            $scope.cboloanproduct = '';
                        });
                    }
                
            }
        }
        $scope.delete_leadstatus = function (marketingcall2leadstatus_gid) {
            var params = {
                marketingcall2leadstatus_gid: marketingcall2leadstatus_gid
            }
            var url = 'api/Marketing/MarketingCallLeadstatusDelete';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                leadstatus_list();
            });
        }


        function leadstatus_list() //GetMarketingCallAssignedView
        {
            var params = {
                marketingcall_gid: marketingcall_gid
            }
            var url = 'api/Marketing/GetMarketingCallLeadstatusList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.ibcallleadstatus_list = resp.data.MarketingCallLeadstatus_list;
              //var  temp = $scope.ibcallleadstatus_list ;
              //  ibcallleadstatus_list =  $scope.ibcallleadstatus_list;

            });
        }

        //

        $scope.changeclosurestatus = function (cboclosurestatus) {
            if (cboclosurestatus == 'Closed') {
                $scope.followup_show = true;
            }
            else {
                $scope.followup_show = false;
            }
            if (cboclosurestatus == 'Converted') {
                $scope.completed_show = true;
            }
            else {
                $scope.completed_show = false;
            }
        }

        $scope.Submit = function () {   
          
             if (($scope.cboclosurestatus == 'Rejected') && ($scope.cboclosed == '' || $scope.cboclosed == null)) {
                Notify.alert('Kindly Fill Closed', 'warning')
             }
            
             
             else//if(($scope.cboclosurestatus == 'Converted')||($scope.cboclosurestatus == 'Closed'))
             {
                var params = {
                    marketingcall_gid: marketingcall_gid,
                    followup_date: $scope.txtfollowup_date,
                    followup_time: $scope.txtfollowup_time,
                    closed: $scope.cboclosed,
                    closure_status: $scope.cboclosurestatus,
                  
                    followup_remarks: $scope.txtfollowup_remarks
                }
                var url = 'api/Marketing/PostCompletedCall';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $location.url("app/MstMarketingWorkInprogressCallSummary");
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
        
        $scope.uploadcall_recording = function (val, val1, name) {
            var frm = new FormData();

            for (var i = 0; i < val.length; i++) {
                var item = {
                    name: val[i].name,
                    file: val[i]
                };
                frm.append('fileupload', item.file);
                frm.append('file_name', item.name);
            }
            frm.append('document_title', $scope.txtdocument_title);
            frm.append('project_flag', "BD");

            $scope.uploadfrm = frm;
        }

        $scope.uploadcallrecording_doc = function () {

            if ($scope.uploadfrm != undefined) {
                lockUI();
                var url = 'api/Marketing/CallRecordingDocumentUpload';
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                    $("#file").val('');

                    $scope.txtdocument_title = "";
                    $scope.uploadfrm = undefined;
                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    callrecording_list();
                    unlockUI();
                });
            }
            else {
               // alert('Please select a file.')
                Notify.alert('Please select a file.', 'warning')
            }
            $scope.txtdocument_title = '';
        }

        function callrecording_list() {
            var params = {
                marketingcall_gid: marketingcall_gid
            }
            var url = 'api/Marketing/MarketingCallRecordingDocumentTmpList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.Uploadcallrecordingdocument_list = resp.data.callproofupload_list;
            });
        }
        $scope.rec_cancel = function (MarketingCallrecordingocupload_gid) {

            var params = {
                MarketingCallrecordingocupload_gid: MarketingCallrecordingocupload_gid
            }
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do You Want To Delete the Photo ?',
                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Yes, delete it!',
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    lockUI();
                    var url = 'api/Marketing/MarketingCallRecordingDocumentDelete';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }
                        callrecording_list();
                        unlockUI();
                    });
                    SweetAlert.swal('Deleted Successfully!');
                }
            });
        }

        //$scope.rec_downloads = function (val1, val2) {
        //    var phyPath = val1;
        //    var relPath = phyPath.split("StoryboardAPI");
        //    var relpath1 = relPath[1].replace("\\", "/");
        //    var hosts = window.location.host;
        //    var prefix = location.protocol + "//";
        //    var str = prefix.concat(hosts, relpath1);
        //    var link = document.createElement("a");
        //    link.download = val2;
        //    var uri = str;
        //    link.href = uri;
        //    link.click();
        //}
        
        $scope.rec_downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }


        $scope.proofdocument_upload = function (val, val1, name) {
            var IsValidExtension =cmnfunctionService.fnCheckValidDocType(val[0].name, "BD")
            
            
            if (IsValidExtension == false) {
                $("#fileupload").val('');
                Notify.alert("File format is not supported..!", {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                return false;
            }
            else
            {
            var frm = new FormData();

            for (var i = 0; i < val.length; i++) {
                var item = {
                    name: val[i].name,
                    file: val[i]
                };
                frm.append('fileupload', item.file);
                frm.append('file_name', item.name);
            }
            frm.append('document_title', $scope.txtdocumentcallproof_title);
                frm.append('marketingcall_gid', marketingcall_gid);
                frm.append('project_flag', "BD");

            $scope.uploadfrm = frm;
            }
        }
        $scope.uploadcallproof_doc = function () {
           
            if ($scope.uploadfrm != undefined) {
                lockUI();
                
                var url = 'api/Marketing/CallProofDocumentUpload';
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                    $("#fileupload").val('');
                  
                    $scope.txtdocumentcallproof_title = "";
                    $scope.uploadfrm = undefined;
                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    proofdocument_list();
                    unlockUI();
                });
            }
            else {
                Notify.alert('Please select a file.', 'warning')
            }
            $scope.txtdocumentcallproof_title = '';
        }

        function proofdocument_list() {
            var params = {
                marketingcall_gid: marketingcall_gid
            }
            var url = 'api/Marketing/MarketingCallProofDocumentTmpList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.lsfilename = resp.data.filename;
                $scope.lsfilepath = resp.data.filepath;
                $scope.Uploadcallproofdocument_list = resp.data.callproofupload_list;
            });
        }
        $scope.recproof_cancel = function (MarketingCallproofdocupload_gid) {

            var params = {
                MarketingCallproofdocupload_gid: MarketingCallproofdocupload_gid
            }
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do You Want To Delete the Photo ?',
                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Yes, delete it!',
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    lockUI();
                    var url = 'api/Marketing/MarketingCallProofDocumentDelete';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }
                        proofdocument_list();
                        unlockUI();
                    });
                    SweetAlert.swal('Deleted Successfully!');
                }
            });
        }
        
        $scope.credit_amountChange = function () {
            var input = document.getElementById('credit_amount').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lswords_creditamount =  cmnfunctionService.fnConvertNumbertoWord(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txt_amount = "";
            }
            else {
                $scope.txt_amount = output;
                document.getElementById('words_creditamt').innerHTML = lswords_creditamount;
            }
        } 
        $scope.limit_amountChange = function () {
            var input = document.getElementById('limit_amount').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lswords_creditamount =  cmnfunctionService.fnConvertNumbertoWord(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txt_amount = "";
            }
            else {
                $scope.txt_amount = output;
                document.getElementById('words_limitamt').innerHTML = lswords_creditamount;
            }
        } 
       
       


        $scope.recproof_downloads = function (val1, val2) {

            //var phyPath = val1;
            //var relPath = phyPath.split("StoryboardAPI");
            //var relpath1 = relPath[1].replace("\\", "/");
            //var hosts = window.location.host;
            //var prefix = location.protocol + "//";
            //var str = prefix.concat(hosts, relpath1);
            //var link = document.createElement("a");
            //link.download = val2;
            //var uri = str;
            //link.href = uri;
            //link.click();
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.add_followup = function () {
            if (($scope.txtfollowup_date == undefined) || ($scope.txtfollowup_date == '') || ($scope.txtfollowup_time == undefined) || ($scope.txtfollowup_time == '')) {
                Notify.alert('Enter Follow Up Date/Follow Up Time', 'warning');
            }
            else {
                var params = {
                    marketingcall_gid:marketingcall_gid,
                    followup_date: $scope.txtfollowup_date,
                    followup_time: $scope.txtfollowup_time,
                    followup_status: $scope.cbofollowup,
                    followup_remarks: $scope.cboremarks,
                }
                var url = 'api/Marketing/PostMarketingCallFollowUpMg';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        followup_list();
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    $scope.txtfollowup_date = '';
                    $scope.cbofollowup = '';
                    $scope.cboremarks = '';
                    followup_list();

                    $scope.txtfollowup_time = '';
                });
            }
        }
        $scope.delete_followup = function (marketingcall2followup_gid) {
            var params = {
                marketingcall2followup_gid: marketingcall2followup_gid
            }
            var url = 'api/Marketing/MarketingCallFollowUpDelete';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    followup_list();
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                followup_list();
            });
        }
        function followup_list() {
            var params = {
                marketingcall_gid: marketingcall_gid
            }
            var url = 'api/Marketing/GetMarketingCallMyFollowUpList';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.ibcallfollowup_list = resp.data.MarketingCallfollowup_list;
            });
        }
        $scope.edit_followup = function (marketingcall2followup_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editinboundcallfollowup.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    marketingcall2followup_gid: marketingcall2followup_gid
                }
                var url = 'api/Marketing/EditMarketingCallFollowUp';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txteditfollowup_date = new Date(resp.data.followup_date);
                    $scope.cboeditfollowup = resp.data.followup_status;
                    $scope.cboremarks = resp.data.followup_remarks;
                    if (resp.data.Tfollowup_time == '0001-01-01T00:00:00') {
                        $scope.txteditfollowup_time = '';
                    }
                    else {
                        $scope.txteditfollowup_time = new Date(resp.data.Tfollowup_time);
                    }
                  
                    followup_list();
                });


                $scope.editfollowup_change = function (cboeditfollowup) {
                    if (cboeditfollowup == 'Hot') {
                        $scope.cboremarks = 'Hot:will be closed in 1 month';
                    }
                    else if (cboeditfollowup == 'Warm') {
                        $scope.cboremarks = 'Warm:will be closed in 3 month';
                    }
                    else if (cboeditfollowup == 'Cold') {
                        $scope.cboremarks = 'Cold: will be closed in 6 month';
                    }
                    else if (cboeditfollowup == 'Others') {
                        $scope.cboremarks = 'Unqualified';
                    }
                    else {
                        $scope.cboremarks = '';
                    }
                }
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update_followup = function () {

                    var params = {
                        followup_date: $scope.txteditfollowup_date,
                        followup_time: $scope.txteditfollowup_time,
                        followup_status: $scope.cboeditfollowup,
                        followup_remarks: $scope.cboremarks,
                        marketingcall2followup_gid: marketingcall2followup_gid,
                        marketingcall_gid: $scope.marketingcall_gid,
                    }
                    var url = 'api/Marketing/UpdateMarketingCallFollowUp';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            followup_list();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                      //  followup_templist();
                        followup_list();

                    });

                    $modalInstance.close('closed');

                }
            }
            followup_list();
        }
    
        $scope.followup_change = function (cbofollowup) {
            if (cbofollowup == 'Hot') {
                $scope.cboremarks = 'Hot:will be closed in 1 month';
            }
            else if (cbofollowup == 'Warm') {
                $scope.cboremarks = 'Warm:will be closed in 3 month';
            }
            else if (cbofollowup == 'Cold') {
                $scope.cboremarks = 'Cold: will be closed in 6 month';
            }
            else if (cbofollowup == 'Others') {
                $scope.cboremarks = 'Unqualified';
            }
            else {
                $scope.cboremarks = '';
            }
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstMarketingAssignedCallViewController', MstMarketingAssignedCallViewController);

    MstMarketingAssignedCallViewController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'DownloaddocumentService', 'cmnfunctionService'];

    function MstMarketingAssignedCallViewController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstMarketingAssignedCallViewController';

        //$scope.marketingcall_gid = $location.search().marketingcall_gid;
        //var marketingcall_gid = $scope.marketingcall_gid;
        //$scope.lspage = $location.search().lspage;
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        var marketingcall_gid = searchObject.marketingcall_gid;
        $scope.lspage = searchObject.lspage;
        var lspage = $scope.lspage;


        $scope.transfershow = false;
        $scope.followupshow = false;
        $scope.completedshow = false;

        lockUI();
        activate();
        function activate() {
            vm.calender1 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open1 = true;
            };
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };

            if (lspage == 'TransferCall') {
                $scope.transfershow = true;
                $scope.followupshow = false;
                $scope.completedshow = false;
            }
            else if (lspage == 'FollowUpCall') {
                $scope.followupshow = true;
                $scope.transfershow = false;
                $scope.completedshow = false;
            }
            else if (lspage == 'CompletedCall') {
                $scope.completedshow = true;
                $scope.transfershow = false;
                $scope.followupshow = false;
            }
            else {

            }

            var params = {
                marketingcall_gid: marketingcall_gid
            }
            var url = 'api/Marketing/GetMarketingCallAssignedView';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtticket_refid = resp.data.ticket_refid,
                $scope.txtentity_name = resp.data.entity_name,
                $scope.txtsourceofcontact_name = resp.data.sourceofcontact_name,
                $scope.txtcallreceivednumber_name = resp.data.callreceivednumber_name,
                $scope.txtcustomer_type = resp.data.leadrequesttype_name,
                $scope.txtcallreceived_date = resp.data.callreceived_date,
                $scope.txtcaller_name = resp.data.caller_name,
                $scope.txtinternalreference_name = resp.data.internalreference_name,
                $scope.txtcallerassociate_company = resp.data.callerassociate_company,
                $scope.txtoffice_landlineno = resp.data.office_landlineno,
                $scope.txtcalltype_name = resp.data.calltype_name,
                $scope.txtfunction_name = resp.data.function_name,
                $scope.txtfunction_remarks = resp.data.function_remarks,
                  $scope.txttat_hours = resp.data.tat_hours,
                $scope.txtrequirement = resp.data.requirement,
                $scope.txtenquiry_description = resp.data.enquiry_description,
                $scope.txtcallclosure_status = resp.data.callclosure_status,
                $scope.txtassignemployee_name = resp.data.assignemployee_name,
                $scope.txtassign_date = resp.data.assign_date,
                $scope.txttagemployee_name = resp.data.tagemployee_name,
                $scope.txtassignclosure_remarks = resp.data.assignclosure_remarks,
                $scope.ibcalladdress_list = resp.data.MarketingCalladdress_list;
                $scope.ibcallfollowup_list = resp.data.MarketingCallfollowup_list;
                $scope.ibcalltransfer_list = resp.data.MarketingCalltransfer_list,
                $scope.ibcalltaggedmember_list = resp.data.MarketingCalltaggedmember_list;
                $scope.txtcompleted_by = resp.data.completed_by,
                 $scope.txtcompleted_date = resp.data.completed_date,
                $scope.txtcompleted_remarks = resp.data.completed_remarks,
               $scope.txtfollowup_date = resp.data.followup_date,
                $scope.txtfollowup_time = resp.data.followup_time,
                $scope.txtfollowup_by = resp.data.followup_by,
                $scope.txtfollowup_remarks = resp.data.followup_remarks,
                 $scope.txtrejected_date = resp.data.rejected_date,
                $scope.txtrejected_by = resp.data.rejected_by,
                $scope.txtrejected_remarks = resp.data.rejected_remarks,
                $scope.txtloanproduct_name = resp.data.loanproduct_name,
                $scope.txtloansubproduct_name = resp.data.loansubproduct_name,
                $scope.txtloan_amount = resp.data.loan_amount,
                $scope.txtbase_location = resp.data.baselocation_name,
                $scope.origination = resp.data.origination,
                  $scope.ibcallmobileno_list = resp.data.MarketingCallmobileno_list;
                $scope.txtprimary_email = resp.data.primary_email,
                    $scope.ibcallemail_list = resp.data.MarketingCallemail_list;
                $scope.txtleadrequire_name = resp.data.leadrequire_name,
                    $scope.txtmilletrequire_name = resp.data.milletrequire_name,
                    $scope.txtenquiryrequire_name = resp.data.enquiryrequire_name,
                    $scope.txtstartuprequire_name = resp.data.startuprequire_name,
                    $scope.txtbusiness_name = resp.data.business_name,
                    $scope.txtindustry_name = resp.data.industry_name,

                unlockUI();
            });


            var url = 'api/Marketing/MarketingCallProofDocumentList';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.lsfilename = resp.data.filename;
                $scope.lsfilepath = resp.data.filepath;

                $scope.Uploadcallproofdocument_list = resp.data.callproofupload_list;
                unlockUI();
            });
            var url = 'api/Marketing/MarketingCallRecordingDocumentList';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.Uploadcallrecordingdocument_list = resp.data.callproofupload_list;
                unlockUI();
            });

            var url = 'api/Marketing/GetMarketingCallLeadstatusList';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.MarketingCallLeadstatus_list = resp.data.MarketingCallLeadstatus_list;
                unlockUI();
            });
            var url = 'api/Marketing/GetDocumentList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.lefilename = resp.data.filename;
                $scope.lefilepath = resp.data.filepath;
                $scope.document_list = resp.data.document_list;
            });
            var url = 'api/Marketing/GetMilletDocumentList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.lmfilename = resp.data.filename;
                $scope.lmfilepath = resp.data.filepath;
                $scope.milletdocument_list = resp.data.milletdocument_list;
            });
            var url = 'api/Marketing/GetEnquiryDocumentList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.lmfilename = resp.data.filename;
                $scope.lmfilepath = resp.data.filepath;
                $scope.enquirydocument_list = resp.data.enquirydocument_list;
            });
        }
        $scope.document_downloads = function (val1,val2) {
            DownloaddocumentService.Downloaddocument(val1,val2);
        }
        $scope.downloads_all = function (val1, val2) {

            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }

        }
        $scope.download_allmillet = function (val1,val2) {
            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }
        }        
        $scope.milletdocument_downloads = function (val1,val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.download_allenquiry = function (val1,val2) {
            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }
        }        
        $scope.enquirydocument_downloads = function (val1,val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.documentviewerenquiry = function (val1, val2) {
            lockUI();
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
            if (IsValidExtension == false) {
                Notify.alert("View is not supported for this format..!", {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                unlockUI();
                return false;
            }
            DownloaddocumentService.DocumentViewer(val1, val2);
        }
        $scope.documentviewer = function (val1, val2) {
            lockUI();
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
            if (IsValidExtension == false) {
            Notify.alert("View is not supported for this format..!", {
             status: 'danger',
             pos: 'top-center',
             timeout: 3000
             });
             unlockUI();
           return false;
             }
             DownloaddocumentService.DocumentViewer(val1, val2);
             }

             $scope.documentviewermillet = function (val1, val2) {
                lockUI();
                var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
                if (IsValidExtension == false) {
                Notify.alert("View is not supported for this format..!", {
                 status: 'danger',
                 pos: 'top-center',
                 timeout: 3000
                 });
                 unlockUI();
               return false;
                 }
                 DownloaddocumentService.DocumentViewer(val1, val2);
                 }

                 $scope.documentviewerupload = function (val1, val2) {
                    lockUI();
                    var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
                    if (IsValidExtension == false) {
                    Notify.alert("View is not supported for this format..!", {
                     status: 'danger',
                     pos: 'top-center',
                     timeout: 3000
                     });
                     unlockUI();
                   return false;
                     }
                     DownloaddocumentService.DocumentViewer(val1, val2);
                     }
        $scope.rec_downloads = function (val1, val2) {
            //var phyPath = val1;
            //var relPath = phyPath.split("StoryboardAPI");
            //var relpath1 = relPath[1].replace("\\", "/");
            //var hosts = window.location.host;
            //var prefix = location.protocol + "//";
            //var str = prefix.concat(hosts, relpath1);
            //var link = document.createElement("a");
            //link.download = val2;
            //var uri = str;
            //link.href = uri;
            //link.click();
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        
        $scope.recproof_downloads = function (val1, val2) {
            //var phyPath = val1;
            //var relPath = phyPath.split("StoryboardAPI");
            //var relpath1 = relPath[1].replace("\\", "/");
            //var hosts = window.location.host;
            //var prefix = location.protocol + "//";
            //var str = prefix.concat(hosts, relpath1);
            //var link = document.createElement("a");
            //link.download = val2;
            //var uri = str;
            //link.href = uri;
            //link.click();
             DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.download_all = function (val1, val2) {

            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }

        }
        $scope.add_leadstatus = function () {
            var lsentity_name = '';
            var lsentity_gid = '';
            lsentity_name = $('#entity :selected').text();
            lsentity_gid = $scope.cboentity;
            var status = lsentity_name;

            if (status == 'SAMFIN') {
                var lsloanproduct_name = '';
                var lsloanproduct_gid = '';
                var lsloansubproduct_name = '';
                var lsloansubproduct_gid = '';

                lsloanproduct_name = $('#saloanproductname :selected').text();
                lsloanproduct_gid = $scope.cboloanproduct;
                lsloansubproduct_name = $('#saloansubproductname :selected').text();
                lsloansubproduct_gid = $scope.cboloansubproduct;


                if ((status == 'SAMFIN' || status == 'SAMAGRO') && (($scope.cboloansubproduct == '' || $scope.cboloansubproduct == null) || ($scope.cboloanproduct == '' || $scope.cboloanproduct == null) || ($scope.txt_amount == '' || $scope.txt_amount == null))) {
                    Notify.alert('Kindly Fill Converted Details', 'warning')
                }
                else {
                    var params = {
                        marketingcall_gid: marketingcall_gid,
                        lead_type: lsentity_name,
                        closure_status: $scope.cboclosurestatus,
                        ticket_refid: $scope.txtticket_refid,
                        loanproduct_name: lsloanproduct_name,
                        loanproduct_gid: lsloanproduct_gid,
                        loansubproduct_name: lsloansubproduct_name,
                        loansubproduct_gid: lsloansubproduct_gid,
                        loan_amount: $scope.txt_amount,
                    }
                    var url = 'api/Marketing/PostMarketingCallLeadstatus';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            leadstatus_list();

                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        $scope.txt_amount = '';
                        $scope.cboloansubproduct = '';
                        $scope.cboloanproduct = '';
                    });
                }
            }
            else {

                var lsloanproduct_name = '';
                var lsloanproduct_gid = '';
                var lsloansubproduct_name = '';
                var lsloansubproduct_gid = '';


                lsloanproduct_name = $('#loanproductname :selected').text();
                lsloanproduct_gid = $scope.cboloanproduct;
                lsloansubproduct_name = $('#loansubproductname :selected').text();
                lsloansubproduct_gid = $scope.cboloansubproduct;

                if ((status == 'SAMFIN' || status == 'SAMAGRO') && (($scope.cboloansubproduct == '' || $scope.cboloansubproduct == null) || ($scope.cboloanproduct == '' || $scope.cboloanproduct == null) || ($scope.txt_amount == '' || $scope.txt_amount == null))) {
                    Notify.alert('Kindly Fill Converted Details', 'warning')
                }
                else {
                    var params = {
                        marketingcall_gid: marketingcall_gid,
                        lead_type: lsentity_name,
                        closure_status: $scope.cboclosurestatus,
                        ticket_refid: $scope.txtticket_refid,
                        loanproduct_name: lsloanproduct_name,
                        loanproduct_gid: lsloanproduct_gid,
                        loansubproduct_name: lsloansubproduct_name,
                        loansubproduct_gid: lsloansubproduct_gid,
                        loan_amount: $scope.txt_amount,
                    }
                    var url = 'api/Marketing/PostMarketingCallLeadstatus';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            leadstatus_list();

                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        $scope.txt_amount = '';
                        $scope.cboloansubproduct = '';
                        $scope.cboloanproduct = '';
                    });
                }

            }
        }
        $scope.delete_leadstatus = function (marketingcall2leadstatus_gid) {
            var params = {
                marketingcall2leadstatus_gid: marketingcall2leadstatus_gid
            }
            var url = 'api/Marketing/MarketingCallLeadstatusDelete';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                leadstatus_list();
            });
        }


        function leadstatus_list() //GetMarketingCallAssignedView
        {
            var params = {
                marketingcall_gid: marketingcall_gid
            }
            var url = 'api/Marketing/GetMarketingCallLeadstatusList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.ibcallleadstatus_list = resp.data.MarketingCallLeadstatus_list;
                //var  temp = $scope.ibcallleadstatus_list ;
                //  ibcallleadstatus_list =  $scope.ibcallleadstatus_list;

            });
        }

        //

        $scope.changeclosurestatus = function (cboclosurestatus) {
            if (cboclosurestatus == 'Closed') {
                $scope.followup_show = true;
            }
            else {
                $scope.followup_show = false;
            }
            if (cboclosurestatus == 'Converted') {
                $scope.completed_show = true;
            }
            else {
                $scope.completed_show = false;
            }
        }


        $scope.Back = function () {
            if (lspage == 'TransferCall') {
                $location.url('app/MstMarketingTransferCallSummary');
            }
            else if (lspage == 'FollowUpCall') {
                $state.go('app.MstMarketingFollowUpCallSummary');
            }
            else if (lspage == 'CompletedCall') {
                $state.go('app.MstMarketingCompletedCallSummary');
            }
            else {
                
            }
        }

        
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstMarketingAssignedFollowupLeadsController', MstMarketingAssignedFollowupLeadsController);

    MstMarketingAssignedFollowupLeadsController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'DownloaddocumentService'];

    function MstMarketingAssignedFollowupLeadsController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstMarketingAssignedFollowupLeadsController';

        $scope.marketingcall_gid = $location.search().marketingcall_gid;
        var marketingcall_gid = $scope.marketingcall_gid;
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;
        $scope.transfershow = false;
        $scope.followupshow = false;
        $scope.completedshow = false;

        lockUI();
        activate();
        function activate() {
            vm.calender1 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open1 = true;
            };
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };

           

            var params = {
                marketingcall_gid: marketingcall_gid
            }
            var url = 'api/Marketing/GetMarketingCallAssignedView';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtticket_refid = resp.data.ticket_refid,
                $scope.txtentity_name = resp.data.entity_name,
                $scope.txtsourceofcontact_name = resp.data.sourceofcontact_name,
                $scope.txtcallreceivednumber_name = resp.data.callreceivednumber_name,
                $scope.txtcustomer_type = resp.data.leadrequesttype_name,
                $scope.txtcallreceived_date = resp.data.callreceived_date,
                $scope.txtcaller_name = resp.data.caller_name,
                $scope.txtinternalreference_name = resp.data.internalreference_name,
                $scope.txtcallerassociate_company = resp.data.callerassociate_company,
                $scope.txtoffice_landlineno = resp.data.office_landlineno,
                $scope.txtcalltype_name = resp.data.calltype_name,
                $scope.txtfunction_name = resp.data.function_name,
                $scope.txtfunction_remarks = resp.data.function_remarks,
                  $scope.txttat_hours = resp.data.tat_hours,
                $scope.txtrequirement = resp.data.requirement,
                $scope.txtenquiry_description = resp.data.enquiry_description,
                $scope.txtcallclosure_status = resp.data.callclosure_status,
                $scope.txtassignemployee_name = resp.data.assignemployee_name,
                $scope.txtassign_date = resp.data.assign_date,
                $scope.txttagemployee_name = resp.data.tagemployee_name,
                $scope.txtassignclosure_remarks = resp.data.assignclosure_remarks,
                $scope.ibcalladdress_list = resp.data.MarketingCalladdress_list;
                $scope.ibcallfollowup_list = resp.data.MarketingCallfollowup_list;
                $scope.ibcalltransfer_list = resp.data.MarketingCalltransfer_list,
                $scope.ibcalltaggedmember_list = resp.data.MarketingCalltaggedmember_list;
                $scope.txtcompleted_by = resp.data.completed_by,
                 $scope.txtcompleted_date = resp.data.completed_date,
                $scope.txtcompleted_remarks = resp.data.completed_remarks,
               $scope.txtfollowup_date = resp.data.followup_date,
                $scope.txtfollowup_time = resp.data.followup_time,
                $scope.txtfollowup_by = resp.data.followup_by,
                $scope.txtfollowup_remarks = resp.data.followup_remarks,
                 $scope.txtrejected_date = resp.data.rejected_date,
                $scope.txtrejected_by = resp.data.rejected_by,
                $scope.txtrejected_remarks = resp.data.rejected_remarks,
                $scope.txtloanproduct_name = resp.data.loanproduct_name,
                $scope.txtloansubproduct_name = resp.data.loansubproduct_name,
                $scope.txtloan_amount = resp.data.loan_amount,
                $scope.txtbase_location = resp.data.baselocation_name,
                  $scope.ibcallmobileno_list = resp.data.MarketingCallmobileno_list;
                $scope.txtprimary_email = resp.data.primary_email,
                $scope.ibcallemail_list = resp.data.MarketingCallemail_list;
                unlockUI();
            });


            var url = 'api/Marketing/MarketingCallProofDocumentList';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.lsfilename = resp.data.filename;
                $scope.lsfilepath = resp.data.filepath;

                $scope.Uploadcallproofdocument_list = resp.data.callproofupload_list;
                unlockUI();
            });
            var url = 'api/Marketing/MarketingCallRecordingDocumentList';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.Uploadcallrecordingdocument_list = resp.data.callproofupload_list;
                unlockUI();
            });

            var url = 'api/Marketing/GetMarketingCallLeadstatusList';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.MarketingCallLeadstatus_list = resp.data.MarketingCallLeadstatus_list;
                unlockUI();
            });

        //for close and convert call
          
        var url = 'api/Marketing/GetEntity';
        SocketService.get(url).then(function (resp) {
            $scope.entity_list = resp.data.inboundentity_list;
        });
        var url = 'api/Marketing/GetLoanProduct';
        lockUI();
        SocketService.get(url).then(function (resp) {
            $scope.samfinloanproduct_data = resp.data.samapplication_list;
            unlockUI();
        });
        var url = 'api/Marketing/GetLoanSubProduct';
        lockUI();
        SocketService.get(url).then(function (resp) {
            $scope.samfinloansubproduct_list = resp.data.samapplication_list;
            unlockUI();
        });
        var url = 'api/Marketing/GetAgrLoanProduct';
        lockUI();
        SocketService.get(url).then(function (resp) {
            $scope.loanproduct_data = resp.data.samapplication_list;
            unlockUI();
        });
        var url = 'api/Marketing/GetAgrLoanSubProduct';
        lockUI();
        SocketService.get(url).then(function (resp) {
            $scope.loansubproduct_list = resp.data.samapplication_list;
            unlockUI();
        });
        leadstatus_list();
   

    }//activate ends here

 //
        //lead status Multiple Add
        $scope.add_leadstatus = function ()
        {
            var lsentity_name = '';
            var lsentity_gid = '';
            lsentity_name = $('#entity :selected').text();
            lsentity_gid = $scope.cboentity;
            var status = lsentity_name;

            if (status == 'SAMFIN') {
                var lsloanproduct_name = '';
                var lsloanproduct_gid = '';
                var lsloansubproduct_name = '';
                var lsloansubproduct_gid = '';

                lsloanproduct_name = $('#saloanproductname :selected').text();
                lsloanproduct_gid = $scope.cboloanproduct;
                lsloansubproduct_name = $('#saloansubproductname :selected').text();
                lsloansubproduct_gid = $scope.cboloansubproduct;

               
              if ((status == 'SAMFIN' || status == 'SAMAGRO') && (($scope.cboloansubproduct == '' || $scope.cboloansubproduct == null) || ($scope.cboloanproduct == '' || $scope.cboloanproduct == null) || ($scope.txt_amount == '' || $scope.txt_amount == null))) {
                    Notify.alert('Kindly Fill Converted Details', 'warning')
                }
                else {
                    var params = {
                        marketingcall_gid: marketingcall_gid,
                        lead_type: lsentity_name,
                        closure_status: $scope.cboclosurestatus,
                        ticket_refid: $scope.txtticket_refid,
                        loanproduct_name: lsloanproduct_name,
                        loanproduct_gid: lsloanproduct_gid,
                        loansubproduct_name: lsloansubproduct_name,
                        loansubproduct_gid: lsloansubproduct_gid,
                        loan_amount: $scope.txt_amount,
                    }
                    var url = 'api/Marketing/PostMarketingCallLeadstatus';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            leadstatus_list();

                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        $scope.txt_amount = '';
                        $scope.cboloansubproduct = '';
                        $scope.cboloanproduct = '';
                    });
                }
            }
            else {
                             
                    var lsloanproduct_name = '';
                    var lsloanproduct_gid = '';
                    var lsloansubproduct_name = '';
                    var lsloansubproduct_gid = '';

                   
                    lsloanproduct_name = $('#loanproductname :selected').text();
                    lsloanproduct_gid = $scope.cboloanproduct;
                    lsloansubproduct_name = $('#loansubproductname :selected').text();
                    lsloansubproduct_gid = $scope.cboloansubproduct;

                    if ((status == 'SAMFIN' || status == 'SAMAGRO') && (($scope.cboloansubproduct == '' || $scope.cboloansubproduct == null) || ($scope.cboloanproduct == '' || $scope.cboloanproduct == null) || ($scope.txt_amount == '' || $scope.txt_amount == null))) {
                        Notify.alert('Kindly Fill Converted Details', 'warning')
                    }
                    else {
                        var params = {
                            marketingcall_gid: marketingcall_gid,
                            lead_type: lsentity_name,
                            closure_status: $scope.cboclosurestatus,
                            ticket_refid: $scope.txtticket_refid,
                            loanproduct_name: lsloanproduct_name,
                            loanproduct_gid: lsloanproduct_gid,
                            loansubproduct_name: lsloansubproduct_name,
                            loansubproduct_gid: lsloansubproduct_gid,
                            loan_amount: $scope.txt_amount,
                        }
                        var url = 'api/Marketing/PostMarketingCallLeadstatus';
                        lockUI();
                        SocketService.post(url, params).then(function (resp) {
                            unlockUI();
                            if (resp.data.status == true) {

                                Notify.alert(resp.data.message, {
                                    status: 'success',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                leadstatus_list();

                            }
                            else {
                                Notify.alert(resp.data.message, {
                                    status: 'warning',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                            }
                            $scope.txt_amount = '';
                            $scope.cboloansubproduct = '';
                            $scope.cboloanproduct = '';
                        });
                    }
                
            }
        }
        $scope.delete_leadstatus = function (marketingcall2leadstatus_gid) {
            var params = {
                marketingcall2leadstatus_gid: marketingcall2leadstatus_gid
            }
            var url = 'api/Marketing/MarketingCallLeadstatusDelete';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                leadstatus_list();
            });
        }


        function leadstatus_list() //GetMarketingCallAssignedView
        {
            var params = {
                marketingcall_gid: marketingcall_gid
            }
            var url = 'api/Marketing/GetMarketingCallLeadstatusList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.ibcallleadstatus_list = resp.data.MarketingCallLeadstatus_list;
              //var  temp = $scope.ibcallleadstatus_list ;
              //  ibcallleadstatus_list =  $scope.ibcallleadstatus_list;

            });
        }

           
        $scope.changeentitiy = function (cboentity) {
            for (var i = 0; i < $scope.entity_list.length; i++)
            {
                if (cboentity == $scope.entity_list[i].entity_gid)
                $scope.entityselect = $scope.entity_list[i].entity_name
            }
        }
        $scope.Back = function () {
            $location.url("app/MstMarketingFollowUpCallSummary");
        }
        $scope.FollowupSubmit = function () {   
          
            if (($scope.cboclosurestatus == 'Closed') && ($scope.cboclosed == '' || $scope.cboclosed == null)) {
               Notify.alert('Kindly Fill Closed', 'warning')
            }
           
            
            else//if(($scope.cboclosurestatus == 'Converted')||($scope.cboclosurestatus == 'Closed'))
            {
               var params = {
                   marketingcall_gid: marketingcall_gid,
                   followup_date: $scope.txtfollowup_date,
                   followup_time: $scope.txtfollowup_time,
                   closed: $scope.cboclosed,
                   closure_status: $scope.cboclosurestatus,
                 
                   followup_remarks: $scope.txtfollowup_remarks
               }
               var url = 'api/Marketing/PostCompletedCall';
               lockUI();
               SocketService.post(url, params).then(function (resp) {
                   unlockUI();
                   if (resp.data.status == true) {

                       Notify.alert(resp.data.message, {
                           status: 'success',
                           pos: 'top-center',
                           timeout: 3000
                       });
                       $location.url("app/MstMarketingFollowUpCallSummary");
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

        //
        $scope.rec_downloads = function (val1, val2) {
            //var phyPath = val1;
            //var relPath = phyPath.split("StoryboardAPI");
            //var relpath1 = relPath[1].replace("\\", "/");
            //var hosts = window.location.host;
            //var prefix = location.protocol + "//";
            //var str = prefix.concat(hosts, relpath1);
            //var link = document.createElement("a");
            //link.download = val2;
            //var uri = str;
            //link.href = uri;
            //link.click();
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.recproof_downloads = function (val1, val2) {
            //var phyPath = val1;
            //var relPath = phyPath.split("StoryboardAPI");
            //var relpath1 = relPath[1].replace("\\", "/");
            //var hosts = window.location.host;
            //var prefix = location.protocol + "//";
            //var str = prefix.concat(hosts, relpath1);
            //var link = document.createElement("a");
            //link.download = val2;
            //var uri = str;
            //link.href = uri;
            //link.click();
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.download_all = function (val1, val2) {

            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }

        }
        $scope.add_leadstatus = function () {
            var lsentity_name = '';
            var lsentity_gid = '';
            lsentity_name = $('#entity :selected').text();
            lsentity_gid = $scope.cboentity;
            var status = lsentity_name;

            if (status == 'SAMFIN') {
                var lsloanproduct_name = '';
                var lsloanproduct_gid = '';
                var lsloansubproduct_name = '';
                var lsloansubproduct_gid = '';

                lsloanproduct_name = $('#saloanproductname :selected').text();
                lsloanproduct_gid = $scope.cboloanproduct;
                lsloansubproduct_name = $('#saloansubproductname :selected').text();
                lsloansubproduct_gid = $scope.cboloansubproduct;


                if ((status == 'SAMFIN' || status == 'SAMAGRO') && (($scope.cboloansubproduct == '' || $scope.cboloansubproduct == null) || ($scope.cboloanproduct == '' || $scope.cboloanproduct == null) || ($scope.txt_amount == '' || $scope.txt_amount == null))) {
                    Notify.alert('Kindly Fill Converted Details', 'warning')
                }
                else {
                    var params = {
                        marketingcall_gid: marketingcall_gid,
                        lead_type: lsentity_name,
                        closure_status: $scope.cboclosurestatus,
                        ticket_refid: $scope.txtticket_refid,
                        loanproduct_name: lsloanproduct_name,
                        loanproduct_gid: lsloanproduct_gid,
                        loansubproduct_name: lsloansubproduct_name,
                        loansubproduct_gid: lsloansubproduct_gid,
                        loan_amount: $scope.txt_amount,
                    }
                    var url = 'api/Marketing/PostMarketingCallLeadstatus';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            leadstatus_list();

                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        $scope.txt_amount = '';
                        $scope.cboloansubproduct = '';
                        $scope.cboloanproduct = '';
                    });
                }
            }
            else {

                var lsloanproduct_name = '';
                var lsloanproduct_gid = '';
                var lsloansubproduct_name = '';
                var lsloansubproduct_gid = '';


                lsloanproduct_name = $('#loanproductname :selected').text();
                lsloanproduct_gid = $scope.cboloanproduct;
                lsloansubproduct_name = $('#loansubproductname :selected').text();
                lsloansubproduct_gid = $scope.cboloansubproduct;

                if ((status == 'SAMFIN' || status == 'SAMAGRO') && (($scope.cboloansubproduct == '' || $scope.cboloansubproduct == null) || ($scope.cboloanproduct == '' || $scope.cboloanproduct == null) || ($scope.txt_amount == '' || $scope.txt_amount == null))) {
                    Notify.alert('Kindly Fill Converted Details', 'warning')
                }
                else {
                    var params = {
                        marketingcall_gid: marketingcall_gid,
                        lead_type: lsentity_name,
                        closure_status: $scope.cboclosurestatus,
                        ticket_refid: $scope.txtticket_refid,
                        loanproduct_name: lsloanproduct_name,
                        loanproduct_gid: lsloanproduct_gid,
                        loansubproduct_name: lsloansubproduct_name,
                        loansubproduct_gid: lsloansubproduct_gid,
                        loan_amount: $scope.txt_amount,
                    }
                    var url = 'api/Marketing/PostMarketingCallLeadstatus';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            leadstatus_list();

                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        $scope.txt_amount = '';
                        $scope.cboloansubproduct = '';
                        $scope.cboloanproduct = '';
                    });
                }

            }
        }
        $scope.delete_leadstatus = function (marketingcall2leadstatus_gid) {
            var params = {
                marketingcall2leadstatus_gid: marketingcall2leadstatus_gid
            }
            var url = 'api/Marketing/MarketingCallLeadstatusDelete';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                leadstatus_list();
            });
        }


        function leadstatus_list() //GetMarketingCallAssignedView
        {
            var params = {
                marketingcall_gid: marketingcall_gid
            }
            var url = 'api/Marketing/GetMarketingCallLeadstatusList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.ibcallleadstatus_list = resp.data.MarketingCallLeadstatus_list;
                //var  temp = $scope.ibcallleadstatus_list ;
                //  ibcallleadstatus_list =  $scope.ibcallleadstatus_list;

            });
        }

        //

        $scope.changeclosurestatus = function (cboclosurestatus) {
            if (cboclosurestatus == 'Closed') {
                $scope.followup_show = true;
            }
            else {
                $scope.followup_show = false;
            }
            if (cboclosurestatus == 'Converted') {
                $scope.completed_show = true;
            }
            else {
                $scope.completed_show = false;
            }
        }
        $scope.Submit = function () {

            if (($scope.cboclosurestatus == 'Closed') && ($scope.cboclosed == '' || $scope.cboclosed == null)) {
                Notify.alert('Kindly Fill Closed', 'warning')
            }


            else//if(($scope.cboclosurestatus == 'Converted')||($scope.cboclosurestatus == 'Closed'))
            {
                var params = {
                    marketingcall_gid: marketingcall_gid,
                    followup_date: $scope.txtfollowup_date,
                    followup_time: $scope.txtfollowup_time,
                    closed: $scope.cboclosed,
                    closure_status: $scope.cboclosurestatus,

                    followup_remarks: $scope.txtfollowup_remarks
                }
                var url = 'api/Marketing/PostFollowupLeadCall';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $location.url("app/MstMarketingWorkInprogressCallSummary");
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
        $scope.credit_amountChange = function () {
            var input = document.getElementById('credit_amount').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lswords_creditamount = inWords(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txt_amount = "";
            }
            else {
                $scope.txt_amount = output;
                document.getElementById('words_creditamt').innerHTML = lswords_creditamount;
            }
        } 
        $scope.limit_amountChange = function () {
            var input = document.getElementById('limit_amount').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lswords_creditamount = inWords(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txt_amount = "";
            }
            else {
                $scope.txt_amount = output;
                document.getElementById('words_limitamt').innerHTML = lswords_creditamount;
            }
        } 
       
         function inWords(num) {
            var a = ['', 'One ', 'Two ', 'Three ', 'Four ', 'Five ', 'Six ', 'Seven ', 'Eight ', 'Nine ', 'Ten ', 'Eleven ', 'Twelve ', 'Thirteen ', 'Fourteen ', 'Fifteen ', 'Sixteen ', 'Seventeen ', 'Eighteen ', 'Nineteen '];
            var b = ['', '', 'Twenty', 'Thirty', 'Forty', 'Fifty', 'Sixty', 'Seventy', 'Eighty', 'Ninety'];
            var s = num.toString();
            s = s.replace(/[\, ]/g, '');
            if (s != parseFloat(s)) return '';
            if ((num = num.toString()).length > 9) return 'Overflow';
            var n = ('000000000' + num).substr(-9).match(/^(\d{2})(\d{2})(\d{2})(\d{1})(\d{2})$/);
            if (!n) return; var str = '';
            str += (n[1] != 0) ? (a[Number(n[1])] || b[n[1][0]] + ' ' + a[n[1][1]]) + 'Crore ' : '';
            str += (n[2] != 0) ? (a[Number(n[2])] || b[n[2][0]] + ' ' + a[n[2][1]]) + 'Lakh ' : '';
            str += (n[3] != 0) ? (a[Number(n[3])] || b[n[3][0]] + ' ' + a[n[3][1]]) + 'Thousand ' : '';
            str += (n[4] != 0) ? (a[Number(n[4])] || b[n[4][0]] + ' ' + a[n[4][1]]) + 'Hundred ' : '';

            str += (n[5] != 0) ? ((str != '') ? 'and ' : '') + (a[Number(n[5])] || b[n[5][0]] + ' ' + a[n[5][1]]) + 'only ' : '';
            return str;
        }

        $scope.Back = function () {
            if (lspage == 'TransferCall') {
                $location.url('app/MstMarketingTransferCallSummary');
            }
            else if (lspage == 'FollowUpCall') {
                $state.go('app.MstMarketingFollowUpCallSummary');
            }
            else if (lspage == 'CompletedCall') {
                $state.go('app.MstMarketingCompletedCallSummary');
            }
            else {

            }
        }
        $scope.add_followup = function () {
            if (($scope.txtfollowup_date == undefined) || ($scope.txtfollowup_date == '') || ($scope.txtfollowup_time == undefined) || ($scope.txtfollowup_time == '')) {
                Notify.alert('Enter Follow Up Date/Follow Up Time', 'warning');
            }
            else {
                var params = {
                    marketingcall_gid:marketingcall_gid,
                    followup_date: $scope.txtfollowup_date,
                    followup_time: $scope.txtfollowup_time,
                    followup_status: $scope.cbofollowup,
                    followup_remarks: $scope.cboremarks,
                }
                var url = 'api/Marketing/PostMarketingCallFollowUpMg';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        followup_list();
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    $scope.txtfollowup_date = '';
                    $scope.cbofollowup = '';
                    $scope.cboremarks = '';
                    followup_list();

                    $scope.txtfollowup_time = '';
                });
            }
        }
        $scope.delete_followup = function (marketingcall2followup_gid) {
            var params = {
                marketingcall2followup_gid: marketingcall2followup_gid
            }
            var url = 'api/Marketing/MarketingCallFollowUpDelete';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    followup_list();
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                followup_list();
            });
        }
        function followup_list() {
            var params = {
                marketingcall_gid: marketingcall_gid
            }
            var url = 'api/Marketing/GetMarketingCallMyFollowUpList';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.ibcallfollowup_list = resp.data.MarketingCallfollowup_list;
            });
        }
        $scope.edit_followup = function (marketingcall2followup_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editinboundcallfollowup.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    marketingcall2followup_gid: marketingcall2followup_gid
                }
                var url = 'api/Marketing/EditMarketingCallFollowUp';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txteditfollowup_date = new Date(resp.data.followup_date);
                    $scope.cboeditfollowup = resp.data.followup_status;
                    $scope.cboremarks = resp.data.followup_remarks;
                    if (resp.data.Tfollowup_time == '0001-01-01T00:00:00') {
                        $scope.txteditfollowup_time = '';
                    }
                    else {
                        $scope.txteditfollowup_time = new Date(resp.data.Tfollowup_time);
                    }
                  
                    followup_list();
                });


                $scope.editfollowup_change = function (cboeditfollowup) {
                    if (cboeditfollowup == 'Hot') {
                        $scope.cboremarks = 'Hot:will be closed in 1 month';
                    }
                    else if (cboeditfollowup == 'Warm') {
                        $scope.cboremarks = 'Warm:will be closed in 3 month';
                    }
                    else if (cboeditfollowup == 'Cold') {
                        $scope.cboremarks = 'Cold: will be closed in 6 month';
                    }
                    else if (cboeditfollowup == 'Others') {
                        $scope.cboremarks = 'Unqualified';
                    }
                    else {
                        $scope.cboremarks = '';
                    }
                }
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update_followup = function () {

                    var params = {
                        followup_date: $scope.txteditfollowup_date,
                        followup_time: $scope.txteditfollowup_time,
                        followup_status: $scope.cboeditfollowup,
                        followup_remarks: $scope.cboremarks,
                        marketingcall2followup_gid: marketingcall2followup_gid,
                        marketingcall_gid: $scope.marketingcall_gid,
                    }
                    var url = 'api/Marketing/UpdateMarketingCallFollowUp';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            followup_list();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                      //  followup_templist();
                        followup_list();

                    });

                    $modalInstance.close('closed');

                }
            }
            followup_list();
        }
    
        $scope.followup_change = function (cbofollowup) {
            if (cbofollowup == 'Hot') {
                $scope.cboremarks = 'Hot:will be closed in 1 month';
            }
            else if (cbofollowup == 'Warm') {
                $scope.cboremarks = 'Warm:will be closed in 3 month';
            }
            else if (cbofollowup == 'Cold') {
                $scope.cboremarks = 'Cold: will be closed in 6 month';
            }
            else if (cbofollowup == 'Others') {
                $scope.cboremarks = 'Unqualified';
            }
            else {
                $scope.cboremarks = '';
            }
        }


    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstMarketingAssignedFollowupLeadsController', MstMarketingAssignedFollowupLeadsController);

    MstMarketingAssignedFollowupLeadsController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'DownloaddocumentService','cmnfunctionService'];

    function MstMarketingAssignedFollowupLeadsController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstMarketingAssignedFollowupLeadsController';

        //$scope.marketingcall_gid = $location.search().marketingcall_gid;
        //var marketingcall_gid = $scope.marketingcall_gid;
        //$scope.lspage = $location.search().lspage;
        //var lspage = $scope.lspage;

        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        var marketingcall_gid = searchObject.marketingcall_gid;
        $scope.lspage = searchObject.lspage;
        var lspage = $scope.lspage;

        $scope.transfershow = false;
        $scope.followupshow = false;
        $scope.completedshow = false;

        lockUI();
        activate();
        function activate() {
            vm.calender1 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open1 = true;
            };
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };

            $scope.minDate = new Date();

            var params = {
                marketingcall_gid: marketingcall_gid
            }
            var url = 'api/Marketing/GetMarketingCallAssignedView';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtticket_refid = resp.data.ticket_refid,
                $scope.txtentity_name = resp.data.entity_name,
                $scope.txtsourceofcontact_name = resp.data.sourceofcontact_name,
                $scope.txtcallreceivednumber_name = resp.data.callreceivednumber_name,
                $scope.txtcustomer_type = resp.data.leadrequesttype_name,
                $scope.txtcallreceived_date = resp.data.callreceived_date,
                $scope.txtcaller_name = resp.data.caller_name,
                $scope.txtinternalreference_name = resp.data.internalreference_name,
                $scope.txtcallerassociate_company = resp.data.callerassociate_company,
                $scope.txtoffice_landlineno = resp.data.office_landlineno,
                $scope.txtcalltype_name = resp.data.calltype_name,
                $scope.txtfunction_name = resp.data.function_name,
                $scope.txtfunction_remarks = resp.data.function_remarks,
                  $scope.txttat_hours = resp.data.tat_hours,
                $scope.txtrequirement = resp.data.requirement,
                $scope.txtenquiry_description = resp.data.enquiry_description,
                $scope.txtcallclosure_status = resp.data.callclosure_status,
                $scope.txtassignemployee_name = resp.data.assignemployee_name,
                $scope.txtassign_date = resp.data.assign_date,
                $scope.txttagemployee_name = resp.data.tagemployee_name,
                $scope.txtassignclosure_remarks = resp.data.assignclosure_remarks,
                $scope.ibcalladdress_list = resp.data.MarketingCalladdress_list;
                $scope.ibcallfollowup_list = resp.data.MarketingCallfollowup_list,
                $scope.ibcalltransfer_list = resp.data.MarketingCalltransfer_list,
                $scope.ibcalltaggedmember_list = resp.data.MarketingCalltaggedmember_list;
                $scope.txtcompleted_by = resp.data.completed_by,
                 $scope.txtcompleted_date = resp.data.completed_date,
                $scope.txtcompleted_remarks = resp.data.completed_remarks,
               $scope.txtfollowup_date = resp.data.followup_date,
                $scope.txtfollowup_time = resp.data.followup_time,
                $scope.txtfollowup_by = resp.data.followup_by,
                $scope.txtfollowup_remarks = resp.data.followup_remarks,
                 $scope.txtrejected_date = resp.data.rejected_date,
                $scope.txtrejected_by = resp.data.rejected_by,
                $scope.txtrejected_remarks = resp.data.rejected_remarks,
                $scope.txtloanproduct_name = resp.data.loanproduct_name,
                $scope.txtloansubproduct_name = resp.data.loansubproduct_name,
                $scope.txtloan_amount = resp.data.loan_amount,
                $scope.txtbase_location = resp.data.baselocation_name,
                $scope.origination = resp.data.origination,
                  $scope.ibcallmobileno_list = resp.data.MarketingCallmobileno_list;
                $scope.txtprimary_email = resp.data.primary_email,
                    $scope.ibcallemail_list = resp.data.MarketingCallemail_list;
                $scope.txtleadrequire_name = resp.data.leadrequire_name,
                    $scope.txtmilletrequire_name = resp.data.milletrequire_name,
                    $scope.txtenquiryrequire_name = resp.data.enquiryrequire_name,
                    $scope.txtstartuprequire_name = resp.data.startuprequire_name,
                    $scope.txtbusiness_name = resp.data.business_name,
                    $scope.txtindustry_name = resp.data.industry_name,

                unlockUI();
            });


            var url = 'api/Marketing/MarketingCallProofDocumentList';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.lufilename = resp.data.filename;
                $scope.lufilepath = resp.data.filepath;
                $scope.Uploadcallproofdocument_list = resp.data.callproofupload_list;
                unlockUI();
            });
            var url = 'api/Marketing/MarketingCallRecordingDocumentList';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.Uploadcallrecordingdocument_list = resp.data.callproofupload_list;
                unlockUI();
            });

            var url = 'api/Marketing/GetMarketingCallLeadstatusList';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.MarketingCallLeadstatus_list = resp.data.MarketingCallLeadstatus_list;
                unlockUI();
            });
            var url = 'api/Marketing/GetDocumentList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.document_list = resp.data.document_list;
            });
        //for close and convert call
          
        var url = 'api/Marketing/GetEntity';
        SocketService.get(url).then(function (resp) {
            $scope.entity_list = resp.data.inboundentity_list;
        });
        var url = 'api/Marketing/GetLoanProduct';
        lockUI();
        SocketService.get(url).then(function (resp) {
            $scope.samfinloanproduct_data = resp.data.samapplication_list;
            unlockUI();
        });
        var url = 'api/Marketing/GetLoanSubProduct';
        lockUI();
        SocketService.get(url).then(function (resp) {
            $scope.samfinloansubproduct_list = resp.data.samapplication_list;
            unlockUI();
        });
        var url = 'api/Marketing/GetAgrLoanProduct';
        lockUI();
        SocketService.get(url).then(function (resp) {
            $scope.loanproduct_data = resp.data.samapplication_list;
            unlockUI();
        });
        var url = 'api/Marketing/GetAgrLoanSubProduct';
        lockUI();
        SocketService.get(url).then(function (resp) {
            $scope.loansubproduct_list = resp.data.samapplication_list;
            unlockUI();
        });
        leadstatus_list();
   
        var url = 'api/Marketing/GetMilletDocumentList';
        SocketService.getparams(url, params).then(function (resp) {
            $scope.lmfilename = resp.data.filename;
            $scope.lmfilepath = resp.data.filepath;
            $scope.milletdocument_list = resp.data.milletdocument_list;
        });
            var url = 'api/Marketing/GetEnquiryDocumentList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.lmfilename = resp.data.filename;
                $scope.lmfilepath = resp.data.filepath;
                $scope.enquirydocument_list = resp.data.enquirydocument_list;
            });
    }//activate ends here

 //
        //lead status Multiple Add
        $scope.download_allmillet = function (val1,val2) {
            for (var i = 0; i < val2.length; i++) {
               //  console.log(array[i]);
               DownloaddocumentService.Downloaddocument(val1, val2[i]);
           }
        }   
        $scope.download_allenquiry = function (val1, val2) {

            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }

        }
       $scope.download_allupload = function (val1,val2) {
        for (var i = 0; i < val2.length; i++) {
           //  console.log(array[i]);
           DownloaddocumentService.Downloaddocument(val1, val2[i]);
       }
   }        
       $scope.milletdocument_downloads = function (val1,val2) {
           DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.enquirydocument_downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
       $scope.documentviewer = function (val1, val2) {
        lockUI();
        var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
        if (IsValidExtension == false) {
        Notify.alert("View is not supported for this format..!", {
         status: 'danger',
         pos: 'top-center',
         timeout: 3000
         });
         unlockUI();
       return false;
         }
         DownloaddocumentService.DocumentViewer(val1, val2);
         }

         $scope.documentviewermillet = function (val1, val2) {
            lockUI();
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
            if (IsValidExtension == false) {
            Notify.alert("View is not supported for this format..!", {
             status: 'danger',
             pos: 'top-center',
             timeout: 3000
             });
             unlockUI();
           return false;
             }
             DownloaddocumentService.DocumentViewer(val1, val2);
             }

             $scope.documentviewerupload = function (val1, val2) {
                lockUI();
                var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
                if (IsValidExtension == false) {
                Notify.alert("View is not supported for this format..!", {
                 status: 'danger',
                 pos: 'top-center',
                 timeout: 3000
                 });
                 unlockUI();
               return false;
                 }
                 DownloaddocumentService.DocumentViewer(val1, val2);
                 }
        $scope.documentviewerenquiry = function (val1, val2) {
            lockUI();
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
            if (IsValidExtension == false) {
                Notify.alert("View is not supported for this format..!", {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                unlockUI();
                return false;
            }
            DownloaddocumentService.DocumentViewer(val1, val2);
        }
        $scope.add_leadstatus = function ()
        {
            var lsentity_name = '';
            var lsentity_gid = '';
            lsentity_name = $('#entity :selected').text();
            lsentity_gid = $scope.cboentity;
            var status = lsentity_name;

            if (status == 'SAMFIN') {
                var lsloanproduct_name = '';
                var lsloanproduct_gid = '';
                var lsloansubproduct_name = '';
                var lsloansubproduct_gid = '';

                lsloanproduct_name = $('#saloanproductname :selected').text();
                lsloanproduct_gid = $scope.cboloanproduct;
                lsloansubproduct_name = $('#saloansubproductname :selected').text();
                lsloansubproduct_gid = $scope.cboloansubproduct;

               
              if ((status == 'SAMFIN' || status == 'SAMAGRO') && (($scope.cboloansubproduct == '' || $scope.cboloansubproduct == null) || ($scope.cboloanproduct == '' || $scope.cboloanproduct == null) || ($scope.txt_amount == '' || $scope.txt_amount == null))) {
                    Notify.alert('Kindly Fill Converted Details', 'warning')
                }
                else {
                    var params = {
                        marketingcall_gid: marketingcall_gid,
                        lead_type: lsentity_name,
                        closure_status: $scope.cboclosurestatus,
                        ticket_refid: $scope.txtticket_refid,
                        loanproduct_name: lsloanproduct_name,
                        loanproduct_gid: lsloanproduct_gid,
                        loansubproduct_name: lsloansubproduct_name,
                        loansubproduct_gid: lsloansubproduct_gid,
                        loan_amount: $scope.txt_amount,
                    }
                    var url = 'api/Marketing/PostMarketingCallLeadstatus';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            leadstatus_list();

                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        $scope.txt_amount = '';
                        $scope.cboloansubproduct = '';
                        $scope.cboloanproduct = '';
                    });
                }
            }
            else {
                             
                    var lsloanproduct_name = '';
                    var lsloanproduct_gid = '';
                    var lsloansubproduct_name = '';
                    var lsloansubproduct_gid = '';

                   
                    lsloanproduct_name = $('#loanproductname :selected').text();
                    lsloanproduct_gid = $scope.cboloanproduct;
                    lsloansubproduct_name = $('#loansubproductname :selected').text();
                    lsloansubproduct_gid = $scope.cboloansubproduct;

                    if ((status == 'SAMFIN' || status == 'SAMAGRO') && (($scope.cboloansubproduct == '' || $scope.cboloansubproduct == null) || ($scope.cboloanproduct == '' || $scope.cboloanproduct == null) || ($scope.txt_amount == '' || $scope.txt_amount == null))) {
                        Notify.alert('Kindly Fill Converted Details', 'warning')
                    }
                    else {
                        var params = {
                            marketingcall_gid: marketingcall_gid,
                            lead_type: lsentity_name,
                            closure_status: $scope.cboclosurestatus,
                            ticket_refid: $scope.txtticket_refid,
                            loanproduct_name: lsloanproduct_name,
                            loanproduct_gid: lsloanproduct_gid,
                            loansubproduct_name: lsloansubproduct_name,
                            loansubproduct_gid: lsloansubproduct_gid,
                            loan_amount: $scope.txt_amount,
                        }
                        var url = 'api/Marketing/PostMarketingCallLeadstatus';
                        lockUI();
                        SocketService.post(url, params).then(function (resp) {
                            unlockUI();
                            if (resp.data.status == true) {

                                Notify.alert(resp.data.message, {
                                    status: 'success',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                leadstatus_list();

                            }
                            else {
                                Notify.alert(resp.data.message, {
                                    status: 'warning',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                            }
                            $scope.txt_amount = '';
                            $scope.cboloansubproduct = '';
                            $scope.cboloanproduct = '';
                        });
                    }
                
            }
        }



        $scope.delete_leadstatus = function (marketingcall2leadstatus_gid) {
            var params = {
                marketingcall2leadstatus_gid: marketingcall2leadstatus_gid
            }
            var url = 'api/Marketing/MarketingCallLeadstatusDelete';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                leadstatus_list();
            });
        }


        function leadstatus_list() //GetMarketingCallAssignedView
        {
            var params = {
                marketingcall_gid: marketingcall_gid
            }
            var url = 'api/Marketing/GetMarketingCallLeadstatusList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.ibcallleadstatus_list = resp.data.MarketingCallLeadstatus_list;
              //var  temp = $scope.ibcallleadstatus_list ;
              //  ibcallleadstatus_list =  $scope.ibcallleadstatus_list;

            });
        }

           
        $scope.changeentitiy = function (cboentity) {
            for (var i = 0; i < $scope.entity_list.length; i++)
            {
                if (cboentity == $scope.entity_list[i].entity_gid)
                $scope.entityselect = $scope.entity_list[i].entity_name
            }
        }
        $scope.Back = function () {
            $location.url("app/MstMarketingFollowUpCallSummary");
        }
        $scope.limit_amountChange = function () {
            var input = document.getElementById('limit_amount').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lswords_creditamount =  cmnfunctionService.fnConvertNumbertoWord(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txt_amount = "";
            }
            else {
                $scope.txt_amount = output;
                document.getElementById('words_limitamt').innerHTML = lswords_creditamount;
            }
        } 
        $scope.credit_amountChange = function () {
            var input = document.getElementById('credit_amount').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lswords_creditamount =  cmnfunctionService.fnConvertNumbertoWord(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txt_amount = "";
            }
            else {
                $scope.txt_amount = output;
                document.getElementById('words_creditamt').innerHTML = lswords_creditamount;
            }
        } 
       
        $scope.FollowupSubmit = function () {   
          
            if (($scope.cboclosurestatus == 'Closed') && ($scope.cboclosed == '' || $scope.cboclosed == null)) {
               Notify.alert('Kindly Fill Closed', 'warning')
            }
           
            
            else//if(($scope.cboclosurestatus == 'Converted')||($scope.cboclosurestatus == 'Closed'))
            {
               var params = {
                   marketingcall_gid: marketingcall_gid,
                   followup_date: $scope.txtfollowup_dates,
                   followup_time: $scope.txtfollowup_time,
                   closed: $scope.cboclosed,
                   closure_status: $scope.cboclosurestatus,
                 
                   followup_remarks: $scope.txtfollowup_remarks
               }
               var url = 'api/Marketing/PostCompletedCall';
               lockUI();
               SocketService.post(url, params).then(function (resp) {
                   unlockUI();
                   if (resp.data.status == true) {

                       Notify.alert(resp.data.message, {
                           status: 'success',
                           pos: 'top-center',
                           timeout: 3000
                       });
                       $location.url("app/MstMarketingFollowUpCallSummary");
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

        //
        $scope.rec_downloads = function (val1, val2) {
            //var phyPath = val1;
            //var relPath = phyPath.split("StoryboardAPI");
            //var relpath1 = relPath[1].replace("\\", "/");
            //var hosts = window.location.host;
            //var prefix = location.protocol + "//";
            //var str = prefix.concat(hosts, relpath1);
            //var link = document.createElement("a");
            //link.download = val2;
            //var uri = str;
            //link.href = uri;
            //link.click();
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.recproof_downloads = function (val1, val2) {
            //var phyPath = val1;
            //var relPath = phyPath.split("StoryboardAPI");
            //var relpath1 = relPath[1].replace("\\", "/");
            //var hosts = window.location.host;
            //var prefix = location.protocol + "//";
            //var str = prefix.concat(hosts, relpath1);
            //var link = document.createElement("a");
            //link.download = val2;
            //var uri = str;
            //link.href = uri;
            //link.click();
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.download_all = function (val1, val2) {

            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }

        }
        $scope.add_leadstatus = function () {
            var lsentity_name = '';
            var lsentity_gid = '';
            lsentity_name = $('#entity :selected').text();
            lsentity_gid = $scope.cboentity;
            var status = lsentity_name;

            if (status == 'SAMFIN') {
                var lsloanproduct_name = '';
                var lsloanproduct_gid = '';
                var lsloansubproduct_name = '';
                var lsloansubproduct_gid = '';

                lsloanproduct_name = $('#saloanproductname :selected').text();
                lsloanproduct_gid = $scope.cboloanproduct;
                lsloansubproduct_name = $('#saloansubproductname :selected').text();
                lsloansubproduct_gid = $scope.cboloansubproduct;


                if ((status == 'SAMFIN' || status == 'SAMAGRO') && (($scope.cboloansubproduct == '' || $scope.cboloansubproduct == null) || ($scope.cboloanproduct == '' || $scope.cboloanproduct == null) || ($scope.txt_amount == '' || $scope.txt_amount == null))) {
                    Notify.alert('Kindly Fill Converted Details', 'warning')
                }
                else {
                    var params = {
                        marketingcall_gid: marketingcall_gid,
                        lead_type: lsentity_name,
                        closure_status: $scope.cboclosurestatus,
                        ticket_refid: $scope.txtticket_refid,
                        loanproduct_name: lsloanproduct_name,
                        loanproduct_gid: lsloanproduct_gid,
                        loansubproduct_name: lsloansubproduct_name,
                        loansubproduct_gid: lsloansubproduct_gid,
                        loan_amount: $scope.txt_amount,
                    }
                    var url = 'api/Marketing/PostMarketingCallLeadstatus';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            leadstatus_list();

                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        $scope.txt_amount = '';
                        $scope.cboloansubproduct = '';
                        $scope.cboloanproduct = '';
                    });
                }
            }
            else {

                var lsloanproduct_name = '';
                var lsloanproduct_gid = '';
                var lsloansubproduct_name = '';
                var lsloansubproduct_gid = '';


                lsloanproduct_name = $('#loanproductname :selected').text();
                lsloanproduct_gid = $scope.cboloanproduct;
                lsloansubproduct_name = $('#loansubproductname :selected').text();
                lsloansubproduct_gid = $scope.cboloansubproduct;

                if ((status == 'SAMFIN' || status == 'SAMAGRO') && (($scope.cboloansubproduct == '' || $scope.cboloansubproduct == null) || ($scope.cboloanproduct == '' || $scope.cboloanproduct == null) || ($scope.txt_amount == '' || $scope.txt_amount == null))) {
                    Notify.alert('Kindly Fill Converted Details', 'warning')
                }
                else {
                    var params = {
                        marketingcall_gid: marketingcall_gid,
                        lead_type: lsentity_name,
                        closure_status: $scope.cboclosurestatus,
                        ticket_refid: $scope.txtticket_refid,
                        loanproduct_name: lsloanproduct_name,
                        loanproduct_gid: lsloanproduct_gid,
                        loansubproduct_name: lsloansubproduct_name,
                        loansubproduct_gid: lsloansubproduct_gid,
                        loan_amount: $scope.txt_amount,
                    }
                    var url = 'api/Marketing/PostMarketingCallLeadstatus';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            leadstatus_list();

                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        $scope.txt_amount = '';
                        $scope.cboloansubproduct = '';
                        $scope.cboloanproduct = '';
                    });
                }

            }
        }
        $scope.delete_leadstatus = function (marketingcall2leadstatus_gid) {
            var params = {
                marketingcall2leadstatus_gid: marketingcall2leadstatus_gid
            }
            var url = 'api/Marketing/MarketingCallLeadstatusDelete';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                leadstatus_list();
            });
        }


        function leadstatus_list() //GetMarketingCallAssignedView
        {
            var params = {
                marketingcall_gid: marketingcall_gid
            }
            var url = 'api/Marketing/GetMarketingCallLeadstatusList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.ibcallleadstatus_list = resp.data.MarketingCallLeadstatus_list;
                //var  temp = $scope.ibcallleadstatus_list ;
                //  ibcallleadstatus_list =  $scope.ibcallleadstatus_list;

            });
        }

        //

        $scope.changeclosurestatus = function (cboclosurestatus) {
            if (cboclosurestatus == 'Closed') {
                $scope.followup_show = true;
            }
            else {
                $scope.followup_show = false;
            }
            if (cboclosurestatus == 'Converted') {
                $scope.completed_show = true;
            }
            else {
                $scope.completed_show = false;
            }
        }
        $scope.Submit = function () {

            if (($scope.cboclosurestatus == 'Closed') && ($scope.cboclosed == '' || $scope.cboclosed == null)) {
                Notify.alert('Kindly Fill Closed', 'warning')
            }


            else//if(($scope.cboclosurestatus == 'Converted')||($scope.cboclosurestatus == 'Closed'))
            {
                var params = {
                    marketingcall_gid: marketingcall_gid,
                    followup_date: $scope.txtfollowup_dates,
                    followup_time: $scope.txtfollowup_time,
                    closed: $scope.cboclosed,
                    closure_status: $scope.cboclosurestatus,

                    followup_remarks: $scope.txtfollowup_remarks
                }
                var url = 'api/Marketing/PostFollowupLeadCall';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $location.url("app/MstMarketingWorkInprogressCallSummary");
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


        $scope.Back = function () {
            if (lspage == 'TransferCall') {
                $location.url('app/MstMarketingTransferCallSummary');
            }
            else if (lspage == 'FollowUpCall') {
                $state.go('app.MstMarketingFollowUpCallSummary');
            }
            else if (lspage == 'CompletedCall') {
                $state.go('app.MstMarketingCompletedCallSummary');
            }
            else if (lspage == 'MyFollowUpLead') {
                $state.go('app.MstMarketingFollowUpCallSummary');
            }
            else {

            }
        }
        $scope.add_followup = function () {
            if (($scope.txtfollowup_dates == undefined) || ($scope.txtfollowup_dates == '') || ($scope.txtfollowup_time == undefined) || ($scope.txtfollowup_time == '')) {
                Notify.alert('Enter Follow Up Date/Follow Up Time', 'warning');
            }
            else {
                var params = {
                    marketingcall_gid:marketingcall_gid,
                    followup_date: $scope.txtfollowup_dates,
                    followup_time: $scope.txtfollowup_time,
                    followup_status: $scope.cbofollowup,
                    followup_remarks: $scope.cboremarks,
                }
                var url = 'api/Marketing/PostMarketingCallFollowUpMg';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        followup_list();
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    $scope.txtfollowup_dates = '';
                    $scope.cbofollowup = '';
                    $scope.cboremarks = '';
                    followup_list();

                    $scope.txtfollowup_time = '';
                });
            }
        }
        $scope.delete_followup = function (marketingcall2followup_gid) {
            var params = {
                marketingcall2followup_gid: marketingcall2followup_gid
            }
            var url = 'api/Marketing/MarketingCallFollowUpDelete';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    followup_list();
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                followup_deletelist();
            });
        }
        function followup_deletelist() {
            var params = {
                marketingcall_gid: marketingcall_gid
            }
            var url = 'api/Marketing/GetMarketingCallMyFollowUpList';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.ibcallfollowup_list = resp.data.MarketingCallfollowup_list;
            });
        }
        function followup_list() {
            var params = {
                marketingcall_gid: marketingcall_gid
            }
            var url = 'api/Marketing/GetMarketingCallMyFollowUpList';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.ibcallfollowup_list = resp.data.MarketingCallfollowup_list;
            });
        }
        $scope.edit_followup = function (marketingcall2followup_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editinboundcallfollowup.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    marketingcall2followup_gid: marketingcall2followup_gid
                }
                var url = 'api/Marketing/EditMarketingCallFollowUp';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txteditfollowup_date = new Date(resp.data.followup_date);
                    $scope.cboeditfollowup = resp.data.followup_status;
                    $scope.cboremarks = resp.data.followup_remarks;
                    if (resp.data.Tfollowup_time == '0001-01-01T00:00:00') {
                        $scope.txteditfollowup_time = '';
                    }
                    else {
                        $scope.txteditfollowup_time = new Date(resp.data.Tfollowup_time);
                    }
                  
                    followup_list();
                });


                $scope.editfollowup_change = function (cboeditfollowup) {
                    if (cboeditfollowup == 'Hot') {
                        $scope.cboremarks = 'Hot:will be closed in 1 month';
                    }
                    else if (cboeditfollowup == 'Warm') {
                        $scope.cboremarks = 'Warm:will be closed in 3 month';
                    }
                    else if (cboeditfollowup == 'Cold') {
                        $scope.cboremarks = 'Cold: will be closed in 6 month';
                    }
                    else if (cboeditfollowup == 'Others') {
                        $scope.cboremarks = 'Unqualified';
                    }
                    else {
                        $scope.cboremarks = '';
                    }
                }
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update_followup = function () {

                    var params = {
                        followup_date: $scope.txteditfollowup_date,
                        followup_time: $scope.txteditfollowup_time,
                        followup_status: $scope.cboeditfollowup,
                        followup_remarks: $scope.cboremarks,
                        marketingcall2followup_gid: marketingcall2followup_gid,
                        marketingcall_gid: $scope.marketingcall_gid,
                    }
                    var url = 'api/Marketing/UpdateMarketingCallFollowUp';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            followup_list();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                      //  followup_templist();
                        followup_list();

                    });

                    $modalInstance.close('closed');

                }
            }
            followup_list();
        }
    
        $scope.followup_change = function (cbofollowup) {
            if (cbofollowup == 'Hot') {
                $scope.cboremarks = 'Hot:will be closed in 1 month';
            }
            else if (cbofollowup == 'Warm') {
                $scope.cboremarks = 'Warm:will be closed in 3 month';
            }
            else if (cbofollowup == 'Cold') {
                $scope.cboremarks = 'Cold: will be closed in 6 month';
            }
            else if (cbofollowup == 'Others') {
                $scope.cboremarks = 'Unqualified';
            }
            else {
                $scope.cboremarks = '';
            }
        }


    }
})();


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstMarketingAssignViewController', MstMarketingAssignViewController);

    MstMarketingAssignViewController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'DownloaddocumentService','cmnfunctionService'];

    function MstMarketingAssignViewController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstMarketingAssignViewController';
        //$scope.marketingcall_gid = $location.search().lsmarketingcall_gid;
        //var marketingcall_gid = $scope.marketingcall_gid;
        //$scope.lspage = $location.search().lspage;
        //var lspage = $scope.lspage;
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        var marketingcall_gid = searchObject.lsmarketingcall_gid;
        var lspage = searchObject.lspage;
       
        activate();        
        function activate() {
            var params = {
                marketingcall_gid:marketingcall_gid
            }
             var url = 'api/Marketing/GetMarketingCallAssignedView';
            SocketService.getparams(url,params).then(function (resp) {
                    $scope.txtticket_refid = resp.data.ticket_refid,
                    $scope.txtentity_name = resp.data.entity_name,
                    $scope.txtsourceofcontact_name = resp.data.sourceofcontact_name,
                    $scope.txtcallreceivednumber_name = resp.data.callreceivednumber_name,
                    $scope.txtcustomer_type = resp.data.leadrequesttype_name,
                    $scope.txtcallreceived_date = resp.data.callreceived_date,
                    $scope.txtcaller_name = resp.data.caller_name,
                    $scope.txtinternalreference_name = resp.data.internalreference_name,
                    $scope.txtcallerassociate_company = resp.data.callerassociate_company,
                    $scope.txtoffice_landlineno = resp.data.office_landlineno,
                    $scope.txtcalltype_name = resp.data.calltype_name,
                    $scope.txtfunction_name = resp.data.function_name,
                    $scope.txtfunction_remarks = resp.data.function_remarks,
                    $scope.txttat_hours = resp.data.tat_hours,
                    $scope.txtrequirement = resp.data.requirement,
                    $scope.ibcalltransfer_list = resp.data.MarketingCalltransfer_list,//
                    $scope.txtenquiry_description = resp.data.enquiry_description,
                    $scope.txtcallclosure_status = resp.data.callclosure_status,
                    $scope.txtassignemployee_name = resp.data.assignemployee_name,
                    $scope.txtassign_date = resp.data.assign_date,
                    $scope.txtbase_location = resp.data.baselocation_name,
                    $scope.txttagemployee_name = resp.data.tagemployee_name,
                    $scope.txtassignclosure_remarks = resp.data.assignclosure_remarks,
                $scope.ibcallextendfollowup_list = resp.data.marketingcallextendfollowup_list,
                    $scope.txtprimary_mobileno = resp.data.primary_mobileno,
                    $scope.ibcallmobileno_list = resp.data.MarketingCallmobileno_list;
                    $scope.txtprimary_email = resp.data.primary_email,
                    $scope.ibcallemail_list = resp.data.MarketingCallemail_list;
                    $scope.ibcallfollowup_list = resp.data.MarketingCallfollowup_list;
                    $scope.ibcalladdress_list = resp.data.MarketingCalladdress_list;
                    $scope.origination = resp.data.origination;
                    $scope.ibcalltaggedmember_list = resp.data.MarketingCalltaggedmember_list;
                $scope.txtleadrequire_name = resp.data.leadrequire_name,
                    $scope.txtmilletrequire_name = resp.data.milletrequire_name,
                    $scope.txtenquiryrequire_name = resp.data.enquiryrequire_name,
                    $scope.txtstartuprequire_name = resp.data.startuprequire_name,
                    $scope.txtbusiness_name = resp.data.business_name,
                    $scope.txtindustry_name = resp.data.industry_name,

                unlockUI();
            }); 
            var url = 'api/Marketing/GetDocumentList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.lsfilename = resp.data.filename;
                $scope.lsfilepath = resp.data.filepath;
                $scope.document_list = resp.data.document_list;
            });
            var url = 'api/Marketing/GetMilletDocumentList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.lmfilename = resp.data.filename;
                $scope.lmfilepath = resp.data.filepath;
                $scope.milletdocument_list = resp.data.milletdocument_list;
            });
            var url = 'api/Marketing/GetEnquiryDocumentList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.lmfilename = resp.data.filename;
                $scope.lmfilepath = resp.data.filepath;
                $scope.enquirydocument_list = resp.data.enquirydocument_list;
            });
        }
        $scope.document_downloads = function (val1,val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.download_all = function (val1,val2) {
            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }
        }
        $scope.download_allmillet = function (val1,val2) {
            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }
        }        
        $scope.milletdocument_downloads = function (val1,val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.download_allenquiry = function (val1, val2) {
            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }
        }
        $scope.enquirydocument_downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.documentviewer = function (val1, val2) {
            lockUI();
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
            if (IsValidExtension == false) {
            Notify.alert("View is not supported for this format..!", {
             status: 'danger',
             pos: 'top-center',
             timeout: 3000
             });
             unlockUI();
           return false;
             }
             DownloaddocumentService.DocumentViewer(val1, val2);
             }

             $scope.documentviewermillet = function (val1, val2) {
                lockUI();
                var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
                if (IsValidExtension == false) {
                Notify.alert("View is not supported for this format..!", {
                 status: 'danger',
                 pos: 'top-center',
                 timeout: 3000
                 });
                 unlockUI();
               return false;
                 }
                 DownloaddocumentService.DocumentViewer(val1, val2);
             }
        $scope.documentviewerenquiry = function (val1, val2) {
            lockUI();
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
            if (IsValidExtension == false) {
                Notify.alert("View is not supported for this format..!", {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                unlockUI();
                return false;
            }
            DownloaddocumentService.DocumentViewer(val1, val2);
        }

        $scope.Back = function () {
            if (lspage == 'MarketingAddCall') {
                $location.url('app/MstMarketingSummary');
            }
            else if (lspage == 'AssignedMarketing') {
                $state.go('app.MstAssignedMarketingSummary');
            }
            else if (lspage == 'MarketingUnassignedLead') 
          {
                $state.go('app.MstMarketingUnassignedLeadSummary');
            }
        }

    }
})();
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstMarketingCallReceivedNumberController', MstMarketingCallReceivedNumberController);

    MstMarketingCallReceivedNumberController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstMarketingCallReceivedNumberController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstMarketingCallReceivedNumberController';

        activate();

        function activate() {          
            var url = 'api/MstMarketingCallReceivedNumber/GetMarketingCallReceivedNumber';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.callreceivednumber_list = resp.data.marketingcallreceivednumber_list;
                unlockUI();
            });
        }
        $scope.addcallreceivednumber = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addcallreceivednumber.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                               
                $scope.back = function () {
                    $modalInstance.close('closed');
                };
                $scope.submit = function () {
                    var params = {
                        marketingcallreceivednumber_name: $scope.txtcallreceivednumber_name,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code
                    }
                    var url = 'api/MstMarketingCallReceivedNumber/CreateMarketingCallReceivedNumber';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                            activate();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                            activate();
                        }
                    });
                }
                
            }
        }
        $scope.editcallreceivednumber = function (marketingcallreceivednumber_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editcallreceivednumber.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.back = function () {
                    $modalInstance.close('closed');
                };
                var params = {
                    marketingcallreceivednumber_gid: marketingcallreceivednumber_gid
                }
                var url = 'api/MstMarketingCallReceivedNumber/EditMarketingCallReceivedNumber';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txteditcallreceivednumber_name = resp.data.marketingcallreceivednumber_name;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.marketingcallreceivednumber_gid = resp.data.marketingcallreceivednumber_gid;
                });
                $scope.back = function () {
                    $modalInstance.close('closed');
                };
                $scope.update = function () {
                    var url = 'api/MstMarketingCallReceivedNumber/UpdateMarketingCallReceivedNumber';
                    var params = {
                        marketingcallreceivednumber_name: $scope.txteditcallreceivednumber_name,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        marketingcallreceivednumber_gid: $scope.marketingcallreceivednumber_gid
                    }
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                        else {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                    });
                }
            }
        }
        $scope.Status_update = function (marketingcallreceivednumber_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statuscallreceivednumber.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    marketingcallreceivednumber_gid: marketingcallreceivednumber_gid
                }
                var url = 'api/MstMarketingCallReceivedNumber/EditmarketingCallReceivedNumber';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.marketingcallreceivednumber_gid = resp.data.marketingcallreceivednumber_gid
                    $scope.txtcallreceivednumber_name = resp.data.marketingcallreceivednumber_name;
                    $scope.rbo_status = resp.data.Status;
                });
                $scope.back = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {
                    var params = {
                        marketingcallreceivednumber_gid: marketingcallreceivednumber_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status
                    }
                    var url = 'api/MstMarketingCallReceivedNumber/InactiveMarketingCallReceivedNumber';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        } activate();
                    });
                    $modalInstance.close('closed');
                }
                var params = {
                    marketingcallreceivednumber_gid: marketingcallreceivednumber_gid
                }
                var url = 'api/MstMarketingCallReceivedNumber/InactiveMarketingCallReceivedNumberHistory';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.callreceivednumberinactivelog_list = resp.data.marketingcallreceivednumberinactivehistory_list;
                    unlockUI();
                });
            }
        }

        $scope.delete = function (marketingcallreceivednumber_gid) {
            var params = {
                marketingcallreceivednumber_gid: marketingcallreceivednumber_gid
            }
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do You Want To Delete the Record ?',
                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Yes, delete it!',
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    var url = 'api/MstMarketingCallReceivedNumber/DeleteMarketingCallReceivedNumber';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            SweetAlert.swal('Deleted Successfully!');
                            activate();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                    });
                }
            });
        }
    }
})();
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstMarketingCallResponseController', MstMarketingCallResponseController);

    MstMarketingCallResponseController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'DownloaddocumentService', 'cmnfunctionService'];

    function MstMarketingCallResponseController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstMarketingCallResponseController';
       
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        var marketingcall_gid = searchObject.marketingcall_gid;
        lockUI();
        activate();        
        function activate() {
            vm.calender1 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open1 = true;
            };
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };
            var params = {
                   marketingcall_gid:marketingcall_gid
               }
               var url = 'api/Marketing/GetMarketingCallAssignedView';
               SocketService.getparams(url, params).then(function (resp) {
                   $scope.txtticket_refid = resp.data.ticket_refid,
                   $scope.txtentity_name = resp.data.entity_name,
                   $scope.txtsourceofcontact_name = resp.data.sourceofcontact_name,
                   $scope.txtcallreceivednumber_name = resp.data.callreceivednumber_name,
                   $scope.txtcustomer_type = resp.data.leadrequesttype_name,
                   $scope.txtcallreceived_date = resp.data.callreceived_date,
                   $scope.txtcaller_name = resp.data.caller_name,
                   $scope.txtinternalreference_name = resp.data.internalreference_name,
                   $scope.txtcallerassociate_company = resp.data.callerassociate_company,
                   $scope.txtoffice_landlineno = resp.data.office_landlineno,
                   $scope.txtcalltype_name = resp.data.calltype_name,
                   $scope.txtfunction_name = resp.data.function_name,
                   $scope.txtfunction_remarks = resp.data.function_remarks,
                  $scope.txttat_hours = resp.data.tat_hours,
                   $scope.txtrequirement = resp.data.requirement,
                   $scope.txtenquiry_description = resp.data.enquiry_description,
                   $scope.txtcallclosure_status = resp.data.callclosure_status,
                   $scope.txtassignemployee_name = resp.data.assignemployee_name,
                   $scope.txtbase_location = resp.data.baselocation_name,
                   $scope.txtassign_date = resp.data.assign_date,
                   $scope.origination = resp.data.origination,
                   $scope.txttagemployee_name = resp.data.tagemployee_name,
                   $scope.txtassignclosure_remarks = resp.data.assignclosure_remarks,
                    $scope.ibcalladdress_list = resp.data.MarketingCalladdress_list;
                   $scope.ibcallfollowup_list = resp.data.MarketingCallfollowup_list;
                   $scope.ibcalltaggedmember_list = resp.data.MarketingCalltaggedmember_list;
                   $scope.txtacknowledgement_date = resp.data.acknowledge_date;
                   $scope.txtprimary_mobileno = resp.data.primary_mobileno,
                   $scope.ibcallmobileno_list = resp.data.MarketingCallmobileno_list;
                   $scope.txtprimary_email = resp.data.primary_email,
                       $scope.ibcallemail_list = resp.data.MarketingCallemail_list;
                   $scope.txtleadrequire_name = resp.data.leadrequire_name,
                       $scope.txtmilletrequire_name = resp.data.milletrequire_name,
                       $scope.txtenquiryrequire_name = resp.data.enquiryrequire_name,
                       $scope.txtstartuprequire_name = resp.data.startuprequire_name,
                       $scope.txtbusiness_name = resp.data.business_name,
                       $scope.txtindustry_name = resp.data.industry_name,

                   unlockUI();
               });
            var url = 'api/Marketing/GetDocumentList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.lsfilename = resp.data.filename;
                $scope.lsfilepath = resp.data.filepath;
                $scope.document_list = resp.data.document_list;
            });
            var url = 'api/Marketing/GetMilletDocumentList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.lmfilename = resp.data.filename;
                $scope.lmfilepath = resp.data.filepath;
                $scope.milletdocument_list = resp.data.milletdocument_list;
            });
            var url = 'api/Marketing/GetEnquiryDocumentList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.lmfilename = resp.data.filename;
                $scope.lmfilepath = resp.data.filepath;
                $scope.enquirydocument_list = resp.data.enquirydocument_list;
            });
        }

        $scope.Back = function () {
            $location.url("app/MstMarketingMyAssignedCallSummary");
        }

        $scope.Reject = function () {
            var modalInstance = $modal.open({
                templateUrl: '/rejectrequest.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.ok = function () {
                    modalInstance.close('closed');
                };
                $scope.rejectSubmit = function () {
                    var params = {
                        marketingcall_gid: marketingcall_gid,
                        reject_remarks: $scope.txtreject_remarks
                    }
                    var url = 'api/Marketing/RejectMarketingCall';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            modalInstance.close('closed');
                            $state.go("app.MstMarketingMyAssignedCallSummary");
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            modalInstance.close('closed');
                        }
                    });
                    $state.go("app.MstMarketingMyAssignedCallSummary");
                }
            }
        }
        $scope.acknowledge = function () {
            var url = 'api/Marketing/PostUpdateAck';
            lockUI();
            var params = {
                marketingcall_gid: marketingcall_gid
            }
            SocketService.post(url, params).then(function (resp) {

                if (resp.data.status == true) {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $location.url("app/MstMarketingMyAssignedCallSummary");
                }
                else {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $location.url("app/MstMarketingMyAssignedCallSummary");
                }
            });
        }
          $scope.closed_call = function (marketingcall_gid) {

            var modalInstance = $modal.open({
                templateUrl: '/closedContent.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];
            function ModalInstanceCtrl($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {


                var params = {
                    marketingcall_gid: marketingcall_gid
                }
                
                $scope.close_call = function () {                   
                    var params = {
                        marketingcall_gid: marketingcall_gid,                       
                        closed_remarks: $scope.closed_remarks
                    }

                    var url = "api/Marketing/MarketingCallAssignedClosed";
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status = true) {
                            modalInstance.close('closed');
                            Notify.alert(resp.data.message, 'success');
                            activate();
                        }
                        else {
                            modalInstance.close('closed');
                            Notify.alert(resp.data.message, 'warning');
                            activate();
                        }
                    });


                }
                
                $scope.close = function () {
                    modalInstance.close('closed');
                };
            }
        }
        $scope.download_all = function (val1, val2) {

            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }

        }
        $scope.document_downloads = function (val1,val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.download_allmillet = function (val1, val2) {
            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }
        }
        $scope.download_allenquiry = function (val1, val2) {
            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }
        }
        $scope.milletdocument_downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.enquirydocument_downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.documentviewer = function (val1, val2) {
            lockUI();
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
            if (IsValidExtension == false) {
            Notify.alert("View is not supported for this format..!", {
             status: 'danger',
             pos: 'top-center',
             timeout: 3000
             });
             unlockUI();
           return false;
             }
             DownloaddocumentService.DocumentViewer(val1, val2);
             }

             $scope.documentviewermillet = function (val1, val2) {
                lockUI();
                var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
                if (IsValidExtension == false) {
                Notify.alert("View is not supported for this format..!", {
                 status: 'danger',
                 pos: 'top-center',
                 timeout: 3000
                 });
                 unlockUI();
               return false;
                 }
                 DownloaddocumentService.DocumentViewer(val1, val2);
        }
        $scope.documentviewerenquiry = function (val1, val2) {
            lockUI();
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
            if (IsValidExtension == false) {
                Notify.alert("View is not supported for this format..!", {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                unlockUI();
                return false;
            }
            DownloaddocumentService.DocumentViewer(val1, val2);
        }
    }
})();
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstMarketingCallTypeController', MstMarketingCallTypeController);

    MstMarketingCallTypeController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstMarketingCallTypeController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstMarketingCallTypeController';

        activate();

        function activate() {          
            var url = 'api/MstMarketingCallType/GetCreateMarketingCallType';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.calltype_list = resp.data.marketingcalltype_list;
                unlockUI();
            });
        }
        $scope.addcalltype = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addcalltype.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                               
                $scope.back = function () {
                    $modalInstance.close('closed');
                };
                $scope.submit = function () {
                    var params = {
                        marketingcalltype_name: $scope.txtcalltype_name,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code
                    }
                    var url = 'api/MstMarketingCallType/CreateMarketingCallType';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                            activate();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                            activate();
                        }
                    });
                }
                
            }
        }
        $scope.editcalltype = function (marketingcalltype_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editcalltype.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.back = function () {
                    $modalInstance.close('closed');
                };
                var params = {
                    marketingcalltype_gid: marketingcalltype_gid
                }
                var url = 'api/MstMarketingCallType/EditMarketingCallType';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txteditcalltype_name = resp.data.marketingcalltype_name;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.marketingcalltype_gid = resp.data.marketingcalltype_gid;
                });
                $scope.back = function () {
                    $modalInstance.close('closed');
                };
                $scope.update = function () {
                    var url = 'api/MstMarketingCallType/UpdateMarketingCallType';
                    var params = {
                        marketingcalltype_name: $scope.txteditcalltype_name,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        marketingcalltype_gid: $scope.marketingcalltype_gid
                    }
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                        else {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                    });
                }
            }
        }
        $scope.Status_update = function (marketingcalltype_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statuscalltype.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    marketingcalltype_gid: marketingcalltype_gid
                }
                var url = 'api/MstMarketingCallType/EditMarketingCallType';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.marketingcalltype_gid = resp.data.marketingcalltype_gid
                    $scope.txtcalltype_name = resp.data.marketingcalltype_name;
                    $scope.rbo_status = resp.data.Status;
                });
                $scope.back = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {
                    var params = {
                        marketingcalltype_gid: marketingcalltype_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status
                    }
                    var url = 'api/MstMarketingCallType/InactiveMarketingCallType';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        } activate();
                    });
                    $modalInstance.close('closed');
                }
                var params = {
                    marketingcalltype_gid: marketingcalltype_gid
                }
                var url = 'api/MstMarketingCallType/InactiveMarketingCallTypeHistory';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.calltypeinactivelog_list = resp.data.marketinginactivehistory_list;
                    unlockUI();
                });
            }
        }
        $scope.delete = function (marketingcalltype_gid) {
            var params = {
                marketingcalltype_gid: marketingcalltype_gid
            }
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do You Want To Delete the Record ?',
                showSpinner: true,
                showCancelButton: true,
  //confirmButtonColor: '#3085d6',
  CancelButtonColor: '#3085d6',
  confirmButtonText: 'Yes, delete it!',
               // showCancelButton: true,
              //  cancelButtonColor: '#d9dcde',
              //  showCancelButton: true,
                confirmButtonColor: '#d64b3c',
               // confirmButtonText: 'Yes, delete it!',
              //  showConfirmButton: true,
             // confirmButtonClass: 'btn btn-success',
cancelButtonClass: 'btn btn-danger',
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    var url = 'api/MstMarketingCallType/DeleteMarketingCallType';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            SweetAlert.swal('Deleted Successfully!');
                            activate();
                        }
                        
  else if (resp.data.status == false) {
    SweetAlert.swal({
        title: 'Warning!',
        text: "Can't able to delete Enquiry Type because it is mapped to add Business Development call",
        timer: 5000,
       
        showCancelButton: false,
        showConfirmButton: false,
        
        backgroundcolor: '#d64b3c'
});
    activate();
    }

                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                    });
                }
            });
        }
    }
})();
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstMarketingCloseController', MstMarketingCloseController);

    MstMarketingCloseController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'DownloaddocumentService', 'cmnfunctionService'];

    function MstMarketingCloseController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstMarketingCloseController';
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        var marketingcall_gid = searchObject.lsmarketingcall_gid;

        activate();
        function activate() {

            $scope.followup_show = false;

            vm.calender1 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open1 = true;
            };

            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };

            var params = {
                marketingcall_gid:marketingcall_gid
            }
            var url = 'api/Marketing/GetMarketingCallAssignedView';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtticket_refid = resp.data.ticket_refid,
                $scope.txtentity_name = resp.data.entity_name,
                $scope.txtsourceofcontact_name = resp.data.sourceofcontact_name,
                $scope.txtcallreceivednumber_name = resp.data.callreceivednumber_name,
                $scope.txtcustomer_type = resp.data.leadrequesttype_name,
                $scope.txtcallreceived_date = resp.data.callreceived_date,
                $scope.txtcaller_name = resp.data.caller_name,
                $scope.txtinternalreference_name = resp.data.internalreference_name,
                $scope.txtcallerassociate_company = resp.data.callerassociate_company,
                $scope.txtoffice_landlineno = resp.data.office_landlineno,
                $scope.txtcalltype_name = resp.data.calltype_name,
                $scope.txtfunction_name = resp.data.function_name,
                $scope.txtrequirement = resp.data.requirement,
                $scope.txtenquiry_description = resp.data.enquiry_description,
                $scope.txtcallclosure_status = resp.data.callclosure_status,
                $scope.txtassignemployee_name = resp.data.assignemployee_name,
                $scope.txttagemployee_name = resp.data.tagemployee_name,
                $scope.txtassignclosure_remarks = resp.data.assignclosure_remarks,
                $scope.txtcompleted_by = resp.data.completed_by,
                $scope.txtcompleted_remarks = resp.data.completed_remarks,
                $scope.txttat_hours = resp.data.tat_hours,
                $scope.txtfunction_remarks = resp.data.function_remarks,
                $scope.txtloanproduct_name = resp.data.loanproduct_name,
                $scope.txtloansubproduct_name = resp.data.loansubproduct_name,
                $scope.txtloan_amount = resp.data.loan_amount,
                 $scope.txtbase_location = resp.data.baselocation_name,
                 $scope.ibcalladdress_list = resp.data.MarketingCalladdress_list,
                $scope.ibcallfollowup_list = resp.data.MarketingCallfollowup_list,
                $scope.ibcalltaggedmember_list = resp.data.MarketingCalltaggedmember_list,
                 $scope.ibcallmobileno_list = resp.data.MarketingCallmobileno_list;
                $scope.txtprimary_email = resp.data.primary_email,
                    $scope.ibcallemail_list = resp.data.MarketingCallemail_list;
                $scope.origination = resp.data.origination;
                $scope.txtleadrequire_name = resp.data.leadrequire_name,
                    $scope.txtmilletrequire_name = resp.data.milletrequire_name,
                    $scope.txtenquiryrequire_name = resp.data.enquiryrequire_name,
                    $scope.txtstartuprequire_name = resp.data.startuprequire_name,
                    $scope.txtbusiness_name = resp.data.business_name,
                    $scope.txtindustry_name = resp.data.industry_name,

                unlockUI();
            });
        
            var url = 'api/Marketing/MarketingCallProofDocumentList';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.lsfilename1 = resp.data.filename;
                $scope.lsfilepath1 = resp.data.filepath;
                $scope.Uploadcallproofdocument_list = resp.data.callproofupload_list;
                unlockUI();
            });
            var url = 'api/Marketing/MarketingCallRecordingDocumentList';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.Uploadcallrecordingdocument_list = resp.data.callproofupload_list;
                unlockUI();
            });
            var url = 'api/Marketing/GetMarketingCallLeadstatusList';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.MarketingCallLeadstatus_list = resp.data.MarketingCallLeadstatus_list;
                unlockUI();
            });
            var url = 'api/Marketing/GetDocumentList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.lsfilename = resp.data.filename;
                $scope.lsfilepath = resp.data.filepath;
                $scope.document_list = resp.data.document_list;
            });
            var url = 'api/Marketing/GetMilletDocumentList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.lmfilename = resp.data.filename;
                $scope.lmfilepath = resp.data.filepath;
                $scope.milletdocument_list = resp.data.milletdocument_list;
            });
            var url = 'api/Marketing/GetEnquiryDocumentList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.lmfilename = resp.data.filename;
                $scope.lmfilepath = resp.data.filepath;
                $scope.enquirydocument_list = resp.data.enquirydocument_list;
            });
        }
        $scope.download_all = function (val1, val2) {

            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }

        }
        $scope.downloads_all = function (val1, val2) {

            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }

        }
        $scope.document_downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.download_allmillet = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.milletdocument_downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.download_allenquiry = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.enquirydocument_downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.rec_downloads = function (val1, val2) {
            //var phyPath = val1;
            //var relPath = phyPath.split("StoryboardAPI");
            //var relpath1 = relPath[1].replace("\\", "/");
            //var hosts = window.location.host;
            //var prefix = location.protocol + "//";
            //var str = prefix.concat(hosts, relpath1);
            //var link = document.createElement("a");
            //link.download = val2;
            //var uri = str;
            //link.href = uri;
            //link.click();
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.recproof_downloads = function (val1, val2) {
            //var phyPath = val1;
            //var relPath = phyPath.split("StoryboardAPI");
            //var relpath1 = relPath[1].replace("\\", "/");
            //var hosts = window.location.host;
            //var prefix = location.protocol + "//";
            //var str = prefix.concat(hosts, relpath1);
            //var link = document.createElement("a");
            //link.download = val2;
            //var uri = str;
            //link.href = uri;
            //link.click();
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.documentviewer = function (val1, val2) {
            lockUI();
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
            if (IsValidExtension == false) {
                Notify.alert("View is not supported for this format..!", {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                unlockUI();
                return false;
            }
            DownloaddocumentService.DocumentViewer(val1, val2);
        }

        $scope.documentviewermillet = function (val1, val2) {
            lockUI();
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
            if (IsValidExtension == false) {
                Notify.alert("View is not supported for this format..!", {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                unlockUI();
                return false;
            }
            DownloaddocumentService.DocumentViewer(val1, val2);
        }
        $scope.documentviewerenquiry = function (val1, val2) {
            lockUI();
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
            if (IsValidExtension == false) {
                Notify.alert("View is not supported for this format..!", {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                unlockUI();
                return false;
            }
            DownloaddocumentService.DocumentViewer(val1, val2);
        }
        $scope.Back = function () {
            $location.url('app/MstMarketingCompletedCall');
        }

        $scope.changeclosurestatus = function (cboclosurestatus) {
            if (cboclosurestatus == 'Extend Follow Up') {
                $scope.followup_show = true;
            }
            else {
                $scope.followup_show = false;
            }
            if (cboclosurestatus == 'Rejected') {
                $scope.Rejected_show = true;
            }
            else {
                $scope.Rejected_show = false;
            }
        }

        var closeRef = document.querySelector('#close_i');
        closeRef.addEventListener('keypress', function(evt)  {
  var code = evt.keyCode;
  var val = evt.currentTarget.value;
  var len = val.length;
  console.log(code); 
 
  if(len === 0 && code === 32) {
      console.log('do not allow white space more');
      evt.preventDefault();
  }
});
var followRef = document.querySelector('#follow_i');
followRef.addEventListener('keypress',function(evt)  {
    var code = evt.keyCode;
    var val = evt.currentTarget.value;
    var len = val.length;
  console.log(code); 
 
  if(len === 0 && code === 32) {
      console.log('do not allow white space more');
      evt.preventDefault();
  }
});

        $scope.submit = function () {
              if (($scope.cboclosurestatus == 'Extend Follow Up') && ($scope.txtfollowup_date == '' || $scope.txtfollowup_date == null || $scope.txtfollowup_time == '' || $scope.txtfollowup_time == null || $scope.txtremarks == '' || $scope.txtremarks == null)) {
                Notify.alert('Kindly Fill Follow Up Details', 'warning')
            }
            else if (($scope.cboclosurestatus == 'Rejected') &&( $scope.closedremarks == '' || $scope.closedremarks == null)) {
                  Notify.alert('Kindly Fill Rejected Remarks', 'warning')
            }
            else {
                var params = {
                    marketingcall_gid: marketingcall_gid,
                    followup_date: $scope.txtfollowup_date,
                    followup_time: $scope.txtfollowup_time,
                    followup_remarks: $scope.txtremarks,
                    closure_status: $scope.cboclosurestatus,
                    closed_remarks: $scope.closedremarks
                }
                var url = 'api/Marketing/PostCloseCall';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $state.go("app.MstMarketingCompletedCall");
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
                $state.go("app.MstMarketingCompletedCall");
                activate();
            }
        }
}
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstMarketingClosedCallController', MstMarketingClosedCallController);

    MstMarketingClosedCallController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'cmnfunctionService'];

    function MstMarketingClosedCallController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstMarketingClosedCallController';
        activate();
        lockUI();
        function activate() {
            var url = 'api/Marketing/GetClosedMarketingCallSummary';
            SocketService.get(url).then(function (resp) {
                $scope.ibcall_list = resp.data.MarketingCall_list;
                unlockUI();
            });           

            var url = "api/Marketing/MarketingCallCount";
            SocketService.get(url).then(function (resp) {
                $scope.unassignedcall_count = resp.data.unassignedcall_count;
                $scope.assignedcall_count = resp.data.assignedcall_count;
                $scope.completedcall_count = resp.data.completedcall_count;
                $scope.followupcall_count = resp.data.followupcall_count;
                $scope.closedcall_count = resp.data.closedcall_count;
                $scope.rejectedcall_count = resp.data.rejectedcall_count;
                unlockUI();
            });

        }
        $scope.addinbound = function () {
            $location.url("app/MstMarketingAdd");
        }
        $scope.closedcall= function() {
            $location.url("app/MstMarketingClosedCall");
        }
        $scope.followupcall= function() {
            $location.url("app/MstMarketingFollowupCall");
        }
        $scope.assignedcall= function() {
            $location.url("app/MstMarketingSummary");
        }
        $scope.unassignedcall= function() {
            $location.url("app/MstMarketingUnassignedLeadSummary");
        }
        $scope.completedcall= function() {
            $location.url("app/MstMarketingCompletedCall");
        }
        $scope.edit_inboundcall = function (marketingcall_gid) {
            $location.url('app/MstMarketingEdit?hash=' + cmnfunctionService.encryptURL('lsmarketingcall_gid=' + marketingcall_gid));
        }
        $scope.view_inboundcall = function (marketingcall_gid) {
            $location.url('app/MstMarketingClosedView?hash=' + cmnfunctionService.encryptURL('lsmarketingcall_gid=' + marketingcall_gid + '&lspage=MarketingCloseAddLead'));
        }
        $scope.rejectedcall = function () {
            $location.url("app/MstMarketingRejectedCallSummary");
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstMarketingClosedViewController', MstMarketingClosedViewController);

    MstMarketingClosedViewController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams','DownloaddocumentService','cmnfunctionService'];

    function MstMarketingClosedViewController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstMarketingClosedViewController';
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        var marketingcall_gid = searchObject.lsmarketingcall_gid;
        $scope.lspage = searchObject.lspage;
        var lspage = $scope.lspage;

        activate();
        lockUI();
        function activate() {
            var params = {
                marketingcall_gid:marketingcall_gid
            }
            var url = 'api/Marketing/GetMarketingCallAssignedView';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtticket_refid = resp.data.ticket_refid,
                $scope.txtentity_name = resp.data.entity_name,
                $scope.txtsourceofcontact_name = resp.data.sourceofcontact_name,
                $scope.txtcallreceivednumber_name = resp.data.callreceivednumber_name,
                $scope.txtcustomer_type = resp.data.leadrequesttype_name,
                $scope.txtcallreceived_date = resp.data.callreceived_date,
                $scope.txtcaller_name = resp.data.caller_name,
                $scope.txtinternalreference_name = resp.data.internalreference_name,
                $scope.txtcallerassociate_company = resp.data.callerassociate_company,
                $scope.txtoffice_landlineno = resp.data.office_landlineno,
                $scope.txtcalltype_name = resp.data.calltype_name,
                $scope.txtfunction_name = resp.data.function_name,
                $scope.txtrequirement = resp.data.requirement,
                $scope.txtenquiry_description = resp.data.enquiry_description,
                $scope.txtcallclosure_status = resp.data.callclosure_status,
                $scope.txtassignemployee_name = resp.data.assignemployee_name,
                $scope.txttagemployee_name = resp.data.tagemployee_name,
                $scope.txtassignclosure_remarks = resp.data.assignclosure_remarks,
                
                $scope.txtclosed_remarks = resp.data.closed_remarks,
                 $scope.txtfunction_remarks = resp.data.function_remarks,
                    $scope.ibcalladdress_list = resp.data.MarketingCalladdress_list;
                $scope.txttat_hours = resp.data.tat_hours,
               $scope.txtprimary_mobileno = resp.data.primary_mobileno,
                    $scope.ibcallmobileno_list = resp.data.MarketingCallmobileno_list;
                $scope.txtbase_location = resp.data.baselocation_name,
                $scope.txtprimary_email = resp.data.primary_email,
                $scope.ibcallemail_list = resp.data.MarketingCallemail_list;
                $scope.origination = resp.data.origination;

                $scope.ibcallfollowup_list = resp.data.MarketingCallfollowup_list;

                $scope.ibcalltaggedmember_list = resp.data.MarketingCalltaggedmember_list;
                $scope.txtclosed_date = resp.data.closed_date;
                $scope.txtclosed_by = resp.data.closed_by;
                $scope.txtrejected_date = resp.data.rejected_date;
                $scope.txtrejected_by = resp.data.rejected_by;
                $scope.txtrejected_remarks = resp.data.rejected_remarks,
                $scope.txtclosed = resp.data.closed;
                $scope.txtleadrequire_name = resp.data.leadrequire_name,
                    $scope.txtmilletrequire_name = resp.data.milletrequire_name,
                    $scope.txtenquiryrequire_name = resp.data.enquiryrequire_name,
                    $scope.txtstartuprequire_name = resp.data.startuprequire_name,
                    $scope.txtbusiness_name = resp.data.business_name,
                    $scope.txtindustry_name = resp.data.industry_name,

                unlockUI();
            });

            var url = 'api/Marketing/GetMarketingCallLeadstatusList';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.MarketingCallLeadstatus_list = resp.data.MarketingCallLeadstatus_list;
                unlockUI();
            });
            var url = 'api/Marketing/MarketingCallProofDocumentList';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.lufilename = resp.data.filename;
                $scope.lufilepath = resp.data.filepath;
                $scope.Uploadcallproofdocument_list = resp.data.callproofupload_list;
                unlockUI();
            });
            var url = 'api/Marketing/MarketingCallRecordingDocumentList';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.Uploadcallrecordingdocument_list = resp.data.callproofupload_list;
                unlockUI();
            });
            var url = 'api/Marketing/GetDocumentList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.lsfilename = resp.data.filename;
                $scope.lsfilepath = resp.data.filepath;
                $scope.document_list = resp.data.document_list;
            });
            var url = 'api/Marketing/GetMilletDocumentList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.lmfilename = resp.data.filename;
                $scope.lmfilepath = resp.data.filepath;
                $scope.milletdocument_list = resp.data.milletdocument_list;
            });
            var url = 'api/Marketing/GetEnquiryDocumentList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.lmfilename = resp.data.filename;
                $scope.lmfilepath = resp.data.filepath;
                $scope.enquirydocument_list = resp.data.enquirydocument_list;
            });
        }

        $scope.download_allmillet = function (val1,val2) {
            for (var i = 0; i < val2.length; i++) {
               //  console.log(array[i]);
               DownloaddocumentService.Downloaddocument(val1, val2[i]);
           }
        }     
        $scope.download_allenquiry = function (val1, val2) {
            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }
        }     
       $scope.download_allupload = function (val1,val2) {
        for (var i = 0; i < val2.length; i++) {
           //  console.log(array[i]);
           DownloaddocumentService.Downloaddocument(val1, val2[i]);
       }
   }        
       $scope.milletdocument_downloads = function (val1,val2) {
           DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.enquirydocument_downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.document_downloads = function (val1,val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.recproof_downloads = function (val1,val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.documentviewerupload = function (val1, val2) {
            lockUI();
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
            if (IsValidExtension == false) {
            Notify.alert("View is not supported for this format..!", {
             status: 'danger',
             pos: 'top-center',
             timeout: 3000
             });
             unlockUI();
           return false;
             }
             DownloaddocumentService.DocumentViewer(val1, val2);
             }
        $scope.documentviewer = function (val1, val2) {
            lockUI();
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
            if (IsValidExtension == false) {
            Notify.alert("View is not supported for this format..!", {
             status: 'danger',
             pos: 'top-center',
             timeout: 3000
             });
             unlockUI();
           return false;
             }
             DownloaddocumentService.DocumentViewer(val1, val2);
             }

             $scope.documentviewermillet = function (val1, val2) {
                lockUI();
                var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
                if (IsValidExtension == false) {
                Notify.alert("View is not supported for this format..!", {
                 status: 'danger',
                 pos: 'top-center',
                 timeout: 3000
                 });
                 unlockUI();
               return false;
                 }
                 DownloaddocumentService.DocumentViewer(val1, val2);
        }
        $scope.documentviewerenquiry = function (val1, val2) {
            lockUI();
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
            if (IsValidExtension == false) {
                Notify.alert("View is not supported for this format..!", {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                unlockUI();
                return false;
            }
            DownloaddocumentService.DocumentViewer(val1, val2);
        }
        $scope.download_all = function (val1, val2) {

            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }

        }
     //   $scope.Back = function(){
       //     $location.url("app/MstMarketingClosedCall");
     //   }
        $scope.Back = function () {
            if (lspage == 'MarketingCloseAddLead') {
                $location.url('app/MstMarketingClosedCall');
            }
            else if (lspage == 'MarketingCloseMyLead') {
                $state.go('app.MstMarketingMyleadsClosedCall');
            }           
        }
    }
})();
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstMarketingCompletedCallController', MstMarketingCompletedCallController);

    MstMarketingCompletedCallController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'cmnfunctionService'];

    function MstMarketingCompletedCallController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstMarketingCompletedCallController';
        activate();
        lockUI();
        function activate() {
            var url = 'api/Marketing/GetCompletedMarketingCallSummary';
            SocketService.get(url).then(function (resp) {
                $scope.ibcall_list = resp.data.MarketingCall_list;
                unlockUI();
            });           

            var url = "api/Marketing/MarketingCallCount";
            SocketService.get(url).then(function (resp) {
                $scope.unassignedcall_count = resp.data.unassignedcall_count;
                $scope.assignedcall_count = resp.data.assignedcall_count;
                $scope.completedcall_count = resp.data.completedcall_count;
                $scope.followupcall_count = resp.data.followupcall_count;
                $scope.closedcall_count = resp.data.closedcall_count;
                $scope.rejectedcall_count = resp.data.rejectedcall_count;
                unlockUI();
            });

        }
        $scope.addinbound = function () {
            $location.url("app/MstMarketingAdd");
        }
        $scope.closedcall= function() {
            $location.url("app/MstMarketingClosedCall");
        }
        $scope.followupcall= function() {
            $location.url("app/MstMarketingFollowupCall");
        }
        $scope.assignedcall= function() {
            $location.url("app/MstMarketingSummary");
        }
        $scope.completedcall= function() {
            $location.url("app/MstMarketingCompletedCall");
        }
        $scope.unassignedcall= function() {
            $location.url("app/MstMarketingUnassignedLeadSummary");
        }
        $scope.edit_inboundcall = function (marketingcall_gid) {
            $location.url('app/MstMarketingEdit?hash=' + cmnfunctionService.encryptURL('lsmarketingcall_gid=' + marketingcall_gid));
        }
        $scope.view_inboundcall = function (marketingcall_gid) {
            $location.url('app/MstMarketingCompletedView?hash=' + cmnfunctionService.encryptURL('lsmarketingcall_gid=' + marketingcall_gid));
        }
        $scope.close_inboundcall = function (marketingcall_gid) {
            $location.url('app/MstMarketingClose?hash=' + cmnfunctionService.encryptURL('lsmarketingcall_gid=' + marketingcall_gid));
        }

        $scope.rejectedcall = function () {
            $location.url("app/MstMarketingRejectedCallSummary");
        }

    }
})();
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstMarketingCompletedCallSummaryController', MstMarketingCompletedCallSummaryController);

    MstMarketingCompletedCallSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'cmnfunctionService'];

    function MstMarketingCompletedCallSummaryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstMarketingCompletedCallSummaryController';
        activate();
        lockUI();
        function activate() {
            var url = 'api/Marketing/GetEmpCompletedMarketingCallSummary';
            SocketService.get(url).then(function (resp) {
                $scope.marketingcallcompleted_list = resp.data.MarketingCall_list;
                unlockUI();
            });           

            var url = "api/Marketing/EmployeeMarketingCallCount";
            SocketService.get(url).then(function (resp) {
                $scope.assignedcall_count = resp.data.assignedcall_count;
                $scope.transfercall_count = resp.data.transfercall_count;
                $scope.completedcall_count = resp.data.completedcall_count;
                $scope.inprogresscall_count = resp.data.inprogresscall_count;
                $scope.tagmember_count = resp.data.taggedcall_count;
                $scope.followupcall_count = resp.data.followupcall_count;
                $scope.rejectedcall_count = resp.data.rejectedcall_count;
                $scope.closedcall_count = resp.data.closedcall_count;

                unlockUI();
            });
        }
        $scope.closedcall= function() {
            $location.url("app/MstMarketingMyleadsClosedCall");
        }
        $scope.myassigned_calls = function () {
            $location.url("app/MstMarketingMyAssignedCallSummary");
        }
        $scope.tag_member = function () {
            $location.url("app/MstMarketingTaggedMemberSummary");
        }
        $scope.transfer_calls = function () {
            $location.url("app/MstMarketingTransferCallSummary");
        }
        $scope.completed_calls = function () {
            $location.url("app/MstMarketingCompletedCallSummary");
        }
        $scope.view = function (marketingcall_gid) {
            $location.url("app/MstMarketingAssignedCallView?hash=" + cmnfunctionService.encryptURL("marketingcall_gid=" + marketingcall_gid + '&lspage=CompletedCall'));
        }
        $scope.edit = function (marketingcall_gid) {
            $location.url("app/MstMarketingEdit?hash=" + cmnfunctionService.encryptURL("marketingcall_gid=" + marketingcall_gid));
        }
        $scope.work_inprogress = function () {
            $location.url("app/MstMarketingWorkInprogressCallSummary");
        }
        $scope.followup_call = function () {
            $location.url("app/MstMarketingFollowUpCallSummary");
        }
        $scope.mbdrejected_calls = function () {
            $location.url("app/MstMarketingMBDRejectedCallSummary");
        }
        $scope.transfer = function (marketingcall_gid, ticketref_no, assigned_to) {

            var modalInstance = $modal.open({
                templateUrl: '/transferContent.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];
            function ModalInstanceCtrl($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {
                var url = 'api/OsdTrnCustomerQueryMgmt/TransferEmployee';
                SocketService.get(url).then(function (resp) {
                    $scope.TransferEmployeeList = resp.data.TransferEmployeeList;
                    unlockUI();
                });

                $scope.marketingcall_gid = marketingcall_gid;
                $scope.ticketref_no = ticketref_no;
                $scope.assigned_to = assigned_to;

                $scope.transfercall = function () {

                    if ($scope.transfer_to == undefined) {
                        modalInstance.close('closed');
                        Notify.alert('Kindly Select the Assign to Person', 'warning');
                        return;
                    }

                    var params = {
                        marketingcall_gid: $scope.marketingcall_gid,
                        employee_gid: $scope.transfer_to,
                        employee_name: $('#transfer_to :selected').text(),
                        transfer_remarks: $scope.transfer_remarks
                    }

                    var url = "api/MstTelecalling/TicketTransfer";
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status = true) {
                            modalInstance.close('closed');
                            Notify.alert(resp.data.message, 'success');
                            activate();
                        }
                        else {
                            modalInstance.close('closed');
                            Notify.alert(resp.data.message, 'warning');
                            activate();
                        }
                    });


                }
                /* var url = 'api/MstTelecalling/TransferLog';
                 var params = {
                     inboundcall_gid:inboundcall_gid
                 }
                 SocketService.getparams(url, params).then(function (resp) {
                         $scope.TransferLog_list = resp.data.transferLog_list;
                 }); */
                $scope.close = function () {
                    modalInstance.close('closed');
                };
            }
        }
    }
})();
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstMarketingCompletedViewController', MstMarketingCompletedViewController);

    MstMarketingCompletedViewController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'DownloaddocumentService','cmnfunctionService'];

    function MstMarketingCompletedViewController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstMarketingCompletedViewController';
       
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        var marketingcall_gid = searchObject.lsmarketingcall_gid;
        activate();
        lockUI();
        function activate()
        {
               var params = {
                marketingcall_gid:marketingcall_gid
            }
        
            var url = 'api/Marketing/GetMarketingCallAssignedView';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtticket_refid = resp.data.ticket_refid,
                $scope.txtentity_name = resp.data.entity_name,
                $scope.txtsourceofcontact_name = resp.data.sourceofcontact_name,
                $scope.txtcallreceivednumber_name = resp.data.callreceivednumber_name,
                $scope.txtcustomer_type = resp.data.leadrequesttype_name,
                $scope.txtcallreceived_date = resp.data.callreceived_date,
                $scope.txtcaller_name = resp.data.caller_name,
                $scope.txtinternalreference_name = resp.data.internalreference_name,
                $scope.txtcallerassociate_company = resp.data.callerassociate_company,
                $scope.txtoffice_landlineno = resp.data.office_landlineno,
                $scope.txtcalltype_name = resp.data.calltype_name,
                $scope.txtfunction_name = resp.data.function_name,
                 $scope.txtfunction_remarks = resp.data.function_remarks,
                 $scope.txttat_hours = resp.data.tat_hours,
                $scope.txtrequirement = resp.data.requirement,
                $scope.txtenquiry_description = resp.data.enquiry_description,
                $scope.txtcallclosure_status = resp.data.callclosure_status,
                $scope.txtassignemployee_name = resp.data.assignemployee_name,
                $scope.txttagemployee_name = resp.data.tagemployee_name,
                $scope.txtassignclosure_remarks = resp.data.assignclosure_remarks,
                $scope.txtcompleted_by = resp.data.completed_by,
                $scope.txtcompleted_date = resp.data.completed_date,
                $scope.txtcompleted_remarks = resp.data.completed_remarks,
                $scope.ibcalladdress_list = resp.data.MarketingCalladdress_list;
                $scope.txtprimary_mobileno = resp.data.primary_mobileno;
                $scope.ibcallmobileno_list = resp.data.MarketingCallmobileno_list;
                $scope.txtprimary_email = resp.data.primary_email;
                $scope.ibcallemail_list = resp.data.MarketingCallemail_list;
                $scope.ibcallfollowup_list = resp.data.MarketingCallfollowup_list;
                $scope.ibcalltransfer_list = resp.data.MarketingCalltransfer_list,
                $scope.ibcalltaggedmember_list = resp.data.MarketingCalltaggedmember_list;
                $scope.txtloanproduct_name = resp.data.loanproduct_name,
                $scope.txtloansubproduct_name = resp.data.loansubproduct_name,
                $scope.txtloan_amount = resp.data.loan_amount,
                  $scope.txtbase_location = resp.data.baselocation_name,
                  $scope.origination = resp.data.origination,
                    $scope.txtleadrequire_name = resp.data.leadrequire_name,
                    $scope.txtmilletrequire_name = resp.data.milletrequire_name,
                    $scope.txtenquiryrequire_name = resp.data.enquiryrequire_name,
                    $scope.txtstartuprequire_name = resp.data.startuprequire_name,
                    $scope.txtbusiness_name = resp.data.business_name,
                    $scope.txtindustry_name = resp.data.industry_name,

                unlockUI();
            });

            var url = 'api/Marketing/MarketingCallProofDocumentList';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.lsfilename1 = resp.data.filename;
                $scope.lsfilepath1 = resp.data.filepath;
                $scope.Uploadcallproofdocument_list = resp.data.callproofupload_list;
                unlockUI();
            });
            var url = 'api/Marketing/MarketingCallRecordingDocumentList';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.Uploadcallrecordingdocument_list = resp.data.callproofupload_list;
                unlockUI();
            });
            var url = 'api/Marketing/GetMarketingCallLeadstatusList';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.MarketingCallLeadstatus_list = resp.data.MarketingCallLeadstatus_list;
                unlockUI();
            });
            var url = 'api/Marketing/GetDocumentList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.lsfilename = resp.data.filename;
                $scope.lsfilepath = resp.data.filepath;
                $scope.document_list = resp.data.document_list;
            });
            var url = 'api/Marketing/GetMilletDocumentList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.lmfilename = resp.data.filename;
                $scope.lmfilepath = resp.data.filepath;
                $scope.milletdocument_list = resp.data.milletdocument_list;
            });
            var url = 'api/Marketing/GetEnquiryDocumentList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.lmfilename = resp.data.filename;
                $scope.lmfilepath = resp.data.filepath;
                $scope.enquirydocument_list = resp.data.enquirydocument_list;
            });
        }
        $scope.download_all = function (val1, val2) {

            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }

        }
        $scope.document_downloads = function (val1,val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.download_allmillet = function (val1,val2) {
            for (var i = 0; i < val2.length; i++) {
               //  console.log(array[i]);
               DownloaddocumentService.Downloaddocument(val1, val2[i]);
           }
        }   
        $scope.download_allenquiry = function (val1, val2) {
            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }
        }   
        $scope.downloads_all = function (val1, val2) {

            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }

        }
       $scope.milletdocument_downloads = function (val1,val2) {
           DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.enquirydocument_downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.rec_downloads = function (val1, val2) {
            //var phyPath = val1;
            //var relPath = phyPath.split("StoryboardAPI");
            //var relpath1 = relPath[1].replace("\\", "/");
            //var hosts = window.location.host;
            //var prefix = location.protocol + "//";
            //var str = prefix.concat(hosts, relpath1);
            //var link = document.createElement("a");
            //link.download = val2;
            //var uri = str;
            //link.href = uri;
            //link.click();
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.recproof_downloads = function (val1, val2) {
            //var phyPath = val1;
            //var relPath = phyPath.split("StoryboardAPI");
            //var relpath1 = relPath[1].replace("\\", "/");
            //var hosts = window.location.host;
            //var prefix = location.protocol + "//";
            //var str = prefix.concat(hosts, relpath1);
            //var link = document.createElement("a");
            //link.download = val2;
            //var uri = str;
            //link.href = uri;
            //link.click();
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.documentviewer = function (val1, val2) {
            lockUI();
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
            if (IsValidExtension == false) {
            Notify.alert("View is not supported for this format..!", {
             status: 'danger',
             pos: 'top-center',
             timeout: 3000
             });
             unlockUI();
           return false;
             }
             DownloaddocumentService.DocumentViewer(val1, val2);
        }

             $scope.documentviewermillet = function (val1, val2) {
                lockUI();
                var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
                if (IsValidExtension == false) {
                Notify.alert("View is not supported for this format..!", {
                 status: 'danger',
                 pos: 'top-center',
                 timeout: 3000
                 });
                 unlockUI();
               return false;
                 }
                 DownloaddocumentService.DocumentViewer(val1, val2);
        }
        $scope.documentviewerenquiry = function (val1, val2) {
            lockUI();
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
            if (IsValidExtension == false) {
                Notify.alert("View is not supported for this format..!", {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                unlockUI();
                return false;
            }
            DownloaddocumentService.DocumentViewer(val1, val2);
        }
        $scope.documentviewerupload = function (val1, val2) {
            lockUI();
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
            if (IsValidExtension == false) {
                Notify.alert("View is not supported for this format..!", {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                unlockUI();
                return false;
            }
            DownloaddocumentService.DocumentViewer(val1, val2);
        }
        $scope.Back = function () {
            $location.url('app/MstMarketingCompletedCall');
        }
    }
}());
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstMarketingEditController', MstMarketingEditController);

    MstMarketingEditController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams','DownloaddocumentService','cmnfunctionService'];

    function MstMarketingEditController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstMarketingEditController';
        //$scope.marketingcall_gid = $location.search().lsmarketingcall_gid;
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        $scope.marketingcall_gid = searchObject.lsmarketingcall_gid;
        activate();

        function activate() {
            $scope.cbotagemployee = [];
            vm.calender1 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open1 = true;
            };
            vm.calender2 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open2 = true;
            };
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };
            $scope.cbointernal_reference = "NA";
            $scope.minDate = new Date();

            var today = new Date();
            var date = today.getDate() + '-' + (today.getMonth() + 1) + '-' + today.getFullYear();
          //  $scope.txtcallreceived_date = date;

            var url = 'api/Marketing/MarketingCallTempClear';
            SocketService.get(url).then(function (resp) {
            });

            var param = {
                marketingcall_gid: $scope.marketingcall_gid
            };

            var url = 'api/Marketing/GetEntity';
            SocketService.get(url).then(function (resp) {
                $scope.entity_list = resp.data.inboundentity_list;
            });
            var url = 'api/employee/Employee';
            SocketService.get(url).then(function (resp) {
                $scope.employee_list = resp.data.employee_list;
            }); 
            var url = 'api/Marketing/GetMarketingSourceofContact';
            SocketService.get(url).then(function (resp) {
                $scope.sourceofcontact_list = resp.data.MarketingSourceofContact_list;
            });
            var url = 'api/Marketing/GetMarketingCallReceivedNumber';
            SocketService.get(url).then(function (resp) {
                $scope.callreceivednumber_list = resp.data.MarketingCallReceivedNumber_list;
            });
            var url = 'api/Marketing/GetMarketingCallType';
            SocketService.get(url).then(function (resp) {
                $scope.calltype_list = resp.data.MarketingCallType_list;
            });
            var url = 'api/Marketing/GetMarketingTelecallingFunction';
            SocketService.get(url).then(function (resp) {
                $scope.telecallingfunction_list = resp.data.MarketingTelecallingFunction_list;
            });
            var url = 'api/Marketing/GetLeadRequestType';
            SocketService.get(url).then(function (resp) {
                $scope.leadrequesttype_list = resp.data.leadrequest_list;
            });
            var url = 'api/MarMstMilletRequire/GetMilletRequire';
            SocketService.get(url).then(function (resp) {
                $scope.milletrequire_list = resp.data.milletrequire_list;
            });
            var url = 'api/MarMstLeadRequire/GetLeadRequire';
            SocketService.get(url).then(function (resp) {
                $scope.leadrequire_list = resp.data.leadrequire_list;
            });
            var url = 'api/MstEnquiryRequire/GetEnquiryRequire';
            SocketService.get(url).then(function (resp) {
                $scope.enquiryrequire_list = resp.data.enquiryrequire_list;
            });
            var url = 'api/MstStartupRequire/GetStartupRequire';
            SocketService.get(url).then(function (resp) {
                $scope.startuprequire_list = resp.data.startuprequire_list;
            });
            var url = 'api/Marketing/MarketingCallMobileNoList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.ibcallmobileno_list = resp.data.MarketingCallmobileno_list;
            });
            var url = 'api/Marketing/MarketingCallEmailList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.ibcallemail_list = resp.data.MarketingCallemail_list;
            });
            var url = 'api/Marketing/MarketingCallAddressList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.ibcalladdress_list = resp.data.MarketingCalladdress_list;
            });
            var url = 'api/Marketing/MarketingCallFollowUpList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.ibcallfollowup_list = resp.data.marketingcallfollowup_list;
            });
            var url = 'api/Marketing/GetDocumentList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.lsfilename = resp.data.filename;
                $scope.lsfilepath = resp.data.filepath;
                $scope.document_list = resp.data.document_list;
            });
            var url = 'api/Marketing/GetMilletDocumentList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.lmfilename = resp.data.filename;
                $scope.lmfilepath = resp.data.filepath;
                $scope.milletdocument_list = resp.data.milletdocument_list;
            });
            var url = 'api/Marketing/GetEnquiryDocumentList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.lmfilename = resp.data.filename;
                $scope.lmfilepath = resp.data.filepath;
                $scope.enquirydocument_list = resp.data.enquirydocument_list;
            });
            var url = 'api/Marketing/EditMarketingCall';

            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                $scope.txtticket_refid = resp.data.ticket_refid;
                $scope.cboentity = resp.data.entity_gid;
                $scope.cbosourceofcontact = resp.data.marketingsourceofcontact_gid;
                $scope.cboleadrequesttype = resp.data.leadrequesttype_gid;
                $scope.cbocallreceivednumber = resp.data.marketingcallreceivednumber_gid;
                $scope.cbocustomer_type = resp.data.customer_type;
                $scope.txtcallreceived_date = resp.data.callreceived_date;
                $scope.origination = resp.data.origination;
                $scope.txtcaller_name = resp.data.caller_name;
                $scope.cbointernalreference = resp.data.internalreference_gid;
                $scope.txtcallerassociate_company = resp.data.callerassociate_company;
                $scope.txtoffice_landlineno = resp.data.office_landlineno;
                $scope.txtbase_location = resp.data.baselocation_name;
                $scope.cbocalltype = resp.data.marketingcalltype_gid;
                $scope.cbofunction = resp.data.marketingfunction_gid;
                $scope.txtfunction_remarks = resp.data.function_remarks;
                $scope.txtrequirement = resp.data.requirement;
                $scope.txtenquiry_description = resp.data.enquiry_description;
                $scope.cbocallclosure_status = resp.data.callclosure_status;
                $scope.cboassignemployee = resp.data.assignemployee_gid;
                $scope.txttat_hours = resp.data.tat_hours;
                $scope.cboleadrequirename = resp.data.leadrequire_gid;
                $scope.cbomilletrequirename = resp.data.milletrequire_gid;
                $scope.cboenquiryrequirename = resp.data.enquiryrequire_gid;
                $scope.cbostartuprequirename = resp.data.startuprequire_gid;
                $scope.txtbusiness_name = resp.data.business_name;
                $scope.emp_list = resp.data.emp_list;
                if (resp.data.tagemployee_list != null) {
                    var count = resp.data.tagemployee_list.length;
                    for (var i = 0; i < count; i++) {
                        var workerIndex = $scope.emp_list.map(function (x) { return x.employee_gid; }).indexOf(resp.data.tagemployee_list[i].employee_gid);
                        //var indexs = $scope.emp_list.findIndex(x => x.employee_gid === resp.data.tagemployee_list[i].employee_gid);
                        $scope.cbotagemployee.push($scope.emp_list[workerIndex]);
                        $scope.$parent.cbotagemployee = $scope.cbotagemployee;
                    }
                }






                $scope.txtassignclosure_remarks = resp.data.assignclosure_remarks;

               

                if (resp.data.marketingcall_status == "Incomplete") {
                    $scope.ibcallSubmit = true;
                    $scope.ibcallUpdate = false;
                }
                else {
                    $scope.ibcallSubmit = false;
                    $scope.ibcallUpdate = true;
                }

                if (resp.data.function_name == 'Others') {
                    $scope.function_show = true;
                }
                else {
                    $scope.function_show = false;
                }

                unlockUI();
            });
        

        }

        $scope.changefunctionstatus = function (Marketingfunction_name) {
            if ($('#function :selected').text() == 'Others') {
                $scope.function_show = true;
            }
            else {
                $scope.function_show = false;
            }
        }
        $scope.auditname_change = function (cboassignemployee) {
            var params = {
                employee_gid: $scope.cboassignemployee
            }
            var url = 'api/Marketing/GetBaselocation';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.employee_gid = resp.data.employee_gid;
                $scope.txtbase_location = resp.data.baselocation_name;

            });

        }
        $scope.documentviewer = function (val1, val2) {
            lockUI();
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
            if (IsValidExtension == false) {
            Notify.alert("View is not supported for this format..!", {
             status: 'danger',
             pos: 'top-center',
             timeout: 3000
             });
             unlockUI();
           return false;
             }
             DownloaddocumentService.DocumentViewer(val1, val2);
             }

             $scope.documentviewermillet = function (val1, val2) {
                lockUI();
                var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
                if (IsValidExtension == false) {
                Notify.alert("View is not supported for this format..!", {
                 status: 'danger',
                 pos: 'top-center',
                 timeout: 3000
                 });
                 unlockUI();
               return false;
                 }
                 DownloaddocumentService.DocumentViewer(val1, val2);
        }
        $scope.documentviewerenquiry = function (val1, val2) {
            lockUI();
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
            if (IsValidExtension == false) {
                Notify.alert("View is not supported for this format..!", {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                unlockUI();
                return false;
            }
            DownloaddocumentService.DocumentViewer(val1, val2);
        }
        $scope.back = function () {
            $location.url('app/MstMarketingSummary');
        }
        $scope.save = function () {

            var entity_Name = $('#entity :selected').text();
            var sourceofcontact_Name = $('#sourceofcontact :selected').text();
            var leadrequesttype = $('#leadrequesttype :selected').text();
            var callreceivednumber_Name = $('#callreceivednumber :selected').text();
            var internalreference_Name = $('#internalreference :selected').text();
            var calltype_Name = $('#calltype :selected').text();
            var function_Name = $('#function :selected').text();
            var assignemployee_Name = $('#assignemployee :selected').text();

          
            var tagemployee = $scope.cbotagemployee;
            if (tagemployee == [undefined]) {
                var cbotagemployee = null;
            }
            else {
                var cbotagemployee = $scope.cbotagemployee;
            }
            var params = {
                entity_name: entity_Name,
                entity_gid: $scope.cboentity,
                marketingsourceofcontact_name: sourceofcontact_Name,
                marketingsourceofcontact_gid: $scope.cbosourceofcontact,
                marketingcallreceivednumber_name: callreceivednumber_Name,
                marketingcallreceivednumber_gid: $scope.cbocallreceivednumber,
                leadrequesttype_name: leadrequesttype,
                leadrequesttype_gid: $scope.cboleadrequesttype,
                customer_type: $scope.cbocustomer_type,
                callreceived_date: $scope.txtcallreceived_date,
                caller_name: $scope.txtcaller_name,
                internalreference_name: internalreference_Name,
                internalreference_gid: $scope.cbointernalreference,
                callerassociate_company: $scope.txtcallerassociate_company,
                office_landlineno: $scope.txtoffice_landlineno,
                marketingcalltype_name: calltype_Name,
                marketingcalltype_gid: $scope.cbocalltype,
                marketingfunction_name: function_Name,
                marketingfunction_gid: $scope.cbofunction,
                function_remarks: $scope.txtfunction_remarks,
                requirement: $scope.txtrequirement,
                enquiry_description: $scope.txtenquiry_description,
                callclosure_status: $scope.cbocallclosure_status,
                assignemployee_name: assignemployee_Name,
                assignemployee_gid: $scope.cboassignemployee,
                tat_hours: $scope.txttat_hours,
                baselocation_name: $scope.txtbase_location,
                tagemployee_list: $scope.cbotagemployee,
                assignclosure_remarks: $scope.txtassignclosure_remarks,
                marketingcall_gid: $scope.marketingcall_gid
            }
            var url = 'api/Marketing/MarketingCallEditSave';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $location.url("app/MstMarketingSummary");
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

        $scope.submit = function () {

            var entity_Name = $('#entity :selected').text();
            var sourceofcontact_Name = $('#sourceofcontact :selected').text();
            var callreceivednumber_Name = $('#callreceivednumber :selected').text();
            var leadrequesttype = $('#leadrequesttype :selected').text();
            var internalreference_Name = $('#internalreference :selected').text();
            var calltype_Name = $('#calltype :selected').text();
            var function_Name = $('#function :selected').text();
            var assignemployee_Name = $('#assignemployee :selected').text();


            var tagemployee = $scope.cbotagemployee;
            if (tagemployee == [undefined]) {
                var cbotagemployee = null;
            }
            else {
                var cbotagemployee = $scope.cbotagemployee;
            }
            var params = {
                entity_name: entity_Name,
                entity_gid: $scope.cboentity,
                marketingsourceofcontact_name: sourceofcontact_Name,
                marketingsourceofcontact_gid: $scope.cbosourceofcontact,
                marketingcallreceivednumber_name: callreceivednumber_Name,
                marketingcallreceivednumber_gid: $scope.cbocallreceivednumber,
                leadrequesttype_name: leadrequesttype,
                leadrequesttype_gid: $scope.cboleadrequesttype,
                customer_type: $scope.cbocustomer_type,
                callreceived_date: $scope.txtcallreceived_date,
                caller_name: $scope.txtcaller_name,
                internalreference_name: internalreference_Name,
                internalreference_gid: $scope.cbointernalreference,
                callerassociate_company: $scope.txtcallerassociate_company,
                office_landlineno: $scope.txtoffice_landlineno,
                marketingcalltype_name: calltype_Name,
                marketingcalltype_gid: $scope.cbocalltype,
                marketingfunction_name: function_Name,
                marketingfunction_gid: $scope.cbofunction,
                function_remarks: $scope.txtfunction_remarks,
                requirement: $scope.txtrequirement,
                enquiry_description: $scope.txtenquiry_description,
                callclosure_status: $scope.cbocallclosure_status,
                assignemployee_name: assignemployee_Name,
                assignemployee_gid: $scope.cboassignemployee,
                tat_hours: $scope.txttat_hours,
                baselocation_name: $scope.txtbase_location,
                tagemployee_list: $scope.cbotagemployee,
                assignclosure_remarks: $scope.txtassignclosure_remarks,
                marketingcall_gid: $scope.marketingcall_gid
            }
            var url = 'api/Marketing/MarketingCallEditSubmit';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $location.url("app/MstMarketingSummary");
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

        $scope.update = function () {

            var entity_Name = $('#entity :selected').text();
            var sourceofcontact_Name = $('#sourceofcontact :selected').text();
            var callreceivednumber_Name = $('#callreceivednumber :selected').text();
            var leadrequesttypename = $('#leadrequesttype :selected').text();
            var internalreference_Name = $('#internalreference :selected').text();
            var calltype_Name = $('#calltype :selected').text();
            var function_Name = $('#function :selected').text();
            var assignemployee_Name = $('#assignemployee :selected').text();
            var leadrequire_name = $('#leadrequire :selected').text();
            var milletrequire_name = $('#milletrequire :selected').text();
            var enquiryrequire_name = $('#enquiryrequire_name :selected').text();
            var startuprequire_name = $('#startuprequire_name :selected').text();

            var tagemployee = $scope.cbotagemployee;
            if (tagemployee == [undefined]) {
                var cbotagemployee = null;
            }
            else {
                var cbotagemployee = $scope.cbotagemployee;
            }

            if (($scope.cbocallclosure_status == 'Assign') && ($scope.cboassignemployee == null || $scope.cboassignemployee == '')) {
                Notify.alert('Kindly Select Assign Employee Name', 'warning')
            }
            //else if (($scope.cbocallclosure_status == 'Assign') && ($scope.txttat_hours == null || $scope.txttat_hours == '')) {
            //    Notify.alert('Kindly Select Assign TAT Hours', 'warning')
            //}
            
            if ((($scope.cbocallclosure_status == 'Assign')||($scope.cbocallclosure_status == 'Follow Up')) && ($scope.txtassignclosure_remarks == null || $scope.txtassignclosure_remarks == '')) {
                Notify.alert('Kindly Enter Remark', 'warning')
            }
            else if (($scope.cbocallclosure_status == 'Rejected') && ($scope.txtclosure_remarks == null || $scope.txtclosure_remarks == '')) {
                Notify.alert('Kindly Enter Remark', 'warning')
            }
           
            else {
                var params = {
                    entity_name: entity_Name,
                    entity_gid: $scope.cboentity,
                    marketingsourceofcontact_name: sourceofcontact_Name,
                    marketingsourceofcontact_gid: $scope.cbosourceofcontact,
                    marketingcallreceivednumber_name: callreceivednumber_Name,
                    marketingcallreceivednumber_gid: $scope.cbocallreceivednumber,
                    leadrequesttype_name: leadrequesttypename,
                    leadrequesttype_gid: $scope.cboleadrequesttype,
                    customer_type: $scope.cbocustomer_type,
                    callreceived_date: $scope.txtcallreceived_date,
                    caller_name: $scope.txtcaller_name,
                    internalreference_name: internalreference_Name,
                    internalreference_gid: $scope.cbointernalreference,
                    callerassociate_company: $scope.txtcallerassociate_company,
                    office_landlineno: $scope.txtoffice_landlineno,
                    marketingcalltype_name: calltype_Name,
                    marketingcalltype_gid: $scope.cbocalltype,
                    marketingfunction_name: function_Name,
                    marketingfunction_gid: $scope.cbofunction,
                    function_remarks: $scope.txtfunction_remarks,
                    requirement: $scope.txtrequirement,
                    enquiry_description: $scope.txtenquiry_description,
                    callclosure_status: $scope.cbocallclosure_status,
                    assignemployee_name: assignemployee_Name,
                    assignemployee_gid: $scope.cboassignemployee,
                    tat_hours: $scope.txttat_hours,
                    baselocation_name: $scope.txtbase_location,
                    tagemployee_list: $scope.cbotagemployee,
                    assignclosure_remarks: $scope.txtassignclosure_remarks,
                    closed_remarks: $scope.txtclosure_remarks,
                    leadrequire_name: leadrequire_name,
                    leadrequire_gid: $scope.cboleadrequirename,
                    milletrequire_name: milletrequire_name,
                    milletrequire_gid: $scope.cbomilletrequirename,
                    milletrequire_name: milletrequire_name,
                    milletrequire_gid: $scope.cbomilletrequirename,
                    enquiryrequire_name: enquiryrequire_name,
                    enquiryrequire_gid: $scope.cboenquiryrequirename,
                    startuprequire_name: startuprequire_name,
                    startuprequire_gid: $scope.cbostartuprequirename,
                    business_name: $scope.txtbusiness_name,
                    industry_name: $scope.txtindustry_name,
                    marketingcall_gid: $scope.marketingcall_gid
                }
                var url = 'api/Marketing/MarketingCallEditUpdate';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $location.url("app/MstMarketingSummary");
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

        //Address Multiple Add

        $scope.address_add = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addaddress.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.geocodingFailed = false;

                var url = 'api/AddressType/GetAddressTypeASC';
                SocketService.get(url).then(function (resp) {
                    $scope.addresstype_list = resp.data.addresstype_list;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.onchangepostal_code = function () {
                    var params = {
                        postal_code: $scope.txtpostal_code
                    }
                    var url = 'api/Mstbuyer/GetPostalCodeDetails';

                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.txtcity = resp.data.city;
                        $scope.txttaluka = resp.data.taluka;
                        $scope.txtdistrict = resp.data.district;
                        $scope.txtstate = resp.data.state_name;
                    });
                   
                }

                $scope.getGeoCoding = function () {
                    if($scope.txtpostal_code == undefined || $scope.txtpostal_code.length < 6){

                        $scope.txtlatitude = '';

                        $scope.txtlongitude = '';

                    }

                    else {
                        if ($scope.txtaddressline2 == undefined) {
                            var addressString = ''.concat($scope.txtaddressline1.toString(), ",", $scope.txtpostal_code.toString());
                        } else {
                            var addressString = ''.concat($scope.txtaddressline1.toString(), ",", $scope.txtaddressline2.toString(), ",", $scope.txtpostal_code.toString());
                        }
                        var params = {
                            address: addressString
                        }
                        var url = 'api/GoogleMapsAPI/GetGeoCoding';
                        SocketService.getparams(url, params).then(function (resp) {
                            if (resp.data.status == "OK") {
                                $scope.txtlatitude = resp.data.results[0].geometry.location.lat.toString();
                                $scope.txtlongitude = resp.data.results[0].geometry.location.lng.toString();
                                $scope.geocodingFailed = false;
                            }
                            else if (resp.data.status == "ZERO_RESULTS") {
                                $scope.geocodingFailed = true;
                            }
                        });
                    }
                }

                $scope.txtcountry = "India";
                $scope.addressSubmit = function () {

                    var params = {
                        addresstype_gid: $scope.cboaddresstype.address_gid,
                        addresstype_name: $scope.cboaddresstype.address_type,
                        primary_status: $scope.rdbprimaryaddress,
                        addressline1: $scope.txtaddressline1,
                        addressline2: $scope.txtaddressline2,
                        postal_code: $scope.txtpostal_code,
                        landmark: $scope.txtLand_Mark,
                        taluka: $scope.txttaluka,
                        city: $scope.txtcity,
                        state: $scope.txtstate,
                        district: $scope.txtdistrict,
                        country: $scope.txtcountry,
                        latitude: $scope.txtlatitude,
                        longitude: $scope.txtlongitude
                    }
                    var url = 'api/Marketing/PostMarketingCallAddress';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            address_list();
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
        function address_list() {
            var url = 'api/Marketing/GetMarketingCallAddressList';
            SocketService.get(url).then(function (resp) {
                $scope.ibcalladdress_list = resp.data.MarketingCalladdress_list;
            });
        }
        $scope.address_delete = function (marketingcall2address_gid) {
            var params =
                {
                    marketingcall2address_gid: marketingcall2address_gid
                }
            var url = 'api/Marketing/MarketingCallAddressDelete';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    address_templist();
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
        

        function address_templist() {
            var param = {
                marketingcall_gid: $scope.marketingcall_gid
            };
            var url = 'api/Marketing/MarketingCallAddressTempList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.ibcalladdress_list = resp.data.MarketingCalladdress_list;
            });
        }

        $scope.address_edit = function (marketingcall2address_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editinboundcalladdress.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var url = 'api/AddressType/GetAddressTypeASC';
                SocketService.get(url).then(function (resp) {
                    $scope.addresstype_list = resp.data.addresstype_list;
                });

                var params = {
                    inboundcall2address_gid: inboundcall2address_gid
                }
                var url = 'api/Marketing/EditMarketingCallAddress';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.cboAddressType = resp.data.addresstype_gid;

                    $scope.rdbprimary_status = resp.data.primary_status;
                    $scope.txtaddressline1 = resp.data.addressline1;
                    $scope.txtaddressline2 = resp.data.addressline2;
                    $scope.txtlandmark = resp.data.landmark;
                    $scope.txtpostal_code = resp.data.postal_code;
                    $scope.txtcity = resp.data.city;
                    $scope.txttaluka = resp.data.taluka;
                    $scope.txtdistrict = resp.data.district;
                    $scope.txtstate = resp.data.state;
                    $scope.txtcountry = resp.data.country;
                    $scope.txtlatitude = resp.data.latitude;
                    $scope.txtlongitude = resp.data.longitude;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.txtcountry = "India";
                $scope.address_update = function () {
                    var address_type = $('#address_type :selected').text();

                    var params = {
                        addresstype_gid: $scope.cboAddressType,
                        addresstype_name: address_type,
                        primary_status: $scope.rdbprimary_status,
                        addressline1: $scope.txtaddressline1,
                        addressline2: $scope.txtaddressline2,
                        postal_code: $scope.txtpostal_code,
                        taluka: $scope.txttaluka,
                        city: $scope.txtcity,
                        district: $scope.txtdistrict,
                        state: $scope.txtstate,
                        country: $scope.txtcountry,
                        latitude: $scope.txtlatitude,
                        longitude: $scope.txtlongitude,
                        inboundcall2address_gid: inboundcall2address_gid,
                        marketingcall_gid: $scope.marketingcall_gid,
                    }
                    var url = 'api/Marketing/UpdateMarketingCallAddress';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            address_templist();
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
        $scope.StaticMapAndPhotos_View = function (latitude, longitude, addressline1, addressline2, postal_code) {
            var modalInstance = $modal.open({
                templateUrl: '/StaticMapAndPhotosView.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    latitude: latitude,
                    longitude: longitude
                }
                var url = 'api/GoogleMapsAPI/GetStaticMapUrl';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.staticmapImgUrl = resp.data;
                });
                if (addressline2 == '') {
                    var addressString = ''.concat(addressline1.toString(), ",", postal_code.toString());
                } else {
                    var addressString = ''.concat(addressline1.toString(), ",", addressline2.toString(), ",", postal_code.toString());
                }
                var params = {
                    address: addressString
                }
                var url = 'api/GoogleMapsAPI/GetPlaceImage';
                SocketService.getparams(url, params).then(function (resp) {
                    var photoUrlArray = [];
                    for (var i = 0; i < resp.data.length; i++) {
                        if (resp.data[i] != null) {
                            photoUrlArray[i] = resp.data[i];
                        }
                    }
                    if (photoUrlArray.length == 0) {
                        $scope.photoNotFound = true;
                    } else {
                        $scope.photoUrlList = photoUrlArray;
                        $scope.photoFound = true;
                    }
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        //Mobile Number Multiple Add
        $scope.add_mobileno = function () {
            if (($scope.txtmobile_no == undefined) || ($scope.txtmobile_no == '') || ($scope.rdbprimary_status == undefined) || ($scope.rdbwhatsapp_status == undefined) || ($scope.rdbsms_to == undefined) || ($scope.rdbprimary_status == '') || ($scope.rdbwhatsapp_status == '') || ($scope.rdbsms_to == '')) {
                Notify.alert('Enter Mobile Number / Select Primary Status', 'warning');
            }
            else if ($scope.txtmobile_no.length < 10) {
                Notify.alert('Enter 10 Digit Mobile Number', 'warning');
            }
            else {
                var params = {
                    mobile_no: $scope.txtmobile_no,
                    primary_status: $scope.rdbprimary_status,
                    whatsapp_status: $scope.rdbwhatsapp_status,
                    sms_to: $scope.rdbsms_to,
                    marketingcall_gid: $scope.marketingcall_gid,
                }
                var url = 'api/Marketing/PostMarketingCallMobileNo';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        mobileno_templist();
                        $scope.txtmobile_no = '';
                        $scope.rdbprimary_status = '';
                        $scope.rdbprimary_status == false;
                        $scope.rdbsms_to = '';
                        $scope.rdbsms_to == false;
                        $scope.rdbwhatsapp_status = '';
                        $scope.rdbwhatsapp_status == false;
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
        $scope.delete_mobileno = function (marketingcall2mobileno_gid) {
            var params = {
                marketingcall2mobileno_gid: marketingcall2mobileno_gid
            }
            var url = 'api/Marketing/MarketingCallMobileNoDelete';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                mobileno_templist();
            });
        }
        function mobileno_templist() {
            var param = {
                marketingcall_gid: $scope.marketingcall_gid
            };
            var url = 'api/Marketing/MarketingCallMobileNoTempList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.ibcallmobileno_list = resp.data.MarketingCallmobileno_list;
            });            
        }
        $scope.edit_mobileno = function (marketingcall2mobileno_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editinboundcallmobileno.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    marketingcall2mobileno_gid: marketingcall2mobileno_gid
                }
                var url = 'api/Marketing/EditMarketingCallMobileNo';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditmobile_no = resp.data.mobile_no;
                    $scope.rdbeditprimary_status = resp.data.primary_status;
                    $scope.rdbeditwhatsapp_status = resp.data.whatsapp_status;
                    $scope.rdbeditsms_to = resp.data.sms_to;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update_mobileno = function () {

                    var params = {
                        mobile_no: $scope.txteditmobile_no,
                        primary_status: $scope.rdbeditprimary_status,
                        whatsapp_status: $scope.rdbeditwhatsapp_status,
                        sms_to: $scope.rdbeditsms_to,
                        marketingcall2mobileno_gid: marketingcall2mobileno_gid,
                        marketingcall_gid: $scope.marketingcall_gid,
                    }
                    var url = 'api/Marketing/UpdateMarketingCallMobileNo';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        mobileno_templist();
                    });

                    $modalInstance.close('closed');

                }
            }
        }
        //Email Address Multiple Add
        $scope.add_emailaddress = function () {
            if (($scope.txtemail_address == undefined) || ($scope.txtemail_address == '') || ($scope.rdbprimary_email == undefined) || ($scope.rdbprimary_email == '')) {
                Notify.alert('Enter Email Address/Select Status', 'warning');
            }
            else {
                var params = {
                    email_address: $scope.txtemail_address,
                    primary_status: $scope.rdbprimary_email,
                    marketingcall_gid: $scope.marketingcall_gid,
                }
                var url = 'api/Marketing/PostMarketingCallEmail';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        email_templist();
                        $scope.txtemail_address = '';
                        $scope.rdbprimary_email = '';
                        $scope.rdbprimary_email == false;
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
        $scope.delete_emailaddress = function (marketingcall2email_gid) {
            var params = {
                marketingcall2email_gid: marketingcall2email_gid
            }
            var url = 'api/Marketing/MarketingCallEmailDelete';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                email_templist();
            });
        }
        

        function email_templist() {
            var param = {
                marketingcall_gid: $scope.marketingcall_gid
            };
            var url = 'api/Marketing/MarketingCallEmailTempList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.ibcallemail_list = resp.data.MarketingCallemail_list;
            });
        }

        $scope.edit_emailaddress = function (marketingcall2email_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editinboundcallemail.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    marketingcall2email_gid: marketingcall2email_gid
                }
                var url = 'api/Marketing/EditMarketingCallEmail';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txteditemail_address = resp.data.email_address;
                    $scope.rdbeditprimary_status = resp.data.primary_status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update_emailaddress = function () {

                    var params = {
                        email_address: $scope.txteditemail_address,
                        primary_status: $scope.rdbeditprimary_status,
                        inboundcall2email_gid: inboundcall2email_gid,
                        marketingcall_gid: $scope.marketingcall_gid,
                    }
                    var url = 'api/Marketing/UpdateMarketingCallEmail';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        email_templist();
                    });

                    $modalInstance.close('closed');

                }
            }
        }

        //Follow Up Multiple Add
        $scope.add_followup = function () {
            if (($scope.txtfollowup_date == undefined) || ($scope.txtfollowup_date == '') || ($scope.txtfollowup_time == undefined) || ($scope.txtfollowup_time == '')) {
                Notify.alert('Enter Follow Up Date/Follow Up Time', 'warning');
            }
            else {
                var params = {
                    followup_date: $scope.txtfollowup_date,
                    followup_time: $scope.txtfollowup_time,
                }
                var url = 'api/Marketing/PostMarketingCallFollowUp';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    $scope.txtfollowup_date = '';
                    followup_templist();
                    $scope.txtfollowup_date = '';
                    $scope.txtfollowup_time = '';
                });
            }
        }
        $scope.delete_followup = function (marketingcall2followup_gid) {
            var params = {
                marketingcall2followup_gid: marketingcall2followup_gid
            }
            var url = 'api/Marketing/MarketingCallFollowUpDelete';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                followup_templist();
            });
        }       

        function followup_templist() {
            var param = {
                marketingcall_gid: $scope.marketingcall_gid
            };
            var url = 'api/Marketing/MarketingCallFollowUpTempList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.ibcallfollowup_list = resp.data.MarketingCallfollowup_list;
            });
        }

        $scope.edit_followup = function (marketingcall2followup_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editinboundcallfollowup.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {                

                var params = {
                    marketingcall2followup_gid: marketingcall2followup_gid
                }
                var url = 'api/Marketing/EditMarketingCallFollowUp';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txteditfollowup_date = new Date(resp.data.followup_date);

                    if (resp.data.Tfollowup_time == '0001-01-01T00:00:00') {
                        $scope.txteditfollowup_time = '';
                    }
                    else {
                        $scope.txteditfollowup_time = new Date(resp.data.Tfollowup_time);
                    }

                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update_followup = function () {

                    var params = {
                        followup_date: $scope.txteditfollowup_date,
                        followup_time: $scope.txteditfollowup_time,
                        marketingcall2followup_gid: marketingcall2followup_gid,
                        marketingcall_gid: $scope.marketingcall_gid,
                    }
                    var url = 'api/Marketing/UpdateMarketingCallFollowUp';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        followup_templist();
                    });

                    $modalInstance.close('closed');

                }
            }
        }
        $scope.download_all = function (val1, val2) {

            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }

        }

        $scope.document_downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.download_allmillet = function (val1,val2) {
            for (var i = 0; i < val2.length; i++) {
               //  console.log(array[i]);
               DownloaddocumentService.Downloaddocument(val1, val2[i]);
           }
       }        
       $scope.milletdocument_downloads = function (val1,val2) {
           DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.download_allenquiry = function (val1, val2) {
            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }
        }
        $scope.enquirydocument_downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
    }
})();
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstMarketingFollowupCallController', MstMarketingFollowupCallController);

    MstMarketingFollowupCallController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'cmnfunctionService'];

    function MstMarketingFollowupCallController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstMarketingFollowupCallController';
        activate();
        lockUI();
        function activate() {
            var url = 'api/Marketing/GetFollowUpMarketingCallSummary';
            SocketService.get(url).then(function (resp) {
                $scope.ibcall_list = resp.data.MarketingCall_list;
                unlockUI();
            });



            var url = "api/Marketing/MarketingCallCount";
            SocketService.get(url).then(function (resp) {
                $scope.unassignedcall_count = resp.data.unassignedcall_count;
                $scope.assignedcall_count = resp.data.assignedcall_count;
                $scope.completedcall_count = resp.data.completedcall_count;
                $scope.followupcall_count = resp.data.followupcall_count;
                $scope.closedcall_count = resp.data.closedcall_count;
                $scope.rejectedcall_count = resp.data.rejectedcall_count;
                unlockUI();
            });
        }
        $scope.addinbound = function () {
            $location.url("app/MstMarketingAdd");
        }
        $scope.closedcall = function () {
            $location.url("app/MstMarketingClosedCall");
        }
        $scope.followupcall = function () {
            $location.url("app/MstMarketingFollowupCall");
        }
        $scope.assignedcall = function () {
            $location.url("app/MstMarketingSummary");
        }
        $scope.unassignedcall= function() {
            $location.url("app/MstMarketingUnassignedLeadSummary");
        }
        $scope.completedcall = function () {
            $location.url("app/MstMarketingCompletedCall");
        }
        $scope.rejectedcall = function () {
            $location.url("app/MstMarketingRejectedCallSummary");
        }

        $scope.view = function (marketingcall_gid) {
            $location.url('app/MstMarketingFollowupCallView?hash=' + cmnfunctionService.encryptURL('marketingcall_gid=' + marketingcall_gid));
        }

        $scope.transfer = function (marketingcall_gid) {

            var modalInstance = $modal.open({
                templateUrl: '/transferContent.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];
            function ModalInstanceCtrl($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {


                var params = {
                    marketingcall_gid: marketingcall_gid
                }
                var url = 'api/Marketing/MarketingCallDetailsForTransfer';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.marketingcall_gid = resp.data.marketingcall_gid;
                    $scope.ticket_refid = resp.data.ticket_refid;
                    $scope.assignemployee_gid = resp.data.assignemployee_gid;
                    $scope.assignemployee_name = resp.data.assignemployee_name;
                    $scope.ibcalltransfer_list = resp.data.MarketingCalltransfer_list;
                });

                var url = 'api/OsdTrnCustomerQueryMgmt/TransferEmployee';
                SocketService.get(url).then(function (resp) {
                    $scope.TransferEmployeeList = resp.data.TransferEmployeeList;
                    unlockUI();
                });

                /*      $scope.marketingcall_gid = marketingcall_gid;
                      $scope.ticketref_no = ticketref_no;
                      $scope.assigned_to = assigned_to; */

                $scope.transfer_call = function () {

                    if ($scope.cboTransferTo == undefined) {
                        modalInstance.close('closed');
                        Notify.alert('Kindly Select the Transfer to Person', 'warning');
                        return;
                    }

                    var params = {
                        marketingcall_gid: $scope.marketingcall_gid,
                        ticket_refid: $scope.ticket_refid,
                        transferfrom_gid: $scope.assignemployee_gid,
                        transferfrom_name: $scope.assignemployee_name,
                        transferto_gid: $scope.cboTransferTo,
                        transferto_name: $('#transfer_to :selected').text(),
                        transfer_remarks: $scope.transfer_remarks
                    }

                    var url = "api/Marketing/MarketingCallTransferEmployee";
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status = true) {
                            modalInstance.close('closed');
                            Notify.alert(resp.data.message, 'success');
                            activate();
                        }
                        else {
                            modalInstance.close('closed');
                            Notify.alert(resp.data.message, 'warning');
                            activate();
                        }
                    });


                }
                /*     var url = 'api/MstTelecalling/TransferLog';
                     var params = {
                         marketingcall_gid:marketingcall_gid
                     }
                     SocketService.getparams(url, params).then(function (resp) {
                             $scope.TransferLog = resp.data.TransferLog;
                     });  */
                $scope.close = function () {
                    modalInstance.close('closed');
                };
            }
        }
    }
})();
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstMarketingFollowupCallsController', MstMarketingFollowupCallsController);

    MstMarketingFollowupCallsController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function MstMarketingFollowupCallsController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstMarketingFollowupCallsController';
        activate();
        lockUI();
        function activate() {
             /*var url = 'api/MstTelecalling/GetAssignedSummary';
            SocketService.get(url).then(function (resp) {
                $scope.followup_list = resp.data.followup_list;
                unlockUI();
            }); */

            var url = "api/Marketing/MarketingCallCount";
            SocketService.get(url).then(function (resp) {
                $scope.assignedcall_count = resp.data.assignedcall_count;
                $scope.completedcall_count = resp.data.completedcall_count;
                $scope.followupcall_count = resp.data.followupcall_count;
                $scope.closedcall_count = resp.data.closedcall_count;
                unlockUI();
            });
        }
        $scope.addinbound = function () {
            $location.url("app/MstMarketingAdd");
        }
        $scope.closedcall = function () {
            $location.url("app/MstMarketingClosedCall");
        }
        $scope.followupcall = function () {
            $location.url("app/MstMarketingFollowupCall");
        }
        $scope.assignedcall = function () {
            $location.url("app/MstMarketingSummary");
        }
        $scope.completedcall = function () {
            $location.url("app/MstMarketingCompletedCall");
        }

        $scope.view = function (marketingcall_gid) {
            $location.url('app/MstMarketingFollowupCallView?marketingcall_gid=' + marketingcall_gid);
        }

        $scope.transfer = function (marketingcall_gid) {

            var modalInstance = $modal.open({
                templateUrl: '/transferContent.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];
            function ModalInstanceCtrl($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {


                var params = {
                    marketingcall_gid: marketingcall_gid
                }
                var url = 'api/Marketing/MarketingCallDetailsForTransfer';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.marketingcall_gid = resp.data.marketingcall_gid;
                    $scope.ticket_refid = resp.data.ticket_refid;
                    $scope.assignemployee_gid = resp.data.assignemployee_gid;
                    $scope.assignemployee_name = resp.data.assignemployee_name;
                    $scope.ibcalltransfer_list = resp.data.ibcalltransfer_list;
                });

                var url = 'api/OsdTrnCustomerQueryMgmt/TransferEmployee';
                SocketService.get(url).then(function (resp) {
                    $scope.TransferEmployeeList = resp.data.TransferEmployeeList;
                    unlockUI();
                });

                /*      $scope.marketingcall_gid = marketingcall_gid;
                      $scope.ticketref_no = ticketref_no;
                      $scope.assigned_to = assigned_to; */

                $scope.transfer_call = function () {

                    if ($scope.cboTransferTo == undefined) {
                        modalInstance.close('closed');
                        Notify.alert('Kindly Select the Transfer to Person', 'warning');
                        return;
                    }

                    var params = {
                        marketingcall_gid: $scope.marketingcall_gid,
                        ticket_refid: $scope.ticket_refid,
                        transferfrom_gid: $scope.assignemployee_gid,
                        transferfrom_name: $scope.assignemployee_name,
                        transferto_gid: $scope.cboTransferTo,
                        transferto_name: $('#transfer_to :selected').text(),
                        transfer_remarks: $scope.transfer_remarks
                    }

                    var url = "api/Marketing/MarketingCallTransferEmployee";
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status = true) {
                            modalInstance.close('closed');
                            Notify.alert(resp.data.message, 'success');
                            activate();
                        }
                        else {
                            modalInstance.close('closed');
                            Notify.alert(resp.data.message, 'warning');
                            activate();
                        }
                    });


                }
                /*     var url = 'api/MstTelecalling/TransferLog';
                     var params = {
                         marketingcall_gid:marketingcall_gid
                     }
                     SocketService.getparams(url, params).then(function (resp) {
                             $scope.TransferLog = resp.data.TransferLog;
                     });  */
                $scope.close = function () {
                    modalInstance.close('closed');
                };
            }
        }
    }
})();
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstMarketingFollowUpCallSummaryController', MstMarketingFollowUpCallSummaryController);

    MstMarketingFollowUpCallSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'cmnfunctionService'];

    function MstMarketingFollowUpCallSummaryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstMarketingFollowUpCallSummaryController';

        activate();
        lockUI();
        function activate() {
            var url = 'api/Marketing/GetEmpFollowUpMarketingCallSummary';
            SocketService.get(url).then(function (resp) {
                $scope.ibcallfollowup_list = resp.data.MarketingCall_list;
                unlockUI();
            });
            var url = "api/Marketing/EmployeeMarketingCallCount";
            SocketService.get(url).then(function (resp) {
                $scope.assignedcall_count = resp.data.assignedcall_count;
                $scope.transfercall_count = resp.data.transfercall_count;
                $scope.completedcall_count = resp.data.completedcall_count;
                $scope.inprogresscall_count = resp.data.inprogresscall_count;
                $scope.tagmember_count = resp.data.taggedcall_count;
                $scope.followupcall_count = resp.data.followupcall_count;
                $scope.rejectedcall_count = resp.data.rejectedcall_count;
                $scope.closedcall_count = resp.data.closedcall_count;


                unlockUI();
            });

        }
        $scope.closedcall= function() {
            $location.url("app/MstMarketingMyleadsClosedCall");
        }
        $scope.myassigned_calls = function () {
            $location.url("app/MstMarketingMyAssignedCallSummary");
        }
        $scope.tag_member = function () {
            $location.url("app/MstMarketingTaggedMemberSummary");
        }
        $scope.transfer_calls = function () {
            $location.url("app/MstMarketingTransferCallSummary");
        }
        $scope.completed_calls = function () {
            $location.url("app/MstMarketingCompletedCallSummary");
        }
        $scope.call_response = function (marketingcall_gid) {
            $location.url("app/MstMarketingCallResponse?hash=" + cmnfunctionService.encryptURL("marketingcall_gid=" + marketingcall_gid));
        }
        $scope.view = function (marketingcall_gid) {
            $location.url("app/MstMarketingAssignedFollowupLeadsView?hash=" + cmnfunctionService.encryptURL("marketingcall_gid=" + marketingcall_gid + '&lspage=MyFollowUpLead'));
        }
        $scope.edit = function (marketingcall_gid) {
            $location.url("app/MstMarketingEdit?hash=" + cmnfunctionService.encryptURL("marketingcall_gid=" + marketingcall_gid));
        }
        $scope.work_inprogress = function () {
            $location.url("app/MstMarketingWorkInprogressCallSummary");
        }
        $scope.followup_call = function () {
            $location.url("app/MstMarketingFollowUpCallSummary");
        }
        $scope.mbdrejected_calls = function () {
            $location.url("app/MstMarketingMBDRejectedCallSummary");
        }
        $scope.transfer = function (marketingcall_gid) {

            var modalInstance = $modal.open({
                templateUrl: '/transferContent.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];
            function ModalInstanceCtrl($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {


                var params = {
                    marketingcall_gid: marketingcall_gid
                }
                var url = 'api/Marketing/MarketingDetailsForTransfer';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.marketingcall_gid = resp.data.marketingcall_gid;
                    $scope.ticket_refid = resp.data.ticket_refid;
                    $scope.assignemployee_gid = resp.data.assignemployee_gid;
                    $scope.assignemployee_name = resp.data.assignemployee_name;
                    $scope.ibcalltransfer_list = resp.data.ibcalltransfer_list;
                });

                var url = 'api/OsdTrnCustomerQueryMgmt/TransferEmployee';
                SocketService.get(url).then(function (resp) {
                    $scope.TransferEmployeeList = resp.data.TransferEmployeeList;
                    unlockUI();
                });

                /*      $scope.inboundcall_gid = inboundcall_gid;
                      $scope.ticketref_no = ticketref_no;
                      $scope.assigned_to = assigned_to; */

                $scope.transfer_call = function () {

                    if ($scope.cboTransferTo == undefined) {
                        modalInstance.close('closed');
                        Notify.alert('Kindly Select the Transfer to Person', 'warning');
                        return;
                    }

                    var params = {
                        marketingcall_gid: $scope.marketingcall_gid,
                        ticket_refid: $scope.ticket_refid,
                        transferfrom_gid: $scope.assignemployee_gid,
                        transferfrom_name: $scope.assignemployee_name,
                        transferto_gid: $scope.cboTransferTo,
                        transferto_name: $('#transfer_to :selected').text(),
                        transfer_remarks: $scope.transfer_remarks
                    }

                    var url = "api/Marketing/MarketingCallTransferEmployee";
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status = true) {
                            modalInstance.close('closed');
                            Notify.alert(resp.data.message, 'success');
                            activate();
                        }
                        else {
                            modalInstance.close('closed');
                            Notify.alert(resp.data.message, 'warning');
                            activate();
                        }
                    });


                }
                /*     var url = 'api/MstTelecalling/TransferLog';
                     var params = {
                         inboundcall_gid:inboundcall_gid
                     }
                     SocketService.getparams(url, params).then(function (resp) {
                             $scope.TransferLog = resp.data.TransferLog;
                     });  */
                $scope.close = function () {
                    modalInstance.close('closed');
                };
            }
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstMarketingFollowupCallViewController', MstMarketingFollowupCallViewController);

    MstMarketingFollowupCallViewController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams','DownloaddocumentService','cmnfunctionService'];

    function MstMarketingFollowupCallViewController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstMarketingFollowupCallViewController';
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        var marketingcall_gid = searchObject.marketingcall_gid;
        lockUI();
        activate();
        function activate() {
            var params = {
                marketingcall_gid: marketingcall_gid
            }

            var url = 'api/Marketing/GetMarketingCallAssignedView';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtticket_refid = resp.data.ticket_refid,
                    $scope.txtentity_name = resp.data.entity_name,
                    $scope.txtsourceofcontact_name = resp.data.sourceofcontact_name,
                    $scope.txtcallreceivednumber_name = resp.data.callreceivednumber_name,
                    $scope.txtcustomer_type = resp.data.leadrequesttype_name,
                    $scope.txtcallreceived_date = resp.data.callreceived_date,
                    $scope.txtcaller_name = resp.data.caller_name,
                    $scope.txtinternalreference_name = resp.data.internalreference_name,
                    $scope.txtcallerassociate_company = resp.data.callerassociate_company,
                    $scope.txtoffice_landlineno = resp.data.office_landlineno,
                    $scope.txtcalltype_name = resp.data.calltype_name,
                    $scope.txtfunction_name = resp.data.function_name,
                    $scope.txtfunction_remarks = resp.data.function_remarks,
                    $scope.txttat_hours = resp.data.tat_hours,
                    $scope.txtrequirement = resp.data.requirement,
                    $scope.txtenquiry_description = resp.data.enquiry_description,
                    $scope.txtcallclosure_status = resp.data.callclosure_status,
                    $scope.txtassignemployee_name = resp.data.assignemployee_name,
                    $scope.txttagemployee_name = resp.data.tagemployee_name,
                    $scope.txtassignclosure_remarks = resp.data.assignclosure_remarks,
                    $scope.ibcalladdress_list = resp.data.MarketingCalladdress_list;

                $scope.txtprimary_mobileno = resp.data.primary_mobileno,
                    $scope.ibcallmobileno_list = resp.data.MarketingCallmobileno_list;

                $scope.txtprimary_email = resp.data.primary_email,
                    $scope.ibcallemail_list = resp.data.MarketingCallemail_list;
                $scope.extendcallfollowup_list = resp.data.marketingcallextendfollowup_list;
                $scope.ibcallfollowup_list = resp.data.MarketingCallfollowup_list;
                $scope.ibcalltransfer_list = resp.data.MarketingCalltransfer_list,
                    $scope.ibcalltaggedmember_list = resp.data.MarketingCalltaggedmember_list;

                $scope.txtfollowup_date = resp.data.followup_date,
                    $scope.txtfollowup_by = resp.data.followup_by,
                    $scope.txtfollowup_remarks = resp.data.followup_remarks,
                    $scope.txtbase_location = resp.data.baselocation_name,
                    $scope.origination = resp.data.origination,
                    $scope.txtleadrequire_name = resp.data.leadrequire_name,
                    $scope.txtmilletrequire_name = resp.data.milletrequire_name,
                    $scope.txtenquiryrequire_name = resp.data.enquiryrequire_name,
                    $scope.txtstartuprequire_name = resp.data.startuprequire_name,
                    $scope.txtbusiness_name = resp.data.business_name,
                    $scope.txtindustry_name = resp.data.industry_name,

                    unlockUI();
            });
            var url = 'api/Marketing/GetDocumentList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.lsfilename = resp.data.filename;
                $scope.lsfilepath = resp.data.filepath;
                $scope.document_list = resp.data.document_list;
            });
            var url = 'api/Marketing/GetMilletDocumentList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.lmfilename = resp.data.filename;
                $scope.lmfilepath = resp.data.filepath;
                $scope.milletdocument_list = resp.data.milletdocument_list;
            });
            var url = 'api/Marketing/GetEnquiryDocumentList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.lmfilename = resp.data.filename;
                $scope.lmfilepath = resp.data.filepath;
                $scope.enquirydocument_list = resp.data.enquirydocument_list;
            });
            var url = 'api/Marketing/MarketingCallProofDocumentList';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.lsfilename1 = resp.data.filename;
                $scope.lsfilepath1 = resp.data.filepath;
                $scope.Uploadcallproofdocument_list = resp.data.callproofupload_list;
                unlockUI();
            });
            var url = 'api/Marketing/MarketingCallRecordingDocumentList';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.Uploadcallrecordingdocument_list = resp.data.callproofupload_list;
                unlockUI();
            });
        }
        $scope.download_all = function (val1, val2) {

            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }

        }
        $scope.document_downloads = function (val1,val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.download_allmillet = function (val1, val2) {
            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }
        }
        $scope.downloads_all = function (val1, val2) {

            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }

        }
        $scope.milletdocument_downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.recproof_downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.documentviewer = function (val1, val2) {
            lockUI();
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
            if (IsValidExtension == false) {
            Notify.alert("View is not supported for this format..!", {
             status: 'danger',
             pos: 'top-center',
             timeout: 3000
             });
             unlockUI();
           return false;
             }
             DownloaddocumentService.DocumentViewer(val1, val2);
             }

             $scope.documentviewermillet = function (val1, val2) {
                lockUI();
                var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
                if (IsValidExtension == false) {
                Notify.alert("View is not supported for this format..!", {
                 status: 'danger',
                 pos: 'top-center',
                 timeout: 3000
                 });
                 unlockUI();
               return false;
                 }
                 DownloaddocumentService.DocumentViewer(val1, val2);
                 }
                 $scope.documentviewerupload = function (val1, val2) {
                    lockUI();
                    var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
                    if (IsValidExtension == false) {
                    Notify.alert("View is not supported for this format..!", {
                     status: 'danger',
                     pos: 'top-center',
                     timeout: 3000
                     });
                     unlockUI();
                   return false;
                     }
                     DownloaddocumentService.DocumentViewer(val1, val2);
                     }
                       
        $scope.Back = function () {
            $location.url('app/MstMarketingFollowupCall');
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstMarketingManageRejectedCallViewController', MstMarketingManageRejectedCallViewController);

    MstMarketingManageRejectedCallViewController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function MstMarketingManageRejectedCallViewController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstMarketingManageRejectedCallViewController';
        $scope.marketingcall_gid = $location.search().marketingcall_gid;
        var marketingcall_gid = $scope.marketingcall_gid;
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;

        activate();
        lockUI();
        function activate() {
            var params = {
                marketingcall_gid: marketingcall_gid
            }

            var url = 'api/Marketing/GetMarketingCallAssignedView';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtticket_refid = resp.data.ticket_refid,
                $scope.txtentity_name = resp.data.entity_name,
                $scope.txtsourceofcontact_name = resp.data.sourceofcontact_name,
                $scope.txtcallreceivednumber_name = resp.data.callreceivednumber_name,
                $scope.txtcustomer_type = resp.data.leadrequesttype_name,
                $scope.txtcallreceived_date = resp.data.callreceived_date,
                $scope.txtcaller_name = resp.data.caller_name,
                $scope.txtinternalreference_name = resp.data.internalreference_name,
                $scope.txtcallerassociate_company = resp.data.callerassociate_company,
                $scope.txtoffice_landlineno = resp.data.office_landlineno,
                $scope.txtcalltype_name = resp.data.calltype_name,
                $scope.txtfunction_name = resp.data.function_name,
                $scope.txtfunction_remarks = resp.data.function_remarks,
                  $scope.txttat_hours = resp.data.tat_hours,
                $scope.txtrequirement = resp.data.requirement,
                $scope.txtenquiry_description = resp.data.enquiry_description,
                $scope.txtcallclosure_status = resp.data.callclosure_status,
                $scope.txtassignemployee_name = resp.data.assignemployee_name,
                $scope.txtassign_date = resp.data.assign_date,
                $scope.txttagemployee_name = resp.data.tagemployee_name,
                $scope.txtassignclosure_remarks = resp.data.assignclosure_remarks,
                $scope.ibcalladdress_list = resp.data.MarketingCalladdress_list;
                $scope.ibcallfollowup_list = resp.data.MarketingCallfollowup_list;
                $scope.ibcalltransfer_list = resp.data.MarketingCalltransfer_list,
                $scope.ibcalltaggedmember_list = resp.data.MarketingCalltaggedmember_list;
                $scope.txtcompleted_by = resp.data.completed_by,
                 $scope.txtcompleted_date = resp.data.completed_date,
                $scope.txtcompleted_remarks = resp.data.completed_remarks,
               $scope.txtfollowup_date = resp.data.followup_date,
                $scope.txtfollowup_time = resp.data.followup_time,
                $scope.txtfollowup_by = resp.data.followup_by,
                $scope.txtfollowup_remarks = resp.data.followup_remarks,
                 $scope.txtrejected_date = resp.data.rejected_date,
                $scope.txtrejected_by = resp.data.rejected_by,
                 $scope.txtbase_location = resp.data.baselocation_name,
                $scope.txtrejected_remarks = resp.data.rejected_remarks,
                unlockUI();
            });
        }
        $scope.Back = function () {
            $location.url('app/MstRejectedMarketingSummary');
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstMarketingMBDRejectedCallSummaryController', MstMarketingMBDRejectedCallSummaryController);

    MstMarketingMBDRejectedCallSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function MstMarketingMBDRejectedCallSummaryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstMarketingMBDRejectedCallSummaryController';
        activate();
        lockUI();
        function activate() {
            var url = 'api/Marketing/GetEmpRejectedMarketingCallSummary';
            SocketService.get(url).then(function (resp) {
                $scope.marketingcallcompleted_list = resp.data.MarketingCall_list;
                unlockUI();
            });

            var url = "api/Marketing/EmployeeMarketingCallCount";
            SocketService.get(url).then(function (resp) {
                $scope.assignedcall_count = resp.data.assignedcall_count;
                $scope.transfercall_count = resp.data.transfercall_count;
                $scope.completedcall_count = resp.data.completedcall_count;
                $scope.rejectedcall_count = resp.data.rejectedcall_count
                $scope.inprogresscall_count = resp.data.inprogresscall_count;
                $scope.tagmember_count = resp.data.taggedcall_count;
                $scope.followupcall_count = resp.data.followupcall_count;
                $scope.closedcall_count = resp.data.closedcall_count;
                unlockUI();
            });
        }
        $scope.myassigned_calls = function () {
            $location.url("app/MstMarketingMyAssignedCallSummary");
        }
        $scope.closedcall= function() {
            $location.url("app/MstMarketingMyleadsClosedCall");
        }
        $scope.tag_member = function () {
            $location.url("app/MstMarketingTaggedMemberSummary");
        }
        $scope.transfer_calls = function () {
            $location.url("app/MstMarketingTransferCallSummary");
        }
        $scope.completed_calls = function () {
            $location.url("app/MstMarketingCompletedCallSummary");
        }
        $scope.view = function (marketingcall_gid) {
            $location.url("app/MstMarketingMBDRejectedCallView?marketingcall_gid=" + marketingcall_gid);
        }
        $scope.edit = function (marketingcall_gid) {
            $location.url("app/MstMarketingEdit?marketingcall_gid=" + marketingcall_gid);
        }
        $scope.work_inprogress = function () {
            $location.url("app/MstMarketingWorkInprogressCallSummary");
        }
        $scope.followup_call = function () {
            $location.url("app/MstMarketingFollowUpCallSummary");
        }
        $scope.mbdrejected_calls = function () {
            $location.url("app/MstMarketingMBDRejectedCallSummary");
        }
        $scope.transfer = function (marketingcall_gid, ticketref_no, assigned_to) {

            var modalInstance = $modal.open({
                templateUrl: '/transferContent.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];
            function ModalInstanceCtrl($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {
                var url = 'api/OsdTrnCustomerQueryMgmt/TransferEmployee';
                SocketService.get(url).then(function (resp) {
                    $scope.TransferEmployeeList = resp.data.TransferEmployeeList;
                    unlockUI();
                });

                $scope.marketingcall_gid = marketingcall_gid;
                $scope.ticketref_no = ticketref_no;
                $scope.assigned_to = assigned_to;

                $scope.transfercall = function () {

                    if ($scope.transfer_to == undefined) {
                        modalInstance.close('closed');
                        Notify.alert('Kindly Select the Assign to Person', 'warning');
                        return;
                    }

                    var params = {
                        marketingcall_gid: $scope.marketingcall_gid,
                        employee_gid: $scope.transfer_to,
                        employee_name: $('#transfer_to :selected').text(),
                        transfer_remarks: $scope.transfer_remarks
                    }

                    var url = "api/MstTelecalling/TicketTransfer";
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status = true) {
                            modalInstance.close('closed');
                            Notify.alert(resp.data.message, 'success');
                            activate();
                        }
                        else {
                            modalInstance.close('closed');
                            Notify.alert(resp.data.message, 'warning');
                            activate();
                        }
                    });


                }
                /* var url = 'api/MstTelecalling/TransferLog';
                 var params = {
                     inboundcall_gid:inboundcall_gid
                 }
                 SocketService.getparams(url, params).then(function (resp) {
                         $scope.TransferLog_list = resp.data.transferLog_list;
                 }); */
                $scope.close = function () {
                    modalInstance.close('closed');
                };
            }
        }
    }
})();
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstMarketingMBDRejectedCallViewController', MstMarketingMBDRejectedCallViewController);

    MstMarketingMBDRejectedCallViewController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function MstMarketingMBDRejectedCallViewController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstMarketingMBDRejectedCallViewController';
        $scope.marketingcall_gid = $location.search().marketingcall_gid;
        var marketingcall_gid = $scope.marketingcall_gid;
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;

        activate();
        lockUI();
        function activate() {
            var params = {
                marketingcall_gid: marketingcall_gid
            }

            var url = 'api/Marketing/GetMarketingCallAssignedView';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtticket_refid = resp.data.ticket_refid,
                $scope.txtentity_name = resp.data.entity_name,
                $scope.txtsourceofcontact_name = resp.data.sourceofcontact_name,
                $scope.txtcallreceivednumber_name = resp.data.callreceivednumber_name,
                $scope.txtcustomer_type = resp.data.leadrequesttype_name,
                $scope.txtcallreceived_date = resp.data.callreceived_date,
                $scope.txtcaller_name = resp.data.caller_name,
                $scope.txtinternalreference_name = resp.data.internalreference_name,
                $scope.txtcallerassociate_company = resp.data.callerassociate_company,
                $scope.txtoffice_landlineno = resp.data.office_landlineno,
                $scope.txtcalltype_name = resp.data.calltype_name,
                $scope.txtfunction_name = resp.data.function_name,
                $scope.txtfunction_remarks = resp.data.function_remarks,
                  $scope.txttat_hours = resp.data.tat_hours,
                $scope.txtrequirement = resp.data.requirement,
                $scope.txtenquiry_description = resp.data.enquiry_description,
                $scope.txtcallclosure_status = resp.data.callclosure_status,
                $scope.txtassignemployee_name = resp.data.assignemployee_name,
                $scope.txtassign_date = resp.data.assign_date,
                $scope.txttagemployee_name = resp.data.tagemployee_name,
                $scope.txtassignclosure_remarks = resp.data.assignclosure_remarks,
                $scope.ibcalladdress_list = resp.data.MarketingCalladdress_list;
                $scope.ibcallfollowup_list = resp.data.MarketingCallfollowup_list;
                $scope.ibcalltransfer_list = resp.data.MarketingCalltransfer_list,
                $scope.ibcalltaggedmember_list = resp.data.MarketingCalltaggedmember_list;
                $scope.txtcompleted_by = resp.data.completed_by,
                 $scope.txtcompleted_date = resp.data.completed_date,
                $scope.txtcompleted_remarks = resp.data.completed_remarks,
               $scope.txtfollowup_date = resp.data.followup_date,
                $scope.txtfollowup_time = resp.data.followup_time,
                $scope.txtfollowup_by = resp.data.followup_by,
                $scope.txtfollowup_remarks = resp.data.followup_remarks,
                 $scope.txtrejected_date = resp.data.rejected_date,
                $scope.txtrejected_by = resp.data.rejected_by,
                $scope.origination = resp.data.origination,
                 $scope.txtbase_location = resp.data.baselocation_name,
                $scope.txtrejected_remarks = resp.data.rejected_remarks,
                unlockUI();
            });
        }
        $scope.Back = function () {
            $location.url('app/MstMarketingMBDRejectedCallSummary');
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstMarketingMyAssignedCallSummaryController', MstMarketingMyAssignedCallSummaryController);

    MstMarketingMyAssignedCallSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams','cmnfunctionService'];

    function MstMarketingMyAssignedCallSummaryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstMarketingMyAssignedCallSummaryController';
        activate();

        function activate() {
            var url = 'api/Marketing/GetEmpAssignedMarketingCallSummary';
            SocketService.get(url).then(function (resp) {
                $scope.ibcall_list = resp.data.MarketingCall_list;
                unlockUI();
            });           

            var url = "api/Marketing/EmployeeMarketingCallCount";
            SocketService.get(url).then(function (resp) {
                $scope.assignedcall_count = resp.data.assignedcall_count;
                $scope.transfercall_count = resp.data.transfercall_count;
                $scope.completedcall_count = resp.data.completedcall_count;
                $scope.rejectedcall_count = resp.data.rejectedcall_count;
                $scope.inprogresscall_count = resp.data.inprogresscall_count;
                $scope.tagmember_count = resp.data.taggedcall_count;
                $scope.followupcall_count = resp.data.followupcall_count;
                $scope.closedcall_count = resp.data.closedcall_count;

                unlockUI();
            });
        }
       
        $scope.myassigned_calls = function () {
            $location.url("app/MstMarketingMyAssignedCallSummary");
        }
        $scope.tag_member = function () {
            $location.url("app/MstMarketingTaggedMemberSummary");
        }
        $scope.transfer_calls = function () {
            $location.url("app/MstMarketingTransferCallSummary");
        }
        $scope.completed_calls = function () {
            $location.url("app/MstMarketingCompletedCallSummary");
        }
        $scope.call_response = function (marketingcall_gid) {
            $location.url("app/MstMarketingCallResponse?hash=" + cmnfunctionService.encryptURL("marketingcall_gid=" + marketingcall_gid));
        }
        $scope.view = function (marketingcall_gid) {
            $location.url("app/MstMarketingAssignView?hash=" + cmnfunctionService.encryptURL("marketingcall_gid=" + marketingcall_gid));
        }
        $scope.edit = function (marketingcall_gid) {
            $location.url("app/MstMarketingEdit?hash=" + cmnfunctionService.encryptURL("marketingcall_gid=" + marketingcall_gid));
        }
        $scope.work_inprogress = function () {
            $location.url("app/MstMarketingWorkInprogressCallSummary");
        }
        $scope.followup_call = function () {
            $location.url("app/MstMarketingFollowUpCallSummary");
        }
        $scope.mbdrejected_calls = function () {
            $location.url("app/MstMarketingMBDRejectedCallSummary");
        }
        $scope.closedcall= function() {
            $location.url("app/MstMarketingMyleadsClosedCall");
        }

        $scope.transfer = function (marketingcall_gid) {

            var modalInstance = $modal.open({
                templateUrl: '/transferContent.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];
            function ModalInstanceCtrl($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {

                var params = {
                    marketingcall_gid: marketingcall_gid
                }
                var url = 'api/Marketing/MarketingCallDetailsForTransfer';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.marketingcall_gid = resp.data.marketingcall_gid;
                    $scope.ticket_refid = resp.data.ticket_refid;
                    $scope.assignemployee_gid = resp.data.assignemployee_gid;
                    $scope.assignemployee_name = resp.data.assignemployee_name;
                    $scope.ibcalltransfer_list = resp.data.MarketingCalltransfer_list;
                });

                var url = 'api/OsdTrnCustomerQueryMgmt/TransferEmployee';
                SocketService.get(url).then(function (resp) {
                    $scope.TransferEmployeeList = resp.data.TransferEmployeeList;
                    unlockUI();
                });

           /*     $scope.marketingcall_gid = marketingcall_gid;
                $scope.ticketref_no = ticketref_no;
                $scope.assigned_to = assigned_to; */

                $scope.transfer_call = function () {

                    if ($scope.cboTransferTo == undefined) {
                        modalInstance.close('closed');
                        Notify.alert('Kindly Select the Assign to Person', 'warning');
                        return;
                    }

                    var params = {
                        marketingcall_gid: $scope.marketingcall_gid,
                        ticket_refid: $scope.ticket_refid,
                        transferfrom_gid: $scope.assignemployee_gid,
                        transferfrom_name: $scope.assignemployee_name,
                        transferto_gid: $scope.cboTransferTo,
                        transferto_name: $('#transfer_to :selected').text(),                       
                        transfer_remarks: $scope.transfer_remarks
                    }

                    var url = "api/Marketing/MarketingCallTransferEmployee";
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status = true) {
                            modalInstance.close('closed');
                            Notify.alert(resp.data.message, 'success');
                            activate();
                        }
                        else {
                            modalInstance.close('closed');
                            Notify.alert(resp.data.message, 'warning');
                            activate();
                        }
                    });


                }
                /* var url = 'api/MstMarketing/TransferLog';
                 var params = {
                     marketingcall_gid:marketingcall_gid
                 }
                 SocketService.getparams(url, params).then(function (resp) {
                         $scope.TransferLog_list = resp.data.transferLog_list;
                 }); */
                $scope.close = function () {
                    modalInstance.close('closed');
                };
            }
        }
    }
})();
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstMarketingMyleadsClosedCallController', MstMarketingMyleadsClosedCallController);

    MstMarketingMyleadsClosedCallController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'cmnfunctionService'];

    function MstMarketingMyleadsClosedCallController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstMarketingMyleadsClosedCallController';
        activate();
        lockUI();
        function activate() {
            var url = 'api/Marketing/GetClosedMyleadsMarketingCallSummary';
            SocketService.get(url).then(function (resp) {
                $scope.ibcall_list = resp.data.MarketingCall_list;
                unlockUI();
            });           

            var url = "api/Marketing/EmployeeMarketingCallCount";
            SocketService.get(url).then(function (resp) {
                $scope.assignedcall_count = resp.data.assignedcall_count;
                $scope.transfercall_count = resp.data.transfercall_count;
                $scope.completedcall_count = resp.data.completedcall_count;
                $scope.inprogresscall_count = resp.data.inprogresscall_count;
                $scope.tagmember_count = resp.data.taggedcall_count;
                $scope.followupcall_count = resp.data.followupcall_count;
                $scope.rejectedcall_count = resp.data.rejectedcall_count; 
                $scope.closedcall_count = resp.data.closedcall_count;
                unlockUI();
            });

        }
        $scope.myassigned_calls = function () {
            $location.url("app/MstMarketingMyAssignedCallSummary");
        }
        $scope.tag_member = function () {
            $location.url("app/MstMarketingTaggedMemberSummary");
        }
        $scope.transfer_calls = function () {
            $location.url("app/MstMarketingTransferCallSummary");
        }
        $scope.completed_calls = function () {
            $location.url("app/MstMarketingCompletedCallSummary");
        }
        $scope.call_response = function (marketingcall_gid) {
            $location.url("app/MstMarketingCallResponse?hash=" + cmnfunctionService.encryptURL("marketingcall_gid=" + marketingcall_gid));
        }
        $scope.view = function (marketingcall_gid) {
            $location.url("app/MstMarketingAssignedFollowupLeadsView?hash=" + cmnfunctionService.encryptURL("marketingcall_gid=" + marketingcall_gid));
        }
        $scope.edit = function (marketingcall_gid) {
            $location.url("app/MstMarketingEdit?hash=" + cmnfunctionService.encryptURL("marketingcall_gid=" + marketingcall_gid));
        }
        $scope.work_inprogress = function () {
            $location.url("app/MstMarketingWorkInprogressCallSummary");
        }
        $scope.followup_call = function () {
            $location.url("app/MstMarketingFollowUpCallSummary");
        }
        $scope.mbdrejected_calls = function () {
            $location.url("app/MstMarketingMBDRejectedCallSummary");
        }
        $scope.closedcall= function() {
            $location.url("app/MstMarketingMyleadsClosedCall");
        }
      
        $scope.edit_inboundcall = function (marketingcall_gid) {
            $location.url("app/MstMarketingEdit?hash=" + cmnfunctionService.encryptURL("lsmarketingcall_gid=" + marketingcall_gid));
        }
        $scope.view_inboundcall = function (marketingcall_gid) {
            $location.url("app/MstMarketingClosedView?hash=" + cmnfunctionService.encryptURL("lsmarketingcall_gid=" + marketingcall_gid + '&lspage=MarketingCloseMyLead'));
        }
        $scope.rejectedcall = function () {
            $location.url("app/MstMarketingRejectedCallSummary");
        }
    }
})();
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstMarketingRejectedCallSummaryController', MstMarketingRejectedCallSummaryController);

    MstMarketingRejectedCallSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function MstMarketingRejectedCallSummaryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstMarketingRejectedCallSummaryController';
        activate();
        function activate() {
            var url = "api/Marketing/MarketingCallCount";
            SocketService.get(url).then(function (resp) {
                $scope.unassignedcall_count = resp.data.unassignedcall_count;
                $scope.assignedcall_count = resp.data.assignedcall_count;
                $scope.completedcall_count = resp.data.completedcall_count;
                $scope.followupcall_count = resp.data.followupcall_count;
                $scope.closedcall_count = resp.data.closedcall_count;
                $scope.rejectedcall_count = resp.data.rejectedcall_count;
                unlockUI();
            });

            var url = 'api/Marketing/GetRejectedMarketingCallSummary';
            SocketService.get(url).then(function (resp) {
                $scope.ibrejectedcall_list = resp.data.MarketingCall_list;
                unlockUI();
            });
        }
        $scope.addinbound = function () {
            $location.url("app/MstMarketingAdd");
        }
        $scope.closedcall = function () {
            $location.url("app/MstMarketingClosedCall");
        }
        $scope.followupcall = function () {
            $location.url("app/MstMarketingFollowupCall");
        }
        $scope.assignedcall = function () {
            $location.url("app/MstMarketingSummary");
        }
        $scope.unassignedcall= function() {
            $location.url("app/MstMarketingUnassignedLeadSummary");
        }
        $scope.completedcall = function () {
            $location.url("app/MstMarketingCompletedCall");
        }

        $scope.view = function (marketingcall_gid) {
            $location.url('app/MstMarketingRejectedCallView?marketingcall_gid=' + marketingcall_gid);
        }

        $scope.rejectedcall = function () {
            $location.url("app/MstMarketingRejectedCallSummary");
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstMarketingRejectedCallViewController', MstMarketingRejectedCallViewController);

    MstMarketingRejectedCallViewController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function MstMarketingRejectedCallViewController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstMarketingRejectedCallViewController';
        $scope.marketingcall_gid = $location.search().marketingcall_gid;
        var marketingcall_gid = $scope.marketingcall_gid;
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;

        activate();
        lockUI();
        function activate() {
            var params = {
                marketingcall_gid: marketingcall_gid
            }

            var url = 'api/Marketing/GetMarketingCallAssignedView';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtticket_refid = resp.data.ticket_refid,
                $scope.txtentity_name = resp.data.entity_name,
                $scope.txtsourceofcontact_name = resp.data.sourceofcontact_name,
                $scope.txtcallreceivednumber_name = resp.data.callreceivednumber_name,
                $scope.txtcustomer_type = resp.data.leadrequesttype_name,
                $scope.txtcallreceived_date = resp.data.callreceived_date,
                $scope.txtcaller_name = resp.data.caller_name,
                $scope.txtinternalreference_name = resp.data.internalreference_name,
                $scope.txtcallerassociate_company = resp.data.callerassociate_company,
                $scope.txtoffice_landlineno = resp.data.office_landlineno,
                $scope.txtcalltype_name = resp.data.calltype_name,
                $scope.txtfunction_name = resp.data.function_name,
                $scope.txtfunction_remarks = resp.data.function_remarks,
                  $scope.txttat_hours = resp.data.tat_hours,
                $scope.txtrequirement = resp.data.requirement,
                $scope.txtenquiry_description = resp.data.enquiry_description,
                $scope.txtcallclosure_status = resp.data.callclosure_status,
                $scope.txtassignemployee_name = resp.data.assignemployee_name,
                $scope.txtassign_date = resp.data.assign_date,
                $scope.txttagemployee_name = resp.data.tagemployee_name,
                $scope.txtassignclosure_remarks = resp.data.assignclosure_remarks,
                $scope.ibcalladdress_list = resp.data.MarketingCalladdress_list;
                $scope.ibcallfollowup_list = resp.data.MarketingCallfollowup_list;
                $scope.ibcalltransfer_list = resp.data.MarketingCalltransfer_list,
                $scope.ibcalltaggedmember_list = resp.data.MarketingCalltaggedmember_list;
                $scope.txtcompleted_by = resp.data.completed_by,
                 $scope.txtcompleted_date = resp.data.completed_date,
                $scope.txtcompleted_remarks = resp.data.completed_remarks,
               $scope.txtfollowup_date = resp.data.followup_date,
                $scope.txtfollowup_time = resp.data.followup_time,
                $scope.txtfollowup_by = resp.data.followup_by,
                $scope.txtfollowup_remarks = resp.data.followup_remarks,
                 $scope.txtrejected_date = resp.data.rejected_date,
                $scope.txtrejected_by = resp.data.rejected_by,
                 $scope.txtbase_location = resp.data.baselocation_name,
                $scope.txtrejected_remarks = resp.data.rejected_remarks,
                $scope.origination = resp.data.origination,
                unlockUI();
            });
        }
        $scope.Back = function () {
            $location.url('app/MstMarketingRejectedCallSummary');
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstMarketingReportController', MstMarketingReportController);

    MstMarketingReportController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'DownloaddocumentService', 'cmnfunctionService'];

    function MstMarketingReportController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstMarketingReportController';
        activate();
        function activate() {
            var url = 'api/Marketing/MarketingReportSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.TeleCallingReportList = resp.data.MarketingReportList;
                unlockUI();
            }); 
             
        }
        
        $scope.TeleCallingReport = function () {
            lockUI();
            var url = 'api/Marketing/ExportMarketingReport';
            SocketService.get(url).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    DownloaddocumentService.Downloaddocument(resp.data.lscloudpath, resp.data.lsname);

                    //var phyPath = resp.data.lspath;
                    //var relPath = phyPath.split("EMS");
                    //var relpath1 = relPath[1].replace("\\", "/");
                    //var hosts = window.location.host;
                    //var prefix = location.protocol + "//";
                    //var str = prefix.concat(hosts, relpath1);
                    //var link = document.createElement("a");
                    //var name = resp.data.lsname.split('.');
                    //link.download = name[0];
                    //var uri = str;
                    //link.href = uri;
                    //link.click();
                }
                else {
                    unlockUI();
                    Notify.alert('Error Occurred While Exporting !', 'warning')

                }

            });
        }
       

        $scope.view = function (val) {
            $location.url('app/MstMarketingReportView?hash=' + cmnfunctionService.encryptURL('marketingcall_gid=' + val));
        }
        
    }
})();
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstMarketingReportViewController', MstMarketingReportViewController);

    MstMarketingReportViewController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'DownloaddocumentService','cmnfunctionService'];

    function MstMarketingReportViewController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstMarketingReportViewController';
       
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        var marketingcall_gid = searchObject.marketingcall_gid;
        var lspage = searchObject.lspage;

        $scope.transfershow = false;
        $scope.followupshow = false;
        $scope.closedshow = false;
        $scope.RejectedShow = false;

        lockUI();
        activate();
        function activate() {
            vm.calender1 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open1 = true;
            };
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };

            //if (inboundcall_status == 'close') {
            //    $scope.closedshow = true;
            //    $scope.followupshow = false;
            //    $scope.completedshow = false;
            //    $scope.RejectedShow = false;
            //}
            //else if (lspage == 'FollowUpInboundCall') {
            //    $scope.followupshow = true;
            //    $scope.completedshow = false;
            //    $scope.closedshow = false;
            //    $scope.RejectedShow = false;
            //}
            //else if (lspage == 'CompletedInboundCall') {
            //    $scope.completedshow = true;
            //    $scope.closedshow = false;
            //    $scope.followupshow = false;
            //    $scope.RejectedShow = false;
            //}
            //else if (lspage == 'RejectedInboundCall') {
            //    $scope.RejectedShow = true;
            //    $scope.closedshow = false;
            //    $scope.followupshow = false;
            //    $scope.completedshow = false;
            //}
            //else {

            //}
//
            var params = {
                marketingcall_gid: marketingcall_gid
            }
            var url = 'api/Marketing/GetMarketingCallReportView';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtticket_refid = resp.data.ticket_refid,
                $scope.txtentity_name = resp.data.entity_name,
                $scope.txtsourceofcontact_name = resp.data.sourceofcontact_name,
                $scope.txtcallreceivednumber_name = resp.data.callreceivednumber_name,
                $scope.txtcustomer_type = resp.data.leadrequesttype_name,
                $scope.txtcallreceived_date = resp.data.callreceived_date,
                $scope.txtcaller_name = resp.data.caller_name,
                $scope.txtinternalreference_name = resp.data.internalreference_name,
                $scope.txtcallerassociate_company = resp.data.callerassociate_company,
                $scope.txtoffice_landlineno = resp.data.office_landlineno,
                $scope.txtcalltype_name = resp.data.calltype_name,
                $scope.txtfunction_name = resp.data.function_name,
                $scope.txtrequirement = resp.data.requirement,
                $scope.txtenquiry_description = resp.data.enquiry_description,
                $scope.txtcallclosure_status = resp.data.callclosure_status,
                $scope.txtassignemployee_name = resp.data.assignemployee_name,
                $scope.txtassign_date = resp.data.assign_date,
                $scope.txttagemployee_name = resp.data.tagemployee_name,
                $scope.txtassignclosure_remarks = resp.data.assignclosure_remarks,
                $scope.txtclosed_remarks = resp.data.closed_remarks,
                $scope.ibcalladdress_list = resp.data.MarketingCalladdress_list;
                $scope.inboundcallfollowup_list = resp.data.inboundcallfollowup_list,
                $scope.txtprimary_mobileno = resp.data.primary_mobileno,
                $scope.ibcallmobileno_list = resp.data.MarketingCallmobileno_list;
                $scope.txtprimary_email = resp.data.primary_email,
                $scope.ibcallemail_list = resp.data.MarketingCallemail_list;
                $scope.ibcallextendfollowup_list = resp.data.marketingcallextendfollowup_list;
                $scope.ibcallfollowup_list = resp.data.MarketingCallfollowup_list;
                $scope.ibcalltaggedmember_list = resp.data.MarketingCalltaggedmember_list;
                $scope.ibcalltransfer_list = resp.data.MarketingCalltransfer_list,
                $scope.txtcompleted_by = resp.data.completed_by,
                $scope.txtcompleted_date = resp.data.completed_date,
                $scope.txtcompleted_remarks = resp.data.completed_remarks,
                $scope.txtclosed_date = resp.data.closed_date,
                $scope.txtclosed_by = resp.data.closed_by,
                $scope.txtclosed_remarks = resp.data.closed_remarks,
                $scope.txtrejected_date = resp.data.rejected_date,
                $scope.txtrejected_by = resp.data.rejected_by,
                $scope.txtrejected_remarks = resp.data.rejected_remarks,
                $scope.txtfollowup_date = resp.data.followup_date,
                $scope.txtfollowup_time = resp.data.followup_time,
                $scope.txtfollowup_by = resp.data.followup_by,
                $scope.txtfollowup_remarks = resp.data.followup_remarks,
                $scope.txtbase_location = resp.data.baselocation_name,
                $scope.txtacknowledged_date = resp.data.acknowledge_date,
                $scope.txtacknowledged_by = resp.data.acknowledge_by,
                $scope.txttat_hours = resp.data.tat_hours,
                $scope.txtfunction_remarks = resp.data.function_remarks,
                $scope.ibcallstatus_list = resp.data.MarketingCallstatus_list,
                $scope.txtloanproduct_name = resp.data.loanproduct_name,
                $scope.txtloansubproduct_name = resp.data.loansubproduct_name,
                $scope.txtloan_amount = resp.data.loan_amount,
                $scope.txtclosed = resp.data.closed,
                    $scope.origination = resp.data.origination,
                    $scope.txtleadrequire_name = resp.data.leadrequire_name,
                    $scope.txtmilletrequire_name = resp.data.milletrequire_name,
                    $scope.txtenquiryrequire_name = resp.data.enquiryrequire_name,
                    $scope.txtstartuprequire_name = resp.data.startuprequire_name,
                    $scope.txtbusiness_name = resp.data.business_name,
                    $scope.txtindustry_name = resp.data.industry_name,

                unlockUI();
            });


            var url = 'api/Marketing/MarketingCallProofDocumentList';
            lockUI();
            SocketService.getparams(url,params).then(function (resp) {
                $scope.lufilename = resp.data.filename;
                $scope.lufilepath = resp.data.filepath;
                $scope.Uploadcallproofdocument_list = resp.data.callproofupload_list;
                unlockUI();
            });
            var url = 'api/Marketing/GetDocumentList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.lsfilename = resp.data.filename;
                $scope.lsfilepath = resp.data.filepath;
                $scope.document_list = resp.data.document_list;
            });
            var url = 'api/Marketing/GetMilletDocumentList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.lmfilename = resp.data.filename;
                $scope.lmfilepath = resp.data.filepath;
                $scope.milletdocument_list = resp.data.milletdocument_list;
            });
            var url = 'api/Marketing/GetEnquiryDocumentList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.lmfilename = resp.data.filename;
                $scope.lmfilepath = resp.data.filepath;
                $scope.enquirydocument_list = resp.data.enquirydocument_list;
            });
            var url = 'api/Marketing/MarketingCallRecordingDocumentList';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.Uploadcallrecordingdocument_list = resp.data.callproofupload_list;
                unlockUI();
            });
            var url = 'api/Marketing/GetMarketingCallLeadstatusList';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.MarketingCallLeadstatus_list = resp.data.MarketingCallLeadstatus_list;
                unlockUI();
            });
        }

      
        $scope.documentviewer = function (val1, val2) {
            lockUI();
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
            if (IsValidExtension == false) {
            Notify.alert("View is not supported for this format..!", {
             status: 'danger',
             pos: 'top-center',
             timeout: 3000
             });
             unlockUI();
           return false;
             }
             DownloaddocumentService.DocumentViewer(val1, val2);
             }

             $scope.documentviewermillet = function (val1, val2) {
                lockUI();
                var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
                if (IsValidExtension == false) {
                Notify.alert("View is not supported for this format..!", {
                 status: 'danger',
                 pos: 'top-center',
                 timeout: 3000
                 });
                 unlockUI();
               return false;
                 }
                 DownloaddocumentService.DocumentViewer(val1, val2);
                 }

                 $scope.documentviewerupload = function (val1, val2) {
                    lockUI();
                    var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
                    if (IsValidExtension == false) {
                    Notify.alert("View is not supported for this format..!", {
                     status: 'danger',
                     pos: 'top-center',
                     timeout: 3000
                     });
                     unlockUI();
                   return false;
                     }
                     DownloaddocumentService.DocumentViewer(val1, val2);
                     }
        $scope.download_allmillet = function (val1, val2) {
            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }
        }
        $scope.download_allenquiry = function (val1, val2) {
            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }
        }  
        $scope.milletdocument_downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.enquirydocument_downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.documentviewerenquiry = function (val1, val2) {
            lockUI();
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
            if (IsValidExtension == false) {
                Notify.alert("View is not supported for this format..!", {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                unlockUI();
                return false;
            }
            DownloaddocumentService.DocumentViewer(val1, val2);
        }
        $scope.rec_downloads = function (val1, val2) {
            //var phyPath = val1;
            //var relPath = phyPath.split("StoryboardAPI");
            //var relpath1 = relPath[1].replace("\\", "/");
            //var hosts = window.location.host;
            //var prefix = location.protocol + "//";
            //var str = prefix.concat(hosts, relpath1);
            //var link = document.createElement("a");
            //link.download = val2;
            //var uri = str;
            //link.href = uri;
            //link.click();
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.document_downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.downloads_all = function (val1, val2) {

            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }

        }

        $scope.downloadall1 = function (val1, val2) {

            for (var i = 0; i < val2.length; i++) {
                //console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }
        
        }
        $scope.downloadall = function (val1, val2) {

            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }

        }
        
        $scope.recproof_downloads = function (val1, val2) {
            //var phyPath = val1;
            //var relPath = phyPath.split("StoryboardAPI");
            //var relpath1 = relPath[1].replace("\\", "/");
            //var hosts = window.location.host;
            //var prefix = location.protocol + "//";
            //var str = prefix.concat(hosts, relpath1);
            //var link = document.createElement("a");
            //link.download = val2;
            //var uri = str;
            //link.href = uri;
            //link.click();
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.view_remarks = function (remarks) {
            var modalInstance = $modal.open({
                templateUrl: '/remarks.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {               
                $scope.txtremarks = remarks;
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }
        $scope.back = function () {
            $state.go('app.MstMarketingReport');
        }
        
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstMarketingSourceofContactController', MstMarketingSourceofContactController);

    MstMarketingSourceofContactController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstMarketingSourceofContactController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstMarketingSourceofContactController';

        activate();

        function activate() {          
            var url = 'api/MstMarketingSourceOfContact/GetMarketingSourceofContact';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.sourceofcontact_data = resp.data.application_list;
                unlockUI();
            });
        }
        $scope.addsourceofcontact = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addsource.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                               
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.submit = function () {

                    var params = {
                        marketingsourceofcontact_name: $scope.txtsource_name,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code
                    
                    }
                    var url = 'api/MstMarketingSourceOfContact/CreateMarketingSourceofContact';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                            activate();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                            activate();
                        }
                    });
                }
                
            }
        }
        $scope.editsourceofcontact = function (marketingsourceofcontact_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editsource.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                var params = {
                    marketingsourceofcontact_gid: marketingsourceofcontact_gid
                }
                var url = 'api/MstMarketingSourceOfContact/EditMarketingSourceofContact';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditsource_name = resp.data.marketingsourceofcontact_name;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.marketingsourceofcontact_gid = resp.data.marketingsourceofcontact_gid;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update = function () {

                    var url = 'api/MstMarketingSourceOfContact/UpdateMarketingSourceofContact';
                    var params = {
                        marketingsourceofcontact_name: $scope.txteditsource_name,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        marketingsourceofcontact_gid: $scope.marketingsourceofcontact_gid
                    }
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();

                        }
                        else {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                    });
                }

            }
        }

        $scope.Status_update = function (marketingsourceofcontact_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statussource.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    marketingsourceofcontact_gid: marketingsourceofcontact_gid
                }            
                var url = 'api/MstMarketingSourceOfContact/EditMarketingSourceofContact';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.marketingsourceofcontact_gid = resp.data.marketingsourceofcontact_gid
                    $scope.txtsource_name = resp.data.marketingsourceofcontact_name;
                    $scope.rbo_status = resp.data.Status;
                });    
                           
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        marketingsourceofcontact_gid: marketingsourceofcontact_gid,
                        remarks: $scope.txtremarks,
                        rbo_status:$scope.rbo_status
                    
                    }
                    var url = 'api/MstMarketingSourceOfContact/InactiveMarketingSourceofContact';
                     lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }activate();
                    }); 

                    $modalInstance.close('closed');

                }
                var params = {
                    marketingsourceofcontact_gid: marketingsourceofcontact_gid
                }

                var url = 'api/MstMarketingSourceOfContact/InactiveMarketingSourceofcontactHistory';

                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.sourceofcontactinactivelog_data = resp.data.inactivehistory_list;
                    unlockUI();
                }); 
            }
        }

        $scope.delete = function (marketingsourceofcontact_gid) {
            var params = {
                marketingsourceofcontact_gid: marketingsourceofcontact_gid
            }
           
            
           SweetAlert.swal({
                        title: 'Are you sure?',
                        text: 'Do You Want To Delete the Record ?',
                        showCancelButton: true,
                        confirmButtonColor: '#d64b3c',
                        confirmButtonText: 'Yes, delete it!',
                        closeOnConfirm: false,
                        cancelButtonColor: 'Crimson',                       
                
                    }, function (isConfirm) {
                        if (isConfirm) {
                            var url = 'api/MstMarketingSourceOfContact/DeleteMarketingSourceofContact';
                            SocketService.getparams(url, params).then(function (resp) {
                                if (resp.data.status == true) {
                                SweetAlert.swal('Deleted Successfully!');
                                activate();
                                }
                                else if (resp.data.status == false) {
                                    SweetAlert.swal({
                                        title: 'Warning!',
                                        text: "Can't able to delete Source Of Contact because it is mapped to add Business Development call",
                                        timer: 5000,
                                       
                                        showCancelButton: false,
                                        showConfirmButton: false,
                                        
                                        backgroundcolor: '#d64b3c'
                                });
                                    activate();
                                    }

                                else {
                                Notify.alert(SweetAlert.swal(resp.data.message,{
                             
                                    type: 'warning',
      title: 'warning!',
      text: "Can't able to delete Source Of Contact because it is mapped to add Business Development call",
      timer: 2000,
      showCancelButton: false,
      showConfirmButton: false
                                }

                                ));
                                activate();
                                }
                            });
                        }
                    });
        }
    }
})();


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstMarketingSummaryController', MstMarketingSummaryController);

    MstMarketingSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'cmnfunctionService'];

    function MstMarketingSummaryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstMarketingSummaryController';
        var selectedIndex = $location.search().selectedIndex;
        activate();
        lockUI();
        function activate() {
           
            var url = "api/Marketing/MarketingCallCount";
            SocketService.get(url).then(function (resp) {
                $scope.unassignedcall_count = resp.data.unassignedcall_count;
                $scope.assignedcall_count = resp.data.assignedcall_count;
                $scope.completedcall_count = resp.data.completedcall_count;
                $scope.followupcall_count = resp.data.followupcall_count;
                $scope.closedcall_count = resp.data.closedcall_count;
                $scope.rejectedcall_count = resp.data.rejectedcall_count;
                unlockUI();
            });

            var url = 'api/Marketing/GetMarketingCallSummary';
            SocketService.get(url).then(function (resp) {
                $scope.ibcall_list = resp.data.MarketingCall_list;
                unlockUI();
            });
        }
        $scope.addinbound = function () {
            $location.url("app/MstMarketingAdd");
        }
        $scope.closedcall= function() {
            $location.url("app/MstMarketingClosedCall");
        }
        $scope.followupcall= function() {
            $location.url("app/MstMarketingFollowupCall");
        }
        $scope.assignedcall= function() {
            $location.url("app/MstMarketingSummary");
        }
        $scope.unassignedcall= function() {
            $location.url("app/MstMarketingUnassignedLeadSummary");
        }
        $scope.completedcall= function() {
            $location.url("app/MstMarketingCompletedCall");
        }
        //$scope.applcreation_view = function (application_gid) {
        //    $location.url('app/AgrMstCustomerOnboardingApproval?hash=' + cmnfunctionService.encryptURL('application_gid=' + application_gid + '&selectedIndex=' + $scope.selectedIndex + '&lsApp=N&FromRM=Y'));
        //}
        $scope.view_inboundcall = function (marketingcall_gid) {
            $location.url('app/MstMarketingAssignView?hash=' + cmnfunctionService.encryptURL('lsmarketingcall_gid=' + marketingcall_gid + '&lspage=MarketingAddCall'));
        }
        $scope.edit_inboundcall = function (marketingcall_gid) {
            $location.url('app/MstMarketingEdit?hash=' + cmnfunctionService.encryptURL('lsmarketingcall_gid=' + marketingcall_gid));
        }

        $scope.rejectedcall = function () {
            $location.url("app/MstMarketingRejectedCallSummary");
        }

        $scope.transfer = function (marketingcall_gid) {

            var modalInstance = $modal.open({
                templateUrl: '/transferContent.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];
            function ModalInstanceCtrl($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {


                var params = {
                    marketingcall_gid: marketingcall_gid
                }
                var url = 'api/Marketing/MarketingCallDetailsForTransfer';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.marketingcall_gid = resp.data.marketingcall_gid;
                    $scope.ticket_refid = resp.data.ticket_refid;
                    $scope.assignemployee_gid = resp.data.assignemployee_gid;
                    $scope.assignemployee_name = resp.data.assignemployee_name;
                    $scope.ibcalltransfer_list = resp.data.MarketingCalltransfer_list;
                });

                var url = 'api/OsdTrnCustomerQueryMgmt/TransferEmployee';
                SocketService.get(url).then(function (resp) {
                    $scope.TransferEmployeeList = resp.data.TransferEmployeeList;
                    unlockUI();
                });

          /*      $scope.marketingcall_gid = marketingcall_gid;
                $scope.ticketref_no = ticketref_no;
                $scope.assigned_to = assigned_to; */

                $scope.transfer_call = function () {

                    if ($scope.cboTransferTo == undefined) {
                        modalInstance.close('closed');
                        Notify.alert('Kindly Select the Transfer to Person', 'warning');
                        return;
                    }

                    var params = {
                        marketingcall_gid: $scope.marketingcall_gid,
                        ticket_refid: $scope.ticket_refid,
                        transferfrom_gid: $scope.assignemployee_gid,
                        transferfrom_name: $scope.assignemployee_name,
                        transferto_gid: $scope.cboTransferTo,
                        transferto_name: $('#transfer_to :selected').text(),                       
                        transfer_remarks: $scope.transfer_remarks
                    }

                    var url = "api/Marketing/MarketingCallTransferEmployee";
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status = true) {
                            modalInstance.close('closed');
                            Notify.alert(resp.data.message, 'success');
                            activate();
                        }
                        else {
                            modalInstance.close('closed');
                            Notify.alert(resp.data.message, 'warning');
                            activate();
                        }
                    });
                    

                }
           /*     var url = 'api/MstTelecalling/TransferLog';
                var params = {
                    marketingcall_gid:marketingcall_gid
                }
                SocketService.getparams(url, params).then(function (resp) {
                        $scope.TransferLog = resp.data.TransferLog;
                });  */
                $scope.close = function () {
                    modalInstance.close('closed');
                };
            }
        }
        $scope.closed_call = function (marketingcall_gid) {

            var modalInstance = $modal.open({
                templateUrl: '/closedContent.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];
            function ModalInstanceCtrl($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {


                var params = {
                    marketingcall_gid: marketingcall_gid
                }
                
                $scope.close_call = function () {                   
                    var params = {
                        marketingcall_gid: marketingcall_gid,                       
                        closed_remarks: $scope.closed_remarks
                    }

                    var url = "api/Marketing/MarketingCallAssignedClosed";
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status = true) {
                            modalInstance.close('closed');
                            Notify.alert(resp.data.message, 'success');
                            activate();
                        }
                        else {
                            modalInstance.close('closed');
                            Notify.alert(resp.data.message, 'warning');
                            activate();
                        }
                    });


                }
                
                $scope.close = function () {
                    modalInstance.close('closed');
                };
            }
        }
    }
})();
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstMarketingTaggedMemberSummaryController', MstMarketingTaggedMemberSummaryController);

    MstMarketingTaggedMemberSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'cmnfunctionService'];

    function MstMarketingTaggedMemberSummaryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstMarketingTaggedMemberSummaryController';
        activate();

        function activate() {
            var url = 'api/Marketing/GetEmpTaggedSummary';
            SocketService.get(url).then(function (resp) {
                $scope.ibcalltagged_list = resp.data.MarketingCall_list;
                unlockUI();
            });           

            var url = "api/Marketing/EmployeeMarketingCallCount";
            SocketService.get(url).then(function (resp) {
                $scope.assignedcall_count = resp.data.assignedcall_count;
                $scope.transfercall_count = resp.data.transfercall_count;
                $scope.completedcall_count = resp.data.completedcall_count;
                $scope.rejectedcall_count = resp.data.rejectedcall_count;
                $scope.inprogresscall_count = resp.data.inprogresscall_count;
                $scope.tagmember_count = resp.data.taggedcall_count;
                $scope.followupcall_count = resp.data.followupcall_count;
                $scope.closedcall_count = resp.data.closedcall_count;
                unlockUI();
            });
        }
        $scope.myassigned_calls = function () {
            $location.url("app/MstMarketingMyAssignedCallSummary");
        }
        $scope.tag_member = function () {
            $location.url("app/MstMarketingTaggedMemberSummary");
        }
        $scope.transfer_calls = function () {
            $location.url("app/MstMarketingTransferCallSummary");
        }
        $scope.completed_calls = function () {
            $location.url("app/MstMarketingCompletedCallSummary");
        }
        $scope.view = function (marketingcall_gid) {
            $location.url("app/MstMarketingTaggedMemberView?hash=" + cmnfunctionService.encryptURL("marketingcall_gid=" + marketingcall_gid));
        }
        $scope.edit = function (marketingcall_gid) {
            $location.url("app/MstMarketingEdit?hash=" + cmnfunctionService.encryptURL("marketingcall_gid=" + marketingcall_gid));
        }
        $scope.work_inprogress = function () {
            $location.url("app/MstMarketingWorkInprogressCallSummary");
        }
        $scope.followup_call = function () {
            $location.url("app/MstMarketingFollowUpCallSummary");
        }
        $scope.mbdrejected_calls = function () {
            $location.url("app/MstMarketingMBDRejectedCallSummary");
        }
        $scope.closedcall= function() {
            $location.url("app/MstMarketingMyleadsClosedCall");
        }
      
        $scope.transfer = function (marketingcall_gid, ticketref_no, assigned_to) {

            var modalInstance = $modal.open({
                templateUrl: '/transferContent.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];
            function ModalInstanceCtrl($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {
                var url = 'api/OsdTrnCustomerQueryMgmt/TransferEmployee';
                SocketService.get(url).then(function (resp) {
                    $scope.TransferEmployeeList = resp.data.TransferEmployeeList;
                    unlockUI();
                });

                $scope.marketingcall_gid = marketingcall_gid;
                $scope.ticketref_no = ticketref_no;
                $scope.assigned_to = assigned_to;

                $scope.transfercall = function () {

                    if ($scope.transfer_to == undefined) {
                        modalInstance.close('closed');
                        Notify.alert('Kindly Select the Assign to Person', 'warning');
                        return;
                    }

                    var params = {
                        marketingcall_gid: $scope.marketingcall_gid,
                        employee_gid: $scope.transfer_to,
                        employee_name: $('#transfer_to :selected').text(),
                        transfer_remarks: $scope.transfer_remarks
                    }

                    var url = "api/MstMarketing/TicketTransfer";
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status = true) {
                            modalInstance.close('closed');
                            Notify.alert(resp.data.message, 'success');
                            activate();
                        }
                        else {
                            modalInstance.close('closed');
                            Notify.alert(resp.data.message, 'warning');
                            activate();
                        }
                    });


                }
                /* var url = 'api/MstMarketing/TransferLog';
                 var params = {
                     marketingcall_gid:marketingcall_gid
                 }
                 SocketService.getparams(url, params).then(function (resp) {
                         $scope.TransferLog_list = resp.data.transferLog_list;
                 }); */
                $scope.close = function () {
                    modalInstance.close('closed');
                };
            }
        }
    }
})();
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstMarketingTaggedMemberViewController', MstMarketingTaggedMemberViewController);

    MstMarketingTaggedMemberViewController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams','DownloaddocumentService','cmnfunctionService'];

    function MstMarketingTaggedMemberViewController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstMarketingTaggedMemberViewController';
       
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        var marketingcall_gid = searchObject.marketingcall_gid;
        lockUI();
        activate();
        function activate() {
            vm.calender1 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open1 = true;
            };
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };
            var params = {
                marketingcall_gid: marketingcall_gid
            }
            var url = 'api/Marketing/GetMarketingCallAssignedView';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtticket_refid = resp.data.ticket_refid,
                $scope.txtentity_name = resp.data.entity_name,
                $scope.txtsourceofcontact_name = resp.data.sourceofcontact_name,
                $scope.txtcallreceivednumber_name = resp.data.callreceivednumber_name,
                $scope.txtcustomer_type = resp.data.leadrequesttype_name,
                $scope.txtcallreceived_date = resp.data.callreceived_date,
                $scope.txtcaller_name = resp.data.caller_name,
                $scope.txtinternalreference_name = resp.data.internalreference_name,
                $scope.txtcallerassociate_company = resp.data.callerassociate_company,
                $scope.txtoffice_landlineno = resp.data.office_landlineno,
                $scope.txtcalltype_name = resp.data.calltype_name,
                $scope.txtfunction_name = resp.data.function_name,
                 $scope.txtfunction_remarks = resp.data.function_remarks,
                 $scope.txttat_hours = resp.data.tat_hours,
                $scope.txtrequirement = resp.data.requirement,
                $scope.txtenquiry_description = resp.data.enquiry_description,
                $scope.txtcallclosure_status = resp.data.callclosure_status,
                $scope.txtassignemployee_name = resp.data.assignemployee_name,
                $scope.txtassign_date = resp.data.assign_date,
                $scope.txttagemployee_name = resp.data.tagemployee_name,
                $scope.txtassignclosure_remarks = resp.data.assignclosure_remarks,
                 $scope.ibcalladdress_list = resp.data.MarketingCalladdress_list;
                $scope.ibcallfollowup_list = resp.data.MarketingCallfollowup_list;
                $scope.ibcalltaggedmember_list = resp.data.MarketingCalltaggedmember_list;
                $scope.ibcalltransfer_list = resp.data.MarketingCalltransfer_list,
                $scope.txtcompleted_by = resp.data.completed_by,
                $scope.txtcompleted_date = resp.data.completed_date,
                $scope.txtcompleted_remarks = resp.data.completed_remarks,
                $scope.txtclosed_date = resp.data.closed_date,
                $scope.txtclosed_by = resp.data.closed_by,
                $scope.txtclosed = resp.data.closed,
                $scope.txtloanproduct_name = resp.data.loanproduct_name,
                $scope.txtloansubproduct_name = resp.data.loansubproduct_name,
                $scope.txtloan_amount = resp.data.loan_amount,
                 $scope.txtbase_location = resp.data.baselocation_name,
                 $scope.origination = resp.data.origination,
                  $scope.ibcallmobileno_list = resp.data.MarketingCallmobileno_list;
                $scope.txtprimary_email = resp.data.primary_email,
                    $scope.ibcallemail_list = resp.data.MarketingCallemail_list;
                $scope.txtleadrequire_name = resp.data.leadrequire_name,
                    $scope.txtmilletrequire_name = resp.data.milletrequire_name,
                    $scope.txtenquiryrequire_name = resp.data.enquiryrequire_name,
                    $scope.txtstartuprequire_name = resp.data.startuprequire_name,
                    $scope.txtbusiness_name = resp.data.business_name,
                    $scope.txtindustry_name = resp.data.industry_name,

                unlockUI();
            });
            var url = 'api/Marketing/GetDocumentList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.lsfilename = resp.data.filename;
                $scope.lsfilepath = resp.data.filepath;
                $scope.document_list = resp.data.document_list;
            });
            var url = 'api/Marketing/GetMilletDocumentList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.lmfilename = resp.data.filename;
                $scope.lmfilepath = resp.data.filepath;
                $scope.milletdocument_list = resp.data.milletdocument_list;
            });
            var url = 'api/Marketing/GetEnquiryDocumentList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.lmfilename = resp.data.filename;
                $scope.lmfilepath = resp.data.filepath;
                $scope.enquirydocument_list = resp.data.enquirydocument_list;
            });
        }
        $scope.document_downloads = function (val1,val2) {
            DownloaddocumentService.Downloaddocument(val1,val2);
        }
        $scope.download_all = function (val1, val2) {

            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }

        }
        $scope.download_allmillet = function (val1, val2) {
            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }
        }
        $scope.milletdocument_downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.download_allenquiry = function (val1, val2) {

            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }

        }
        $scope.enquirydocument_downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.documentviewerenquiry = function (val1, val2) {
            lockUI();
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
            if (IsValidExtension == false) {
                Notify.alert("View is not supported for this format..!", {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                unlockUI();
                return false;
            }
            DownloaddocumentService.DocumentViewer(val1, val2);
        }
        $scope.documentviewer = function (val1, val2) {
            lockUI();
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
            if (IsValidExtension == false) {
            Notify.alert("View is not supported for this format..!", {
             status: 'danger',
             pos: 'top-center',
             timeout: 3000
             });
             unlockUI();
           return false;
             }
             DownloaddocumentService.DocumentViewer(val1, val2);
             }

             $scope.documentviewermillet = function (val1, val2) {
                lockUI();
                var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
                if (IsValidExtension == false) {
                Notify.alert("View is not supported for this format..!", {
                 status: 'danger',
                 pos: 'top-center',
                 timeout: 3000
                 });
                 unlockUI();
               return false;
                 }
                 DownloaddocumentService.DocumentViewer(val1, val2);
                 }

        $scope.Back = function () {
            $location.url("app/MstMarketingTaggedMemberSummary");
        }

       
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstMarketingTelecallingFunctionController', MstMarketingTelecallingFunctionController);

    MstMarketingTelecallingFunctionController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstMarketingTelecallingFunctionController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstMarketingTelecallingFunctionController';

        activate();

        function activate() {          
            var url = 'api/MstMarketingTelecallingFunction/GetMarketingTelecallingFunction';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.marketingtelecallingfunction_list = resp.data.marketingtelecallingfunction_list;
                unlockUI();
            });
        }
        $scope.addtelecallingfunction = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addtcfunction.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                               
                $scope.back = function () {
                    $modalInstance.close('closed');
                };
                $scope.submit = function () {
                    var params = {
                        marketingtelecallingfunction_name: $scope.txttcfunction_name,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code
                    }
                    var url = 'api/MstMarketingTelecallingFunction/CreateMarketingTelecallingFunction';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                            activate();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                            activate();
                        }
                    });
                }
                
            }
        }
        $scope.edittelecallingfunction = function (marketingtelecallingfunction_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/edittcfunction.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.back = function () {
                    $modalInstance.close('closed');
                };
                var params = {
                    marketingtelecallingfunction_gid: marketingtelecallingfunction_gid
                }
                var url = 'api/MstMarketingTelecallingFunction/EditMarketingTelecallingFunction';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txtedittcfunction_name= resp.data.marketingtelecallingfunction_name;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.marketingtelecallingfunction_gid = resp.data.marketingtelecallingfunction_gid;
                });
                $scope.back = function () {
                    $modalInstance.close('closed');
                };
                $scope.update = function () {
                    var url = 'api/MstMarketingTelecallingFunction/UpdateMarketingTelecallingFunction';
                    var params = {
                        marketingtelecallingfunction_name: $scope.txtedittcfunction_name,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        marketingtelecallingfunction_gid: $scope.marketingtelecallingfunction_gid
                    }
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                        else {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                    });
                }
            }
        }

        $scope.Status_update = function (marketingtelecallingfunction_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statustcfunction.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    marketingtelecallingfunction_gid: marketingtelecallingfunction_gid
                }
                var url = 'api/MstMarketingTelecallingFunction/EditMarketingTelecallingFunction';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.marketingtelecallingfunction_gid = resp.data.marketingtelecallingfunction_gid
                    $scope.txttcfunction_name = resp.data.marketingtelecallingfunction_name;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        marketingtelecallingfunction_gid: marketingtelecallingfunction_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/MstMarketingTelecallingFunction/InactiveMarketingTelecallingFunction';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        } activate();
                    });

                    $modalInstance.close('closed');

                }
                var params = {
                    marketingtelecallingfunction_gid: marketingtelecallingfunction_gid
                }

                var url = 'api/MstMarketingTelecallingFunction/InactiveMarketingTelecallingFunctionHistory';

                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.telecallingfunctioninactivelog_data = resp.data.marketingtelecallinginactivehistory_list;
                    unlockUI();
                });
            }
        }

        $scope.delete = function (marketingtelecallingfunction_gid) {
            var params = {
                marketingtelecallingfunction_gid: marketingtelecallingfunction_gid
            }
            SweetAlert.swal({
                
 title: 'Are you sure?',
 text: 'Do You Want To Delete the Record ?',
 showSpinner: true,
// showCancelButton: true,
 CancelButtonColor: '#dedad9',
 showCancelButton: true,
 confirmButtonColor: '#d64b3c',
 confirmButtonText: 'Yes, delete it!',
//  showConfirmButton: true,
 closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    var url = 'api/MstMarketingTelecallingFunction/DeleteMarketingTelecallingFunction';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            SweetAlert.swal('Deleted Successfully!');
                            activate();
                        }
                        else if (resp.data.status == false) {
                            SweetAlert.swal({
                                title: 'Warning!',
                                text: "Can't able to delete Business Development Function because it is mapped to add Business Development call",
                                timer: 5000,                                       
                                showCancelButton: false,
                                showConfirmButton: false,                                        
                                backgroundcolor: '#d64b3c'
                        });
                            activate();
                            }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                    });
                }
            });
        }
    }
})();
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstMarketingTransferCallSummaryController', MstMarketingTransferCallSummaryController);

    MstMarketingTransferCallSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'cmnfunctionService'];

    function MstMarketingTransferCallSummaryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstMarketingTransferCallSummaryController';
        activate();

        function activate() {
            var url = 'api/Marketing/GetEmpTransferredMarketingCallSummary';
            SocketService.get(url).then(function (resp) {
                $scope.ibcalltransfer_list = resp.data.MarketingCall_list;
                unlockUI();
            });           

            var url = "api/Marketing/EmployeeMarketingCallCount";
            SocketService.get(url).then(function (resp) {
                $scope.assignedcall_count = resp.data.assignedcall_count;
                $scope.transfercall_count = resp.data.transfercall_count;
                $scope.completedcall_count = resp.data.completedcall_count;
                $scope.inprogresscall_count = resp.data.inprogresscall_count;
                $scope.tagmember_count = resp.data.taggedcall_count;
                $scope.followupcall_count = resp.data.followupcall_count;
                $scope.rejectedcall_count = resp.data.rejectedcall_count;
                $scope.closedcall_count = resp.data.closedcall_count;


                unlockUI();
            });
        }

        $scope.closedcall= function() {
            $location.url("app/MstMarketingMyleadsClosedCall");
        }
      
        $scope.myassigned_calls = function () {
            $location.url("app/MstMarketingMyAssignedCallSummary");
        }
        $scope.tag_member = function () {
            $location.url("app/MstMarketingTaggedMemberSummary");
        }
        $scope.transfer_calls = function () {
            $location.url("app/MstMarketingTransferCallSummary");
        }
        $scope.completed_calls = function () {
            $location.url("app/MstMarketingCompletedCallSummary");
        }
        $scope.view = function (marketingcall_gid) {
            $location.url("app/MstMarketingAssignedCallView?hash=" + cmnfunctionService.encryptURL("marketingcall_gid=" + marketingcall_gid + '&lspage=TransferCall'));
        }
        $scope.edit = function (marketingcall_gid) {
            $location.url("app/MstMarketingEdit?hash=" + cmnfunctionService.encryptURL("marketingcall_gid=" + marketingcall_gid));
        }
        $scope.work_inprogress = function () {
            $location.url("app/MstMarketingWorkInprogressCallSummary");
        }
        $scope.followup_call = function () {
            $location.url("app/MstMarketingFollowUpCallSummary");
        }
        $scope.mbdrejected_calls = function () {
            $location.url("app/MstMarketingMBDRejectedCallSummary");
        }
        $scope.transfer = function (marketingcall_gid) {

            var modalInstance = $modal.open({
                templateUrl: '/transferContent.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];
            function ModalInstanceCtrl($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {


                var params = {
                    marketingcall_gid: marketingcall_gid
                }
                var url = 'api/Marketing/MarketingCallDetailsForTransfer';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.marketingcall_gid = resp.data.marketingcall_gid;
                    $scope.ticket_refid = resp.data.ticket_refid;
                    $scope.assignemployee_gid = resp.data.assignemployee_gid;
                    $scope.assignemployee_name = resp.data.assignemployee_name;
                    $scope.ibcalltransfer_list = resp.data.MarketingCalltransfer_list;
                });

                var url = 'api/OsdTrnCustomerQueryMgmt/TransferEmployee';
                SocketService.get(url).then(function (resp) {
                    $scope.TransferEmployeeList = resp.data.TransferEmployeeList;
                    unlockUI();
                });

                /*      $scope.marketingcall_gid = marketingcall_gid;
                      $scope.ticketref_no = ticketref_no;
                      $scope.assigned_to = assigned_to; */

                $scope.transfer_call = function () {

                    if ($scope.cboTransferTo == undefined) {
                        modalInstance.close('closed');
                        Notify.alert('Kindly Select the Transfer to Person', 'warning');
                        return;
                    }

                    var params = {
                        marketingcall_gid: $scope.marketingcall_gid,
                        ticket_refid: $scope.ticket_refid,
                        transferfrom_gid: $scope.assignemployee_gid,
                        transferfrom_name: $scope.assignemployee_name,
                        transferto_gid: $scope.cboTransferTo,
                        transferto_name: $('#transfer_to :selected').text(),
                        transfer_remarks: $scope.transfer_remarks
                    }

                    var url = "api/Marketing/MarketingCallTransferEmployee";
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status = true) {
                            modalInstance.close('closed');
                            Notify.alert(resp.data.message, 'success');
                            activate();
                        }
                        else {
                            modalInstance.close('closed');
                            Notify.alert(resp.data.message, 'warning');
                            activate();
                        }
                    });


                }
                /*     var url = 'api/MstTelecalling/TransferLog';
                     var params = {
                         marketingcall_gid:marketingcall_gid
                     }
                     SocketService.getparams(url, params).then(function (resp) {
                             $scope.TransferLog = resp.data.TransferLog;
                     });  */
                $scope.close = function () {
                    modalInstance.close('closed');
                };
            }
        }
    }
})();
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstMarketingUnassignedLeadEditController', MstMarketingUnassignedLeadEditController);

    MstMarketingUnassignedLeadEditController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'DownloaddocumentService','cmnfunctionService'];

    function MstMarketingUnassignedLeadEditController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstMarketingUnassignedLeadEditController';
        //$scope.marketingcall_gid = $location.search().lsmarketingcall_gid;
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        $scope.marketingcall_gid = searchObject.lsmarketingcall_gid;
        activate();

        function activate() {
            $scope.cbotagemployee = [];
            vm.calender1 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open1 = true;
            };
            vm.calender2 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open2 = true;
            };
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };
            $scope.cbointernal_reference = "NA";
            var d = new Date();
            var time = d.toLocaleString([], { hour: '2-digit', minute: '2-digit' });


            var today = new Date();
            var date = 0 + today.getDate() + '-' + (today.getMonth() + 1) + '-' + today.getFullYear();
            var todaytime = date + ' ' + '/' + ' ' + time;
           // $scope.txtcallreceived_date = todaytime;

            $scope.minDate = new Date();
            var d = new Date();
            var time = d.toLocaleString([], { hour: '2-digit', minute: '2-digit' });

            $scope.txtcallreceived_time = time;


            var url = 'api/Marketing/MarketingCallTempClear';
            SocketService.get(url).then(function (resp) {
            });

            var param = {
                marketingcall_gid: $scope.marketingcall_gid
            };
            var url = 'api/MarMstLeadRequire/GetLeadRequire';
            SocketService.get(url).then(function (resp) {
                $scope.leadrequire_list = resp.data.leadrequire_list;
            });
            var url = 'api/MarMstMilletRequire/GetMilletRequire';
            SocketService.get(url).then(function (resp) {
                $scope.milletrequire_list = resp.data.milletrequire_list;
            });
            var url = 'api/MstEnquiryRequire/GetEnquiryRequire';
            SocketService.get(url).then(function (resp) {
                $scope.enquiryrequire_list = resp.data.enquiryrequire_list;
            });
            var url = 'api/MstStartupRequire/GetStartupRequire';
            SocketService.get(url).then(function (resp) {
                $scope.startuprequire_list = resp.data.startuprequire_list;
            });
            var url = 'api/Marketing/GetEntity';
            SocketService.get(url).then(function (resp) {
                $scope.entity_list = resp.data.inboundentity_list;
            });
            var url = 'api/employee/Employee';
            SocketService.get(url).then(function (resp) {
                $scope.employee_list = resp.data.employee_list;
            });
            var url = 'api/Marketing/GetMarketingSourceofContact';
            SocketService.get(url).then(function (resp) {
                $scope.sourceofcontact_list = resp.data.MarketingSourceofContact_list;
            });
            var url = 'api/Marketing/GetMarketingCallReceivedNumber';
            SocketService.get(url).then(function (resp) {
                $scope.callreceivednumber_list = resp.data.MarketingCallReceivedNumber_list;
            });
            var url = 'api/Marketing/GetMarketingCallType';
            SocketService.get(url).then(function (resp) {
                $scope.calltype_list = resp.data.MarketingCallType_list;
            });
            var url = 'api/Marketing/GetMarketingTelecallingFunction';
            SocketService.get(url).then(function (resp) {
                $scope.telecallingfunction_list = resp.data.MarketingTelecallingFunction_list;
            });
            var url = 'api/Marketing/GetLeadRequestType';
            SocketService.get(url).then(function (resp) {
                $scope.leadrequesttype_list = resp.data.leadrequest_list;
            });
            var url = 'api/Marketing/MarketingCallMobileNoList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.ibcallmobileno_list = resp.data.MarketingCallmobileno_list;
            });
            var url = 'api/Marketing/MarketingCallEmailList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.ibcallemail_list = resp.data.MarketingCallemail_list;
            });
            var url = 'api/Marketing/MarketingCallAddressList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.ibcalladdress_list = resp.data.MarketingCalladdress_list;
            });
            var url = 'api/Marketing/MarketingCallFollowUpList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.ibcallfollowup_list = resp.data.marketingcallfollowup_list;
            });
            var url = 'api/Marketing/GetDocumentList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.lsfilename = resp.data.filename;
                $scope.lsfilepath = resp.data.filepath;
                $scope.document_list = resp.data.document_list;
            });
            var url = 'api/Marketing/GetMilletDocumentList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.lmfilename = resp.data.filename;
                $scope.lmfilepath = resp.data.filepath;
                $scope.milletdocument_list = resp.data.milletdocument_list;
            });
            var url = 'api/Marketing/GetEnquiryDocumentList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.lmfilename = resp.data.filename;
                $scope.lmfilepath = resp.data.filepath;
                $scope.enquirydocument_list = resp.data.enquirydocument_list;
            });
            var url = 'api/Marketing/EditMarketingCall';

            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                $scope.txtticket_refid = resp.data.ticket_refid;
                $scope.cboentity = resp.data.entity_gid;
                $scope.cbosourceofcontact = resp.data.marketingsourceofcontact_gid;
                $scope.cboleadrequirename = resp.data.leadrequire_gid;
                $scope.cbomilletrequirename = resp.data.milletrequire_gid;
                $scope.cboenquiryrequirename = resp.data.enquiryrequire_gid;
                $scope.cbostartuprequirename = resp.data.startuprequire_gid;
                $scope.cboleadrequesttype = resp.data.leadrequesttype_gid;
                $scope.cbocallreceivednumber = resp.data.marketingcallreceivednumber_gid;
                $scope.cbocustomer_type = resp.data.customer_type;
                $scope.txtcallreceived_date = resp.data.created_date;
                $scope.origination = resp.data.origination;
                $scope.txtcaller_name = resp.data.caller_name;
                $scope.txtcompany_name = resp.data.company_name;
                $scope.txtindustry_name = resp.data.industry_name;
                $scope.txtbusiness_name = resp.data.business_name;
                $scope.txtmessage_name = resp.data.message_name;
                $scope.txtyour_name = resp.data.your_name;
                //$scope.cbointernalreference = resp.data.internalreference_gid;
                $scope.txtcallerassociate_company = resp.data.callerassociate_company;
                $scope.txtoffice_landlineno = resp.data.office_landlineno;
                $scope.txtbase_location = resp.data.baselocation_name;
                $scope.cbocalltype = resp.data.marketingcalltype_gid;
                $scope.cbofunction = resp.data.marketingfunction_gid;
                $scope.txtfunction_remarks = resp.data.function_remarks;
                $scope.txtrequirement = resp.data.requirement;
                $scope.txtenquiry_description = resp.data.enquiry_description;
                $scope.cbocallclosure_status = resp.data.callclosure_status;
                //$scope.cboassignemployee = resp.data.assignemployee_gid;
                $scope.txttat_hours = resp.data.tat_hours;
                //$scope.emp_list = resp.data.emp_list;
                //if (resp.data.tagemployee_list != null) {
                //    var count = resp.data.tagemployee_list.length;
                //    for (var i = 0; i < count; i++) {
                //        var workerIndex = $scope.emp_list.map(function (x) { return x.employee_gid; }).indexOf(resp.data.tagemployee_list[i].employee_gid);
                //        $scope.cbotagemployee.push($scope.emp_list[workerIndex]);
                //        $scope.$parent.cbotagemployee = $scope.cbotagemployee;
                //    }
                //}



                $scope.txtassignclosure_remarks = resp.data.assignclosure_remarks;

               

                //if (resp.data.marketingcall_status == "Incomplete") {
                //    $scope.ibcallSubmit = true;
                //    $scope.ibcallUpdate = false;
                //}
                //else {
                //    $scope.ibcallSubmit = false;
                //    $scope.ibcallUpdate = true;
                //}

                //if (resp.data.function_name == 'Others') {
                //    $scope.function_show = true;
                //}
                //else {
                //    $scope.function_show = false;
                //}

                unlockUI();
            });
        

        }

        $scope.changefunctionstatus = function (Marketingfunction_name) {
            if ($('#function :selected').text() == 'Others') {
                $scope.function_show = true;
            }
            else {
                $scope.function_show = false;
            }
        }
        $scope.auditname_change = function (cboassignemployee) {
            var params = {
                employee_gid: $scope.cboassignemployee
            }
            var url = 'api/Marketing/GetBaselocation';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.employee_gid = resp.data.employee_gid;
                $scope.txtbase_location = resp.data.baselocation_name;

            });

        }
        $scope.download_allmillet = function (val1,val2) {
            for (var i = 0; i < val2.length; i++) {
               //  console.log(array[i]);
               DownloaddocumentService.Downloaddocument(val1, val2[i]);
           }
       }        
       $scope.milletdocument_downloads = function (val1,val2) {
           DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.download_allenquiry = function (val1, val2) {
            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }
        }
        $scope.enquirydocument_downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
       

       $scope.documentviewer = function (val1, val2) {
        lockUI();
        var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
        if (IsValidExtension == false) {
        Notify.alert("View is not supported for this format..!", {
         status: 'danger',
         pos: 'top-center',
         timeout: 3000
         });
         unlockUI();
       return false;
         }
         DownloaddocumentService.DocumentViewer(val1, val2);
         }

         $scope.documentviewermillet = function (val1, val2) {
            lockUI();
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
            if (IsValidExtension == false) {
            Notify.alert("View is not supported for this format..!", {
             status: 'danger',
             pos: 'top-center',
             timeout: 3000
             });
             unlockUI();
           return false;
             }
             DownloaddocumentService.DocumentViewer(val1, val2);
        }
        $scope.documentviewerenquiry = function (val1, val2) {
            lockUI();
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
            if (IsValidExtension == false) {
                Notify.alert("View is not supported for this format..!", {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                unlockUI();
                return false;
            }
            DownloaddocumentService.DocumentViewer(val1, val2);
        }

        $scope.save = function () {

            var entity_Name = $('#entity :selected').text();
            var sourceofcontact_Name = $('#sourceofcontact :selected').text();
            var leadrequesttype = $('#leadrequesttype :selected').text();
            var callreceivednumber_Name = $('#callreceivednumber :selected').text();
            var internalreference_Name = $('#internalreference :selected').text();
            var calltype_Name = $('#calltype :selected').text();
            var function_Name = $('#function :selected').text();
            var assignemployee_Name = $('#assignemployee :selected').text();
            var leadrequire_name = $('#leadrequire :selected').text();

          
            var tagemployee = $scope.cbotagemployee;
            if (tagemployee == [undefined]) {
                var cbotagemployee = null;
            }
            else {
                var cbotagemployee = $scope.cbotagemployee;
            }
            var params = {
                entity_name: entity_Name,
                entity_gid: $scope.cboentity,
                marketingsourceofcontact_name: sourceofcontact_Name,
                marketingsourceofcontact_gid: $scope.cbosourceofcontact,
                leadrequire_name: leadrequire_name,
                leadrequire_gid: $scope.cboleadrequirename,
                marketingcallreceivednumber_name: callreceivednumber_Name,
                marketingcallreceivednumber_gid: $scope.cbocallreceivednumber,
                leadrequesttype_name: leadrequesttype,
                leadrequesttype_gid: $scope.cboleadrequesttype,
                customer_type: $scope.cbocustomer_type,
                callreceived_date: $scope.txtcallreceived_date,
                caller_name: $scope.txtcaller_name,
                your_name: $scope.txtyour_name,
                industry_name: $scope.txtindustry_name,
                company_name: $scope.txtcompany_name,
                message_name: $scope.txtmessage_name,
                internalreference_name: internalreference_Name,
                internalreference_gid: $scope.cbointernalreference,
                callerassociate_company: $scope.txtcallerassociate_company,
                office_landlineno: $scope.txtoffice_landlineno,
                marketingcalltype_name: calltype_Name,
                marketingcalltype_gid: $scope.cbocalltype,
                marketingfunction_name: function_Name,
                marketingfunction_gid: $scope.cbofunction,
                function_remarks: $scope.txtfunction_remarks,
                requirement: $scope.txtrequirement,
                enquiry_description: $scope.txtenquiry_description,
                callclosure_status: $scope.cbocallclosure_status,
                assignemployee_name: assignemployee_Name,
                assignemployee_gid: $scope.cboassignemployee,
                tat_hours: $scope.txttat_hours,
                baselocation_name: $scope.txtbase_location,
                tagemployee_list: $scope.cbotagemployee,
                assignclosure_remarks: $scope.txtassignclosure_remarks,
                marketingcall_gid: $scope.marketingcall_gid
            }
            var url = 'api/Marketing/MarketingCallEditSave';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $location.url("app/MstMarketingSummary");
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
       
        $scope.submit = function () {

            var entity_Name = $('#entity :selected').text();
            var sourceofcontact_Name = $('#sourceofcontact :selected').text();
            var callreceivednumber_Name = $('#callreceivednumber :selected').text();
            var leadrequesttype = $('#leadrequesttype :selected').text();
            var internalreference_Name = $('#internalreference :selected').text();
            var calltype_Name = $('#calltype :selected').text();
            var function_Name = $('#function :selected').text();
            var assignemployee_Name = $('#assignemployee :selected').text();
            var leadrequire_name = $('#leadrequire :selected').text();
            var milletrequire_name = $('#milletrequire :selected').text();


            var tagemployee = $scope.cbotagemployee;
            if (tagemployee == [undefined]) {
                var cbotagemployee = null;
            }
            else {
                var cbotagemployee = $scope.cbotagemployee;
            }
            var params = {
                entity_name: entity_Name,
                entity_gid: $scope.cboentity,
                marketingsourceofcontact_name: sourceofcontact_Name,
                marketingsourceofcontact_gid: $scope.cbosourceofcontact,
                leadrequire_name: leadrequire_name,
                leadrequire_gid: $scope.cboleadrequirename,
                milletrequire_name: milletrequire_name,
                milletrequire_gid: $scope.cbomilletrequirename,
                marketingcallreceivednumber_name: callreceivednumber_Name,
                marketingcallreceivednumber_gid: $scope.cbocallreceivednumber,
                leadrequesttype_name: leadrequesttype,
                leadrequesttype_gid: $scope.cboleadrequesttype,
                customer_type: $scope.cbocustomer_type,
                callreceived_date: $scope.txtcallreceived_date,
                caller_name: $scope.txtcaller_name,
                your_name: $scope.txtyour_name,
                industry_name: $scope.txtindustry_name,
                company_name: $scope.txtcompany_name,
                message_name: $scope.txtmessage_name,
                internalreference_name: internalreference_Name,
                internalreference_gid: $scope.cbointernalreference,
                callerassociate_company: $scope.txtcallerassociate_company,
                office_landlineno: $scope.txtoffice_landlineno,
                marketingcalltype_name: calltype_Name,
                marketingcalltype_gid: $scope.cbocalltype,
                marketingfunction_name: function_Name,
                marketingfunction_gid: $scope.cbofunction,
                function_remarks: $scope.txtfunction_remarks,
                requirement: $scope.txtrequirement,
                enquiry_description: $scope.txtenquiry_description,
                callclosure_status: $scope.cbocallclosure_status,
                assignemployee_name: assignemployee_Name,
                assignemployee_gid: $scope.cboassignemployee,
                tat_hours: $scope.txttat_hours,
                baselocation_name: $scope.txtbase_location,
                tagemployee_list: $scope.cbotagemployee,
                assignclosure_remarks: $scope.txtassignclosure_remarks,
                marketingcall_gid: $scope.marketingcall_gid
            }
            var url = 'api/Marketing/MarketingCallEditSubmit';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $location.url("app/MstMarketingSummary");
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
        $scope.update = function(){
            var lsentity_name = '';
            var lsentity_gid = '';
            var lssourceofcontact_name = '';
            var lssourceofcontact_gid = '';
            var lsleadrequesttype_name = '';
            var lsleadrequesttype_gid = '';
            var lscallreceivednumber_name = '';
            var lscallreceivednumber_gid = '';
            var lsinternalreference_name = '';
            var lsinternalreference_gid = '';
            var lscalltype_name = '';
            var lscalltype_gid = '';
            var lsmilletrequire_name = '';
            var lsmilletrequire_gid = '';
            var lsleadrequire_name = '';
            var lsleadrequire_gid = '';
            var lsenquiryrequire_name = '';
            var lsenquiryrequire_gid = '';
            var lsstartuprequire_name = '';
            var lsstartuprequire_gid = '';
            var lsassignemployee_name = '';
            var lsassignemployee_gid = '';
            var lstagemployee_name = '';
            //var lsmarketingcall_gid = $location.search().lsmarketingcall_gid;
            
            if ($scope.cboentity != undefined || $scope.cboentity != null) {
                lsentity_name = $('#entity :selected').text();
                lsentity_gid = $scope.cboentity;
            }
            if ($scope.cbosourceofcontact != undefined || $scope.cbosourceofcontact != null) {
                lssourceofcontact_name = $('#sourceofcontact :selected').text();
                lssourceofcontact_gid = $scope.cbosourceofcontact;
            }
            if ($scope.cbocallreceivednumber != undefined || $scope.cbocallreceivednumber != null) {
                lscallreceivednumber_name = $('#callreceivednumber :selected').text();
                lscallreceivednumber_gid = $scope.cbocallreceivednumber;
            }
            if ($scope.cboleadrequesttype != undefined || $scope.cboleadrequesttype != null) {
                lsleadrequesttype_name = $('#leadrequesttype :selected').text();
                lsleadrequesttype_gid = $scope.cboleadrequesttype;
            }
            if ($scope.cbointernalreference != undefined || $scope.cbointernalreference != null) {
                lsinternalreference_name = $('#internalreference :selected').text();
                lsinternalreference_gid = $scope.cbointernalreference;
            }
            if ($scope.cbointernalreference == undefined || $scope.cbointernalreference == null) {
                lsinternalreference_name = 'NA';
                lsinternalreference_gid = 'gid';
            }
            if ($scope.cbocalltype != undefined || $scope.cbocalltype != null) {
                lscalltype_name = $('#call_type :selected').text();
                lscalltype_gid = $scope.cbocalltype;
            }
            if ($scope.cboleadrequirename != undefined || $scope.cboleadrequirename != null) {
                lsleadrequire_name = $('#leadrequire :selected').text();
                lsleadrequire_gid = $scope.cboleadrequirename;
            }
            if ($scope.cbomilletrequirename != undefined || $scope.cbomilletrequirename != null) {
                lsmilletrequire_name = $('#milletrequire :selected').text();
                lsmilletrequire_gid = $scope.cbomilletrequirename;
            }
            if ($scope.cboenquiryrequirename != undefined || $scope.cboenquiryrequirename != null) {
                lsenquiryrequire_name = $('#enquiryrequire :selected').text();
                lsenquiryrequire_gid = $scope.cboenquiryrequirename;
            }
            if ($scope.cbostartuprequirename != undefined || $scope.cbostartuprequirename != null) {
                lsstartuprequire_name = $('#startuprequire :selected').text();
                lsstartuprequire_gid = $scope.cbostartuprequirename;
            }
            if ($scope.cboassignemployee != undefined || $scope.cboassignemployee != null) {
                lsassignemployee_name = $('#assignemployee :selected').text();
                lsassignemployee_gid = $scope.cboassignemployee;
            }

            if (($scope.cbocallclosure_status == 'Assign') && ($scope.cboassignemployee == null || $scope.cboassignemployee == '' )) {
                Notify.alert('Kindly Select Assign Employee Name', 'warning')
            }
            //else if (($scope.cbocallclosure_status == 'Assign') && ($scope.txttat_hours == null || $scope.txttat_hours == '')) {
            //    Notify.alert('Kindly Select Assign TAT Hours', 'warning')
            //}
            if ((($scope.cbocallclosure_status == 'Assign') || ($scope.cbocallclosure_status == 'Follow Up')) && ($scope.txtassignclosure_remarks == null || $scope.txtassignclosure_remarks == '')) {
                Notify.alert('Kindly Enter Remark', 'warning')
            }
            else if (($scope.cbocallclosure_status == 'Rejected') && ($scope.txtclosure_remarks == null || $scope.txtclosure_remarks == '')) {
                Notify.alert('Kindly Enter Remark', 'warning')
            }

            else {
                var params = {
                    entity_name: lsentity_name,
                    entity_gid: lsentity_gid,
                    ticket_refid: $scope.txtticket_refid,
                    marketingcall_gid: $scope.marketingcall_gid,
                    marketingsourceofcontact_name: lssourceofcontact_name,
                    marketingsourceofcontact_gid: lssourceofcontact_gid,
                    marketingcallreceivednumber_name: lscallreceivednumber_name,
                    marketingcallreceivednumber_gid: lscallreceivednumber_gid,
                    leadrequesttype_name: lsleadrequesttype_name,
                    leadrequesttype_gid: lsleadrequesttype_gid,
                    customer_type: $scope.cbocustomertype,
                    callreceived_date: $scope.txtcallreceived_date,
                    callreceived_time: $scope.txtcallreceived_time,
                    caller_name: $scope.txtcaller_name,
                    internalreference_name: lsinternalreference_name,
                    internalreference_gid: lsinternalreference_gid,
                    callerassociate_company: $scope.txtcallerassociate_company,
                    office_landlineno: $scope.txtoffice_landlineno,
                    marketingcalltype_name: lscalltype_name,
                    marketingcalltype_gid: lscalltype_gid,
                    leadrequire_name: lsleadrequire_name,
                    leadrequire_gid: lsleadrequire_gid,
                    milletrequire_name: lsmilletrequire_name,
                    milletrequire_gid: lsmilletrequire_gid,
                    enquiryrequire_name: lsenquiryrequire_name,
                    enquiryrequire_gid: lsenquiryrequire_gid,
                    startuprequire_name: lsstartuprequire_name,
                    startuprequire_gid: lsstartuprequire_gid,
                    business_name: $scope.txtbusiness_name,
                    industry_name: $scope.txtindustry_name,
                    function_remarks: $scope.txtfunction_remarks,
                    requirement: $scope.txtrequirement,
                    enquiry_description: $scope.txtenquiry_description,
                    callclosure_status: $scope.cbocallclosure_status,
                    assignemployee_name: lsassignemployee_name,
                    assignemployee_gid: lsassignemployee_gid,
                    baselocation_name: $scope.txtbase_location,
                    tat_hours: $scope.txttat_hours,
                    tagemployee_list: $scope.cbotagemployee,
                    assignclosure_remarks: $scope.txtassignclosure_remarks,
                    closed_remarks: $scope.txtclosure_remarks
                }
                var url = 'api/Marketing/MarketingCallFormUpdate';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $location.url("app/MstMarketingUnassignedLeadSummary");
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
       $scope.rejected = function () {

            var modalInstance = $modal.open({
                templateUrl: '/rejectapproval.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.ok = function () {
                    modalInstance.close('closed');
                };
                $scope.rejectSubmit = function () {
                    var params = {
 
                     /*   marketingcall_gid: $scope.marketingcall_gid,*/

                    //    $scope.marketingcall_gid = $location.search().lsmarketingcall_gid;

                        marketingcall_gid: $location.search().lsmarketingcall_gid,

                        rejected_remarks: $scope.txtreject_remarks,
                    }
                    var url = 'api/Marketing/MarketingRejected';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            modalInstance.close('closed');
                            $state.go("app.MstMarketingUnassignedLeadSummary");
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            modalInstance.close('closed');
                        }
                    });
                    $state.go("app.MstMarketingUnassignedLeadSummary");
                }
            }

        }



        //Address Multiple Add

        $scope.address_add = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addaddress.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.geocodingFailed = false;

                var url = 'api/AddressType/GetAddressTypeASC';
                SocketService.get(url).then(function (resp) {
                    $scope.addresstype_list = resp.data.addresstype_list;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.onchangepostal_code = function () {
                    var params = {
                        postal_code: $scope.txtpostal_code
                    }
                    var url = 'api/Mstbuyer/GetPostalCodeDetails';

                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.txtcity = resp.data.city;
                        $scope.txttaluka = resp.data.taluka;
                        $scope.txtdistrict = resp.data.district;
                        $scope.txtstate = resp.data.state_name;
                    });
                }

                $scope.getGeoCoding = function () {
                    if($scope.txtpostal_code == undefined || $scope.txtpostal_code.length < 6){

                        $scope.txtlatitude = '';

                        $scope.txtlongitude = '';

                    }
                    else
                    {
                        if ($scope.txtaddressline2 == undefined) {
                            var addressString = ''.concat($scope.txtaddressline1.toString(), ",", $scope.txtpostal_code.toString());
                        } else {
                            var addressString = ''.concat($scope.txtaddressline1.toString(), ",", $scope.txtaddressline2.toString(), ",", $scope.txtpostal_code.toString());
                        }
                        var params = {
                            address: addressString
                        }
                        var url = 'api/GoogleMapsAPI/GetGeoCoding';
                        SocketService.getparams(url, params).then(function (resp) {
                            if (resp.data.status == "OK") {
                                $scope.txtlatitude = resp.data.results[0].geometry.location.lat.toString();
                                $scope.txtlongitude = resp.data.results[0].geometry.location.lng.toString();
                                $scope.geocodingFailed = false;
                            }
                            else if (resp.data.status == "ZERO_RESULTS") {
                                $scope.geocodingFailed = true;
                            }
                        });
                    }
                }

                $scope.txtcountry = "India";
                $scope.addressSubmit = function () {

                    var params = {
                        addresstype_gid: $scope.cboaddresstype.address_gid,
                        addresstype_name: $scope.cboaddresstype.address_type,
                        primary_status: $scope.rdbprimaryaddress,
                        addressline1: $scope.txtaddressline1,
                        addressline2: $scope.txtaddressline2,
                        postal_code: $scope.txtpostal_code,
                        landmark: $scope.txtLand_Mark,
                        taluka: $scope.txttaluka,
                        city: $scope.txtcity,
                        state: $scope.txtstate,
                        district: $scope.txtdistrict,
                        country: $scope.txtcountry,
                        latitude: $scope.txtlatitude,
                        longitude: $scope.txtlongitude,
                        marketingcall_gid: $location.search().lsmarketingcall_gid,
                    }
                    var url = 'api/Marketing/PostMarketingCallAddress';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            address_templist();
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
        $scope.address_delete = function (marketingcall2address_gid) {
            var params =
                {
                    marketingcall2address_gid: marketingcall2address_gid
                }
            var url = 'api/Marketing/MarketingCallAddressDelete';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    address_templist();
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
        

        function address_templist() {
            var param = {
                marketingcall_gid: $scope.marketingcall_gid
            };
            var url = 'api/Marketing/MarketingCallAddressTempList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.ibcalladdress_list = resp.data.MarketingCalladdress_list;
            });
        }

        $scope.address_edit = function (marketingcall2address_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editinboundcalladdress.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var url = 'api/AddressType/GetAddressTypeASC';
                SocketService.get(url).then(function (resp) {
                    $scope.addresstype_list = resp.data.addresstype_list;
                });

                var params = {
                    inboundcall2address_gid: inboundcall2address_gid
                }
                var url = 'api/Marketing/EditMarketingCallAddress';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.cboAddressType = resp.data.addresstype_gid;

                    $scope.rdbprimary_status = resp.data.primary_status;
                    $scope.txtaddressline1 = resp.data.addressline1;
                    $scope.txtaddressline2 = resp.data.addressline2;
                    $scope.txtlandmark = resp.data.landmark;
                    $scope.txtpostal_code = resp.data.postal_code;
                    $scope.txtcity = resp.data.city;
                    $scope.txttaluka = resp.data.taluka;
                    $scope.txtdistrict = resp.data.district;
                    $scope.txtstate = resp.data.state;
                    $scope.txtcountry = resp.data.country;
                    $scope.txtlatitude = resp.data.latitude;
                    $scope.txtlongitude = resp.data.longitude;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.txtcountry = "India";
                $scope.address_update = function () {
                    var address_type = $('#address_type :selected').text();

                    var params = {
                        addresstype_gid: $scope.cboAddressType,
                        addresstype_name: address_type,
                        primary_status: $scope.rdbprimary_status,
                        addressline1: $scope.txtaddressline1,
                        addressline2: $scope.txtaddressline2,
                        postal_code: $scope.txtpostal_code,
                        taluka: $scope.txttaluka,
                        city: $scope.txtcity,
                        district: $scope.txtdistrict,
                        state: $scope.txtstate,
                        country: $scope.txtcountry,
                        latitude: $scope.txtlatitude,
                        longitude: $scope.txtlongitude,
                        inboundcall2address_gid: inboundcall2address_gid,
                        marketingcall_gid: $scope.marketingcall_gid,
                    }
                    var url = 'api/Marketing/UpdateMarketingCallAddress';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            address_templist();
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
        $scope.StaticMapAndPhotos_View = function (latitude, longitude, addressline1, addressline2, postal_code) {
            var modalInstance = $modal.open({
                templateUrl: '/StaticMapAndPhotosView.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    latitude: latitude,
                    longitude: longitude
                }
                var url = 'api/GoogleMapsAPI/GetStaticMapUrl';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.staticmapImgUrl = resp.data;
                });
                if (addressline2 == '') {
                    var addressString = ''.concat(addressline1.toString(), ",", postal_code.toString());
                } else {
                    var addressString = ''.concat(addressline1.toString(), ",", addressline2.toString(), ",", postal_code.toString());
                }
                var params = {
                    address: addressString
                }
                var url = 'api/GoogleMapsAPI/GetPlaceImage';
                SocketService.getparams(url, params).then(function (resp) {
                    var photoUrlArray = [];
                    for (var i = 0; i < resp.data.length; i++) {
                        if (resp.data[i] != null) {
                            photoUrlArray[i] = resp.data[i];
                        }
                    }
                    if (photoUrlArray.length == 0) {
                        $scope.photoNotFound = true;
                    } else {
                        $scope.photoUrlList = photoUrlArray;
                        $scope.photoFound = true;
                    }
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        //Mobile Number Multiple Add
        $scope.add_mobileno = function () {
            if (($scope.txtmobile_no == undefined) || ($scope.txtmobile_no == '') || ($scope.rdbprimary_status == undefined) || ($scope.rdbwhatsapp_status == undefined) || ($scope.rdbsms_to == undefined) || ($scope.rdbprimary_status == '') || ($scope.rdbwhatsapp_status == '') || ($scope.rdbsms_to == '')) {
                Notify.alert('Enter Mobile Number / Select Primary Status', 'warning');
            }
            else if ($scope.txtmobile_no.length < 10) {
                Notify.alert('Enter 10 Digit Mobile Number', 'warning');
            }
            else {
                var params = {
                    mobile_no: $scope.txtmobile_no,
                    primary_status: $scope.rdbprimary_status,
                    whatsapp_status: $scope.rdbwhatsapp_status,
                    sms_to: $scope.rdbsms_to,
                    marketingcall_gid: $location.search().lsmarketingcall_gid,
                }
                var url = 'api/Marketing/PostMarketingCallMobileNo';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        mobileno_templist();
                        $scope.txtmobile_no = '';
                        $scope.rdbprimary_status = '';
                        $scope.rdbprimary_status == false;
                        $scope.rdbsms_to = '';
                        $scope.rdbsms_to == false;
                        $scope.rdbwhatsapp_status = '';
                        $scope.rdbwhatsapp_status == false;
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
        $scope.delete_mobileno = function (marketingcall2mobileno_gid) {
            var params = {
                marketingcall2mobileno_gid: marketingcall2mobileno_gid
            }
            var url = 'api/Marketing/MarketingCallMobileNoDelete';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                mobileno_templist();
            });
        }
        function mobileno_templist() {
            var param = {
                marketingcall_gid: $scope.marketingcall_gid
            };
            var url = 'api/Marketing/MarketingCallMobileNoTempList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.ibcallmobileno_list = resp.data.MarketingCallmobileno_list;
            });            
        }
        $scope.edit_mobileno = function (marketingcall2mobileno_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editinboundcallmobileno.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    marketingcall2mobileno_gid: marketingcall2mobileno_gid
                }
                var url = 'api/Marketing/EditMarketingCallMobileNo';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditmobile_no = resp.data.mobile_no;
                    $scope.rdbeditprimary_status = resp.data.primary_status;
                    $scope.rdbeditwhatsapp_status = resp.data.whatsapp_status;
                    $scope.rdbeditsms_to = resp.data.sms_to;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update_mobileno = function () {

                    var params = {
                        mobile_no: $scope.txteditmobile_no,
                        primary_status: $scope.rdbeditprimary_status,
                        whatsapp_status: $scope.rdbeditwhatsapp_status,
                        sms_to: $scope.rdbeditsms_to,
                        marketingcall2mobileno_gid: marketingcall2mobileno_gid,
                        marketingcall_gid: $scope.marketingcall_gid,
                    }
                    var url = 'api/Marketing/UpdateMarketingCallMobileNo';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        mobileno_templist();
                    });

                    $modalInstance.close('closed');

                }
            }
        }
        //Email Address Multiple Add
        $scope.add_emailaddress = function () {
            if (($scope.txtemail_address == undefined) || ($scope.txtemail_address == '') || ($scope.rdbprimary_email == undefined) || ($scope.rdbprimary_email == '')) {
                Notify.alert('Enter Email Address/Select Status', 'warning');
            }
            else {
                var params = {
                    email_address: $scope.txtemail_address,
                    primary_status: $scope.rdbprimary_email,
                    marketingcall_gid: $location.search().lsmarketingcall_gid,
                }
                var url = 'api/Marketing/PostMarketingCallEmail';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        email_templist();
                        $scope.txtemail_address = '';
                        $scope.rdbprimary_email = '';
                        $scope.rdbprimary_email == false;
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
        $scope.delete_emailaddress = function (marketingcall2email_gid) {
            var params = {
                marketingcall2email_gid: marketingcall2email_gid
            }
            var url = 'api/Marketing/MarketingCallEmailDelete';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                email_templist();
            });
        }
        

        function email_templist() {
            var param = {
                marketingcall_gid: $scope.marketingcall_gid
            };
            var url = 'api/Marketing/MarketingCallEmailTempList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.ibcallemail_list = resp.data.MarketingCallemail_list;
            });
        }

        $scope.edit_emailaddress = function (marketingcall2email_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editinboundcallemail.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    marketingcall2email_gid: marketingcall2email_gid
                }
                var url = 'api/Marketing/EditMarketingCallEmail';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txteditemail_address = resp.data.email_address;
                    $scope.rdbeditprimary_status = resp.data.primary_status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update_emailaddress = function () {

                    var params = {
                        email_address: $scope.txteditemail_address,
                        primary_status: $scope.rdbeditprimary_status,
                        inboundcall2email_gid: inboundcall2email_gid,
                        marketingcall_gid: $scope.marketingcall_gid,
                    }
                    var url = 'api/Marketing/UpdateMarketingCallEmail';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        email_templist();
                    });

                    $modalInstance.close('closed');

                }
            }
        }

        //Follow Up Multiple Add
        $scope.add_followup = function () {
            if (($scope.txtfollowup_date == undefined) || ($scope.txtfollowup_date == '') || ($scope.txtfollowup_time == undefined) || ($scope.txtfollowup_time == '')) {
                Notify.alert('Enter Follow Up Date/Follow Up Time', 'warning');
            }
            else {
                var params = {
                    followup_date: $scope.txtfollowup_date,
                    followup_time: $scope.txtfollowup_time,
                }
                var url = 'api/Marketing/PostMarketingCallFollowUp';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    $scope.txtfollowup_date = '';
                    followup_templist();
                    $scope.txtfollowup_date = '';
                    $scope.txtfollowup_time = '';
                });
            }
        }
        $scope.delete_followup = function (marketingcall2followup_gid) {
            var params = {
                marketingcall2followup_gid: marketingcall2followup_gid
            }
            var url = 'api/Marketing/MarketingCallFollowUpDelete';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                followup_templist();
            });
        }       

        function followup_templist() {
            var param = {
                marketingcall_gid: $scope.marketingcall_gid
            };
            var url = 'api/Marketing/MarketingCallFollowUpTempList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.ibcallfollowup_list = resp.data.MarketingCallfollowup_list;
            });
        }

        $scope.edit_followup = function (marketingcall2followup_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editinboundcallfollowup.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {                

                var params = {
                    marketingcall2followup_gid: marketingcall2followup_gid
                }
                var url = 'api/Marketing/EditMarketingCallFollowUp';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txteditfollowup_date = new Date(resp.data.followup_date);

                    if (resp.data.Tfollowup_time == '0001-01-01T00:00:00') {
                        $scope.txteditfollowup_time = '';
                    }
                    else {
                        $scope.txteditfollowup_time = new Date(resp.data.Tfollowup_time);
                    }

                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update_followup = function () {

                    var params = {
                        followup_date: $scope.txteditfollowup_date,
                        followup_time: $scope.txteditfollowup_time,
                        marketingcall2followup_gid: marketingcall2followup_gid,
                        marketingcall_gid: $scope.marketingcall_gid,
                    }
                    var url = 'api/Marketing/UpdateMarketingCallFollowUp';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        followup_templist();
                    });

                    $modalInstance.close('closed');

                }
            }
        }
        $scope.download_all = function (val1, val2) {

            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }

        }

        $scope.document_downloads = function (val1,val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }


        $scope.Back = function () {
            $location.url('app/MstMarketingUnassignedLeadSummary');
        }
    }
})();
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstMarketingUnassignedLeadSummaryController', MstMarketingUnassignedLeadSummaryController);

    MstMarketingUnassignedLeadSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'cmnfunctionService'];

    function MstMarketingUnassignedLeadSummaryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstMarketingUnassignedLeadSummaryController';
        activate();
        lockUI();
        function activate() {
           
            var url = "api/Marketing/MarketingCallCount";
            SocketService.get(url).then(function (resp) {
                $scope.unassignedcall_count = resp.data.unassignedcall_count;
                $scope.assignedcall_count = resp.data.assignedcall_count;
                $scope.completedcall_count = resp.data.completedcall_count;
                $scope.followupcall_count = resp.data.followupcall_count;
                $scope.closedcall_count = resp.data.closedcall_count;
                $scope.rejectedcall_count = resp.data.rejectedcall_count;
                unlockUI();
            });

            var url = 'api/Marketing/GetMarketingLeadSummary';
            SocketService.get(url).then(function (resp) {
                $scope.ibcall_list = resp.data.MarketingCall_list;
                unlockUI();
            });
        }
        $scope.addinbound = function () {
            $location.url("app/MstMarketingAdd");
        }
        $scope.closedcall= function() {
            $location.url("app/MstMarketingClosedCall");
        }
        $scope.followupcall= function() {
            $location.url("app/MstMarketingFollowupCall");
        }
        $scope.assignedcall= function() {
            $location.url("app/MstMarketingSummary");
        }
        $scope.completedcall= function() {
            $location.url("app/MstMarketingCompletedCall");
        }

        $scope.view_inboundcall = function (marketingcall_gid) {
            $location.url('app/MstMarketingUnassignedLeadView?hash=' + cmnfunctionService.encryptURL('lsmarketingcall_gid=' + marketingcall_gid + '&lspage=MarketingUnassignedLead'));
        }
        
        $scope.edit_uninboundcall = function (marketingcall_gid) {
            $location.url('app/MstMarketingUnassignedLeadEdit?hash=' + cmnfunctionService.encryptURL('lsmarketingcall_gid=' + marketingcall_gid));
        }


        $scope.rejectedcall = function () {
            $location.url("app/MstMarketingRejectedCallSummary");
        }

        $scope.transfer = function (marketingcall_gid) {

            var modalInstance = $modal.open({
                templateUrl: '/transferContent.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];
            function ModalInstanceCtrl($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {


                var params = {
                    marketingcall_gid: marketingcall_gid
                }
                var url = 'api/Marketing/MarketingCallDetailsForTransfer';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.marketingcall_gid = resp.data.marketingcall_gid;
                    $scope.ticket_refid = resp.data.ticket_refid;
                    $scope.assignemployee_gid = resp.data.assignemployee_gid;
                    $scope.assignemployee_name = resp.data.assignemployee_name;
                    $scope.ibcalltransfer_list = resp.data.MarketingCalltransfer_list;
                });

                var url = 'api/OsdTrnCustomerQueryMgmt/TransferEmployee';
                SocketService.get(url).then(function (resp) {
                    $scope.TransferEmployeeList = resp.data.TransferEmployeeList;
                    unlockUI();
                });

          /*      $scope.marketingcall_gid = marketingcall_gid;
                $scope.ticketref_no = ticketref_no;
                $scope.assigned_to = assigned_to; */

                $scope.transfer_call = function () {

                    if ($scope.cboTransferTo == undefined) {
                        modalInstance.close('closed');
                        Notify.alert('Kindly Select the Transfer to Person', 'warning');
                        return;
                    }

                    var params = {
                        marketingcall_gid: $scope.marketingcall_gid,
                        ticket_refid: $scope.ticket_refid,
                        transferfrom_gid: $scope.assignemployee_gid,
                        transferfrom_name: $scope.assignemployee_name,
                        transferto_gid: $scope.cboTransferTo,
                        transferto_name: $('#transfer_to :selected').text(),                       
                        transfer_remarks: $scope.transfer_remarks
                    }

                    var url = "api/Marketing/MarketingCallTransferEmployee";
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status = true) {
                            modalInstance.close('closed');
                            Notify.alert(resp.data.message, 'success');
                            activate();
                        }
                        else {
                            modalInstance.close('closed');
                            Notify.alert(resp.data.message, 'warning');
                            activate();
                        }
                    });
                    

                }
           /*     var url = 'api/MstTelecalling/TransferLog';
                var params = {
                    marketingcall_gid:marketingcall_gid
                }
                SocketService.getparams(url, params).then(function (resp) {
                        $scope.TransferLog = resp.data.TransferLog;
                });  */
                $scope.close = function () {
                    modalInstance.close('closed');
                };
            }
        }
        $scope.closed_call = function (marketingcall_gid) {

            var modalInstance = $modal.open({
                templateUrl: '/closedContent.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];
            function ModalInstanceCtrl($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {


                var params = {
                    marketingcall_gid: marketingcall_gid
                }
                
                $scope.close_call = function () {                   
                    var params = {
                        marketingcall_gid: marketingcall_gid,                       
                        closed_remarks: $scope.closed_remarks
                    }

                    var url = "api/Marketing/MarketingCallAssignedClosed";
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status = true) {
                            modalInstance.close('closed');
                            Notify.alert(resp.data.message, 'success');
                            activate();
                        }
                        else {
                            modalInstance.close('closed');
                            Notify.alert(resp.data.message, 'warning');
                            activate();
                        }
                    });


                }
                
                $scope.close = function () {
                    modalInstance.close('closed');
                };
            }
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstMarketingUnassignedLeadViewController', MstMarketingUnassignedLeadViewController);

    MstMarketingUnassignedLeadViewController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'DownloaddocumentService', 'cmnfunctionService'];

    function MstMarketingUnassignedLeadViewController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstMarketingUnassignedLeadViewController';
        
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        var marketingcall_gid = searchObject.lsmarketingcall_gid;
        $scope.lspage = searchObject.lspage;
        var lspage = $scope.lspage;

        activate();
        function activate() {
            var params = {
                marketingcall_gid: marketingcall_gid
            }
            var url = 'api/Marketing/GetMarketingCallAssignedView';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtticket_refid = resp.data.ticket_refid,
                    $scope.txtentity_name = resp.data.entity_name,
                    $scope.txtsourceofcontact_name = resp.data.sourceofcontact_name,
                    $scope.txtcallreceivednumber_name = resp.data.callreceivednumber_name,
                    $scope.txtcustomer_type = resp.data.leadrequesttype_name,
                    $scope.txtcallreceived_date = resp.data.callreceived_date,
                    $scope.txtcaller_name = resp.data.caller_name,
                    $scope.txtinternalreference_name = resp.data.internalreference_name,
                    $scope.txtcallerassociate_company = resp.data.callerassociate_company,
                    $scope.txtoffice_landlineno = resp.data.office_landlineno,
                    $scope.txtcalltype_name = resp.data.calltype_name,
                    $scope.txtfunction_name = resp.data.function_name,
                    $scope.txtfunction_remarks = resp.data.function_remarks,
                    $scope.txttat_hours = resp.data.tat_hours,
                    $scope.txtrequirement = resp.data.requirement,
                    $scope.ibcalltransfer_list = resp.data.MarketingCalltransfer_list,//
                    $scope.txtenquiry_description = resp.data.enquiry_description,
                    $scope.txtcallclosure_status = resp.data.callclosure_status,
                    $scope.txtassignemployee_name = resp.data.assignemployee_name,
                    $scope.txtassign_date = resp.data.assign_date,
                    $scope.txtbase_location = resp.data.baselocation_name,
                    $scope.txttagemployee_name = resp.data.tagemployee_name,
                    $scope.txtassignclosure_remarks = resp.data.assignclosure_remarks,
                    $scope.ibcallextendfollowup_list = resp.data.marketingcallextendfollowup_list,
                    $scope.txtprimary_mobileno = resp.data.primary_mobileno,
                    $scope.ibcallmobileno_list = resp.data.MarketingCallmobileno_list;
                $scope.txtprimary_email = resp.data.primary_email,
                    $scope.ibcallemail_list = resp.data.MarketingCallemail_list;
                $scope.ibcallfollowup_list = resp.data.MarketingCallfollowup_list;
                $scope.ibcalladdress_list = resp.data.MarketingCalladdress_list;
                $scope.origination = resp.data.origination;
                $scope.ibcalltaggedmember_list = resp.data.MarketingCalltaggedmember_list;
                $scope.txtleadrequire_name = resp.data.leadrequire_name,
                    $scope.txtmilletrequire_name = resp.data.milletrequire_name,
                    $scope.txtenquiryrequire_name = resp.data.enquiryrequire_name,
                    $scope.txtstartuprequire_name = resp.data.startuprequire_name,
                    $scope.txtbusiness_name = resp.data.business_name,
                    $scope.txtindustry_name = resp.data.industry_name,

                unlockUI();
            });
            var url = 'api/Marketing/GetDocumentList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.lefilename = resp.data.filename;
                $scope.lefilepath = resp.data.filepath;
                $scope.document_list = resp.data.document_list;
            });
            var url = 'api/Marketing/GetMilletDocumentList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.lmfilename = resp.data.filename;
                $scope.lmfilepath = resp.data.filepath;
                $scope.milletdocument_list = resp.data.milletdocument_list;
            });
            var url = 'api/Marketing/GetEnquiryDocumentList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.lmfilename = resp.data.filename;
                $scope.lmfilepath = resp.data.filepath;
                $scope.enquirydocument_list = resp.data.enquirydocument_list;
            });
        }
        $scope.document_downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.download_all = function (val1,val2) {
            for (var i = 0; i < val2.length; i++) {
               //  console.log(array[i]);
               DownloaddocumentService.Downloaddocument(val1, val2[i]);
           }
       }        

        $scope.download_allmillet = function (val1,val2) {
            for (var i = 0; i < val2.length; i++) {
               //  console.log(array[i]);
               DownloaddocumentService.Downloaddocument(val1, val2[i]);
           }
        }    
        $scope.download_allenquiry = function (val1, val2) {
            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }
        }    
       $scope.milletdocument_downloads = function (val1,val2) {
           DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.enquirydocument_downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

       $scope.documentviewer = function (val1, val2) {
        lockUI();
        var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
        if (IsValidExtension == false) {
        Notify.alert("View is not supported for this format..!", {
         status: 'danger',
         pos: 'top-center',
         timeout: 3000
         });
         unlockUI();
       return false;
         }
         DownloaddocumentService.DocumentViewer(val1, val2);
         }

         $scope.documentviewermillet = function (val1, val2) {
            lockUI();
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
            if (IsValidExtension == false) {
            Notify.alert("View is not supported for this format..!", {
             status: 'danger',
             pos: 'top-center',
             timeout: 3000
             });
             unlockUI();
           return false;
             }
             DownloaddocumentService.DocumentViewer(val1, val2);
        }
        $scope.documentviewerenquiry = function (val1, val2) {
            lockUI();
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
            if (IsValidExtension == false) {
                Notify.alert("View is not supported for this format..!", {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                unlockUI();
                return false;
            }
            DownloaddocumentService.DocumentViewer(val1, val2);
        }

        $scope.Back = function () {
            if (lspage == 'MarketingAddCall') {
                $location.url('app/MstMarketingUnassignedLeadSummary');
            }
            else if (lspage == 'AssignedMarketing') {
                $state.go('app.MstMarketingUnassignedLeadSummary');
            }
            else if (lspage == 'MarketingUnassignedLead') {
                $state.go('app.MstMarketingUnassignedLeadSummary');
            }
        }

    }
})();
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstMarketingWorkInprogressCallSummaryController', MstMarketingWorkInprogressCallSummaryController);

    MstMarketingWorkInprogressCallSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'cmnfunctionService'];

    function MstMarketingWorkInprogressCallSummaryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstMarketingWorkInprogressCallSummaryController';

        activate();

        function activate() {
            var url = 'api/Marketing/GetEmpInProgressMarketingCallSummary';
            SocketService.get(url).then(function (resp) {
                $scope.ibcall_list = resp.data.MarketingCall_list;
                unlockUI();
            });

            var url = "api/Marketing/EmployeeMarketingCallCount";
            SocketService.get(url).then(function (resp) {
                $scope.assignedcall_count = resp.data.assignedcall_count;
                $scope.transfercall_count = resp.data.transfercall_count;
                $scope.completedcall_count = resp.data.completedcall_count;
                $scope.rejectedcall_count = resp.data.rejectedcall_count;
                $scope.inprogresscall_count = resp.data.inprogresscall_count;
                $scope.tagmember_count = resp.data.taggedcall_count;
                $scope.followupcall_count = resp.data.followupcall_count;
                $scope.closedcall_count = resp.data.closedcall_count;

                unlockUI();
            });
        }

        $scope.myassigned_calls = function () {
            $location.url("app/MstMarketingMyAssignedCallSummary");
        }
        $scope.tag_member = function () {
            $location.url("app/MstMarketingTaggedMemberSummary");
        }
        $scope.transfer_calls = function () {
            $location.url("app/MstMarketingTransferCallSummary");
        }
        $scope.completed_calls = function () {
            $location.url("app/MstMarketingCompletedCallSummary");
        }
        $scope.call_response = function (marketingcall_gid) {
            $location.url("app/MstMarketingCallResponse?hash=" + cmnfunctionService.encryptURL("marketingcall_gid=" + marketingcall_gid));
        }
        $scope.view = function (marketingcall_gid) {
            $location.url("app/MstMarketingAssignView?hash=" + cmnfunctionService.encryptURL("marketingcall_gid=" + marketingcall_gid));
        }
        $scope.edit = function (marketingcall_gid) {
            $location.url("app/MstMarketingEdit?hash=" + cmnfunctionService.encryptURL("marketingcall_gid=" + marketingcall_gid));
        }
        $scope.work_inprogress = function () {
            $location.url("app/MstMarketingWorkInprogressCallSummary");
        }
        $scope.acknowledge = function (marketingcall_gid) {
            $location.url("app/MstMarketingAssignedCalls?hash=" + cmnfunctionService.encryptURL("marketingcall_gid=" + marketingcall_gid));
        }
        $scope.followup_call = function () {
            $location.url("app/MstMarketingFollowUpCallSummary");
        }
        $scope.mbdrejected_calls = function () {
            $location.url("app/MstMarketingMBDRejectedCallSummary");
        }
        $scope.closedcall= function() {
            $location.url("app/MstMarketingMyleadsClosedCall");
        }

        $scope.transfer = function (marketingcall_gid) {

            var modalInstance = $modal.open({
                templateUrl: '/transferContent.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];
            function ModalInstanceCtrl($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {

                var params = {
                    marketingcall_gid: marketingcall_gid
                }
                var url = 'api/Marketing/MarketingCallDetailsForTransfer';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.marketingcall_gid = resp.data.marketingcall_gid;
                    $scope.ticket_refid = resp.data.ticket_refid;
                    $scope.assignemployee_gid = resp.data.assignemployee_gid;
                    $scope.assignemployee_name = resp.data.assignemployee_name;
                    $scope.ibcalltransfer_list = resp.data.ibcalltransfer_list;
                });

                var url = 'api/OsdTrnCustomerQueryMgmt/TransferEmployee';
                SocketService.get(url).then(function (resp) {
                    $scope.TransferEmployeeList = resp.data.TransferEmployeeList;
                    unlockUI();
                });

                /*     $scope.marketingcall_gid = marketingcall_gid;
                     $scope.ticketref_no = ticketref_no;
                     $scope.assigned_to = assigned_to; */

                $scope.transfer_call = function () {

                    if ($scope.cboTransferTo == undefined) {
                        modalInstance.close('closed');
                        Notify.alert('Kindly Select the Assign to Person', 'warning');
                        return;
                    }

                    var params = {
                        marketingcall_gid: $scope.marketingcall_gid,
                        ticket_refid: $scope.ticket_refid,
                        transferfrom_gid: $scope.assignemployee_gid,
                        transferfrom_name: $scope.assignemployee_name,
                        transferto_gid: $scope.cboTransferTo,
                        transferto_name: $('#transfer_to :selected').text(),
                        transfer_remarks: $scope.transfer_remarks
                    }

                    var url = "api/Marketing/MarketingCallTransferEmployee";
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status = true) {
                            modalInstance.close('closed');
                            Notify.alert(resp.data.message, 'success');
                            activate();
                        }
                        else {
                            modalInstance.close('closed');
                            Notify.alert(resp.data.message, 'warning');
                            activate();
                        }
                    });


                }
                /* var url = 'api/MstTelecalling/TransferLog';
                 var params = {
                     marketingcall_gid:marketingcall_gid
                 }
                 SocketService.getparams(url, params).then(function (resp) {
                         $scope.TransferLog_list = resp.data.transferLog_list;
                 }); */
                $scope.close = function () {
                    modalInstance.close('closed');
                };
            }
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstRejectedMarketingSummaryController', MstRejectedMarketingSummaryController);

    MstRejectedMarketingSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function MstRejectedMarketingSummaryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstRejectedMarketingSummaryController';

        activate();

        function activate() {
            var url = "api/Marketing/MarketingAssignedCallCount";
            SocketService.get(url).then(function (resp) {
                $scope.assignedcall_count = resp.data.assignedcall_count;
                $scope.completedcall_count = resp.data.completedcall_count;
                $scope.followupcall_count = resp.data.followupcall_count;
                $scope.closedcall_count = resp.data.closedcall_count;
                $scope.rejectedcall_count = resp.data.rejectedcall_count;
                unlockUI();
            });

            var url = 'api/Marketing/GetRejectedCallSummary';
            SocketService.get(url).then(function (resp) {
                $scope.ibrejectedcall_list = resp.data.MarketingCall_list;
                unlockUI();
            });
        }


        $scope.closed_call = function () {
            $location.url("app/MstClosedMarketingSummary");
        }
        $scope.followup_call = function () {
            $location.url("app/MstFollowUpMarketingSummary");
        }
        $scope.assigned_call = function () {
            $location.url("app/MstAssignedMarketingSummary");
        }
        $scope.completed_call = function () {
            $location.url("app/MstCompletedMarketingSummary");
        }

        $scope.view = function (marketingcall_gid) {
            $location.url('app/MstMarketingManageRejectedCallView?marketingcall_gid=' + marketingcall_gid);
        }
        $scope.rejected_call = function () {
            $location.url("app/MstRejectedMarketingSummary");
        }
    }
})();

// JavaScript source code
// JavaScript source code
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('StartupRequireController', StartupRequireController);

    StartupRequireController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function StartupRequireController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'StartupRequireController';
        activate();


        function activate() {

            var url = 'api/MstStartupRequire/GetStartupRequire';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.startuprequire_list = resp.data.startuprequire_list;
                unlockUI();
            });
        }

        $scope.popupstartuprequire = function () {
            var modalInstance = $modal.open({
                templateUrl: '/myModalContent.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.startuprequireSubmit = function () {
                    var params = {
                        startuprequire_name: $scope.txtstartuprequire,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code
                    }

                    var url = 'api/MstStartupRequire/CreateStartupRequire';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        else {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }

                        activate();

                    });
                    $modalInstance.close('closed');

                }

            }
        }

        $scope.editstartuprequire = function (startuprequire_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editstartuprequire.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    startuprequire_gid: startuprequire_gid
                }
                var url = 'api/MstStartupRequire/EditStartupRequire';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.txtstartuprequire = resp.data.startuprequire_name;
                    $scope.startuprequire_gid = resp.data.startuprequire_gid;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.startuprequireUpdate = function () {

                    var url = 'api/MstStartupRequire/UpdateStartupRequire';
                    var params = {
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        startuprequire_name: $scope.txtstartuprequire,
                        startuprequire_gid: $scope.startuprequire_gid
                    }
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });


                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }

                        activate();

                    });
                    $modalInstance.close('closed');
                }

            }
        }

        $scope.Status_update = function (startuprequire_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusaudittype.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    startuprequire_gid: startuprequire_gid
                }
                var url = 'api/MstStartupRequire/EditStartupRequire';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.startuprequire_gid = resp.data.startuprequire_gid
                    $scope.txtstartuprequire = resp.data.startuprequire_name;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        startuprequire_name: $scope.txtstartuprequire,
                        startuprequire_gid: $scope.startuprequire_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/MstStartupRequire/InactiveStartupRequire';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'info',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        activate();
                    });

                    $modalInstance.close('closed');

                }
                var param = {
                    startuprequire_gid: startuprequire_gid
                }

                var url = 'api/MstStartupRequire/StartupRequireInactiveLogview';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.startuprequireinactivelog_data = resp.data.startuprequire_list;
                    unlockUI();
                });
            }
        }


        $scope.deletestartuprequire = function (startuprequire_gid) {
            var params = {
                startuprequire_gid: startuprequire_gid
            }
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do You Want To Delete the Record ?',
                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Yes, delete it!',
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    var url = 'api/MstStartupRequire/DeleteStartupRequire';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            SweetAlert.swal('Deleted Successfully!');

                            activate();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                    });
                }

            });
        };
    }

})();