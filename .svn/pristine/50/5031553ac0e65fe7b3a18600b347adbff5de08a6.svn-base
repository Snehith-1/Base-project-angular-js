(function () {
    'use strict';

    angular
        .module('angle')
        .controller('idasTrnEditNocAndNdcController', idasTrnEditNocAndNdcController);

    idasTrnEditNocAndNdcController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams','cmnfunctionService'];

    function idasTrnEditNocAndNdcController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'idasTrnEditNocAndNdcController';
        $scope.nocandndc_gid = $location.search().lsnocandndc_gid;
        var nocandndc_gid = $scope.nocandndc_gid;
        activate();

        function activate() {
            var param = {
                nocandndc_gid: $scope.nocandndc_gid
            };
            var url = 'api/IdasNocAndNdc/GetNocDocumentEditList';
            SocketService.getparams(url, param).then(function (resp) {

                $scope.UploadDocumentList = resp.data.UploadNocDocumentList;
            });

            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.calender1 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.open1 = true;
            };
            vm.calender = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.open2 = true;
            };
            vm.calender3 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.open3 = true;
            };
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };
            var url = 'api/employee/employee';
            SocketService.get(url).then(function (resp) {
                $scope.employee_list = resp.data.employee_list;
            });

            var url = 'api/IdasNocAndNdc/TempClear';
            SocketService.get(url).then(function (resp) {
            });

            var url = 'api/IdasNocAndNdc/GetDropDown';
            SocketService.get(url).then(function (resp) {
                $scope.vertical_list = resp.data.vertical_list;
          
            });

            var param = {
                nocandndc_gid: $scope.nocandndc_gid
            }
            var url = 'api/IdasNocAndNdc/EditNoc';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.cbomaker = resp.data.maker_gid;
                $scope.cbochecker = resp.data.checker_gid;
                $scope.txtnocrequestdate = resp.data.nocandndc_date;
                $scope.txtcustomer_name = resp.data.customer_name;
                $scope.cboVertical = resp.data.vertical_gid;
             
                $scope.txtsanction_ref_no = resp.data.sanction_ref_no;
                $scope.txtsanctiondate = resp.data.sanction_date;
                $scope.txtloan_account_no = resp.data.loan_account_no;
                $scope.txtnocissuancedate = resp.data.noc_issuance_date;

            });
        }
        $scope.Nocclose = function () {
            $state.go('app.idasTrnNocAndNdc');

        }

        $scope.NocNDcDocumentUpload = function () {
            
            var fi = document.getElementById('file');
            if (fi.files.length > 0) {
                var frm = new FormData();
                for (var i = 0; i <= fi.files.length - 1; i++) {

                    frm.append(fi.files[i].name, fi.files[i]);
                    frm.append('project_flag', "Default");
                    $scope.uploadfrm = frm;
                    var fname = fi.files.item(i).name;
                    var fsize = fi.files.item(i).size;
                    var IsValidExtension = cmnfunctionService.fnCheckValidDocType(fname, "");

                    if (IsValidExtension == false) {
                        Notify.alert("File format is not supported..!", {
                            status: 'danger',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        return false;
                    }
                }
                frm.append('file_name', $scope.txtdoc_name);
                var url = 'api/IdasNocAndNdc/NocDocumentUpload';
                lockUI();
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                    $("#file").val('');

                    var param = {
                        nocandndc_gid: $scope.nocandndc_gid
                    };
                    var url = 'api/IdasNocAndNdc/GetNocDocumentTempEditList';
                    SocketService.getparams(url, param).then(function (resp) {

                        $scope.UploadDocumentList = resp.data.UploadNocDocumentList;
                    });


                    unlockUI();
                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    unlockUI();
                });
            }
            else {
                alert('Please select a file.')
                unlockUI();
            }
            $scope.txtdoc_name = '';
        }


        $scope.downloads = function (val1, val2) {
            var phyPath = val1;
            var relPath = phyPath.split("StoryboardAPI");
            var relpath1 = relPath[1].replace("\\", "/");
            var hosts = window.location.host;
            var prefix = location.protocol + "//";
            var str = prefix.concat(hosts, relpath1);
            var link = document.createElement("a");
            link.download = val2;
            var uri = str;
            link.href = uri;
            link.click();
        }
        $scope.uploaddocumentcancel = function (nocandndcdocument_gid) {
            var params = {
                nocandndcdocument_gid: nocandndcdocument_gid,
                nocandndc_gid: nocandndc_gid
            }
            var url = 'api/IdasNocAndNdc/GetNocDocumentDelete';
            SocketService.getparams(url, params).then(function (resp) {

                if (resp.data.status == true) {
                    var params = {
                        
                        nocandndc_gid: nocandndc_gid
                    }
                    var url = 'api/IdasNocAndNdc/GetNocDocumentTempEditList';
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.UploadDocumentList = resp.data.UploadNocDocumentList;
                    });

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                unlockUI();
            });
        }

        $scope.NocUpdate = function () {
            var lsmaker_name = "";
            var lschecker_name = "";
            var lsmaker_gid = "";
            var lschecker_gid = "";
            
            var vertical_Name = $('#Vertical :selected').text();

            if ($scope.cbomaker != undefined || $scope.cbomaker != null) {

                lsmaker_name = $('#maker_name :selected').text();

                lsmaker_gid = $scope.cbomaker;
            }
            if ($scope.cbochecker != undefined || $scope.cbochecker != null) {

                lschecker_name = $('#checker_name :selected').text();

                lschecker_gid = $scope.cbochecker;
            }
           
            
            var params = {
                nocandndc_gid: nocandndc_gid,
                maker_gid: lsmaker_gid,
                maker_name: lsmaker_name,
                checker_gid: lschecker_gid,
                checker_name: lschecker_name,
                
                vertical_gid: $scope.cboVertical,
                vertical_name: vertical_Name,

                customer_name: $scope.txtcustomer_name,
                sanction_ref_no: $scope.txtsanction_ref_no,
                sanction_date: $scope.txtsanctiondate,
                loan_account_no: $scope.txtloan_account_no,
                noc_issuance_date: $scope.txtnocissuancedate, 
                nocandndc_date: $scope.txtnocrequestdate
            }
            var url = 'api/IdasNocAndNdc/UpdateNoc';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'info',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                $state.go('app.idasTrnNocAndNdc');
            });
        }
    }
})();
