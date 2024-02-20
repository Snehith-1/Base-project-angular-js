

(function () {
    'use strict';
    angular
        .module('angle')
        .controller('AtmTrnCheckerCheckpointObservationController', AtmTrnCheckerCheckpointObservationController);

    AtmTrnCheckerCheckpointObservationController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function AtmTrnCheckerCheckpointObservationController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AtmTrnCheckerCheckpointObservationController';
        //$scope.auditcreation_gid = $location.search().auditcreation_gid;
        var auditcreation_gid = $location.search().auditcreation_gid;
        var auditcreation2checklist_gid = $location.search().auditcreation2checklist_gid;
        $scope.sampleimport_gid = $location.search().sampleimport_gid;
        var sampleimport_gid = $scope.sampleimport_gid;

        activate();

        function activate() {

            var url = 'api/AtmTrnAuditorMaker/EditAuditorMakerCheckpointObservation';
            var params = {
                auditcreation_gid: auditcreation_gid,

            };
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.cboauditfrequency = resp.data.auditfrequency_gid,
                  $scope.cboauditfrequency = resp.data.auditfrequency_name,
                 $scope.cboauditname = resp.data.checklistmaster_gid,
                  $scope.cboauditname = resp.data.audit_name,
                  $scope.cboauditmaker = resp.data.employee_gid,
                  $scope.cboauditmaker = resp.data.audit_maker,
                 $scope.cboauditchecker = resp.data.auditmapping_gid,
                  $scope.cboauditchecker = resp.data.audit_checker,
                  $scope.cboauditapprover = resp.data.employee_gid,
                  $scope.cboauditapprover = resp.data.audit_approver,
                 $scope.cboauditpriority = resp.data.auditpriority_gid,
                  $scope.cboauditpriority = resp.data.auditpriority_name,
                $scope.txtdue_date = resp.data.due_date,
                $scope.txtreport_date = resp.data.report_date,
                $scope.txtperiod_from = resp.data.periodfrom_date,
                $scope.txtperiod_to = resp.data.auditperiod_to,
                $scope.txtaudit_ref_no = resp.data.audit_uniqueno,
                  $scope.cboauditmaker_name = resp.data.auditmaker_name,
                $scope.cboauditchecker_name = resp.data.auditchecker_name,
                unlockUI();
            });
            var url = 'api/AtmTrnAuditorMaker/AuditorMakerCheckpointObservationView';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.makercheckpointobservation_list = resp.data.makercheckpointobservationview_list;
                $scope.txttotal_score = resp.data.total_score;

                for (var i = 0; i < $scope.makercheckpointobservation_list.length; i++) {
                    $scope.makercheckpointobservation_list[i].checked = true;

                }

                angular.forEach($scope.makercheckpointobservation_list, function (value, key) {

                    if (value.capture_score == value.yes_score) {
                        value.yes_radio = true;
                    }
                    else if (value.capture_score == value.no_score) {
                        value.no_radio = true;
                    }
                    else if (value.capture_score == value.partial_score) {
                        value.partialscore_radio = true;
                    }
                    else if (value.capture_score == value.na_score) {
                        value.nascore_radio = true;
                    }
                });
            });
            var url = 'api/AtmTrnSampling/GetSampleAuditor';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI()
                $scope.sample_list = resp.data.sample_list

            });
        }
        $scope.onselected = function (val, val1, val2) {
            $scope.total_score = val;
            $scope.auditcreation2checklist_gid = val1;
            $scope.checklistmasteradd_gid = val2;

            var params = {
                auditcreation_gid: auditcreation_gid,
                auditcreation2checklist_gid: $scope.auditcreation2checklist_gid,
                checklistmasteradd_gid: $scope.checklistmasteradd_gid,
                capture_totalscore: $scope.total_score,

            }


            var url = 'api/AtmTrnAuditorMaker/PostAuditorMakerObservationTotalAmount';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.txttotal_score = resp.data.total_amount;

                    unlockUI()
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
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
        $scope.submitobservation = function () {
            var assignList = [];
            angular.forEach($scope.makercheckpointobservation_list, function (val) {

                if (val.checked == true) {
                    var auditcreation2checklist_gid = val.auditcreation2checklist_gid;
                    assignList.push(auditcreation2checklist_gid);

                }
            });
            if (assignList.length == 0) {
                Notify.alert('Select Atleast One Record!');
                return false;
                unlockUI();
            }
            var params = {
                auditcreation2checklist_gid: assignList,
                auditcreation_gid: auditcreation_gid,
            }

            var url = 'api/AtmTrnAuditorMaker/PostAuditorMakerCheckpointObservation';
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    Notify.alert('Check point  Successfully!', 'success');
                    $state.go('app.AtmTrnAuditorCheckerSummary');
                }

                else {
                    Notify.alert('Select Atleast One..!!', 'warning')
                }

                $scope.txttotal_score = "";
            });

        }



        $scope.back = function (val) {
            $state.go('app.AtmTrnAuditorCheckerSummary');
        }


        $scope.checkpointintent = function (auditcreation_gid, checkpoint_intent) {
            var modalInstance = $modal.open({
                templateUrl: '/checkpointintent.html',
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
                var url = 'api/AtmTrnAuditorMaker/GetAuditCreationIntent';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtcheckpointintent = resp.data.checkpoint_intent;

                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }
        $scope.description = function (auditcreation_gid, checkpoint_description) {
            var modalInstance = $modal.open({
                templateUrl: '/checkpointdescription.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                'use strict';

                angular
                    .module('angle')
                var params = {
                    auditcreation_gid: auditcreation_gid
                }
                lockUI();
                var url = 'api/AtmTrnAuditorMaker/GetAuditCreationDescription';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtcheckpointdescription = resp.data.checkpoint_description;

                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }
        $scope.notetoauditor = function (auditcreation_gid, noteto_auditor) {
            var modalInstance = $modal.open({
                templateUrl: '/notetoauditor.html',
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
                var url = 'api/AtmTrnAuditorMaker/GetAuditCreationAuditor';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtnotetoauditor = resp.data.noteto_auditor;

                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }


        $scope.btntaguser = function (auditcreation_gid, sampleimport_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/taguser.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    sampleimport_gid: sampleimport_gid
                }

                var url = 'api/AtmTrnSampling/GetSampleName';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.sampleimport_gid = resp.data.sampleimport_gid;
                    $scope.txtsample_name = resp.data.sample_name;
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

                $scope.btnconfirm = function () {

                    var params = {
                        employelist: $scope.cboemployee_name,
                        sample_name: $scope.txtsample_name,
                        sampleimport_gid: sampleimport_gid,
                        auditcreation_gid: auditcreation_gid
                    }

                    var url = 'api/AtmTrnSampling/GetTagUser';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $location.url('app/AtmTrnCheckerCheckpointObservation?auditcreation_gid=' + val1)
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

        $scope.showPopover = function (sampleimport_gid, sample_name) {
            var modalInstance = $modal.open({
                templateUrl: '/showemployee.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    sampleimport_gid: sampleimport_gid
                }
                lockUI();
                var url = 'api/AtmTrnSampling/GetEmployeeName';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.employee_name = resp.data.employee_name;
                    $scope.sample_name = resp.data.sample_name;
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }


        $scope.raisequery = function (sampleimport_gid, auditcreation_gid) {
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
                    sampleimport_gid: sampleimport_gid
                }
                var url = 'api/AtmTrnSampling/EditSampleQuery';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.sampleimport_gid = resp.data.sampleimport_gid

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
                        sampleimport_gid: $scope.sampleimport_gid,
                        employelist: $scope.cboemployee_name,
                        description: $scope.txtdescription,
                        auditcreation_gid: auditcreation_gid,

                    }

                    var url = 'api/AtmTrnSampling/PostRaiseQuery';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $location.url('app/AtmTrnSampling?auditcreation_gid=' + val1)
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

        $scope.assignedquery = function (val, val1) {
            $location.url('app/AtmTrnSampleAssignedQuery?auditcreation_gid=' + val + '&sampleimport_gid=' + val1);
        }


        $scope.view = function (val, val1) {
            var sampleimport_gid = $scope.sampleimport_gid;
            var auditcreation_gid = $scope.auditcreation_gid;
            $location.url('app/AtmTrnSamplingView?auditcreation_gid=' + val + '&sampleimport_gid=' + val1)
        }


        $scope.back = function () {
            $state.go('app.AtmTrnAuditorCheckerSummary');
        }

        $scope.samplequery = function (val, val1) {
            $location.url('app/AtmTrnSampleQueryAuditor?auditcreation_gid=' + val + '&sampleimport_gid=' + val1)
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
                        var url = 'api/AtmTrnSampling/ImportExcelSample';
                        lockUI();
                        SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                            $modalInstance.close('closed');
                            if (resp.data.status == true) {
                                location.reload();
                                unlockUI();
                                Notify.alert(resp.data.message, {
                                    status: 'success',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                //  $location.url('app/AtmTrnAudit360View?auditcreation_gid=' + val1 + '&checklistmaster_gid=' + val2 + '&sampleimport_gid=' + val3)
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