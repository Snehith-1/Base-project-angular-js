(function () {
    'use strict';

    angular
        .module('angle')
        .controller('courseDashboardcontroller', courseDashboardcontroller);

    courseDashboardcontroller.$inject = ['$rootScope', '$scope', '$state', '$modal', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function courseDashboardcontroller($rootScope, $scope, $state, $modal, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'courseDashboardcontroller';

        activate();

        function activate() {
            var params = {
                course_gid: localStorage.getItem('course_gid')
            };
            console.log(params);
            var url = 'api/course/courseDetail';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.coursedetails = resp.data;
                console.log(resp);
            });
            var url = 'api/course/sectionSummary';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.section_list = resp.data.section_list;
            });
            var url = 'api/course/accessories';
            SocketService.get(url).then(function (resp) {
                $scope.packageaccessories_list = resp.data.packageaccessories_list
            });
            var url = 'api/course/packageview';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status = true) {
                    $scope.courseview = true;
                    $scope.courseedit = false;
                    $scope.packageview = true;
                    $scope.packageedit = false;
                    $scope.packagedeatils = resp.data;
                }
                else {
                    $scope.courseview = false;
                    $scope.courseedit = false;
                    $scope.packageview = false;
                    $scope.packageedit = false;
                }

            });
        }
        $scope.back = function () {
            $state.go('app.course')
        }
        $scope.lesson = function (val) {
            $scope.section_gid = localStorage.setItem('section_gid', val);
            $state.go('app.addlesson')
        }
        $scope.editcourse = function (param) {
            if (param == 'editcourse') {
               
                var url = 'api/course/category'
                SocketService.get(url).then(function (resp) {
                    $scope.category_list = resp.data.categorylist;
                });
                var url = 'api/course/author'
                SocketService.get(url).then(function (resp) {
                    $scope.author_list = resp.data.authorlist;
                });
                var url = 'api/course/courseDetail';
                var param = {
                    course_gid: localStorage.getItem('course_gid')
                };
                SocketService.getparams(url, param).then(function (resp) {
                    if (resp.data.status == true) {
                        $scope.course_gid = resp.data.course_gid;
                        $scope.txtcourseref_noediting = resp.data.course_refedit;
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
                        $scope.txtcourse_durationedit = resp.data.duration_edit,
                        $scope.featured_courseedit = resp.data.featured_courseedit
                        console.log(resp);
                    }

                });
                $scope.courseview = false;
                $scope.courseedit = true;
            }
            else {
                $scope.courseview = true;
                $scope.courseedit = false;
            }
        }
        $scope.cancelcourse = function (param) {
            if (param == 'cancelcourse') {
                $scope.courseview = true;
                $scope.courseedit = false;
            }
        }
        $scope.editpackage = function (param) {
            if (param == 'edit') {
                $scope.packageview = false;
                $scope.packageedit = true;
                var params = {
                    course_gid: localStorage.getItem('course_gid')
                };
                var url = 'api/course/accessories'
                SocketService.get(url).then(function (resp) {
                    $scope.packageaccessories_list = resp.data.packageaccessories_list;
                });
                var url = 'api/course/packageview';
                SocketService.getparams(url, params).then(function (resp) {
                    console.log(resp);
                    if (resp.data.status = true) {
                        $scope.txtpackage_refnoedit = resp.data.package_refno;
                        $scope.txtpackage_titleedit = resp.data.package_title;
                        $scope.txtpackage_priceedit = resp.data.package_stdprice;
                        $scope.txtdiscounted_priceedit = resp.data.package_disprice;
                        $scope.txtauthor_costedit = resp.data.package_authorcost;
                        $scope.accessories_gid = resp.data.accessories_gid;
                        $scope.cboaccessoriesedit = resp.data.accessories_edit;
                    }
                    else {
                        $scope.packageview = false;
                        $scope.packageedit = false;
                    }

                });
            }
            else {

            }
        }
        $scope.cancelpackage = function (param) {
            if (param == 'cancelpackage') {
                $scope.courseview = true;
                $scope.courseedit = false;
                $scope.packageview = true;
                $scope.packageedit = false;
            }
        }
        $scope.addSection = function () {
            var modalInstance = $modal.open({
                templateUrl: '/mymodalsectionadd.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.sectionSubmit = function () {
                    var params = {
                        course_gid: localStorage.getItem('course_gid'),
                        section_refno: $scope.txtsection_refno,
                        section_title: $scope.txtsection_title,
                        short_description: $scope.txtshort_description
                    }
                    console.log(params);
                    var url = 'api/course/addSection'
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $scope.txtsection_refno = "";
                            $scope.txtsection_title = "";
                            $scope.txtshort_description = "";
                            activate();
                            $modalInstance.close('closed');
                            Notify.alert('Section Added Successfully', 'success');

                        }
                        else {

                            Notify.alert('Error Occured While Adding the Section', 'failure');
                        }
                    });
                }
            }
        }
        $scope.edit = function (section_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/mymodalsectionedit.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var param = {
                    section_gid: section_gid,
                }
                console.log(param);
                var url = 'api/course/sectiondetails'
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.section_gid = resp.data.section_gid;
                    $scope.txtsectionref_edit = resp.data.section_refno;
                    $scope.txtsectiontitle_edit = resp.data.section_title;
                    $scope.txtsectiondesc_edit = resp.data.short_description
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.updateSection = function (section_gid) {
                    var params = {
                        section_gid: $scope.section_gid,
                        section_refno: $scope.txtsectionref_edit,
                        section_title: $scope.txtsectiontitle_edit,
                        short_description: $scope.txtsectiondesc_edit
                    }
                    console.log(params);
                    var url = 'api/course/updateSection'
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                            $modalInstance.close('closed');
                            Notify.alert('Section Updated Successfully..!!', 'success')
                        }
                        else {
                            Notify.alert('Error Occurred While Updating Section !', 'Warning')
                            activate();
                        }
                    });
                }
            }
        }
        $scope.packageUpdate = function () {
            var accessories_name = $('#accessoriesname :selected').text();
            var params = {
                course_gid: localStorage.getItem('course_gid'),
                package_refnoedit: $scope.txtpackage_refnoedit,
                package_titleedit: $scope.txtpackage_titleedit,
                package_stdpriceedit: $scope.txtpackage_priceedit,
                package_dispriceedit: $scope.txtdiscounted_priceedit,
                package_authorcostedit: $scope.txtauthor_costedit,
                accessories_gid: $scope.accessories_gid,
                accessories_name:accessories_name
            }
            console.log(params);
            var url = 'api/course/updatepackagefeaturedCourse';
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    activate();
                    $scope.txtpackage_refno = "";
                    $scope.txtpackage_title = "";
                    $scope.txtpackage_price = "";
                    $scope.txtdiscounted_price = "";
                    $scope.txtauthor_cost = "";
                    $scope.featured_course = "";
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
        $scope.updatecourse = function () {
            $scope.courseview = true;
            $scope.courseedit = false;
            var category_name = $('#categortyname :selected').text();
            var author_name = $('#authorname :selected').text();

            var params = {
                course_gid: $scope.course_gid,
                ref_no: $scope.txtcourseref_noediting,
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
                course_duration: $scope.txtcourse_durationedit,
                featured_course: $scope.featured_courseedit
            }
            console.log(params);
            //var url = 'api/course/courseUpdate';
            //SocketService.post(url, params).then(function (resp) {
            //    if (resp.data.status == true) {
            //        activate()
            //        $state.go('app.courseDashboard');
            //        Notify.alert('Course Updated Successfully', 'success');
            //    }
            //    else {
            //        activate()
            //        $state.go('app.courseDashboard');
            //        Notify.alert('Error Occurred while updating the course', 'failure');
            //    }
            //});
        }


        $scope.lessondetails = function (section_gid) {
            var url = 'api/course/lessonSummary';
            var params = {
                section_gid: section_gid
            };


            SocketService.getparams(url, params).then(function (resp) {
                $scope.lesson_list = resp.data.lesson_list;
            });
        }
        $scope.editlesson = function (val) {
            localStorage.setItem('lesson_gid', val);
            $state.go('app.editLesson')
        }
        $scope.deletelesson = function (lesson_gid) {
            var params = {
                lesson_gid: lesson_gid
            }
            console.log(params);
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do You Want To Delete the Record ?',
                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Yes, delete it!',
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    var url = 'api/course/deletelesson';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            SweetAlert.swal('Deleted Successfully!');

                        }
                        else {
                            Notify.alert('Error Occured while deleting this lesson', {
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
        $scope.deletesection = function (section_gid) {
            var params = {
                section_gid: section_gid
            }
            console.log(params);
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do You Want To Delete the Record ?',
                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Yes, delete it!',
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    var url = 'api/course/deleteSection';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            SweetAlert.swal('Deleted Successfully!');

                        }
                        else {
                            Notify.alert('Error Occured while deleting this Section', {
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

    }

})();
