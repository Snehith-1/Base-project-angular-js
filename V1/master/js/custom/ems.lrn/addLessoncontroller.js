(function () {
    'use strict';

    angular
        .module('angle')
        .controller('addLessoncontroller', addLessoncontroller);

    addLessoncontroller.$inject = ['$rootScope', '$scope', '$state', '$modal', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function addLessoncontroller($rootScope, $scope, $state, $modal, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'addLessoncontroller';

        activate();

        function activate() {
            $scope.course_gid = localStorage.getItem('course_gid');
            var param = {
                course_gid: $scope.course_gid
            };
            console.log(param);
        }

        $scope.lessonback = function () {
            $state.go('app.lessonSummary')
        }
        $scope.lessonSubmit = function () {
            $scope.section_gid = localStorage.getItem('section_gid');
            var param = {
                  course_gid: $scope.course_gid,
                  section_gid: $scope.section_gid,
                  lessonref_no: $scope.txtlesson_refno,
                  lesson_title:$scope.txtlesson_title,
                  lesson_transcript:$scope.txtlesson_transcript,
                  lesson_description:$scope.txtlesson_description,
                  lesson_preview:$scope.radio_preview,
                  video_duration:$scope.txtlesson_duration
              }
              console.log(param);
              var url = 'api/course/addlesson'
              SocketService.post(url, param).then(function (resp) {
                  console.log(resp);
                  if (resp.data.status == true) {
                      $scope.txtlesson_refno = "";
                      $scope.txtlesson_title = "";
                      $scope.txtlesson_transcript = "";
                      $scope.txtlesson_description = "";
                      $scope.radio_preview = "";
                      $scope.txtlesson_duration = "";
                      Notify.alert(resp.data.message, {
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
                  $state.go('app.courseDashboard')
              });
        }
    }
})();
