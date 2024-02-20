(function () {
    'use strict';

    angular
        .module('angle')
        .controller('cacApprovalcontroller', cacApprovalcontroller);

    cacApprovalcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$cookies', '$filter', 'DownloaddocumentService'];

    function cacApprovalcontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $cookies, $filter, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'cacApprovalcontroller';

        activate();

        function activate() {
            $scope.release_gid = localStorage.getItem('release_gid');
            var params = {
                release_gid: $scope.release_gid
            };
            var url = 'api/myApprovals/releasedetails';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.releaseapprovaldetails = resp.data;
                $scope.releaseissue_list = resp.data.releaseissue_list;
                $scope.uatlog_list = resp.data.uatlog_list;
                $scope.dependency_list = resp.data.dependency_list;
                $scope.cab_list = resp.data.cab_list;
                $scope.uatdocument_list = resp.data.uatdocument_list;
                $scope.approvaldoc_list = resp.data.ApprovalDocuments_List;
               
              
            });
        }

        // View UAT- Details..//

        $scope.uatdetails = function (issuetracker_gid, id) {
            var params = {
                issuetracker_gid: issuetracker_gid
            };
            var url = 'api/myApprovals/uatdetails';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.releaseissue_list[id][issuetracker_gid] = resp.data.uatlog_list;
            });
        }


        // CAB Approve & Reject .....//
      

        $scope.btn_cabapprove = function (cacapproval_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/getCACApprovalmodal.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
              
                $scope.ok = function () {
                    modalInstance.close('closed');
                };

                $scope.getcacapprovalclick = function () {
                    var params = {
                        cacapproval_gid: cacapproval_gid,
                        approval_remarks: $scope.approval_remarks
                            }
                            var url = 'api/myApprovals/cabapprove';
                            SocketService.post(url, params).then(function (resp) {
                                if (resp.data.status = true) {
                                    Notify.alert(resp.data.message, {
                                        status: 'success',
                                        pos: 'top-center',
                                        timeout: 3000
                                         
                                    });
                                    modalInstance.close('closed');
                                }
                                else {
                                    Notify.alert(resp.data.message, {
                                        status: 'warning',
                                        pos: 'top-center',
                                        timeout: 3000
                                    });
                                    modalInstance.close('closed');
                                }
                                $state.go('app.myApproval');
                            });
                          
                }
            }
        }

        $scope.btn_cabreject = function (cacapproval_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/getCACRejectmodal.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.ok = function () {
                    modalInstance.close('closed');
                };

                $scope.getcacrejectclick = function () {
                    var params = {
                        cacapproval_gid: cacapproval_gid,
                        reject_remarks: $scope.reject_remarks
                            }
                            var url ='api/myApprovals/cabreject';
                            SocketService.post(url, params).then(function (resp) {
                                if (resp.data.status = true) {
                                    //$scope.close('view_cabdetails');
                                    Notify.alert(resp.data.message, {
                                        status: 'success',
                                        pos: 'top-center',
                                        timeout: 3000
                                        
                                    });
                                  
                                    modalInstance.close('closed');
                                }
                                else {
                                    Notify.alert(resp.data.message, {
                                        status: 'warning',
                                        pos: 'top-center',
                                        timeout: 3000
                                    });
                                    modalInstance.close('closed');
                                }
                                $scope.cabapproval = resp.data.cabapproval_list;
                               
                                $state.go('app.myApproval');
                            });
                           
                }
            }
        }

        //$scope.btn_cabapprove = function (cacapproval_gid) {
        //    var params = {
        //        cacapproval_gid: cacapproval_gid
        //    }
        //    var url = 'api/myApprovals/cabapprove';
        //    SocketService.post(url, params).then(function (resp) {
        //        if (resp.data.status = true) {
        //            Notify.alert(resp.data.message, {
        //                status: 'success',
        //                pos: 'top-center',
        //                timeout: 3000
        //            });
        //        }
        //        else {
        //            Notify.alert(resp.data.message, {
        //                status: 'warning',
        //                pos: 'top-center',
        //                timeout: 3000
        //            });
        //        }
        //        $state.go('app.myApproval');
        //    });
        //}

        //$scope.btn_cabreject = function (cacapproval_gid) {
        //    var params = {
        //        cacapproval_gid: cacapproval_gid
        //    }
        //    var url ='api/myApprovals/cabreject';
        //    SocketService.post(url, params).then(function (resp) {
        //        if (resp.data.status = true) {
        //            $scope.close('view_cabdetails');
        //            Notify.alert(resp.data.message, {
        //                status: 'success',
        //                pos: 'top-center',
        //                timeout: 3000
        //            });
        //        }
        //        else {
        //            Notify.alert(resp.data.message, {
        //                status: 'warning',
        //                pos: 'top-center',
        //                timeout: 3000
        //            });
        //        }
        //        $scope.cabapproval = resp.data.cabapproval_list;
        //        $state.go('app.myApproval');
        //    });
        //}


        // Download Document

        $scope.downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        //$scope.downloads = function (val1, val2) {

        //    var phyPath = val1;
        //    var relPath = phyPath.split("StoryboardAPI");
        //    var relpath1 = relPath[1].replace("\\", "/");
        //    var hosts = window.location.host;
        //    var prefix = location.protocol + "//";
        //    var str = prefix.concat(hosts, relpath1);
        //    var link = document.createElement("a");
        //    var name = val2.split(".")
        //    link.download = name[0];
        //    var uri = str;
        //    link.href = uri;
        //    link.click();
        //}

        $scope.back = function () {
            $state.go('app.myApproval');
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('departmentApprovalView', departmentApprovalView);

    departmentApprovalView.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function departmentApprovalView($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'departmentApprovalView';

        activate();

        function activate() {
            $scope.serviceapproval_gid = localStorage.getItem('serviceapproval_gid');
            $scope.lsinternalapproval = localStorage.getItem('lsinternalapproval');
            var params = {
                serviceapproval_gid: $scope.serviceapproval_gid
            }
            lockUI();
            var url = 'api/myApprovals/viewdepartment';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.viewdepartment = resp.data;
            });
        }

        // Department Approve ...//

        //$scope.departmentapprove_click = function (category_gid, serviceapproval_gid) {
        //    var params = {
        //        category_gid: category_gid,
        //        serviceapproval_gid: serviceapproval_gid
        //    }
        //    lockUI();
        //    var url = 'api/myApprovals/departmentApproveclick';
        //    SocketService.post(url, params).then(function (resp) {
        //        unlockUI();
        //        if (resp.data.status = true) {
        //            Notify.alert(resp.data.message, {
        //                status: 'success',
        //                pos: 'top-center',
        //                timeout: 3000
        //            });
        //        }
        //        else {
        //            Notify.alert(resp.data.message, {
        //                status: 'warning',
        //                pos: 'top-center',
        //                timeout: 3000
        //            });
        //        }
        //        $state.go('app.myApproval');
        //    });
        //}

        $scope.departmentapprove_click = function (category_gid, serviceapproval_gid) {
            var serviceapproval_gid = serviceapproval_gid;
            var category_gid = category_gid;
            console.log(category_gid)
            var modalInstance = $modal.open({
                templateUrl: '/departmentapprove.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.approve_click = function (serviceapproval_gid) {

                    var params = {
                                category_gid: category_gid,
                                serviceapproval_gid: localStorage.getItem('serviceapproval_gid'),
                                remarks: $scope.txtremarks
                            }
                    console.log(params);
                    lockUI();
                    var url = 'api/myApprovals/departmentApproveclick';
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status = true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        $modalInstance.close('closed');
                        $state.go('app.myApproval');
                    });
                }
            }
        }


        // Department  Reject...//
        $scope.departmentreject_click = function (serviceapproval_gid) {
            var serviceapproval_gid = serviceapproval_gid;

            console.log(serviceapproval_gid)
            var modalInstance = $modal.open({
                templateUrl: '/departmentreject.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.reject_click = function (serviceapproval_gid) {

                    var params = {
                        serviceapproval_gid: localStorage.getItem('serviceapproval_gid'),
                        remarks:$scope.txtremarks
                    }
                    console.log(params);
                    lockUI();
                    var url = 'api/myApprovals/departmentreject';
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status = true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        $modalInstance.close('closed');
                        $state.go('app.myApproval');
                    });
                }
            }
        }
       

        // Department Internal Approval...//

        

        $scope.departmentinternal_click = function (complaint_gid, serviceapproval_gid) {
            var params = {
                serviceapproval_gid: serviceapproval_gid,
                complaint_gid: complaint_gid,
                remarks: $scope.txtremarks
            }
            var url = 'api/myApprovals/departmentinternal';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status = true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                $state.go('app.myApproval');
            });
        }

        $scope.back = function () {
            $state.go('app.myApproval');
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('dependencyApprovalcontroller', dependencyApprovalcontroller);

    dependencyApprovalcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'], 'DownloaddocumentService';

    function dependencyApprovalcontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'dependencyApprovalcontroller';

        activate();

        function activate() {
            $scope.release_gid = localStorage.getItem('release_gid');
            var params = {
                release_gid: $scope.release_gid
            };
            var url = 'api/myApprovals/releasedetails';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.releaseapprovaldetails = resp.data;
                $scope.releaseissue_list = resp.data.releaseissue_list;
                $scope.uatlog_list = resp.data.uatlog_list;
                $scope.dependency_list = resp.data.dependency_list;
                $scope.uatdocument_list = resp.data.uatdocument_list;
            });
        }

        // View UAT- Details..//

        $scope.uatdetails = function (issuetracker_gid,id) {
            var params = {
                issuetracker_gid: issuetracker_gid
            };
            var url = 'api/myApprovals/uatdetails';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.releaseissue_list[id][issuetracker_gid] = resp.data.uatlog_list;  
            });
        }

        // Dependency Approve & Reject .....//

        $scope.btn_dependencyapprove = function (dependentapproval_gid, release_gid) {
            var params = {
                dependentapproval_gid: dependentapproval_gid,
                release_gid: release_gid
            }
            lockUI();
            var url = 'api/myApprovals/dependencyapprove';
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status = true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
             
                $state.go('app.myApproval');
            });
        }

        $scope.btn_dependencyreject = function (dependentapproval_gid, release_gid) {
            var params = {
                dependentapproval_gid: dependentapproval_gid,
                release_gid: release_gid
            }
            lockUI();
            var url = 'api/myApprovals/dependencyreject';
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status = true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
              
                $state.go('app.myApproval');
            });
        }

        // Download Document

        $scope.downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        //$scope.downloads = function (val1, val2) {

        //    var phyPath = val1;
        //    var relPath = phyPath.split("StoryboardAPI");
        //    var relpath1 = relPath[1].replace("\\", "/");
        //    var hosts = window.location.host;
        //    var prefix = location.protocol + "//";
        //    var str = prefix.concat(hosts, relpath1);
        //    var link = document.createElement("a");
        //    var name = val2.split(".")
        //    link.download = name[0];
        //    var uri = str;
        //    link.href = uri;
        //    link.click();
        //}

        $scope.back = function () {
            $state.go('app.myApproval');
        }
    }
    dependencyApprovalcontroller.$inject = ["$rootScope", "$scope", "$state", "AuthenticationService", "$modal", "ScopeValueService", "$http", "SocketService", "Notify", "$location", "apiManage", "SweetAlert", "$route", "ngTableParams", "DownloaddocumentService"];
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('deploymentController', deploymentController);

    deploymentController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies'];
    function deploymentController($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies) {
        var vm = this;
        vm.title = 'deploymentController';

        activate();

        function activate() {
            var url = 'api/deployment/getClientList';
            SocketService.get(url).then(function (resp) {
                $scope.client_list = resp.data.clients;
            });
        };

        $scope.getProject = function (val) {
            var url = 'api/deployment/getProjectList';
            SocketService.get(url + '?client_gid=' + val).then(function (resp) {
                $scope.project_list = resp.data.projects;
               
            });
        };
        vm.validateInput = function (name, type) {
            var input = vm.formValidate[name];
            return (input.$dirty || vm.submitted) && input.$error[type];
        };
        vm.submitForm = function () {
            vm.submitted = true;
            if (vm.formValidate.$valid) {
                //console.log('Submitted!!');
                var indexProject = $scope.project_list.map(function (e) { return e.project_gid }).indexOf($scope.selectProject);
                var indexClient = $scope.client_list.map(function (e) { return e.client_gid }).indexOf($scope.selectClient);
                var project = $scope.project_list[indexProject].project_name;
                var client = $scope.client_list[indexClient].client_name;
                var params = {
                    user_gid: localStorage.getItem('user_gid'),
                    deployment_mode: $scope.selectMode,
                    deployment_client_gid: $scope.selectClient,
                    deployment_client: client,
                    deployment_project: project,
                    deployment_project_gid: $scope.selectProject,
                    deployment_description: $scope.description,
                    deployment_client_need: $scope.selectNeed,
                    deployment_page_flag: $scope.checkPage,
                    deployment_pages: $scope.pagename,
                    deployment_report_flag: $scope.checkRpt,
                    deployment_report: $scope.reportname
                };

                var url = 'api/deployment/addDeployment';
                SocketService.post(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        SweetAlert.swal('Success!', 'Your Deployment Record Added!', 'success');
                    }
                    else {
                        SweetAlert.swal('Error!', 'An Error Occured While Adding Deployment Record', 'warning');
                    }
                    setTimeout($state.go('app.deploymentsummary'), 2000);
                });
            } else {
                   console.log('Not valid!!');
                return false;
            }
        };
     //   $scope.alert = function () {
                       
     //   }

        $scope.back = function () {
            $state.go('app.deploymentsummary');
        };
    }
})();
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('deploymentsummaryController', deploymentsummaryController);

    deploymentsummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$filter', 'ngTableParams', '$resource', '$timeout', 'ngTableDataService'];
    function deploymentsummaryController($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $filter, ngTableParams, $resource, $timeout, ngTableDataService) {
        var vm = this;
        vm.title = 'deploymentsummaryController';

        activate();

        function activate() {
          
            var url = 'api/deployment/getSummaryLive';
            // Live Deployment Data
            SocketService.get(url).then(function (resp) {
                $scope.liveData = resp.data.SummariesLive;
                
                $scope.liveTable = new ngTableParams({
                    page: 1,
                    count: 5
                    
                }, {
                    total: $scope.liveData.length,
                        getData: function ($defer, params) {
                        $scope.datasLive = $scope.liveData.slice((params.page() - 1) * params.count(), params.page() * params.count());
                        $defer.resolve($scope.datasLive);
                    }
                });
            });
            var url = 'api/deployment/getSummaryUAT';
            // UAT Deployment Data
            SocketService.get(url).then(function (resp) {
                $scope.UATData = resp.data.SummariesUAT;
                
                $scope.UATtable = new ngTableParams({
                    page: 1,
                    count: 5
                 
                }, {
                    total: $scope.UATData.length,
                    getData: function ($defer, params) {
                        
                        $scope.datasUAT = $scope.UATData.slice((params.page() - 1) * params.count(), params.page() * params.count());
                        $defer.resolve($scope.datasUAT);
                    }
                });
            });
            var url = 'api/deployment/getSummaryTest';
            // Test Deployment Data 
            SocketService.get(url).then(function (resp) {
                $scope.TestData = resp.data.SummariesTest;
                
                $scope.Testtable = new ngTableParams({
                    page: 1,
                    count: 5
                   
                }, {
                    total: $scope.TestData.length,
                    getData: function ($defer, params) {
                        
                        $scope.datasTest = $scope.TestData.slice((params.page() - 1) * params.count(), params.page() * params.count());
                        $defer.resolve($scope.datasTest);
                    }
                });
            });

        };
        $scope.add = function () {
            $state.go('app.deploymentadd');
        };


        $scope.viewModal = function (val) {
           
            var modalInstance = $modal.open({
                templateUrl: '/myModalContent.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var url ='api/deployment/viewDepDtl';
                SocketService.get(url + '?dep_gid=' + val).then(function (resp) {
                    $scope.viewDepData = resp.data;
                    if ($scope.viewDepData.new_reports == '') {
                        $scope.viewDepData.new_reports = 'No New Reports Added';
                    }
                    
                    if ($scope.viewDepData.new_pages == '') {
                        $scope.viewDepData.new_pages = 'No New Pages Added';
                    }
                    
                    if ($scope.viewDepData.dep_by == '---') {
                        $scope.viewDepData.dep_by = 'Not Yet Deployed';
                    }
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.cancel = function () {
                    $modalInstance.dismiss('cancel');
                };
            }
        };

      
       
    }
})();


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('itDashboardcontroller', itDashboardcontroller);

    itDashboardcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function itDashboardcontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'itDashboardcontroller';

        activate();

        function activate() {
            
            var user_gid = localStorage.getItem('user_gid');
            var url = 'api/user/privilegelevel3';
            SocketService.get(url + '?user_gid=' + user_gid).then(function (resp) {
                var newticket = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("ITSSVTNEW");
                var viewticket = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("ITSSVTVIW");
                if (newticket != -1) {                 
                    $scope.newticket = 'Y';
                }
                if (viewticket != -1) {
                    $scope.viewticket = 'Y';                   
                }

            });

        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('managementApprovalcontroller', managementApprovalcontroller);

    managementApprovalcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function managementApprovalcontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'managementApprovalcontroller';

        activate();

        function activate() {
            $scope.serviceapproval_gid = localStorage.getItem('serviceapproval_gid');
            var params = {
                serviceapproval_gid: $scope.serviceapproval_gid
            };
            var url = 'api/myApprovals/viewmanagement';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.managementdetails = resp.data;
                console.log(resp.data);
            });
        }

        // Management Approve & Reject ...//

        $scope.managerapprove = function (serviceapproval_gid) {
            var params = {
                serviceapproval_gid: serviceapproval_gid
            }
            lockUI();
            var url = 'api/myApprovals/manageapprove';
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status = true) {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                $state.go('app.myApproval');
            });
        }


        $scope.managerapprove = function (serviceapproval_gid) {
            var serviceapproval_gid = serviceapproval_gid;
            
            var modalInstance = $modal.open({
                templateUrl: '/manageapprove.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.approve_click = function (serviceapproval_gid) {

                    var params = {
                        serviceapproval_gid: localStorage.getItem('serviceapproval_gid'),
                        remarks: $scope.txtremarks
                    }
                    lockUI();
                    var url = 'api/myApprovals/manageapprove';
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status = true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        $modalInstance.close('closed');
                        $state.go('app.myApproval');
                    });
                }
            }
        }
     
        $scope.managerreject = function (serviceapproval_gid) {
            var serviceapproval_gid = serviceapproval_gid;

             var modalInstance = $modal.open({
                 templateUrl: '/managementreject.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.reject_click = function (serviceapproval_gid) {

                    var params = {
                        serviceapproval_gid: localStorage.getItem('serviceapproval_gid'),
                        remarks: $scope.txtremarks
                    }
                    lockUI();
                    var url = 'api/myApprovals/managereject';
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status = true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        $modalInstance.close('closed');
                        $state.go('app.myApproval');
                    });
                }
            }
        }
        $scope.back = function () {
            $state.go('app.myApproval');
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('myApprovalController', myApprovalController);

    myApprovalController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'cmnfunctionService'];

    function myApprovalController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'myApprovalController';

        activate();

        function activate() {
            //var url = 'api/taskManagement/taskapprovallist';
            //SocketService.get(url).then(function (resp) {
            //    $scope.taskapproval = resp.data.taskapproval_list;
            //});
            var url = 'api/myApprovals/myapproval';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.departmentapproval = resp.data.departmentapproval_list;
                $scope.serviceapproval = resp.data.serviceapproval_list;
                $scope.managementapproval = resp.data.managerapproval_list;
                $scope.historyapproval = resp.data.approvalhistory_list;
                $scope.dependencyapproval = resp.data.dependencyapproval_list;
                $scope.cacapproval = resp.data.cabapproval_list;
                $scope.dependencyhistory = resp.data.dependencyhistory_list;
                $scope.cachistory = resp.data.cachistory_list;
                $scope.approvalcount = resp.data.myapprovalcount;
            });

            var url = 'api/OsdTrnRequestApproval/GetApprovalSummary';
            SocketService.get(url).then(function (resp) {
                $scope.approvalsummarylist = resp.data.approvalsummarylist;
                $scope.approvalcompletedlist = resp.data.approvalcompletedlist;
            });

            var url = 'api/OsdTrnRequestApproval/GetRHApprovalSummary';
            SocketService.get(url).then(function (resp) {
                $scope.rhapprovalsummarylist = resp.data.rhapprovalsummarylist;
                $scope.rhapprovalcompletedlist = resp.data.rhapprovalcompletedlist;               
            });

            var url = 'api/MstScannedDocument/GetDeferralApprovalSummary';
            SocketService.get(url).then(function (resp) {
                $scope.deferralapprovallist = resp.data.mdldeferralapproval;
            });
            var url = 'api/MstScannedDocument/GetDeferralApprovalHistorySummary';
            SocketService.get(url).then(function (resp) {
                $scope.deferralcompletedapprovallist = resp.data.mdldeferralapproval;
            });
            var url = 'api/MstRMPostCCWaiver/GetWaiverApprovalSummary';
            SocketService.get(url).then(function (resp) {
                $scope.waiverapproval_list = resp.data.rmpostccwaiver_list;               
            });

            var url = 'api/MstRMPostCCWaiver/GetWaiverApprovalHistorySummary';
            SocketService.get(url).then(function (resp) {
                $scope.waiverapprovalhistory_list = resp.data.rmpostccwaiver_list;               
            });
            
            var url = 'api/AgrMstScannedDocument/GetDeferralApprovalSummary';
            SocketService.get(url).then(function (resp) {
                $scope.samagrodeferralapprovallist = resp.data.mdldeferralapproval;
            });
            var url = 'api/AgrMstScannedDocument/GetDeferralApprovalHistorySummary';
            SocketService.get(url).then(function (resp) {
                $scope.samagrodeferralcompletedapprovallist = resp.data.mdldeferralapproval;
            });

            var url = 'api/AgrMstSuprScannedDocument/GetDeferralApprovalSummary';
            SocketService.get(url).then(function (resp) {
                $scope.samagrosupplierdeferralapprovallist = resp.data.mdldeferralapproval;
            });
            var url = 'api/AgrMstSuprScannedDocument/GetDeferralApprovalHistorySummary';
            SocketService.get(url).then(function (resp) {
                $scope.samagrosupplierdeferralcompletedapprovallist = resp.data.mdldeferralapproval;
            });

            var url = 'api/MstSAOnboardingInstitution/GetSaApprovalPendingSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.saOnboardSummary_list = resp.data.saOnboardSummary_list;
                unlockUI();
            });
            var url = 'api/MstSAOnboardingInstitution/GetSaApprovalInitiatedSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.saOnboardSummary1_list = resp.data.saOnboardSummary_list;
                unlockUI();
            });
            var url = 'api/MstSAOnboardingIndividual/GetApprovalPendingSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.saOnboardSummary2_list = resp.data.saOnboardSummary_list;
                unlockUI();
            });
            var url = 'api/MstSAOnboardingIndividual/GetSaApprovalInitiateSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.saOnboardSummary3_list = resp.data.saOnboardSummary_list;
                unlockUI();
            });
        }

        // Task Approve & Reject ...//

        $scope.taskapprove_click = function (trntask_gid, trntask2activity_gid) {
            var params = {
                trntask_gid: trntask_gid,
                trntask2activity_gid: trntask2activity_gid
            }
            var url = apiManage.apiList["taskapprove"].api;
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status = true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                activate();
            });
        }

        $scope.showPopover = function (release_gid, approval_remarks) {
            var modalInstance = $modal.open({
                templateUrl: '/showpopupModal.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    release_gid: release_gid
                }
                var url = 'api/myApprovals/ApprovalRemarksView';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.approval_remarks = resp.data.approval_remarks;
                    $scope.application = resp.data.application;


                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        $scope.taskreject_click = function (trntask_gid, trntask2activity_gid) {
            var params = {
                trntask_gid: trntask_gid,
                trntask2activity_gid: trntask2activity_gid
            }
            var url = apiManage.apiList["taskreject"].api;
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status = true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                activate();
            });
        }

        // View Department Details Popup....//

        $scope.btn_viewdepartmentclick = function (serviceapproval_gid, lsinternalapproval) {

            $scope.serviceapproval_gid = localStorage.setItem('serviceapproval_gid', serviceapproval_gid);
            $scope.lsinternalapproval = localStorage.setItem('lsinternalapproval', lsinternalapproval);
            $state.go('app.departmentApprovalView');

        }

        // View Service Details Popup....//

        $scope.btn_viewserviceclick = function (serviceapproval_gid, lsinternalapproval) {

            $scope.serviceapproval_gid = localStorage.setItem('serviceapproval_gid', serviceapproval_gid);
            $scope.lsinternalapproval = localStorage.setItem('lsinternalapproval', lsinternalapproval);
            $state.go('app.serviceApprovalView');

        }

        // View Management Details Popup....//

        $scope.btn_viewmanagementclick = function (serviceapproval_gid) {
            $scope.serviceapproval_gid = localStorage.setItem('serviceapproval_gid', serviceapproval_gid);
            $state.go('app.managementApprovalView');
        }

        // View History Details Popup....//

        $scope.btn_viewhistoryclick = function (serviceapproval_gid) {
            $scope.serviceapproval_gid = localStorage.setItem('serviceapproval_gid', serviceapproval_gid);
            $state.go('app.historyApprovalView');
        }

        // View Dependency Details Popup..//

        $scope.btn_viewdependencyclick = function (release_gid) {
            $scope.serviceapproval_gid = localStorage.setItem('release_gid', release_gid);
            $state.go('app.dependencyApprovalView');
        }

        // View CAB Details Popup....//

        $scope.btn_viewcabclick = function (release_gid) {
            $scope.release_gid = localStorage.setItem('release_gid', release_gid);
            $state.go('app.cacApproval');
        }


        $scope.btn_approvalclick = function (val) {
            $scope.requestapproval_gid = val;
            $scope.requestapproval_gid = localStorage.setItem('requestapproval_gid', val);
            $state.go('app.osdTrnApprovalView');
        }
        
        $scope.btn_approvalhistory = function (val) {
            $scope.requestapproval_gid = val;
            $scope.requestapproval_gid = localStorage.setItem('requestapproval_gid', val);
            $state.go('app.osdTrnApprovalViewHistory');
        }

        $scope.btn_rhapprovalclick = function (val,val1,val2,val3) {
            $scope.bankalertrefundapprl_gid = val;
            $scope.bankalertrefundapprl_gid = localStorage.setItem('bankalertrefundapprl_gid', val);
            $scope.bankalert2allocated_gid = val1;
            $scope.bankalert2allocated_gid = localStorage.setItem('bankalert2allocated_gid', val1);
            $scope.customername = val2;
            $scope.customername = localStorage.setItem('customername', val2);
            $scope.customerurn = val3;
            $scope.customerurn = localStorage.setItem('customerurn', val3);
            $state.go('app.osdTrnRHApprovalView');
        }
        $scope.btn_rhapprovalhistory = function (val,val1,val2,val3) {
            $scope.bankalertrefundapprl_gid = val;
            $scope.bankalertrefundapprl_gid = localStorage.setItem('bankalertrefundapprl_gid', val);
            $scope.bankalert2allocated_gid = val1;
            $scope.bankalert2allocated_gid = localStorage.setItem('bankalert2allocated_gid', val1);
            $scope.customername = val2;
            $scope.customername = localStorage.setItem('customername', val2);
            $scope.customerurn = val3;
            $scope.customerurn = localStorage.setItem('customerurn', val3);
            $state.go('app.osdTrnRHApprovalViewHistory');
        }

        $scope.approvaldeferral_view = function (approval_initiationgid, documentcheckdtl_gid, initiateextendorwaiver_gid, application_gid,fromphysical_document) {
            $location.url('app/MstDeferralMyApproval?application_gid=' + application_gid + '&approval_initiationgid=' + approval_initiationgid + '&initiateextendorwaiver_gid=' + initiateextendorwaiver_gid + '&documentcheckdtl_gid=' + documentcheckdtl_gid+ '&fromphysical_document=' + fromphysical_document);
        }

        $scope.approvaldeferral_historyview = function (extendorwaiverapproval_gid, documentcheckdtl_gid, initiateextendorwaiver_gid, application_gid,fromphysical_document) {
            $location.url('app/MstDeferralMyApprovalHistory?application_gid=' + application_gid + '&extendorwaiverapproval_gid=' + extendorwaiverapproval_gid + '&initiateextendorwaiver_gid=' + initiateextendorwaiver_gid + '&documentcheckdtl_gid=' + documentcheckdtl_gid+ '&fromphysical_document=' + fromphysical_document);
        }

        $scope.approvalsamagrodeferral_view = function (approval_initiationgid, application_gid, initiateextendorwaiver_gid, documentcheckdtl_gid, fromphysical_document) {
            $location.url('app/AgrTrnBuyerDeferralMyApproval?application_gid=' + application_gid + '&approval_initiationgid=' + approval_initiationgid + '&initiateextendorwaiver_gid=' + initiateextendorwaiver_gid + '&documentcheckdtl_gid=' + documentcheckdtl_gid + '&fromphysical_document=' + fromphysical_document);
        }

        $scope.approvalsamagrodeferral_historyview = function (extendorwaiverapproval_gid, documentcheckdtl_gid, initiateextendorwaiver_gid, application_gid, fromphysical_document) {
            $location.url('app/AgrTrnDeferralMyApprovalHistory?application_gid=' + application_gid + '&extendorwaiverapproval_gid=' + extendorwaiverapproval_gid + '&initiateextendorwaiver_gid=' + initiateextendorwaiver_gid + '&documentcheckdtl_gid=' + documentcheckdtl_gid + '&fromphysical_document=' + fromphysical_document);
        }

        $scope.approvalsamagrosupplierdeferral_view = function (extendorwaiverapproval_gid, documentcheckdtl_gid, initiateextendorwaiver_gid, application_gid) {
            $location.url('app/AgrTrnSuprDeferralMyApproval?application_gid=' + application_gid + '&extendorwaiverapproval_gid=' + extendorwaiverapproval_gid + '&initiateextendorwaiver_gid=' + initiateextendorwaiver_gid + '&documentcheckdtl_gid=' + documentcheckdtl_gid);
        }

        $scope.approvalsamagrosupplierdeferral_historyview = function (extendorwaiverapproval_gid, documentcheckdtl_gid, initiateextendorwaiver_gid, application_gid) {
            $location.url('app/AgrTrnSuprDeferralMyApprovalHistory?application_gid=' + application_gid + '&extendorwaiverapproval_gid=' + extendorwaiverapproval_gid + '&initiateextendorwaiver_gid=' + initiateextendorwaiver_gid + '&documentcheckdtl_gid=' + documentcheckdtl_gid);
        }

        // View Task Details Popup....//

        $scope.btn_viewtaskdetailclick = function (trntask_gid) {
            var doc = document.getElementById('view_taskdetails');
            doc.style.display = 'block';
            var params = {
                trntask_gid: trntask_gid
            };
            lockUI();
            var url = 'api/taskManagement/viewtaskapproval_details';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.taskdetails = resp.data;
            });

        }


        // Click Event Hide & Show...//

        $scope.servicedesk = function () {
            $scope.service_desk = true;
            $scope.change_management = false;
            $scope.task_managment = false;
        }
        $scope.changemanagment = function () {
            $scope.service_desk = false;
            $scope.change_management = true;
            $scope.task_managment = false;

        }
        $scope.taskmanagement = function () {
            $scope.service_desk = false;
            $scope.change_management = false;
            $scope.task_managment = true;
        }

        $scope.approval_view = function (rmpostccwaiver_gid, application_gid) {
            $location.url('app/MstRMWaiverApprovalView?rmpostccwaiver_gid=' + rmpostccwaiver_gid + '&application_gid=' + application_gid);
        }

        $scope.approvalhistory_view = function (rmpostccwaiver_gid, application_gid) {
            $location.url('app/MstRMWaiverApprovalHistoryView?rmpostccwaiver_gid=' + rmpostccwaiver_gid + '&application_gid=' + application_gid);
        }
        $scope.saonboardingverification = function (sacontactinstitution_gid) {

            // $location.url('app/MstSAOnboardingInstitutionVerification');

            $location.url('app/MstSBAInstitutionFinalApproval?hash=' + cmnfunctionService.encryptURL('lssacontactinstitution_gid=' + sacontactinstitution_gid + '&lspage=InstitutePending'));
        }
        $scope.saonboardingverificationcompleted = function (sacontactinstitution_gid) {

            // $location.url('app/MstSAOnboardingInstitutionVerification');

            $location.url('app/MstSBAInstitutionFinalApproval?hash=' + cmnfunctionService.encryptURL('lssacontactinstitution_gid=' + sacontactinstitution_gid + '&lspage=InstituteInitiate'));
        }
        $scope.saonboardingverificationpending = function (sacontact_gid) {
            $location.url('app/MstSBAIndividualFinalApproval?hash=' + cmnfunctionService.encryptURL('lssacontact_gid=' + sacontact_gid + '&lspage=IndividualPending'));
        }
        $scope.verificationindividualcompleted = function (sacontact_gid) {
            $location.url('app/MstSBAIndividualFinalApprovalView?hash=' + cmnfunctionService.encryptURL('lssacontact_gid=' + sacontact_gid + '&lspage=IndividualInitiate'));
        }
    }
})();
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('navbar', navbar);

    navbar.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route'];

    function navbar($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'navbar';

        activate();

        function activate() {
            setInterval(notifications, 6000);
            var user_gid = localStorage.getItem('user_gid');
            var url = apiManage.apiList['userData'].api;
            SocketService.get(url + '?user_gid=' + user_gid).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.userData = resp.data;
                   
                }
            });
        };
        function notifications() {
            var url = 'api/landingPage/landingpagedata';
            SocketService.get(url).then(function (resp) {
                var lscount_acknowledgement, lscount_surrender, lscount_tmpsurrender, lscount_tmpholding, lscount_temporaryhandover, lscount_response, lscount_myapprovals;
                if (resp.data.count_acknowledgement==null) {
                  lscount_acknowledgement = '0';
                }
                else {
                     lscount_acknowledgement = resp.data.count_acknowledgement;
                }
                if (resp.data.count_surrender == null) {
                    lscount_surrender = '0';
                }
                else {
                    lscount_surrender = resp.data.count_surrender;
                }
                if (resp.data.count_tmpsurrender == null) {
                    var lscount_tmpsurrender = '0';
                }
                else {
                    lscount_tmpsurrender = resp.data.count_tmpsurrender;
                }
                if (resp.data.count_tmpholding == null) {
                    lscount_tmpholding = '0';
                }
                else {
                    lscount_tmpholding = resp.data.count_tmpholding;
                }
                if (resp.data.count_temporaryhandover == null) {
                    lscount_temporaryhandover = '0';
                }
                else {
                    lscount_temporaryhandover = resp.data.count_temporaryhandover;
                }
                if (resp.data.count_myapprovals == null) {
                    lscount_myapprovals = '0';
                }
                else {
                    lscount_myapprovals = resp.data.count_myapprovals;
                }
                if (resp.data.count_response == null) {
                    lscount_response = '0';
                }
                else {
                    lscount_response = resp.data.count_response;
                }

                $scope.notification_count = lscount_acknowledgement + lscount_surrender + lscount_tmpsurrender + lscount_tmpholding + lscount_temporaryhandover + lscount_myapprovals + lscount_response;
 
            });
        };
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('newServiceTicketcontroller', newServiceTicketcontroller);

    newServiceTicketcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route'];

    function newServiceTicketcontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'newServiceTicketcontroller';

        activate();

        function activate() {
            $scope.showval = false;
            $scope.hidecontact = false;
            $scope.hideapproval = false;

            var url = 'api/newServiceTicket/category';
            SocketService.get(url).then(function (resp) {


                $scope.category_list = resp.data.category_list;
            });

            $scope.close = function (val) {
                var doc = document.getElementById(val);
                doc.style.display = 'none';
            }
            var url = 'api/newServiceTicket/employee';
            SocketService.get(url).then(function (resp) {

                $scope.employee_list = resp.data.employee_list;
            });

            var url = 'api/newServiceTicket/tmpcleardocument';
            SocketService.get(url).then(function (resp) {
            });
        }


        // Get Sub_Category List //
        $scope.onselectedchange = function (category) {
            var params = {
                category_gid: category,
                employee_gid: $scope.employee
            };
            console.log(params);
            if ($scope.employee == "" && $scope.radio_selfothers=="") {
            }
            else if ($scope.radio_selfothers == "Self") {
                var url = 'api/newServiceTicket/employee_contactdetails';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.approvaldetails = resp.data;

                });
            }
            else {
                var url = 'api/newServiceTicket/employeecontactdetails';

                SocketService.getparams(url, params).then(function (resp) {
                    $scope.approvaldetails = resp.data;

                });

            }
            var url = 'api/newServiceTicket/subcategory';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.approval_flag == "Y") {
                    var modalInstance = $modal.open({
                        templateUrl: '/myModalContent.html',
                        controller: ModalInstanceCtrl,
                        size: 'md'
                    });
                    ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
                    function ModalInstanceCtrl($scope, $modalInstance) {     

                        $scope.subcategory = resp.data;
                        $scope.ok = function () {
                            $modalInstance.close('closed');
                        };     
                    }
                }
                    
                $scope.subcategory = resp.data;
                $scope.subcategory_list = resp.data.subcategory_list;
            });
        }

        // Get Type List //
        $scope.onselectedchangesubcategory = function (subcategory) {
            var params = {
                subcategory_gid: subcategory
            }
            var url = 'api/newServiceTicket/type';


            SocketService.getparams(url, params).then(function (resp) {

                $scope.type_list = resp.data.type_list;

            });
            
        }

        $scope.onselectedchangeemployee = function (employee) {
            var params = {
                employee_gid: employee,
                category_gid: $scope.category
            }
            var url = 'api/newServiceTicket/employeecontactdetails';

            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.txtcontact_number = resp.data.employee_mobileno;
                    $scope.txtemail_address = resp.data.employee_emailid;
                    $scope.approvaldetails = resp.data;
                    $scope.hideapproval = true;
                }
                else {           
                    Notify.alert('You Cannot able to Raise Ticket to this Selected Employee', {
                        status: 'info',
                        pos: 'top-center',
                        timeout: 3000
                    });
                  
                }
            });
          
            $scope.hidecontact = true;

        }

        $scope.onchangeself = function (radio_selfothers) {
            var params = {
                category_gid: $scope.category
            }
            var url = 'api/newServiceTicket/employee_contactdetails';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.txtcontact_number = resp.data.employee_mobileno;
                    $scope.txtemail_address = resp.data.employee_emailid;
                    $scope.approvaldetails = resp.data;        
                     
                }
                else {                    
                    Notify.alert('Contact Admin', {
                        status: 'info',
                        pos: 'top-center',
                        timeout: 3000
                    });
                   
                }
             
            });
          

        }

        $scope.isShowHide = function (param) {
            if (param == "show") {
                $scope.showval = true;
                $scope.hideval = true;               
                $scope.hidecontact = false;
                $scope.hideapproval = false;
                console.log("show");
                //$('#contactnumber').hide();
                $scope.txtcontact_number = "";
                $scope.txtemail_address = "";
                Notify.alert('Select Employee Name..!!', {
                    status: 'info',
                    pos: 'top-center',
                    timeout: 3000
                });
                
            }
                // SELF //
            else if (param == "hide") {
                $scope.showval = false;
                $scope.hideval = false;
                $scope.hidecontact = true;
                $scope.hideapproval = true;
                $scope.employee = "";
                $scope.txtcontact_number = "";
                $scope.txtemail_address = "";
            }
            else {
                $scope.showval = false;
                $scope.hideval = false;
            }
        }

        // Document Upload //
        $scope.upload = function (val, val1, name) {

            var item = {
                name: val[0].name,
                file: val[0]
            };
            
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "Default");
                    if (IsValidExtension == false) {
                        Notify.alert("File format is not supported..!", {
                            status: 'danger',
                            pos: 'top-center',
                            timeout: 3000
                        });

                        return false;
                    }
                    var frm = new FormData();
            frm.append('fileupload', item.file);
            frm.append('file_name', item.name);
            frm.append('document_name', $scope.documentname);
            frm.append('project_flag', "Default");

            $scope.uploadfrm = frm;
            var url = 'api/newServiceTicket/UploadDocument';

            SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                $scope.filename_list = resp.data.filename_list;
                $("#addupload").val('');

                if (resp.data.status == true) {

                    Notify.alert('Document Uploaded Successfully..!!', {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });

                }
                else {
                    Notify.alert('File Format Not Supported!', {
                        status: 'info',
                        pos: 'top-center',
                        timeout: 3000
                    });

                }
            });

        }

        // Document Delete //
        $scope.document_cancelclick = function (val, data) {
            var params = { id: val };
            var url = 'api/newServiceTicket/documentdelete';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    angular.forEach($scope.filename_list, function (value, key) {
                        if (value.id == val) {
                            $scope.filename_list.splice(key, 1);
                        }
                    });
                    Notify.alert('Document Deleted Successfully..!!', {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    activate();
                }
                else {
                    Notify.alert('Internal Error Occurred!', {
                        status: 'Warning',
                        pos: 'top-center',
                        timeout: 3000
                    });

                }
            });
        }

        $scope.serviceticket_submit = function () {
            var params = {
                category_gid: $scope.category,
                subcategory_gid: $scope.subcategory,
                type_gid: $scope.type,
                raisedfor: $scope.radio_selfothers,
                raisedemployee: $scope.employee,
                txtcontact_number: $scope.txtcontact_number,
                txtemail_address: $scope.txtemail_address,
                txt_title: $scope.txt_title,
                txt_remarks: $scope.txt_remarks,
                file_name: $scope.modelhere

            }

            var url = 'api/newServiceTicket/raiseticket';
         lockUI();
            SocketService.post('api/newServiceTicket/raiseticket', params).then(function (resp) {
           unlockUI();
                if (resp.data.status == true) {
                    $state.go('app.itDashboard');
                    Notify.alert('Ticket Raised Successfully..!!', {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert('Contact Admin', {
                        status: 'info',
                        pos: 'top-center',
                        timeout: 3000
                    });

                }
            });
        }
        $scope.back = function () {
            $state.go('app.itDashboard');
        }

    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('serviceApprovalViewcontroller', serviceApprovalViewcontroller);

    serviceApprovalViewcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function serviceApprovalViewcontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'serviceApprovalViewcontroller';

        activate();

        function activate() {
            $scope.serviceapproval_gid = localStorage.getItem('serviceapproval_gid');
            $scope.lsinternalapproval = localStorage.getItem('lsinternalapproval');
            var params = {
                serviceapproval_gid: $scope.serviceapproval_gid
            };
            var url = 'api/myApprovals/viewservice';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.servicedetails = resp.data;
            });
        }

        // Service Approve & Reject ...//

        $scope.serviceapprove = function (serviceapproval_gid, category_gid, complaint_gid) {
            var params = {
                serviceapproval_gid: serviceapproval_gid,
                category_gid: category_gid,
                complaint_gid: complaint_gid
            }
            lockUI();
            var url = 'api/myApprovals/serviceapprove' ;
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status = true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                $state.go('app.myApproval');
            });
        }

     
        $scope.serviceapprove = function (serviceapproval_gid, category_gid, complaint_gid) {
            var serviceapproval_gid = serviceapproval_gid;
            var category_gid = category_gid;
            var complaint_gid = complaint_gid;
            console.log(complaint_gid)
            console.log(category_gid)
            var modalInstance = $modal.open({
                templateUrl: '/serviceapprove.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.approve_click = function (serviceapproval_gid) {

                       var params = {
                           serviceapproval_gid: localStorage.getItem('serviceapproval_gid'),
                           remarks: $scope.txtremarks,
                category_gid: category_gid,
                complaint_gid: complaint_gid
            }
            lockUI();
            var url = 'api/myApprovals/serviceapprove' ;
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status = true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        $modalInstance.close('closed');
                        $state.go('app.myApproval');
                    });
                }
            }
        }

        $scope.servicereject_click = function (serviceapproval_gid) {
            var serviceapproval_gid = serviceapproval_gid;

            console.log(serviceapproval_gid)
            var modalInstance = $modal.open({
                templateUrl: '/servicereject.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.reject_click = function (serviceapproval_gid) {

                    var params = {
                        serviceapproval_gid: localStorage.getItem('serviceapproval_gid'),
                        remarks: $scope.txtremarks
                    }
                    console.log(params);
                    lockUI();
                    var url = 'api/myApprovals/servicereject';
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status = true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        $modalInstance.close('closed');
                        $state.go('app.myApproval');
                    });
                }
            }
        }

        // Service Internal Approval...//

        $scope.internalapprove_click = function (serviceapproval_gid) {
            var params = {
                serviceapproval_gid: serviceapproval_gid
            }
            lockUI();
            var url = 'api/myApprovals/serviceinternalapprove';
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status = true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                $state.go('app.myApproval');
            });
        };
        $scope.back = function () {
            $state.go('app.myApproval');
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('viewServicecontroller', viewServicecontroller);

    viewServicecontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function viewServicecontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'viewServicecontroller';

        activate();

        function activate() {
            lockUI();
            var url = 'api/viewServiceTicket/viewserviceticket';
            
            SocketService.get(url).then(function (resp) {
               
                $scope.viewservice_list = resp.data.viewservice_list;
                $scope.leadstage_name = resp.data.leadstage_name;
                $scope.response_new = resp.data.response_new;
                unlockUI();
            });

        }
        var params = {
            complaint_gid: $scope.complaint_gid
        }

        var url = 'api/viewServiceTicket/responselogdetails_view';
        lockUI();
        SocketService.getparams(url, params).then(function (resp) {
          
            $scope.responselog_detailslist = resp.data.responselog_detailslist;
              unlockUI();
        });


        $scope.btn_incompleteclick = function (val) {
            var params = {
                complaint_gid: val
            };
            var url = 'api/viewServiceTicket/incompleteticket';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                location.reload();
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert('Ticket Incompleted ...!!', {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
            });
        }
        $scope.btn_closeclick = function (complaint_gid) {
            var params = {
                complaint_gid: complaint_gid
            };
            var url = 'api/viewServiceTicket/closeticket';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                location.reload();
                if (resp.data.status == true) {

                    Notify.alert('Ticket Closed Successfully..!!', {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });

                }
                else {
                    Notify.alert('Internal Error Occurred!', {
                        status: 'Warning',
                        pos: 'top-center',
                        timeout: 3000
                    });

                }
            });
        }


        $scope.btn_viewclick = function (complaint_gid) {

            $scope.complaint_gid = localStorage.setItem('complaint_gid', complaint_gid);
            console.log(complaint_gid);
            $state.go('app.viewTicketDetails');
        }

        $scope.btnviewresponselog = function (complaint_gid) {
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


                $scope.complaint_gid = complaint_gid;
                var params = {
                    complaint_gid: $scope.complaint_gid
                }
                var url = 'api/viewServiceTicket/responselogdetails_view';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.responselog_detailslist = resp.data.responselog_detailslist;
                });
                $scope.responseclick = function (complaint_gid) {
                    var params = {
                        complaint_gid: $scope.complaint_gid,
                        response_description: $scope.txtresponse
                    }
                    var url = 'api/viewServiceTicket/response_logdetails';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        $('#txt_response').val('');

                        if (resp.data.status == true) {
                            var url = 'api/viewServiceTicket/responselogdetails_view';
                            lockUI();
                            SocketService.getparams(url, params).then(function (resp) {
                                unlockUI();
                                $scope.responselog_detailslist = resp.data.responselog_detailslist;

                            });
                        }
                        else {
                            Notify.alert('Internal Error Occurred!', {
                                status: 'Warning',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }
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
        .controller('viewTicketDetailscontroller', viewTicketDetailscontroller);

    viewTicketDetailscontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function viewTicketDetailscontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'viewTicketDetailscontroller';

        activate();

        function activate() {
            var params = {
                complaint_gid: localStorage.getItem('complaint_gid')
            };
            var url = 'api/viewServiceTicket/ticketdetails_view';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.ticket_details = resp.data;
              
            });
            var url = 'api/viewServiceTicket/viewdocument';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.viewDocumentList = resp.data.viewDocumentList;
            });
            //var url = apiManage.apiList['document_tmpclear'].api;
            //SocketService.get(url).then(function (resp) {
            //});
        }
        $scope.downloads = function (val1, val2) {
            var phyPath = val1;
            var relPath = phyPath.split("StoryboardAPI");
            var relpath1 = relPath[1].replace("\\", "/");
            var hosts = window.location.host;
            var prefix = location.protocol + "//";
            var str = prefix.concat(hosts, relpath1);
            var link = document.createElement("a");
            var name = val2.split('.');
            link.download = name[0];
            var uri = str;
            link.href = uri;
            link.click();
        }
        $scope.back = function () {
            $state.go('app.viewServiceTicket');
        }
          }
})();
