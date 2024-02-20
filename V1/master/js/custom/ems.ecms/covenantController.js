(function () {
    'use strict';

    angular
        .module('angle')
        .controller('covenantcontroller', covenantcontroller);

    covenantcontroller.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$filter', 'ngTableParams', '$resource', '$timeout', 'ngTableDataService'];

    function covenantcontroller($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $filter, ngTableParams, $resource, $timeout, ngTableDataService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'covenantcontroller';
       
        activate();


        function activate() {
            $scope.totalDisplayed=100;
            var url = 'api/covenanttype/getcovenanttype';
            SocketService.get(url).then(function (resp) {
                $scope.covenant_data = resp.data.covenanttype_list;
                unlockUI();
                // new code start   
                if ($scope.covenant_data== null) {
                                        $scope.total = 0;
                                        $scope.totalDisplayed = 0;
                                    }
                                    else {
                                        $scope.total = $scope.covenant_data.length;
                                        if ($scope.covenant_data.length < 100) {
                                            $scope.totalDisplayed = $scope.covenant_data.length;
                                        }
                                    }
                    // new code endd
                //$scope.total=$scope.covenant_data.length;
              
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
        if ($scope.covenant_data!= null) {
       
                if (pagecount < $scope.covenant_data.length) {
                    $scope.totalDisplayed += Number;
                    if($scope.covenant_data.length<$scope.totalDisplayed){
                        $scope.totalDisplayed =$scope.covenant_data.length;
                        Notify.alert(" Total Summary " + $scope.covenant_data.length + " Records Only", "warning");
                    }
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(" Total Summary " + $scope.covenant_data.length + " Records Only", "warning");
                    return;
                }
            }
            // new code end
            // $scope.totalDisplayed += Number;
            // console.log(pagecount);
            unlockUI();
        };
        $scope.popupcovenenttype = function () {
            var modalInstance = $modal.open({
                templateUrl: '/covenanttype.html',
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


                //$scope.onselectedchangecriticallity = function (criticallity) {
                //    if (criticallity == "Critical") {
                //        $scope.showval = true;
                //    }
                //    else {
                //        $scope.showval = false;
                //    }
                //}

                $scope.covenantSubmit = function () {
                    if ($scope.comments == undefined) {
                        $scope.comments = ""
                    }

                var params = {
                    covenanttype_name: $scope.covenantTypename,
                    covenanttype_code: $scope.covenantTypecode,
                    criticallity: $scope.criticality,
                    comments: $scope.comments
                }             
                var url = 'api/covenanttype/createcovenanttype';
              
                SocketService.post(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        
                        $modalInstance.close('closed');
                        //$scope.close('popupcovenenttype');
                        Notify.alert('Covenant Type created Successfully..!!', {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        activate();
                  }
                    else {
                        Notify.alert('Error Occurred ', {
                            status: 'warning',
                            pos: 'top-right',
                            timeout: 3000
                        });
                        activate();
                    }
                   
              });
            }
            }
        }

        //Edit Function

        $scope.edit = function (covenanttype_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/covenantedit.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            //var doc = document.getElementById('edit');
            //doc.style.display = 'block';
            $scope.covenanttype_gid = covenanttype_gid;
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                //$scope.customer_gid = customer_gid;
                $scope.covenanttype_gid = localStorage.setItem('covenanttype_gid', covenanttype_gid);
                var params = {
                    covenanttype_gid: covenanttype_gid
                }
                var url = 'api/covenanttype/GetCovenantTypeupdate';
               
                SocketService.getparams(url, params).then(function (resp) {
                   
                    $scope.covenantTypecodeedit = resp.data.covenantTypecodeedit;
                    $scope.covenantTypenameedit = resp.data.covenantTypenameedit;
                    $scope.criticality = resp.data.criticallity;
                    $scope.commentsEdit = resp.data.comments;
                    $scope.covenantTypegid = resp.data.covenanttype_gid;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.cancel = function () {
                    $modalInstance.dismiss('cancel');
                };
                $scope.covenantUpdate = function () {

                    var params = {
                        covenanttypenameedit: $scope.covenantTypenameedit,
                        covenanttypecodeedit: $scope.covenantTypecodeedit,
                        criticallity: $scope.criticality,
                        comments: $scope.commentsEdit,
                        covenanttype_gid: $scope.covenantTypegid
                    }
                    var url = 'api/covenanttype/covenantTypeUpdate';
                   
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            
                            //$scope.close('edit');
                            $modalInstance.close('closed');
                            //SweetAlert.swal('Success!', 'Covenant Type Updated!', 'success');
                            Notify.alert('Covenant Type Updated Successfully..!!', {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                        else {
                            //SweetAlert.swal('Success!', 'Error Occured While Updating Covenant Type', 'success');
                            Notify.alert('Error Occurred ', {
                                status: 'warning',
                                pos: 'top-right',
                                timeout: 3000
                            });
                            activate();
                        }
                    });
                }

            }

        }
    
        $scope.delete = function (covenanttype_gid) {
            var params = {
                covenanttype_gid: covenanttype_gid
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
                    var url = 'api/covenanttype/covenantTypeDelete';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                        }
                        else {
                            Notify.alert('Error Occurred !', {
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



        }
})();