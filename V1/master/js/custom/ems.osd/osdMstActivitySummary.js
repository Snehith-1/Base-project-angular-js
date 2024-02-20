(function () {
    'use strict';

    angular
        .module('angle')
        .controller('osdMstActivityController', osdMstActivityController);

    osdMstActivityController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'DownloaddocumentService'];

    function osdMstActivityController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'osdMstActivityController';

        activate();


        function activate() {
            $scope.totalDisplayed = 100;
            var url = 'api/OsdMstActivity/GetActivitySummary';
            lockUI(); 
            SocketService.get(url).then(function (resp) {
                $scope.activitymasterlist = resp.data.activitydtl;
                if ($scope.activitymasterlist == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.activitymasterlist.length;
                    if ($scope.activitymasterlist.length < 100) {
                        $scope.totalDisplayed = $scope.activitymasterlist.length;
                    }
                }
                unlockUI();
            });
        }
        $scope.loadMore = function (pagecount) {
            if (pagecount == undefined) {
                Notify.alert("Enter the Total Summary Count", "warning");
                return;
            }
            lockUI();

            var Number = parseInt(pagecount);
            // new code start
            if ($scope.activitymasterlist != null) {

                if (pagecount < $scope.activitymasterlist.length) {
                    $scope.totalDisplayed += Number;
                    if ($scope.activitymasterlist.length < $scope.totalDisplayed) {
                        $scope.totalDisplayed = $scope.activitymasterlist.length;
                        Notify.alert(" Total Summary " + $scope.activitymasterlist.length + " Records Only", "warning");
                    }
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(" Total Summary " + $scope.activitymasterlist.length + " Records Only", "warning");
                    return;
                }
            }
            // new code end
            unlockUI();
        };
        // Add Code Starts
        $scope.popupactivity = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addActivityModalContent.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var url = 'api/OsdMstDepartmentManagement/GetActivatedept';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.deptmasterlist = resp.data.deptlist;

                    unlockUI();
                    if ($scope.deptmasterlist.length == 1) {
                        $scope.single = true
                        $scope.lbldepartmentname = resp.data.department_name;
                        $scope.lbldepartmentgid = resp.data.department_gid

                        var params = {
                            department_gid: resp.data.department_gid
                        }
                        var url = 'api/OsdMstActivity/GetDeptTeam';
                        SocketService.getparams(url, params).then(function (resp) {
                            $scope.supportdtllist = resp.data.supportdtl;
                        });
                    }
                    else {
                        $scope.multiple = true
                        $scope.single = false
                    }
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.onselectdept = function (department_gid) {
                    var params = {
                        department_gid: department_gid.department_gid
                    }
                    var url = 'api/OsdMstActivity/GetDeptTeam';
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.supportdtllist = resp.data.supportdtl;
                    });
                }

                $scope.activityname = function (string) {
                    if (string.length >= 100) {
                        $scope.message = "Maximum 100 characters Length";
                    }
                    else {
                        $scope.message = ""
                    }
                }


                

                $scope.activitySubmit = function () {
                    lockUI();
                    if ($scope.cbodepartment == '' || $scope.cbodepartment == null || $scope.cbodepartment == undefined) {
                        if ($scope.lbldepartmentgid == '' || $scope.lbldepartmentgid == null || $scope.lbldepartmentgid == undefined) {
                            alert('Select Department', 'warning');
                        }
                        else {
                            var params = {
                                activity_name: $scope.activity_name,
                                supportteam_gid: $scope.cbosuppport_team.supportteam_gid,
                                supportteam_name: $scope.cbosuppport_team.team_name,
                                activity_tat: $scope.activity_tat,
                                department_gid: $scope.lbldepartmentgid,
                                department_name: $scope.lbldepartmentname
                            }
                            var url = 'api/OsdMstActivity/PostActivityAdd';
                            SocketService.post(url, params).then(function (resp) {
                                if (resp.data.status == true) {
                                    $modalInstance.close('closed');
                                    Notify.alert(resp.data.message, {
                                        status: 'success',
                                        pos: 'top-center',
                                        timeout: 3000
                                    });
                                    activate();
                                    unlockUI();
                                }
                                else {
                                    //$modalInstance.close('closed');
                                    alert(resp.data.message, {
                                        status: 'danger',
                                        pos: 'top-center',
                                        timeout: 3000
                                    });
                                    activate();
                                    unlockUI();
                                }
                            });
                        }
                    }
                    else {
                        var params = {
                            activity_name: $scope.activity_name,
                            supportteam_gid: $scope.cbosuppport_team.supportteam_gid,
                            supportteam_name: $scope.cbosuppport_team.team_name,
                            activity_tat: $scope.activity_tat,
                            department_gid: $scope.cbodepartment.department_gid,
                            department_name: $scope.cbodepartment.department_name
                        }
                        var url = 'api/OsdMstActivity/PostActivityAdd';
                        SocketService.post(url, params).then(function (resp) {
                            if (resp.data.status == true) {
                                $modalInstance.close('closed');
                                Notify.alert(resp.data.message, {
                                    status: 'success',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                activate();
                                unlockUI();
                            }
                            else {
                                //$modalInstance.close('closed');
                                alert(resp.data.message, {
                                    status: 'danger',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                activate();
                                unlockUI();
                            }
                        });

                    }
                  
                }

            }
        }
        // Add Code Ends

        // Edit Code Starts
        $scope.edit = function (activitymaster_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/activityModaledit.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                

              /*  var url = 'api/OsdMstActivity/GetTeamSummary';
                SocketService.get(url).then(function (resp) {
                   $scope.supportdtleditlist = resp.data.supportdtl;
                }); */
                var params = {
                    activitymaster_gid: activitymaster_gid
                }
                var url = 'api/OsdMstActivity/GetActivityView';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.activitycodeedit = resp.data.activity_code,
                    $scope.activitynameedit = resp.data.activity_name,
                    $scope.supportteam_name = resp.data.supportteam_name;
                    $scope.activitytatedit = resp.data.activity_tat;
                    $scope.supportteam_gid = resp.data.supportteam_gid;
                    $scope.lbldepartmentnameedit = resp.data.department_name;
                    $scope.lbldepartmentgidedit = resp.data.department_gid;
                    var param = {
                        department_gid: resp.data.department_gid
                    }
                    var url = 'api/OsdMstActivity/GetTeamSummary';
                    SocketService.getparams(url, param).then(function (resp) {
                        $scope.supportdtleditlist = resp.data.supportdtl;
                        if ($scope.supportteam_gid != null) {
                            console.log($scope.supportteam_gid);
                            var indexs = $scope.supportdtleditlist.map(function (x) { return x.supportteam_gid; }).indexOf(resp.data.supportdtleditlist[i].supportteam_gid);
                            //var indexs = ($scope.supportdtleditlist.findIndex(x => x.supportteam_gid === $scope.supportteam_gid));
                            console.log(indexs);
                            $scope.cbosuppport_teamedit = $scope.supportdtleditlist[indexs];
                        }
                    });
                    
                });
                

                $scope.onselectdeptedit = function (department_gid) {
                    var params = {
                        department_gid: department_gid.department_gid
                    }
                    var url = 'api/OsdMstActivity/GetDeptTeam';
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.supportdtllist = resp.data.supportdtl;
                    });
                }


                $scope.ok = function () {
                    modalInstance.close('closed');
                };
                $scope.activityUpdate = function () {
                
                            var params = {
                                activitymaster_gid: activitymaster_gid,
                                activity_name: $scope.activitynameedit,
                                activity_tat: $scope.activitytatedit,
                                department_gid: $scope.lbldepartmentgidedit,
                                department_name: $scope.lbldepartmentnameedit,
                                supportteam_gid: $scope.cbosuppport_teamedit.supportteam_gid,
                                supportteam_name: $scope.cbosuppport_teamedit.team_name
                            }

                            var url = 'api/OsdMstActivity/GetActivityUpdate';
                            SocketService.post(url, params).then(function (resp) {
                                if (resp.data.status == true) {
                                    $modalInstance.close('closed');
                                    Notify.alert(resp.data.message, {
                                        status: 'success',
                                        pos: 'top-center',
                                        timeout: 3000
                                    });
                                    activate();
                                }
                                else {
                                    //$modalInstance.close('closed');
                                    alert(resp.data.message, {
                                        status: 'danger',
                                        pos: 'top-center',
                                        timeout: 3000
                                    });
                                    activate();
                                }
                            });
                       
                }
            }
        }
        // Edit Code Ends

        // Delete Code Starts
        $scope.delete = function (val) {
            var params = {
                activitymaster_gid: val
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
                    lockUI();
                    var url = "api/OsdMstActivity/GetActivityDelete";
                    SocketService.getparams(url, params).then(function (resp) {

                        if (resp.data.status == true) {
                            activate();
                            SweetAlert.swal('Deleted Successfully!');
                            unlockUI();
                        }
                        else {
                            alert(resp.data.message, {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                        }
                    });

                }

            });
        }


   // Delete Code Ends
        $scope.activityreport = function () {
            lockUI();
            var url = 'api/OsdMstActivity/ExportActivityReport';
            SocketService.get(url).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    DownloaddocumentService.Downloaddocument(resp.data.lscloudpath, resp.data.lsname);

                }
                else {
                    unlockUI();
                    Notify.alert('Error Occurred While Export !', 'success')

                }

            });
        }

    }
})();
