(function () {
    'use strict';
    angular
           .module('angle')
           .controller('marketingDashboardController', marketingDashboardController);

           marketingDashboardController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$filter', 'ngTableParams', '$resource', '$timeout', 'ngTableDataService'];

    function marketingDashboardController($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $filter, ngTableParams, $resource, $timeout, ngTableDataService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'marketingDashboardController';

        activate();
        lockUI();
        function activate() {
           
           // New Tab

           $scope.totalDisplayednew = 50;
           var url = 'api/MarketingDashboard/GetNewTabDtl';
           SocketService.get(url).then(function (resp) {
               $scope.newtab_list = resp.data.newtab_list;
               if ($scope.newtab_list == null) {
                   $scope.totalDisplayednew = 0;
                   $scope.totalnew = 0;
               }
               else {
                   $scope.totalnew = $scope.newtab_list.length;
                   if ($scope.newtab_list.length < 100) {
                       $scope.totalDisplayednew = $scope.newtab_list.length;
                   }
               }
               unlockUI();
           });

           var url = 'api/MarketingDashboard/GetOverAllLeadCount';
            SocketService.get(url).then(function (resp) {
                $scope.count_new = resp.data.count_new;
                $scope.count_followup = resp.data.count_followup;
                $scope.count_prospect = resp.data.count_prospect;
                $scope.count_potential = resp.data.count_potential;              
                $scope.count_close = resp.data.count_close;              
                $scope.count_drop = resp.data.count_drop;              
                $scope.count_all = resp.data.count_all;  
            });
            var url = 'api/MarketingDashboard/GetDashboardCount';
            SocketService.get(url).then(function (resp) {
                $scope.Dashboardcount_list = resp.data.Dashboardcount_list;
               
                angular.forEach($scope.Dashboardcount_list, function (value, key) {
                  /*   var params = {
                        employee_gid: value.employee_gid
                    }; */
                    var url = 'api/MarketingDashboard/GetOverAllCount';
                    SocketService.get(url).then(function (resp) {
                        value.alltabcount_list = resp.data.alltabcount_list;
                        console.log(resp.data);
                        value.expand = false;
                       
                    });
                });
                unlockUI();
            });

        /*     var url = 'api/MarketingDashboard/GetOverAllCount';
           SocketService.get(url).then(function (resp) {
               $scope.alltabcount_list = resp.data.alltabcount_list;
               console.log(resp.data.alltabcount_list)
               if ($scope.newtab_list == null) {
                   $scope.totalDisplayednew = 0;
                   $scope.totalnew = 0;
               }
               else {
                   $scope.totalnew = $scope.newtab_list.length;
                   if ($scope.newtab_list.length < 100) {
                       $scope.totalDisplayednew = $scope.newtab_list.length;
                   }
               }
               unlockUI();
           }); */
           
            
        }

        $scope.loadMorenew = function (pagecountnew) {
            if (pagecountnew == undefined) {
                Notify.alert("Enter the Total Summary Count", "warning");
                return;
            }
            lockUI();
            var Number = parseInt(pagecountnew);
            if ($scope.newtab_list != null) {

                if (pagecountnew < $scope.newtab_list.length) {
                    $scope.totalDisplayednew += Number;
                    if ($scope.newtab_list.length < $scope.totalDisplayednew) {
                        $scope.totalDisplayednew = $scope.newtab_list.length;
                        Notify.alert(" Total Summary " + $scope.newtab_list.length + " Records Only", "warning");
                    }
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(" Total Summary " + $scope.newtab_list.length + " Records Only", "warning");
                    return;
                }
            }
            unlockUI();
        };

        // FollowUp Tab

          $scope.Followup_Tab = function () {
            lockUI();
            $scope.totalDisplayedfollowup = 50;
            var url = 'api/MarketingDashboard/GetFollowUpTabDtl';
            SocketService.get(url).then(function (resp) {
                $scope.FollowUptab_list = resp.data.FollowUptab_list;    
                if ($scope.FollowUptab_list == null) {
                    $scope.totalDisplayedfollowup = 0;
                    $scope.totalfollowup = 0;
                }
                else {
                    $scope.totalfollowup = $scope.FollowUptab_list.length;
                    if ($scope.FollowUptab_list.length < 100) {
                        $scope.totalDisplayedfollowup = $scope.FollowUptab_list.length;
                    }
                }          
                unlockUI();
            });
        }
        $scope.loadmoreFollowup = function (followuptab) {
            if (followuptab == undefined) {
                Notify.alert("Enter the Total Summary Count", "warning");
                return;
            }
            lockUI();
            var Number = parseInt(followuptab);
            if ($scope.FollowUptab_list != null) {

                if (followuptab < $scope.FollowUptab_list.length) {
                    $scope.totalDisplayedfollowup += Number;
                    if ($scope.FollowUptab_list.length < $scope.totalDisplayedfollowup) {
                        $scope.totalDisplayedfollowup = $scope.FollowUptab_list.length;
                        Notify.alert(" Total Summary " + $scope.FollowUptab_list.length + " Records Only", "warning");
                    }
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(" Total Summary " + $scope.FollowUptab_list.length + " Records Only", "warning");
                    return;
                }
            }
            unlockUI();
        };
        
        // Prospect Tab
        $scope.Prospect_Tab = function () {
            lockUI();
            $scope.totalDisplayedProspectloadmore = 100;
            var url = 'api/MarketingDashboard/GetProspectTabDtl';
            SocketService.get(url).then(function (resp) {
                $scope.Prospecttab_list = resp.data.Prospecttab_list;  
                if ($scope.Prospecttab_list == null) {
                    $scope.totalDisplayedProspectloadmore = 0;
                    $scope.totalprospect = 0;
                }
                else {
                    $scope.totalprospect = $scope.Prospecttab_list.length;
                    if ($scope.Prospecttab_list.length < 100) {
                        $scope.totalDisplayedProspectloadmore = $scope.Prospecttab_list.length;
                    }
                }             
                unlockUI();
            });
        }
        $scope.loadmoreProspect = function (countprospect) {
            if (countprospect == undefined) {
                Notify.alert("Enter the Total Summary Count", "warning");
                return;
            }
            lockUI();
            var Number = parseInt(countprospect);
            if ($scope.Prospecttab_list != null) {

                if (countprospect < $scope.Prospecttab_list.length) {
                    $scope.totalDisplayedProspectloadmore += Number;
                    if ($scope.Prospecttab_list.length < $scope.totalDisplayedProspectloadmore) {
                        $scope.totalDisplayedProspectloadmore = $scope.Prospecttab_list.length;
                        Notify.alert(" Total Summary " + $scope.Prospecttab_list.length + " Records Only", "warning");
                    }
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(" Total Summary " + $scope.Prospecttab_list.length + " Records Only", "warning");
                    return;
                }
            }
            unlockUI();
        };

         // Potential Tab
         $scope.Potential_Tab = function () {
            lockUI();
            $scope.totalDisplayedPotential = 50;
            var url = 'api/MarketingDashboard/GetPotentialTabDtl';
            SocketService.get(url).then(function (resp) {
                $scope.Potentialtab_list = resp.data.Potentialtab_list;
                if ($scope.Potentialtab_list == null) {
                    $scope.totalDisplayedPotential = 0;
                    $scope.totalcpotential = 0;
                }
                else {
                    $scope.totalcpotential = $scope.Potentialtab_list.length;
                    if ($scope.Potentialtab_list.length < 100) {
                        $scope.totalDisplayedPotential = $scope.Potentialtab_list.length;
                    }
                }  
                unlockUI();
            });
        }
        $scope.loadMorePotential = function (Potentialcount) {
            if (Potentialcount == undefined) {
                Notify.alert("Enter the Total Summary Count", "warning");
                return;
            }
            lockUI();
            var Number = parseInt(Potentialcount);
            if ($scope.Potentialtab_list != null) {

                if (Potentialcount < $scope.Potentialtab_list.length) {
                    $scope.totalDisplayedPotential += Number;
                    if ($scope.Potentialtab_list.length < $scope.totalDisplayedPotential) {
                        $scope.totalDisplayedPotential = $scope.Potentialtab_list.length;
                        Notify.alert(" Total Summary " + $scope.Potentialtab_list.length + " Records Only", "warning");
                    }
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(" Total Summary " + $scope.Potentialtab_list.length + " Records Only", "warning");
                    return;
                }
            }
            unlockUI();
        };
        // Drop Tab
         $scope.Drop_Tab = function () {
            lockUI();
            $scope.totalDisplayedDroploadmore = 50;
            var url = 'api/MarketingDashboard/GetDropTabDtl';
            SocketService.get(url).then(function (resp) {
                $scope.Droptab_list = resp.data.Droptab_list;
                if ($scope.Droptab_list == null) {
                    $scope.totalDisplayedDroploadmore = 0;
                    $scope.totalDrop = 0;
                }
                else {
                    $scope.totalDrop = $scope.Droptab_list.length;
                    if ($scope.Droptab_list.length < 100) {
                        $scope.totalDisplayedDroploadmore = $scope.Droptab_list.length;
                    }
                }  
                unlockUI();
            });
        }
        $scope.loadMoreDrop = function (dropcount) {
            if (dropcount == undefined) {
                Notify.alert("Enter the Total Summary Count", "warning");
                return;
            }
            lockUI();
            var Number = parseInt(dropcount);
            if ($scope.Droptab_list != null) {

                if (dropcount < $scope.Droptab_list.length) {
                    $scope.totalDisplayedDroploadmore += Number;
                    if ($scope.Droptab_list.length < $scope.totalDisplayedDroploadmore) {
                        $scope.totalDisplayedDroploadmore = $scope.Droptab_list.length;
                        Notify.alert(" Total Summary " + $scope.Droptab_list.length + " Records Only", "warning");
                    }
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(" Total Summary " + $scope.Droptab_list.length + " Records Only", "warning");
                    return;
                }
            }
            unlockUI();
        };
         // All Tab
         $scope.All_Tab = function () {
            lockUI();
            $scope.totalDisplayedAll = 50;
            var url = 'api/MarketingDashboard/GetAllTabDtl';
            SocketService.get(url).then(function (resp) {
                $scope.Alltab_list = resp.data.Alltab_list;
                if ($scope.Alltab_list == null) {
                    $scope.totalDisplayedAll = 0;
                    $scope.totalAll = 0;
                }
                else {
                    $scope.totalAll = $scope.Alltab_list.length;
                    if ($scope.Alltab_list.length < 100) {
                        $scope.totalDisplayedAll = $scope.Alltab_list.length;
                    }
                }  
                unlockUI();
            });
        }
       
  
    $scope.loadMoreAll = function (countAll) {
        if (countAll == undefined) {
            Notify.alert("Enter the Total Summary Count", "warning");
            return;
        }
        lockUI();
        var Number = parseInt(countAll);
        if ($scope.Alltab_list != null) {

            if (countAll < $scope.Alltab_list.length) {
                $scope.totalDisplayedAll += Number;
                if ($scope.Alltab_list.length < $scope.totalDisplayedAll) {
                    $scope.totalDisplayedAll = $scope.Alltab_list.length;
                    Notify.alert(" Total Summary " + $scope.Alltab_list.length + " Records Only", "warning");
                }
                unlockUI();
            }
            else {
                unlockUI();
                Notify.alert(" Total Summary " + $scope.Alltab_list.length + " Records Only", "warning");
                return;
            }
        }
        unlockUI();
    };
}
   
})();
