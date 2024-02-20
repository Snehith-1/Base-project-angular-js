(function () {
    'use strict';

    angular
        .module('angle')
        .controller('deferralManagement', deferralManagement);

    deferralManagement.$inject = ['$rootScope', '$scope','$modal', '$state', 'SweetAlert','AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route','ngTableParams'];

    function deferralManagement($rootScope, $scope,$modal, $state,SweetAlert,AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'deferralManagement';
        //$scope.loandata = [];
        activate();
        function activate() {
            $scope.totalDisplayed=100;
            var url = 'api/deferral/directDeferralSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.deferral_data = resp.data.deferralSummaryDtls;  
                // new code start  
                unlockUI(); 
                if ($scope.deferral_data== null) {
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
                // if( $scope.deferral_data ==null){
                //     $scope.total= 0;
                // }   
                // else{
                    // $scope.total = $scope.deferral_data .length;
                // }         
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
        if ($scope.deferral_data!= null) {
       
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

        $scope.edit = function (val) {
            $scope.deferral_gid = val;
            $scope.deferral_gid = localStorage.setItem('deferral_gid', val);
            $state.go('app.editDeferral');
        }
       


        $scope.delete = function (val) {
            var params = {
                deferral_gid: val
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
                    var url = 'api/deferral/deferraldeleterecords';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                            SweetAlert.swal('Deleted Successfully!');
                        }
                        else {
                            
                            activate();
                            SweetAlert.swal('Deferral Moved to multiple Stages.You cant Delete it!');
                        }
                    });
                  
                }

            });
           
        }


        $scope.popupdeferral = function () {
            $state.go('app.createDeferral');
        }

        

        $scope.popupView = function (val) {
            $scope.deferral_gid = val;
            $scope.deferral_gid = localStorage.setItem('deferral_gid', val);
            $state.go('app.viewDeferral');

        }


    }
})();