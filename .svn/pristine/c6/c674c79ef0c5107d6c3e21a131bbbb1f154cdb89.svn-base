(function () {
    'use strict';

    angular
        .module('angle')
        .controller('DtsRptUserReport2', DtsRptUserReport2);

    DtsRptUserReport2.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams','DownloaddocumentService'];

    function DtsRptUserReport2($rootScope, $scope, $modal, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'DtsRptUserReport2';

        activate();

        function activate() {
            var url = 'api/customer/state';
            SocketService.get(url).then(function (resp) {
                $scope.state_list = resp.data.state_list;
            });
            var url = 'api/vertical/vertical';
            SocketService.get(url).then(function (resp) {
                $scope.vertical_list = resp.data.vertical_list;
            });
            var url = 'api/entity/entity';
            SocketService.get(url).then(function (resp) {
                $scope.entity_list = resp.data.entity_list;
            });
            $scope.page = localStorage.getItem('page');
            $scope.totalDisplayed = 100;

            var url = 'api/deferral/User2reportsummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.User2_data = resp.data.deferralSummaryDtls;
                unlockUI();
                if ($scope.User2_data == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.User2_data.length;
                    if ($scope.User2_data.length < 100) {
                        $scope.totalDisplayed = $scope.User2_data.length;
                    }
                }
            });
        }

        $scope.loadMore = function (pagecount) {
            if (pagecount == undefined) {
                Notify.alert("Enter the Total Summary Count", "warning");
                return;
            }
            lockUI();

            var Number = parseInt(pagecount);

            if ($scope.User2_data != null) {

                if (pagecount < $scope.User2_data.length) {
                    $scope.totalDisplayed += Number;
                    if ($scope.User2_data.length < $scope.totalDisplayed) {
                        $scope.totalDisplayed = $scope.User2_data.length;
                        Notify.alert(" Total Summary " + $scope.User2_data.length + " Records Only", "warning");
                    }
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(" Total Summary " + $scope.User2_data.length + " Records Only", "warning");
                    return;
                }
            }
            $scope.vertical = ""
            $scope.entity_gid=""
            $scope.cbostate=""
            unlockUI();
        };

        $scope.all = function () {
            $scope.entity_gid = "";
            $scope.vertical = "";
            $scope.cbostate = "";
            activate();
        }

        $scope.search = function () {
            var params = {
                vertical_gid: $scope.vertical,
                entity_gid: $scope.entity_gid,
                state_gid:$scope.cbostate
            }
            
            var url = 'api/deferral/User2reportsummarysearch';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                $scope.User2_data = resp.data.deferralSummaryDtls;
              });
        }

        $scope.popupView = function (val) {
            $scope.deferral_gid = val;
            $scope.deferral_gid = localStorage.setItem('deferral_gid', val);
            localStorage.setItem('page', 'UserReport2');
            $state.go('app.reportpagedetails');
        }

        $scope.export = function () {
            if($scope.cbostate == undefined || $scope.cbostate == "")
            {
                $scope.cbostate = "";
            }
           
            var params = {
                vertical_gid: $scope.vertical,
                entity_gid: $scope.entity_gid,
                state_gid:$scope.cbostate        
              }
            lockUI();

            var url = 'api/deferral/UserReport2export';
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    DownloaddocumentService.Downloaddocument(resp.data.lspath, resp.data.lsname);
                    unlockUI();
                    // var phyPath = resp.data.lspath;
                    // var relPath = phyPath.split("EMS");
                    // var relpath1 = relPath[1].replace("\\", "/");
                    // var hosts = window.location.host;
                    // var prefix = location.protocol + "//";
                    // var str = prefix.concat(hosts, relpath1);
                    // var link = document.createElement("a");
                    // var name = resp.data.lsname.split('.');
                    // link.download = name[0];
                    // var uri = str;
                    // link.href = uri;
                    // link.click();

                }
                else {
                    unlockUI();
                    Notify.alert('Error Occurred While Export !', 'warning')
                    activate();

                }

            });
        }
    }
})();