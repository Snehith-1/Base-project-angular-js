(function () {
    'use strict';

    angular
        .module('angle')
        .controller('myLeavecontroller', myLeavecontroller);

    myLeavecontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams', 'SweetAlert'];

    function myLeavecontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams, SweetAlert) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'myLeavecontroller';

        activate();

        function activate() {
            var url = "api/applyLeave/leavetype";
            SocketService.get(url).then(function (resp) {
                $scope.leavetype_list = resp.data.leavetype_list;
                console.log(resp.data.leavetype_list);
            });

            var url = "api/applyLeave/leavesummary";
            SocketService.get(url).then(function (resp) {
                $scope.leave_list = resp.data.leave_list;
            });

            //var url = "api/applyLeave/leavereport";
            //SocketService.get(url).then(function (resp) {
            //    var result = resp.data.response;
            //    $scope.newresponse = result.replace(" ", "");

            //    $scope.leavereport = JSON.parse($scope.newresponse);
            //    console.log($scope.leavereport);
            //});

        }

        $scope.applyleave = function (val) {
            $scope.disablevalue = true;
            var modalInstance = $modal.open({
                templateUrl: '/applyLeaveModal.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var url = 'api/applyLeave/getleavetype_name';
                var param = {
                    leavetype_gid: val
                }
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.applyleavedetails = resp.data;
                    $scope.leavetype_name = resp.data.leavetype_name;
                    $scope.leavetype_gid = resp.data.leavetype_gid;

                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.fullclick = function () {
                    $scope.leavefull = true;
                    $scope.disablevalue = false;
                    $scope.leavehalf = false;
                    $scope.onselectedchangeto = function (val) {
                        var Difference_In_Days = (val.getTime() - $scope.fromdate.getTime()) / (1000 * 3600 * 24);
                        var lsleave_days = Difference_In_Days + 1;
                        var leavefrom_date = new Date();
                        leavefrom_date.setFullYear($scope.fromdate.getFullYear());
                        leavefrom_date.setMonth($scope.fromdate.getMonth());
                        leavefrom_date.setDate($scope.fromdate.getDate());

                        var leaveto_date = new Date();
                        leaveto_date.setFullYear($scope.todate.getFullYear());
                        leaveto_date.setMonth($scope.todate.getMonth());
                        leaveto_date.setDate($scope.todate.getDate());
                        var param = {
                            leave_gid: $scope.leavetype_gid,
                            leave_from: leavefrom_date,
                            leave_to: leaveto_date,
                            leave_days: lsleave_days,
                            leave_session: "NA"
                        }
                        console.log(param);
                        var url = 'api/applyLeave/leavevalidate';
                        lockUI();
                        SocketService.post(url, param).then(function (resp) {
                           
                            //if (resp.data.leave_days <= 0) {
                            //    unlockUI();
                            //    alert(resp.data.message, {
                            //        status: 'warning',
                            //        pos: 'top-center',
                            //        timeout: 3000
                            //    });
                            //    $scope.todate = '';
                            //    $scope.leave_days = '';
                            //}
                            //else {
                            //    unlockUI();
                            //    $scope.leave_days = resp.data.leave_days;
                            //    Notify.alert(resp.data.message, {
                            //        status: 'warning',
                            //        pos: 'top-center',
                            //        timeout: 3000
                            //    });
                            //}
                            if (resp.data.status == true) {
                                unlockUI();
                                $scope.leave_days = resp.data.leave_days;
                                Notify.alert(resp.data.message, 'success')

                               
                            }
                            else {
                                unlockUI();
                                $scope.leave_days = resp.data.leave_days;
                                Notify.alert(resp.data.message, 'warning')
                            }
                        });
                    }
                }

                $scope.halfclick = function () {
                    $scope.leavefull = false;
                    $scope.disablevalue = false;
                    $scope.leavehalf = true;
                }

                // Apply Leave (Full) ....//

                $scope.fullleavesubmit = function () {

                    var leavefrom_date = new Date();
                    leavefrom_date.setFullYear($scope.fromdate.getFullYear());
                    leavefrom_date.setMonth($scope.fromdate.getMonth());
                    leavefrom_date.setDate($scope.fromdate.getDate());

                    var leaveto_date = new Date();
                    leaveto_date.setFullYear($scope.todate.getFullYear());
                    leaveto_date.setMonth($scope.todate.getMonth());
                    leaveto_date.setDate($scope.todate.getDate());

                    var leave_session = "NA";

                    var params = {
                        leavetype_gid: $scope.leavetype_gid,
                        leave_from: leavefrom_date,
                        leave_session: leave_session,
                        leave_to: leaveto_date,
                        leave_reason: $scope.leave_reason
                    }
                    var url = 'api/applyLeave/applyleave';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
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
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                    });
                    $state.go('app.myLeave');

                }

                $scope.upload = function (val, val1, name) {
                    var item = {
                        name: val[0].name,
                        file: val[0]
                    };
                    var frm = new FormData();
                    frm.append('fileupload', item.file);
                    frm.append('file_name', item.name);
                    frm.append('document_name', $scope.documentname);
                    frm.append('leave_gid', $scope.leave_gid);
                    $scope.uploadfrm = frm;

                    var url = 'api/applyLeave/uploaddocument';
                    lockUI();
                    SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                        unlockUI();
                        $scope.filename_list = resp.data.filename_list;
                        $scope.disablevalue = true;
                        $("#addupload").val('');
                        $("#addhalfupload").val('');

                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {

                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                         
                        else {
                            unlockUI();
                            Notify.alert(resp.data.message, {

                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            
                        }
                    });
                }


                //Document Delete //

                $scope.document_delete = function (tmpdocument_gid) {
                    var params = {
                        tmpdocument_gid: tmpdocument_gid
                    }

                    var url = 'api/applyLeave/documentDelete';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {

                            $scope.filename_list = resp.data.filename_list;

                            $scope.disablevalue = false;
                            ////activate();
                        }
                        else {
                            Notify.alert('Error Occurred While Deleting Document!', {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                    });
                    SweetAlert.swal('Deleted Successfully!');
                    activate();
                };

               

                // Apply Leave (Half) ....//


                $scope.halfleavesubmit = function () {
                    var leavefrom_date = new Date();
                    leavefrom_date.setFullYear($scope.fromdate.getFullYear());
                    leavefrom_date.setMonth($scope.fromdate.getMonth());
                    leavefrom_date.setDate($scope.fromdate.getDate());

                    var params = {
                        leavetype_gid: $scope.leavetype_gid,
                        leave_from: leavefrom_date,
                        leave_session: $scope.radio_fnan,
                        leave_to: leavefrom_date,
                        leave_reason: $scope.leavehalf_reason
                    }
                    console.log(params);

                    var url = 'api/applyLeave/applyleave';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
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
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {

                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                    });

                    $state.go('app.myLeave');
                }
            }
        }

        // Document Leave Click .....//

        $scope.documentleave = function (leavetype_gid) {
            var params = {
                leave_gid: leavetype_gid
            }
            console.log(params);
            var url = 'api/approveLeave/getleavedocument';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    var modalInstance = $modal.open({
                        templateUrl: '/DocumentModal.html',
                        controller: ModalInstanceCtrl,
                        size: 'md'
                    });
                    ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
                    function ModalInstanceCtrl($scope, $modalInstance) {
                        $scope.filename_list = resp.data.filename_list;
                        $scope.ok = function () {
                            $modalInstance.close('closed');
                        };

                        // Document Download .....//

                        $scope.document_downloadclick = function (val1, val2) {

                            var phyPath = val1;
                            var relPath = phyPath.split("EMS");
                            var relpath1 = relPath[1].replace("\\", "/");
                            var hosts = window.location.host;
                            var prefix = "http://"
                            var str = prefix.concat(hosts, relpath1);
                            var link = document.createElement("a");
                            var name = val2.split(".")
                            link.download = name[0];
                            var uri = str;
                            link.href = uri;
                            link.click();
                        }
                    }
                }
                else {
                    SweetAlert.swal('No Documents...!');
                }
            });

        }

        $scope.deleteleave = function (leavetype_gid) {
            var params = {
                leavetype_gid: leavetype_gid
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
                    var url = "api/applyLeave/leavePendingDelete";
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            var url = "api/applyLeave/leavesummary";
                            SocketService.get(url).then(function (resp) {
                                $scope.leave_list = resp.data.leave_list;
                            });
                        }
                        else {
                            Notify.alert('Error Occurred !', {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }
                    });
                    SweetAlert.swal('Deleted Successfully!');
                }

            });
        }


    }
})();
