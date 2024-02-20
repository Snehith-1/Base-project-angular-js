(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrTrnCreditGroupBankStatementAnalysisAddController', AgrTrnCreditGroupBankStatementAnalysisAddController);

        AgrTrnCreditGroupBankStatementAnalysisAddController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','$sce', '$resource','DownloaddocumentService','cmnfunctionService'];

    function AgrTrnCreditGroupBankStatementAnalysisAddController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce, $resource,DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrTrnCreditGroupBankStatementAnalysisAddController';
        $scope.group_gid = $location.search().group_gid;
        var group_gid = $scope.group_gid;
        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;
      
        activate();
        lockUI();
        function activate() { 
              var params = {
                credit_gid: group_gid,
                applicant_type: 'Group'
            }

            var url = 'api/AgrTrnAppCreditUnderWriting/GetCreditOperationsView';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtgroup_name = resp.data.group_name;
                $scope.txtgroup_type = resp.data.group_type;
            }); 

            var params = {
                credit_gid: group_gid,
                application_gid : application_gid
             }
             var url = 'api/AgrTrnAppCreditUnderWriting/GetBankStatementList';
             SocketService.getparams(url, params).then(function (resp) {
                 unlockUI();
                $scope.BankStatement_list = resp.data.BankStatement_list;
              });

        }

        $scope.upload = function (val, val1, name) {
            var application_gid = $scope.application_gid;
            var fileInput = document.getElementById('fileimport');
            var filePath = fileInput.value;
            $scope.fileinputvalue = filePath;

            // Allowing file type
            var allowedExtensions = /(\.xls|\.xlsx|\.csv)$/i;

            if (!allowedExtensions.exec(filePath)) {
                Notify.alert('File Format Not Supported!', 'warning')
                fileInput.value = '';               
            }
            else {
                var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "documentformatonly");

                        if (IsValidExtension == false) {
                            Notify.alert("File format is not supported..!", {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            return false;
                        }
                var item = {
                    name: val[0].name,
                    file: val[0]
                };
                var frm = new FormData();
                frm.append('fileupload', item.file);
                frm.append('file_name', item.name);               
                frm.append('application_gid', application_gid);
                frm.append('credit_gid', group_gid);
                frm.append('project_flag', "documentformatonly");
                $scope.uploadfrm = frm;                
            }
        }

        $scope.uploadExcel = function () { 
            if ($scope.fileinputvalue == '' || $scope.fileinputvalue == undefined || $scope.fileinputvalue == null) {
                Notify.alert('Kindly Select the Excel file', 'warning')
            }
            else{ 
                var url = 'api/AgrTrnAppCreditUnderWriting/ImportExcelBankStatement';
                lockUI();
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {    
                    if (resp.data.status == true) {                       
                        unlockUI();
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        activate();
                    }
                    else {
                        unlockUI();
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });    
                    }
                    $("#fileimport").val('');
                });
            }           
           
        }

        $scope.Excel_cancel = function () {
            lockUI();
            var fileInput = document.getElementById('fileimport');
            var filePath = fileInput.value;
            $scope.fileinputvalue = filePath;
            fileInput.value = '';  
            activate();
        }

        $scope.templateexport_excel = function () {
            //var filename = "Bank Statement Analysis.xlsx";
            ////var phyPath = resp.data.file_path;
            //var phyPath = "E:\\Web\\EMS\\templates\\Bank Statement Analysis.xlsx";
            //var relPath = phyPath.split("EMS");
            //var relpath1 = relPath[1].replace("\\", "/");
            //var hosts = window.location.host;
            //var prefix = "http://"
            //var str = prefix.concat(hosts, relpath1);
            //var link = document.createElement("a");
            //var name = filename.split('.');
            //link.download = name[0];
            //var uri = str;
            //link.href = uri;
            //link.click();

            var Templateurl = apiManage.GetCommonData['TemplatePath'].Path;
            var filename = "\Bank Statement Analysis.xlsx";
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

        $scope.export_excel = function () { 
            var params = {
                credit_gid: group_gid,
                application_gid : application_gid
             }  
            lockUI();
            var url = 'api/AgrTrnAppCreditUnderWriting/BankStatementExportExcel';
            SocketService.post(url,params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    DownloaddocumentService.Downloaddocument(resp.data.lscloudpath, resp.data.lsname);
                    // var phyPath = resp.data.lspath;
                    // var relPath = phyPath.split("EMS");
                    // var relpath1 = relPath[1].replace("\\", "/");
                    // var hosts = window.location.host;
                    // var prefix = location.protocol + "//";
                    // var str = prefix.concat(hosts, relpath1);
                    // var link = document.createElement("a");
                    // var name = resp.data.lsname.split('.');
                    // link.download = name[0];
                    // var uri = str;
                    // link.href = uri;
                    // link.click();
                }
                else {
                    unlockUI();
                    Notify.alert('Error Occurred While Export !')
                }

            });
        }  

        $scope.Back = function () {
            if (lspage == "StartCreditUnderwriting") {
                $location.url('app/AgrTrnStartCreditUnderwriting?application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else if (lspage == "PendingCADReview") {
                $location.url('app/AgrTrnCADApplicationEdit?application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else if (lspage == "CADApplicationEdit") {
                $location.url('app/AgrTrnCADApplicationEdit?application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else if (lspage == "CADAcceptanceCustomers") {
                $location.url('app/AgrTrnCADApplicationEdit?application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else {

            }
        }

        $scope.group_docchecklist = function () {
            $location.url('app/AgrTrnGroupDocCheckList?application_gid=' + application_gid + '&group_gid=' + group_gid + '&lspage=' + lspage);
        }

        $scope.group_covenantdocchecklist = function () {
            $location.url('app/AgrTrnGroupCovenantDocChecklist?application_gid=' + application_gid + '&group_gid=' + group_gid + '&lspage=' + lspage);
        }

        $scope.group_bureauadd = function () {
            $location.url('app/AgrTrnCreditGroupDtlAdd?application_gid=' + application_gid + '&group_gid=' + group_gid + '&lspage=' + lspage);
        }

        $scope.group_bankaccount = function () {
            $location.url('app/AgrTrnCreditGroupBankAcctAdd?application_gid=' + application_gid + '&group_gid=' + group_gid + '&lspage=' + lspage);
        }

        $scope.group_existingbankaccount = function () {
            $location.url('app/AgrTrnCreditGroupExistingBankAdd?application_gid=' + application_gid + '&group_gid=' + group_gid + '&lspage=' + lspage);
        }

        $scope.group_PSLdata = function () {
            $location.url('app/AgrTrnCreditGroupPSLDataFlagAdd?application_gid=' + application_gid + '&group_gid=' + group_gid + '&lspage=' + lspage);
        }       

        $scope.group_repayment = function () {
            $location.url('app/AgrTrnCreditGroupRepaymentAdd?application_gid=' + application_gid + '&group_gid=' + group_gid + '&lspage=' + lspage);
        }

        $scope.group_observation = function () {
            $location.url('app/AgrTrnCreditGroupObservationAdd?application_gid=' + application_gid + '&group_gid=' + group_gid + '&lspage=' + lspage);
        }
        
        $scope.company_bankstatement = function () {
            $location.url('app/AgrTrnCreditGroupBankStatementAnalysisAdd?application_gid=' + application_gid + '&group_gid=' + group_gid + '&lspage=' + lspage);
        }
        
    }
})();