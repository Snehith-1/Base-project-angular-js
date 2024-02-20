(function () {
    'use strict';

    angular
        .module('angle')
        .controller('reportView', reportView);

    reportView.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams','DownloaddocumentService'];

    function reportView($rootScope, $scope, $modal, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        //vm.title = 'deferralReport';

        activate();
        function activate() {
            $scope.totalDisplayed=100;
            var url = 'api/deferral/directDeferralSummaryview';
            lockUI();
            SocketService.get(url).then(function (resp) {
               unlockUI();
                $scope.deferral_data = resp.data.deferralSummaryDtls;
                // new code start   
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
            // var url = 'api/customer/customer';
            // SocketService.get(url).then(function (resp) {
            //     $scope.customer_list = resp.data.customer_list;
            // });

            var url = 'api/vertical/vertical';
            SocketService.get(url).then(function (resp) {
                $scope.vertical_list = resp.data.vertical_list;
            });
            
            var url = 'api/branch/branch';
            SocketService.get(url).then(function (resp) {
                $scope.branch_list = resp.data.branch_list;
            });
        
            var url = 'api/customer/state';
            SocketService.get(url).then(function (resp) {
                $scope.state_list = resp.data.state_list;
            });

            var url = 'api/entity/entity';
            SocketService.get(url).then(function (resp) {
                $scope.entity_list = resp.data.entity_list;
            });

        }
        



        $scope.complete = function (string) {

            if (string.length >= 3) {
                $scope.message = "";
                var url = 'api/customer/ExploreCustomer';
                var params = {
                    customername: string
                }
                SocketService.getparams(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        $scope.message = "";
                        $scope.customer_list = resp.data.Customers;
                    }
                    else {
                        $scope.message = "No Records";
                    }
                });
            }
            else {
                $scope.customer_list = null;
                $scope.message = "Type atleast three character";
            }
        }

        $scope.fillTextbox = function (customer_gid, customer_name) {
            //console.log('string', customer_name, customer_gid);
            $scope.customer = customer_name;
            $scope.customer_gid = customer_gid;
            $scope.customer_list = null;
        }

         $scope.all = function () {
             $scope.entity_gid = "";
             $scope.branch = "";
             $scope.customer_gid = "";
             $scope.customer = "";
             $scope.vertical = "";
             $scope.cbostate = "";
             //document.getElementById(userform7).reset();
             activate();
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
           $scope.search = function () {
            var params = {
                customer_gid: $scope.customer_gid,
                vertical_gid: $scope.vertical,
                entity_gid: $scope.entity_gid,
                branch_gid: $scope.branch,
                relationshipMgmt: $scope.relationshipMgmt,
                zonalHead: $scope.zonalHead,
                businessHead: $scope.businessHead,
                deferralApprover: $scope.deferralApprover,
                state_gid:$scope.cbostate
            }
            lockUI();
            var url = 'api/deferral/directDeferralSummaryreportview';
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                $scope.deferral_data = resp.data.deferralSummaryDtls;
            });
        }

        $scope.popupView = function (val) {
            $scope.deferral_gid = val;
            $scope.deferral_gid = localStorage.setItem('deferral_gid', val);
            $state.go('app.reportViewdetails');

        }

        $scope.export = function () {
            var params = {
                customer_gid: $scope.customer_gid,
                vertical_gid: $scope.vertical,
                branch_gid: $scope.branch,
                entity_gid: $scope.entity_gid,
                relationshipMgmt: $scope.relationshipMgmt,
                zonalHead: $scope.zonalHead,
                businessHead: $scope.businessHead,
                deferralApprover: $scope.deferralApprover,
                state_gid:$scope.cbostate
            }
            //console.log(params);
            lockUI();
            var url = 'api/deferral/excel';
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
                    // //console.log(str);
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