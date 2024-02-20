(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AtmTrnAuditorApproverClosedAuditController', AtmTrnAuditorApproverClosedAuditController);

    AtmTrnAuditorApproverClosedAuditController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$filter', '$route', 'ngTableParams', 'cmnfunctionService'];

    function AtmTrnAuditorApproverClosedAuditController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $filter, $route, ngTableParams, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AtmTrnAuditorApproverClosedAuditController';

        activate();

        function activate() {

            //$scope.closed_audit = function () {

                var url = 'api/AtmTrnAuditorMaker/GetClosedAuditorApprover';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.myauditormaker_list = resp.data.myauditormaker_list;
                    $scope.employee_gid = resp.data.employee_gid; unlockUI();

                });
            //}

                var url = 'api/AtmTrnAuditorMaker/GetAuditorApproverCounts';
                SocketService.get(url).then(function (resp) {
                    unlockUI()
                    $scope.auditsapproveronhold_count = resp.data.auditsapproveronhold_count;
                    $scope.closedapproveraudit_count = resp.data.closedapproveraudit_count;
                    $scope.openapproveraudit_count = resp.data.openapproveraudit_count;
                    $scope.pendingapproverapproval_count = resp.data.auditapproverpending_count;
                    $scope.completed_count = resp.data.completedapproveraudit_count

                });


        }

        $scope.auditor_maker = function () {
            $location.url('app/AtmTrnAuditorMakerSummary')
        }

        $scope.auditor_checker = function () {
            $location.url('app/AtmTrnAuditorCheckerSummary')
        }

        $scope.auditor_approver = function () {
            $state.go('app.AtmTrnAuditorApproverPendingApproval');
        }

        $scope.open_audit = function () {
            $location.url('app/AtmTrnAuditorApproverSummary')
        }

        $scope.pending_approval = function () {
            $location.url('app/AtmTrnAuditorApproverPendingApproval')
        }

        $scope.hold_audit = function () {
            $location.url('app/AtmTrnAuditorApproverHoldAudit')
        }

        $scope.closed_audit = function () {
            $location.url('app/AtmTrnAuditorApproverClosedAudit')
        }

        $scope.tagged_items = function () {
            $location.url('app/AtmTrnAuditorApproverTaggedItems')
        }

        $scope.completed_audit = function () {
            $location.url('app/AtmTrnAuditorApproverCompletedAudit')
        }

        //$scope.taggedsamples = function () {

        //    var url = 'api/AtmTrnMyAuditTaskAuditee/GetTaggedSampleTask';
        //    lockUI();
        //    SocketService.get(url).then(function (resp) {
        //        $scope.myaudittaskauditee_list = resp.data.myaudittaskauditee_list;
        //        unlockUI();

        //    });
        //}

       
        $scope.viewtask = function (val) {
            $location.url('app/AtmTrnAuditorApproverView?hash=' + cmnfunctionService.encryptURL('auditcreation_gid=' + val + '&lspage=auditorapproverClosed'))
        }
       
        $scope.statusupdate = function (auditcreation_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusupdate.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    auditcreation_gid: auditcreation_gid
                }
                var url = 'api/AtmTrnAuditorMaker/EditAuditorMaker';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.auditcreation_gid = resp.data.auditcreation_gid
                    $scope.txtaudit_name = resp.data.audit_name;
                    $scope.txtstatus_update = resp.data.status_update;

                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.submit = function () {

                    var params = {

                        auditcreation_gid: auditcreation_gid,
                        status_update: $scope.status_update

                    }

                    var url = 'api/AtmTrnAuditorMaker/GetAuditorMakerStatus';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                            activate();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }
                    });

                    $modalInstance.close('closed');

                }

            }
        }

        $scope.approvalinformation = function (auditcreation_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/Approvalinformation.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    auditcreation_gid: auditcreation_gid
                }
                var url = 'api/AtmTrnAuditCreation/GetAuditeeSummaryList';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.multipleauditee_list = resp.data.multipleauditee_list;
                    unlockUI();

                });
                var url = 'api/AtmTrnAuditCreation/GetAssignedInformation';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.lblaudit_maker = resp.data.audit_maker;
                    $scope.lblaudit_checker = resp.data.audit_checker;
                    $scope.lblauditapprover_name = resp.data.auditapprover_name;
                    $scope.lblauditperiod_fromdate = resp.data.auditperiod_fromdate;
                    $scope.lblauditperiod_todate = resp.data.auditperiod_todate;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        $scope.getApprovalRequest = function (auditcreation_gid, checklistmaster_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/getApprovalRequest.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    auditcreation_gid: auditcreation_gid,
                    checklistmaster_gid: checklistmaster_gid
                }



                var url = 'api/AtmMstAuditMapping/GetAuditChecker';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.auditorchecker_list = resp.data.auditorchecker_list;



                });
                var url = 'api/AtmTrnAuditorMaker/TmpAllMembersDelete';
                SocketService.get(url).then(function (resp) {

                });
                $scope.ok = function () {
                    modalInstance.close('closed');
                };
                $scope.ApprovalMembercancel = function (tmpapprovalmember_gid) {
                    var params = {
                        tmpapprovalmember_gid: tmpapprovalmember_gid,
                        auditcreation_gid: auditcreation_gid,
                    }
                    var url = 'api/AtmTrnAuditorMaker/TmpApprovalMembersDelete';
                    SocketService.post(url, params).then(function (resp) {
                        $scope.approvalmember = resp.data.approvalmember;
                    });



                }

                $scope.Changed = function (cboapproval_membername) {
                    var params = {
                        approvalgid: $scope.cboapproval_membername.auditmapping_gid,
                        approvalname: $scope.cboapproval_membername.employee_name,
                        auditcreation_gid: auditcreation_gid,
                    }

                    lockUI();
                    var url = "api/AtmTrnAuditorMaker/TempApprovalMember";
                    SocketService.post(url, params).then(function (resp) {

                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000

                            });
                            unlockUI();
                            $scope.current = $state.current.name;
                            ScopeValueService.store("dataldCtrl", $scope);
                            //$state.go('app.pageredirect');
                            $scope.cboapproval_membername = "";
                            var params = {
                                auditcreation_gid: auditcreation_gid
                            }
                            var url = 'api/AtmTrnAuditorMaker/TmpApprovalMembersView';
                            SocketService.getparams(url, params).then(function (resp) {
                                $scope.approvalmember = resp.data.approvalmember;
                            });
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();

                        }
                    });


                }

                $scope.getapprovalclick = function () {
                    var params = {
                        approve_remarks: $scope.approve_remarks,
                        approve_type: $scope.approve_type,
                        auditcreation_gid: auditcreation_gid,

                    }

                    lockUI();
                    var url = "api/AtmTrnAuditorMaker/PostApprovalGet";
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
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

                        }
                    });
                }
            }
        }


        $scope.raisequery = function (auditcreation_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/raisequery.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    auditcreation_gid: auditcreation_gid
                }
                var url = 'api/AtmTrnAuditorMaker/EditAuditorMakerStatus';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.auditcreation_gid = resp.data.auditcreation_gid

                });


                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                var url = 'api/SystemMaster/GetEmployeelist';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.employee_list = resp.data.employeelist;
                    unlockUI();
                });



                $scope.submit = function () {

                    var params = {
                        auditcreation_gid: $scope.auditcreation_gid,
                        employe: $scope.cboemployee_name,
                        description: $scope.txtdescription,

                    }

                    var url = 'api/AtmTrnAuditorMaker/PostRaiseQuery';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                            activate();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }
                    });

                    $modalInstance.close('closed');

                }

            }
        }

        $scope.importexcel = function (auditcreation_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/importexcel.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {


                var params = {
                    auditcreation_gid: auditcreation_gid,
                }

                var url = 'api/AtmTrnSampling/GetSampleAuditor';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.sample_list = resp.data.sample_list;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.downloadtemplate_importexcel = function () {
                   
                    var Templateurl = apiManage.GetCommonData['TemplatePath'].Path;
                    var filename = "\ImportExcelSample.xlsx";
                    //var phyPath = resp.data.file_path;
                    var phyPath = Templateurl + filename;
                    var relPath = phyPath.split("EMS");
                    var relpath1 = relPath[1].replace("\\", "/");
                    var prefix = window.location.protocol + "//";
                    var hosts = window.location.host;
                    var str = prefix.concat(hosts, relpath1);
                    var link = document.createElement("a");
                    var name = filename.split('.');
                    link.download = name[0];
                    link.href = str;
                    link.click();
                }

                $scope.excelupload = function (val, val1, name) {

                    var fileInput = document.getElementById('fileimport');
                    var filePath = fileInput.value;

                    $scope.fileinputvalue = filePath;

                    // Allowing file type
                    var allowedExtensions = /(\.xls|\.xlsx|\.csv)$/i;

                    if (!allowedExtensions.exec(filePath)) {
                        Notify.alert('File Format Not Supported!', 'warning')
                        $modalInstance.close('closed');
                        //fileInput.value = '';
                    }
                    else if (filePath.includes("ImportExcelSample") == false) {
                        Notify.alert('File Name / Template Not Supported!', 'warning')
                        $modalInstance.close('closed');
                    }
                    else {
                        var item = {
                            name: val[0].name,
                            file: val[0]
                        };
                        var frm = new FormData();
                        frm.append('fileupload', item.file);
                        frm.append('file_name', item.name);
                        frm.append('auditcreation_gid', auditcreation_gid);
                        $scope.uploadfrm = frm;
                    }
                }

                $scope.uploadexcel = function () {

                    if ($scope.fileinputvalue == '' || $scope.fileinputvalue == undefined || $scope.fileinputvalue == null) {
                        Notify.alert('Kindly Select the Excel file', 'warning')
                    }
                    else {
                        var url = 'api/AtmTrnSampling/Sampleexcelupload';
                        lockUI();
                        SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                            if (resp.data.status == true) {
                                activate();
                                unlockUI();
                                Notify.alert(resp.data.message, {
                                    status: 'success',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                $modalInstance.close('closed');
                            }
                            else {
                                unlockUI();
                                Notify.alert(resp.data.message, {
                                    status: 'warning',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                $modalInstance.close('closed');
                            }
                            $("#fileimport").val('');
                        });
                    }

                }

                $scope.uploadexcelcancel = function () {
                    $("#fileimport").val('');
                };
            }
        }
    }
})();
