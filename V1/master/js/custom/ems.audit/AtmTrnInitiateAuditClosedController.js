// JavaScript source code
// JavaScript source code
// JavaScript source code
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AtmTrnInitiateAuditClosedController', AtmTrnInitiateAuditClosedController);

    AtmTrnInitiateAuditClosedController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$modal', 'cmnfunctionService'];

    function AtmTrnInitiateAuditClosedController($rootScope, $scope, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $modal, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AtmTrnInitiateAuditClosedController';

        activate();

        function activate() {


         var url = 'api/AtmTrnAuditCreation/GetMyClosedAuditTask';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.auditcreation_list = resp.data.auditcreation_list; 
            });

            var url = 'api/AtmTrnAuditCreation/GetAuditCreationCounts';
            SocketService.get(url).then(function (resp) {
                unlockUI()
                $scope.auditsonhold_count = resp.data.auditsonhold_count;
                $scope.closedaudit_count = resp.data.closedaudit_count;
                $scope.openaudit_count = resp.data.openaudit_count;
                $scope.Approvedaudit_count = resp.data.Approvedaudit_count;
                $scope.completedaudit_count = resp.data.completedaudit_count;
                $scope.rejectedaudit_count = resp.data.rejectedaudit_count;

            });
  
        }   


     $scope.openaudit = function () {
            $state.go('app.AtmTrnAuditCreationSummary');
        }

        $scope.approvedaudit = function () {
            $state.go('app.AtmTrnInitiateAuditApproved');
        }

        $scope.auditsonhold = function () {
            $state.go('app.AtmTrnInitiateAuditHold');
        }   
      $scope.closedaudit = function () {
            $state.go('app.AtmTrnInitiateAuditClosed');

        } 

       $scope.rejectedaudit = function () {
            $state.go('app.AtmTrnInitiateAuditRejected');
        }
       $scope.completedaudit = function () {
            $state.go('app.AtmTrnInitiateAuditCompleted');
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

        $scope.createaudit = function () {
            $state.go('app.AtmTrnCreateAudit');
        }
        $scope.view = function (val1, val2, val3) {
            $location.url('app/AtmTrnAudit360View?hash=' + cmnfunctionService.encryptURL('auditcreation_gid=' + val1 + '&checklistmaster_gid=' + val2 + '&sampleimport_gid=' + val3 + '&lspage=auditclosed'))
        }

       
        $scope.edit = function (val1) {
            $location.url('app/AtmTrnAuditCreationEdit?hash=' + cmnfunctionService.encryptURL('auditcreation_gid=' + val1))
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
                    $scope.status_remarks = resp.data.status_remarks;

                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.submit = function () {

                    var params = {

                        auditcreation_gid: auditcreation_gid,
                        status_update: $scope.status_update,
                        status_remarks: $scope.status_remarks
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

                //var url = 'api/AtmTrnSampling/GetSample';
                //SocketService.getparams(url, params).then(function (resp) {
                //    $scope.sample_list = resp.data.sample_list;
                //});

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
