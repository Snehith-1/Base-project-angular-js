(function () {
    'use strict';

    angular
        .module('angle')
        .controller('editLessoncontroller', editLessoncontroller);

        editLessoncontroller.$inject = ['$rootScope', '$scope', '$state', '$modal', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function editLessoncontroller($rootScope, $scope, $state, $modal, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'editLessoncontroller';

        activate();

        function activate() {
            $scope.view = true;
            $scope.edit = false;
            var param = {
                lesson_gid: localStorage.getItem('lesson_gid')
            }
            console.log(param);
            var url = ('api/course/viewlesson')
            SocketService.getparams(url, param).then(function (resp) {
                $scope.lessondetails = resp.data;
                console.log(resp);
            });

        }
        $scope.editlesson = function (param) {
            if (param == 'editlesson') {
                $scope.view = false;
                $scope.edit = true;
                var param = {
                    lesson_gid: localStorage.getItem('lesson_gid')
                }
                var url = ('api/course/viewlesson')
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.txtlesson_refnoedit = resp.data.lessonref_no;
                    $scope.txtlesson_title = resp.data.lesson_title;
                    $scope.txtlesson_durationedit = resp.data.video_duration;
                    $scope.radio_previewedit = resp.data.lesson_preview;
                    $scope.txtlesson_transcript = resp.data.lesson_transcript;
                    $scope.txtlesson_description = resp.data.lesson_description
                });

            }
        }
        $scope.cancellesson = function () {
            activate();
        }
        $scope.lessonupdate = function (lesson_gid) {
            var params = {
            lesson_gid: lesson_gid,
            lessonref_no:  $scope.txtlesson_refnoedit,
            lesson_title: $scope.txtlesson_title,
            video_duration: $scope.txtlesson_durationedit,
            lesson_preview:$scope.radio_previewedit,
            lesson_transcript:$scope.txtlesson_transcript,
            lesson_description:$scope.txtlesson_description
            }
            var url = ('api/course/updateLesson')
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    Notify.alert('Lesson Updated Successfully', 'success');
                }
                else {
                    Notify.alert('Error Occurred while updating the Lesson', 'failure');
                }
                activate()
                $state.go('app.course');
            });
        }
    }
})();
