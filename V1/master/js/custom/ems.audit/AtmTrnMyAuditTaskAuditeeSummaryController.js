(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AtmTrnMyAuditTaskAuditeeSummaryController', AtmTrnMyAuditTaskAuditeeSummaryController);

    AtmTrnMyAuditTaskAuditeeSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$filter', '$route', 'ngTableParams', 'cmnfunctionService'];

    function AtmTrnMyAuditTaskAuditeeSummaryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $filter, $route, ngTableParams, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AtmTrnMyAuditTaskAuditeeSummaryController';
        //var searchObject = cmnfunctionService.decryptURL($location.search().hash);

        //$scope.lspage = searchObject.lspage;
        //var lspage = $scope.lspage;

       

        activate();
        function activate() {           

            var url = 'api/AtmTrnMyAuditTaskAuditee/GetOpenAuditee';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.myaudittaskauditee_list = resp.data.myaudittaskauditee_list;
                unlockUI();

            });

            var url = 'api/AtmTrnMyAuditTaskAuditee/GetMyAuditTaskCounts';
            SocketService.get(url).then(function (resp) {
                unlockUI()
                $scope.auditsonhold_count = resp.data.auditsonhold_count;
                $scope.closedaudit_count = resp.data.closedaudit_count;
                $scope.openaudit_count = resp.data.openaudit_count;
                $scope.taggedsample_count = resp.data.taggedsample_count;
                $scope.completed_count = resp.data.completedaudit_count;

            });

          
        }

        $scope.auditee_maker = function () {
            $location.url('app/AtmTrnMyAuditTaskAuditeeSummary')
        }

        //$scope.auditee_checker = function () {
        //    $location.url('app/AtmTrnMyAuditeeCheckerSummary')
        //}
        $scope.auditee_checker = function () {
            $location.url('app/AtmTrnMyAuditeeCheckerSummary')
        }
        $scope.open_audit = function () {
            $location.url('app/AtmTrnMyAuditTaskAuditeeSummary')
        }

        $scope.hold_audit = function () {
            $location.url('app/AtmTrnHoldAuditeeSummary')
        }

        $scope.closed_audit = function () {
            $location.url('app/AtmTrnClosedAuditeeSummary')
        }

        $scope.tagged_items = function () {
            $location.url('app/AtmTrnTaggedAuditeeSummary')
        }

        $scope.completed_audit = function () {
            $location.url('app/AtmTrnCompletedAuditeeSummary')
        }

        

        $scope.viewtask = function (val, val1) {
            $location.url('app/AtmTrnMyAuditTaskAuditeeMakerView?hash=' + cmnfunctionService.encryptURL('auditcreation_gid=' + val + '&sampleimport_gid=' + val1 + '&lspage=auditeemakeropen'))
        }

       
        $scope.auditeecheckerapproval = function (val3) {
            $location.url('app/AtmTrnAuditeeCheckerApproval?hash=' + cmnfunctionService.encryptURL('auditcreation_gid=' + val3))
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
                var url = 'api/AtmTrnMyAuditTaskAuditee/EditMyAuditTaskAuidtee';
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

                    var url = 'api/AtmTrnMyAuditTaskAuditee/PostAuditeeRaiseQuery';
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

                var url = 'api/AtmTrnSampling/GetSample';
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
                        var url = 'api/AtmTrnSampling/ImportExcelSample';
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


