(function () {
    'use strict';

    angular
        .module('angle')
        .controller('acknowledgeMyAssetcontroller', acknowledgeMyAssetcontroller);

    acknowledgeMyAssetcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route'];

    function acknowledgeMyAssetcontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'acknowledgeMyAssetcontroller';

        activate();
        $scope.input = {
            reason_reject: ''
        };
        function activate() {
            var url = 'api/acknowledgeMyAsset/acknowledgement';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.acksummary = resp.data.acksummary;
            });
        }

        $scope.ack_click = function (val1) {
            var params = { asset2custodian_gid: val1 }
            var url = 'api/acknowledgeMyAsset/submitacknowledgement';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    activate();
                }
            })


        };

        $scope.reject_popup = function (val) {
            $scope.asset2custodian_gid = localStorage.setItem('val', val);
            var modalInstance = $modal.open({
                templateUrl: '/rejectasset.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var url = 'api/employee/employee';
                SocketService.get(url).then(function (resp) {
                    $scope.employee_list = resp.data.employee_list;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                //Submit event

                $scope.ackreject_click = function () {
                    $scope.asset2custodian_gid=localStorage.getItem('val');
                    var params = {

                        asset2custodian_gid: $scope.asset2custodian_gid,
                        reason_reject: $scope.input.reason_reject
                    }
                    console.log(params);
                    var url = 'api/acknowledgeMyAsset/acknowledgementreject';
                    lockUI();
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

    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('adminlogincontroller', adminlogincontroller);

    adminlogincontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route'];

    function adminlogincontroller($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'adminlogincontroller';

        activate();

        function activate() {
            var url = 'api/AdminLogin/SValues';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                var host = window.location.host;
                var prefix = "https://"
                var win = window.open(prefix.concat(host, "/Framework/adlogin.aspx?userCode=", resp.data.user_code, "&?&userPassword=", resp.data.user_password, "&?&companyCode=", resp.data.company_code), '_blank');
                win.focus();
            })
            $state.go('app.welcome');
        }


    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('assetDashboardcontroller', assetDashboardcontroller);

    assetDashboardcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function assetDashboardcontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'assetDashboardcontroller';

        activate();

        function activate() {
            var user_gid = localStorage.getItem('user_gid');
            var url = 'api/user/privilegelevel3';
            SocketService.get(url + '?user_gid=' + user_gid).then(function (resp) {
                var viewasset = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("AMSAMSVIW");
                var ackasset = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("AMSAMSACK");
                var surrenderasset = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("AMSAMSSRA");
                var tempasset = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("AMSAMSTHO");
                if (viewasset != -1) {
                    $scope.viewasset = 'Y';
                }
                if (ackasset != -1) {
                    $scope.ackasset = 'Y';
                }
                if (surrenderasset != -1) {
                    $scope.surrenderasset = 'Y';
                }
                if (tempasset != -1) {
                    $scope.tempasset = 'Y';
                }

            });
            var url = 'api/landingPage/landingpagedata';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.count_acknowledgement = resp.data.count_acknowledgement;
                $scope.count_myasset = resp.data.count_myasset;
                $scope.count_surrender = resp.data.count_surrender;
                $scope.count_temporaryhandover = resp.data.count_temporaryhandover + resp.data.count_tmpsurrender + resp.data.count_tmpholding;
                $scope.employee_id = resp.data.employee_id;
                $scope.count_response = resp.data.count_response;
                $scope.count_myapprovals = resp.data.count_myapprovals;
            });
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('assetSurrendercontroller', assetSurrendercontroller);

    assetSurrendercontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route'];

    function assetSurrendercontroller($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'assetSurrendercontroller';
        activate();

        function activate() {
            var url = 'api/surrenderAsset/surrender';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.surrendersummary = resp.data.surrendersummary;
            });
        }

        $scope.surrender_submit = function (val1) {
            var params = { asset2custodian_gid: val1 }
            var url = 'api/surrenderAsset/submitsurrender';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    activate();
                }
            })
        };
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('loginerp', loginerp);

    loginerp.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$cookies', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route'];

    function loginerp($rootScope, $scope, $state, AuthenticationService, $cookies, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'loginerp';

        activate();

        function activate() {
            var params = {
                user_code: getCookie("user_code"),
                company_code: getCookie("company_code")
            }
            console.log(params);
            var url = 'api/Login/LoginERP';
            lockUI();
            SocketService.postlogin(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status = true) {
                    $cookies.putObject('token', resp.data.token);
                    localStorage.setItem('user_gid', resp.data.user_gid)
                    $state.go('app.welcome');
                }
            })
        }
        function getCookie(cname) {
            var name = cname + "=";
            var decodedCookie = decodeURIComponent(document.cookie);
            var ca = decodedCookie.split(';');
            for (var i = 0; i < ca.length; i++) {
                var c = ca[i];
                while (c.charAt(0) == ' ') {
                    c = c.substring(1);
                }
                if (c.indexOf(name) == 0) {
                    return c.substring(name.length, c.length);
                }
            }
            return "";
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('temporaryHandovercontroller', temporaryHandovercontroller);

    temporaryHandovercontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route'];

    function temporaryHandovercontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'temporaryHandovercontroller';

        activate();

        function activate() {
            var url = 'api/temporaryHandover/tmphandover';
            //lockUI();
            SocketService.get(url).then(function (resp) {
                //unlockUI();
                $scope.tmphandoversummary = resp.data.tmphandoversummary;
                $scope.tempHoldersummary = resp.data.tempHoldersummary;
                $scope.tempadminsurrendersummary = resp.data.tempadminsurrendersummary;
                $scope.tempHoldinsassetsummary = resp.data.tempHoldinsassetsummary;
            });
        }

        //  Surrender to IT Admin....//

        $scope.surrenderitadminclick = function (val) {
            var params = { asset_id: val };
            var url = 'api/temporaryHandover/surrenderitadmin';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    activate();
                }
                else {
                    Notify.alert('Internal Error Occurred!', {
                        status: 'Warning',
                        pos: 'top-center',
                        timeout: 3000
                    });

                }
            });
        }


        // Holding Asset....//

        $scope.holdingassetclick = function (val) {
            var params = { asset_id: val };
            var url = 'api/temporaryHandover/holdingasset';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    activate();
                }
                else {
                    Notify.alert('Internal Error Occurred!', {
                        status: 'Warning',
                        pos: 'top-center',
                        timeout: 3000
                    });

                }
            });
        }

        $scope.surrender_submit = function (val1) {
            var params = { asset_id: val1 }
            var url = 'api/temporaryHandover/submittmphandover';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    activate();
                }
                else {
                    Notify.alert('Internal Error Occurred!', {
                        status: 'Warning',
                        pos: 'top-center',
                        timeout: 3000
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
        .controller('viewMyAssetcontroller', viewMyAssetcontroller);

    viewMyAssetcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route'];

    function viewMyAssetcontroller($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'viewMyAssetcontroller';

        activate();

        function activate() {
            //lockUI();
            var url = 'api/viewMyAsset/myasset';
            SocketService.get(url).then(function (resp) {
                //unlockUI();
                $scope.myassetsummary = resp.data.myassetsummary;
            });
        }
    }
})();
