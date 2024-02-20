(function () {
    'use strict';

    angular
        .module('angle')
        .controller('idasTrnFile2Despatch', idasTrnFile2Despatch);

    idasTrnFile2Despatch.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', '$cookies', '$filter', 'ngTableParams', '$timeout', 'DownloaddocumentService','cmnfunctionService'];

    function idasTrnFile2Despatch($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, $cookies, $filter, ngTableParams, $timeout, DownloaddocumentService,cmnfunctionService) {
        var vm = this;
        $scope.tabbatch = false;
        $scope.tabbox = false;
        $scope.tabdespatch = false;
        $scope.tabpendingbatch = false;

        activate();

        function activate() {
            $scope.DivFile = false;
            $scope.IsCreate = false;
            $scope.IsCreateBox = false;
            $scope.user = {};
            $scope.box = {};
            $scope.tab = {};
           
            // $scope.totalDisplayedBatch = 100;
            // $scope.totalDisplayedBox = 100;
            $scope.totalDisplayedDespatch = 100;
            $scope.totalDisplayedpendingbatch = 100;
            vm.calenderDespatch = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.openDespatch = true;
            };
            vm.calenderBox = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.openBox = true;
            };
           
          
            var url = window.location.href;
            var relPath = url.split("lstab=");
            var relpath1 = relPath[1];
            if (relpath1 != undefined)
            {
                if(relpath1=="batch")
                {
                    $scope.tabbatch = true;
                }
                else if(relpath1=="box")
                {
                    $scope.tabbox = true;
                }
                else if(relpath1=="despatch")
                {
                    $scope.tabdespatch = true;
                }
                else if (relpath1 == "pendingbatch") {
                    $scope.tabpendingbatch = true;
                }
            }
            else {
                if ($scope.tab.activeTabId == undefined)
                {
                    $scope.tabpendingbatch = true;
                }
                else if ($scope.tab.activeTabId=='batch')
                {
                    $scope.tabbatch = true;

                }
                else if($scope.tab.activeTabId=='box')
                {
                    $scope.tabbox = true;
                }
                else if($scope.tab.activeTabId=='despatch')
                {
                    $scope.tabdespatch = true;
                }
                else if ($scope.tab.activeTabId == 'pendingbatch') {
                    $scope.tabpendingbatch = true;
                }
            }


            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };

            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];

           

            var url = 'api/IdasTrnFile2Despatch/GetFileCount';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.totalfile_count = resp.data.totalfile_count;
                $scope.taggedfile_count = resp.data.taggedfile_count;
                $scope.untaggedfile_count = resp.data.untaggedfile_count;

                $scope.totalbox_count = resp.data.totalbox_count;
                $scope.taggedbox_count = resp.data.taggedbox_count;
                $scope.untaggedbox_count = resp.data.untaggedbox_count;

                $scope.despatch_count = resp.data.despatch_count;
             

            });

            var url = 'api/IdasTrnFile2Despatch/BatchSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.batch_list = resp.data.MdlbatchSummary;
               
                if( $scope.batch_list==null){
                    $scope.totalBatch=0;
                }
                else{
                    $scope.totalBatch=$scope.batch_list.length;
                 }
               
              
            });

            var url = 'api/IdasTrnFile2Despatch/CartonBoxSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.box_list = resp.data.MdlCartonBoxSummary;
              if($scope.box_list!=null){
                $scope.totalBox=$scope.box_list.length;
              }
               else{
                $scope.totalBox=0;
               }
               
            });

            var url = 'api/IdasTrnFile2Despatch/DespatchSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                if($scope.despatch_list!=null){
                    $scope.totalDespatch=0;
                }
                else{
                    $scope.despatch_list = resp.data.MdlDespatch;
                    $scope.totalDespatch=$scope.despatch_list.length;
                }
               
            });

            var url = 'api/IdasTrnFile2Despatch/BatchPendingSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.pendingbatch_list = resp.data.MdlbatchSummary;

                if ($scope.pendingbatch_list == null) {
                    $scope.totalpendingbatch = 0;
                }
                else {
                    $scope.totalpendingbatch = $scope.pendingbatch_list.length;
                }
            });

        }
      
        $scope.loadMoreDespatch = function (pagecount) {
            if(pagecount==undefined){
                Notify.alert("Enter the Total Summary Count","warning");
                return;
            }
            lockUI();
            var Number = parseInt(pagecount);
            
            $scope.totalDisplayedDespatch += Number;
            unlockUI();
        };
        $scope.loadMorependingbatch = function (pagecount) {
            if (pagecount == undefined) {
                Notify.alert("Enter the Total Summary Count", "warning");
                return;
            }
            lockUI();
            var Number = parseInt(pagecount);

            $scope.totalDisplayedpendingbatch += Number;
            unlockUI();
        };

        $scope.checkallBatch = function (selected) {
            angular.forEach($scope.batch_list, function (val) {  
                
                    val.checked = selected;
            });
        }
        $scope.checkallBox = function (selected) {
            angular.forEach($scope.box_list, function (val) {
                val.checked = selected;

            });

         
        }
        $scope.UpdateStamp=function(sanction_gid,stampref_no)
        {
            var url = 'api/IdasTrnFile2Despatch/BatchStampNo';
            var params = {
                sanction_gid: sanction_gid,
                stampref_no:stampref_no
            }
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, 'success')
                }
                else {
                    Notify.alert(resp.data.message)
                }
                $scope.tab.activeTabId = 'batch';
                activate();


            });

        }
        $scope.boxclose=function()
        {
            $scope.IsCreateBox = false;
        }
        $scope.createBox = function () {
            var batch_gid;
            angular.forEach($scope.batch_list, function (val) {

                if (val.checked == true) {
                    batch_gid = val.batch_gid;

                   
                }
            });
            if (batch_gid == undefined)
                {
                Notify.alert('Select Atleast One Record!')
            }
            else {
                $scope.IsCreateBox = true;
            }
        }
        $scope.BoxSubmit=function()
        {
            var batch_list = [];
            var batch_gid;
            angular.forEach($scope.batch_list, function (val) {

                if (val.checked == true) {
                    batch_gid = val.batch_gid;

                    batch_list.push(batch_gid);

                }
            });
            var params = {

                batch_gid: batch_list,
                stampref_no: $scope.box.stampref_no,
                cartonbox_date: $scope.box.BoxedDate,
                remarks: $scope.box.boxRemarks,
                boxbarcoderef_no: $scope.box.barcoderef_no,
            }
            if (batch_gid != undefined) {
                var url = 'api/IdasTrnFile2Despatch/CreateCartonBox';
                lockUI()
                SocketService.post(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        unlockUI()
                        Notify.alert(resp.data.message, 'success')
                    }
                    else {
                        unlockUI();

                        Notify.alert(resp.data.message)
                    }
                    $scope.tab.activeTabId = 'batch';
                    activate();
                });
            }
            else {
                Notify.alert('Select Atleast One Record!')
            }
        }
        $scope.gotodespatchdetails = function () {
            $state.go('app.despatchdetails')
        }
        $scope.boxupdate = function () {
            $state.go('app.boxStampupdate')
        }
        $scope.filedetails = function () {
            $state.go('app.fileStamprefupdate')
        }
        $scope.despatchback = function () {
            $state.go('app.idasTrnFile2Despatch')
        }
        $scope.backfiles = function () {
            $state.go('app.idasTrnFile2Despatch')
        }
        $scope.createDespatch = function () {
            var box_list = [];
            var box_gid;
            angular.forEach($scope.box_list, function (val) {

                if (val.checked == true) {
                    box_gid = val.cartonbox_gid;

                    box_list.push(box_gid);

                }
            });

            if(box_gid!=undefined)
            {
                var params = {
                    conversationdocument_gid: 'undefine'
                }
                var url = 'api/IdasTrnSanctionDoc/deleteconversedoc';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {

                });
                unlockUI();
                $scope.IsCreate = true;
            }
            else {
                Notify.alert('Select Atleast One Record!')
            }
          
          
        }
        $scope.close=function()
        {
            $scope.IsCreate = false;
        }
        $scope.FileShow = function () {
            if ($scope.DivFile == true) {
                $scope.DivFile = false;
            }
            else {
                $scope.DivFile = true;
            }
        }
        $scope.despatchdocumentupload = function (val, val1, name) {
            var item = {
                name: val[0].name,
                file: val[0]
            };
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "");

            if (IsValidExtension == false) {
                Notify.alert("File format is not supported..!", {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                return false;
            }
            var frm = new FormData();
            frm.append('fileupload', item.file);
            frm.append('file_name', item.name);
            frm.append('document_name', $scope.documentname);
            frm.append('document_title', $scope.user.txtdocument_title);
            frm.append('project_flag', "Default");
            $scope.uploadfrm = frm;
            var url = 'api/IdasTrnSanctionDoc/ConversationDocUpload';
            lockUI();
            SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                $("#commonupload").val('');
                $scope.txtdocument_title = '';
                if (resp.data.status == true) {
                    unlockUI();
                    Notify.alert('Document Uploaded Successfully..!!', 'success')

                    var url = 'api/IdasTrnSanctionDoc/GetConverseDoc';

                    SocketService.get(url).then(function (resp) {

                        $scope.uploaddocument = resp.data.uploaddocument;

                    });
                }
                else {
                    unlockUI();
                    Notify.alert('File Format Not Supported!')

                }

            });

        }
        $scope.downloadsdocument = function (val1, val2) {

            //var phyPath = val1;

            //var relPath = phyPath.split("EMS");
            //var relpath1 = relPath[1].replace("\\", "/");
            //var hosts = window.location.host;
            //var prefix = location.protocol + "//";
            //var str = prefix.concat(hosts, relpath1);
            //var link = document.createElement("a");
            //var name = val2.split(".")
            //link.download = val2;
            //var uri = str;
            //link.href = uri;
            //link.click();

            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.deletedocument = function (val) {
            var params = {
                conversationdocument_gid: val
            }
            var url = 'api/IdasTrnSanctionDoc/deleteconversedoc';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    Notify.alert('Document deleted Successfully..!!', 'success')

                    var url = 'api/IdasTrnSanctionDoc/GetConverseDoc';

                    SocketService.get(url).then(function (resp) {

                        $scope.uploaddocument = resp.data.uploaddocument;

                    });
                }
                else {
                    unlockUI();
                    Notify.alert('Error Occurred')

                }

            });
        }
        $scope.gotoBatch360 = function (sanction_gid)
        {
            localStorage.setItem('sanction_gid', sanction_gid);
            localStorage.setItem('page', 'Batch');
            $state.go('app.IdasTrnBatchView');
        }
        $scope.gotoBox360 = function (cartonbox_gid) {
            localStorage.setItem('cartonbox_gid', cartonbox_gid);

            $state.go('app.IdasTrnBoxDtlsView');
        }
        $scope.gotoDespatch360=function(despatch_gid)
        {
            localStorage.setItem('despatch_gid', despatch_gid);
          
            $state.go('app.IdasTrnDespatchDtlsView');

        }
        $scope.pendingBatch360 = function (sanction_gid) {
            localStorage.setItem('sanction_gid', sanction_gid);
            localStorage.setItem('page', 'pendingbatch');
            $state.go('app.IdasTrnBatchView');
        }

        $scope.DespatchSubmit = function () {
            var box_list = [];
            var box_gid;
           
            angular.forEach($scope.box_list, function (val) {

                if (val.checked == true) {
                    box_gid = val.cartonbox_gid;

                    box_list.push(box_gid);

                }
            });
          
            var params = {
                cartonbox_gid: box_list,
                despatch_date: $scope.user.txtDespatchDate,
                vendor_name:'Crown',
                contact_person: $scope.user.contactPerson,
                mobile_no: $scope.user.mobileNo,
                stampref_no: $scope.user.stampref_no,
                desptached_by: $scope.user.DespatchedBy,
                remarks: $scope.user.despatchRemarks

            }
          

            var url = 'api/IdasTrnFile2Despatch/CreateDespatch';
            lockUI()
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI()
                    $scope.tab.activeTabId = 'despatch';
                    activate();
                    Notify.alert(resp.data.message, 'success')
                }
                else {
                    unlockUI();

                    Notify.alert(resp.data.message)
                }
            });

        }

        $scope.UpdateBarCode = function (sanction_gid) {

            var modalInstance = $modal.open({
                templateUrl: '/updatebarcodenumberpopup.html',
                controller: ModalInstanceCtrl,
                size: 'md',
                backdrop: 'static',
                keyboard: false
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                var params = {
                    sanction_gid: sanction_gid,
                }
                var url = "api/IdasTrnFile2Despatch/EditBarCode";
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.customername = resp.data.customer_name;
                    $scope.customer_urn = resp.data.customer_urn;
                    $scope.barcoderef_no = resp.data.barcoderef_no;
                });

                $scope.update = function () {
                    var params = {
                        sanction_gid: sanction_gid,
                        barcoderef_no: $scope.barcoderef_no,
                    }
                    var url = "api/IdasTrnFile2Despatch/UpdateBarCode";
                    SocketService.post(url, params).then(function (resp) {
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
                                status: 'info',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                        }
                    });
                }
            }
        }

        $scope.importbatch = function () {
            var modalInstance = $modal.open({
                templateUrl: '/importBatch.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.exportbatchreport = function () {
                    lockUI();
                    var url = 'api/IdasTrnFile2Despatch/BatchExportExcel';
                    SocketService.get(url).then(function (resp) {
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
                            Notify.alert('Error Occurred While Export !', 'warning')
                        }
                    });
                }

                $scope.uploadbatch = function (val, val1, name) {
                  
                    var fileInput = document.getElementById('fileimport');
                    var filePath = fileInput.value;

                    $scope.fileinputvalue = filePath;

                    // Allowing file type
                    var allowedExtensions = /(\.xls|\.xlsx|\.csv)$/i;

                    if (!allowedExtensions.exec(filePath)) {
                        Notify.alert('File Format Not Supported!', 'warning')
                        $modalInstance.close('closed');
                    }
                    //else if (filePath.includes("ImportExcelIndividual") == false) {
                    //    Notify.alert('File Name / Template Not Supported!', 'warning')
                    //    $modalInstance.close('closed');
                    //}
                    else {
                        var item = {
                            name: val[0].name,
                            file: val[0]
                        };
                        var frm = new FormData();
                        frm.append('fileupload', item.file);
                        frm.append('file_name', item.name);
                        frm.append('project_flag', "Default");
                        $scope.uploadfrm = frm;
                    }
                }
                $scope.uploadExcelBatch = function () {

                    if ($scope.fileinputvalue == '' || $scope.fileinputvalue == undefined || $scope.fileinputvalue == null) {
                        Notify.alert('Kindly Select the Excel file', 'warning')
                    }
                    else {
                        var url = 'api/IdasTrnFile2Despatch/BatchImportExcel';
                        lockUI();
                        SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                            if (resp.data.status == true) {
                                unlockUI();
                                Notify.alert(resp.data.message, {
                                    status: 'success',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                $modalInstance.close('closed');
                                activate();
                                $state.go('app.idasTrnFile2Despatch');
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

                $scope.uploadExcelCancel = function () {
                    $("#fileimport").val('');
                };
            }
        }
    }
})();
