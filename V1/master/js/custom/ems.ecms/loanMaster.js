(function () {
    'use strict';

    angular
        .module('angle')
        .controller('loancontroller', loancontroller);

    loancontroller.$inject = ['$rootScope', '$scope', '$state', '$modal', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function loancontroller($rootScope, $scope, $state, $modal, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'loancontroller';
        //console.log($scope.segment_name);
        activate();


        function activate() {
            $scope.totalDisplayed=100;
            lockUI();
            var url = 'api/loan/getLoanmasterSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.vertical = resp.data.loanDetails;
                 // new code start   
                 if ($scope.vertical== null) {
                                        $scope.total = 0;
                                        $scope.totalDisplayed = 0;
                                    }
                                    else {
                                        $scope.total = $scope.vertical.length;
                                        if ($scope.vertical.length < 100) {
                                            $scope.totalDisplayed = $scope.vertical.length;
                                        }
                                    }
                    // new code endd
                //$scope.total=$scope.vertical.length;
                //console.log(resp.data.loanDetails);
                
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
        if ($scope.vertical!= null) {
       
                if (pagecount < $scope.vertical.length) {
                    $scope.totalDisplayed += Number;
                    if($scope.vertical.length<$scope.totalDisplayed){
                        $scope.totalDisplayed =$scope.vertical.length;
                        Notify.alert(" Total Summary " + $scope.vertical.length + " Records Only", "warning");
                    }
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(" Total Summary " + $scope.vertical.length + " Records Only", "warning");
                    return;
                }
            }
            // new code end
            // $scope.totalDisplayed += Number;
            // console.log(pagecount);
            unlockUI();
        };

        $scope.delete = function (loanmaster_gid) {
            var params = {
                loanmaster_gid: loanmaster_gid
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
                    var url = 'api/loan/deleteloanmaster';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                        }
                        else {
                            Notify.alert('Error Occurred While Deleting Loan!', {
                                status: 'warning',
                                pos: 'top-right',
                                timeout: 3000
                            });
                            activate();
                        }
                    });
                    SweetAlert.swal('Deleted Successfully!');
                }

            });
        };

        $scope.popuploan = function () {
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
                $scope.segmentSubmit = function () {
                    var params = {
                      
                        remarks: $scope.remarks
                    }
                    var url = 'api/loan/loanCreate';

                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $modalInstance.close('closed');
                            Notify.alert('Loan Added Successfully..!!', 'success')
                            activate();

                        }
                        else {
                            Notify.alert('Error Occurred While Adding Loan!', 'warning')
                            activate();
                        }
                    });
                    $state.go('app.loanMaster');
                }
            }
        }

        $scope.edit = function (loanmaster_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/myModaledit.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    loanmaster_gid: loanmaster_gid
                }
                var url = 'api/loan/editloanmaster';
                SocketService.getparams(url, params).then(function (resp) {

              
                    $scope.descriptionedit = resp.data.loanTitleedit;
                    $scope.loanmaster_gid = resp.data.loanmaster_gid;
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.segmentUpdate = function (loanmaster_gid) {

                    var params = {
                       
                        loanTitleedit: $scope.descriptionedit,
                        loanmaster_gid: $scope.loanmaster_gid
                    }
                    var url = 'api/loan/updateloanmaster';

                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                            $modalInstance.close('closed');
                            Notify.alert('Loan Updated Successfully..!!', 'success')

                        }
                        else {
                            Notify.alert('Error Occurred While Updating Loan !', 'success')
                            activate();

                        }
                    });
                }
            }

        }
    }
})();