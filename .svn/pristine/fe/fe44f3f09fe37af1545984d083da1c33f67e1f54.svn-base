(function () {
    'use strict';

    angular
        .module('app.service')
        
        .controller('issue', issue);

    issue.$inject = ['$rootScope', '$scope', '$modal','statusService', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', 'ngTableParams'];

    function issue($rootScope, $scope, $modal,statusService, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'issue';

        activate();

        function activate() {
    
            //Issue Summary Table Data

            var url = apiManage.apiList['issuedetail'].api;
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.notification = resp.data;
                $scope.datas = resp.data.tabledata;
     
            });
        }

        // View Issue 

        $scope.view = function (issue_gid) {
            localStorage.setItem('issue_gid', issue_gid);
            $state.go('app.viewIssueDetails');
        }

        // chat Issue

        $scope.chat = function (val) {
            $scope.issueid = val;
            localStorage.setItem('issue_id', val);
            $state.go('app.responselog');
        }
        $scope.statusLog = function (val) {
            var doc = document.getElementById('statusLog');
            doc.style.display = 'block';
            var params = {
                issue_gid: val
            };
            var url = apiManage.apiList["statusLog"].api;
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.data = resp.data.tabledata;
            });
        }


        // Issue status PopUp
        
        $scope.state = function (val) {
            $scope.issue_id=val;
            var doc = document.getElementById('status');
            doc.style.display = 'block';
        }

        // Download Document

        $scope.downloads = function (val1, val2) {
           
            var phyPath = val1;
            var relPath = phyPath.split("EMS");
            var relpath1 = relPath[1].replace("\\", "/");
            var hosts = window.location.host;
            var prefix = "http://"
            var str = prefix.concat(hosts, relpath1);
            var link = document.createElement("a");
            var name = val2.split(".")
            link.download = name[0];
            var uri = str;
            link.href = uri;
            link.click();
        }

        // Send Vendor Chat Data

        $scope.logsend = function (val) {
            var params = {
                issue_gid: $scope.issueid,
                vendor_text: val
            };
            var url = apiManage.apiList["sendlog"].api;
            SocketService.post(url, params).then(function (resp) {
                var params = {
                    issue_gid: $scope.issueid
                };
                var url = apiManage.apiList["issuechat"].api;
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.chatdtl = resp.data.datelist;
                });
            });
            $scope.sendText = '';
        }
        $scope.readytorelease = function () {
            $state.go('app.readytorelease');
        }
    }
    
})();
