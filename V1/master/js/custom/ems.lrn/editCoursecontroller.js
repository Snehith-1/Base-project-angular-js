/// <reference path="../../../../app/views/ems.ecms/customerEdit.html" />
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('editCoursecontroller', editCoursecontroller);

    editCoursecontroller.$inject = ['$rootScope', '$scope', '$state', '$modal', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function editCoursecontroller($rootScope, $scope, $state, $modal, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'editCoursecontroller';

        activate();

        function activate() {
            var url = 'api/course/category'
            SocketService.get(url).then(function (resp) {
                $scope.category_list = resp.data.categorylist;              
            });
            var url = 'api/course/author'
            SocketService.get(url).then(function (resp) {
                $scope.author_list = resp.data.authorlist;                
            });
            $scope.course_gid = localStorage.getItem('course_gid');
            var url = 'api/course/courseDetail';
            var param = {
                course_gid: $scope.course_gid               
            };
            console.log(param);

            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                $scope.course_gid = resp.data.course_gid;
                $scope.txtcourseref_noedit = resp.data.course_refedit;
                $scope.txtcourse_titleedit = resp.data.course_title_edit;
                $scope.category_gid = resp.data.category_gid;
                $scope.cbocategoryedit = resp.data.category_edit;
                $scope.author_gid = resp.data.author_gid;
                $scope.cboauthoredit = resp.data.author_edit;
                $scope.radio_aredit = resp.data.ar_edit;
                $scope.radio_worsheet = resp.data.worksheet_edit;
                $scope.cbolevel = resp.data.level_edit;
                $scope.txtper_linkedit = resp.data.link_edit;
                $scope.txtshort_descriptionedit = resp.data.short_edit;
                $scope.txtlong_descriptionedit = resp.data.long_edit;
                $scope.txtcourse_slugedit = resp.data.slug_edit;
                $scope.txtcourse_durationedit = resp.data.duration_edit
                unlockUI();
                console.log(resp);
            });
        }
        $scope.updatecourse = function () {
            var category_name = $('#categortyname :selected').text();
            var author_name = $('#authorname :selected').text();

            var params = {
                course_gid: $scope.course_gid,
                ref_no: $scope.txtcourseref_noedit,
                course_title: $scope.txtcourse_titleedit,
                category_gid: $scope.category_gid,
                category_name: category_name,
                author_gid: $scope.author_gid,
                author_name: author_name,
                course_ar: $scope.radio_aredit,
                course_worksheet: $scope.radio_worsheet,
                course_level: $scope.cbolevel,
                course_link: $scope.txtper_linkedit,
                short_description: $scope.txtshort_descriptionedit,
                long_description: $scope.txtlong_descriptionedit,
                course_slug: $scope.txtcourse_slugedit,
                course_duration: $scope.txtcourse_durationedit
            }
            console.log(params);
            var url = 'api/course/courseUpdate';
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    activate()
                    $state.go('app.course');
                    Notify.alert('Course Updated Successfully', 'success');
                }
                else {
                    activate()
                    $state.go('app.course');
                    Notify.alert('Error Occurred while updating the course', 'failure');
                }
            });
        }
        $scope.coursebackedit = function () {
            $state.go('app.course')
        }
    }
})();
