(function () {
    'use strict';

    angular
        .module('angle')
        .controller('deferralcontroller', deferralcontroller);

    deferralcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function deferralcontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'deferralcontroller';
        activate();
        function activate() {
            $scope.totalDisplayed=100;
            var url = 'api/deferral/deferralmasterSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.deferral = resp.data.deferral_list;
                unlockUI();
                console.log($scope.total);
                // new code start   
                if ($scope.deferral == null) {
                                        $scope.total = 0;
                                        $scope.totalDisplayed = 0;
                                    }
                                    else {
                                        $scope.total = $scope.deferral.length;
                                        if ($scope.deferral.length < 100) {
                                            $scope.totalDisplayed = $scope.deferral.length;
                                        }
                                    }
                    // new code end
                // $scope.total=$scope.deferral.length;
            });
           
      console.log($scope.totalDisplayed);
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
            if ($scope.deferral != null) {
       
                        if (pagecount < $scope.deferral.length) {
                            $scope.totalDisplayed += Number;
                            if($scope.deferral.length<$scope.totalDisplayed){
                                $scope.totalDisplayed =$scope.deferral.length;
                                Notify.alert(" Total Summary " + $scope.deferral.length + " Records Only", "warning");
                            }
                            unlockUI();
                        }
                        else {
                            unlockUI();
                            Notify.alert(" Total Summary " + $scope.deferral.length + " Records Only", "warning");
                            return;
                        }
                    }
                    // new code end
                    // $scope.totalDisplayed += Number;
                    // console.log(pagecount);
                    unlockUI();
                };      
        /* ADD DEFERRAL */
        $scope.popupdeferral = function () {
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

                $scope.deferralSubmit = function () {
                    if ($scope.comments == undefined) {
                        $scope.comments = ""
                    }

                    var params = {
                        deferral_code: $scope.deferral_code,
                        deferral_name: $scope.deferral_name,
                        criticallity: $scope.criticality,
                        comments: $scope.comments
                    }

                    var url = 'api/deferral/deferralmasterSubmit';
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {

                            $modalInstance.close('closed');
                            Notify.alert('Deferral Created Successfully..!!', {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                        else {

                        }
                    });
                }
            }
        }
      

        /* EDIT DEFERRAL */
        $scope.edit = function (deferral_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/myModaledit.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.deferral_gid = deferral_gid;
                $scope.deferral_gid = localStorage.setItem('deferral_gid', deferral_gid);
                var params = {
                    deferral_gid: deferral_gid
                }
                //console.log(deferral_gid);
                var url = 'api/deferral/Getdeferralupdate';
                SocketService.getparams(url, params).then(function (resp) {
                    console.log(params);
                    $scope.deferralCodeedit = resp.data.deferralCodeedit;
                    $scope.deferralNameedit = resp.data.deferralNameedit;
                    $scope.criticality = resp.data.criticallity;
                    $scope.commentsEdit = resp.data.comments;
                    $scope.deferral_gid = resp.data.deferral_gid;

                });

                $scope.deferralUpdate = function () {

                    var params = {
                        deferralCodeedit: $scope.deferralCodeedit,
                        deferralNameedit: $scope.deferralNameedit,
                        criticallity: $scope.criticality,
                        comments: $scope.commentsEdit,
                        deferral_gid: $scope.deferral_gid
                    }
                    var url = 'api/deferral/deferralUpdate';
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                            $modalInstance.close('closed');
                            Notify.alert('Deferral Updated Successfully..!!', {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            //SweetAlert.swal('Deferral Updated Successfully..!!', 'success');

                        }
                        else {
                            SweetAlert.swal('Error Occurred While Updating Deferral !', 'warning');


                        }
                    });
                }

            }
        }
        /* DELETE DEFERRAL */
        $scope.delete = function (deferral_gid) {
            $scope.deferral_gid = localStorage.setItem('deferral_gid', deferral_gid);
            var params = {
                deferral_gid: deferral_gid
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
                    var url = 'api/deferral/deferralDelete';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                        }
                        else {
                            Notify.alert('Error Occurred While Deleting Customer!', {
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
        
        }

    }


})();