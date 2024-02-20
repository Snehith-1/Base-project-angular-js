(function () {
    'use strict';

    angular
        .module('angle')
        .controller('addAuthorcontroller', addAuthorcontroller);

    addAuthorcontroller.$inject = ['$rootScope', '$scope', '$state', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function addAuthorcontroller($rootScope, $scope, $state, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'addAuthorcontroller';

        activate();

        function activate() { }
        $scope.authorback = function (val) {
            $state.go('app.authorSummary');
        };

        $scope.authorSubmit = function () {
            var params = {
                author_firstname: $scope.txtfirstname,
                author_lastname: $scope.txtlastname,
                author_emailid: $scope.txtemail,
                author_mobno: $scope.txtmobileno,
                author_code: $scope.txtauthcode,
                author_address1: $scope.txtaddress1,
                acc_name: $scope.txtaccname,
                ifsc_code: $scope.txtifsccode,
                acc_no: $scope.txaccno,
                bank_name: $scope.txtbankname
            }
        console.log(params);
            var url = 'api/author/addAuthor';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    Notify.alert('Author Added Successfully..!!', 'success')
                    activate();
                    

                }
                else {
                    unlockUI();
                    Notify.alert('Author Code already Exist!', 'warning')
                    activate();
                }
            });
            var url = 'api/author/authorSummary';
            SocketService.get(url).then(function (resp) {
                $scope.author_list = resp.data.author_list;
            });
            $state.go('app.authorSummary');
        }
    }
})();

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

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('addSectioncontroller', addSectioncontroller);

    addSectioncontroller.$inject = ['$rootScope', '$scope', '$state', '$modal', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function addSectioncontroller($rootScope, $scope, $state, $modal, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'addSectioncontroller';

        activate();

        function activate() { }
        $scope.sectionback = function () {
            $state.go('app.sectionsummary')
        }
    }
})();


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('addStudentcontroller', addStudentcontroller);

    addStudentcontroller.$inject = ['$rootScope', '$scope', '$state', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function addStudentcontroller($rootScope, $scope, $state, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'addStudentcontroller';

        activate();

        function activate() {
            vm.open = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.opened = true;
            };

            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };
        }
        $scope.studentback = function (val) {
            $state.go('app.studentSummary');
        };
        $scope.studentSubmit = function () {
            var dob = new Date();
            dob.setFullYear($scope.dob.getFullYear());
            dob.setMonth($scope.dob.getMonth());
            dob.setDate($scope.dob.getDate());

            var params = {
                student_firstname: $scope.txtfirstname,
                student_lastname: $scope.txtlastname,
                student_emailid: $scope.txtemail,
                student_mobno: $scope.txtmobileno,
                student_code: $scope.txtstucode,
                student_dob: dob
            }
            //console.log(params);
            var url = 'api/student/addStudent';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    Notify.alert('Student Added Successfully..!!', 'success')
                    activate();


                }
                else {
                    unlockUI();
                    Notify.alert('Error Occurred While Adding Student!', 'warning')
                    activate();
                }
            });
            var url = 'api/student/studentSummary';
            SocketService.get(url).then(function (resp) {
                $scope.student_list = resp.data.student_list;
            });
            $state.go('app.studentSummary');
        }
    }
})();


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('authorSummarycontroller', authorSummarycontroller);

    authorSummarycontroller.$inject = ['$rootScope', '$scope', '$state', 'SweetAlert', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function authorSummarycontroller($rootScope, $scope, $state,SweetAlert, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'authorSummarycontroller';

        activate();

        function activate() {
            var url = 'api/author/authorSummary';
            SocketService.get(url).then(function (resp) {
                $scope.author_list = resp.data.author_list;
            });
            console.log($scope.author_list);
        }
        $scope.addauthor = function () {
            $state.go('app.addAuthor');
        };

        $scope.edit = function (val) {
            $scope.author_gid = val;
            $scope.author_gid = localStorage.setItem('author_gid', val);
            $state.go('app.editAuthor');
        };
        $scope.view = function (val) {
            $scope.author_gid = val;
            $scope.author_gid = localStorage.setItem('author_gid', val);
            $state.go('app.viewAuthor');
        };
        $scope.delete = function (author_gid) {
            var params = {
                author_gid: author_gid
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
                    var url = 'api/author/authorDelete';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            SweetAlert.swal('Deleted Successfully!');
                            activate();
                        }
                        else {
                            Notify.alert('you can not delete this author because he had created the Course', {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                    });
                    
                }

            });
        };
       
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('categorycontroller', categorycontroller);

    categorycontroller.$inject = ['$rootScope', '$scope', '$state', '$modal', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function categorycontroller($rootScope, $scope, $state, $modal, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'categorycontroller';

        activate();

        function activate() {

            var url = 'api/category/categorySummary';
            SocketService.get(url).then(function (resp) {
                $scope.category_list = resp.data.category_list;
                
            });
        }

        $scope.delete = function (category_gid) {
            var params = {
                category_gid: category_gid
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
                    var url = 'api/category/categoryDelete';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            SweetAlert.swal('Deleted Successfully!');
                        }
                        else {
                            Notify.alert('You can not delete this category because course had been created under this category', {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 4000
                            });
                        }
                        activate();
                    });
                   
                }

            });
        };
        $scope.popupcategory = function () {
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
                $scope.addcategory = function () {
                    var params = {
                        category_code: $scope.txtcategorycode,
                        category_name: $scope.txtcategoryname
                    }

                    var url = 'api/category/addCategory';

                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $modalInstance.close('closed');
                            Notify.alert('Category Added Successfully..!!', 'success')
                        }
                        else {
                            Notify.alert('Category code already exist!', 'warning')
                        }
                        activate();
                    });
                    $state.go('app.category');
                }
            }
        }
        $scope.edit = function (category_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/myModaledit.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    category_gid: category_gid
                }
                var url = 'api/category/categoryUpdatedetails';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txtcategorycodeedit = resp.data.category_codeedit;
                    $scope.txtcategorynameedit = resp.data.category_nameedit;
                    $scope.category_gid = resp.data.category_gid;
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.updatecategory = function () {

                    var params = {
                        category_codeedit: $scope.txtcategorycodeedit,
                        category_nameedit: $scope.txtcategorynameedit,
                        category_gid: category_gid
                    }
                    console.log();
                    var url = 'api/category/updateCategory';

                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $modalInstance.close('closed');
                            Notify.alert('Category Updated Successfully..!!', 'success')
                        }
                        else {
                            Notify.alert('Error Occurred While Updating Category !', 'success')
                        }
                        activate();
                    });
                }
            }

        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('coursecontroller', coursecontroller);

    coursecontroller.$inject = ['$rootScope', '$scope', '$state', '$modal', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function coursecontroller($rootScope, $scope, $state, $modal, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'coursecontroller';

        activate();

        function activate() {

        }
        $scope.addcourse = function () {

            $state.go('app.addcourse');
        };
        $scope.viewfeaturedcourse = function () {

            $state.go('app.sectionsummary');
        };
        $scope.section = function () {
            $state.go('app.sectionsummary')
        }
        $scope.edit = function (val) {
            $state.go('app.editCourse')
        }
    }
})();

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

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('editAuthorcontroller', editAuthorcontroller);

    editAuthorcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route'];

    function editAuthorcontroller($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'editAuthorcontroller';

        activate();

        function activate() {

            $scope.author_gid = localStorage.getItem('author_gid');
            var url = 'api/author/authorUpdatedetails';
            var param = {
                author_gid: $scope.author_gid
            };

          
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {

                $scope.txtfirstname = resp.data.author_firstnameedit;
                $scope.txtlastname = resp.data.author_lastnameedit;
                $scope.txtauthcode = resp.data.author_codeedit;
                $scope.txtemail = resp.data.author_emailidedit;
                $scope.txtmobileno = resp.data.author_mobnoedit;
                $scope.txtaddress1 = resp.data.author_address1edit;
                $scope.txtaccname = resp.data.acc_nameedit;
                $scope.txaccno = resp.data.acc_noedit;
                $scope.txtifsccode = resp.data.ifsc_codeedit;
                $scope.txtbankname = resp.data.bank_nameedit;
                unlockUI();
                console.log(resp.data);
            });
        }

        $scope.editauthorback = function () {
            $state.go('app.authorSummary');
        }

        $scope.editauthorupdate = function () {

            var params = {
                author_gid: $scope.author_gid,
                author_firstnameedit: $scope.txtfirstname,
                author_lastnameedit: $scope.txtlastname,
                author_codeedit: $scope.txtauthcode,
                author_emailidedit: $scope.txtemail,
                author_mobnoedit: $scope.txtmobileno,          
                author_address1edit: $scope.txtaddress1,
                acc_nameedit: $scope.txtaccname,
                acc_noedit: $scope.txaccno,
                ifsc_codeedit: $scope.txtifsccode,
                bank_nameedit: $scope.txtbankname

            }

            var url = 'api/author/updateAuthor';
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    activate();
                    $state.go('app.authorSummary');
                    Notify.alert('Author Updated Successfully..!!', 'success')
                }

                else {
                    Notify.alert('Error Occurred While Updating Author !')
                }
                activate();
            });
        }
    }
})();

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

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('editSectioncontroller', editSectioncontroller);

    editSectioncontroller.$inject = ['$rootScope', '$scope', '$state', '$modal', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function editSectioncontroller($rootScope, $scope, $state, $modal, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'editSectioncontroller';

        activate();

        function activate() { }
        $scope.sectionbackedit = function () {
            $state.go('app.sectionSummary')
        }
    }
})();


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('editStudentcontroller', editStudentcontroller);

    editStudentcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route'];

    function editStudentcontroller($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'editStudentcontroller';

        activate();

        function activate() {
            vm.open = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.opened = true;
            };

            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };

            $scope.student_gid = localStorage.getItem('student_gid');
            var url = 'api/student/studentUpdatedetails';
            var param = {
                student_gid: $scope.student_gid
            };


            lockUI();
            SocketService.getparams(url, param).then(function (resp) {

                $scope.txtfirstname = resp.data.student_firstnameedit;
                $scope.txtlastname = resp.data.student_lastnameedit;
                $scope.txtstucode = resp.data.student_codeedit;
                $scope.txtemail = resp.data.student_emailidedit;
                $scope.txtmobileno = resp.data.student_mobnoedit;
                $scope.dobedit = resp.data.student_dobedit;
                unlockUI();
                console.log(resp.data);
            });
        }

        $scope.editstudentback = function () {
            $state.go('app.studentSummary');
        }

        $scope.editstudentupdate = function () {
            var dobedit = new Date();
            dobedit.setFullYear($scope.dobedit.getFullYear());
            dobedit.setMonth($scope.dobedit.getMonth());
            dobedit.setDate($scope.dobedit.getDate());

            var params = {
                student_gid: $scope.student_gid,
                student_firstnameedit: $scope.txtfirstname,
                student_lastnameedit: $scope.txtlastname,
                student_codeedit: $scope.txtstucode,
                student_emailidedit: $scope.txtemail,
                student_mobnoedit: $scope.txtmobileno,
                student_dobedit: dobedit
            }

            var url = 'api/student/updateStudent';
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    activate();
                    $state.go('app.studentSummary');
                    Notify.alert('Student Updated Successfully..!!', 'success')
                }

                else {
                    Notify.alert('Error Occurred While Updating Student !')
                }
                activate();
            });
        }
    }
})();


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('featuredCoursecontroller', featuredCoursecontroller);

    featuredCoursecontroller.$inject = ['$rootScope', '$scope', '$state', '$modal', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function featuredCoursecontroller($rootScope, $scope, $state, $modal, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'featuredCoursecontroller';

        activate();

        function activate() { }
        $scope.featuredcourseback = function () {
            $state.go('app.course')
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('lessonSummarycontroller', lessonSummarycontroller);

    lessonSummarycontroller.$inject = ['$rootScope', '$scope', '$state', '$modal', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function lessonSummarycontroller($rootScope, $scope, $state, $modal, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'lessonSummarycontroller';

        activate();

        function activate() { }
        $scope.addlesson = function () {
            $state.go('app.addlesson')
        }
        $scope.edit = function (val) {
            $state.go('app.editlesson')
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('packageAccessoriescontroller', packageAccessoriescontroller);

    packageAccessoriescontroller.$inject = ['$rootScope', '$scope', '$state', '$modal', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function packageAccessoriescontroller($rootScope, $scope, $state, $modal, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'packageAccessoriescontroller';

        activate();

        function activate() {
            var url = 'api/accessories/accessoriesSummary';
            SocketService.get(url).then(function (resp) {
                $scope.accessories_list = resp.data.accessories_list;

            });
        }
        $scope.delete = function (packageaccessories_gid) {
            var params = {
                packageaccessories_gid: packageaccessories_gid
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
                    var url = 'api/accessories/accessoriesDelete';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            SweetAlert.swal('Deleted Successfully!');
                        }
                        else {
                            Notify.alert('You can not delete this category because course had been created under this category', {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 4000
                            });
                        }
                        activate();
                    });

                }

            });
        };
        $scope.popupaccessories = function () {
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
                $scope.addaccessories = function () {
                    var params = {
                        accessories_code: $scope.txtaccessories_code,
                        accessories_name: $scope.txtaccessories_name,
                        accessories_amnt: $scope.txtaccessories_amnt
                    }

                    var url = 'api/accessories/addAccessories';

                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $modalInstance.close('closed');
                            Notify.alert('Accessories Added Successfully..!!', 'success')
                        }
                        else {
                            Notify.alert('Accessories code already exist!', 'warning')
                        }
                        activate();
                    });
                    $state.go('app.Accessories');
                }
            }
        }
        $scope.edit = function (packageaccessories_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/myModaledit.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    packageaccessories_gid: packageaccessories_gid
                }
                var url = 'api/accessories/accessoriesUpdatedetails';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.accessories_codeedit = resp.data.accessories_codeedit;
                    $scope.accessories_nameedit = resp.data.accessories_nameedit;
                    $scope.txtaccessories_amntedit = resp.data.txtaccessories_amntedit;
                    $scope.packageaccessories_gid = resp.data.packageaccessories_gid;
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.updateaccessories = function () {

                    var params = {
                        accessories_codeedit: $scope.accessories_codeedit,
                        accessories_nameedit: $scope.accessories_nameedit,
                        txtaccessories_amntedit: $scope.txtaccessories_amntedit,
                        packageaccessories_gid: packageaccessories_gid
                    }
                    console.log();
                    var url = 'api/accessories/updateAccessories';

                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $modalInstance.close('closed');
                            Notify.alert('Accessories Updated Successfully..!!', 'success')
                        }
                        else {
                            Notify.alert('Error Occurred While Updating Accessories !', 'success')
                        }
                        activate();
                    });
                }
            }

        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('sectionSummarycontroller', sectionSummarycontroller);

    sectionSummarycontroller.$inject = ['$rootScope', '$scope', '$state', '$modal', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function sectionSummarycontroller($rootScope, $scope, $state, $modal, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'sectionSummarycontroller';

        activate();

        function activate() { }
        $scope.lesson = function () {
            $state.go('app.lessonSummary')
        }
        $scope.section = function () {
            $state.go('app.addSection')
        }
        $scope.edit = function (val) {
            $state.go('app.editSection')
        }
    }
})();


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('studentSummarycontroller', studentSummarycontroller);

    studentSummarycontroller.$inject = ['$rootScope', '$scope', '$state', '$modal','SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function studentSummarycontroller($rootScope, $scope, $state, $modal,SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'studentSummarycontroller';

        activate();

        function activate() {
            var url = 'api/student/studentSummary';
            SocketService.get(url).then(function (resp) {
                $scope.student_list = resp.data.student_list;
            });
        }
        $scope.addstudent = function () {
            $state.go('app.addStudent');
        };
        $scope.edit = function (val) {
            $scope.student_gid = val;
            $scope.student_gid = localStorage.setItem('student_gid', val);
            $state.go('app.editStudent');
        };
        $scope.view = function (val) {
            $scope.student_gid = val;
            $scope.student_gid = localStorage.setItem('student_gid', val);
            $state.go('app.viewStudent');
        };
        $scope.delete = function (student_gid) {
            var params = {
                student_gid: student_gid
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
                    var url = 'api/student/studentDelete';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            SweetAlert.swal('Deleted Successfully!');
                            activate();
                        }
                        else {
                            Notify.alert('Error Occurred While Deleting Author!', {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                    });

                }

            });
        };


    }
})();


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('viewAuthorcontroller', viewAuthorcontroller);

    viewAuthorcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function viewAuthorcontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'viewAuthorcontroller';

        activate();

        function activate() {
            var params = {
                author_gid: localStorage.getItem('author_gid')
            };
            var url = 'api/author/authorUpdatedetails';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.author_details = resp.data;
            });
        }
        $scope.back = function () {
            $state.go('app.authorSummary');
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('viewCoursecontroller', viewCoursecontroller);

    viewCoursecontroller.$inject = ['$rootScope', '$scope', '$state', '$modal', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function viewCoursecontroller($rootScope, $scope, $state, $modal, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'viewCoursecontroller';

        activate();

        function activate() { }
        $scope.viewcourseback = function () {
            $state.go('app.lessonSummary')
        }

    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('viewLessoncontroller', viewLessoncontroller);

    viewLessoncontroller.$inject = ['$rootScope', '$scope', '$state', '$modal', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function viewLessoncontroller($rootScope, $scope, $state, $modal, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'viewLessoncontroller';

        activate();

        function activate() { }
        $scope.viewlessonback = function () {
            $state.go('app.lessonSummary')
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('viewSectioncontroller', viewSectioncontroller);

    viewSectioncontroller.$inject = ['$rootScope', '$scope', '$state', '$modal', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function viewSectioncontroller($rootScope, $scope, $state, $modal, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'viewSectioncontroller';

        activate();

        function activate() { }
        $scope.viewsectionback = function () {
            $state.go('app.sectionsummary')
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('viewStudentcontroller', viewStudentcontroller);

    viewStudentcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function viewStudentcontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'viewStudentcontroller';

        activate();

        function activate() {
            var params = {
                student_gid: localStorage.getItem('student_gid')
            };
    
            var url = 'api/student/studentUpdatedetails';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.student_details = resp.data;
            });
        }
        $scope.back = function () {
            $state.go('app.studentSummary');
        }
    }
})();
