(function () {
    'use strict';


    angular
        .module('angle')
        .controller('osdMstTeamController', osdMstTeamController);

    osdMstTeamController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location','$route', 'ngTableParams'];

    function osdMstTeamController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'osdMstTeamController';

        activate();

        function activate() {
           
            $scope.totalDisplayed = 100;
            var url = 'api/OsdMstSupportTeam/GetSupportTeamSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.teamlist = resp.data.supportdtl;
                unlockUI();
                if ($scope.teamlist == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.teamlist.length;
                    if ($scope.teamlist.length < 100) {
                        $scope.totalDisplayed = $scope.teamlist.length;
                    }
                }
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
            if ($scope.teamlist != null) {

                if (pagecount < $scope.teamlist.length) {
                    $scope.totalDisplayed += Number;
                    if ($scope.teamlist.length < $scope.totalDisplayed) {
                        $scope.totalDisplayed = $scope.teamlist.length;
                        Notify.alert(" Total Summary " + $scope.teamlist.length + " Records Only", "warning");
                    }
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(" Total Summary " + $scope.teamlist.length + " Records Only", "warning");
                    return;
                }
            }
            // new code end
            unlockUI();
        };

        // Add Code Starts
        $scope.addteam = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addTeamModalContent.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];
            function ModalInstanceCtrl($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {
                $scope.ok = function () {
                    modalInstance.close('closed');
                };


                var url = 'api/OsdMstDepartmentManagement/GetActivatedept';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.deptmasterlist = resp.data.deptlist;

                    unlockUI();
                    if(  $scope.deptmasterlist.length == 1)
                    {
                        $scope.single = true
                        $scope.lbldepartmentname = resp.data.department_name
                        $scope.lbldepartmentgid = resp.data.department_gid
                        var params = {
                            department_gid: resp.data.department_gid
                        }
                        var url = 'api/OsdMstDepartmentManagement/GetDeptEmployee';
                        SocketService.getparams(url, params).then(function (resp) {
                            $scope.employee_list = resp.data.employeeasssign_list;
                        });
                    }
                    else {
                        $scope.multiple = true
                        $scope.single = false
                    }
                });

                $scope.onselectdept = function (department_gid) {
                    var params = {
                        department_gid: department_gid.department_gid
                    }
                    var url = 'api/OsdMstDepartmentManagement/GetDeptEmployee';
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.employee_list = resp.data.employeeasssign_list;
                    });
                }
                

                $scope.teamname = function (string) {
                    if (string.length >= 100) {
                        $scope.message = "Maximum 100 characters Length";
                    }
                    else {
                        $scope.message =""                    
                    }
                }

                $scope.teamSubmit = function () {
                    if ($scope.cbodepartment == '' || $scope.cbodepartment == null || $scope.cbodepartment == undefined) {
                        if ($scope.lbldepartmentgid == '' || $scope.lbldepartmentgid == null || $scope.lbldepartmentgid == undefined)
                        {
                            alert('Select Department', 'warning');
                        }
                        else {
                            
                            var params = {
                                team_name: $scope.team_name,
                                team_description: $scope.team_description,
                                teammembers: $scope.cboteam_member,
                                department_gid: $scope.lbldepartmentgid,
                                department_name: $scope.lbldepartmentname
                            }
                            lockUI();
                            var url = 'api/OsdMstSupportTeam/PostSupportTeamAdd';
                            SocketService.post(url, params).then(function (resp) {
                                if (resp.data.status == true) {
                                    modalInstance.close('closed');
                                    Notify.alert(resp.data.message, {
                                        status: 'success',
                                        pos: 'top-center',
                                        timeout: 3000
                                    });
                                    unlockUI();
                                    activate();
                                }
                                else {
                                    //modalInstance.close('closed');
                                    alert(resp.data.message, {
                                        status: 'danger',
                                        pos: 'top-center',
                                        timeout: 3000
                                    });
                                    unlockUI();
                                    activate();
                                }
                            });
                        }

                    }
                    else {
                        
                        var params = {
                            team_name: $scope.team_name,
                            team_description: $scope.team_description,
                            teammembers: $scope.cboteam_member,
                            department_gid:$scope.cbodepartment.department_gid,
                            department_name: $scope.cbodepartment.department_name
                        }
                        lockUI();
                        var url = 'api/OsdMstSupportTeam/PostSupportTeamAdd';
                        SocketService.post(url, params).then(function (resp) {
                            if (resp.data.status == true) {
                                modalInstance.close('closed');
                                Notify.alert(resp.data.message, {
                                    status: 'success',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                unlockUI();
                                activate();
                            }
                            else {
                                //modalInstance.close('closed');
                                alert(resp.data.message, {
                                    status: 'danger',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                unlockUI();
                                activate();
                            }
                        });
                    }
                }
            }
        }
        // Add Code Ends

        // Edit Code Starts
        $scope.editteam = function (supportteam_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/teamModaledit.html',
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
                        $scope.lbldepartmentnameedit = resp.data.department_name;
                        $scope.lbldepartmentgidedit = resp.data.department_gid;
                    
                    }
                    else {
                        $scope.multiple = true
                        $scope.single = false
                    }
                });
             

                $scope.onselectdeptedit = function  (department_gid) {
                    var params = {
                        department_gid: department_gid
                    }
                    var url = 'api/OsdMstDepartmentManagement/GetDeptEmployee';
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.employee_listedit = resp.data.employeeasssign_list;
                    });
                }

                $scope.ok = function () {
                    modalInstance.close('closed');
                };
                var params = {
                    supportteam_gid: supportteam_gid
                }
                var url = 'api/OsdMstSupportTeam/GetSupportTeamView';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.teamcodeedit = resp.data.team_code,
                    $scope.teamnameedit = resp.data.team_name,
                    $scope.teamdescriptionedit = resp.data.team_description;
                    $scope.cbodepartmentedit = resp.data.department_gid;
                    $scope.teammembers = resp.data.teammembers;
                    $scope.employee_listedit = resp.data.employeelist;
                   
                    $scope.cboteam_member = [];
                    if (resp.data.teammembers != null) {
                        var count = resp.data.teammembers.length;
                        for (var i = 0; i < count; i++) {
                            var indexs = $scope.employee_listedit.map(function (x) { return x.employee_gid; }).indexOf(resp.data.teammembers[i].employee_gid);
                           // var indexs = $scope.employee_listedit.findIndex(x => x.employee_gid === resp.data.teammembers[i].employee_gid);
                            $scope.cboteam_member.push($scope.employee_listedit[indexs]);
                        }
                    }
                });
               
              
                $scope.teamUpdate = function () {
                    if ($scope.cbodepartmentedit == '' || $scope.cbodepartmentedit == null || $scope.cbodepartmentedit == undefined) {
                        if ($scope.lbldepartmentgidedit == '' || $scope.lbldepartmentgidedit == null || $scope.lbldepartmentgidedit == undefined) {
                            alert('Select Department', 'warning');
                        }
                        else
                        {
                            var departmentname;
                            var dept_index = $scope.deptmasterlist.map(function (e) { return e.department_gid }).indexOf($scope.cbodepartmentedit);
                            if (dept_index == -1) { departmentname = ''; } else { departmentname = $scope.deptmasterlist[dept_index].department_name; };
                            var params = {
                                supportteam_gid: supportteam_gid,
                                team_name: $scope.teamnameedit,
                                team_description: $scope.teamdescriptionedit,
                                teammembers: $scope.cboteam_member,
                                department_gid: $scope.cbodepartmentedit,
                                department_name: departmentname
                            }

                            lockUI();
                            var url = 'api/OsdMstSupportTeam/GetSupportTeamUpdate';
                            SocketService.post(url, params).then(function (resp) {
                                if (resp.data.status == true) {
                                    modalInstance.close('closed');
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
                                    unlockUI();
                                    activate();
                                }
                            });
                        }
                        }
                    else {
                        var departmentname;
                        var dept_index = $scope.deptmasterlist.map(function (e) { return e.department_gid }).indexOf($scope.cbodepartmentedit);
                        if (dept_index == -1) { departmentname = ''; } else { departmentname = $scope.deptmasterlist[dept_index].department_name; };
                        var params = {
                            supportteam_gid: supportteam_gid,
                            team_name: $scope.teamnameedit,
                            team_description: $scope.teamdescriptionedit,
                            teammembers: $scope.cboteam_member,
                            department_gid: $scope.cbodepartmentedit,
                            department_name: departmentname
                        }

                        lockUI();
                        var url = 'api/OsdMstSupportTeam/GetSupportTeamUpdate';
                        SocketService.post(url, params).then(function (resp) {
                            if (resp.data.status == true) {
                                modalInstance.close('closed');
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
                                unlockUI();
                                activate();
                            }
                        });
                    }
                }
            }
        }
        // Edit Code Ends

        // Delete Code Starts
        $scope.deleteteam = function (val) {
            var params = {
                supportteam_gid: val
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
                    var url = "api/OsdMstSupportTeam/GetSupportTeamDelete";
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

        $scope.showPopover = function (supportteam_gid, team_name, team_description) {
            var modalInstance = $modal.open({
                templateUrl: '/showpopupModal.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    supportteam_gid: supportteam_gid
                }
                var url = 'api/OsdMstSupportTeam/GetTeamMember';
                SocketService.getparams(url, params).then(function (resp) {
                        $scope.teammembers_list = resp.data.teammembers;
                        $scope.team_name = team_name;
                        $scope.team_description = team_description;

                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

    }
})();