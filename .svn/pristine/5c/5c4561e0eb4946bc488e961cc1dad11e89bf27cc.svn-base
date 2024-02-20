(function () {
    'use strict';

    angular
        .module('angle')
        .controller('reopen', reopen);

    reopen.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function reopen($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'reopen';

        activate();
        function activate() {
            $scope.totalDisplayed=100;
            var url = 'api/deferral/reopensummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                // $scope.deferralSummary = resp.data;
                $scope.cadApproval_data = resp.data.rmdeferralSummaryDtls;
                 // new code start   
                 if ($scope.cadApproval_data == null) {
                                        $scope.total = 0;
                                        $scope.totalDisplayed = 0;
                                    }
                                    else {
                                        $scope.total = $scope.cadApproval_data.length;
                                        if ($scope.cadApproval_data.length < 100) {
                                            $scope.totalDisplayed = $scope.cadApproval_data.length;
                                        }
                                    }
                    // new code end
                // $scope.total=$scope.cadApproval_data.length;

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
        if ($scope.cadApproval_data != null) {
       
                if (pagecount < $scope.cadApproval_data.length) {
                    $scope.totalDisplayed += Number;
                    if($scope.cadApproval_data.length<$scope.totalDisplayed){
                        $scope.totalDisplayed =$scope.cadApproval_data.length;
                        Notify.alert(" Total Summary " + $scope.cadApproval_data.length + " Records Only", "warning");
                    }
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(" Total Summary " + $scope.cadApproval_data.length + " Records Only", "warning");
                    return;
                }
            }
            // new code end
            // $scope.totalDisplayed += Number;
            // console.log(pagecount);
            unlockUI();
        };

        $scope.isShowHide = function (param) {
            if (param == "show") {
                $scope.showval = true;
                $scope.hideval = true;
            }
            else if (param == "hide") {
                $scope.showval = false;
                $scope.hideval = false;
            }
            else {
                $scope.showval = false;
                $scope.hideval = false;
            }
        }

        $scope.popupApprove = function (val) {
            $scope.deferral_gid = val;
            $scope.deferral_gid = localStorage.setItem('deferral_gid', val);
            //console.log(val);
            $state.go('app.reopenclosed');

        }



        $scope.downloads = function (val1, val2) {
            var phyPath = val1;
            var relPath = phyPath.split("StoryboardAPI");
            var relpath1 = relPath[1].replace("\\", "/");
            var hosts = window.location.host;
            var prefix = location.protocol + "//";
            var str = prefix.concat(hosts, relpath1);
            var link = document.createElement("a");
            var name = val2.split(".")
            link.download = val2;
            var uri = str;
            link.href = uri;
            link.click();
        }
        // Close Modals

        $scope.close = function (val) {
            document.getElementById("userform").reset();
            var doc = document.getElementById(val);
            doc.style.display = 'none';
        }

    }
})();
