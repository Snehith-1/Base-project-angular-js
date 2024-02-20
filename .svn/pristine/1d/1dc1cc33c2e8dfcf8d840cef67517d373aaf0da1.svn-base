(function () {
    'use strict';

    angular
        .module('angle')
        .controller('courseSummaryController', courseSummaryController);

    courseSummaryController.$inject = ['$rootScope', '$scope', '$state', '$modal', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function courseSummaryController($rootScope, $scope, $state, $modal, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'courseSummaryController';

        activate();

        function activate() {
            var url = 'api/course/courseSummary'
            SocketService.get(url).then(function (resp) {
                $scope.course_list = resp.data.course_list;
                $scope.course = true;
                $scope.featuredCourse = false;
            });
         }
        $scope.addcourse = function () {

            $state.go('app.addcourse');
        };
        $scope.featuredcourses = function () {
            var url = 'api/course/featuredcourseSummary'
            SocketService.get(url).then(function (resp) {
                $scope.featuredcourse_list = resp.data.featuredcourse_list;
                $scope.course = false;
                $scope.featuredCourse = true;

            });
        };
        $scope.viewcourses = function () {
            var url = 'api/course/courseSummary'
            SocketService.get(url).then(function (resp) {
                $scope.course_list = resp.data.course_list;
                $scope.course = true;
                $scope.featuredCourse = false;

            });
        }
        $scope.section = function (val) {
            $scope.course_gid = localStorage.setItem('course_gid', val);
           
            $state.go('app.addSection')
        }
        $scope.edit = function (val) {
            localStorage.setItem('course_gid', val);
            $state.go('app.editCourse')
        }
        $scope.view = function (val) {
            localStorage.setItem('course_gid', val);
            $state.go('app.courseDashboard');
        }
        $scope.tag = function (course_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/myModalfeatured.html',
                controller: ModalInstanceCtrl,
                size: 'sm'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params ={
                    course_gid: course_gid
                }
                $scope.featuredSubmit = function (val) {
                    $scope.course_gid = val;
                    course_gid: $scope.course_gid;
                    console.log(course_gid);
                    var url = 'api/course/updatefeaturedCourse';
                    SocketService.post(url, params).then(function (resp) {
                        if(resp.data.status==true){
                           
                            $modalInstance.close('closed');
                            Notify.alert('Course Tagged Successfully')
                        }
                        else {
                            Notify.alert('Error Occurred While Tagging the Course !', {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 4000
                            });
                           
                        }
                        activate();
                    });
                }
                $scope.ok = function () {
                    $modalInstance.close('closed');
                }
            }
        }

        $scope.packageaccessories = function (course_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/myModalpackageaccessories.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                }
                $scope.isShowHide = function (param) {
                    console.log(param);
                    if (param == "show") {
                        $scope.Kit = true;
                    }
                    else if (param == "hide") {
                        $scope.Kit = false;
                        $scope.txtkit_cost = "";
                    }
                }
                $scope.isShowHidesupport = function (param) {
                    if (param == "show") {
                        $scope.support = true;
                    }
                    else if (param == "hide") {
                        $scope.support = false;
                        $scope.txtsupport_cost = "";
                    }
                }
            }

        }

        $scope.deletecourse = function (course_gid) {
                var params = {
                    course_gid: course_gid
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
                        var url = 'api/course/coursedelete';
                        SocketService.getparams(url, params).then(function (resp) {
                            if (resp.data.status == true) {
                                SweetAlert.swal('Deleted Successfully!');
                                
                            }
                            else {
                                Notify.alert('you can not delete this Course because it contain Sections', {
                                    status: 'warning',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                               
                            }
                            activate();
                        });

                    }

                });
            };
    
        $scope.package = function (course_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/myModalpackage.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.packageSubmit = function (course_gid) {
                    $scope.course_gid = val;
                    course_gid = $scope.course_gid;
                    //var params = {
                    //    course_gid: course_gid,                      
                    //    package_refno:$scope.txtpackage_refno,
                    //    package_title:$scope.txtpackage_title,
                    //    package_stdprice:$scope.txtpackage_price,
                    //    package_disprice:$scope.txtdiscounted_price,
                    //    package_authorcost:$scope.txtauthor_cost
                    //}
                }
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                var url = 'api/course/coursePackage';
                SocketService.post(url, course_gid).then(function (resp) {
                    if (resp.data.status == true) {
                        activate();
                        $modalInstance.close('closed');
                        Notify.alert('Package Added Successfully..!!', 'success')
                    }
                    else {
                        Notify.alert('Error Occurred While Adding the Package !', {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 5000
                        });
                        activate();
                    }                
                });
            }

        }
    }
})();
