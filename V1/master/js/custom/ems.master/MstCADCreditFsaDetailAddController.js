(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCADCreditFsaDetailAddController', MstCADCreditFsaDetailAddController);

    MstCADCreditFsaDetailAddController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','$sce', '$resource', 'DownloaddocumentService'];

        function MstCADCreditFsaDetailAddController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce, $resource, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCADCreditFsaDetailAddController';
        $scope.institution_gid = $location.search().institution_gid;
        var institution_gid = $scope.institution_gid;
        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;

        const lspagename = 'MstCADCreditFsaDetailAdd';

        lockUI();
        activate();
        function activate() {
            var params = {
                credit_gid: institution_gid,
                applicant_type: 'Institution'
            }

            var url = 'api/MstCADCreditAction/GetCreditOperationsView';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtinstitution_name = resp.data.company_name;
                $scope.txtstakeholder_type = resp.data.stakeholder_type;
            });

            var params = {
                application_gid: application_gid,
                credit_gid: institution_gid                
            }

            var url = 'api/MstCADCreditAction/GetFSASummary';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.FSASummary_list = resp.data.MstFSASummary_list;
            });

            $scope.balancetemp1 = false;
            $scope.balancetemp2 = false;
            $scope.PandLtemp1 = false;
            $scope.PandLtemp2 = false;
            $scope.summarytemp1 = false;
            $scope.summarytemp2 = false;          

        }
         
        $scope.balancesheet_change = function (cbobalancesheet) {
            cbobalancesheet = $scope.cbobalancesheet;
            if ($scope.cbobalancesheet == 'BalanceSheetTemplate1') {
                $scope.balancetemp1 = true;
                $scope.balancetemp2 = false;
                $scope.PandLtemp1 = false;
                $scope.PandLtemp2 = false;
                $scope.summarytemp1 = false;
                $scope.summarytemp2 = false;
            }
            else if($scope.cbobalancesheet == 'BalanceSheetTemplate2') {
                $scope.balancetemp2 = true;
                $scope.balancetemp1 = false;
                $scope.PandLtemp1 = false;
                $scope.PandLtemp2 = false;
                $scope.summarytemp1 = false;
                $scope.summarytemp2 = false;
            }
            else if($scope.cbobalancesheet == 'PLTemplate1') {
                $scope.PandLtemp1 = true;
                $scope.balancetemp2 = false;
                $scope.balancetemp1 = false;               
                $scope.PandLtemp2 = false;
                $scope.summarytemp1 = false;
                $scope.summarytemp2 = false;
            }
            else if($scope.cbobalancesheet == 'PLTemplate2') {
                $scope.PandLtemp2 = true;
                $scope.balancetemp1 = false; 
                $scope.balancetemp2 = false;                              
                $scope.PandLtemp1 = false;
                $scope.summarytemp1 = false;
                $scope.summarytemp2 = false;
            }
            else if($scope.cbobalancesheet == 'SummaryTemplate1') {
                $scope.summarytemp1 = true; 
                $scope.balancetemp1 = false;              
                $scope.balancetemp2 = false;                               
                $scope.PandLtemp1 = false;
                $scope.PandLtemp2 = false;
                $scope.summarytemp2 = false;
            }
            else if($scope.cbobalancesheet == 'SummaryTemplate2') {
                $scope.summarytemp2 = true; 
                $scope.balancetemp1 = false;              
                $scope.balancetemp2 = false;                               
                $scope.PandLtemp1 = false;
                $scope.PandLtemp2 = false;
                $scope.summarytemp1 = false;
            }
            else {
                $scope.balancetemp1 = false;
                $scope.balancetemp2 = false;
                $scope.PandLtemp1 = false;
                $scope.PandLtemp2 = false;
                $scope.summarytemp1 = false;
                $scope.summarytemp2 = false;
            }
        }

        // Balance Sheet Template - 1 

        $scope.uploadExcel_balancesheet1 = function () {
            if ($scope.fileinputvalue == '' || $scope.fileinputvalue == undefined || $scope.fileinputvalue == null) {
                Notify.alert('Kindly Select the Excel file', 'warning')
            }
            else {
                var url = 'api/MstCADCreditAction/ImportExcelBalanceSheetTemplate1';
                lockUI();
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                    if (resp.data.status == true) {
                        activate();
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        unlockUI();
                    }
                    else {
                        unlockUI();
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });

                    }
                    $scope.cbobalancesheet = '';
                    $("#fileimport").val('');
                });
            }

        }

        $scope.uploadbalancetemp1 = function (val, val1, name) {
            var application_gid = $scope.application_gid;
            var fileInput = document.getElementById('fileimportbalancetemp1');
            var filePath = fileInput.value;
            $scope.fileinputvalue = filePath;

            // Allowing file type
            var allowedExtensions = /(\.xls|\.xlsx|\.csv)$/i;

            if (!allowedExtensions.exec(filePath)) {
                Notify.alert('File Format Not Supported!', 'warning')
                fileInput.value = '';               
            }
            else {
                var item = {
                    name: val[0].name,
                    file: val[0]
                };
                var frm = new FormData();
                frm.append('fileupload', item.file);
                frm.append('file_name', item.name); 
                if (item.name.includes("Balance Sheet Template 1")) {
                    frm.append('application_gid', application_gid);
                    frm.append('credit_gid', institution_gid);
                    frm.append('template_type', $scope.cbobalancesheet);
                    frm.append('project_flag', "Default");
                    $scope.uploadfrm = frm;    
                } 
                else {
                    Notify.alert('Check the File Name','warning')
                    $scope.fileinputvalue = '';
                    $("#fileimportbalancetemp1").val('');
                }              
                            
            }
        }

        $scope.balancetemp1_cancel = function () {           
            var fileInput = document.getElementById('fileimportbalancetemp1');
            var filePath = fileInput.value;
            $scope.fileinputvalue = filePath;
            $scope.fileinputvalue = ''; 
            fileInput.value = '';       
        }

        $scope.balance_template1 = function () {
            // var filename = "Balance Sheet Template 1.xlsx";
            // //var phyPath = resp.data.file_path;
            // var phyPath = "E:\\Web\\EMS\\templates\\Balance Sheet Template 1.xlsx";
            // var relPath = phyPath.split("EMS");
            // var relpath1 = relPath[1].replace("\\", "/");
            // var hosts = window.location.host;
            // var prefix = "http://"
            // var str = prefix.concat(hosts, relpath1);
            // var link = document.createElement("a");
            // var name = filename.split('.');
            // link.download = name[0];
            // var uri = str;
            // link.href = uri;
            // link.click(); 

            var Templateurl = apiManage.GetCommonData['TemplatePath'].Path;
            var filename = "\Balance Sheet Template 1.xlsx";
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

        // Balance Sheet Template - 2 

        $scope.uploadExcel_balancesheet2 = function () {
            if ($scope.fileinputvalue == '' || $scope.fileinputvalue == undefined || $scope.fileinputvalue == null) {
                Notify.alert('Kindly Select the Excel file', 'warning')
            }
            else {
                var url = 'api/MstCADCreditAction/ImportExcelBalanceSheetTemplate2';
                lockUI();
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                    if (resp.data.status == true) {
                        activate();
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        unlockUI();
                    }
                    else {
                        unlockUI();
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });

                    }
                    $scope.cbobalancesheet = '';
                    $("#fileimport").val('');
                });
            }

        }   
        
        $scope.balancetemp2_cancel = function () {           
            var fileInput = document.getElementById('fileimportbalancetemp2');
            var filePath = fileInput.value;
            $scope.fileinputvalue = filePath;
            $scope.fileinputvalue = '';
            fileInput.value = '';
        }       

        $scope.uploadbalancetemp2 = function (val, val1, name) {
            var application_gid = $scope.application_gid;
            var fileInput = document.getElementById('fileimportbalancetemp2');
            var filePath = fileInput.value;
            $scope.fileinputvalue = filePath;

            // Allowing file type
            var allowedExtensions = /(\.xls|\.xlsx|\.csv)$/i;

            if (!allowedExtensions.exec(filePath)) {
                Notify.alert('File Format Not Supported!', 'warning')
                fileInput.value = '';               
            }
            else {
                var item = {
                    name: val[0].name,
                    file: val[0]
                };
                var frm = new FormData();
                frm.append('fileupload', item.file);
                frm.append('file_name', item.name); 
                if (item.name.includes("Balance Sheet Template 2")) {
                    frm.append('application_gid', application_gid);
                    frm.append('credit_gid', institution_gid);
                    frm.append('template_type', $scope.cbobalancesheet);
                    frm.append('project_flag', "Default");
                    $scope.uploadfrm = frm;    
                } 
                else {
                    Notify.alert('Check the File Name','warning')
                    $scope.fileinputvalue = '';
                    $("#fileimportbalancetemp2").val('');
                }              
                            
            }
        }

        $scope.balance_template2 = function () {
            // var filename = "Balance Sheet Template 2.xlsx";
            // //var phyPath = resp.data.file_path;
            // var phyPath = "E:\\Web\\EMS\\templates\\Balance Sheet Template 2.xlsx";
            // var relPath = phyPath.split("EMS");
            // var relpath1 = relPath[1].replace("\\", "/");
            // var hosts = window.location.host;
            // var prefix = "http://"
            // var str = prefix.concat(hosts, relpath1);
            // var link = document.createElement("a");
            // var name = filename.split('.');
            // link.download = name[0];
            // var uri = str;
            // link.href = uri;
            // link.click(); 

            var Templateurl = apiManage.GetCommonData['TemplatePath'].Path;
            var filename = "\Balance Sheet Template 2.xlsx";
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

        //P & L Template - 1 

        $scope.uploadexcel_pltemp1 = function () {
            if ($scope.fileinputpathPL == '' || $scope.fileinputpathPL == undefined || $scope.fileinputpathPL == null) {
                Notify.alert('Kindly Select the Excel file', 'warning')
            }
            else {

                var url = 'api/MstCADCreditAction/ImportProfitLoss';
                lockUI();
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                    if (resp.data.status == true) {
                        activate();
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        unlockUI();
                    }
                    else {
                        unlockUI();
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });

                    }
                    $scope.cbobalancesheet = '';
                    $("#fileimportpandltemp1").val('');
                });
            }

        }

        $scope.uploadpandltemp1 = function (val, val1, name) {
            var application_gid = $scope.application_gid;
            var fileInputPL = document.getElementById('fileimportpandltemp1');
            var filePath = fileInputPL.value;
            $scope.fileinputpathPL = filePath;

            // Allowing file type
            var allowedExtensions = /(\.xls|\.xlsx|\.csv)$/i;

            if (!allowedExtensions.exec(filePath)) {
                Notify.alert('File Format Not Supported!', 'warning')
                fileInputPL.value = '';
            }
            else {
                
                var item = {
                    name: val[0].name,
                    file: val[0]
                };
                var frm = new FormData();
                frm.append('fileupload', item.file);
                frm.append('file_name', item.name);
                if (item.name.includes("P&L Template 1")) {
                    frm.append('application_gid', application_gid);
                    frm.append('credit_gid', institution_gid);
                    frm.append('template_name', $scope.cbobalancesheet);
                    frm.append('project_flag', "Default");
                    $scope.uploadfrm = frm;    
                } 
                else {
                    Notify.alert('Check the File Name','warning')
                    $scope.fileinputpathPL = '';
                    $("#fileimportpandltemp1").val('');
                } 
               
            }
        }

        $scope.pandltemp1_cancel = function () {           
            var fileinputPL = document.getElementById('fileimportpandltemp1');
            var filePath = fileinputPL.value;
            $scope.fileinputpathPL = filePath;
            $scope.fileinputpathPL = '';
            fileinputPL.value = '';
        }
        
        $scope.pandL_template1 = function () {
            // var filename = "P&L Template 1.xlsx";
            // //var phyPath = resp.data.file_path;
            // var phyPath = "E:\\Web\\EMS\\templates\\P&L Template 1.xlsx";
            // var relPath = phyPath.split("EMS");
            // var relpath1 = relPath[1].replace("\\", "/");
            // var hosts = window.location.host;
            // var prefix = "http://"
            // var str = prefix.concat(hosts, relpath1);
            // var link = document.createElement("a");
            // var name = filename.split('.');
            // link.download = name[0];
            // var uri = str;
            // link.href = uri;
            // link.click(); 

            var Templateurl = apiManage.GetCommonData['TemplatePath'].Path;
            var filename = "\P&L Template 1.xlsx";
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
        
        // p & L Template -2 

        $scope.uploadexcel_pltemp2 = function () {
        if ($scope.fileinputvaluePL2 == '' || $scope.fileinputvaluePL2 == undefined || $scope.fileinputvaluePL2 == null) {
            Notify.alert('Kindly Select the Excel file', 'warning')
        }
        else {

            var url = 'api/MstCADCreditAction/ImportProfitLossTemp2';
            lockUI();
            SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                if (resp.data.status == true) {
                    activate();
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });

                }
                $scope.cbobalancesheet = '';
                $("#fileimportpandltemp2").val('');
            });
        }

        }

        $scope.uploadpandltemp2 = function (val, val1, name) {
        var application_gid = $scope.application_gid;
        var fileInputPL2 = document.getElementById('fileimportpandltemp2');
        var filePath = fileInputPL2.value;
        $scope.fileinputvaluePL2 = filePath;

        // Allowing file type
        var allowedExtensions = /(\.xls|\.xlsx|\.csv)$/i;

        if (!allowedExtensions.exec(filePath)) {
            Notify.alert('File Format Not Supported!', 'warning')
            fileInputPL2.value = '';
        }
        else {
            var item = {
                name: val[0].name,
                file: val[0]
            };
            var frm = new FormData();
            frm.append('fileupload', item.file);
            frm.append('file_name', item.name);  
            if (item.name.includes("P&L Template 2")) {
                frm.append('application_gid', application_gid);
                frm.append('credit_gid', institution_gid);
                frm.append('template_name', $scope.cbobalancesheet);
                frm.append('project_flag', "Default");
                $scope.uploadfrm = frm;    
            } 
            else {
                Notify.alert('Check the File Name','warning')
                $scope.fileinputpathPL = '';
                $("#fileimportpandltemp2").val('');
            }                
            }
        }  

        $scope.pandltemp2_cancel = function () {           
            var fileinputvaluePL2 = document.getElementById('fileimportpandltemp2');
            var filePath = fileinputvaluePL2.value;
            $scope.fileinputvaluePL2 = filePath;
            $scope.fileinputvaluePL2 = '';
            fileinputvaluePL2.value = '';
        } 

        $scope.pandL_template2 = function () {
            // var filename = "P&L Template 2.xlsx";
            // //var phyPath = resp.data.file_path;
            // var phyPath = "E:\\Web\\EMS\\templates\\P&L Template 2.xlsx";
            // var relPath = phyPath.split("EMS");
            // var relpath1 = relPath[1].replace("\\", "/");
            // var hosts = window.location.host;
            // var prefix = "http://"
            // var str = prefix.concat(hosts, relpath1);
            // var link = document.createElement("a");
            // var name = filename.split('.');
            // link.download = name[0];
            // var uri = str;
            // link.href = uri;
            // link.click(); 

            var Templateurl = apiManage.GetCommonData['TemplatePath'].Path;
            var filename = "\P&L Template 2.xlsx";
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

        //Summary Template - 1 Upload

        $scope.uploadExcel_summarytemp1 = function () {
            if ($scope.fileinputvalue == '' || $scope.fileinputvalue == undefined || $scope.fileinputvalue == null) {
                Notify.alert('Kindly Select the Excel file', 'warning')
            }
            else {
                var url = 'api/MstCADCreditAction/ImportExcelSummaryTemplate1';
                lockUI();
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                    if (resp.data.status == true) {
                        activate();
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        unlockUI();
                    }
                    else {
                        unlockUI();
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });

                    }
                    $scope.cbobalancesheet = '';
                    $("#fileimport").val('');
                });
            }

        }

        $scope.uploadsummarytemp1 = function (val, val1, name) {
            var application_gid = $scope.application_gid;
            var fileInput = document.getElementById('fileimportsummarytemp1');
            var filePath = fileInput.value;
            $scope.fileinputvalue = filePath;

            // Allowing file type
            var allowedExtensions = /(\.xls|\.xlsx|\.csv)$/i;

            if (!allowedExtensions.exec(filePath)) {
                Notify.alert('File Format Not Supported!', 'warning')
                fileInput.value = '';
            }
            else {
                var item = {
                    name: val[0].name,
                    file: val[0]
                };
                var frm = new FormData();
                frm.append('fileupload', item.file);
                frm.append('file_name', item.name);
                if (item.name.includes("Summary Template 1")) {
                    frm.append('application_gid', application_gid);
                    frm.append('credit_gid', institution_gid);
                    frm.append('template_name', $scope.cbobalancesheet);
                    frm.append('project_flag', "Default");
                    $scope.uploadfrm = frm;    
                } 
                else {
                    Notify.alert('Check the File Name','warning')
                    $scope.fileinputvalue = '';
                    $("#fileimportsummarytemp1").val('');
                }                
            }
        }
       
        $scope.summarytemp1_cancel = function () {           
            var fileInput = document.getElementById('fileimportsummarytemp1');
            var filePath = fileInput.value;
            $scope.fileinputvalue = filePath;
            $scope.fileinputvalue = '';  
            fileInput.value = '';           
        }

        $scope.summary_template1 = function () {
            // var filename = "Summary Template 1.xlsx";
            // //var phyPath = resp.data.file_path;
            // var phyPath = "E:\\Web\\EMS\\templates\\Summary Template 1.xlsx";
            // var relPath = phyPath.split("EMS");
            // var relpath1 = relPath[1].replace("\\", "/");
            // var hosts = window.location.host;
            // var prefix = "http://"
            // var str = prefix.concat(hosts, relpath1);
            // var link = document.createElement("a");
            // var name = filename.split('.');
            // link.download = name[0];
            // var uri = str;
            // link.href = uri;
            // link.click(); 

            var Templateurl = apiManage.GetCommonData['TemplatePath'].Path;
            var filename = "\Summary Template 1.xlsx";
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

        //Summary Template - 2 Upload

        $scope.uploadExcel_summarytemp2 = function () {
            if ($scope.fileinputvalue == '' || $scope.fileinputvalue == undefined || $scope.fileinputvalue == null) {
                Notify.alert('Kindly Select the Excel file', 'warning')
            }
            else {
                var url = 'api/MstCADCreditAction/ImportExcelSummaryTemplate2';
                lockUI();
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                    if (resp.data.status == true) {
                        activate();
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        unlockUI();
                    }
                    else {
                        unlockUI();
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });

                    }
                    $scope.cbobalancesheet = '';
                    $("#fileimport").val('');
                });
            }

        }

        $scope.uploadsummarytemp2 = function (val, val1, name) {
            var application_gid = $scope.application_gid;
            var fileInput = document.getElementById('fileimportsummarytemp2');
            var filePath = fileInput.value;
            $scope.fileinputvalue = filePath;

            // Allowing file type
            var allowedExtensions = /(\.xls|\.xlsx|\.csv)$/i;

            if (!allowedExtensions.exec(filePath)) {
                Notify.alert('File Format Not Supported!', 'warning')
                fileInput.value = '';               
            }
            else {
                var item = {
                    name: val[0].name,
                    file: val[0]
                };
                var frm = new FormData();
                frm.append('fileupload', item.file);
                frm.append('file_name', item.name);               
                if (item.name.includes("Summary Template 2")) {
                    frm.append('application_gid', application_gid);
                    frm.append('credit_gid', institution_gid);
                    frm.append('template_name', $scope.cbobalancesheet);
                    frm.append('project_flag', "Default");
                    $scope.uploadfrm = frm;
                }
                else {
                    Notify.alert('Check the File Name', 'warning')
                    $scope.fileinputvalue = '';
                    $("#fileimportsummarytemp1").val('');
                }
            }
        }

        $scope.summarytemp2_cancel = function () {           
            var fileInput = document.getElementById('fileimportsummarytemp2');
            var filePath = fileInput.value;
            $scope.fileinputvalue = filePath;
            $scope.fileinputvalue = '';
            fileInput.value = '';
        } 
        
        $scope.summary_template2 = function () {
            // var filename = "Summary Template 2.xlsx";
            // //var phyPath = resp.data.file_path;
            // var phyPath = "E:\\Web\\EMS\\templates\\Summary Template 2.xlsx";
            // var relPath = phyPath.split("EMS");
            // var relpath1 = relPath[1].replace("\\", "/");
            // var hosts = window.location.host;
            // var prefix = "http://"
            // var str = prefix.concat(hosts, relpath1);
            // var link = document.createElement("a");
            // var name = filename.split('.');
            // link.download = name[0];
            // var uri = str;
            // link.href = uri;
            // link.click(); 

            var Templateurl = apiManage.GetCommonData['TemplatePath'].Path;
            var filename = "\Summary Template 2.xlsx";
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
        
        // Excel Download

        $scope.download = function (val1, val2) {
            //var phyPath = val1;
            //var relPath = phyPath.split("EMS");
            //var relpath1 = relPath[1].replace("\\", "/");
            //var hosts = window.location.host;
            //var prefix = location.protocol + "//";
            //var str = prefix.concat(hosts, relpath1);
            //var link = document.createElement("a");
            //var name = val2.split('.');
            //link.download = name[0];
            //var uri = str;
            //link.href = uri;
            //link.click();
            DownloaddocumentService.Downloaddocument(val1, val2);

        }
        
        $scope.Back = function () {
            if (lspage == "StartCreditUnderwriting") {
                $location.url('app/MstStartCreditUnderwriting?application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else if (lspage == "PendingCADReview") {
                $location.url('app/MstCADApplicationEdit?application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else if (lspage == "CADApplicationEdit") {
                $location.url('app/MstCADApplicationEdit?application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else if (lspage == "CADAcceptanceCustomers") {
                $location.url('app/MstCADApplicationEdit?application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else {

            }
        }

        $scope.company_addguarantee = function () {
            $location.url('app/MstCADGuaranteeDetailAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_deferraldocchecklist = function () {
            $location.url('app/MstCADDocumentCheckList?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_covenantdocchecklist = function () {
            $location.url('app/MstCADCreditAddCovenantCheckList?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_economicadd = function () {
            $location.url('app/MstCADCreditEconomicCapitalAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_geneticadd = function () {
            $location.url('app/MstCADCreditCompanyDtlAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_PSLdata = function () {
            $location.url('app/MstCADCreditPSLDataFlaggingAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_supplier = function () {
            $location.url('app/MstCADCreditSuppliersDtlAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_buyer = function () {
            $location.url('app/MstCADCreditBuyerDtlAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }
        
        $scope.company_bankaccount = function () {
            $location.url('app/MstCADCreditBankAccountDtlAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_existingbankaccount = function () {
            $location.url('app/MstCADCreditExistingBankDtlAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_repayment = function () {
            $location.url('app/MstCADCreditRepaymentDtlAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_observation = function () {
            $location.url('app/MstCADCreditObservationAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }
        $scope.tan_verification = function () {
            $location.url('app/MstCADCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=TAN' + '&lspage=' + lspage);
        }
        $scope.companyllpno_vertification = function () {
            $location.url('app/MstCADCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=LLPNO' + '&lspage=' + lspage);
        }
        $scope.mcasign_vertification = function () {
            $location.url('app/MstCADCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=MCASIGNATURE' + '&lspage=' + lspage);
        }
        $scope.iecdetailed_vertification = function () {
            $location.url('app/MstCADCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=IECDETAILED' + '&lspage=' + lspage);
        }
        $scope.fssai_vertification = function () {
            $location.url('app/MstCADCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=FSSAI' + '&lspage=' + lspage);
        }
        $scope.fda_vertification = function () {
            $location.url('app/MstCADCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=FDA' + '&lspage=' + lspage);
        }
        $scope.gst_verification = function () {
            $location.url('app/MstCADCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=GST' + '&lspage=' + lspage);
        }
        $scope.lpgid_verification = function () {
            $location.url('app/MstCADCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=LPGID' + '&lspage=' + lspage);
        }
        $scope.shop_vertification = function () {
            $location.url('app/MstCADCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=SHOP' + '&lspage=' + lspage);
        }
        $scope.rcauthadv_vertification = function () {
            $location.url('app/MstCADCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=RCAUTHADV' + '&lspage=' + lspage);
        }
        $scope.rcsearch_vertification = function () {
            $location.url('app/MstCADCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=RCSEARCH' + '&lspage=' + lspage);
        }
        $scope.propertytax_vertification = function () {
            $location.url('app/MstCADCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=PROPERTYTAX' + '&lspage=' + lspage);
        }
        $scope.company_bankstatement = function () {
            $location.url('app/MstCADCreditBankStatementAnalysisAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }
        $scope.company_fsa = function () {
            $location.url('app/MstCADCreditFsaDetailAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }
        $scope.view = function (application_gid, credit_gid, template_name) {
            $location.url('app/MstCADCreditFSAView?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&template_name=' + template_name + '&lspage=' + lspage + '&lspagename=' + lspagename);
        }
        $scope.institution_bureauadd = function () {
            $location.url('app/MstCADCreditInstitutionDtlAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }
        $scope.crimecheck_record = function () {
            $location.url('app/MstCADCompanyCrimeCheckRecordAPI?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }
        $scope.crimecheckreport_realtime = function () {
            $location.url('app/MstCADCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=CRIMECHECKREPORTREALTIME' + '&lspage=' + lspage);    
        }
    }
})();
