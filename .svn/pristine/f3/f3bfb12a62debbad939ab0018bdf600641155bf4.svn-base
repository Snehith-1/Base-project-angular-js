(function () {
    'use strict';

    angular
        .module('angle')
        .controller('addCoursecontroller', addCoursecontroller);

    addCoursecontroller.$inject = ['$rootScope', '$scope', '$state', '$modal', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function addCoursecontroller($rootScope, $scope, $state, $modal, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'addCoursecontroller';

        activate();

        function activate() {
            var url = 'api/course/category';
            SocketService.get(url).then(function (resp) {
                $scope.category_list = resp.data.categorylist;
            });
            var url = 'api/course/author';
            SocketService.get(url).then(function (resp) {
                $scope.author_list = resp.data.authorlist;
            });
            var url = 'api/course/accessories';
            SocketService.get(url).then(function (resp) {
                $scope.packageaccessories_list = resp.data.packageaccessories_list
            });
        }
        $scope.courseSubmit = function () {
            var category = $('#categoryname :selected').text();
            var author = $('#authorname :selected').text();
            var accessories = $('#package_accessories :selected').text();
            var params = {
                ref_no: $scope.txtcourse_ref,
                course_title: $scope.txtcourse_title,
                category_gid:$scope.cbocategory,
                category_name:category,
                author_gid:$scope.cboauthor,
                author_name: author,
                accessories_gid: $scope.cboaccessories,
                accessories_name: accessories,
                course_ar: $scope.radio_ar,
                course_worksheet: $scope.radio_worsheet,
                course_level: $scope.cbolevel,
                course_link: $scope.txtpermanant_link,
                short_description: $scope.txtshort_description,
                long_description: $scope.txtdetail_description,
                course_slug: $scope.txtcourseslug,
                featured_course: $scope.featured_course,
                course_duration: $scope.txtcourse_duration,
                package_refno: $scope.txtpackage_refno,
                package_title: $scope.txtpackage_title,
                package_stdprice: $scope.txtpackage_price,
                package_disprice: $scope.txtdiscounted_price,
                package_authorcost: $scope.txtauthor_cost              
            }
            console.log(params);
            var url = 'api/course/addCourse';
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.txtpackage_refno = "";
                    $scope.txtpackage_title = "";
                    $scope.txtpackage_price = "";
                    $scope.txtdiscounted_price = "";
                    $scope.txtauthor_cost = "";
                    $scope.featured_course = "";
                    $scope.txtcourse_ref = "";
                    $scope.txtcourse_title = "";
                    $scope.cbocategory = "";
                    $scope.cboauthor = "";
                    $scope.cboaccessories = "";
                    $scope.radio_ar = "";
                    $scope.radio_worsheet = "";
                    $scope.cbolevel = "";
                    $scope.txtpermanant_link = "";
                    $scope.txtshort_description = "";
                    $scope.txtdetail_description = "";
                    $scope.txtcourseslug = "";
                    Notify.alert(resp.data.message,{
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                });
                   
                }
                else {
                    Notify.alert(rep.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                activate();
            });
            $state.go('app.course');
        }
   
        $scope.courseback = function () {
            $state.go('app.course')
        }
    }

})();
