﻿(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AtmTrnAuditorApproverSummaryController', AtmTrnAuditorApproverSummaryController);

    AtmTrnAuditorApproverSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$filter', '$route', 'ngTableParams'];

    function AtmTrnAuditorApproverSummaryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $filter, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AtmTrnAuditorApproverSummaryController';

        activate();

        function activate() {

           

                var url = 'api/AtmTrnAuditorMaker/GetOpenAuditorApprover';
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
            });

        }

        $scope.auditor_maker = function () {
            $location.url('app/AtmTrnAuditorMakerSummary')
        }

        $scope.auditor_checker = function () {
            $location.url('app/AtmTrnAuditorCheckerSummary')
        }

        $scope.auditor_approver = function () {
            $state.go('app.AtmTrnAuditorApproverSummary');
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

       
       


        $scope.auditraisequery = function (val1) {
            $location.url('app/AtmTrnAuditorApproverRaiseQuery?auditcreation_gid=' + val1 + '&lspage=auditorapproveropen')
        }

        $scope.updateobservations = function (val1) {
            $location.url('app/AtmTrnApproverCheckpointObservation?auditcreation_gid=' + val1)
        }
        $scope.viewtask = function (val) {
            $location.url('app/AtmTrnAuditorApproverView?auditcreation_gid=' + val + '&lspage=auditorapproveropen')
        }
        $scope.approval = function (val2) {
            $location.url('app/AtmTrnApproval?auditcreation_gid=' + val2)
        }
        $scope.assignedquery = function (val) {
            $location.url('app/AtmTrnAssignedQuery?auditcreation_gid=' + val);
        }
        $scope.auditapproval = function (val3) {
            $location.url('app/AtmTrnAuditorApproval?auditcreation_gid=' + val3)
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
        $scope.statusremarks = function (auditcreation_gid, status_remarks) {
            var modalInstance = $modal.open({
                templateUrl: '/statusremarks.html',
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
                lockUI();
                var url = 'api/AtmTrnAuditorMaker/GetStatusRemarks';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtremarks = resp.data.status_remarks;

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
